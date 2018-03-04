using SharpGL;

namespace LR_3
{
    public class Scene
    {
        OpenGL gl;
        double FOV, zNear, zFar;

        public Camera Cam;
        public Axies Axies;
        public bool ShowAxies;
        public Model Model;
        float[] pos = { 20f, 20f, 20f, 0f };
        int W, H;

        private double orthoFactor;
        public double OrthoFactor
        {
            get => orthoFactor;
            set
            {
                orthoFactor = 25.0 + (50.0 - value);
                if (ortho)
                {
                    gl.MatrixMode(OpenGL.GL_PROJECTION);
                    gl.LoadIdentity();
                    gl.Ortho(
                        -W / orthoFactor, W / orthoFactor,
                        -H / orthoFactor, H / orthoFactor,
                        zNear - orthoFactor, zFar - orthoFactor);
                    gl.MatrixMode(OpenGL.GL_MODELVIEW);
                }
            }
        }

        private bool ortho;
        public bool Ortho
        {
            get => ortho;
            set
            {
                if (!value)
                {
                    gl.MatrixMode(OpenGL.GL_PROJECTION);
                    gl.LoadIdentity();
                    gl.Perspective(FOV, (double)W / (double)H, zNear, zFar);
                    gl.MatrixMode(OpenGL.GL_MODELVIEW);
                }
                else
                {
                    gl.MatrixMode(OpenGL.GL_PROJECTION);
                    gl.LoadIdentity();
                    gl.Ortho(
                        -W / orthoFactor, W / orthoFactor,
                        -H / orthoFactor, H / orthoFactor,
                        zNear - orthoFactor, zFar - orthoFactor);
                    gl.MatrixMode(OpenGL.GL_MODELVIEW);
                }
                ortho = value;
            }
        }

        public Scene(OpenGLControl GLControl, double FOV, double zNear, double zFar)
        {
            gl = GLControl.OpenGL;
            this.FOV = FOV;
            this.zNear = zNear;
            this.zFar = zFar;

            W = GLControl.Width;
            H = GLControl.Height;
            Ortho = false;

            gl.ClearColor(0.7f, 0.7f, 0.8f, 1.0f);

            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();
            gl.Perspective(FOV, (double)W / (double)H, zNear, zFar);
            gl.MatrixMode(OpenGL.GL_MODELVIEW);

            Axies = new Axies(gl, 35f);
            ShowAxies = true;
            Cam = new Camera(gl);
            OrthoFactor = Cam.R;
            Model = new Model(gl, @"Models/Spiral.xml");

            gl.Enable(OpenGL.GL_DEPTH_TEST);
            gl.Enable(OpenGL.GL_LIGHT0);
            gl.LightModel(OpenGL.GL_LIGHT_MODEL_COLOR_CONTROL_EXT, OpenGL.GL_SEPARATE_SPECULAR_COLOR_EXT);
        }

        public void Resize(int Width, int Height)
        {
            W = Width;
            H = Height;
            if (!ortho)
            {
                gl.MatrixMode(OpenGL.GL_PROJECTION);
                gl.LoadIdentity();
                gl.Perspective(FOV, (double)W / (double)H, zNear, zFar);
                gl.MatrixMode(OpenGL.GL_MODELVIEW);
            }
            else
            {
                gl.MatrixMode(OpenGL.GL_PROJECTION);
                gl.LoadIdentity();
                gl.Ortho(
                    -W / orthoFactor, W / orthoFactor,
                    -H / orthoFactor, H / orthoFactor,
                    zNear - orthoFactor, zFar - orthoFactor);
                gl.MatrixMode(OpenGL.GL_MODELVIEW);
            }
        }

        public void Render()
        {
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_POSITION, pos);

            if (ShowAxies) Axies.Render();
            Model.Render();
            gl.Finish();
        }
    }
}
