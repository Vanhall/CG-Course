using System;
using System.Collections.Generic;
using System.Drawing;
using SharpGL;

namespace LR_2
{
    public class Hexagon
    {
        [Flags] public enum RenderFlags
        {
            None = 0,
            Outline = 1,
            Origin = 2,
            Points = 4,
            TranslationLines = 8,
            RotationLines = 16
        };
        public RenderFlags RenderMode;

        Point center;
        public Point Center
        {
            get => center;
            set
            {
                center = value;
                VBO[0] = center.X;
                VBO[1] = center.Y;
                UpdateVBO();
                UpdateTranslationVBO();
                UpdateRotationVBO();
            }
        }
        
        OpenGL gl;
        public Color FillColor;
        uint[] VBOPtr = new uint[3];                // "указатель" VBO
        float[] VBO = new float[32];        // список координат вершин VBO
        float[] VBOTransLines = new float[6];
        float[] VBORotLines = new float[16];
        public Texture Texture;

        const float minRadius = 20f;
        const int VertexCount = 8;

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

        float rotation;
        public float Rotation
        {
            get => rotation;
            set
            {
                rotation = value;
            }
        }

        public Hexagon(OpenGL GL, Point Center, Color Color)
        {
            gl = GL;
            center = Center;
            FillColor = Color;
            radius = minRadius;
            gl.GenBuffers(3, VBOPtr);
            VBO[0] = Center.X;
            VBO[1] = Center.Y;
            UpdateVBO();
            
            Texture = new Texture(gl, @"Textures/default.png");
            GenerateUV();

            translation = new Point(0, 0);
            UpdateTranslationVBO();

            rotation = 0f;
            UpdateRotationVBO();
        }

        private void UpdateVBO()
        {
            float angle = (float)(Math.PI / 3.0);
            for (int i = 2, step = 0; step < 6; step++, i += 2)
            {
                VBO[i] = radius * (float)Math.Cos(angle * step) + center.X;
                VBO[i + 1] = radius * (float)Math.Sin(angle * step) + center.Y;
            }
            VBO[14] = VBO[2];
            VBO[15] = VBO[3];

            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[0]);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, VBO, OpenGL.GL_DYNAMIC_DRAW);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);
        }

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

        private void UpdateTranslationVBO()
        {
            int cX = center.X, cY = center.Y;
            int tX = translation.X, tY = translation.Y;
            VBOTransLines[0] = cX;
            VBOTransLines[1] = cY;
            VBOTransLines[2] = tX + cX;
            VBOTransLines[3] = cY;
            VBOTransLines[4] = tX + cX;
            VBOTransLines[5] = tY + cY;

            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[1]);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, VBOTransLines, OpenGL.GL_DYNAMIC_DRAW);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);
        }

        private void UpdateRotationVBO()
        {
            int cX = center.X, cY = center.Y;
            
            VBORotLines[2] = radius;
            VBORotLines[6] = radius;
            VBORotLines[8] = radius;
            VBORotLines[10] = radius - 10f;
            VBORotLines[11] = 10f;
            VBORotLines[12] = radius;
            VBORotLines[14] = radius - 10f;
            VBORotLines[15] = -10f;

            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[2]);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, VBORotLines, OpenGL.GL_DYNAMIC_DRAW);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);
        }

        public void Render()
        {
            gl.Color(FillColor.R, FillColor.G, FillColor.B);

            gl.Enable(OpenGL.GL_TEXTURE_2D);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[0]);
            Texture.Bind();

            gl.VertexPointer(2, OpenGL.GL_FLOAT, 0, IntPtr.Zero);
            gl.TexCoordPointer(2, OpenGL.GL_FLOAT, 0, (IntPtr)(16 * sizeof(float)));

            gl.EnableClientState(OpenGL.GL_VERTEX_ARRAY);
            gl.EnableClientState(OpenGL.GL_TEXTURE_COORD_ARRAY);

            gl.PushMatrix();
            gl.Translate(center.X, center.Y, 0f);
            gl.Rotate(rotation, 0f, 0f, 1f);
            gl.Translate(-center.X, -center.Y, 0f);
            gl.Translate(translation.X, translation.Y, 0f);
            gl.DrawArrays(OpenGL.GL_TRIANGLE_FAN, 0, VertexCount);
            gl.PopMatrix();
            
            Texture.UnBind();
            gl.Disable(OpenGL.GL_TEXTURE_2D);
            gl.DisableClientState(OpenGL.GL_TEXTURE_COORD_ARRAY);

            if ((RenderMode & RenderFlags.Points) != 0)
            {
                gl.PointSize(5f);
                gl.Color(new float[] { 0.9f, 0f, 0f });
                gl.DrawArrays(OpenGL.GL_POINTS, 0, VertexCount);
            }

            if ((RenderMode & RenderFlags.Origin) != 0)
            {
                gl.PointSize(10f);
                gl.Color(new float[] { 1f, 1f, 1f });
                gl.DrawArrays(OpenGL.GL_POINTS, 0, 1);
            }

            if ((RenderMode & RenderFlags.Outline) != 0)
            {
                gl.Enable(OpenGL.GL_LINE_STIPPLE);
                gl.LineStipple(1, 0xF0F0);
                gl.Color(new float[] { 0f, 0f, 0f });
                gl.DrawArrays(OpenGL.GL_LINE_STRIP, 1, 7);
                gl.Disable(OpenGL.GL_LINE_STIPPLE);
            }

            if ((RenderMode & RenderFlags.TranslationLines) != 0)
            {
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[1]);
                gl.VertexPointer(2, OpenGL.GL_FLOAT, 0, IntPtr.Zero);
                gl.Enable(OpenGL.GL_LINE_STIPPLE);
                gl.LineStipple(1, 0xFFF0);
                gl.Color(new float[] { 0.8f, 0f, 0f });
                gl.DrawArrays(OpenGL.GL_LINE_STRIP, 0, 3);
                gl.Disable(OpenGL.GL_LINE_STIPPLE);
            }

            if ((RenderMode & RenderFlags.RotationLines) != 0)
            {
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[2]);
                gl.VertexPointer(2, OpenGL.GL_FLOAT, 0, IntPtr.Zero);
                gl.Enable(OpenGL.GL_LINE_STIPPLE);
                gl.LineStipple(1, 0xAFFA);

                gl.Color(new float[] { 0f, 0f, 0f });
                gl.PushMatrix();
                gl.Translate(center.X, center.Y, 0f);
                gl.DrawArrays(OpenGL.GL_LINES, 0, 2);

                gl.Color(new float[] { 0.8f, 0f, 0f });
                gl.Rotate(rotation, 0f, 0f, 1f);
                gl.DrawArrays(OpenGL.GL_LINES, 2, 6);
                gl.PopMatrix();
                gl.Disable(OpenGL.GL_LINE_STIPPLE);
            }

            gl.DisableClientState(OpenGL.GL_VERTEX_ARRAY);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);
        }
    }
}
