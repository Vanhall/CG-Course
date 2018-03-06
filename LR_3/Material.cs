using SharpGL;
using System;

namespace LR_3
{
    public class Material
    {
        OpenGL gl;

        private float[] ambient;
        private float[] diffuse;
        private float[] specular;
        private float shininess;

        [Flags]
        public enum ID
        {
            Bronze,
            Silver,
            Gold,
        }

        private ID kind;
        public ID Kind
        {
            get => kind;
            set
            {
                kind = value;
                switch (value)
                {
                    case ID.Bronze:
                        {
                            ambient = new float[] { 0.21f, 0.12f, 0.05f, 1f };
                            diffuse = new float[] { 0.71f, 0.42f, 0.18f, 1f };
                            specular = new float[] { 0.39f, 0.27f, 0.16f, 1f };
                            shininess = 26f;
                        } break;
                    case ID.Silver:
                        {
                            ambient = new float[] { 0.19f, 0.19f, 0.19f, 1f };
                            diffuse = new float[] { 0.5f, 0.5f, 0.5f, 1f };
                            specular = new float[] { 0.5f, 0.5f, 0.5f, 1f };
                            shininess = 51f;
                        }
                        break;
                    case ID.Gold:
                        {
                            ambient = new float[] { 0.24f, 0.19f, 0.07f, 1f };
                            diffuse = new float[] { 0.75f, 0.6f, 0.22f, 1f };
                            specular = new float[] { 0.62f, 0.55f, 0.36f, 1f };
                            shininess = 51f;
                        }
                        break;
                }
                Apply();
            }
        }

        public Material(OpenGL GL, ID ID)
        {
            gl = GL;
            Kind = ID;
        }
        
        public void Apply()
        {
            gl.Material(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_AMBIENT, ambient);
            gl.Material(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_DIFFUSE, diffuse);
            gl.Material(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_SPECULAR, specular);
            gl.Material(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_SHININESS, shininess);
        }
    }
}
