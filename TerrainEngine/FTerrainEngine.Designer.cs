namespace TerrainEngine
{
    partial class FTerrainEngine
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.glControl = new SharpGL.OpenGLControl();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageScene = new System.Windows.Forms.TabPage();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.tabPageBrush = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.trackBarBrushSize = new System.Windows.Forms.TrackBar();
            this.tabPageSceneInfo = new System.Windows.Forms.TabPage();
            this.tbCameraInfo = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.glControl)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPageScene.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.tabPageBrush.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBrushSize)).BeginInit();
            this.tabPageSceneInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // glControl
            // 
            this.glControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.glControl.DrawFPS = false;
            this.glControl.Location = new System.Drawing.Point(12, 27);
            this.glControl.Name = "glControl";
            this.glControl.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL4_2;
            this.glControl.RenderContextType = SharpGL.RenderContextType.NativeWindow;
            this.glControl.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            this.glControl.Size = new System.Drawing.Size(857, 609);
            this.glControl.TabIndex = 0;
            this.glControl.OpenGLInitialized += new System.EventHandler(this.glControl_OpenGLInitialized);
            this.glControl.OpenGLDraw += new SharpGL.RenderEventHandler(this.glControl_OpenGLDraw);
            this.glControl.Resized += new System.EventHandler(this.glControl_Resized);
            this.glControl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.glControl_KeyDown);
            this.glControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.glControl_MouseDown);
            this.glControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.glControl_MouseMove);
            this.glControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.glControl_MouseUp);
            this.glControl.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.glControl_MouseWheel);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1184, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 639);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1184, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageScene);
            this.tabControl1.Controls.Add(this.tabPageBrush);
            this.tabControl1.Controls.Add(this.tabPageSceneInfo);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.tabControl1.Location = new System.Drawing.Point(875, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(309, 615);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPageScene
            // 
            this.tabPageScene.AutoScroll = true;
            this.tabPageScene.Controls.Add(this.numericUpDown2);
            this.tabPageScene.Controls.Add(this.numericUpDown1);
            this.tabPageScene.Location = new System.Drawing.Point(4, 22);
            this.tabPageScene.Name = "tabPageScene";
            this.tabPageScene.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageScene.Size = new System.Drawing.Size(301, 589);
            this.tabPageScene.TabIndex = 0;
            this.tabPageScene.Text = "Scene";
            this.tabPageScene.UseVisualStyleBackColor = true;
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(88, 144);
            this.numericUpDown2.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown2.TabIndex = 1;
            this.numericUpDown2.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(88, 118);
            this.numericUpDown1.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown1.TabIndex = 0;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // tabPageBrush
            // 
            this.tabPageBrush.Controls.Add(this.label1);
            this.tabPageBrush.Controls.Add(this.trackBarBrushSize);
            this.tabPageBrush.Location = new System.Drawing.Point(4, 22);
            this.tabPageBrush.Name = "tabPageBrush";
            this.tabPageBrush.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBrush.Size = new System.Drawing.Size(301, 589);
            this.tabPageBrush.TabIndex = 2;
            this.tabPageBrush.Text = "Brush";
            this.tabPageBrush.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(73, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "size";
            // 
            // trackBarBrushSize
            // 
            this.trackBarBrushSize.BackColor = System.Drawing.Color.White;
            this.trackBarBrushSize.Location = new System.Drawing.Point(104, 94);
            this.trackBarBrushSize.Maximum = 100;
            this.trackBarBrushSize.Minimum = 1;
            this.trackBarBrushSize.Name = "trackBarBrushSize";
            this.trackBarBrushSize.Size = new System.Drawing.Size(189, 45);
            this.trackBarBrushSize.TabIndex = 0;
            this.trackBarBrushSize.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBarBrushSize.Value = 1;
            this.trackBarBrushSize.Scroll += new System.EventHandler(this.trackBarBrushSize_Scroll);
            // 
            // tabPageSceneInfo
            // 
            this.tabPageSceneInfo.Controls.Add(this.tbCameraInfo);
            this.tabPageSceneInfo.Location = new System.Drawing.Point(4, 22);
            this.tabPageSceneInfo.Name = "tabPageSceneInfo";
            this.tabPageSceneInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSceneInfo.Size = new System.Drawing.Size(301, 589);
            this.tabPageSceneInfo.TabIndex = 1;
            this.tabPageSceneInfo.Text = "Scene Info";
            this.tabPageSceneInfo.UseVisualStyleBackColor = true;
            // 
            // tbCameraInfo
            // 
            this.tbCameraInfo.Location = new System.Drawing.Point(72, 74);
            this.tbCameraInfo.Multiline = true;
            this.tbCameraInfo.Name = "tbCameraInfo";
            this.tbCameraInfo.Size = new System.Drawing.Size(173, 147);
            this.tbCameraInfo.TabIndex = 0;
            // 
            // FTerrainEngine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.glControl);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FTerrainEngine";
            this.Text = "Terrain Engine";
            ((System.ComponentModel.ISupportInitialize)(this.glControl)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPageScene.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.tabPageBrush.ResumeLayout(false);
            this.tabPageBrush.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBrushSize)).EndInit();
            this.tabPageSceneInfo.ResumeLayout(false);
            this.tabPageSceneInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SharpGL.OpenGLControl glControl;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageScene;
        private System.Windows.Forms.TabPage tabPageSceneInfo;
        private System.Windows.Forms.TextBox tbCameraInfo;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.TabPage tabPageBrush;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar trackBarBrushSize;
    }
}

