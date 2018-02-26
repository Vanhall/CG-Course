using System;
using System.Collections.Generic;
using SharpGL;

namespace LR_3
{
    public class Model
    {
        OpenGL gl;
        uint[] VBOPtr = new uint[2];
        List<float> VBO = new List<float>();
        float[] VBOTrajectory;
        int VertexCount = 0;

        public Model(OpenGL GL)
        {
            gl = GL;
            gl.GenBuffers(2, VBOPtr);
            BuildModel();
        }

        private void BuildModel()
        {
            var OZ = new Vector(new double[] { 0, 0, 1 });

            var section = new List<Vector>();
            var trajectory = new List<Vector>();
            section.Add(new Vector(new double[] { 2.0, 0, 0 }));
            section.Add(new Vector(new double[] { -2.0, 1.0, 0 }));
            section.Add(new Vector(new double[] { -2.0, -1.0, 0 }));
            trajectory.Add(new Vector(new double[] { 0, 0, 0 }));
            trajectory.Add(new Vector(new double[] { 2.0, 4.0, 0 }));
            trajectory.Add(new Vector(new double[] { 7.0, 6.0, 2.0 }));
            trajectory.Add(new Vector(new double[] { 7.0, 15.0, 0 }));
            trajectory.Add(new Vector(new double[] { 20.0, 6.0, 0 }));

            var rotate = new Matrix(4);
            var translate = new Matrix(4);
            Matrix transform;
            var rotationAxis = new Vector(3);

            
            var transVec = trajectory[0];
            
            translate.SetIdentity();

            double angle = Vector.AngleBetween(trajectory[1] - transVec, OZ);
            rotationAxis = (OZ ^ (trajectory[1] - transVec)).Normalize();

            rotate.GenerateRotation(angle, rotationAxis);
            translate.InsertAt(3, transVec);

            transform = translate * rotate;
            foreach (Vector V in section)
            {
                var vertex = new Vector(4, 1);
                vertex.CopyFrom(V);
                vertex = transform * vertex;
                VBO.Add((float)vertex[Vector.Axis.X]);
                VBO.Add((float)vertex[Vector.Axis.Y]);
                VBO.Add((float)vertex[Vector.Axis.Z]);
                VertexCount++;
            }

            for (int k = 1; k < trajectory.Count - 1; k++)
            {
                transVec = trajectory[k];
                translate.SetIdentity();
                var nextVec = (trajectory[k + 1] - transVec).Normalize() + (transVec - trajectory[k - 1]).Normalize();

                angle = Vector.AngleBetween(nextVec, OZ);
                rotationAxis = (OZ ^ nextVec).Normalize();

                rotate.GenerateRotation(angle, rotationAxis);
                translate.InsertAt(3, transVec);

                transform = translate * rotate;
                foreach (Vector V in section)
                {
                    var vertex = new Vector(4, 1);
                    vertex.CopyFrom(V);
                    vertex = transform * vertex;
                    VBO.Add((float)vertex[Vector.Axis.X]);
                    VBO.Add((float)vertex[Vector.Axis.Y]);
                    VBO.Add((float)vertex[Vector.Axis.Z]);
                    VertexCount++;
                }
            }
            
            VBOTrajectory = new float[trajectory.Count * 3];
            int i = 0;
            foreach (Vector V in trajectory)
            {
                VBOTrajectory[i] = (float)V[Vector.Axis.X];
                VBOTrajectory[i+1] = (float)V[Vector.Axis.Y];
                VBOTrajectory[i+2] = (float)V[Vector.Axis.Z];
                i += 3;
            }
            UpdateVBO();
        }

        private void UpdateVBO()
        {
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[0]);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, VBO.ToArray(), OpenGL.GL_STATIC_DRAW);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[1]);
            gl.BufferData(OpenGL.GL_ARRAY_BUFFER, VBOTrajectory, OpenGL.GL_STATIC_DRAW);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);
        }

        public void Render()
        {
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[0]);
            gl.VertexPointer(3, OpenGL.GL_FLOAT, 0, IntPtr.Zero);
            gl.EnableClientState(OpenGL.GL_VERTEX_ARRAY);

            gl.Color(new float[] { 1f, 1f, 1f });
            gl.DrawArrays(OpenGL.GL_LINE_LOOP, 0, 3);
            gl.DrawArrays(OpenGL.GL_LINE_LOOP, 3, 3);
            gl.DrawArrays(OpenGL.GL_LINE_LOOP, 6, 3);
            gl.DrawArrays(OpenGL.GL_LINE_LOOP, 9, 3);



            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, VBOPtr[1]);
            gl.VertexPointer(3, OpenGL.GL_FLOAT, 0, IntPtr.Zero);
            gl.Color(new float[] { 1f, 0f, 1f });
            gl.DrawArrays(OpenGL.GL_LINE_STRIP, 0, 5);
            gl.Color(new float[] { 1f, 1f, 0f });
            gl.PointSize(5f);
            gl.DrawArrays(OpenGL.GL_POINTS, 0, 5);



            gl.DisableClientState(OpenGL.GL_VERTEX_ARRAY);
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER, 0);
        }
    }
}
