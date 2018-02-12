using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using SharpGL;

namespace LR_2
{
    public class RasterGrid
    {
        OpenGL gl;
        int pixelSize;
        public int PixelSize
        {
            get => pixelSize;
            set
            {
                pixelSize = value;
                cellSize = value + 1;
            }
        }
        int cellSize;
        public int CellSize { get => cellSize; }

        int width, height;
        int gridVertexCount;

        uint[] VBOPtr = new uint[2];
        List<float> VBOGrid;

        public RasterGrid(OpenGLControl GLControl, int PixelSize)
        {
            gl = GLControl.OpenGL;
            width = GLControl.Width;
            height = GLControl.Height;
            pixelSize = PixelSize;
            cellSize = pixelSize + 1;

            gl.GenBuffers(2, VBOPtr);
            VBOGrid = new List<float>();
            GenerateGrid();
        }

        private void GenerateGrid()
        {
            VBOGrid.Clear();
            for (int X = 1; X < width; X += cellSize)
            {
                VBOGrid.Add(X);
                VBOGrid.Add(0);
                VBOGrid.Add(X);
                VBOGrid.Add(height);
            }
            for (int Y = 0; Y < height; Y += cellSize)
            {
                VBOGrid.Add(0);
                VBOGrid.Add(Y);
                VBOGrid.Add(width);
                VBOGrid.Add(Y);
            }

            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[0]);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, VBOGrid.ToArray(), OpenGL.GL_DYNAMIC_DRAW);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);

            gridVertexCount = VBOGrid.Count / 2;
        }
        public void Render()
        {
            gl.Color(new float[] { 0.5f, 0.5f, 0.5f });
            gl.LineWidth(1f);
            gl.EnableClientState(OpenGL.GL_VERTEX_ARRAY);

            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[0]);
            gl.VertexPointer(2, OpenGL.GL_FLOAT, 0, IntPtr.Zero);
            gl.DrawArrays(OpenGL.GL_LINES, 0, gridVertexCount);
            
            gl.DisableClientState(OpenGL.GL_VERTEX_ARRAY);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);
        }
    }
}
