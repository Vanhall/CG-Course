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
        enum EditMode { None, Translation, Rotation, Scaling };
        int Mode = (int)EditMode.None;

        Hexagon Temp;
        Point MouseOrigin, Center;
        PointF OldScale;

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
            gl.LineWidth(2f);

            Center = new Point(GLControl.Width / 2, GLControl.Height / 2);
            Temp = new Hexagon(GLControl.OpenGL, Center, Color.White);
            Mode = (int)EditMode.Translation;
            Temp.RenderMode = Hexagon.RenderFlags.Translation;
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
            Center.X = GLControl.Width / 2;
            Center.Y = GLControl.Height / 2;
        }

        // Нажата кнопка мыши -------------------------------------------------
        private void GLControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                switch (Mode)
                {
                    case (int)EditMode.None:
                        {
                            // no mode selected
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
                            Temp.Rotation = GetAngle(Temp.Translation, new Point(e.X, H - e.Y));
                        }
                        break;
                    case (int)EditMode.Scaling:
                        {
                            MouseOrigin.X = e.X;
                            MouseOrigin.Y = H - e.Y;
                            OldScale = Temp.Scale;
                        }
                        break;
                }

            if (e.Button == MouseButtons.Right)
            {
                Temp.Radius = GetDistance(Temp.Translation, new Point(e.X, H - e.Y));
                Temp.RenderMode |= Hexagon.RenderFlags.Outline;
            }
        }

        // Перемещение мыши ---------------------------------------------------
        private void GLControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                switch (Mode)
                {
                    case (int)EditMode.None:
                        {
                            // no mode selected
                        }
                        break;
                    case (int)EditMode.Translation:
                        {
                            Temp.Translation = new Point(e.X - MouseOrigin.X, H - MouseOrigin.Y - e.Y);
                        }
                        break;
                    case (int)EditMode.Rotation:
                        {
                            Temp.Rotation = GetAngle(Temp.Translation, new Point(e.X, H - e.Y));
                        }
                        break;
                    case (int)EditMode.Scaling:
                        {
                            Temp.Scale = new PointF(
                                OldScale.X + (e.X - MouseOrigin.X) / 70f,
                                OldScale.Y + (H - e.Y - MouseOrigin.Y) / 70f);
                        }
                        break;
                }

            if (e.Button == MouseButtons.Right)
                Temp.Radius = GetDistance(Temp.Translation, new Point(e.X, H - e.Y));
        }

        // Отпущена кнопка мыши -----------------------------------------------
        private void GLControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && Mode != (int)EditMode.Scaling)
                Temp.RenderMode ^= Hexagon.RenderFlags.Outline;
        }
        #endregion

        #region Вспомогательное
        // Расстояние между двумя точками -------------------------------------
        private float GetDistance(Point Origin, Point Location)
        {
            int X = Origin.X - Location.X;
            int Y = Origin.Y - Location.Y;
            return (float)Math.Sqrt(X * X + Y * Y);
        }

        // Угол между двумя векторами (ОХ и прямой, проход. через 0,0)---------
        private float GetAngle(Point Origin, Point Location)
        {
            int X = Location.X - Origin.X;
            int Y = Location.Y - Origin.Y;
            double angle = X / (Math.Sqrt(X * X + Y * Y));
            angle = Math.Acos(angle) * 180.0 / Math.PI;
            if (Y < 0) angle = 360.0 - angle;
            return (float)angle;
        }
        #endregion

        #region Кнопки модельно-видовых преобразований
        private void ButtonTranslate_Click(object sender, EventArgs e)
        {
            Mode = (int)EditMode.Translation;
            Temp.RenderMode = Hexagon.RenderFlags.Translation;
        }

        private void ButtonRotate_Click(object sender, EventArgs e)
        {
            Mode = (int)EditMode.Rotation;
            Temp.RenderMode = Hexagon.RenderFlags.Rotation;
        }

        private void ButtonScale_Click(object sender, EventArgs e)
        {
            Mode = (int)EditMode.Scaling;
            Temp.RenderMode = Hexagon.RenderFlags.Scale;
        }
        #endregion
    }
}
