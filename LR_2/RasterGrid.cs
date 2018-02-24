using System;
using System.Collections.Generic;
using SharpGL;

namespace LR_2
{
    public class RasterGrid
    {
        OpenGL gl;
        int pixelSize;      // Размер пикселя
        public int PixelSize
        {
            get => pixelSize;
            set
            {
                pixelSize = value;
                cellSize = value + 1;
                GenerateGrid();
            }
        }

        int cellSize;       // Размер "ячейки" (пиксель + граница)
        public int CellSize { get => cellSize; }

        int width, height;  // Ширина и высота сетки
        int vertexCount;    // Количество вершин в VBO

        uint[] VBOPtr = new uint[2];    // Указатель VBO
        List<float> VBOGrid;            // Список вершин VBO

        // Конструктор --------------------------------------------------------
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

        // Изменение размеров сетки -------------------------------------------
        public void Resize(int Width, int Height)
        {
            width = Width;
            height = Height;
            GenerateGrid();
        }

        // Генерация сетки ----------------------------------------------------
        private void GenerateGrid()
        {
            VBOGrid.Clear();
            int correction = (width % 2 == 0) ? 1 : 0;
            for (int X = cellSize/2; X < width/2; X += cellSize)
            {
                VBOGrid.Add(X + correction);
                VBOGrid.Add(-height / 2);
                VBOGrid.Add(X + correction);
                VBOGrid.Add(height / 2);
                VBOGrid.Add(-X + correction);
                VBOGrid.Add(-height / 2);
                VBOGrid.Add(-X + correction);
                VBOGrid.Add(height / 2);
            }

            for (int Y = cellSize/2; Y < height/2; Y += cellSize)
            {
                VBOGrid.Add(-width / 2);
                VBOGrid.Add(Y);
                VBOGrid.Add(width / 2);
                VBOGrid.Add(Y);
                VBOGrid.Add(-width / 2);
                VBOGrid.Add(-Y);
                VBOGrid.Add(width / 2);
                VBOGrid.Add(-Y);
            }

            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[0]);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, VBOGrid.ToArray(), OpenGL.GL_DYNAMIC_DRAW);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);

            vertexCount = VBOGrid.Count / 2;
        }

        // Метод отрисовки ----------------------------------------------------
        public void Render()
        {
            gl.Color(new float[] { 0.5f, 0.5f, 0.5f });
            gl.LineWidth(1f);
            gl.EnableClientState(OpenGL.GL_VERTEX_ARRAY);

            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[0]);
            gl.VertexPointer(2, OpenGL.GL_FLOAT, 0, IntPtr.Zero);
            gl.DrawArrays(OpenGL.GL_LINES, 0, vertexCount);
            
            gl.DisableClientState(OpenGL.GL_VERTEX_ARRAY);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);
        }
    }
}
