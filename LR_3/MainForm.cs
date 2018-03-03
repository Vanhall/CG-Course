using System;
using System.Drawing;
using System.Windows.Forms;

namespace LR_3
{
    public partial class MainForm : Form
    {
        Scene scene;
        Point mouseStartDrag;
        bool camMoving;

        public MainForm()
        {
            InitializeComponent();
            mouseStartDrag = new Point(0, 0);
            GLControl.MouseWheel += new MouseEventHandler(GLControl_MouseWheel);
        }

        private void GLControl_OpenGLInitialized(object sender, EventArgs e)
        {
            scene = new Scene(GLControl, 60.0, 1.0, 200.0);
        }

        private void GLControl_OpenGLDraw(object sender, SharpGL.RenderEventArgs args)
        {
            scene.Render();
        }

        private void GLControl_Resized(object sender, EventArgs e)
        {
            scene.Resize(GLControl.Width, GLControl.Height);
        }

        private void GLControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
                scene.Cam.Reset();
            else
                camMoving = true;
        }

        private void GLControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (camMoving)
            {
                if (e.Button == MouseButtons.Left)
                {
                    scene.Cam.Rotate(
                            scene.Cam.Phi - (e.X - mouseStartDrag.X) / 3.0,
                            scene.Cam.Psi - (e.Y - mouseStartDrag.Y) / 3.0);
                }
            }
            mouseStartDrag = e.Location;
        }

        private void GLControl_MouseUp(object sender, MouseEventArgs e)
        {
            camMoving = false;
            mouseStartDrag = e.Location;
        }

        private void GLControl_MouseWheel(object sender, MouseEventArgs e)
        {
            scene.Cam.Zoom(scene.Cam.R - e.Delta / 60);
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W: scene.Cam.Translate(1.0, 0, 0); break;
                case Keys.S: scene.Cam.Translate(-1.0, 0, 0); break;
                case Keys.A: scene.Cam.Translate(0, 1.0, 0); break;
                case Keys.D: scene.Cam.Translate(0, -1.0, 0); break;
                case Keys.R: scene.Cam.Translate(0, 0, 1.0); break;
                case Keys.F: scene.Cam.Translate(0, 0, -1.0); break;
            }
        }

        private void TrajectoryChBox_CheckedChanged(object sender, EventArgs e)
        {
            if (TrajectoryChBox.Checked)
                scene.Model.RenderMode |= Model.RenderFlags.Trajectory;
            else
                scene.Model.RenderMode &= ~Model.RenderFlags.Trajectory;
        }

        private void SectionsChBox_CheckedChanged(object sender, EventArgs e)
        {
            if (SectionsChBox.Checked)
                scene.Model.RenderMode |= Model.RenderFlags.Sections;
            else
                scene.Model.RenderMode &= ~Model.RenderFlags.Sections;
        }

        private void NormalsChBox_CheckedChanged(object sender, EventArgs e)
        {
            if (NormalsChBox.Checked)
                scene.Model.RenderMode |= Model.RenderFlags.Normal;
            else
                scene.Model.RenderMode &= ~Model.RenderFlags.Normal;
        }

        private void SmoothChBox_CheckedChanged(object sender, EventArgs e)
        {
            if (SmoothChBox.Checked)
            {
                scene.Model.RenderMode &= ~Model.RenderFlags.Flat;
                scene.Model.RenderMode |= Model.RenderFlags.Smooth;
            }
            else
            {
                scene.Model.RenderMode &= ~Model.RenderFlags.Smooth;
                scene.Model.RenderMode |= Model.RenderFlags.Flat;
            }
        }
    }
}
