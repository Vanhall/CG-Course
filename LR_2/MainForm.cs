using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpGL;

namespace LR_2
{
    public partial class MainForm : Form
    {
        OpenGL gl;
        int H;
        enum EditMode { None, OriginAndRadius, Translation, Rotation, Scaling};
        int Mode = (int)EditMode.None;

        Hexagon Temp;
        Point MouseOrigin;

        public MainForm()
        {
            InitializeComponent();
            H = GLControl.Height;
        }

        #region Обработчики событий OpenGL Control
        // Инициализация ------------------------------------------------------
        private void GLControl_OpenGLInitialized(object sender, EventArgs e)
        {
            gl = GLControl.OpenGL;

            // Настройка 2D проекции
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();
            gl.Ortho2D(0, GLControl.Width, 0, GLControl.Height);
            gl.MatrixMode(OpenGL.GL_MODELVIEW);

            gl.ClearColor(0.8f, 0.8f, 0.8f, 1.0f);  // цвет фона
            gl.Disable(OpenGL.GL_DEPTH_TEST);

            Temp = new Hexagon(GLControl.OpenGL, new Point(GLControl.Height/2, GLControl.Width/2), Color.White);
            Mode = (int)EditMode.OriginAndRadius;
            Temp.RenderMode = Hexagon.RenderFlags.Outline;
        }

        // Отрисовка ----------------------------------------------------------
        private void GLControl_OpenGLDraw(object sender, RenderEventArgs args)
        {

            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT);

            // Implement rendering routine
            Temp.Render();

            gl.Finish();
        }

        // Изменение размеров окна --------------------------------------------
        private void GLControl_Resized(object sender, EventArgs e)
        {
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();
            gl.Ortho2D(0, GLControl.Width, 0, GLControl.Height);
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            
            H = GLControl.Height;
        }
        #endregion

        private float GetDistance(Point Origin, Point Location)
        {
            int X = Origin.X - Location.X;
            int Y = Origin.Y - Location.Y;
            return (float)Math.Sqrt(X * X + Y * Y);
        }

        private float GetAngle(Point Origin, Point Location)
        {
            int X = Location.X - Origin.X;
            int Y = Location.Y - Origin.Y;
            double angle = X / (Math.Sqrt(X * X + Y * Y));
            
            return (float)(Math.Acos(angle) * 180.0 / Math.PI);
        }

        private void GLControl_MouseDown(object sender, MouseEventArgs e)
        {
            switch(Mode)
            {
                case (int)EditMode.None:
                    {
                        // no mode selected
                    } break;
                case (int)EditMode.OriginAndRadius:
                    {
                        if (e.Button == MouseButtons.Left)
                            Temp.Center = new Point(e.X, H - e.Y);
                    }
                    break;
                case (int)EditMode.Translation:
                    {
                        MouseOrigin.X = e.X - Temp.Translation.X;
                        MouseOrigin.Y = H - e.Y - Temp.Translation.Y;
                    }
                    break;
                case (int)EditMode.Rotation:
                    {
                        Temp.Rotation = GetAngle(Temp.Center, new Point(e.X, H - e.Y));
                    }
                    break;
                case (int)EditMode.Scaling:
                    {
                        // no mode selected
                    }
                    break;
            }
        }

        private void GLControl_MouseMove(object sender, MouseEventArgs e)
        {
            switch (Mode)
            {
                case (int)EditMode.None:
                    {
                        // no mode selected
                    }
                    break;
                case (int)EditMode.OriginAndRadius:
                    {
                        if (e.Button == MouseButtons.Left)
                            Temp.Center = new Point(e.X, H - e.Y);
                        if (e.Button == MouseButtons.Right)
                            Temp.Radius = GetDistance(Temp.Center, new Point(e.X, H - e.Y));
                    }
                    break;
                case (int)EditMode.Translation:
                    {
                        if (e.Button == MouseButtons.Left)
                            Temp.Translation = new Point(e.X - MouseOrigin.X, H - MouseOrigin.Y - e.Y);
                    }
                    break;
                case (int)EditMode.Rotation:
                    {
                        // no mode selected
                    }
                    break;
                case (int)EditMode.Scaling:
                    {
                        // no mode selected
                    }
                    break;
            }
            
        }

        private void ButtonEditOrigin_Click(object sender, EventArgs e)
        {
            Mode = (int)EditMode.OriginAndRadius;
            Temp.RenderMode = Hexagon.RenderFlags.Origin | Hexagon.RenderFlags.Outline | Hexagon.RenderFlags.Points;
        }

        private void ButtonTranslate_Click(object sender, EventArgs e)
        {
            Mode = (int)EditMode.Translation;
            Temp.RenderMode = Hexagon.RenderFlags.Origin | Hexagon.RenderFlags.Outline | Hexagon.RenderFlags.TranslationLines;
        }

        private void ButtonRotate_Click(object sender, EventArgs e)
        {
            Mode = (int)EditMode.Rotation;
            Temp.RenderMode = Hexagon.RenderFlags.Origin | Hexagon.RenderFlags.Outline | Hexagon.RenderFlags.RotationLines;
        }
    }
}
