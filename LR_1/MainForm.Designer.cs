namespace CG_Course
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.GLControl = new SharpGL.OpenGLControl();
            this.ControlPanel = new System.Windows.Forms.Panel();
            this.VertexControls = new System.Windows.Forms.GroupBox();
            this.VertexColorSqare = new System.Windows.Forms.Panel();
            this.CoordsDataGrid = new System.Windows.Forms.DataGridView();
            this.VertexMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.VertexMenuRecolor = new System.Windows.Forms.ToolStripMenuItem();
            this.VertexMenuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.ButtonRecolorVertex = new System.Windows.Forms.Button();
            this.ButtonRemoveVertex = new System.Windows.Forms.Button();
            this.PrimitiveControls = new System.Windows.Forms.GroupBox();
            this.PrimitivesListBox = new System.Windows.Forms.ListBox();
            this.PrimitivesMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.PrimitivesMenuNew = new System.Windows.Forms.ToolStripMenuItem();
            this.PrimitivesMenuFill = new System.Windows.Forms.ToolStripMenuItem();
            this.PrimitivesMenuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.PrimitivesMenuRename = new System.Windows.Forms.ToolStripMenuItem();
            this.PrimitivesMenuName = new System.Windows.Forms.ToolStripTextBox();
            this.ButtonCreatePrimitive = new System.Windows.Forms.Button();
            this.WireframeChkBox = new System.Windows.Forms.CheckBox();
            this.ButtonRemovePrimitive = new System.Windows.Forms.Button();
            this.PrimitiveColorSqare = new System.Windows.Forms.Panel();
            this.FillPrimitive = new System.Windows.Forms.Button();
            this.OpenGLLogo = new System.Windows.Forms.PictureBox();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.VertexColorDialog = new System.Windows.Forms.ColorDialog();
            this.FlatShadingChkBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.GLControl)).BeginInit();
            this.ControlPanel.SuspendLayout();
            this.VertexControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CoordsDataGrid)).BeginInit();
            this.VertexMenu.SuspendLayout();
            this.PrimitiveControls.SuspendLayout();
            this.PrimitivesMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OpenGLLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // GLControl
            // 
            this.GLControl.BackColor = System.Drawing.SystemColors.Desktop;
            this.GLControl.Cursor = System.Windows.Forms.Cursors.Cross;
            this.GLControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GLControl.DrawFPS = false;
            this.GLControl.FrameRate = 30;
            this.GLControl.Location = new System.Drawing.Point(0, 0);
            this.GLControl.Name = "GLControl";
            this.GLControl.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.GLControl.RenderContextType = SharpGL.RenderContextType.FBO;
            this.GLControl.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            this.GLControl.Size = new System.Drawing.Size(796, 729);
            this.GLControl.TabIndex = 0;
            this.GLControl.OpenGLInitialized += new System.EventHandler(this.GLControl_OpenGLInitialized);
            this.GLControl.OpenGLDraw += new SharpGL.RenderEventHandler(this.GLControl_OpenGLDraw);
            this.GLControl.Resized += new System.EventHandler(this.GLControl_Resized);
            this.GLControl.MouseClick += new System.Windows.Forms.MouseEventHandler(this.GLControl_MouseClick);
            // 
            // ControlPanel
            // 
            this.ControlPanel.BackColor = System.Drawing.SystemColors.Control;
            this.ControlPanel.Controls.Add(this.VertexControls);
            this.ControlPanel.Controls.Add(this.PrimitiveControls);
            this.ControlPanel.Controls.Add(this.OpenGLLogo);
            this.ControlPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.ControlPanel.Location = new System.Drawing.Point(796, 0);
            this.ControlPanel.Name = "ControlPanel";
            this.ControlPanel.Size = new System.Drawing.Size(212, 729);
            this.ControlPanel.TabIndex = 1;
            // 
            // VertexControls
            // 
            this.VertexControls.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.VertexControls.Controls.Add(this.VertexColorSqare);
            this.VertexControls.Controls.Add(this.CoordsDataGrid);
            this.VertexControls.Controls.Add(this.ButtonRecolorVertex);
            this.VertexControls.Controls.Add(this.ButtonRemoveVertex);
            this.VertexControls.Location = new System.Drawing.Point(6, 181);
            this.VertexControls.Name = "VertexControls";
            this.VertexControls.Size = new System.Drawing.Size(200, 486);
            this.VertexControls.TabIndex = 11;
            this.VertexControls.TabStop = false;
            this.VertexControls.Text = "Редактирование вершин";
            // 
            // VertexColorSqare
            // 
            this.VertexColorSqare.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.VertexColorSqare.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.VertexColorSqare.Location = new System.Drawing.Point(7, 457);
            this.VertexColorSqare.Name = "VertexColorSqare";
            this.VertexColorSqare.Size = new System.Drawing.Size(19, 22);
            this.VertexColorSqare.TabIndex = 1;
            this.VertexColorSqare.MouseClick += new System.Windows.Forms.MouseEventHandler(this.VertexColorSqare_MouseClick);
            // 
            // CoordsDataGrid
            // 
            this.CoordsDataGrid.AllowUserToAddRows = false;
            this.CoordsDataGrid.AllowUserToDeleteRows = false;
            this.CoordsDataGrid.AllowUserToResizeColumns = false;
            this.CoordsDataGrid.AllowUserToResizeRows = false;
            this.CoordsDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CoordsDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CoordsDataGrid.ContextMenuStrip = this.VertexMenu;
            this.CoordsDataGrid.Location = new System.Drawing.Point(6, 19);
            this.CoordsDataGrid.MultiSelect = false;
            this.CoordsDataGrid.Name = "CoordsDataGrid";
            this.CoordsDataGrid.ReadOnly = true;
            this.CoordsDataGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.CoordsDataGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.CoordsDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.CoordsDataGrid.Size = new System.Drawing.Size(188, 431);
            this.CoordsDataGrid.TabIndex = 5;
            this.CoordsDataGrid.SelectionChanged += new System.EventHandler(this.CoordsDataGrid_SelectionChanged);
            // 
            // VertexMenu
            // 
            this.VertexMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.VertexMenuRecolor,
            this.VertexMenuDelete});
            this.VertexMenu.Name = "VertexMenu";
            this.VertexMenu.Size = new System.Drawing.Size(131, 48);
            this.VertexMenu.Opened += new System.EventHandler(this.VertexMenu_Opened);
            // 
            // VertexMenuRecolor
            // 
            this.VertexMenuRecolor.Name = "VertexMenuRecolor";
            this.VertexMenuRecolor.Size = new System.Drawing.Size(130, 22);
            this.VertexMenuRecolor.Text = "Закрасить";
            // 
            // VertexMenuDelete
            // 
            this.VertexMenuDelete.Name = "VertexMenuDelete";
            this.VertexMenuDelete.Size = new System.Drawing.Size(130, 22);
            this.VertexMenuDelete.Text = "Удалить";
            // 
            // ButtonRecolorVertex
            // 
            this.ButtonRecolorVertex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ButtonRecolorVertex.Location = new System.Drawing.Point(25, 456);
            this.ButtonRecolorVertex.Name = "ButtonRecolorVertex";
            this.ButtonRecolorVertex.Size = new System.Drawing.Size(69, 24);
            this.ButtonRecolorVertex.TabIndex = 7;
            this.ButtonRecolorVertex.Text = "Закрасить вершину";
            this.ButtonRecolorVertex.UseVisualStyleBackColor = true;
            this.ButtonRecolorVertex.Click += new System.EventHandler(this.ButtonRecolorVertex_Click);
            // 
            // ButtonRemoveVertex
            // 
            this.ButtonRemoveVertex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonRemoveVertex.Location = new System.Drawing.Point(106, 456);
            this.ButtonRemoveVertex.Name = "ButtonRemoveVertex";
            this.ButtonRemoveVertex.Size = new System.Drawing.Size(88, 24);
            this.ButtonRemoveVertex.TabIndex = 7;
            this.ButtonRemoveVertex.Text = "Удалить";
            this.ButtonRemoveVertex.UseVisualStyleBackColor = true;
            this.ButtonRemoveVertex.Click += new System.EventHandler(this.ButtonRemoveVertex_Click);
            // 
            // PrimitiveControls
            // 
            this.PrimitiveControls.Controls.Add(this.FlatShadingChkBox);
            this.PrimitiveControls.Controls.Add(this.PrimitivesListBox);
            this.PrimitiveControls.Controls.Add(this.ButtonCreatePrimitive);
            this.PrimitiveControls.Controls.Add(this.WireframeChkBox);
            this.PrimitiveControls.Controls.Add(this.ButtonRemovePrimitive);
            this.PrimitiveControls.Controls.Add(this.PrimitiveColorSqare);
            this.PrimitiveControls.Controls.Add(this.FillPrimitive);
            this.PrimitiveControls.Location = new System.Drawing.Point(6, 12);
            this.PrimitiveControls.Name = "PrimitiveControls";
            this.PrimitiveControls.Size = new System.Drawing.Size(200, 163);
            this.PrimitiveControls.TabIndex = 10;
            this.PrimitiveControls.TabStop = false;
            this.PrimitiveControls.Text = "Управление примитивами";
            // 
            // PrimitivesListBox
            // 
            this.PrimitivesListBox.ContextMenuStrip = this.PrimitivesMenu;
            this.PrimitivesListBox.FormattingEnabled = true;
            this.PrimitivesListBox.Location = new System.Drawing.Point(6, 19);
            this.PrimitivesListBox.Name = "PrimitivesListBox";
            this.PrimitivesListBox.Size = new System.Drawing.Size(94, 134);
            this.PrimitivesListBox.TabIndex = 2;
            this.PrimitivesListBox.SelectedIndexChanged += new System.EventHandler(this.PrimitivesListBox_SelectedIndexChanged);
            this.PrimitivesListBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PrimitivesListBox_MouseDown);
            // 
            // PrimitivesMenu
            // 
            this.PrimitivesMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PrimitivesMenuNew,
            this.PrimitivesMenuFill,
            this.PrimitivesMenuDelete,
            this.PrimitivesMenuRename});
            this.PrimitivesMenu.Name = "PrimitivesMenu";
            this.PrimitivesMenu.Size = new System.Drawing.Size(162, 92);
            this.PrimitivesMenu.Opened += new System.EventHandler(this.PrimitivesMenu_Opened);
            // 
            // PrimitivesMenuNew
            // 
            this.PrimitivesMenuNew.Name = "PrimitivesMenuNew";
            this.PrimitivesMenuNew.Size = new System.Drawing.Size(161, 22);
            this.PrimitivesMenuNew.Text = "Новый";
            // 
            // PrimitivesMenuFill
            // 
            this.PrimitivesMenuFill.Name = "PrimitivesMenuFill";
            this.PrimitivesMenuFill.Size = new System.Drawing.Size(161, 22);
            this.PrimitivesMenuFill.Text = "Закрасить";
            // 
            // PrimitivesMenuDelete
            // 
            this.PrimitivesMenuDelete.Name = "PrimitivesMenuDelete";
            this.PrimitivesMenuDelete.Size = new System.Drawing.Size(161, 22);
            this.PrimitivesMenuDelete.Text = "Удалить";
            // 
            // PrimitivesMenuRename
            // 
            this.PrimitivesMenuRename.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PrimitivesMenuName});
            this.PrimitivesMenuRename.Name = "PrimitivesMenuRename";
            this.PrimitivesMenuRename.Size = new System.Drawing.Size(161, 22);
            this.PrimitivesMenuRename.Text = "Переименовать";
            // 
            // PrimitivesMenuName
            // 
            this.PrimitivesMenuName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PrimitivesMenuName.Name = "PrimitivesMenuName";
            this.PrimitivesMenuName.Size = new System.Drawing.Size(100, 23);
            this.PrimitivesMenuName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PrimitivesMenuName_KeyDown);
            // 
            // ButtonCreatePrimitive
            // 
            this.ButtonCreatePrimitive.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonCreatePrimitive.Location = new System.Drawing.Point(106, 19);
            this.ButtonCreatePrimitive.Name = "ButtonCreatePrimitive";
            this.ButtonCreatePrimitive.Size = new System.Drawing.Size(88, 24);
            this.ButtonCreatePrimitive.TabIndex = 3;
            this.ButtonCreatePrimitive.Text = "Новый";
            this.ButtonCreatePrimitive.UseVisualStyleBackColor = true;
            this.ButtonCreatePrimitive.Click += new System.EventHandler(this.ButtonCreatePrimitive_Click);
            // 
            // WireframeChkBox
            // 
            this.WireframeChkBox.AutoSize = true;
            this.WireframeChkBox.Location = new System.Drawing.Point(106, 108);
            this.WireframeChkBox.Name = "WireframeChkBox";
            this.WireframeChkBox.Size = new System.Drawing.Size(74, 17);
            this.WireframeChkBox.TabIndex = 8;
            this.WireframeChkBox.Text = "Wireframe";
            this.WireframeChkBox.UseVisualStyleBackColor = true;
            this.WireframeChkBox.CheckedChanged += new System.EventHandler(this.WireframeChkBox_CheckedChanged);
            // 
            // ButtonRemovePrimitive
            // 
            this.ButtonRemovePrimitive.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonRemovePrimitive.Location = new System.Drawing.Point(106, 49);
            this.ButtonRemovePrimitive.Name = "ButtonRemovePrimitive";
            this.ButtonRemovePrimitive.Size = new System.Drawing.Size(88, 24);
            this.ButtonRemovePrimitive.TabIndex = 4;
            this.ButtonRemovePrimitive.Text = "Удалить";
            this.ButtonRemovePrimitive.UseVisualStyleBackColor = true;
            this.ButtonRemovePrimitive.Click += new System.EventHandler(this.ButtonRemovePrimitive_Click);
            // 
            // PrimitiveColorSqare
            // 
            this.PrimitiveColorSqare.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PrimitiveColorSqare.Location = new System.Drawing.Point(106, 79);
            this.PrimitiveColorSqare.Name = "PrimitiveColorSqare";
            this.PrimitiveColorSqare.Size = new System.Drawing.Size(19, 22);
            this.PrimitiveColorSqare.TabIndex = 1;
            this.PrimitiveColorSqare.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PrimitiveColorSqare_MouseClick);
            // 
            // FillPrimitive
            // 
            this.FillPrimitive.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FillPrimitive.BackColor = System.Drawing.SystemColors.Control;
            this.FillPrimitive.Location = new System.Drawing.Point(124, 78);
            this.FillPrimitive.Name = "FillPrimitive";
            this.FillPrimitive.Size = new System.Drawing.Size(70, 24);
            this.FillPrimitive.TabIndex = 0;
            this.FillPrimitive.Text = "Закрасить";
            this.FillPrimitive.UseVisualStyleBackColor = true;
            this.FillPrimitive.Click += new System.EventHandler(this.FillPrimitive_Click);
            // 
            // OpenGLLogo
            // 
            this.OpenGLLogo.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.OpenGLLogo.Image = global::CG_Course.Properties.Resources.Opengl_logo_small;
            this.OpenGLLogo.Location = new System.Drawing.Point(58, 673);
            this.OpenGLLogo.Name = "OpenGLLogo";
            this.OpenGLLogo.Size = new System.Drawing.Size(100, 45);
            this.OpenGLLogo.TabIndex = 6;
            this.OpenGLLogo.TabStop = false;
            // 
            // colorDialog
            // 
            this.colorDialog.Color = System.Drawing.Color.Aqua;
            // 
            // VertexColorDialog
            // 
            this.VertexColorDialog.Color = System.Drawing.Color.Aqua;
            // 
            // FlatShadingChkBox
            // 
            this.FlatShadingChkBox.AutoSize = true;
            this.FlatShadingChkBox.Location = new System.Drawing.Point(106, 132);
            this.FlatShadingChkBox.Name = "FlatShadingChkBox";
            this.FlatShadingChkBox.Size = new System.Drawing.Size(83, 17);
            this.FlatShadingChkBox.TabIndex = 9;
            this.FlatShadingChkBox.Text = "Flat shading";
            this.FlatShadingChkBox.UseVisualStyleBackColor = true;
            this.FlatShadingChkBox.CheckedChanged += new System.EventHandler(this.FlatShadingChkBox_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.GLControl);
            this.Controls.Add(this.ControlPanel);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1024, 768);
            this.Name = "MainForm";
            this.Text = "Компьютерная графика - Лабораторная работа №1";
            ((System.ComponentModel.ISupportInitialize)(this.GLControl)).EndInit();
            this.ControlPanel.ResumeLayout(false);
            this.VertexControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CoordsDataGrid)).EndInit();
            this.VertexMenu.ResumeLayout(false);
            this.PrimitiveControls.ResumeLayout(false);
            this.PrimitiveControls.PerformLayout();
            this.PrimitivesMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.OpenGLLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private SharpGL.OpenGLControl GLControl;
        private System.Windows.Forms.Panel ControlPanel;
        private System.Windows.Forms.Button FillPrimitive;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.Panel PrimitiveColorSqare;
        private System.Windows.Forms.ListBox PrimitivesListBox;
        private System.Windows.Forms.Button ButtonCreatePrimitive;
        private System.Windows.Forms.Button ButtonRemovePrimitive;
        private System.Windows.Forms.DataGridView CoordsDataGrid;
        private System.Windows.Forms.PictureBox OpenGLLogo;
        private System.Windows.Forms.Button ButtonRemoveVertex;
        private System.Windows.Forms.CheckBox WireframeChkBox;
        private System.Windows.Forms.Button ButtonRecolorVertex;
        private System.Windows.Forms.GroupBox PrimitiveControls;
        private System.Windows.Forms.GroupBox VertexControls;
        private System.Windows.Forms.ContextMenuStrip PrimitivesMenu;
        private System.Windows.Forms.ToolStripMenuItem PrimitivesMenuNew;
        private System.Windows.Forms.ToolStripMenuItem PrimitivesMenuFill;
        private System.Windows.Forms.ToolStripMenuItem PrimitivesMenuDelete;
        private System.Windows.Forms.ToolStripMenuItem PrimitivesMenuRename;
        private System.Windows.Forms.ToolStripTextBox PrimitivesMenuName;
        private System.Windows.Forms.Panel VertexColorSqare;
        private System.Windows.Forms.ColorDialog VertexColorDialog;
        private System.Windows.Forms.ContextMenuStrip VertexMenu;
        private System.Windows.Forms.ToolStripMenuItem VertexMenuRecolor;
        private System.Windows.Forms.ToolStripMenuItem VertexMenuDelete;
        private System.Windows.Forms.CheckBox FlatShadingChkBox;
    }
}

