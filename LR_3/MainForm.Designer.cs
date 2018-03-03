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
            this.ModelControls = new System.Windows.Forms.GroupBox();
            this.PickModel = new System.Windows.Forms.Button();
            this.TrajectoryChBox = new System.Windows.Forms.CheckBox();
            this.SectionsChBox = new System.Windows.Forms.CheckBox();
            this.NormalsChBox = new System.Windows.Forms.CheckBox();
            this.TextureChBox = new System.Windows.Forms.CheckBox();
            this.SmoothChBox = new System.Windows.Forms.CheckBox();
            this.WireframeChBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.GLControl)).BeginInit();
            this.MainPanel.SuspendLayout();
            this.ModelControls.SuspendLayout();
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
            this.MainPanel.Controls.Add(this.ModelControls);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.MainPanel.Location = new System.Drawing.Point(797, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(211, 729);
            this.MainPanel.TabIndex = 1;
            // 
            // ModelControls
            // 
            this.ModelControls.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ModelControls.Controls.Add(this.WireframeChBox);
            this.ModelControls.Controls.Add(this.SmoothChBox);
            this.ModelControls.Controls.Add(this.TextureChBox);
            this.ModelControls.Controls.Add(this.NormalsChBox);
            this.ModelControls.Controls.Add(this.SectionsChBox);
            this.ModelControls.Controls.Add(this.TrajectoryChBox);
            this.ModelControls.Controls.Add(this.PickModel);
            this.ModelControls.Location = new System.Drawing.Point(3, 13);
            this.ModelControls.Name = "ModelControls";
            this.ModelControls.Size = new System.Drawing.Size(205, 175);
            this.ModelControls.TabIndex = 0;
            this.ModelControls.TabStop = false;
            this.ModelControls.Text = "Модель";
            // 
            // PickModel
            // 
            this.PickModel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PickModel.Location = new System.Drawing.Point(7, 20);
            this.PickModel.Name = "PickModel";
            this.PickModel.Size = new System.Drawing.Size(189, 23);
            this.PickModel.TabIndex = 0;
            this.PickModel.Text = "button1";
            this.PickModel.UseVisualStyleBackColor = true;
            // 
            // TrajectoryChBox
            // 
            this.TrajectoryChBox.AutoSize = true;
            this.TrajectoryChBox.Location = new System.Drawing.Point(7, 50);
            this.TrajectoryChBox.Name = "TrajectoryChBox";
            this.TrajectoryChBox.Size = new System.Drawing.Size(86, 17);
            this.TrajectoryChBox.TabIndex = 1;
            this.TrajectoryChBox.Text = "Траектория";
            this.TrajectoryChBox.UseVisualStyleBackColor = true;
            this.TrajectoryChBox.CheckedChanged += new System.EventHandler(this.TrajectoryChBox_CheckedChanged);
            // 
            // SectionsChBox
            // 
            this.SectionsChBox.AutoSize = true;
            this.SectionsChBox.Location = new System.Drawing.Point(99, 50);
            this.SectionsChBox.Name = "SectionsChBox";
            this.SectionsChBox.Size = new System.Drawing.Size(68, 17);
            this.SectionsChBox.TabIndex = 2;
            this.SectionsChBox.Text = "Сечения";
            this.SectionsChBox.UseVisualStyleBackColor = true;
            this.SectionsChBox.CheckedChanged += new System.EventHandler(this.SectionsChBox_CheckedChanged);
            // 
            // NormalsChBox
            // 
            this.NormalsChBox.AutoSize = true;
            this.NormalsChBox.Location = new System.Drawing.Point(7, 73);
            this.NormalsChBox.Name = "NormalsChBox";
            this.NormalsChBox.Size = new System.Drawing.Size(72, 17);
            this.NormalsChBox.TabIndex = 3;
            this.NormalsChBox.Text = "Нормали";
            this.NormalsChBox.UseVisualStyleBackColor = true;
            this.NormalsChBox.CheckedChanged += new System.EventHandler(this.NormalsChBox_CheckedChanged);
            // 
            // TextureChBox
            // 
            this.TextureChBox.AutoSize = true;
            this.TextureChBox.Location = new System.Drawing.Point(99, 96);
            this.TextureChBox.Name = "TextureChBox";
            this.TextureChBox.Size = new System.Drawing.Size(73, 17);
            this.TextureChBox.TabIndex = 4;
            this.TextureChBox.Text = "Текстура";
            this.TextureChBox.UseVisualStyleBackColor = true;
            // 
            // SmoothChBox
            // 
            this.SmoothChBox.AutoSize = true;
            this.SmoothChBox.Location = new System.Drawing.Point(99, 73);
            this.SmoothChBox.Name = "SmoothChBox";
            this.SmoothChBox.Size = new System.Drawing.Size(94, 17);
            this.SmoothChBox.TabIndex = 5;
            this.SmoothChBox.Text = "Сглаживание";
            this.SmoothChBox.UseVisualStyleBackColor = true;
            this.SmoothChBox.CheckedChanged += new System.EventHandler(this.SmoothChBox_CheckedChanged);
            // 
            // WireframeChBox
            // 
            this.WireframeChBox.AutoSize = true;
            this.WireframeChBox.Location = new System.Drawing.Point(7, 96);
            this.WireframeChBox.Name = "WireframeChBox";
            this.WireframeChBox.Size = new System.Drawing.Size(63, 17);
            this.WireframeChBox.TabIndex = 6;
            this.WireframeChBox.Text = "Каркас";
            this.WireframeChBox.UseVisualStyleBackColor = true;
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
            this.ModelControls.ResumeLayout(false);
            this.ModelControls.PerformLayout();
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
    }
}

