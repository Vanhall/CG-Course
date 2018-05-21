using System;
using System.Collections.Generic;
using SharpGL;

namespace RGZ
{
    public class Grid
    {
        OpenGL gl;
        int Step;
        int W, H;
        int vertexCount;
        int FontSize, LabelPadding;

        uint[] VBOPtr = new uint[1];    // Указатель VBO
        List<float> VBO;            // Список вершин VBO

        internal struct CoordLabel { public int X; public int Y; public string Text; }
        List<CoordLabel> Labels;

        public Grid(OpenGL GL, int Width, int Height)
        {
            gl = GL;
            Step = 50;
            W = Width;
            H = Height;
            VBO = new List<float>();

            FontSize = 12;
            LabelPadding = 2;
            Labels = new List<CoordLabel>();

            gl.GenBuffers(1, VBOPtr);

            GenerateGrid();
        }

        void GenerateGrid()
        {
            VBO.Clear();
            Labels.Clear();
            int w2 = W / 2, h2 = H / 2;
            int XLabelOffset = LabelPadding + w2;
            int YLabelOffset = -LabelPadding - FontSize + h2;

            VBO.AddRange(new float[] { 0f, h2, 0, -h2, w2, 0, -w2, 0 });
            Labels.Add(new CoordLabel
            {
                X = XLabelOffset,
                Y = YLabelOffset,
                Text = "0"
            });

            int L = Math.Max(w2, h2);
            for (int t = Step; t < L; t += Step)
            {
                VBO.AddRange(new float[]
                {
                    t, h2, t, -h2,
                    -t, h2, -t, -h2,
                    w2, t, -w2, t,
                    w2, -t, -w2, -t,
                }
                );

                Labels.AddRange(new CoordLabel[]
                {
                    new CoordLabel
                    {
                        X = t + XLabelOffset,
                        Y = YLabelOffset,
                        Text = t.ToString()
                    },
                    new CoordLabel
                    {
                        X = -t + XLabelOffset,
                        Y = YLabelOffset,
                        Text = (-t).ToString()
                    },
                    new CoordLabel
                    {
                        X = XLabelOffset,
                        Y = t + YLabelOffset,
                        Text = t.ToString()
                    },
                    new CoordLabel
                    {
                        X = XLabelOffset,
                        Y = -t + YLabelOffset,
                        Text = (-t).ToString()
                    }
                });
            }
            vertexCount = VBO.Count / 2;

            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[0]);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, VBO.ToArray(), OpenGL.GL_DYNAMIC_DRAW);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);
        }

        public void Resize(int Width, int Height)
        {
            W = Width;
            H = Height;
            GenerateGrid();
        }

        public void Render()
        {
            foreach (var Label in Labels)
                gl.DrawText(
                    Label.X, Label.Y,
                    0, 0, 0, "Consolas",
                    FontSize,
                    Label.Text
                    );

            gl.Color(new float[] { 0.8f, 0.8f, 0.8f });
            gl.EnableClientState(OpenGL.GL_VERTEX_ARRAY);

            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[0]);
            gl.VertexPointer(2, OpenGL.GL_FLOAT, 0, IntPtr.Zero);

            gl.LineWidth(1.5f);
            gl.DrawArrays(OpenGL.GL_LINES, 0, 4);

            gl.LineWidth(1f);
            gl.DrawArrays(OpenGL.GL_LINES, 4, vertexCount - 4);

            gl.DisableClientState(OpenGL.GL_VERTEX_ARRAY);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);
        }
    }
}
