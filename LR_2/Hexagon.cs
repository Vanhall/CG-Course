using System;
using System.Drawing;
using System.Collections.Generic;
using SharpGL;

namespace LR_2
{
    public class Hexagon
    {
        static int hexagonIndex = 0;    // Счетчик созданных объектов
        RasterGrid raster;
        public RasterGrid Raster
        {
            get => raster;
            set
            {
                raster = value;
            }
        }

        // Флаги режимов рендеринга
        [Flags] public enum RenderFlags
        {
            None = 0,
            Hexagon = 1,
            Outline = 2,
            Center = 4,
            TranslationLines = 8,
            RotationLines = 16,
            ScaleArrows = 32,
            Raster = 64,
            Translation = Center | TranslationLines | Hexagon,
            Rotation = Center | RotationLines | Hexagon,
            Scale = Center | ScaleArrows | Outline | Hexagon
        };
        public RenderFlags RenderMode;  // Флаги режимов рендеринга
        
        OpenGL gl;
        public Color FillColor;
        uint[] VBOPtr = new uint[5];            // указатели VBO
        float[] VBO = new float[32];            // VBO шестигранника
        float[] VBOTransLines = new float[6];   // VBO линий смещения
        float[] VBORotLines = new float[16];    // VBO линий вращения
        float[] VBOScaleArrows;                 // VBO стрелок растяжения
        List<float> VBORaster;
        public Texture Texture;                 // Текстура

        const float minRadius = 20f;            // Минимальный радиус объекта
        const int vertexCount = 8;              // Число вершин

        string name;                            // имя объекта
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
            raster = Raster;
            VBORaster = new List<float>();
            GenerateUV();
            UpdateVBO();
            UpdateTranslationVBO();
            UpdateRotationVBO();
            UpdateScaleVBO();
            UpdateRasterVBO();
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

        // Обновление VBO линий смещения --------------------------------------
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

        // Обновление VBO линий угла поворота ---------------------------------
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

        // Обновление VBO стрелок растяжения ----------------------------------
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

        private void UpdateRasterVBO()
        {
            VBORaster.Clear();
            float[] lines = new float[14];
            for (int i = 0; i < lines.Length; i += 2)
            {
                float X = VBO[i + 2], Y = VBO[i + 3], Sx = Scale.X, Sy = Scale.Y;
                float cos = (float)Math.Cos(rotation * Math.PI / 180.0);
                float sin = (float)Math.Sin(rotation * Math.PI / 180.0);
                lines[i] = X*cos*Sx - Y*sin*Sy + translation.X;
                lines[i + 1] = X*sin*Sx + Y*cos*Sy + translation.Y;
            }

            for (int i = 0; i < lines.Length - 2; i += 2)
            {
                BresenhamLine((int)lines[i], (int)lines[i + 1], (int)lines[i + 2], (int)lines[i + 3]);
            }

            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[4]);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, VBORaster.ToArray(), OpenGL.GL_DYNAMIC_DRAW);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);
        }
        #endregion

        public void Rasterize()
        {
            UpdateRasterVBO();
            RenderMode = RenderFlags.Raster;
        }

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

            if (x0 % raster.CellSize != raster.CellSize / 2)
                x0 -= (x0 % raster.CellSize) - raster.CellSize / 2;
            if (y0 % raster.CellSize != raster.CellSize / 2)
                y0 -= (y0 % raster.CellSize) - raster.CellSize / 2;

            int dx = x1 - x0;
            int dy = Math.Abs(y1 - y0);
            int error = dx / 2; // Здесь используется оптимизация с умножением на dx, чтобы избавиться от лишних дробей
            int ystep = (y0 < y1) ? raster.CellSize : -raster.CellSize; // Выбираем направление роста координаты y
            int y = y0;
            for (int x = x0; x <= x1; x += raster.CellSize)
            {
                VBORaster.Add(slope ? y : x);
                VBORaster.Add(slope ? x : y);
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
                gl.Disable(OpenGL.GL_TEXTURE_2D);
                gl.DisableClientState(OpenGL.GL_TEXTURE_COORD_ARRAY);
            }
            
            // Центр
            if ((RenderMode & RenderFlags.Center) != 0)
            {
                gl.PushMatrix();
                gl.Translate(translation.X, translation.Y, 0f);
                gl.PointSize(10f);
                gl.Color(new float[] { 1f, 1f, 1f });
                gl.DrawArrays(OpenGL.GL_POINTS, 0, 1);
                gl.PopMatrix();
            }

            // Контур
            if ((RenderMode & RenderFlags.Outline) != 0)
            {
                gl.PushMatrix();
                gl.Translate(translation.X, translation.Y, 0f);
                gl.Enable(OpenGL.GL_LINE_STIPPLE);
                gl.LineStipple(1, 0xF0F0);
                gl.Color(new float[] { 0f, 0f, 0f });
                gl.DrawArrays(OpenGL.GL_LINE_STRIP, 1, 7);
                gl.Disable(OpenGL.GL_LINE_STIPPLE);
                gl.PopMatrix();
            }

            // Линии смещения
            if ((RenderMode & RenderFlags.TranslationLines) != 0)
            {
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[1]);
                gl.VertexPointer(2, OpenGL.GL_FLOAT, 0, IntPtr.Zero);
                gl.Enable(OpenGL.GL_LINE_STIPPLE);
                gl.LineStipple(1, 0xFFF0);
                gl.Color(new float[] { 1f, 0f, 0f });
                gl.DrawArrays(OpenGL.GL_LINE_STRIP, 0, 3);
                gl.Disable(OpenGL.GL_LINE_STIPPLE);
            }
            
            // Линии углов поворота
            if ((RenderMode & RenderFlags.RotationLines) != 0)
            {
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[2]);
                gl.VertexPointer(2, OpenGL.GL_FLOAT, 0, IntPtr.Zero);
                gl.Enable(OpenGL.GL_LINE_STIPPLE);
                gl.LineStipple(1, 0xAFFA);

                gl.PushMatrix();
                gl.Translate(translation.X, translation.Y, 0f);
                gl.Color(new float[] { 0f, 0f, 0f });
                gl.DrawArrays(OpenGL.GL_LINES, 0, 2);
                gl.Color(new float[] { 1f, 0f, 0f });
                gl.Rotate(rotation, 0f, 0f, 1f);
                gl.DrawArrays(OpenGL.GL_LINES, 2, 6);
                gl.PopMatrix();

                gl.Disable(OpenGL.GL_LINE_STIPPLE);
            }

            // Стрелки смещения
            if ((RenderMode & RenderFlags.ScaleArrows) != 0)
            {
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[3]);
                gl.VertexPointer(2, OpenGL.GL_FLOAT, 0, IntPtr.Zero);

                gl.Color(new float[] { 1f, 0f, 0f });
                gl.PushMatrix();
                gl.Translate(translation.X, translation.Y, 0f);
                gl.Scale(1f, scale.Y, 0f);
                gl.DrawArrays(OpenGL.GL_TRIANGLES, 0, 6);
                gl.PopMatrix();

                gl.PushMatrix();
                gl.Translate(translation.X, translation.Y, 0f);
                gl.Scale(scale.X, 1f, 0f);
                gl.DrawArrays(OpenGL.GL_TRIANGLES, 6, 6);
                gl.PopMatrix();
            }

            // Растеризация
            if ((RenderMode & RenderFlags.Raster) != 0)
            {
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[4]);
                gl.Color(FillColor.R, FillColor.G, FillColor.B);
                gl.PointSize(raster.PixelSize);
                gl.VertexPointer(2, OpenGL.GL_FLOAT, 0, IntPtr.Zero);
                gl.DrawArrays(OpenGL.GL_POINTS, 0, VBORaster.Count);
            }

            gl.DisableClientState(OpenGL.GL_VERTEX_ARRAY);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);
        }
    }
}
