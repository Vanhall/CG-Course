namespace RGZ
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
            this.DisplayControls = new System.Windows.Forms.GroupBox();
            this.ColorsControls = new System.Windows.Forms.GroupBox();
            this.PointsColorPick = new System.Windows.Forms.Button();
            this.PointsColorSquare = new System.Windows.Forms.Panel();
            this.LinesColorPick = new System.Windows.Forms.Button();
            this.LinesColorSquare = new System.Windows.Forms.Panel();
            this.PointsColorLabel = new System.Windows.Forms.Label();
            this.SplineColorPick = new System.Windows.Forms.Button();
            this.LinesColorLabel = new System.Windows.Forms.Label();
            this.SplineColorSquare = new System.Windows.Forms.Panel();
            this.SplineColorLabel = new System.Windows.Forms.Label();
            this.DrawGridCHB = new System.Windows.Forms.CheckBox();
            this.DrawLinesCHB = new System.Windows.Forms.CheckBox();
            this.DrawPointsCHB = new System.Windows.Forms.CheckBox();
            this.CPGridControls = new System.Windows.Forms.GroupBox();
            this.RemovePoint = new System.Windows.Forms.Button();
            this.ClearAll = new System.Windows.Forms.Button();
            this.ControlPointsGrid = new System.Windows.Forms.DataGridView();
            this.SplineControls = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.StepsPicker = new System.Windows.Forms.NumericUpDown();
            this.ColorPicker = new System.Windows.Forms.ColorDialog();
            this.DegreePicker = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.GLControl)).BeginInit();
            this.ControlPanel.SuspendLayout();
            this.DisplayControls.SuspendLayout();
            this.ColorsControls.SuspendLayout();
            this.CPGridControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ControlPointsGrid)).BeginInit();
            this.SplineControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StepsPicker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DegreePicker)).BeginInit();
            this.SuspendLayout();
            // 
            // GLControl
            // 
            this.GLControl.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.GLControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GLControl.DrawFPS = false;
            this.GLControl.FrameRate = 30;
            this.GLControl.Location = new System.Drawing.Point(0, 0);
            this.GLControl.Name = "GLControl";
            this.GLControl.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.GLControl.RenderContextType = SharpGL.RenderContextType.FBO;
            this.GLControl.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            this.GLControl.Size = new System.Drawing.Size(788, 729);
            this.GLControl.TabIndex = 0;
            this.GLControl.OpenGLInitialized += new System.EventHandler(this.GLControl_OpenGLInitialized);
            this.GLControl.OpenGLDraw += new SharpGL.RenderEventHandler(this.GLControl_OpenGLDraw);
            this.GLControl.Resized += new System.EventHandler(this.GLControl_Resized);
            this.GLControl.MouseClick += new System.Windows.Forms.MouseEventHandler(this.GLControl_MouseClick);
            // 
            // ControlPanel
            // 
            this.ControlPanel.Controls.Add(this.DisplayControls);
            this.ControlPanel.Controls.Add(this.CPGridControls);
            this.ControlPanel.Controls.Add(this.SplineControls);
            this.ControlPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.ControlPanel.Location = new System.Drawing.Point(788, 0);
            this.ControlPanel.Name = "ControlPanel";
            this.ControlPanel.Size = new System.Drawing.Size(220, 729);
            this.ControlPanel.TabIndex = 1;
            // 
            // DisplayControls
            // 
            this.DisplayControls.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DisplayControls.Controls.Add(this.ColorsControls);
            this.DisplayControls.Controls.Add(this.DrawGridCHB);
            this.DisplayControls.Controls.Add(this.DrawLinesCHB);
            this.DisplayControls.Controls.Add(this.DrawPointsCHB);
            this.DisplayControls.Location = new System.Drawing.Point(3, 526);
            this.DisplayControls.Name = "DisplayControls";
            this.DisplayControls.Size = new System.Drawing.Size(214, 200);
            this.DisplayControls.TabIndex = 8;
            this.DisplayControls.TabStop = false;
            this.DisplayControls.Text = "Отображение";
            // 
            // ColorsControls
            // 
            this.ColorsControls.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ColorsControls.Controls.Add(this.PointsColorPick);
            this.ColorsControls.Controls.Add(this.PointsColorSquare);
            this.ColorsControls.Controls.Add(this.LinesColorPick);
            this.ColorsControls.Controls.Add(this.LinesColorSquare);
            this.ColorsControls.Controls.Add(this.PointsColorLabel);
            this.ColorsControls.Controls.Add(this.SplineColorPick);
            this.ColorsControls.Controls.Add(this.LinesColorLabel);
            this.ColorsControls.Controls.Add(this.SplineColorSquare);
            this.ColorsControls.Controls.Add(this.SplineColorLabel);
            this.ColorsControls.Location = new System.Drawing.Point(6, 87);
            this.ColorsControls.Name = "ColorsControls";
            this.ColorsControls.Size = new System.Drawing.Size(202, 107);
            this.ColorsControls.TabIndex = 3;
            this.ColorsControls.TabStop = false;
            this.ColorsControls.Text = "Цвета";
            // 
            // PointsColorPick
            // 
            this.PointsColorPick.Location = new System.Drawing.Point(85, 77);
            this.PointsColorPick.Name = "PointsColorPick";
            this.PointsColorPick.Size = new System.Drawing.Size(111, 23);
            this.PointsColorPick.TabIndex = 2;
            this.PointsColorPick.Text = "Изменить";
            this.PointsColorPick.UseVisualStyleBackColor = true;
            this.PointsColorPick.Click += new System.EventHandler(this.PointsColorPick_Click);
            // 
            // PointsColorSquare
            // 
            this.PointsColorSquare.BackColor = System.Drawing.Color.Black;
            this.PointsColorSquare.Location = new System.Drawing.Point(56, 77);
            this.PointsColorSquare.Name = "PointsColorSquare";
            this.PointsColorSquare.Size = new System.Drawing.Size(23, 23);
            this.PointsColorSquare.TabIndex = 1;
            // 
            // LinesColorPick
            // 
            this.LinesColorPick.Location = new System.Drawing.Point(85, 48);
            this.LinesColorPick.Name = "LinesColorPick";
            this.LinesColorPick.Size = new System.Drawing.Size(111, 23);
            this.LinesColorPick.TabIndex = 2;
            this.LinesColorPick.Text = "Изменить";
            this.LinesColorPick.UseVisualStyleBackColor = true;
            this.LinesColorPick.Click += new System.EventHandler(this.LinesColorPick_Click);
            // 
            // LinesColorSquare
            // 
            this.LinesColorSquare.BackColor = System.Drawing.Color.Blue;
            this.LinesColorSquare.Location = new System.Drawing.Point(56, 48);
            this.LinesColorSquare.Name = "LinesColorSquare";
            this.LinesColorSquare.Size = new System.Drawing.Size(23, 23);
            this.LinesColorSquare.TabIndex = 1;
            // 
            // PointsColorLabel
            // 
            this.PointsColorLabel.AutoSize = true;
            this.PointsColorLabel.Location = new System.Drawing.Point(6, 82);
            this.PointsColorLabel.Name = "PointsColorLabel";
            this.PointsColorLabel.Size = new System.Drawing.Size(37, 13);
            this.PointsColorLabel.TabIndex = 0;
            this.PointsColorLabel.Text = "Точки";
            // 
            // SplineColorPick
            // 
            this.SplineColorPick.Location = new System.Drawing.Point(85, 19);
            this.SplineColorPick.Name = "SplineColorPick";
            this.SplineColorPick.Size = new System.Drawing.Size(111, 23);
            this.SplineColorPick.TabIndex = 2;
            this.SplineColorPick.Text = "Изменить";
            this.SplineColorPick.UseVisualStyleBackColor = true;
            this.SplineColorPick.Click += new System.EventHandler(this.SplineColorPick_Click);
            // 
            // LinesColorLabel
            // 
            this.LinesColorLabel.AutoSize = true;
            this.LinesColorLabel.Location = new System.Drawing.Point(6, 53);
            this.LinesColorLabel.Name = "LinesColorLabel";
            this.LinesColorLabel.Size = new System.Drawing.Size(39, 13);
            this.LinesColorLabel.TabIndex = 0;
            this.LinesColorLabel.Text = "Линии";
            // 
            // SplineColorSquare
            // 
            this.SplineColorSquare.BackColor = System.Drawing.Color.Red;
            this.SplineColorSquare.Location = new System.Drawing.Point(56, 19);
            this.SplineColorSquare.Name = "SplineColorSquare";
            this.SplineColorSquare.Size = new System.Drawing.Size(23, 23);
            this.SplineColorSquare.TabIndex = 1;
            // 
            // SplineColorLabel
            // 
            this.SplineColorLabel.AutoSize = true;
            this.SplineColorLabel.Location = new System.Drawing.Point(6, 24);
            this.SplineColorLabel.Name = "SplineColorLabel";
            this.SplineColorLabel.Size = new System.Drawing.Size(44, 13);
            this.SplineColorLabel.TabIndex = 0;
            this.SplineColorLabel.Text = "Сплайн";
            // 
            // DrawGridCHB
            // 
            this.DrawGridCHB.AutoSize = true;
            this.DrawGridCHB.Checked = true;
            this.DrawGridCHB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DrawGridCHB.Location = new System.Drawing.Point(12, 68);
            this.DrawGridCHB.Name = "DrawGridCHB";
            this.DrawGridCHB.Size = new System.Drawing.Size(130, 17);
            this.DrawGridCHB.TabIndex = 2;
            this.DrawGridCHB.Text = "Координатная сетка";
            this.DrawGridCHB.UseVisualStyleBackColor = true;
            this.DrawGridCHB.CheckedChanged += new System.EventHandler(this.DrawGridCHB_CheckedChanged);
            // 
            // DrawLinesCHB
            // 
            this.DrawLinesCHB.AutoSize = true;
            this.DrawLinesCHB.Checked = true;
            this.DrawLinesCHB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DrawLinesCHB.Location = new System.Drawing.Point(12, 44);
            this.DrawLinesCHB.Name = "DrawLinesCHB";
            this.DrawLinesCHB.Size = new System.Drawing.Size(138, 17);
            this.DrawLinesCHB.TabIndex = 1;
            this.DrawLinesCHB.Text = "Направляющие линии";
            this.DrawLinesCHB.UseVisualStyleBackColor = true;
            this.DrawLinesCHB.CheckedChanged += new System.EventHandler(this.DrawLinesCHB_CheckedChanged);
            // 
            // DrawPointsCHB
            // 
            this.DrawPointsCHB.AutoSize = true;
            this.DrawPointsCHB.Checked = true;
            this.DrawPointsCHB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DrawPointsCHB.Location = new System.Drawing.Point(12, 20);
            this.DrawPointsCHB.Name = "DrawPointsCHB";
            this.DrawPointsCHB.Size = new System.Drawing.Size(125, 17);
            this.DrawPointsCHB.TabIndex = 0;
            this.DrawPointsCHB.Text = "Контрольные точки";
            this.DrawPointsCHB.UseVisualStyleBackColor = true;
            this.DrawPointsCHB.CheckedChanged += new System.EventHandler(this.DrawPointsCHB_CheckedChanged);
            // 
            // CPGridControls
            // 
            this.CPGridControls.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CPGridControls.Controls.Add(this.RemovePoint);
            this.CPGridControls.Controls.Add(this.ClearAll);
            this.CPGridControls.Controls.Add(this.ControlPointsGrid);
            this.CPGridControls.Location = new System.Drawing.Point(3, 84);
            this.CPGridControls.Name = "CPGridControls";
            this.CPGridControls.Size = new System.Drawing.Size(214, 436);
            this.CPGridControls.TabIndex = 7;
            this.CPGridControls.TabStop = false;
            this.CPGridControls.Text = "Контрольные точки";
            // 
            // RemovePoint
            // 
            this.RemovePoint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.RemovePoint.Location = new System.Drawing.Point(118, 403);
            this.RemovePoint.Name = "RemovePoint";
            this.RemovePoint.Size = new System.Drawing.Size(90, 23);
            this.RemovePoint.TabIndex = 8;
            this.RemovePoint.Text = "Удалить";
            this.RemovePoint.UseVisualStyleBackColor = true;
            this.RemovePoint.Click += new System.EventHandler(this.RemovePoint_Click);
            // 
            // ClearAll
            // 
            this.ClearAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ClearAll.Location = new System.Drawing.Point(6, 403);
            this.ClearAll.Name = "ClearAll";
            this.ClearAll.Size = new System.Drawing.Size(90, 23);
            this.ClearAll.TabIndex = 7;
            this.ClearAll.Text = "Очистить";
            this.ClearAll.UseVisualStyleBackColor = true;
            this.ClearAll.Click += new System.EventHandler(this.ClearAll_Click);
            // 
            // ControlPointsGrid
            // 
            this.ControlPointsGrid.AllowUserToAddRows = false;
            this.ControlPointsGrid.AllowUserToDeleteRows = false;
            this.ControlPointsGrid.AllowUserToResizeColumns = false;
            this.ControlPointsGrid.AllowUserToResizeRows = false;
            this.ControlPointsGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ControlPointsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ControlPointsGrid.Location = new System.Drawing.Point(6, 19);
            this.ControlPointsGrid.MultiSelect = false;
            this.ControlPointsGrid.Name = "ControlPointsGrid";
            this.ControlPointsGrid.ReadOnly = true;
            this.ControlPointsGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.ControlPointsGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ControlPointsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ControlPointsGrid.Size = new System.Drawing.Size(202, 378);
            this.ControlPointsGrid.TabIndex = 6;
            this.ControlPointsGrid.SelectionChanged += new System.EventHandler(this.ControlPointsGrid_SelectionChanged);
            // 
            // SplineControls
            // 
            this.SplineControls.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SplineControls.Controls.Add(this.DegreePicker);
            this.SplineControls.Controls.Add(this.label2);
            this.SplineControls.Controls.Add(this.label1);
            this.SplineControls.Controls.Add(this.StepsPicker);
            this.SplineControls.Location = new System.Drawing.Point(3, 12);
            this.SplineControls.Name = "SplineControls";
            this.SplineControls.Size = new System.Drawing.Size(214, 66);
            this.SplineControls.TabIndex = 0;
            this.SplineControls.TabStop = false;
            this.SplineControls.Text = "Сплайн";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Кол-во шагов на интервал:";
            // 
            // StepsPicker
            // 
            this.StepsPicker.Location = new System.Drawing.Point(169, 14);
            this.StepsPicker.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.StepsPicker.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.StepsPicker.Name = "StepsPicker";
            this.StepsPicker.Size = new System.Drawing.Size(39, 20);
            this.StepsPicker.TabIndex = 9;
            this.StepsPicker.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.StepsPicker.ValueChanged += new System.EventHandler(this.StepsPicker_ValueChanged);
            // 
            // ColorPicker
            // 
            this.ColorPicker.FullOpen = true;
            // 
            // DegreePicker
            // 
            this.DegreePicker.Location = new System.Drawing.Point(169, 40);
            this.DegreePicker.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.DegreePicker.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.DegreePicker.Name = "DegreePicker";
            this.DegreePicker.Size = new System.Drawing.Size(39, 20);
            this.DegreePicker.TabIndex = 11;
            this.DegreePicker.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.DegreePicker.ValueChanged += new System.EventHandler(this.DegreePicker_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Степень сплайна:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.GLControl);
            this.Controls.Add(this.ControlPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1024, 768);
            this.Name = "MainForm";
            this.Text = "Компьютерная графика - Расчетно-графическое задание (закрытый B-сплайн)";
            ((System.ComponentModel.ISupportInitialize)(this.GLControl)).EndInit();
            this.ControlPanel.ResumeLayout(false);
            this.DisplayControls.ResumeLayout(false);
            this.DisplayControls.PerformLayout();
            this.ColorsControls.ResumeLayout(false);
            this.ColorsControls.PerformLayout();
            this.CPGridControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ControlPointsGrid)).EndInit();
            this.SplineControls.ResumeLayout(false);
            this.SplineControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StepsPicker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DegreePicker)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private SharpGL.OpenGLControl GLControl;
        private System.Windows.Forms.Panel ControlPanel;
        private System.Windows.Forms.GroupBox SplineControls;
        private System.Windows.Forms.DataGridView ControlPointsGrid;
        private System.Windows.Forms.GroupBox CPGridControls;
        private System.Windows.Forms.Button ClearAll;
        private System.Windows.Forms.GroupBox DisplayControls;
        private System.Windows.Forms.CheckBox DrawLinesCHB;
        private System.Windows.Forms.CheckBox DrawPointsCHB;
        private System.Windows.Forms.Button RemovePoint;
        private System.Windows.Forms.NumericUpDown StepsPicker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox DrawGridCHB;
        private System.Windows.Forms.GroupBox ColorsControls;
        private System.Windows.Forms.Button PointsColorPick;
        private System.Windows.Forms.Panel PointsColorSquare;
        private System.Windows.Forms.Button LinesColorPick;
        private System.Windows.Forms.Panel LinesColorSquare;
        private System.Windows.Forms.Label PointsColorLabel;
        private System.Windows.Forms.Button SplineColorPick;
        private System.Windows.Forms.Label LinesColorLabel;
        private System.Windows.Forms.Panel SplineColorSquare;
        private System.Windows.Forms.Label SplineColorLabel;
        private System.Windows.Forms.ColorDialog ColorPicker;
        private System.Windows.Forms.NumericUpDown DegreePicker;
        private System.Windows.Forms.Label label2;
    }
}

