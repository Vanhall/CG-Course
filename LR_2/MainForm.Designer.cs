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
            this.ColorMixContainer = new System.Windows.Forms.GroupBox();
            this.ColorMixNotOrRB = new System.Windows.Forms.RadioButton();
            this.ColorMixOrRB = new System.Windows.Forms.RadioButton();
            this.ColorMixNoneRB = new System.Windows.Forms.RadioButton();
            this.ObjectsContainer = new System.Windows.Forms.GroupBox();
            this.ButtonDeleteHexagon = new System.Windows.Forms.Button();
            this.ButtonNewHexagon = new System.Windows.Forms.Button();
            this.ObjectsList = new System.Windows.Forms.ListBox();
            this.ObjectControlsContainer = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ButtonTranslate = new System.Windows.Forms.Button();
            this.ButtonResetScale = new System.Windows.Forms.Button();
            this.ButtonRotate = new System.Windows.Forms.Button();
            this.ButtonResetRot = new System.Windows.Forms.Button();
            this.ButtonScale = new System.Windows.Forms.Button();
            this.ButtonResetTrans = new System.Windows.Forms.Button();
            this.ButtonColorPick = new System.Windows.Forms.Button();
            this.ButtonOpenImage = new System.Windows.Forms.Button();
            this.ColorSquare = new System.Windows.Forms.Panel();
            this.GLControl = new SharpGL.OpenGLControl();
            this.openGLControl1 = new SharpGL.OpenGLControl();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.RasterControls = new System.Windows.Forms.GroupBox();
            this.RasterizeChBox = new System.Windows.Forms.CheckBox();
            this.MainPanel.SuspendLayout();
            this.ColorMixContainer.SuspendLayout();
            this.ObjectsContainer.SuspendLayout();
            this.ObjectControlsContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GLControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl1)).BeginInit();
            this.RasterControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.BackColor = System.Drawing.SystemColors.Control;
            this.MainPanel.Controls.Add(this.RasterControls);
            this.MainPanel.Controls.Add(this.ColorMixContainer);
            this.MainPanel.Controls.Add(this.ObjectsContainer);
            this.MainPanel.Controls.Add(this.ObjectControlsContainer);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.MainPanel.Location = new System.Drawing.Point(844, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(164, 729);
            this.MainPanel.TabIndex = 0;
            // 
            // ColorMixContainer
            // 
            this.ColorMixContainer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ColorMixContainer.Controls.Add(this.ColorMixNotOrRB);
            this.ColorMixContainer.Controls.Add(this.ColorMixOrRB);
            this.ColorMixContainer.Controls.Add(this.ColorMixNoneRB);
            this.ColorMixContainer.Location = new System.Drawing.Point(6, 385);
            this.ColorMixContainer.Name = "ColorMixContainer";
            this.ColorMixContainer.Size = new System.Drawing.Size(152, 47);
            this.ColorMixContainer.TabIndex = 10;
            this.ColorMixContainer.TabStop = false;
            this.ColorMixContainer.Text = "Смешивание цветов";
            // 
            // ColorMixNotOrRB
            // 
            this.ColorMixNotOrRB.AutoSize = true;
            this.ColorMixNotOrRB.Location = new System.Drawing.Point(104, 19);
            this.ColorMixNotOrRB.Name = "ColorMixNotOrRB";
            this.ColorMixNotOrRB.Size = new System.Drawing.Size(44, 17);
            this.ColorMixNotOrRB.TabIndex = 0;
            this.ColorMixNotOrRB.Text = "!OR";
            this.ColorMixNotOrRB.UseVisualStyleBackColor = true;
            // 
            // ColorMixOrRB
            // 
            this.ColorMixOrRB.AutoSize = true;
            this.ColorMixOrRB.Location = new System.Drawing.Point(57, 19);
            this.ColorMixOrRB.Name = "ColorMixOrRB";
            this.ColorMixOrRB.Size = new System.Drawing.Size(41, 17);
            this.ColorMixOrRB.TabIndex = 0;
            this.ColorMixOrRB.Text = "OR";
            this.ColorMixOrRB.UseVisualStyleBackColor = true;
            // 
            // ColorMixNoneRB
            // 
            this.ColorMixNoneRB.AutoSize = true;
            this.ColorMixNoneRB.Checked = true;
            this.ColorMixNoneRB.Location = new System.Drawing.Point(6, 19);
            this.ColorMixNoneRB.Name = "ColorMixNoneRB";
            this.ColorMixNoneRB.Size = new System.Drawing.Size(44, 17);
            this.ColorMixNoneRB.TabIndex = 0;
            this.ColorMixNoneRB.TabStop = true;
            this.ColorMixNoneRB.Text = "Нет";
            this.ColorMixNoneRB.UseVisualStyleBackColor = true;
            // 
            // ObjectsContainer
            // 
            this.ObjectsContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ObjectsContainer.Controls.Add(this.ButtonDeleteHexagon);
            this.ObjectsContainer.Controls.Add(this.ButtonNewHexagon);
            this.ObjectsContainer.Controls.Add(this.ObjectsList);
            this.ObjectsContainer.Location = new System.Drawing.Point(6, 12);
            this.ObjectsContainer.Name = "ObjectsContainer";
            this.ObjectsContainer.Size = new System.Drawing.Size(152, 196);
            this.ObjectsContainer.TabIndex = 9;
            this.ObjectsContainer.TabStop = false;
            this.ObjectsContainer.Text = "Объекты";
            // 
            // ButtonDeleteHexagon
            // 
            this.ButtonDeleteHexagon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonDeleteHexagon.Location = new System.Drawing.Point(81, 167);
            this.ButtonDeleteHexagon.Name = "ButtonDeleteHexagon";
            this.ButtonDeleteHexagon.Size = new System.Drawing.Size(65, 23);
            this.ButtonDeleteHexagon.TabIndex = 2;
            this.ButtonDeleteHexagon.Text = "Удалить";
            this.ButtonDeleteHexagon.UseVisualStyleBackColor = true;
            this.ButtonDeleteHexagon.Click += new System.EventHandler(this.ButtonDeleteHexagon_Click);
            // 
            // ButtonNewHexagon
            // 
            this.ButtonNewHexagon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ButtonNewHexagon.Location = new System.Drawing.Point(6, 167);
            this.ButtonNewHexagon.Name = "ButtonNewHexagon";
            this.ButtonNewHexagon.Size = new System.Drawing.Size(65, 23);
            this.ButtonNewHexagon.TabIndex = 1;
            this.ButtonNewHexagon.Text = "Новый";
            this.ButtonNewHexagon.UseVisualStyleBackColor = true;
            this.ButtonNewHexagon.Click += new System.EventHandler(this.ButtonNewHexagon_Click);
            // 
            // ObjectsList
            // 
            this.ObjectsList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ObjectsList.FormattingEnabled = true;
            this.ObjectsList.Location = new System.Drawing.Point(6, 20);
            this.ObjectsList.Name = "ObjectsList";
            this.ObjectsList.Size = new System.Drawing.Size(140, 147);
            this.ObjectsList.TabIndex = 0;
            this.ObjectsList.SelectedIndexChanged += new System.EventHandler(this.ObjectsList_SelectedIndexChanged);
            // 
            // ObjectControlsContainer
            // 
            this.ObjectControlsContainer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ObjectControlsContainer.Controls.Add(this.pictureBox1);
            this.ObjectControlsContainer.Controls.Add(this.ButtonTranslate);
            this.ObjectControlsContainer.Controls.Add(this.ButtonResetScale);
            this.ObjectControlsContainer.Controls.Add(this.ButtonRotate);
            this.ObjectControlsContainer.Controls.Add(this.ButtonResetRot);
            this.ObjectControlsContainer.Controls.Add(this.ButtonScale);
            this.ObjectControlsContainer.Controls.Add(this.ButtonResetTrans);
            this.ObjectControlsContainer.Controls.Add(this.ButtonColorPick);
            this.ObjectControlsContainer.Controls.Add(this.ButtonOpenImage);
            this.ObjectControlsContainer.Controls.Add(this.ColorSquare);
            this.ObjectControlsContainer.Location = new System.Drawing.Point(6, 214);
            this.ObjectControlsContainer.Name = "ObjectControlsContainer";
            this.ObjectControlsContainer.Size = new System.Drawing.Size(152, 165);
            this.ObjectControlsContainer.TabIndex = 8;
            this.ObjectControlsContainer.TabStop = false;
            this.ObjectControlsContainer.Text = "Преобразования";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::LR_2.Properties.Resources.photosicon;
            this.pictureBox1.Location = new System.Drawing.Point(5, 133);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(23, 23);
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // ButtonTranslate
            // 
            this.ButtonTranslate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonTranslate.BackColor = System.Drawing.SystemColors.Control;
            this.ButtonTranslate.Location = new System.Drawing.Point(6, 19);
            this.ButtonTranslate.Name = "ButtonTranslate";
            this.ButtonTranslate.Size = new System.Drawing.Size(93, 23);
            this.ButtonTranslate.TabIndex = 1;
            this.ButtonTranslate.Text = "Translate";
            this.ButtonTranslate.UseVisualStyleBackColor = true;
            this.ButtonTranslate.Click += new System.EventHandler(this.ButtonTranslate_Click);
            // 
            // ButtonResetScale
            // 
            this.ButtonResetScale.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonResetScale.Location = new System.Drawing.Point(101, 77);
            this.ButtonResetScale.Name = "ButtonResetScale";
            this.ButtonResetScale.Size = new System.Drawing.Size(45, 23);
            this.ButtonResetScale.TabIndex = 7;
            this.ButtonResetScale.Text = "Reset";
            this.ButtonResetScale.UseVisualStyleBackColor = true;
            this.ButtonResetScale.Click += new System.EventHandler(this.ButtonResetScale_Click);
            // 
            // ButtonRotate
            // 
            this.ButtonRotate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonRotate.Location = new System.Drawing.Point(6, 48);
            this.ButtonRotate.Name = "ButtonRotate";
            this.ButtonRotate.Size = new System.Drawing.Size(93, 23);
            this.ButtonRotate.TabIndex = 2;
            this.ButtonRotate.Text = "Rotate";
            this.ButtonRotate.UseVisualStyleBackColor = true;
            this.ButtonRotate.Click += new System.EventHandler(this.ButtonRotate_Click);
            // 
            // ButtonResetRot
            // 
            this.ButtonResetRot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonResetRot.Location = new System.Drawing.Point(101, 48);
            this.ButtonResetRot.Name = "ButtonResetRot";
            this.ButtonResetRot.Size = new System.Drawing.Size(45, 23);
            this.ButtonResetRot.TabIndex = 7;
            this.ButtonResetRot.Text = "Reset";
            this.ButtonResetRot.UseVisualStyleBackColor = true;
            this.ButtonResetRot.Click += new System.EventHandler(this.ButtonResetRot_Click);
            // 
            // ButtonScale
            // 
            this.ButtonScale.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonScale.Location = new System.Drawing.Point(6, 77);
            this.ButtonScale.Name = "ButtonScale";
            this.ButtonScale.Size = new System.Drawing.Size(93, 23);
            this.ButtonScale.TabIndex = 3;
            this.ButtonScale.Text = "Scale";
            this.ButtonScale.UseVisualStyleBackColor = true;
            this.ButtonScale.Click += new System.EventHandler(this.ButtonScale_Click);
            // 
            // ButtonResetTrans
            // 
            this.ButtonResetTrans.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonResetTrans.Location = new System.Drawing.Point(101, 19);
            this.ButtonResetTrans.Name = "ButtonResetTrans";
            this.ButtonResetTrans.Size = new System.Drawing.Size(45, 23);
            this.ButtonResetTrans.TabIndex = 7;
            this.ButtonResetTrans.Text = "Reset";
            this.ButtonResetTrans.UseVisualStyleBackColor = true;
            this.ButtonResetTrans.Click += new System.EventHandler(this.ButtonResetTrans_Click);
            // 
            // ButtonColorPick
            // 
            this.ButtonColorPick.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonColorPick.Location = new System.Drawing.Point(27, 105);
            this.ButtonColorPick.Name = "ButtonColorPick";
            this.ButtonColorPick.Size = new System.Drawing.Size(119, 23);
            this.ButtonColorPick.TabIndex = 4;
            this.ButtonColorPick.Text = "Цвет";
            this.ButtonColorPick.UseVisualStyleBackColor = true;
            this.ButtonColorPick.Click += new System.EventHandler(this.ButtonColorPick_Click);
            // 
            // ButtonOpenImage
            // 
            this.ButtonOpenImage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonOpenImage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ButtonOpenImage.Location = new System.Drawing.Point(27, 133);
            this.ButtonOpenImage.Name = "ButtonOpenImage";
            this.ButtonOpenImage.Size = new System.Drawing.Size(119, 23);
            this.ButtonOpenImage.TabIndex = 6;
            this.ButtonOpenImage.Text = "Текстура";
            this.ButtonOpenImage.UseVisualStyleBackColor = true;
            this.ButtonOpenImage.Click += new System.EventHandler(this.ButtonOpenImage_Click);
            // 
            // ColorSquare
            // 
            this.ColorSquare.BackColor = System.Drawing.Color.White;
            this.ColorSquare.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ColorSquare.Location = new System.Drawing.Point(6, 106);
            this.ColorSquare.Name = "ColorSquare";
            this.ColorSquare.Size = new System.Drawing.Size(21, 21);
            this.ColorSquare.TabIndex = 5;
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
            this.GLControl.Size = new System.Drawing.Size(844, 729);
            this.GLControl.TabIndex = 1;
            this.GLControl.OpenGLInitialized += new System.EventHandler(this.GLControl_OpenGLInitialized);
            this.GLControl.OpenGLDraw += new SharpGL.RenderEventHandler(this.GLControl_OpenGLDraw);
            this.GLControl.Resized += new System.EventHandler(this.GLControl_Resized);
            this.GLControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GLControl_MouseDown);
            this.GLControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GLControl_MouseMove);
            this.GLControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GLControl_MouseUp);
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
            // colorDialog
            // 
            this.colorDialog.Color = System.Drawing.Color.White;
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "PNG Images|*.png";
            this.openFileDialog.InitialDirectory = ".";
            // 
            // RasterControls
            // 
            this.RasterControls.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RasterControls.Controls.Add(this.RasterizeChBox);
            this.RasterControls.Location = new System.Drawing.Point(6, 438);
            this.RasterControls.Name = "RasterControls";
            this.RasterControls.Size = new System.Drawing.Size(152, 114);
            this.RasterControls.TabIndex = 11;
            this.RasterControls.TabStop = false;
            this.RasterControls.Text = "Растеризация";
            // 
            // RasterizeChBox
            // 
            this.RasterizeChBox.AutoSize = true;
            this.RasterizeChBox.Location = new System.Drawing.Point(7, 20);
            this.RasterizeChBox.Name = "RasterizeChBox";
            this.RasterizeChBox.Size = new System.Drawing.Size(75, 17);
            this.RasterizeChBox.TabIndex = 0;
            this.RasterizeChBox.Text = "Включить";
            this.RasterizeChBox.UseVisualStyleBackColor = true;
            this.RasterizeChBox.CheckedChanged += new System.EventHandler(this.RasterizeChBox_CheckedChanged);
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
            this.ColorMixContainer.ResumeLayout(false);
            this.ColorMixContainer.PerformLayout();
            this.ObjectsContainer.ResumeLayout(false);
            this.ObjectControlsContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GLControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.openGLControl1)).EndInit();
            this.RasterControls.ResumeLayout(false);
            this.RasterControls.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel MainPanel;
        private SharpGL.OpenGLControl GLControl;
        private SharpGL.OpenGLControl openGLControl1;
        private System.Windows.Forms.Button ButtonTranslate;
        private System.Windows.Forms.Button ButtonRotate;
        private System.Windows.Forms.Button ButtonScale;
        private System.Windows.Forms.Panel ColorSquare;
        private System.Windows.Forms.Button ButtonColorPick;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.Button ButtonOpenImage;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button ButtonResetScale;
        private System.Windows.Forms.Button ButtonResetRot;
        private System.Windows.Forms.Button ButtonResetTrans;
        private System.Windows.Forms.GroupBox ObjectsContainer;
        private System.Windows.Forms.Button ButtonDeleteHexagon;
        private System.Windows.Forms.Button ButtonNewHexagon;
        private System.Windows.Forms.ListBox ObjectsList;
        private System.Windows.Forms.GroupBox ObjectControlsContainer;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox ColorMixContainer;
        private System.Windows.Forms.RadioButton ColorMixNotOrRB;
        private System.Windows.Forms.RadioButton ColorMixOrRB;
        private System.Windows.Forms.RadioButton ColorMixNoneRB;
        private System.Windows.Forms.GroupBox RasterControls;
        private System.Windows.Forms.CheckBox RasterizeChBox;
    }
}

