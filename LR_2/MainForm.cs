using System;
using System.Drawing;
using System.Windows.Forms;
using SharpGL;

namespace LR_2
{
    public partial class MainForm : Form
    {
        OpenGL gl;
        int H;
        enum EditMode { None, Translation, Rotation, Scaling };
        int Mode = (int)EditMode.None;      // Режим редактирования объекта

        HexagonContainer Hexagons;          // Контейнер объектов
        Point MouseOrigin, Center;          // Позиция мыши и центр GLControl'а
        PointF OldScale;                    // Для сохранения текущего значения
                                            // растяжения объекта
        public MainForm()
        {
            InitializeComponent();
            H = GLControl.Height;

            ObjectsList.DataSource = Hexagons.Items;
            ObjectsList.DisplayMember = "Name";

            ColorMixNoneRB.CheckedChanged += new EventHandler(ColorMixMode_Changed);
            ColorMixOrRB.CheckedChanged += new EventHandler(ColorMixMode_Changed);
            ColorMixNotOrRB.CheckedChanged += new EventHandler(ColorMixMode_Changed);
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
            Hexagons = new HexagonContainer(GLControl, Center, Color.White);
            Mode = (int)EditMode.Translation;
            Hexagons.Current.RenderMode = Hexagon.RenderFlags.Translation;
        }

        // Отрисовка ----------------------------------------------------------
        private void GLControl_OpenGLDraw(object sender, RenderEventArgs args)
        {
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT);
            Hexagons.Render();
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
                    case (int)EditMode.Translation:
                        {
                            MouseOrigin.X = e.X - Hexagons.Current.Translation.X;
                            MouseOrigin.Y = H - e.Y - Hexagons.Current.Translation.Y;
                        }
                        break;
                    case (int)EditMode.Rotation:
                        {
                            Hexagons.Current.Rotation = GetAngle(Hexagons.Current.Translation, new Point(e.X, H - e.Y));
                        }
                        break;
                    case (int)EditMode.Scaling:
                        {
                            MouseOrigin.X = e.X;
                            MouseOrigin.Y = H - e.Y;
                            OldScale = Hexagons.Current.Scale;
                        }
                        break;
                }

            if (e.Button == MouseButtons.Right)
            {
                Hexagons.Current.Radius = GetDistance(Hexagons.Current.Translation, new Point(e.X, H - e.Y));
                Hexagons.Current.RenderMode |= Hexagon.RenderFlags.Outline;
            }
        }

        // Перемещение мыши ---------------------------------------------------
        private void GLControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                switch (Mode)
                {
                    case (int)EditMode.Translation:
                        {
                            Hexagons.Current.Translation = new Point(e.X - MouseOrigin.X, H - MouseOrigin.Y - e.Y);
                        }
                        break;
                    case (int)EditMode.Rotation:
                        {
                            Hexagons.Current.Rotation = GetAngle(Hexagons.Current.Translation, new Point(e.X, H - e.Y));
                        }
                        break;
                    case (int)EditMode.Scaling:
                        {
                            Hexagons.Current.Scale = new PointF(
                                OldScale.X + (e.X - MouseOrigin.X) / 70f,
                                OldScale.Y + (H - e.Y - MouseOrigin.Y) / 70f);
                        }
                        break;
                }

            if (e.Button == MouseButtons.Right)
                Hexagons.Current.Radius = GetDistance(Hexagons.Current.Translation, new Point(e.X, H - e.Y));
        }

        // Отпущена кнопка мыши -----------------------------------------------
        private void GLControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && Mode != (int)EditMode.Scaling)
                Hexagons.Current.RenderMode ^= Hexagon.RenderFlags.Outline;
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

        #region Панель редактирования активного объекта
        #region Кнопки модельно-видовых преобразований
        // Смещение -----------------------------------------------------------
        private void ButtonTranslate_Click(object sender, EventArgs e)
        {
            Mode = (int)EditMode.Translation;
            Hexagons.Current.RenderMode = Hexagon.RenderFlags.Translation;
        }

        // Поворот ------------------------------------------------------------
        private void ButtonRotate_Click(object sender, EventArgs e)
        {
            Mode = (int)EditMode.Rotation;
            Hexagons.Current.RenderMode = Hexagon.RenderFlags.Rotation;
        }

        // Растяжение ---------------------------------------------------------
        private void ButtonScale_Click(object sender, EventArgs e)
        {
            Mode = (int)EditMode.Scaling;
            Hexagons.Current.RenderMode = Hexagon.RenderFlags.Scale;
        }

        // Кнопки сброса соответствующих преобразований -----------------------
        private void ButtonResetTrans_Click(object sender, EventArgs e)
        {
            Hexagons.Current.Translation = Center;
        }

        private void ButtonResetRot_Click(object sender, EventArgs e)
        {
            Hexagons.Current.Rotation = 0f;
        }

        private void ButtonResetScale_Click(object sender, EventArgs e)
        {
            Hexagons.Current.Scale = new PointF(1f, 1f);
        }
        #endregion

        // Выбор новой текстуры -----------------------------------------------
        private void ButtonOpenImage_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (Hexagons.Current != null) Hexagons.Current.Texture.ChangeImage(openFileDialog.FileName);
            }
        }

        // Выбор цвета --------------------------------------------------------
        private void ButtonColorPick_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (Hexagons.Current != null) Hexagons.Current.FillColor = colorDialog.Color;
                ColorSquare.BackColor = colorDialog.Color;
            }
        }
        #endregion

        #region Панель управления обектами
        // Выбран другой объект -----------------------------------------------
        private void ObjectsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Hexagons.Current.RenderMode = Hexagon.RenderFlags.Hexagon;
            Hexagons.SwitchTo(ObjectsList.SelectedIndex);
            ColorSquare.BackColor = Hexagons.Current.FillColor;
            colorDialog.Color = Hexagons.Current.FillColor;
            switch (Mode)
            {
                case (int)EditMode.Translation:
                    Hexagons.Current.RenderMode = Hexagon.RenderFlags.Translation;
                    break;
                case (int)EditMode.Rotation:
                    Hexagons.Current.RenderMode = Hexagon.RenderFlags.Rotation;
                    break;
                case (int)EditMode.Scaling:
                    Hexagons.Current.RenderMode = Hexagon.RenderFlags.Scale;
                    break;
            }
        }

        // Создание нового объекта --------------------------------------------
        private void ButtonNewHexagon_Click(object sender, EventArgs e)
        {
            Hexagons.FillColor = colorDialog.Color;
            Hexagons.Origin = Center;
            if (Hexagons.Current != null) Hexagons.Current.RenderMode = Hexagon.RenderFlags.Hexagon;
            Hexagons.Create();
            switch (Mode)
            {
                case (int)EditMode.Translation:
                    Hexagons.Current.RenderMode = Hexagon.RenderFlags.Translation;
                    break;
                case (int)EditMode.Rotation:
                    Hexagons.Current.RenderMode = Hexagon.RenderFlags.Rotation;
                    break;
                case (int)EditMode.Scaling:
                    Hexagons.Current.RenderMode = Hexagon.RenderFlags.Scale;
                    break;
            }
            ObjectsList.SelectedIndex = ObjectsList.Items.Count - 1;
            ObjectControlsContainer.Enabled = true;
        }

        // Удаление активного объекта -----------------------------------------
        private void ButtonDeleteHexagon_Click(object sender, EventArgs e)
        {
            Hexagons.Remove(ObjectsList.SelectedIndex);
            Hexagons.SwitchTo(ObjectsList.SelectedIndex);
            if (Hexagons.Current == null) ObjectControlsContainer.Enabled = false;
        }
        #endregion

        // Изменен режим смешения цветов --------------------------------------
        private void ColorMixMode_Changed(object sender, EventArgs e)
        {
            if (ColorMixNoneRB.Checked)
            {
                gl.Disable(OpenGL.GL_COLOR_LOGIC_OP);
            }
            else if (ColorMixOrRB.Checked)
            {
                gl.LogicOp(OpenGL.GL_OR);
                gl.Enable(OpenGL.GL_COLOR_LOGIC_OP);
            }
            else
            {
                gl.LogicOp(OpenGL.GL_NOR);
                gl.Enable(OpenGL.GL_COLOR_LOGIC_OP);
            }
        }
    }
}
