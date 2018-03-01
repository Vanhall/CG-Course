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
        List<Vector> sections = new List<Vector>();
        List<Vector> trajectory = new List<Vector>();
        List<Vector> surface = new List<Vector>();

        uint[] VBOPtr = new uint[3];
        float[] VBOTrajectory;
        List<float> VBOSections = new List<float>();
        List<float> VBOSurface = new List<float>();
        //List<float> VBOTemp = new List<float>();

        int surfVertices = 0;
        int trajVertices = 0;
        int sectVertices = 0;

        public Model(OpenGL GL)
        {
            gl = GL;
            gl.GenBuffers(3, VBOPtr);
            ParseModelFile(@"Models/Spiral.xml");
            BuildTrajectory();
            BuildSections();
            BuildSurface();

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
            
            var rotate = new Matrix(4);
            var translate = new Matrix(4);
            Matrix transform;
            
            // Первое сечение
            var transVec = trajectory[0];
            translate.SetIdentity();
            rotate.SetIdentity();
            var newNormal = trajectory[1] - transVec;
            var newUp = (trajectory[1] - transVec) ^ (trajectory[2] - transVec);
            if (newUp[Vector.Axis.Z] < 0) newUp = -newUp;
            var newSide = newUp ^ newNormal;

            rotate.InsertAt(0, newNormal.Normalize());
            rotate.InsertAt(1, newSide.Normalize());
            rotate.InsertAt(2, newUp.Normalize());
            translate.InsertAt(3, transVec);
            transform = translate * rotate;

            foreach (Vector V in section)
            {
                var vertex = new Vector(4, 1);
                vertex.CopyFrom(V);
                vertex = transform * vertex;
                VBOSections.AddRange(vertex.ToArray());
                
                sections.Add(new Vector(new double[] { vertex[0], vertex[1], vertex[2] }));
            }

            // Промежуточные сечения
            var prevUp = new Vector(3);
            for (int i = 1; i < trajectory.Count - 1; i++)
            {
                prevUp.CopyFrom(newUp);
                transVec = trajectory[i];
                translate.SetIdentity();
                rotate.SetIdentity();
                var nextVec = trajectory[i + 1] - transVec;
                var prevVec = transVec - trajectory[i - 1];
                newNormal = nextVec.Normalize() + prevVec.Normalize();
                newUp = prevVec ^ nextVec;
                if (newUp[Vector.Axis.Z] * prevUp[Vector.Axis.Z] < 0) newUp = -newUp;
                newSide = newUp ^ newNormal;

                rotate.InsertAt(0, newNormal.Normalize());
                rotate.InsertAt(1, newSide.Normalize());
                rotate.InsertAt(2, newUp.Normalize());
                
                translate.InsertAt(3, transVec);
                transform = translate * rotate;
                
                //VBOTemp.AddRange(transVec.ToArray());
                //VBOTemp.AddRange((newNormal.Normalize()+ transVec).ToArray());
                //VBOTemp.AddRange(transVec.ToArray());
                //VBOTemp.AddRange((newUp.Normalize() + transVec).ToArray());
                //VBOTemp.AddRange(transVec.ToArray());
                //VBOTemp.AddRange((newSide.Normalize() + transVec).ToArray());


                foreach (Vector V in section)
                {
                    var vertex = new Vector(4, 1);
                    vertex.CopyFrom(V);
                    vertex = transform * vertex;
                    VBOSections.AddRange(vertex.ToArray());
                    sections.Add(new Vector(new double[] { vertex[0], vertex[1], vertex[2] }));
                }
            }

            // Последнее сечение
            transVec = trajectory[trajVertices - 1];
            translate.SetIdentity();
            rotate.SetIdentity();
            newNormal = -(trajectory[trajVertices - 2] - transVec);
            newSide = newUp ^ newNormal;

            rotate.InsertAt(0, newNormal.Normalize());
            rotate.InsertAt(1, newSide.Normalize());
            rotate.InsertAt(2, newUp.Normalize());

            translate.InsertAt(3, transVec);
            transform = translate * rotate;

            foreach (Vector V in section)
            {
                var vertex = new Vector(4, 1);
                vertex.CopyFrom(V);
                vertex = transform * vertex;
                VBOSections.AddRange(vertex.ToArray());
                sections.Add(new Vector(new double[] { vertex[0], vertex[1], vertex[2] }));
            }
        }

        private void BuildSurface()
        {
            // Добавляем первую "крышку"
            surface.Add(trajectory[0]);
            for (int i = 0; i < sectVertices; i++) surface.Add(sections[i]);
            surface.Add(sections[0]);
            surfVertices = sectVertices + 1;

            // Добавляем сегменты
            for (int sPtr = 0; sPtr < sections.Count - sectVertices; sPtr += sectVertices)
            {
                int nextsPtr = sPtr + sectVertices;
                for (int i = 0; i < sectVertices; i++)
                {
                    surface.Add(sections[sPtr + i]);
                    surface.Add(sections[nextsPtr + i]);
                }
                surface.Add(sections[sPtr]);
                surface.Add(sections[nextsPtr]);
                surfVertices += sectVertices * 2 + 2;
            }

            // Добавляем последнюю "крышку"
            surface.Add(trajectory[trajVertices - 1]);
            for (int i = sections.Count - sectVertices; i < sections.Count; i++) surface.Add(sections[i]);
            surface.Add(sections[sections.Count - sectVertices]);
            surfVertices += sectVertices + 1;

            foreach (Vector V in surface) VBOSurface.AddRange(V.ToArray());
        }

        private void UpdateVBO()
        {
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[0]);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, VBOSections.ToArray(), OpenGL.GL_STATIC_DRAW);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[1]);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, VBOTrajectory, OpenGL.GL_STATIC_DRAW);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[2]);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, VBOSurface.ToArray(), OpenGL.GL_STATIC_DRAW);
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


            //gl.VertexPointer(3, OpenGL.GL_FLOAT, sectVertices * 3 * sizeof(float), IntPtr.Zero);
            //gl.Color(new float[] { 0f, 0.5f, 1f });
            //gl.DrawArrays(OpenGL.GL_LINE_STRIP, 0, trajVertices);
            //gl.VertexPointer(3, OpenGL.GL_FLOAT, sectVertices * 3 * sizeof(float), (IntPtr)(6 * sizeof(float)));
            //gl.Color(new float[] { 1f, 0.5f, 0f });
            //gl.DrawArrays(OpenGL.GL_LINE_STRIP, 0, trajVertices);
            //gl.VertexPointer(3, OpenGL.GL_FLOAT, sectVertices * 3 * sizeof(float), (IntPtr)(9 * sizeof(float)));
            //gl.Color(new float[] { 0f, 1f, 0.5f });
            //gl.DrawArrays(OpenGL.GL_LINE_STRIP, 0, trajVertices);


            // Траектория
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[1]);
            gl.VertexPointer(3, OpenGL.GL_FLOAT, 0, IntPtr.Zero);
            gl.Color(new float[] { 1f, 0f, 1f });
            gl.DrawArrays(OpenGL.GL_LINE_STRIP, 0, trajVertices);
            gl.Color(new float[] { 1f, 1f, 0f });
            gl.PointSize(5f);
            gl.DrawArrays(OpenGL.GL_POINTS, 0, trajVertices);

            // Поверхность
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[2]);
            gl.Color(new float[] { 0.8f, 0.5f, 0.5f });

            gl.VertexPointer(3, OpenGL.GL_FLOAT, 0, IntPtr.Zero);
            gl.DrawArrays(OpenGL.GL_TRIANGLE_FAN, 0, sectVertices + 2);

            int count = sectVertices * 2 + 2;
            for (int first = sectVertices + 2; first < surfVertices - sectVertices - 2; first += count)
                gl.DrawArrays(OpenGL.GL_TRIANGLE_STRIP, first, count);

            int lastCap = sectVertices + 2 + count * (trajVertices - 1);
            gl.DrawArrays(OpenGL.GL_TRIANGLE_FAN, lastCap, sectVertices + 2);


            gl.DisableClientState(OpenGL.GL_VERTEX_ARRAY);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);
        }
    }
}
