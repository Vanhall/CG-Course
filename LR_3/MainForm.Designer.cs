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
            ((System.ComponentModel.ISupportInitialize)(this.GLControl)).BeginInit();
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
            this.GLControl.Size = new System.Drawing.Size(808, 729);
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
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.MainPanel.Location = new System.Drawing.Point(808, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(200, 729);
            this.MainPanel.TabIndex = 1;
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
            this.ResumeLayout(false);

        }

        #endregion

        private SharpGL.OpenGLControl GLControl;
        private System.Windows.Forms.Panel MainPanel;
    }
}

