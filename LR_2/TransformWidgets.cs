using System;
using SharpGL;
using System.Drawing;

namespace LR_2
{
    public class TransformWidgets
    {
        OpenGL gl;

        public enum ActiveWidget
        {
            None = 0,
            Translation = 1,
            Rotation = 2,
            Scale = 4
        }

        public ActiveWidget Active;

        /*  VBO  */
        uint[] VBOPtr = new uint[3];            // указатели VBO
        float[] VBOTransLines = new float[6];   // VBO виджета смещения
        float[] VBORotLines = new float[16];    // VBO виджета вращения
        float[] VBOScaleArrows;                 // VBO виджета растяжения

        /*  Цвета  */
        float[] white = { 0.5f, 0.5f, 0.5f };         // Белый
        float[] red = { 1f, 0f, 0f };           // Красный

        // Радиус
        float radius;
        public float Radius
        {
            get => radius;
            set
            {
                radius = Math.Abs(value);
                UpdateRotationVBO();
            }
        }

        // Смещение
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
        
        // Поворот
        public float Rotation { get; set; }

        // Растяжение
        public PointF Scale { get; set; }

        public TransformWidgets(OpenGL GL, Hexagon H)
        {
            gl = GL;
            translation = new Point(0, 0);
            radius = 0;
            gl.GenBuffers(3, VBOPtr);
            Scale = new PointF(1f, 1f);
            UpdateTranslationVBO();
            UpdateRotationVBO();
            UpdateScaleVBO();
            Active = ActiveWidget.Translation;
            
            Radius = H.Radius;
            Translation = H.Translation;
            Rotation = H.Rotation;
            Scale = H.Scale;
        }

        public void SetTransform(Hexagon H)
        {
            Radius = H.Radius;
            Translation = H.Translation;
            Rotation = H.Rotation;
            Scale = H.Scale;
        }

        // Обновление VBO виджета смещения ------------------------------------
        private void UpdateTranslationVBO()
        {
            int tX = translation.X, tY = translation.Y;
            //VBOTransLines[1] = 1; // Задаем две линии
            VBOTransLines[2] = tX;  // параллельные OX и OY
            //VBOTransLines[3] = 1; //          o <-(центр объекта)     
            VBOTransLines[4] = tX;  //          |
            VBOTransLines[5] = tY;  // (0,0)----o <-(Х центра,1)

            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[0]);
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

            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[1]);
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

            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[2]);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, VBOScaleArrows, OpenGL.GL_STATIC_DRAW);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);
        }

        public void Render()
        {
            if ((Active & ActiveWidget.None) == 0)
            {
                gl.EnableClientState(OpenGL.GL_VERTEX_ARRAY);
                gl.LineWidth(2f);

                // Виджет смещения
                if ((Active & ActiveWidget.Translation) != 0)
                {
                    gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[0]);
                    gl.VertexPointer(2, OpenGL.GL_FLOAT, 0, IntPtr.Zero);

                    gl.Enable(OpenGL.GL_LINE_STIPPLE);
                    gl.LineStipple(1, 0xFFF0);
                    gl.Color(red);
                    gl.DrawArrays(OpenGL.GL_LINE_STRIP, 0, 3);

                    gl.PointSize(7f);
                    gl.Color(white);
                    gl.DrawArrays(OpenGL.GL_POINTS, 0, 3);

                    gl.Disable(OpenGL.GL_LINE_STIPPLE);
                    gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);
                }

                // Виджет углов поворота
                if ((Active & ActiveWidget.Rotation) != 0)
                {
                    gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[1]);
                    gl.VertexPointer(2, OpenGL.GL_FLOAT, 0, IntPtr.Zero);
                    gl.Enable(OpenGL.GL_LINE_STIPPLE);
                    gl.LineStipple(1, 0xAFFA);

                    gl.PushMatrix();
                    gl.Translate(translation.X, translation.Y, 0f);
                    gl.Color(white);
                    gl.DrawArrays(OpenGL.GL_LINES, 0, 2);
                    gl.Color(red);
                    gl.Rotate(Rotation, 0f, 0f, 1f);
                    gl.DrawArrays(OpenGL.GL_LINES, 2, 6);
                    gl.PopMatrix();

                    gl.Disable(OpenGL.GL_LINE_STIPPLE);
                    gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);
                }

                // Виджет растяжения
                if ((Active & ActiveWidget.Scale) != 0)
                {
                    gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[2]);
                    gl.VertexPointer(2, OpenGL.GL_FLOAT, 0, IntPtr.Zero);
                    gl.Color(red);

                    // "Стрелки" по OY
                    gl.PushMatrix();
                    gl.Translate(translation.X, translation.Y, 0f);
                    gl.Scale(1f, Scale.Y, 0f);
                    gl.DrawArrays(OpenGL.GL_TRIANGLES, 0, 6);
                    gl.PopMatrix();

                    // "Стрелки" по OX
                    gl.PushMatrix();
                    gl.Translate(translation.X, translation.Y, 0f);
                    gl.Scale(Scale.X, 1f, 0f);
                    gl.DrawArrays(OpenGL.GL_TRIANGLES, 6, 6);
                    gl.PopMatrix();

                    // Центр
                    gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[0]);
                    gl.VertexPointer(2, OpenGL.GL_FLOAT, 0, IntPtr.Zero);

                    gl.Color(white);
                    gl.PointSize(10f);
                    gl.PushMatrix();
                    gl.Translate(translation.X, translation.Y, 0f);
                    gl.DrawArrays(OpenGL.GL_POINTS, 0, 1);
                    gl.PopMatrix();

                    gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);
                }
                gl.DisableClientState(OpenGL.GL_VERTEX_ARRAY);
            }

        }
    }
}
