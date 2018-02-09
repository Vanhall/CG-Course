using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using SharpGL;

namespace CG_Course
{
    public class Primitive
    {
        OpenGL gl;

        uint[] VBOPtr = new uint[1];                    // "указатель" VBO
        int VertexCount = 0;                            // кол-во вершин
        List<float> VBO = new List<float>();            // список координат вершин VBO
        public DataTable Coords = new DataTable();      // таблица для DataGridView 

        public static int PrimitiveIndex = 0;           // счетчик созданных объектов
        public Color FillColor;                         // цвет примитива
        int activeVertex;                               // индекс выбранной вершины
        public int ActiveVertex
        {
            get => activeVertex;
            set
            {
                if (value >= 0 && value < VBO.Count) activeVertex = value;
                else activeVertex = -1;
            }
        }
        public bool Active;                           // выбран ли сейчас этот объект

        // имя
        string name;
        public string Name
        {
            get => name;
        }

        // Конструктор --------------------------------------------------------
        public Primitive(OpenGL GL, Color Col)
        {
            gl = GL;
            FillColor = Col;
            name = String.Format("Object #{0}", ++PrimitiveIndex);
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
        public void AddVertex(float x, float y)
        {
            VBO.Add(x);
            VBO.Add(y);
            Coords.Rows.Add(x, y);
            VertexCount++;
            UpdateVBO();
        }

        // Удаление вершины ---------------------------------------------------
        public void RemoveVertex(int index)
        {
            if (index >= 0)
            {
                VBO.RemoveRange(index * 2, 2);
                VertexCount--;
                Coords.Rows.RemoveAt(index);
                UpdateVBO();
                if (VertexCount == 0) activeVertex = -1;
            }
        }

        // Изменение вершины --------------------------------------------------
        public void EditVertex(int newX, int newY)
        {
            if (activeVertex >= 0)
            {
                VBO[activeVertex * 2] = newX;
                VBO[activeVertex * 2 + 1] = newY;
                UpdateVBO();
            }
        }

        // Метод отрисовки ----------------------------------------------------
        public void Render()
        {
            gl.Color(FillColor.R, FillColor.G, FillColor.B);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[0]);
            gl.VertexPointer(2, OpenGL.GL_FLOAT, 0, IntPtr.Zero);
            gl.EnableClientState(OpenGL.GL_VERTEX_ARRAY);
            gl.DrawArrays(OpenGL.GL_TRIANGLE_FAN, 0, VertexCount);

            // Если объект выбран - рисуем вершины
            if (Active)
            {
                gl.PointSize(5f);
                gl.Color(new float[] { 0.9f, 0f, 0f });
                gl.DrawArrays(OpenGL.GL_POINTS, 0, VertexCount);
                if (activeVertex >= 0)
                {
                    gl.PointSize(10f);
                    gl.Color(new float[] { 1f, 1f, 1f });
                    gl.DrawArrays(OpenGL.GL_POINTS, activeVertex, 1);
                }
            }

            gl.DisableClientState(OpenGL.GL_VERTEX_ARRAY);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);
        }
    }
}
