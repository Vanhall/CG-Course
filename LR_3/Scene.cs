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
        Model model;

        public Scene(OpenGLControl GLControl, double FOV, double zNear, double zFar)
        {
            gl = GLControl.OpenGL;
            this.FOV = FOV;
            this.zNear = zNear;
            this.zFar = zFar;

            gl.ClearColor(0.7f, 0.7f, 0.8f, 1.0f);

            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();
            gl.Perspective(FOV, (double)GLControl.Width / (double)GLControl.Height, zNear, zFar);
            gl.MatrixMode(OpenGL.GL_MODELVIEW);

            Axies = new Axies(gl, 35f);
            ShowAxies = true;
            Cam = new Camera(gl);
            model = new Model(gl);

            gl.Enable(OpenGL.GL_DEPTH_TEST);
        }

        public void Resize(int Width, int Height)
        {
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();
            gl.Perspective(FOV, (double)Width / (double)Height, zNear, zFar);
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
        }

        public void Render()
        {
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            if (ShowAxies) Axies.Render();
            model.Render();
            gl.Finish();
        }
    }
}
