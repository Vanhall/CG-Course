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

        public enum RenderFlags
        {
            None = 0,
            Surface = 1,
            Normal = 2,
            Flat = 4,
            Smooth = 8,
            Trajectory = 16,
            Sections = 32
        }

        public RenderFlags RenderMode;

        List<Vector> section = new List<Vector>();
        List<Vector> sections = new List<Vector>();
        List<Vector> trajectory = new List<Vector>();
        List<Vector> surface = new List<Vector>();
        Dictionary<int, List<Vector>> normals = new Dictionary<int, List<Vector>>();

        uint[] VBOPtr = new uint[5];
        List<float> VBOTrajectory = new List<float>();
        List<float> VBOSections = new List<float>();
        List<float> VBOSurface = new List<float>();
        List<float> VBONormals = new List<float>();
        List<float> VBOSmoothNormals = new List<float>();

        int surfVertices = 0;
        int trajVertices = 0;
        int sectVertices = 0;
        int segmVertices = 0;
        int capVertices = 0;

        public Model(OpenGL GL)
        {
            gl = GL;
            gl.GenBuffers(5, VBOPtr);
            ParseModelFile(@"Models/Spiral.xml");
            BuildTrajectory();
            BuildSections();
            BuildSurface();
            BuildNormals();
            BuildSmoothNormals();

            BindVBOs();
            RenderMode = RenderFlags.Surface | RenderFlags.Flat;
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
            foreach (Vector V in trajectory)
                VBOTrajectory.AddRange(V.ToArray());
        }

        private void BuildSections()
        {
            sectVertices = section.Count;
            capVertices = sectVertices + 1;


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
            segmVertices = (sectVertices - 1) * 6;
            // Добавляем первую "крышку"
            surface.Add(trajectory[0]);
            for (int i = 0; i < sectVertices; i++) surface.Add(sections[i]);

            // Добавляем сегменты
            for (int sPtr = 0; sPtr < sections.Count - sectVertices; sPtr += sectVertices)
            {
                int nextsPtr = sPtr + sectVertices;
                for (int i = 0; i < sectVertices - 1; i++)
                {
                    surface.Add(sections[sPtr + i]);
                    surface.Add(sections[nextsPtr + i]);
                    surface.Add(sections[sPtr + i + 1]);
                    surface.Add(sections[sPtr + i + 1]);
                    surface.Add(sections[nextsPtr + i]);
                    surface.Add(sections[nextsPtr + i + 1]);
                }
            }

            // Добавляем последнюю "крышку"
            surface.Add(trajectory[trajVertices - 1]);
            for (int i = sections.Count - sectVertices; i < sections.Count; i++) surface.Add(sections[i]);
            surfVertices = surface.Count;

            foreach (Vector V in surface) VBOSurface.AddRange(V.ToArray());
        }

        private void BuildNormals()
        {
            void AddNormal(int VertexID, Vector Normal)
            {
                if (!normals.ContainsKey(VertexID))
                    normals[VertexID] = new List<Vector> { Normal };
                else
                    normals[VertexID].Add(Normal);
            }
            
            // Первая "крышка"
            var normal = (trajectory[0] - trajectory[1]).Normalize();
            for (int i = 0; i < capVertices; i++)
            {
                VBOSurface.AddRange(normal.ToArray());
                VBONormals.AddRange(surface[i].ToArray());
                VBONormals.AddRange((surface[i] + normal).ToArray());
            }

            // Сегменты
            for (int sPtr = 0; sPtr < sections.Count - sectVertices; sPtr += sectVertices)
            {
                int nextsPtr = sPtr + sectVertices;
                for (int i = 0; i < sectVertices - 1; i++)
                {
                    var a = sections[sPtr + i];
                    var b = sections[sPtr + i + 1];
                    var c = sections[nextsPtr + i];
                    normal = ((c - a) ^ (b - a)).Normalize();
                    for (int k = 0; k < 3; k++)
                    {
                        VBOSurface.AddRange(normal.ToArray());
                    }
                    AddNormal(sPtr + i, normal);
                    AddNormal(sPtr + i + 1, normal);
                    AddNormal(nextsPtr + i, normal);

                    VBONormals.AddRange(a.ToArray());
                    VBONormals.AddRange((a + normal).ToArray());
                    VBONormals.AddRange(b.ToArray());
                    VBONormals.AddRange((b + normal).ToArray());
                    VBONormals.AddRange(c.ToArray());
                    VBONormals.AddRange((c + normal).ToArray());

                    a = sections[sPtr + i + 1];
                    b = sections[nextsPtr + i + 1];
                    c = sections[nextsPtr + i];
                    normal = ((c - a) ^ (b - a)).Normalize();
                    for (int k = 0; k < 3; k++)
                    {
                        VBOSurface.AddRange(normal.ToArray());
                    }
                    AddNormal(sPtr + i + 1, normal);
                    AddNormal(nextsPtr + i + 1, normal);
                    AddNormal(nextsPtr + i, normal);

                    VBONormals.AddRange(a.ToArray());
                    VBONormals.AddRange((a + normal).ToArray());
                    VBONormals.AddRange(b.ToArray());
                    VBONormals.AddRange((b + normal).ToArray());
                    VBONormals.AddRange(c.ToArray());
                    VBONormals.AddRange((c + normal).ToArray());
                }
            }

            // Последняя "крышка"
            normal = (trajectory[trajVertices - 1] - trajectory[trajVertices - 2]).Normalize();
            for (int i = surfVertices - capVertices; i < surfVertices; i++)
            {
                VBOSurface.AddRange(normal.ToArray());
                VBONormals.AddRange(surface[i].ToArray());
                VBONormals.AddRange((surface[i] + normal).ToArray());
            }
        }

        private void BuildSmoothNormals()
        {
            // Первая "крышка"
            for (int i = surfVertices * 3; i < (surfVertices + capVertices) * 3; i++)
                VBOSurface.Add(VBOSurface[i]);
            for (int i = 0; i < capVertices * 6; i++)
                VBOSmoothNormals.Add(VBONormals[i]);

            // Сглаживаем нормали
            foreach (int VertexID in normals.Keys)
            {
                var smoothNormal = new Vector(3);
                for (int i = 1; i < normals[VertexID].Count; i++)
                {
                    smoothNormal += normals[VertexID][i];
                }
                normals[VertexID].Clear();
                normals[VertexID].Add(smoothNormal.Normalize());
            }

            // "Зашиваем" стык (первая и последняя вершина сечения
            for (int sPtr = 0; sPtr < sections.Count; sPtr += sectVertices)
            {
                var smoothNormal = normals[sPtr][0] + normals[sPtr + sectVertices - 1][0];
                normals[sPtr][0] = smoothNormal.Normalize();
                normals[sPtr + sectVertices - 1][0] = smoothNormal.Normalize();
            }

            // Сегменты
            for (int sPtr = 0; sPtr < sections.Count - sectVertices; sPtr += sectVertices)
            {
                int nextsPtr = sPtr + sectVertices;
                for (int i = 0; i < sectVertices - 1; i++)
                {
                    VBOSurface.AddRange(normals[sPtr + i][0].ToArray());
                    VBOSurface.AddRange(normals[nextsPtr + i][0].ToArray());
                    VBOSurface.AddRange(normals[sPtr + i + 1][0].ToArray());

                    VBOSurface.AddRange(normals[sPtr + i + 1][0].ToArray());
                    VBOSurface.AddRange(normals[nextsPtr + i][0].ToArray());
                    VBOSurface.AddRange(normals[nextsPtr + i + 1][0].ToArray());
                }
            }
            
            for (int i = 0; i < sections.Count; i++)
            {
                VBOSmoothNormals.AddRange(sections[i].ToArray());
                VBOSmoothNormals.AddRange((sections[i] + normals[i][0]).ToArray());
            }

            // Последняя "крышка"
            for (int i = (surfVertices * 2 - capVertices) * 3; i < surfVertices * 6; i++)
                VBOSurface.Add(VBOSurface[i]);
            for (int i = (surfVertices - capVertices)*6; i < surfVertices*6; i++)
                VBOSmoothNormals.Add(VBONormals[i]);
        }

        private void BindVBOs()
        {
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[0]);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, VBOSections.ToArray(), OpenGL.GL_STATIC_DRAW);
            VBOSections.Clear();
            section.Clear();
            sections.Clear();
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[1]);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, VBOTrajectory.ToArray(), OpenGL.GL_STATIC_DRAW);
            VBOTrajectory.Clear();
            trajectory.Clear();
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[2]);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, VBOSurface.ToArray(), OpenGL.GL_STATIC_DRAW);
            VBOSurface.Clear();
            surface.Clear();
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[3]);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, VBONormals.ToArray(), OpenGL.GL_STATIC_DRAW);
            VBONormals.Clear();
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[4]);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, VBOSmoothNormals.ToArray(), OpenGL.GL_STATIC_DRAW);
            VBOSmoothNormals.Clear();
            normals.Clear();
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);
        }

        public void Render()
        {
            gl.EnableClientState(OpenGL.GL_VERTEX_ARRAY);
            
            // Нормали
            if ((RenderMode & RenderFlags.Normal) != 0 && (RenderMode & RenderFlags.Flat) != 0)
            {
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[3]);
                gl.VertexPointer(3, OpenGL.GL_FLOAT, 0, IntPtr.Zero);
                gl.Color(new float[] { 0.2f, 0.2f, 1f });
                gl.DrawArrays(OpenGL.GL_LINES, 0, surfVertices*2);
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);
            }

            // Сглаженные нормали
            if ((RenderMode & RenderFlags.Normal) != 0 && (RenderMode & RenderFlags.Smooth) != 0)
            {
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[4]);
                gl.VertexPointer(3, OpenGL.GL_FLOAT, 0, IntPtr.Zero);
                gl.Color(new float[] { 0f, 1f, 1f });
                gl.DrawArrays(OpenGL.GL_LINES, 0, (capVertices * 2 + trajVertices * sectVertices) * 2);
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);
            }

            // Поверхность
            if ((RenderMode & RenderFlags.Surface) != 0)
            {
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[2]);
                gl.EnableClientState(OpenGL.GL_NORMAL_ARRAY);
                gl.Enable(OpenGL.GL_LIGHTING);

                gl.VertexPointer(3, OpenGL.GL_FLOAT, 0, IntPtr.Zero);
                if ((RenderMode & RenderFlags.Flat) != 0)
                    gl.NormalPointer(OpenGL.GL_FLOAT, 0, (IntPtr)(surfVertices * 3 * sizeof(float)));
                else
                    gl.NormalPointer(OpenGL.GL_FLOAT, 0, (IntPtr)(surfVertices * 6 * sizeof(float)));

                gl.DrawArrays(OpenGL.GL_TRIANGLE_FAN, 0, capVertices);
                gl.DrawArrays(OpenGL.GL_TRIANGLES, capVertices, surfVertices - capVertices * 2);
                gl.DrawArrays(OpenGL.GL_TRIANGLE_FAN, surfVertices - capVertices, capVertices);

                gl.Disable(OpenGL.GL_LIGHTING);
                gl.DisableClientState(OpenGL.GL_NORMAL_ARRAY);
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);
            }

            // Траектория
            if ((RenderMode & RenderFlags.Trajectory) != 0)
            {
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[1]);
                gl.VertexPointer(3, OpenGL.GL_FLOAT, 0, IntPtr.Zero);

                gl.Color(new float[] { 1f, 0f, 1f });
                gl.Disable(OpenGL.GL_DEPTH_TEST);
                gl.DrawArrays(OpenGL.GL_LINE_STRIP, 0, trajVertices);

                gl.Color(new float[] { 1f, 1f, 0f });
                gl.PointSize(5f);
                gl.DrawArrays(OpenGL.GL_POINTS, 0, trajVertices);
                gl.Enable(OpenGL.GL_DEPTH_TEST);
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);
            }

            // Сечения
            if ((RenderMode & RenderFlags.Sections) != 0)
            {
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[0]);
                gl.VertexPointer(3, OpenGL.GL_FLOAT, 0, IntPtr.Zero);

                gl.Disable(OpenGL.GL_DEPTH_TEST);
                gl.Color(new float[] { 1f, 1f, 1f });
                for (int first = 0; first < sectVertices * trajVertices; first += sectVertices)
                    gl.DrawArrays(OpenGL.GL_LINE_STRIP, first, sectVertices);
                gl.Enable(OpenGL.GL_DEPTH_TEST);
                gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);
            }
            
            gl.DisableClientState(OpenGL.GL_VERTEX_ARRAY);
        }
    }
}
