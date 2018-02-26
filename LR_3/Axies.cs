using System;
using SharpGL;

namespace LR_3
{
    public class Axies
    {
        OpenGL gl;
        
        uint[] VBOPtr = new uint[2];
        float[] VBOLines;
        float[] VBOArrows;

        public Axies(OpenGL GL, float Length)
        {
            gl = GL;

            gl.GenBuffers(2, VBOPtr);
            VBOLines = new float[]
            {// координата          цвет
                0f, 0f, 0f,         1f, 0f, 0f, //+OX
                Length, 0f, 0f,     1f, 0f, 0f,
                0f, 0f, 0f,         0f, 1f, 0f, //+OY
                0f, Length, 0f,     0f, 1f, 0f,
                0f, 0f, 0f,         0f, 0f, 1f, //+OZ
                0f, 0f, Length,     0f, 0f, 1f,
                0f, 0f, 0f,         1f, 0f, 0f, //-OX
                -Length, 0f, 0f,    1f, 0f, 0f,
                0f, 0f, 0f,         0f, 1f, 0f, //-OY
                0f, -Length, 0f,    0f, 1f, 0f,
                0f, 0f, 0f,         0f, 0f, 1f, //-OZ
                0f, 0f, -Length,    0f, 0f, 1f,
            };

            float r = 1f;
            float dl = Length - 4f;
            float cos = r * (float)Math.Cos(Math.PI / 4.0);
            VBOArrows = new float[]
            {
                Length, 0f, 0f,
                dl, 0f, r,
                dl, cos, cos,
                dl, r, 0f,
                dl, cos, -cos,
                dl, 0f, -r,
                dl, -cos, -cos,
                dl, -r, 0f,
                dl, -cos, cos,
                dl, 0f, r,
            };

            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[0]);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, VBOLines, OpenGL.GL_STATIC_DRAW);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[1]);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, VBOArrows, OpenGL.GL_STATIC_DRAW);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);
        }

        public void Render()
        {
            gl.LineWidth(2f);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[0]);

            gl.VertexPointer(3, OpenGL.GL_FLOAT, 6 * sizeof(float), IntPtr.Zero);
            gl.ColorPointer(3, OpenGL.GL_FLOAT, 6 * sizeof(float), (IntPtr)(3 * sizeof(float)));

            gl.EnableClientState(OpenGL.GL_VERTEX_ARRAY);
            gl.EnableClientState(OpenGL.GL_COLOR_ARRAY);

            gl.DrawArrays(OpenGL.GL_LINES, 0, 6);

            gl.Enable(OpenGL.GL_LINE_STIPPLE);
            gl.LineStipple(1, 0xFF00);
            gl.DrawArrays(OpenGL.GL_LINES, 6, 6);
            gl.Disable(OpenGL.GL_LINE_STIPPLE);

            gl.DisableClientState(OpenGL.GL_COLOR_ARRAY);

            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[1]);
            gl.VertexPointer(3, OpenGL.GL_FLOAT, 0, IntPtr.Zero);
            gl.Color(new float[] { 1f, 0f, 0f });
            gl.DrawArrays(OpenGL.GL_TRIANGLE_FAN, 0, 10);

            gl.PushMatrix();
            gl.Color(new float[] { 0f, 1f, 0f });
            gl.Rotate(0f, 0f, 90f);
            gl.DrawArrays(OpenGL.GL_TRIANGLE_FAN, 0, 10);

            gl.Color(new float[] { 0f, 0f, 1f });
            gl.Rotate(0f, -90f, 0f);
            gl.DrawArrays(OpenGL.GL_TRIANGLE_FAN, 0, 10);
            gl.PopMatrix();

            gl.DisableClientState(OpenGL.GL_VERTEX_ARRAY);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);
        }
    }
}
