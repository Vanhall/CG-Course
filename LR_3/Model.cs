using System;
using System.Globalization;
using System.Collections.Generic;
using System.Xml;
using SharpGL;

namespace LR_3
{
    public class Model
    {
        OpenGL gl;

        List<Vector> section = new List<Vector>();
        List<Vector> trajectory = new List<Vector>();

        uint[] VBOPtr = new uint[2];
        float[] VBOTrajectory;
        List<float> VBOSections = new List<float>();

        int vertexCount = 0;
        int trajVertices = 0;
        int sectVertices = 0;

        public Model(OpenGL GL)
        {
            gl = GL;
            gl.GenBuffers(2, VBOPtr);
            ParseModelFile(@"Models/Model1.xml");
            BuildTrajectory();
            BuildSections();

            UpdateVBO();
        }

        private void ParseModelFile(string path)
        {

            var inputFile = new XmlDocument();
            var dblFormat = CultureInfo.InvariantCulture;
            inputFile.Load(path);
            
            var node = inputFile.SelectSingleNode("model/section");
            foreach (XmlNode vertex in node.ChildNodes)
            {
                var Vec = new Vector(3);
                Vec[0] = double.Parse(vertex.Attributes[0].Value, dblFormat);
                Vec[1] = double.Parse(vertex.Attributes[1].Value, dblFormat);
                Vec[2] = double.Parse(vertex.Attributes[2].Value, dblFormat);
                section.Add(Vec);
            }

            node = inputFile.SelectSingleNode("model/trajectory");
            foreach (XmlNode vertex in node.ChildNodes)
            {
                var Vec = new Vector(3);
                Vec[0] = double.Parse(vertex.Attributes[0].Value, dblFormat);
                Vec[1] = double.Parse(vertex.Attributes[1].Value, dblFormat);
                Vec[2] = double.Parse(vertex.Attributes[2].Value, dblFormat);
                trajectory.Add(Vec);
            }
        }

        private void BuildTrajectory()
        {
            trajVertices = trajectory.Count;
            VBOTrajectory = new float[trajVertices * 3];
            int i = 0;
            foreach (Vector V in trajectory)
            {
                VBOTrajectory[i] = (float)V[Vector.Axis.X];
                VBOTrajectory[i + 1] = (float)V[Vector.Axis.Y];
                VBOTrajectory[i + 2] = (float)V[Vector.Axis.Z];
                i += 3;
            }
        }

        private void BuildSections()
        {
            sectVertices = section.Count;


            var OX = new Vector(new double[] { 1, 0, 0 });
            var OZ = new Vector(new double[] { 0, 0, 1 });
            var rotate = new Matrix(4);
            var translate = new Matrix(4);
            Matrix transform;
            
            // Первое сечение
            var transVec = trajectory[0];
            var nextVec = trajectory[1] - transVec;
            double angle = Vector.AngleBetween(nextVec, OZ);
            var rotationAxis = (OZ ^ nextVec).Normalize();
            translate.SetIdentity();

            rotate.GenerateRotation(angle, rotationAxis);
            translate.InsertAt(3, transVec);
            transform = translate * rotate;

            foreach (Vector V in section)
            {
                var vertex = new Vector(4, 1);
                vertex.CopyFrom(V);
                vertex = transform * vertex;
                VBOSections.AddRange(vertex.ToArray());
                vertexCount++;
            }
            
            // Промежуточные
            for (int k = 1; k < trajectory.Count - 1; k++)
            {
                transVec = trajectory[k];
                nextVec = (trajectory[k + 1] - transVec).Normalize() + (transVec - trajectory[k - 1]).Normalize();
                angle = Vector.AngleBetween(nextVec, OZ);
                rotationAxis = (OZ ^ nextVec).Normalize();
                translate.SetIdentity();

                rotate.GenerateRotation(angle, rotationAxis);
                translate.InsertAt(3, transVec);
                transform = translate * rotate;

                foreach (Vector V in section)
                {
                    var vertex = new Vector(4, 1);
                    vertex.CopyFrom(V);
                    vertex = transform * vertex;
                    VBOSections.AddRange(vertex.ToArray());
                    vertexCount++;
                }
            }

            // Последнее сечение
            transVec = trajectory[trajVertices - 1];
            nextVec = -(trajectory[trajVertices - 2] - transVec);
            angle = Vector.AngleBetween(nextVec, OZ);
            rotationAxis = (OZ ^ nextVec).Normalize();
            translate.SetIdentity();

            rotate.GenerateRotation(angle, rotationAxis);
            translate.InsertAt(3, transVec);
            transform = translate * rotate;

            foreach (Vector V in section)
            {
                var vertex = new Vector(4, 1);
                vertex.CopyFrom(V);
                vertex = transform * vertex;
                VBOSections.AddRange(vertex.ToArray());
                vertexCount++;
            }
        }

        private void UpdateVBO()
        {
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[0]);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, VBOSections.ToArray(), OpenGL.GL_STATIC_DRAW);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[1]);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, VBOTrajectory, OpenGL.GL_STATIC_DRAW);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);
        }

        public void Render()
        {
            gl.EnableClientState(OpenGL.GL_VERTEX_ARRAY);

            // Сечения
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[0]);
            gl.VertexPointer(3, OpenGL.GL_FLOAT, 0, IntPtr.Zero);

            int totalVertices = sectVertices * trajVertices;
            gl.Color(new float[] { 1f, 1f, 1f });
            for (int first = 0; first < totalVertices; first += sectVertices)
                gl.DrawArrays(OpenGL.GL_LINE_LOOP, first, sectVertices);


            gl.VertexPointer(3, OpenGL.GL_FLOAT, sectVertices * 3 * sizeof(float), IntPtr.Zero);
            gl.Color(new float[] { 0f, 0.5f, 1f });
            gl.DrawArrays(OpenGL.GL_LINE_STRIP, 0, trajVertices);
            gl.VertexPointer(3, OpenGL.GL_FLOAT, sectVertices * 3 * sizeof(float), (IntPtr)(6 * sizeof(float)));
            gl.Color(new float[] { 1f, 0.5f, 0f });
            gl.DrawArrays(OpenGL.GL_LINE_STRIP, 0, trajVertices);
            gl.VertexPointer(3, OpenGL.GL_FLOAT, sectVertices * 3 * sizeof(float), (IntPtr)(9 * sizeof(float)));
            gl.Color(new float[] { 0f, 1f, 0.5f });
            gl.DrawArrays(OpenGL.GL_LINE_STRIP, 0, trajVertices);


            // Траектория
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[1]);
            gl.VertexPointer(3, OpenGL.GL_FLOAT, 0, IntPtr.Zero);
            gl.Color(new float[] { 1f, 0f, 1f });
            gl.DrawArrays(OpenGL.GL_LINE_STRIP, 0, trajVertices);
            gl.Color(new float[] { 1f, 1f, 0f });
            gl.PointSize(5f);
            gl.DrawArrays(OpenGL.GL_POINTS, 0, trajVertices);
            
            gl.DisableClientState(OpenGL.GL_VERTEX_ARRAY);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);
        }
    }
}
