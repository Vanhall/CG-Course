using System;
using System.Drawing;
using System.Windows.Forms;
using SharpGL;

namespace RGZ
{
    public partial class MainForm : Form
    {
        OpenGL gl;
        int W, H;
        Scene Scene;
        Point CenterOffset;

        public MainForm()
        {
            InitializeComponent();

            ControlPointsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            foreach (DataGridViewColumn column in ControlPointsGrid.Columns)
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        #region События GLControl
        private void GLControl_OpenGLInitialized(object sender, EventArgs e)
        {
            gl = GLControl.OpenGL;
            Scene = new Scene(GLControl);
            ControlPointsGrid.DataSource = Scene.CPData;
            H = GLControl.Height;
            W = GLControl.Width;
            CenterOffset.X = W / 2;
            CenterOffset.Y = H / 2;

            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();
            gl.Ortho2D(-W / 2, W / 2, -H / 2, H / 2);
            gl.MatrixMode(OpenGL.GL_MODELVIEW);

            gl.ClearColor(1f, 1f, 1f, 1.0f);  // цвет фона
            gl.Disable(OpenGL.GL_DEPTH_TEST);
        }

        private void GLControl_OpenGLDraw(object sender, SharpGL.RenderEventArgs args)
        {
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT);
            Scene.Render();
            gl.Finish();
        }

        private void GLControl_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Scene.AddControlPoint(new Point(e.X - CenterOffset.X, H - e.Y - CenterOffset.Y));
                Scene.ActivePoint = ControlPointsGrid.CurrentRow.Index;
            }
            if (e.Button == MouseButtons.Right)
                Scene.MoveControlPoint(new Point(e.X - CenterOffset.X, H - e.Y - CenterOffset.Y));
        }
        
        private void GLControl_Resized(object sender, EventArgs e)
        {
            H = GLControl.Height;
            W = GLControl.Width;
            CenterOffset.X = W / 2;
            CenterOffset.Y = H / 2;
            Scene.Grid.Resize(W, H);

            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();
            gl.Ortho2D(-W / 2, W / 2, -H / 2, H / 2);
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
        }
        #endregion

        #region Элементы управления сплайном
        private void StepsPicker_ValueChanged(object sender, EventArgs e)
        {
            Scene.Steps = (int)StepsPicker.Value;
        }

        private void DegreePicker_ValueChanged(object sender, EventArgs e)
        {
            Scene.Degree = (int)DegreePicker.Value;
        }
        #endregion

        #region Элементы управления контр. точками
        private void ClearAll_Click(object sender, EventArgs e)
        {
            Scene.ClearControlPoints();
            Scene.ActivePoint = -1;
        }

        private void RemovePoint_Click(object sender, EventArgs e)
        {
            if (ControlPointsGrid.CurrentRow != null)
                Scene.RemoveControlPoint(ControlPointsGrid.CurrentRow.Index);
            else
                Scene.ActivePoint = -1;
        }

        private void ControlPointsGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (ControlPointsGrid.CurrentRow != null)
                Scene.ActivePoint = ControlPointsGrid.CurrentRow.Index;
            else
                Scene.ActivePoint = -1;
        }
        #endregion

        #region Элементы управления отображением
        private void DrawPointsCHB_CheckedChanged(object sender, EventArgs e)
        {
            Scene.DrawPoints = DrawPointsCHB.Checked;
        }

        private void DrawLinesCHB_CheckedChanged(object sender, EventArgs e)
        {
            Scene.DrawLines = DrawLinesCHB.Checked;
        }

        private void DrawGridCHB_CheckedChanged(object sender, EventArgs e)
        {
            Scene.DrawGrid = DrawGridCHB.Checked;
        }


        private void SplineColorPick_Click(object sender, EventArgs e)
        {
            ColorPicker.Color = Scene.SplineColor;
            DialogResult result = ColorPicker.ShowDialog();
            if (result == DialogResult.OK)
            {
                Scene.SplineColor = ColorPicker.Color;
                SplineColorSquare.BackColor = ColorPicker.Color;
            }
        }

        private void LinesColorPick_Click(object sender, EventArgs e)
        {
            ColorPicker.Color = Scene.LinesColor;
            DialogResult result = ColorPicker.ShowDialog();
            if (result == DialogResult.OK)
            {
                Scene.LinesColor = ColorPicker.Color;
                LinesColorSquare.BackColor = ColorPicker.Color;
            }
        }

        private void PointsColorPick_Click(object sender, EventArgs e)
        {
            ColorPicker.Color = Scene.PointsColor;
            DialogResult result = ColorPicker.ShowDialog();
            if (result == DialogResult.OK)
            {
                Scene.PointsColor = ColorPicker.Color;
                PointsColorSquare.BackColor = ColorPicker.Color;
            }
        }
        #endregion
    }
}
