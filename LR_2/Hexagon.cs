﻿using SharpGL;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace LR_2
{
    public class Hexagon
    {
        static int hexagonIndex = 0;    // Счетчик созданных объектов

        const float minRadius = 20f;    // Минимальный радиус объекта
        const int vertexCount = 8;      // Число вершин

        OpenGL gl;
        public Texture Texture;         // Текстура
        public RasterGrid Raster;       // Сетка растеризации
        public Color FillColor;         // Цвет объекта

        /*  Флаги режимов рендеринга  */
        [Flags] public enum RenderFlags
        {
            None = 0,                               // Ничего
            Hexagon = 1,                            // Шестигранник + текстура
            Outline = 2,                            // Контур
            Center = 4,                             // Центральная точка
            TranslateWidget = 8,                    // Виджет смещения
            RotateWidget = 16,                      // Виджет вращения
            ScaleWidget = 32,                       // Виджет растяжения
            RasterOutline = 64,                     // Растеризованный контур
            RasterFill = 128,                       // Растеризованная заливка
            Raster = RasterOutline | RasterFill,    // Включена ли растеризация
            Translation = Center | TranslateWidget, // Режим "Смещение" (центр + виджет)
            Rotation = Center | RotateWidget,       // Режим "Вращение" (центр + виджет)
            Scale = Center | ScaleWidget | Outline  // Режим "Растяжение" (центр + виджет + контур)
        };
        public RenderFlags RenderMode;      // Флаги режимов рендеринга

        /*  Цвета  */
        float[] black = { 0f, 0f, 0f };         // Черный
        float[] white = { 1f, 1f, 1f };         // Белый
        float[] red = { 1f, 0f, 0f };           // Красный

        /*  VBO  */
        uint[] VBOPtr = new uint[5];            // указатели VBO
        float[] VBO = new float[32];            // VBO основной
        float[] VBOTransLines = new float[6];   // VBO виджета смещения
        float[] VBORotLines = new float[16];    // VBO виджета вращения
        float[] VBOScaleArrows;                 // VBO виджета растяжения

        /*  Структуры данных для растеризации  */
        List<float> VBORaster;          // VBO Растра (сетка пикселей)
        int pixOutlineCount = 0;        // Кол-во вершин (пикселей) растра
        HashSet<Point> pixOutline;      // Множество пикселей контура
        Dictionary<int, int> pixLeft;   // "левые" пиксели
        Dictionary<int, int> pixRight;  // "правые" пиксели
        
        string name;                    // имя объекта
        public string Name
        {
            get => name;
        }

        #region Параметры модельно-видовых преобразований
        // "Истинный" радиус объекта ------------------------------------------
        float radius;
        public float Radius
        {
            get => radius;
            set
            {
                float newRadius = Math.Abs(value);
                if (newRadius < minRadius) radius = minRadius;
                else radius = newRadius;
                UpdateVBO();
                UpdateRotationVBO();
                if ((RenderMode & RenderFlags.Raster) != 0)
                    Rasterize();
            }
        }

        // Смещение -----------------------------------------------------------
        Point translation;
        public Point Translation
        {
            get => translation;
            set
            {
                translation = value;
                UpdateTranslationVBO();
                if ((RenderMode & RenderFlags.Raster) != 0)
                    Rasterize();
            }
        }

        // Поворот ------------------------------------------------------------
        float rotation;
        public float Rotation
        {
            get => rotation;
            set
            {
                rotation = value;
                if ((RenderMode & RenderFlags.Raster) != 0)
                    Rasterize();
            }
        }

        // Растяжение ---------------------------------------------------------
        PointF scale;
        public PointF Scale
        {
            get => scale;
            set
            {
                scale = value;
                if ((RenderMode & RenderFlags.Raster) != 0)
                    Rasterize();
            }
        }
        #endregion

        // Конструктор --------------------------------------------------------
        public Hexagon(OpenGL GL, Point Center, Color Color, RasterGrid Raster)
        {
            gl = GL;
            name = String.Format("Object #{0}", ++hexagonIndex);
            translation = Center;
            FillColor = Color;
            radius = minRadius;
            gl.GenBuffers(5, VBOPtr);
            Texture = new Texture(gl, @"Textures/default.png");
            scale = new PointF(1f, 1f);
            this.Raster = Raster;
            VBORaster = new List<float>();
            pixOutline = new HashSet<Point>();
            pixLeft = new Dictionary<int, int>();
            pixRight = new Dictionary<int, int>();
            GenerateUV();
            UpdateVBO();
            UpdateTranslationVBO();
            UpdateRotationVBO();
            UpdateScaleVBO();
            Rasterize();
        }

        #region Генерация VBO
        // Обновление основного VBO -------------------------------------------
        private void UpdateVBO()
        {
            float angle = (float)(Math.PI / 3.0);
            for (int i = 2, step = 0; step < 6; step++, i += 2)
            {
                VBO[i] = radius * (float)Math.Cos(angle * step);
                VBO[i + 1] = radius * (float)Math.Sin(angle * step);
            }
            VBO[14] = VBO[2];
            VBO[15] = VBO[3];

            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[0]);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, VBO, OpenGL.GL_DYNAMIC_DRAW);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);
        }

        // Генерация текстурных координат -------------------------------------
        private void GenerateUV()
        {
            VBO[16] = 0.5f;
            VBO[17] = 0.5f;
            float angle = (float)(Math.PI / 3.0);
            for (int i = 18, step = 0; step < 6; step++, i += 2)
            {
                VBO[i] = 0.5f * (float)Math.Cos(angle * step) + 0.5f;
                VBO[i + 1] = 0.5f * (float)Math.Sin(angle * step) + 0.5f;
            }
            VBO[30] = VBO[18];
            VBO[31] = VBO[19];
        }

        // Обновление VBO виджета смещения ------------------------------------
        private void UpdateTranslationVBO()
        {
            int tX = translation.X, tY = translation.Y;
            VBOTransLines[1] = 1;   // Задаем две линии
            VBOTransLines[2] = tX;  // параллельные OX и OY
            VBOTransLines[3] = 1;   //          o <-(центр объекта)     
            VBOTransLines[4] = tX;  //          |
            VBOTransLines[5] = tY;  // (0,1)----o <-(Х центра,1)

            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[1]);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, VBOTransLines, OpenGL.GL_DYNAMIC_DRAW);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);
        }

        // Обновление VBO виджета угла поворота -------------------------------
        private void UpdateRotationVBO()
        {
            VBORotLines[2] = radius;        // VBO радиус-линии
            VBORotLines[6] = radius;        // и стрелки для
            VBORotLines[8] = radius;        // визуализации угла поворота
            VBORotLines[10] = radius - 10f; //     o
            VBORotLines[11] = 10f;          //      \
            VBORotLines[12] = radius;       //o======o
            VBORotLines[14] = radius - 10f; //      /
            VBORotLines[15] = -10f;         //     o

            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[2]);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, VBORotLines, OpenGL.GL_DYNAMIC_DRAW);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);
        }

        // Обновление VBO виджета растяжения ----------------------------------
        private void UpdateScaleVBO()
        {
            // Задаем 4 треугольника
            VBOScaleArrows = new float[] {
                -10f, 15f, 0f, 20f, 10f, 15f,       // верхний
                -10f, -15f, 0f, -20f, 10f, -15f,    // нижний
                15f, 10f, 20f, 0f, 15f, -10f,       // правый
                -15f, 10f, -20f, 0f, -15f, -10f     // левый
            };

            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[3]);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, VBOScaleArrows, OpenGL.GL_STATIC_DRAW);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);
        }

        // Метод растеризации -------------------------------------------------
        public void Rasterize()
        {
            VBORaster.Clear();      // Очищаем VBO растра,
            pixOutline.Clear();     // можество пикселей,
            pixLeft.Clear();        // словари "левых"
            pixRight.Clear();       // и "правых" пикселей
            pixOutlineCount = 0;

            // Высчитываем параметры модельно-видовых преобразований
            float cos = (float)Math.Cos(rotation * Math.PI / 180.0);
            float sin = (float)Math.Sin(rotation * Math.PI / 180.0);
            float Sx = Scale.X, Sy = Scale.Y;
            float Tx = translation.X, Ty = translation.Y;
            float x0, y0, x1, y1;       // World-Space координаты
            int xt0, yt0, xt1, yt1;     // Преобразованные координаты

            // "Ручное" модельно-видовое преобразование (для OXY)
            void transform(float x, float y, out int xt, out int yt)
            {
                xt = (int)(x * cos * Sx - y * sin * Sy + Tx);
                yt = (int)(x * sin * Sx + y * cos * Sy + Ty);
            }

            // Растеризуем контур
            for (int i = 2; i < 13; i += 2)
            {
                x0 = VBO[i];
                y0 = VBO[i + 1];
                x1 = VBO[i + 2];
                y1 = VBO[i + 3];
                transform(x0, y0, out xt0, out yt0);    // Преобразуем
                transform(x1, y1, out xt1, out yt1);    // координаты и
                BresenhamLine(xt0, yt0, xt1, yt1);      // растеризуем Брезенхемом
            }

            // Заливка
            int step = Raster.CellSize;
            foreach (int y in pixLeft.Keys) // Для всех пикселей в "левом" словаре берем
            {                               // соответствующие "правые" и заполняем все что между
                int x = pixLeft[y];
                while (x < pixRight[y])
                {
                    if (!pixOutline.Contains(new Point(x, y)))  // Учитываем пиксели контура
                    {
                        VBORaster.Add(x);
                        VBORaster.Add(y);
                    }
                    x += step;
                }
            }

            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[4]);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, VBORaster.ToArray(), OpenGL.GL_DYNAMIC_DRAW);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);
        }
        #endregion

        // Растеризация отрезка по Брезенхэму (для любого отрезка) ------------
        void BresenhamLine(int x0, int y0, int x1, int y1)
        {
            void Swap(ref int X, ref int Y)
            {
                int Temp = X;
                X = Y;
                Y = Temp;
            }
            
            // Проверяем рост отрезка по OX и по OY
            bool slope = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);
            if (slope)
            {
                Swap(ref x0, ref y0);
                Swap(ref x1, ref y1);
            }

            // Если линия растёт не слева направо,
            // то меняем начало и конец отрезка местами
            if (x0 > x1)
            {
                Swap(ref x0, ref x1);
                Swap(ref y0, ref y1);
            }

            // Приводим координаты в центр пикселя
            if (x0 % Raster.CellSize != Raster.CellSize / 2)
                x0 -= (x0 % Raster.CellSize) - Raster.CellSize / 2;
            if (y0 % Raster.CellSize != Raster.CellSize / 2)
                y0 -= (y0 % Raster.CellSize) - Raster.CellSize / 2;
            if (x1 % Raster.CellSize != Raster.CellSize / 2)
                x1 -= (x1 % Raster.CellSize) - Raster.CellSize / 2;
            if (y1 % Raster.CellSize != Raster.CellSize / 2)
                y1 -= (y1 % Raster.CellSize) - Raster.CellSize / 2;

            int dx = x1 - x0;
            int dy = Math.Abs(y1 - y0);
            int error = dx / 2; // Здесь домножаем ошибку на dx, чтобы избавиться от дробей
            int ystep = (y0 < y1) ? Raster.CellSize : -Raster.CellSize; // Выбираем направление роста координаты y
            int y = y0;
            for (int x = x0; x <= x1; x += Raster.CellSize)
            {
                Point Pixel = new Point(slope ? y : x, slope ? x : y);

                // Заносим в "левый" словарь
                if (pixLeft.ContainsKey(Pixel.Y))
                {
                    if (pixLeft[Pixel.Y] > Pixel.X) pixLeft[Pixel.Y] = Pixel.X;
                }
                else
                    pixLeft.Add(Pixel.Y, Pixel.X);

                // Заносим в "правый" словарь
                if (pixRight.ContainsKey(Pixel.Y))
                {
                    if (pixRight[Pixel.Y] < Pixel.X) pixRight[Pixel.Y] = Pixel.X;
                }
                else
                    pixRight.Add(Pixel.Y, Pixel.X);

                // Если такого пикселя еще нет, добавляем его в VBO
                if (pixOutline.Add(Pixel))
                {
                    VBORaster.Add(Pixel.X);
                    VBORaster.Add(Pixel.Y);
                    pixOutlineCount++;
                }

                error -= dy;
                if (error < 0)
                {
                    y += ystep;
                    error += dx;
                }
            }
        }

        // Метод отрисовки ----------------------------------------------------
        public void Render()
        {
            gl.EnableClientState(OpenGL.GL_VERTEX_ARRAY);
            gl.LineWidth(2f);

            // Шестиугольник
            if ((RenderMode & RenderFlags.Hexagon) != 0)
            {
                gl.Color(FillColor.R, FillColor.G, FillColor.B);

                gl.Enable(OpenGL.GL_TEXTURE_2D);
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[0]);
                Texture.Bind();

                gl.VertexPointer(2, OpenGL.GL_FLOAT, 0, IntPtr.Zero);
                gl.TexCoordPointer(2, OpenGL.GL_FLOAT, 0, (IntPtr)(16 * sizeof(float)));
                gl.EnableClientState(OpenGL.GL_TEXTURE_COORD_ARRAY);

                gl.PushMatrix();
                gl.Translate(translation.X, translation.Y, 0f);
                gl.Rotate(rotation, 0f, 0f, 1f);
                gl.Scale(scale.X, scale.Y, 0f);
                gl.DrawArrays(OpenGL.GL_TRIANGLE_FAN, 0, vertexCount);
                gl.PopMatrix();

                Texture.Unbind();
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);
                gl.Disable(OpenGL.GL_TEXTURE_2D);
                gl.DisableClientState(OpenGL.GL_TEXTURE_COORD_ARRAY);
            }

            // Растеризация (контур)
            if ((RenderMode & RenderFlags.RasterOutline) != 0)
            {
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[4]);
                gl.Color(FillColor.R, FillColor.G, FillColor.B);
                gl.PointSize(Raster.PixelSize);
                gl.VertexPointer(2, OpenGL.GL_FLOAT, 0, IntPtr.Zero);
                gl.DrawArrays(OpenGL.GL_POINTS, 0, pixOutlineCount);
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);
            }

            // Растеризация (заливка)
            if ((RenderMode & RenderFlags.RasterFill) != 0)
            {
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[4]);
                gl.Color(FillColor.R, FillColor.G, FillColor.B);
                gl.PointSize(Raster.PixelSize);
                gl.VertexPointer(2, OpenGL.GL_FLOAT, 0, IntPtr.Zero);
                gl.DrawArrays(OpenGL.GL_POINTS, 0, VBORaster.Count / 2);
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);
            }
            
            // Центр
            if ((RenderMode & RenderFlags.Center) != 0)
            {
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[0]);
                gl.VertexPointer(2, OpenGL.GL_FLOAT, 0, IntPtr.Zero);
                gl.PushMatrix();
                gl.Translate(translation.X, translation.Y, 0f);
                gl.PointSize(10f);
                gl.Color(white);
                gl.DrawArrays(OpenGL.GL_POINTS, 0, 1);
                gl.PopMatrix();
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);
            }

            // Контур
            if ((RenderMode & RenderFlags.Outline) != 0)
            {
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[0]);
                gl.VertexPointer(2, OpenGL.GL_FLOAT, 0, IntPtr.Zero);
                gl.PushMatrix();
                gl.Translate(translation.X, translation.Y, 0f);
                gl.Enable(OpenGL.GL_LINE_STIPPLE);
                gl.LineStipple(1, 0xF0F0);
                gl.Color(black);
                gl.DrawArrays(OpenGL.GL_LINE_STRIP, 1, 7);
                gl.Disable(OpenGL.GL_LINE_STIPPLE);
                gl.PopMatrix();
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);
            }

            // Виджет смещения
            if ((RenderMode & RenderFlags.TranslateWidget) != 0)
            {
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[1]);
                gl.VertexPointer(2, OpenGL.GL_FLOAT, 0, IntPtr.Zero);
                gl.Enable(OpenGL.GL_LINE_STIPPLE);
                gl.LineStipple(1, 0xFFF0);
                gl.Color(red);
                gl.DrawArrays(OpenGL.GL_LINE_STRIP, 0, 3);
                gl.Disable(OpenGL.GL_LINE_STIPPLE);
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);
            }

            // Виджет углов поворота
            if ((RenderMode & RenderFlags.RotateWidget) != 0)
            {
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[2]);
                gl.VertexPointer(2, OpenGL.GL_FLOAT, 0, IntPtr.Zero);
                gl.Enable(OpenGL.GL_LINE_STIPPLE);
                gl.LineStipple(1, 0xAFFA);

                gl.PushMatrix();
                gl.Translate(translation.X, translation.Y, 0f);
                gl.Color(black);
                gl.DrawArrays(OpenGL.GL_LINES, 0, 2);
                gl.Color(red);
                gl.Rotate(rotation, 0f, 0f, 1f);
                gl.DrawArrays(OpenGL.GL_LINES, 2, 6);
                gl.PopMatrix();

                gl.Disable(OpenGL.GL_LINE_STIPPLE);
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);
            }

            // Виджет растяжения
            if ((RenderMode & RenderFlags.ScaleWidget) != 0)
            {
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[3]);
                gl.VertexPointer(2, OpenGL.GL_FLOAT, 0, IntPtr.Zero);
                gl.Color(red);

                // "Стрелки" по OY
                gl.PushMatrix();
                gl.Translate(translation.X, translation.Y, 0f);
                gl.Scale(1f, scale.Y, 0f);
                gl.DrawArrays(OpenGL.GL_TRIANGLES, 0, 6);
                gl.PopMatrix();

                // "Стрелки" по OX
                gl.PushMatrix();
                gl.Translate(translation.X, translation.Y, 0f);
                gl.Scale(scale.X, 1f, 0f);
                gl.DrawArrays(OpenGL.GL_TRIANGLES, 6, 6);
                gl.PopMatrix();

                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);
            }

            gl.DisableClientState(OpenGL.GL_VERTEX_ARRAY);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);
        }
    }
}
