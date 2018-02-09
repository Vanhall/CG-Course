using System;
using System.Windows.Forms;
using SharpGL;

namespace CG_Course
{
    public partial class MainForm : Form
    {
        PrimitiveContainer Primitives;
        int H;
        OpenGL gl;

        public MainForm()
        {
            InitializeComponent();
            H = GLControl.Height;
            ColorSquare.BackColor = colorDialog.Color;

            PrimitivesListBox.DataSource = Primitives.Items;
            PrimitivesListBox.DisplayMember = "Name";

            CoordsDataGrid.DataSource = Primitives.Current.Coords;
            CoordsDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            foreach (DataGridViewColumn column in CoordsDataGrid.Columns)
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
        }
        
        #region Обработчики событий OpenGL Control
        // Инициализация ------------------------------------------------------
        private void GLControl_OpenGLInitialized(object sender, EventArgs e)
        {
            gl = GLControl.OpenGL;
            Primitives = new PrimitiveContainer(GLControl, colorDialog.Color);

            // Настройка 2D проекции
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();
            gl.Ortho2D(0, GLControl.Width, 0, GLControl.Height);
            gl.MatrixMode(OpenGL.GL_MODELVIEW);

            gl.ClearColor(0.7f, 0.7f, 0.8f, 1.0f);  // цвет фона
            gl.Disable(OpenGL.GL_DEPTH_TEST);
        }

        // Отрисовка ----------------------------------------------------------
        private void GLControl_OpenGLDraw(object sender, RenderEventArgs args)
        {
            Primitives.Render();
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

        // Обработка мыши -----------------------------------------------------
        private void GLControl_MouseClick(object sender, MouseEventArgs e)
        {
            if (Primitives.Current != null)
            {
                // Если левый клик - добавляем новую вершину текущему примитиву
                if (e.Button == MouseButtons.Left)
                {
                    Primitives.Current.AddVertex(e.Location.X, H - e.Location.Y);
                    Primitives.Current.ActiveVertex = CoordsDataGrid.CurrentRow.Index;
                }

                // Если правый клик - меняем координаты вершины
                if (e.Button == MouseButtons.Right)
                {
                    Primitives.Current.EditVertex(e.Location.X, H - e.Location.Y);
                }
            }
        }
        #endregion

        #region Элементы управления примитивами
        // Кнопка диалога выбора цвета ----------------------------------------
        private void ButtonColorPick_Click(object sender, EventArgs e)
        {
            DialogResult result = colorDialog.ShowDialog();
            if(result == DialogResult.OK)
            {
                if (Primitives.Current != null)
                    Primitives.Current.FillColor = colorDialog.Color;
                Primitives.FillColor = colorDialog.Color;
                ColorSquare.BackColor = colorDialog.Color;
            }
        }

        // Кнопка создания нового примитива -----------------------------------
        private void ButtonCreatePrimitive_Click(object sender, EventArgs e)
        {
            Primitives.Create();
            PrimitivesListBox.SelectedIndex = PrimitivesListBox.Items.Count - 1;
            CoordsDataGrid.DataSource = Primitives.Current.Coords;
        }

        // Кнопка удаления примитива ------------------------------------------
        private void ButtonRemovePrimitive_Click(object sender, EventArgs e)
        {
            Primitives.Remove(PrimitivesListBox.SelectedIndex);
            Primitives.SwitchTo(PrimitivesListBox.SelectedIndex);
            if (Primitives.Current != null)
                CoordsDataGrid.DataSource = Primitives.Current.Coords;
            else
                CoordsDataGrid.DataSource = null;
        }

        // Выбран другой примитив ---------------------------------------------
        private void PrimitivesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Primitives.SwitchTo(PrimitivesListBox.SelectedIndex);
            CoordsDataGrid.DataSource = Primitives.Current.Coords;
        }
        #endregion

        #region Элементы управления вершинами
        // Кнопка удаления вершины --------------------------------------------
        private void ButtonRemoveVertex_Click(object sender, EventArgs e)
        {
            if (Primitives.Current != null && CoordsDataGrid.CurrentRow != null)
                Primitives.Current.RemoveVertex(CoordsDataGrid.CurrentRow.Index);
        }

        // Выбрана другая вершина ---------------------------------------------
        private void CoordsDataGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (Primitives.Current != null && CoordsDataGrid.CurrentRow != null)
                Primitives.Current.ActiveVertex = CoordsDataGrid.CurrentRow.Index;
        }
        #endregion

        private void WireframeChkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (WireframeChkBox.Checked)
                gl.PolygonMode(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_LINE);
            else
                gl.PolygonMode(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_FILL);
        }
    }
}
