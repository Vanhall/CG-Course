using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using SharpGL;

namespace RGZ
{
    public class Scene
    {
        OpenGL gl;
        public Grid Grid;
        uint[] VBOPtr = new uint[2];
        float[] VBO;

        public bool DrawPoints;
        public bool DrawLines;
        public bool DrawGrid;

        Spline S;
        List<Point2D> ControlPoints;
        public DataTable CPData;
        bool EnoughPoints;
        public int ActivePoint;

        private int steps = 6;
        public int Steps
        {
            get => steps;
            set
            {
                steps = value;
                if (EnoughPoints) UpdateVBO();
            }
        }

        private int degree = 2;
        public int Degree
        {
            get => degree;
            set
            {
                degree = value;
                if (ControlPoints.Count <= value) EnoughPoints = false;
                else EnoughPoints = true;
                if (EnoughPoints)
                {
                    S = new Spline(ControlPoints.ToArray(), degree);
                    UpdateVBO();
                }
            }
        }

        public Color SplineColor;
        public Color PointsColor;
        public Color ActivePointColor;
        public Color LinesColor;

        public Scene(OpenGLControl GL)
        {
            gl = GL.OpenGL;
            ControlPoints = new List<Point2D>();
            EnoughPoints = false;
            gl.GenBuffers(2, VBOPtr);

            Grid = new Grid(gl, GL.Width, GL.Height);

            ActivePoint = -1;

            DrawPoints = true;
            DrawLines = true;
            DrawGrid = true;

            CPData = new DataTable();
            CPData.Columns.Add("X", typeof(int));
            CPData.Columns.Add("Y", typeof(int));

            SplineColor = Color.Red;
            PointsColor = Color.Black;
            ActivePointColor = Color.Magenta;
            LinesColor = Color.Blue;
        }

        public void AddControlPoint(Point P)
        {
            ControlPoints.Add(new Point2D(P));
            CPData.Rows.Add(P.X, P.Y);
            UpdatePointsVBO();
            if (ControlPoints.Count > degree)
            {
                EnoughPoints = true;
                S = new Spline(ControlPoints.ToArray(), degree);
                UpdateVBO();
            }
        }

        public void MoveControlPoint(Point P)
        {
            if (ActivePoint >= 0)
            {
                ControlPoints[ActivePoint] = new Point2D(P);
                UpdatePointsVBO();
                if (EnoughPoints)
                {
                    S = new Spline(ControlPoints.ToArray(), degree);
                    UpdateVBO();
                }
            }
        }

        public void RemoveControlPoint(int index)
        {
            ControlPoints.RemoveAt(index);
            CPData.Rows.RemoveAt(index);
            UpdatePointsVBO();
            if (ControlPoints.Count <= degree)
            {
                EnoughPoints = false;
                S = null;
            }
            else
            {
                S = new Spline(ControlPoints.ToArray(), degree);
                UpdateVBO();
            }
        }

        public void ClearControlPoints()
        {
            S = null;
            EnoughPoints = false;
            ControlPoints.Clear();
            UpdatePointsVBO();

            CPData.Clear();
        }

        void UpdateVBO()
        {
            VBO = S.CalcPoints(steps);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[0]);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, VBO, OpenGL.GL_STATIC_DRAW);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);
        }

        void UpdatePointsVBO()
        {
            var Points = new List<float>(ControlPoints.Count * 2);
            foreach (Point2D P in ControlPoints) Points.AddRange(P.ToArray());

            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[1]);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, Points.ToArray(), OpenGL.GL_STATIC_DRAW);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);
        }

        public void Render()
        {
            if (DrawGrid) Grid.Render();
            gl.EnableClientState(OpenGL.GL_VERTEX_ARRAY);

            if (EnoughPoints)
            {
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[0]);
                gl.VertexPointer(2, OpenGL.GL_FLOAT, 0, IntPtr.Zero);

                gl.LineWidth(1.5f);
                gl.Color(SplineColor.R, SplineColor.G, SplineColor.B);
                gl.DrawArrays(OpenGL.GL_LINE_STRIP, 0, VBO.Length / 2);
            }
            

            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[1]);
            gl.VertexPointer(2, OpenGL.GL_FLOAT, 0, IntPtr.Zero);

            if (DrawLines)
            {
                gl.Enable(OpenGL.GL_LINE_STIPPLE);
                gl.LineStipple(1, 0x0FF0);
                gl.LineWidth(1f);
                gl.Color(LinesColor.R, LinesColor.G, LinesColor.B);
                gl.DrawArrays(OpenGL.GL_LINE_STRIP, 0, ControlPoints.Count);
                gl.Disable(OpenGL.GL_LINE_STIPPLE);
            }

            if (DrawPoints)
            {
                gl.PointSize(5f);
                gl.Color(PointsColor.R, PointsColor.G, PointsColor.B);
                gl.DrawArrays(OpenGL.GL_POINTS, 0, ControlPoints.Count);

                if (ActivePoint >= 0)
                {
                    gl.PointSize(10f);
                    gl.Color(ActivePointColor.R, ActivePointColor.G, ActivePointColor.B);
                    gl.DrawArrays(OpenGL.GL_POINTS, ActivePoint, 1);
                }
            }

            gl.DisableClientState(OpenGL.GL_VERTEX_ARRAY);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);
        }
    }
}
