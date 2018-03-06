namespace LR_3
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
            this.MainPanel = new System.Windows.Forms.Panel();
            this.ProjectionControls = new System.Windows.Forms.GroupBox();
            this.OrthoRB = new System.Windows.Forms.RadioButton();
            this.PerspectiveRB = new System.Windows.Forms.RadioButton();
            this.ModelControls = new System.Windows.Forms.GroupBox();
            this.MaterialSwitcher = new System.Windows.Forms.ComboBox();
            this.WireframeChBox = new System.Windows.Forms.CheckBox();
            this.SmoothChBox = new System.Windows.Forms.CheckBox();
            this.TextureChBox = new System.Windows.Forms.CheckBox();
            this.NormalsChBox = new System.Windows.Forms.CheckBox();
            this.SectionsChBox = new System.Windows.Forms.CheckBox();
            this.TrajectoryChBox = new System.Windows.Forms.CheckBox();
            this.PickModel = new System.Windows.Forms.Button();
            this.FileDialog = new System.Windows.Forms.OpenFileDialog();
            this.ViewControls = new System.Windows.Forms.GroupBox();
            this.MaterialLabel = new System.Windows.Forms.Label();
            this.PickTexture = new System.Windows.Forms.Button();
            this.TexturePreview = new System.Windows.Forms.PictureBox();
            this.TextureLabel = new System.Windows.Forms.Label();
            this.TextureFileDialog = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.GLControl)).BeginInit();
            this.MainPanel.SuspendLayout();
            this.ProjectionControls.SuspendLayout();
            this.ModelControls.SuspendLayout();
            this.ViewControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TexturePreview)).BeginInit();
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
            this.GLControl.Size = new System.Drawing.Size(797, 729);
            this.GLControl.TabIndex = 0;
            this.GLControl.OpenGLInitialized += new System.EventHandler(this.GLControl_OpenGLInitialized);
            this.GLControl.OpenGLDraw += new SharpGL.RenderEventHandler(this.GLControl_OpenGLDraw);
            this.GLControl.Resized += new System.EventHandler(this.GLControl_Resized);
            this.GLControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GLControl_MouseDown);
            this.GLControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GLControl_MouseMove);
            this.GLControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GLControl_MouseUp);
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.ViewControls);
            this.MainPanel.Controls.Add(this.ProjectionControls);
            this.MainPanel.Controls.Add(this.ModelControls);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.MainPanel.Location = new System.Drawing.Point(797, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(211, 729);
            this.MainPanel.TabIndex = 1;
            // 
            // ProjectionControls
            // 
            this.ProjectionControls.Controls.Add(this.OrthoRB);
            this.ProjectionControls.Controls.Add(this.PerspectiveRB);
            this.ProjectionControls.Location = new System.Drawing.Point(3, 201);
            this.ProjectionControls.Name = "ProjectionControls";
            this.ProjectionControls.Size = new System.Drawing.Size(205, 60);
            this.ProjectionControls.TabIndex = 1;
            this.ProjectionControls.TabStop = false;
            this.ProjectionControls.Text = "Проекция";
            // 
            // OrthoRB
            // 
            this.OrthoRB.AutoSize = true;
            this.OrthoRB.Location = new System.Drawing.Point(99, 19);
            this.OrthoRB.Name = "OrthoRB";
            this.OrthoRB.Size = new System.Drawing.Size(101, 30);
            this.OrthoRB.TabIndex = 1;
            this.OrthoRB.Text = "Ортографичес-\r\nкая";
            this.OrthoRB.UseVisualStyleBackColor = true;
            // 
            // PerspectiveRB
            // 
            this.PerspectiveRB.AutoSize = true;
            this.PerspectiveRB.Checked = true;
            this.PerspectiveRB.Location = new System.Drawing.Point(7, 20);
            this.PerspectiveRB.Name = "PerspectiveRB";
            this.PerspectiveRB.Size = new System.Drawing.Size(89, 30);
            this.PerspectiveRB.TabIndex = 0;
            this.PerspectiveRB.TabStop = true;
            this.PerspectiveRB.Text = "Перспектив-\r\nная";
            this.PerspectiveRB.UseVisualStyleBackColor = true;
            // 
            // ModelControls
            // 
            this.ModelControls.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ModelControls.Controls.Add(this.TexturePreview);
            this.ModelControls.Controls.Add(this.PickTexture);
            this.ModelControls.Controls.Add(this.TextureLabel);
            this.ModelControls.Controls.Add(this.MaterialLabel);
            this.ModelControls.Controls.Add(this.MaterialSwitcher);
            this.ModelControls.Controls.Add(this.PickModel);
            this.ModelControls.Location = new System.Drawing.Point(3, 13);
            this.ModelControls.Name = "ModelControls";
            this.ModelControls.Size = new System.Drawing.Size(205, 90);
            this.ModelControls.TabIndex = 0;
            this.ModelControls.TabStop = false;
            this.ModelControls.Text = "Модель";
            // 
            // MaterialSwitcher
            // 
            this.MaterialSwitcher.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MaterialSwitcher.FormattingEnabled = true;
            this.MaterialSwitcher.Location = new System.Drawing.Point(99, 63);
            this.MaterialSwitcher.Name = "MaterialSwitcher";
            this.MaterialSwitcher.Size = new System.Drawing.Size(97, 21);
            this.MaterialSwitcher.TabIndex = 7;
            this.MaterialSwitcher.SelectedIndexChanged += new System.EventHandler(this.MaterialSwitcher_SelectedIndexChanged);
            // 
            // WireframeChBox
            // 
            this.WireframeChBox.AutoSize = true;
            this.WireframeChBox.Location = new System.Drawing.Point(7, 65);
            this.WireframeChBox.Name = "WireframeChBox";
            this.WireframeChBox.Size = new System.Drawing.Size(63, 17);
            this.WireframeChBox.TabIndex = 6;
            this.WireframeChBox.Text = "Каркас";
            this.WireframeChBox.UseVisualStyleBackColor = true;
            this.WireframeChBox.CheckedChanged += new System.EventHandler(this.WireframeChBox_CheckedChanged);
            // 
            // SmoothChBox
            // 
            this.SmoothChBox.AutoSize = true;
            this.SmoothChBox.Location = new System.Drawing.Point(99, 42);
            this.SmoothChBox.Name = "SmoothChBox";
            this.SmoothChBox.Size = new System.Drawing.Size(94, 17);
            this.SmoothChBox.TabIndex = 5;
            this.SmoothChBox.Text = "Сглаживание";
            this.SmoothChBox.UseVisualStyleBackColor = true;
            this.SmoothChBox.CheckedChanged += new System.EventHandler(this.SmoothChBox_CheckedChanged);
            // 
            // TextureChBox
            // 
            this.TextureChBox.AutoSize = true;
            this.TextureChBox.Checked = true;
            this.TextureChBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.TextureChBox.Location = new System.Drawing.Point(99, 65);
            this.TextureChBox.Name = "TextureChBox";
            this.TextureChBox.Size = new System.Drawing.Size(73, 17);
            this.TextureChBox.TabIndex = 4;
            this.TextureChBox.Text = "Текстура";
            this.TextureChBox.UseVisualStyleBackColor = true;
            this.TextureChBox.CheckedChanged += new System.EventHandler(this.TextureChBox_CheckedChanged);
            // 
            // NormalsChBox
            // 
            this.NormalsChBox.AutoSize = true;
            this.NormalsChBox.Location = new System.Drawing.Point(7, 42);
            this.NormalsChBox.Name = "NormalsChBox";
            this.NormalsChBox.Size = new System.Drawing.Size(72, 17);
            this.NormalsChBox.TabIndex = 3;
            this.NormalsChBox.Text = "Нормали";
            this.NormalsChBox.UseVisualStyleBackColor = true;
            this.NormalsChBox.CheckedChanged += new System.EventHandler(this.NormalsChBox_CheckedChanged);
            // 
            // SectionsChBox
            // 
            this.SectionsChBox.AutoSize = true;
            this.SectionsChBox.Location = new System.Drawing.Point(99, 19);
            this.SectionsChBox.Name = "SectionsChBox";
            this.SectionsChBox.Size = new System.Drawing.Size(68, 17);
            this.SectionsChBox.TabIndex = 2;
            this.SectionsChBox.Text = "Сечения";
            this.SectionsChBox.UseVisualStyleBackColor = true;
            this.SectionsChBox.CheckedChanged += new System.EventHandler(this.SectionsChBox_CheckedChanged);
            // 
            // TrajectoryChBox
            // 
            this.TrajectoryChBox.AutoSize = true;
            this.TrajectoryChBox.Location = new System.Drawing.Point(7, 19);
            this.TrajectoryChBox.Name = "TrajectoryChBox";
            this.TrajectoryChBox.Size = new System.Drawing.Size(86, 17);
            this.TrajectoryChBox.TabIndex = 1;
            this.TrajectoryChBox.Text = "Траектория";
            this.TrajectoryChBox.UseVisualStyleBackColor = true;
            this.TrajectoryChBox.CheckedChanged += new System.EventHandler(this.TrajectoryChBox_CheckedChanged);
            // 
            // PickModel
            // 
            this.PickModel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PickModel.Location = new System.Drawing.Point(7, 20);
            this.PickModel.Name = "PickModel";
            this.PickModel.Size = new System.Drawing.Size(189, 23);
            this.PickModel.TabIndex = 0;
            this.PickModel.Text = "Модель: Name";
            this.PickModel.UseVisualStyleBackColor = true;
            this.PickModel.Click += new System.EventHandler(this.PickModel_Click);
            // 
            // FileDialog
            // 
            this.FileDialog.Filter = "XML Model|*.xml";
            // 
            // ViewControls
            // 
            this.ViewControls.Controls.Add(this.TrajectoryChBox);
            this.ViewControls.Controls.Add(this.WireframeChBox);
            this.ViewControls.Controls.Add(this.SectionsChBox);
            this.ViewControls.Controls.Add(this.SmoothChBox);
            this.ViewControls.Controls.Add(this.NormalsChBox);
            this.ViewControls.Controls.Add(this.TextureChBox);
            this.ViewControls.Location = new System.Drawing.Point(3, 109);
            this.ViewControls.Name = "ViewControls";
            this.ViewControls.Size = new System.Drawing.Size(205, 86);
            this.ViewControls.TabIndex = 2;
            this.ViewControls.TabStop = false;
            this.ViewControls.Text = "Параметры отображения";
            // 
            // MaterialLabel
            // 
            this.MaterialLabel.AutoSize = true;
            this.MaterialLabel.Location = new System.Drawing.Point(96, 47);
            this.MaterialLabel.Name = "MaterialLabel";
            this.MaterialLabel.Size = new System.Drawing.Size(60, 13);
            this.MaterialLabel.TabIndex = 8;
            this.MaterialLabel.Text = "Материал:";
            // 
            // PickTexture
            // 
            this.PickTexture.Location = new System.Drawing.Point(28, 62);
            this.PickTexture.Name = "PickTexture";
            this.PickTexture.Size = new System.Drawing.Size(63, 23);
            this.PickTexture.TabIndex = 9;
            this.PickTexture.Text = "Обзор...";
            this.PickTexture.UseVisualStyleBackColor = true;
            this.PickTexture.Click += new System.EventHandler(this.PickTexture_Click);
            // 
            // TexturePreview
            // 
            this.TexturePreview.Image = global::LR_3.Properties.Resources.photosicon;
            this.TexturePreview.Location = new System.Drawing.Point(5, 62);
            this.TexturePreview.Name = "TexturePreview";
            this.TexturePreview.Size = new System.Drawing.Size(23, 23);
            this.TexturePreview.TabIndex = 3;
            this.TexturePreview.TabStop = false;
            // 
            // TextureLabel
            // 
            this.TextureLabel.AutoSize = true;
            this.TextureLabel.Location = new System.Drawing.Point(4, 46);
            this.TextureLabel.Name = "TextureLabel";
            this.TextureLabel.Size = new System.Drawing.Size(57, 13);
            this.TextureLabel.TabIndex = 8;
            this.TextureLabel.Text = "Текстура:";
            // 
            // TextureFileDialog
            // 
            this.TextureFileDialog.Filter = "PNG Image|*.png";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.GLControl);
            this.Controls.Add(this.MainPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(1024, 768);
            this.Name = "MainForm";
            this.Text = "Компьютерная графика - Лабораторная работа №3";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.GLControl)).EndInit();
            this.MainPanel.ResumeLayout(false);
            this.ProjectionControls.ResumeLayout(false);
            this.ProjectionControls.PerformLayout();
            this.ModelControls.ResumeLayout(false);
            this.ModelControls.PerformLayout();
            this.ViewControls.ResumeLayout(false);
            this.ViewControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TexturePreview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private SharpGL.OpenGLControl GLControl;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.GroupBox ModelControls;
        private System.Windows.Forms.CheckBox WireframeChBox;
        private System.Windows.Forms.CheckBox SmoothChBox;
        private System.Windows.Forms.CheckBox TextureChBox;
        private System.Windows.Forms.CheckBox NormalsChBox;
        private System.Windows.Forms.CheckBox SectionsChBox;
        private System.Windows.Forms.CheckBox TrajectoryChBox;
        private System.Windows.Forms.Button PickModel;
        private System.Windows.Forms.ComboBox MaterialSwitcher;
        private System.Windows.Forms.GroupBox ProjectionControls;
        private System.Windows.Forms.RadioButton OrthoRB;
        private System.Windows.Forms.RadioButton PerspectiveRB;
        private System.Windows.Forms.OpenFileDialog FileDialog;
        private System.Windows.Forms.PictureBox TexturePreview;
        private System.Windows.Forms.GroupBox ViewControls;
        private System.Windows.Forms.Button PickTexture;
        private System.Windows.Forms.Label MaterialLabel;
        private System.Windows.Forms.Label TextureLabel;
        private System.Windows.Forms.OpenFileDialog TextureFileDialog;
    }
}

