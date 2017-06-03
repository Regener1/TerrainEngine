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
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.stLblCamPosX = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.stLblCamPosY = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.stLblCamPosZ = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageScene = new System.Windows.Forms.TabPage();
            this.tabPageBrush = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.trackBarBrushSize = new System.Windows.Forms.TrackBar();
            this.tabPageLight = new System.Windows.Forms.TabPage();
            this.tbPosLightZ = new System.Windows.Forms.TextBox();
            this.tbPosLightY = new System.Windows.Forms.TextBox();
            this.tbPosLightX = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tbBlueLightColor = new System.Windows.Forms.TextBox();
            this.tbGreenLightColor = new System.Windows.Forms.TextBox();
            this.tbRedLightColor = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panelSelectedLightColor = new System.Windows.Forms.Panel();
            this.tabPageSceneInfo = new System.Windows.Forms.TabPage();
            this.tbCameraInfo = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.glControl)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageBrush.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBrushSize)).BeginInit();
            this.tabPageLight.SuspendLayout();
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
            this.glControl.Size = new System.Drawing.Size(1030, 660);
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
            this.menuStrip1.Size = new System.Drawing.Size(1357, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.stLblCamPosX,
            this.toolStripStatusLabel3,
            this.stLblCamPosY,
            this.toolStripStatusLabel5,
            this.stLblCamPosZ});
            this.statusStrip1.Location = new System.Drawing.Point(0, 690);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1357, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(48, 17);
            this.toolStripStatusLabel1.Text = "Camera";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(14, 17);
            this.toolStripStatusLabel2.Text = "X";
            // 
            // stLblCamPosX
            // 
            this.stLblCamPosX.Name = "stLblCamPosX";
            this.stLblCamPosX.Size = new System.Drawing.Size(33, 17);
            this.stLblCamPosX.Text = "posX";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(14, 17);
            this.toolStripStatusLabel3.Text = "Y";
            // 
            // stLblCamPosY
            // 
            this.stLblCamPosY.Name = "stLblCamPosY";
            this.stLblCamPosY.Size = new System.Drawing.Size(33, 17);
            this.stLblCamPosY.Text = "posY";
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(14, 17);
            this.toolStripStatusLabel5.Text = "Z";
            // 
            // stLblCamPosZ
            // 
            this.stLblCamPosZ.Name = "stLblCamPosZ";
            this.stLblCamPosZ.Size = new System.Drawing.Size(33, 17);
            this.stLblCamPosZ.Text = "posZ";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageScene);
            this.tabControl1.Controls.Add(this.tabPageBrush);
            this.tabControl1.Controls.Add(this.tabPageLight);
            this.tabControl1.Controls.Add(this.tabPageSceneInfo);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.tabControl1.Location = new System.Drawing.Point(1048, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(309, 666);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPageScene
            // 
            this.tabPageScene.AutoScroll = true;
            this.tabPageScene.Location = new System.Drawing.Point(4, 22);
            this.tabPageScene.Name = "tabPageScene";
            this.tabPageScene.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageScene.Size = new System.Drawing.Size(301, 640);
            this.tabPageScene.TabIndex = 0;
            this.tabPageScene.Text = "Scene";
            this.tabPageScene.UseVisualStyleBackColor = true;
            // 
            // tabPageBrush
            // 
            this.tabPageBrush.Controls.Add(this.label1);
            this.tabPageBrush.Controls.Add(this.trackBarBrushSize);
            this.tabPageBrush.Location = new System.Drawing.Point(4, 22);
            this.tabPageBrush.Name = "tabPageBrush";
            this.tabPageBrush.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBrush.Size = new System.Drawing.Size(301, 640);
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
            // tabPageLight
            // 
            this.tabPageLight.AutoScroll = true;
            this.tabPageLight.Controls.Add(this.tbPosLightZ);
            this.tabPageLight.Controls.Add(this.tbPosLightY);
            this.tabPageLight.Controls.Add(this.tbPosLightX);
            this.tabPageLight.Controls.Add(this.label5);
            this.tabPageLight.Controls.Add(this.label6);
            this.tabPageLight.Controls.Add(this.label7);
            this.tabPageLight.Controls.Add(this.tbBlueLightColor);
            this.tabPageLight.Controls.Add(this.tbGreenLightColor);
            this.tabPageLight.Controls.Add(this.tbRedLightColor);
            this.tabPageLight.Controls.Add(this.label4);
            this.tabPageLight.Controls.Add(this.label3);
            this.tabPageLight.Controls.Add(this.label2);
            this.tabPageLight.Controls.Add(this.panelSelectedLightColor);
            this.tabPageLight.Location = new System.Drawing.Point(4, 22);
            this.tabPageLight.Name = "tabPageLight";
            this.tabPageLight.Size = new System.Drawing.Size(301, 640);
            this.tabPageLight.TabIndex = 3;
            this.tabPageLight.Text = "Light";
            this.tabPageLight.UseVisualStyleBackColor = true;
            // 
            // tbPosLightZ
            // 
            this.tbPosLightZ.Location = new System.Drawing.Point(220, 311);
            this.tbPosLightZ.Name = "tbPosLightZ";
            this.tbPosLightZ.Size = new System.Drawing.Size(55, 20);
            this.tbPosLightZ.TabIndex = 12;
            // 
            // tbPosLightY
            // 
            this.tbPosLightY.Location = new System.Drawing.Point(220, 285);
            this.tbPosLightY.Name = "tbPosLightY";
            this.tbPosLightY.Size = new System.Drawing.Size(55, 20);
            this.tbPosLightY.TabIndex = 11;
            // 
            // tbPosLightX
            // 
            this.tbPosLightX.Location = new System.Drawing.Point(220, 259);
            this.tbPosLightX.Name = "tbPosLightX";
            this.tbPosLightX.Size = new System.Drawing.Size(55, 20);
            this.tbPosLightX.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(200, 314);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Z";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(200, 288);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Y";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(200, 262);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "X";
            // 
            // tbBlueLightColor
            // 
            this.tbBlueLightColor.Location = new System.Drawing.Point(238, 108);
            this.tbBlueLightColor.Name = "tbBlueLightColor";
            this.tbBlueLightColor.Size = new System.Drawing.Size(55, 20);
            this.tbBlueLightColor.TabIndex = 6;
            // 
            // tbGreenLightColor
            // 
            this.tbGreenLightColor.Location = new System.Drawing.Point(238, 82);
            this.tbGreenLightColor.Name = "tbGreenLightColor";
            this.tbGreenLightColor.Size = new System.Drawing.Size(55, 20);
            this.tbGreenLightColor.TabIndex = 5;
            // 
            // tbRedLightColor
            // 
            this.tbRedLightColor.Location = new System.Drawing.Point(238, 56);
            this.tbRedLightColor.Name = "tbRedLightColor";
            this.tbRedLightColor.Size = new System.Drawing.Size(55, 20);
            this.tbRedLightColor.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(218, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "B";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(218, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "G";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(217, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "R";
            // 
            // panelSelectedLightColor
            // 
            this.panelSelectedLightColor.Location = new System.Drawing.Point(138, 56);
            this.panelSelectedLightColor.Name = "panelSelectedLightColor";
            this.panelSelectedLightColor.Size = new System.Drawing.Size(60, 60);
            this.panelSelectedLightColor.TabIndex = 0;
            // 
            // tabPageSceneInfo
            // 
            this.tabPageSceneInfo.Controls.Add(this.tbCameraInfo);
            this.tabPageSceneInfo.Location = new System.Drawing.Point(4, 22);
            this.tabPageSceneInfo.Name = "tabPageSceneInfo";
            this.tabPageSceneInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSceneInfo.Size = new System.Drawing.Size(301, 640);
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
            this.ClientSize = new System.Drawing.Size(1357, 712);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.glControl);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FTerrainEngine";
            this.Text = "Terrain Engine";
            ((System.ComponentModel.ISupportInitialize)(this.glControl)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPageBrush.ResumeLayout(false);
            this.tabPageBrush.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBrushSize)).EndInit();
            this.tabPageLight.ResumeLayout(false);
            this.tabPageLight.PerformLayout();
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
        private System.Windows.Forms.TabPage tabPageBrush;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar trackBarBrushSize;
        private System.Windows.Forms.TabPage tabPageLight;
        private System.Windows.Forms.TextBox tbPosLightZ;
        private System.Windows.Forms.TextBox tbPosLightY;
        private System.Windows.Forms.TextBox tbPosLightX;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbBlueLightColor;
        private System.Windows.Forms.TextBox tbGreenLightColor;
        private System.Windows.Forms.TextBox tbRedLightColor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panelSelectedLightColor;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel stLblCamPosX;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel stLblCamPosY;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripStatusLabel stLblCamPosZ;
    }
}

