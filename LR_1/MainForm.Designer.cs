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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.GLControl = new SharpGL.OpenGLControl();
            this.ControlPanel = new System.Windows.Forms.Panel();
            this.WireframeChkBox = new System.Windows.Forms.CheckBox();
            this.ButtonRemoveVertex = new System.Windows.Forms.Button();
            this.OpenGLLogo = new System.Windows.Forms.PictureBox();
            this.CoordsDataGrid = new System.Windows.Forms.DataGridView();
            this.ButtonRemovePrimitive = new System.Windows.Forms.Button();
            this.ButtonCreatePrimitive = new System.Windows.Forms.Button();
            this.PrimitivesListBox = new System.Windows.Forms.ListBox();
            this.ColorSquare = new System.Windows.Forms.Panel();
            this.ButtonColorPick = new System.Windows.Forms.Button();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.ButtonFill = new System.Windows.Forms.Button();
            this.ButtonRecolorVertex = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.GLControl)).BeginInit();
            this.ControlPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OpenGLLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CoordsDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // GLControl
            // 
            this.GLControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GLControl.BackColor = System.Drawing.SystemColors.Desktop;
            this.GLControl.Cursor = System.Windows.Forms.Cursors.Cross;
            this.GLControl.DrawFPS = false;
            this.GLControl.FrameRate = 30;
            this.GLControl.Location = new System.Drawing.Point(0, 0);
            this.GLControl.Name = "GLControl";
            this.GLControl.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.GLControl.RenderContextType = SharpGL.RenderContextType.FBO;
            this.GLControl.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            this.GLControl.Size = new System.Drawing.Size(813, 729);
            this.GLControl.TabIndex = 0;
            this.GLControl.OpenGLInitialized += new System.EventHandler(this.GLControl_OpenGLInitialized);
            this.GLControl.OpenGLDraw += new SharpGL.RenderEventHandler(this.GLControl_OpenGLDraw);
            this.GLControl.Resized += new System.EventHandler(this.GLControl_Resized);
            this.GLControl.MouseClick += new System.Windows.Forms.MouseEventHandler(this.GLControl_MouseClick);
            // 
            // ControlPanel
            // 
            this.ControlPanel.BackColor = System.Drawing.SystemColors.Control;
            this.ControlPanel.Controls.Add(this.ButtonFill);
            this.ControlPanel.Controls.Add(this.WireframeChkBox);
            this.ControlPanel.Controls.Add(this.ButtonRecolorVertex);
            this.ControlPanel.Controls.Add(this.ButtonRemoveVertex);
            this.ControlPanel.Controls.Add(this.OpenGLLogo);
            this.ControlPanel.Controls.Add(this.CoordsDataGrid);
            this.ControlPanel.Controls.Add(this.ButtonRemovePrimitive);
            this.ControlPanel.Controls.Add(this.ButtonCreatePrimitive);
            this.ControlPanel.Controls.Add(this.PrimitivesListBox);
            this.ControlPanel.Controls.Add(this.ColorSquare);
            this.ControlPanel.Controls.Add(this.ButtonColorPick);
            this.ControlPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.ControlPanel.Location = new System.Drawing.Point(813, 0);
            this.ControlPanel.Name = "ControlPanel";
            this.ControlPanel.Size = new System.Drawing.Size(195, 729);
            this.ControlPanel.TabIndex = 1;
            // 
            // WireframeChkBox
            // 
            this.WireframeChkBox.AutoSize = true;
            this.WireframeChkBox.Location = new System.Drawing.Point(106, 132);
            this.WireframeChkBox.Name = "WireframeChkBox";
            this.WireframeChkBox.Size = new System.Drawing.Size(74, 17);
            this.WireframeChkBox.TabIndex = 8;
            this.WireframeChkBox.Text = "Wireframe";
            this.WireframeChkBox.UseVisualStyleBackColor = true;
            this.WireframeChkBox.CheckedChanged += new System.EventHandler(this.WireframeChkBox_CheckedChanged);
            // 
            // ButtonRemoveVertex
            // 
            this.ButtonRemoveVertex.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.ButtonRemoveVertex.Location = new System.Drawing.Point(106, 606);
            this.ButtonRemoveVertex.Name = "ButtonRemoveVertex";
            this.ButtonRemoveVertex.Size = new System.Drawing.Size(82, 35);
            this.ButtonRemoveVertex.TabIndex = 7;
            this.ButtonRemoveVertex.Text = "Удалить вершину";
            this.ButtonRemoveVertex.UseVisualStyleBackColor = true;
            this.ButtonRemoveVertex.Click += new System.EventHandler(this.ButtonRemoveVertex_Click);
            // 
            // OpenGLLogo
            // 
            this.OpenGLLogo.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.OpenGLLogo.Image = global::CG_Course.Properties.Resources.Opengl_logo_small;
            this.OpenGLLogo.Location = new System.Drawing.Point(49, 673);
            this.OpenGLLogo.Name = "OpenGLLogo";
            this.OpenGLLogo.Size = new System.Drawing.Size(100, 45);
            this.OpenGLLogo.TabIndex = 6;
            this.OpenGLLogo.TabStop = false;
            // 
            // CoordsDataGrid
            // 
            this.CoordsDataGrid.AllowUserToAddRows = false;
            this.CoordsDataGrid.AllowUserToDeleteRows = false;
            this.CoordsDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.CoordsDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CoordsDataGrid.Location = new System.Drawing.Point(6, 194);
            this.CoordsDataGrid.MultiSelect = false;
            this.CoordsDataGrid.Name = "CoordsDataGrid";
            this.CoordsDataGrid.ReadOnly = true;
            this.CoordsDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.CoordsDataGrid.Size = new System.Drawing.Size(182, 406);
            this.CoordsDataGrid.TabIndex = 5;
            this.CoordsDataGrid.SelectionChanged += new System.EventHandler(this.CoordsDataGrid_SelectionChanged);
            // 
            // ButtonRemovePrimitive
            // 
            this.ButtonRemovePrimitive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonRemovePrimitive.Location = new System.Drawing.Point(106, 42);
            this.ButtonRemovePrimitive.Name = "ButtonRemovePrimitive";
            this.ButtonRemovePrimitive.Size = new System.Drawing.Size(82, 24);
            this.ButtonRemovePrimitive.TabIndex = 4;
            this.ButtonRemovePrimitive.Text = "Удалить";
            this.ButtonRemovePrimitive.UseVisualStyleBackColor = true;
            this.ButtonRemovePrimitive.Click += new System.EventHandler(this.ButtonRemovePrimitive_Click);
            // 
            // ButtonCreatePrimitive
            // 
            this.ButtonCreatePrimitive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonCreatePrimitive.Location = new System.Drawing.Point(106, 12);
            this.ButtonCreatePrimitive.Name = "ButtonCreatePrimitive";
            this.ButtonCreatePrimitive.Size = new System.Drawing.Size(82, 24);
            this.ButtonCreatePrimitive.TabIndex = 3;
            this.ButtonCreatePrimitive.Text = "Новый";
            this.ButtonCreatePrimitive.UseVisualStyleBackColor = true;
            this.ButtonCreatePrimitive.Click += new System.EventHandler(this.ButtonCreatePrimitive_Click);
            // 
            // PrimitivesListBox
            // 
            this.PrimitivesListBox.FormattingEnabled = true;
            this.PrimitivesListBox.Location = new System.Drawing.Point(6, 12);
            this.PrimitivesListBox.Name = "PrimitivesListBox";
            this.PrimitivesListBox.Size = new System.Drawing.Size(94, 160);
            this.PrimitivesListBox.TabIndex = 2;
            this.PrimitivesListBox.SelectedIndexChanged += new System.EventHandler(this.PrimitivesListBox_SelectedIndexChanged);
            // 
            // ColorSquare
            // 
            this.ColorSquare.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ColorSquare.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ColorSquare.Location = new System.Drawing.Point(106, 73);
            this.ColorSquare.Name = "ColorSquare";
            this.ColorSquare.Size = new System.Drawing.Size(19, 22);
            this.ColorSquare.TabIndex = 1;
            // 
            // ButtonColorPick
            // 
            this.ButtonColorPick.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonColorPick.BackColor = System.Drawing.SystemColors.Control;
            this.ButtonColorPick.Location = new System.Drawing.Point(124, 72);
            this.ButtonColorPick.Name = "ButtonColorPick";
            this.ButtonColorPick.Size = new System.Drawing.Size(64, 24);
            this.ButtonColorPick.TabIndex = 0;
            this.ButtonColorPick.Text = "Цвет";
            this.ButtonColorPick.UseVisualStyleBackColor = true;
            this.ButtonColorPick.Click += new System.EventHandler(this.ButtonColorPick_Click);
            // 
            // colorDialog
            // 
            this.colorDialog.Color = System.Drawing.Color.Aqua;
            // 
            // ButtonFill
            // 
            this.ButtonFill.Location = new System.Drawing.Point(106, 102);
            this.ButtonFill.Name = "ButtonFill";
            this.ButtonFill.Size = new System.Drawing.Size(82, 24);
            this.ButtonFill.TabIndex = 9;
            this.ButtonFill.Text = "Закрасить";
            this.ButtonFill.UseVisualStyleBackColor = true;
            this.ButtonFill.Click += new System.EventHandler(this.ButtonFill_Click);
            // 
            // ButtonRecolorVertex
            // 
            this.ButtonRecolorVertex.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.ButtonRecolorVertex.Location = new System.Drawing.Point(6, 606);
            this.ButtonRecolorVertex.Name = "ButtonRecolorVertex";
            this.ButtonRecolorVertex.Size = new System.Drawing.Size(82, 35);
            this.ButtonRecolorVertex.TabIndex = 7;
            this.ButtonRecolorVertex.Text = "Закрасить вершину";
            this.ButtonRecolorVertex.UseVisualStyleBackColor = true;
            this.ButtonRecolorVertex.Click += new System.EventHandler(this.ButtonRecolorVertex_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.ControlPanel);
            this.Controls.Add(this.GLControl);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1024, 768);
            this.Name = "MainForm";
            this.Text = "Компьютерная графика - Лабораторная работа №1";
            ((System.ComponentModel.ISupportInitialize)(this.GLControl)).EndInit();
            this.ControlPanel.ResumeLayout(false);
            this.ControlPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OpenGLLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CoordsDataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private SharpGL.OpenGLControl GLControl;
        private System.Windows.Forms.Panel ControlPanel;
        private System.Windows.Forms.Button ButtonColorPick;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.Panel ColorSquare;
        private System.Windows.Forms.ListBox PrimitivesListBox;
        private System.Windows.Forms.Button ButtonCreatePrimitive;
        private System.Windows.Forms.Button ButtonRemovePrimitive;
        private System.Windows.Forms.DataGridView CoordsDataGrid;
        private System.Windows.Forms.PictureBox OpenGLLogo;
        private System.Windows.Forms.Button ButtonRemoveVertex;
        private System.Windows.Forms.CheckBox WireframeChkBox;
        private System.Windows.Forms.Button ButtonFill;
        private System.Windows.Forms.Button ButtonRecolorVertex;
    }
}

