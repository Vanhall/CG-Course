using System;
using System.Windows.Forms;
using System.Drawing;
using SharpGL;

namespace CG_Course
{
    public partial class MainForm : Form
    {
        PrimitiveContainer Primitives;
        int H;
        OpenGL gl;
        Color FillColor = Color.Aqua, VertexColor = Color.Aqua;

        public MainForm()
        {
            InitializeComponent();
            H = GLControl.Height;
            PrimitiveColorSqare.BackColor = FillColor;
            VertexColorSqare.BackColor = VertexColor;

            PrimitivesListBox.DataSource = Primitives.Items;
            PrimitivesListBox.DisplayMember = "Name";

            CoordsDataGrid.DataSource = Primitives.Current.Coords;
            CoordsDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            foreach (DataGridViewColumn column in CoordsDataGrid.Columns)
                column.SortMode = DataGridViewColumnSortMode.NotSortable;

            PrimitivesMenuNew.Click += new EventHandler(ButtonCreatePrimitive_Click);
            PrimitivesMenuDelete.Click += new EventHandler(ButtonRemovePrimitive_Click);
            PrimitivesMenuFill.Click += new EventHandler(FillPrimitive_Click);

            VertexMenuRecolor.Click += new EventHandler(ButtonRecolorVertex_Click);
            VertexMenuDelete.Click += new EventHandler(ButtonRemoveVertex_Click);
        }
        
        #region Обработчики событий OpenGL Control
        // Инициализация ------------------------------------------------------
        private void GLControl_OpenGLInitialized(object sender, EventArgs e)
        {
            gl = GLControl.OpenGL;
            Primitives = new PrimitiveContainer(GLControl, colorDialog.Color);

            // Настройка 2D проекции
            gl.Viewport(0, 0, GLControl.Width, GLControl.Height);
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
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT);
            Primitives.Render();
            gl.Finish();
        }

        // Изменение размеров окна --------------------------------------------
        private void GLControl_Resized(object sender, EventArgs e)
        {
            gl.Viewport(0, 0, GLControl.Width, GLControl.Height);
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
                    Primitives.Current.AddVertex(new Point(e.X, H - e.Y), FillColor);
                    Primitives.Current.ActiveVertex = CoordsDataGrid.CurrentRow.Index;
                }

                // Если правый клик - меняем координаты вершины
                if (e.Button == MouseButtons.Right)
                {
                    Primitives.Current.MoveVertex(e.Location.X, H - e.Location.Y);
                }
            }
        }
        #endregion

        #region Элементы управления примитивами
        // Кнопка закраски примитива ------------------------------------------
        private void FillPrimitive_Click(object sender, EventArgs e)
        {
            if (Primitives.Current != null)
                Primitives.Current.Fill(FillColor);
        }

        // Кнопка диалога выбора цвета ----------------------------------------
        private void PrimitiveColorSqare_MouseClick(object sender, MouseEventArgs e)
        {
            DialogResult result = colorDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                FillColor = colorDialog.Color;
                PrimitiveColorSqare.BackColor = FillColor;
            }
        }

        // Кнопка создания нового примитива -----------------------------------
        private void ButtonCreatePrimitive_Click(object sender, EventArgs e)
        {
            Primitives.Create(FillColor);
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

        // Открыто контекстное меню примитивов --------------------------------
        private void PrimitivesMenu_Opened(object sender, EventArgs e)
        {
            if (Primitives.Current == null)
            {
                PrimitivesMenuDelete.Enabled = false;
                PrimitivesMenuFill.Enabled = false;
                PrimitivesMenuRename.Enabled = false;
            }
            else
            {
                PrimitivesMenuDelete.Enabled = true;
                PrimitivesMenuFill.Enabled = true;
                PrimitivesMenuRename.Enabled = true;
                PrimitivesMenuName.Text = Primitives.Current.Name;
            }
        }

        // Правый клик по списку примитивов -----------------------------------
        private void PrimitivesListBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int index = PrimitivesListBox.IndexFromPoint(e.Location);
                if (index >= 0)
                {
                    PrimitivesListBox.SelectedIndex = index;
                }
            }
        }

        // В контекстном меню изменено имя объекта ----------------------------
        private void PrimitivesMenuName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Primitives.Current.Name = PrimitivesMenuName.Text;
                Primitives.Items.ResetBindings();
            }
        }

        // Галочка режима "проволока" -----------------------------------------
        private void FlatShadingChkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (FlatShadingChkBox.Checked)
                gl.ShadeModel(OpenGL.GL_FLAT);
            else
                gl.ShadeModel(OpenGL.GL_SMOOTH);
        }

        // Галочка режима заливки ---------------------------------------------
        private void WireframeChkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (WireframeChkBox.Checked)
                gl.PolygonMode(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_LINE);
            else
                gl.PolygonMode(OpenGL.GL_FRONT_AND_BACK, OpenGL.GL_FILL);
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

        // Диалог выбора цвета вершины ----------------------------------------
        private void VertexColorSqare_MouseClick(object sender, MouseEventArgs e)
        {
            DialogResult result = VertexColorDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                VertexColor = VertexColorDialog.Color;
                VertexColorSqare.BackColor = VertexColor;
            }
        }

        // Кнопка перекраски вершины ------------------------------------------
        private void ButtonRecolorVertex_Click(object sender, EventArgs e)
        {
            if (Primitives.Current != null && CoordsDataGrid.CurrentRow != null)
                Primitives.Current.RecolorVertex(VertexColor);
        }

        // Открыто контекстное меню списка вершин -----------------------------
        private void VertexMenu_Opened(object sender, EventArgs e)
        {
            if (CoordsDataGrid.CurrentRow == null)
                VertexMenu.Enabled = false;
            else
                VertexMenu.Enabled = true;
        }
        #endregion
    }
}
