using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using SharpGL;

namespace CG_Course
{
    public class Primitive
    {
        static int PrimitiveIndex = 0;              // счетчик созданных объектов

        OpenGL gl;
        int VertexCount = 0;                        // кол-во вершин
        uint[] VBOPtr = new uint[1];                // "указатель" VBO
        List<float> VBO = new List<float>();        // список координат вершин VBO
        public DataTable Coords = new DataTable();  // таблица для DataGridView
        public bool Active;                         // выбран ли сейчас этот объект
        float colorByteToFloat = 1f / 255f;         // коэффициент для конвертации RGB byte -> float


        int activeVertex;                           // индекс выбранной вершины
        public int ActiveVertex
        {
            get => activeVertex;
            set
            {
                if (value >= 0 && value < VBO.Count) activeVertex = value;
                else activeVertex = -1;
            }
        }
        
        public string Name { get; set; }            // имя объекта

        // Конструктор --------------------------------------------------------
        public Primitive(OpenGL GL, Color Color)
        {
            gl = GL;
            Name = String.Format("Object #{0}", ++PrimitiveIndex);
            gl.GenBuffers(1, VBOPtr);
            activeVertex = -1;
            Active = true;

            Coords.Columns.Add("X", typeof(float));
            Coords.Columns.Add("Y", typeof(float));
        }

        // Обновление VBO -----------------------------------------------------
        private void UpdateVBO()
        {
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[0]);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, VBO.ToArray(), OpenGL.GL_DYNAMIC_DRAW);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);
        }

        // Добавление вершины -------------------------------------------------
        public void AddVertex(Point Location, Color Color)
        {
            VBO.Add(Location.X);
            VBO.Add(Location.Y);
            VBO.Add(colorByteToFloat * Color.R);
            VBO.Add(colorByteToFloat * Color.G);
            VBO.Add(colorByteToFloat * Color.B);
            Coords.Rows.Add(Location.X, Location.Y);
            VertexCount++;
            UpdateVBO();
        }

        // Удаление вершины ---------------------------------------------------
        public void RemoveVertex(int index)
        {
            if (index >= 0 && index < VBO.Count / 5)
            {
                VBO.RemoveRange(index * 5, 5);
                Coords.Rows.RemoveAt(index);
                if (--VertexCount == 0) activeVertex = -1;
                UpdateVBO();
            }
        }

        // Перемещение вершины ------------------------------------------------
        public void MoveVertex(int newX, int newY)
        {
            if (activeVertex >= 0)
            {
                VBO[activeVertex * 5] = newX;
                VBO[activeVertex * 5 + 1] = newY;
                UpdateVBO();
            }
        }

        // Перекраска вершины -------------------------------------------------
        public void RecolorVertex(Color Color)
        {
            if (activeVertex >= 0)
            {
                VBO[activeVertex * 5 + 2] = colorByteToFloat * Color.R;
                VBO[activeVertex * 5 + 3] = colorByteToFloat * Color.G;
                VBO[activeVertex * 5 + 4] = colorByteToFloat * Color.B;
                UpdateVBO();
            }
        }

        // Заливка примитива --------------------------------------------------
        public void Fill(Color Color)
        {
            for (int i = 0; i < VertexCount * 5; i += 5)
            {
                VBO[i + 2] = colorByteToFloat * Color.R;
                VBO[i + 3] = colorByteToFloat * Color.G;
                VBO[i + 4] = colorByteToFloat * Color.B;
            }
            UpdateVBO();
        }

        // Метод отрисовки ----------------------------------------------------
        public void Render()
        {
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[0]);
            gl.VertexPointer(2, OpenGL.GL_FLOAT, 5 * sizeof(float), IntPtr.Zero);
            gl.ColorPointer(3, OpenGL.GL_FLOAT, 5 * sizeof(float), (IntPtr)(2 * sizeof(float)));

            gl.EnableClientState(OpenGL.GL_VERTEX_ARRAY);
            gl.EnableClientState(OpenGL.GL_COLOR_ARRAY);

            gl.DrawArrays(OpenGL.GL_TRIANGLE_FAN, 0, VertexCount);

            gl.DisableClientState(OpenGL.GL_COLOR_ARRAY);

            // Если объект выбран - рисуем вершины
            if (Active)
            {
                gl.PointSize(5f);
                gl.Color(new float[] { 0.8f, 0f, 0f });
                gl.DrawArrays(OpenGL.GL_POINTS, 0, VertexCount);
                if (activeVertex >= 0)
                {
                    gl.PointSize(10f);
                    gl.Color(new float[] { 1f, 0f, 0f });
                    gl.DrawArrays(OpenGL.GL_POINTS, activeVertex, 1);
                }
            }

            gl.DisableClientState(OpenGL.GL_VERTEX_ARRAY);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);
        }
    }
}
