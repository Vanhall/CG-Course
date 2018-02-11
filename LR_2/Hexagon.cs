﻿using System;
using System.Drawing;
using SharpGL;

namespace LR_2
{
    public class Hexagon
    {
        static int hexagonIndex = 0;    // Счетчик созданных объектов
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
            Translation = Center | TranslationLines | Hexagon,
            Rotation = Center | RotationLines | Hexagon,
            Scale = Center | ScaleArrows | Outline | Hexagon
        };
        public RenderFlags RenderMode;  // Флаги режимов рендеринга
        
        OpenGL gl;
        public Color FillColor;
        uint[] VBOPtr = new uint[4];            // указатели VBO
        float[] VBO = new float[32];            // VBO шестигранника
        float[] VBOTransLines = new float[6];   // VBO линий смещения
        float[] VBORotLines = new float[16];    // VBO линий вращения
        float[] VBOScaleArrows;                 // VBO стрелок растяжения
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
        public Hexagon(OpenGL GL, Point Center, Color Color)
        {
            gl = GL;
            name = String.Format("Object #{0}", ++hexagonIndex);
            translation = Center;
            FillColor = Color;
            radius = minRadius;
            gl.GenBuffers(4, VBOPtr);
            Texture = new Texture(gl, @"Textures/default.png");
            scale = new PointF(1f, 1f);
            GenerateUV();
            UpdateVBO();
            UpdateTranslationVBO();
            UpdateRotationVBO();
            UpdateScaleVBO();
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
        #endregion

        // Метод отрисовки ----------------------------------------------------
        public void Render()
        {
            gl.EnableClientState(OpenGL.GL_VERTEX_ARRAY);

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
            
            gl.DisableClientState(OpenGL.GL_VERTEX_ARRAY);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);
        }
    }
}
