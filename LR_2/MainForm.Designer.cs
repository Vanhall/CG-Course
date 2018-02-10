namespace LR_2
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
            this.MainPanel = new System.Windows.Forms.Panel();
            this.GLControl = new SharpGL.OpenGLControl();
            this.openGLControl1 = new SharpGL.OpenGLControl();
            this.ButtonEditOrigin = new System.Windows.Forms.Button();
            this.ButtonTranslate = new System.Windows.Forms.Button();
            this.ButtonRotate = new System.Windows.Forms.Button();
            this.MainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GLControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.BackColor = System.Drawing.SystemColors.Control;
            this.MainPanel.Controls.Add(this.ButtonRotate);
            this.MainPanel.Controls.Add(this.ButtonTranslate);
            this.MainPanel.Controls.Add(this.ButtonEditOrigin);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.MainPanel.Location = new System.Drawing.Point(740, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(268, 729);
            this.MainPanel.TabIndex = 0;
            // 
            // GLControl
            // 
            this.GLControl.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.GLControl.Cursor = System.Windows.Forms.Cursors.Cross;
            this.GLControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GLControl.DrawFPS = false;
            this.GLControl.FrameRate = 30;
            this.GLControl.Location = new System.Drawing.Point(0, 0);
            this.GLControl.Name = "GLControl";
            this.GLControl.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.GLControl.RenderContextType = SharpGL.RenderContextType.FBO;
            this.GLControl.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            this.GLControl.Size = new System.Drawing.Size(740, 729);
            this.GLControl.TabIndex = 1;
            this.GLControl.OpenGLInitialized += new System.EventHandler(this.GLControl_OpenGLInitialized);
            this.GLControl.OpenGLDraw += new SharpGL.RenderEventHandler(this.GLControl_OpenGLDraw);
            this.GLControl.Resized += new System.EventHandler(this.GLControl_Resized);
            this.GLControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GLControl_MouseDown);
            this.GLControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GLControl_MouseMove);
            // 
            // openGLControl1
            // 
            this.openGLControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.openGLControl1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.openGLControl1.DrawFPS = false;
            this.openGLControl1.Location = new System.Drawing.Point(0, 0);
            this.openGLControl1.Name = "openGLControl1";
            this.openGLControl1.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.openGLControl1.RenderContextType = SharpGL.RenderContextType.DIBSection;
            this.openGLControl1.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            this.openGLControl1.Size = new System.Drawing.Size(741, 729);
            this.openGLControl1.TabIndex = 1;
            // 
            // ButtonEditOrigin
            // 
            this.ButtonEditOrigin.Location = new System.Drawing.Point(89, 295);
            this.ButtonEditOrigin.Name = "ButtonEditOrigin";
            this.ButtonEditOrigin.Size = new System.Drawing.Size(75, 23);
            this.ButtonEditOrigin.TabIndex = 0;
            this.ButtonEditOrigin.Text = "Origin";
            this.ButtonEditOrigin.UseVisualStyleBackColor = true;
            this.ButtonEditOrigin.Click += new System.EventHandler(this.ButtonEditOrigin_Click);
            // 
            // ButtonTranslate
            // 
            this.ButtonTranslate.Location = new System.Drawing.Point(89, 324);
            this.ButtonTranslate.Name = "ButtonTranslate";
            this.ButtonTranslate.Size = new System.Drawing.Size(75, 23);
            this.ButtonTranslate.TabIndex = 1;
            this.ButtonTranslate.Text = "Translation";
            this.ButtonTranslate.UseVisualStyleBackColor = true;
            this.ButtonTranslate.Click += new System.EventHandler(this.ButtonTranslate_Click);
            // 
            // ButtonRotate
            // 
            this.ButtonRotate.Location = new System.Drawing.Point(89, 353);
            this.ButtonRotate.Name = "ButtonRotate";
            this.ButtonRotate.Size = new System.Drawing.Size(75, 23);
            this.ButtonRotate.TabIndex = 2;
            this.ButtonRotate.Text = "Rotate";
            this.ButtonRotate.UseVisualStyleBackColor = true;
            this.ButtonRotate.Click += new System.EventHandler(this.ButtonRotate_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.GLControl);
            this.Controls.Add(this.MainPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1024, 768);
            this.Name = "MainForm";
            this.Text = "Компьютерная графика - Лабораторная работа №2";
            this.MainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GLControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel MainPanel;
        private SharpGL.OpenGLControl GLControl;
        private SharpGL.OpenGLControl openGLControl1;
        private System.Windows.Forms.Button ButtonTranslate;
        private System.Windows.Forms.Button ButtonEditOrigin;
        private System.Windows.Forms.Button ButtonRotate;
    }
}

