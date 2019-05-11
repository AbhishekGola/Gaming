namespace GameSupport
{
    partial class GameSupport
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
            this.chbGlow = new System.Windows.Forms.CheckBox();
            this.chbRadar = new System.Windows.Forms.CheckBox();
            this.cbGameCSGO = new System.Windows.Forms.CheckBox();
            this.pnlCSGO = new System.Windows.Forms.Panel();
            this.lblBunnyHop = new System.Windows.Forms.Label();
            this.chbBunnyHop = new System.Windows.Forms.CheckBox();
            this.lblGodMode = new System.Windows.Forms.Label();
            this.lblAimAssist = new System.Windows.Forms.Label();
            this.lblAimBot = new System.Windows.Forms.Label();
            this.lblAntiFlash = new System.Windows.Forms.Label();
            this.lblTriggerBot = new System.Windows.Forms.Label();
            this.lblRadarStatus = new System.Windows.Forms.Label();
            this.lblGLowStatus = new System.Windows.Forms.Label();
            this.chbGodMode = new System.Windows.Forms.CheckBox();
            this.chbAimAssist = new System.Windows.Forms.CheckBox();
            this.chbAimBot = new System.Windows.Forms.CheckBox();
            this.chbAntiFlash = new System.Windows.Forms.CheckBox();
            this.chbTriggerBot = new System.Windows.Forms.CheckBox();
            this.lblLastActionStatus = new System.Windows.Forms.Label();
            this.pnlCSGO.SuspendLayout();
            this.SuspendLayout();
            // 
            // chbGlow
            // 
            this.chbGlow.AutoSize = true;
            this.chbGlow.Location = new System.Drawing.Point(12, 19);
            this.chbGlow.Name = "chbGlow";
            this.chbGlow.Size = new System.Drawing.Size(61, 21);
            this.chbGlow.TabIndex = 1;
            this.chbGlow.Text = "Glow";
            this.chbGlow.UseVisualStyleBackColor = true;
            this.chbGlow.CheckedChanged += new System.EventHandler(this.chbGlow_CheckedChanged);
            // 
            // chbRadar
            // 
            this.chbRadar.AutoSize = true;
            this.chbRadar.Location = new System.Drawing.Point(12, 46);
            this.chbRadar.Name = "chbRadar";
            this.chbRadar.Size = new System.Drawing.Size(69, 21);
            this.chbRadar.TabIndex = 2;
            this.chbRadar.Text = "Radar";
            this.chbRadar.UseVisualStyleBackColor = true;
            this.chbRadar.CheckedChanged += new System.EventHandler(this.chbRadar_CheckedChanged);
            // 
            // cbGameCSGO
            // 
            this.cbGameCSGO.AutoSize = true;
            this.cbGameCSGO.Location = new System.Drawing.Point(12, 46);
            this.cbGameCSGO.Name = "cbGameCSGO";
            this.cbGameCSGO.Size = new System.Drawing.Size(116, 21);
            this.cbGameCSGO.TabIndex = 3;
            this.cbGameCSGO.Text = "Game: CSGO";
            this.cbGameCSGO.UseVisualStyleBackColor = true;
            this.cbGameCSGO.CheckedChanged += new System.EventHandler(this.cbGameCSGO_CheckedChanged);
            // 
            // pnlCSGO
            // 
            this.pnlCSGO.Controls.Add(this.lblBunnyHop);
            this.pnlCSGO.Controls.Add(this.chbBunnyHop);
            this.pnlCSGO.Controls.Add(this.lblGodMode);
            this.pnlCSGO.Controls.Add(this.lblAimAssist);
            this.pnlCSGO.Controls.Add(this.lblAimBot);
            this.pnlCSGO.Controls.Add(this.lblAntiFlash);
            this.pnlCSGO.Controls.Add(this.lblTriggerBot);
            this.pnlCSGO.Controls.Add(this.lblRadarStatus);
            this.pnlCSGO.Controls.Add(this.lblGLowStatus);
            this.pnlCSGO.Controls.Add(this.chbGodMode);
            this.pnlCSGO.Controls.Add(this.chbAimAssist);
            this.pnlCSGO.Controls.Add(this.chbAimBot);
            this.pnlCSGO.Controls.Add(this.chbAntiFlash);
            this.pnlCSGO.Controls.Add(this.chbTriggerBot);
            this.pnlCSGO.Controls.Add(this.chbGlow);
            this.pnlCSGO.Controls.Add(this.chbRadar);
            this.pnlCSGO.Location = new System.Drawing.Point(12, 84);
            this.pnlCSGO.Name = "pnlCSGO";
            this.pnlCSGO.Size = new System.Drawing.Size(278, 237);
            this.pnlCSGO.TabIndex = 4;
            this.pnlCSGO.Tag = "CSGO";
            this.pnlCSGO.Visible = false;
            // 
            // lblBunnyHop
            // 
            this.lblBunnyHop.AutoSize = true;
            this.lblBunnyHop.Location = new System.Drawing.Point(159, 207);
            this.lblBunnyHop.Name = "lblBunnyHop";
            this.lblBunnyHop.Size = new System.Drawing.Size(63, 17);
            this.lblBunnyHop.TabIndex = 16;
            this.lblBunnyHop.Text = "Disabled";
            // 
            // chbBunnyHop
            // 
            this.chbBunnyHop.AutoSize = true;
            this.chbBunnyHop.Location = new System.Drawing.Point(12, 206);
            this.chbBunnyHop.Name = "chbBunnyHop";
            this.chbBunnyHop.Size = new System.Drawing.Size(96, 21);
            this.chbBunnyHop.TabIndex = 15;
            this.chbBunnyHop.Text = "BunnyHop";
            this.chbBunnyHop.UseVisualStyleBackColor = true;
            this.chbBunnyHop.CheckedChanged += new System.EventHandler(this.chbBunnyHop_CheckedChanged);
            // 
            // lblGodMode
            // 
            this.lblGodMode.AutoSize = true;
            this.lblGodMode.Location = new System.Drawing.Point(159, 180);
            this.lblGodMode.Name = "lblGodMode";
            this.lblGodMode.Size = new System.Drawing.Size(63, 17);
            this.lblGodMode.TabIndex = 14;
            this.lblGodMode.Text = "Disabled";
            // 
            // lblAimAssist
            // 
            this.lblAimAssist.AutoSize = true;
            this.lblAimAssist.Location = new System.Drawing.Point(159, 153);
            this.lblAimAssist.Name = "lblAimAssist";
            this.lblAimAssist.Size = new System.Drawing.Size(63, 17);
            this.lblAimAssist.TabIndex = 13;
            this.lblAimAssist.Text = "Disabled";
            // 
            // lblAimBot
            // 
            this.lblAimBot.AutoSize = true;
            this.lblAimBot.Location = new System.Drawing.Point(159, 127);
            this.lblAimBot.Name = "lblAimBot";
            this.lblAimBot.Size = new System.Drawing.Size(63, 17);
            this.lblAimBot.TabIndex = 12;
            this.lblAimBot.Text = "Disabled";
            // 
            // lblAntiFlash
            // 
            this.lblAntiFlash.AutoSize = true;
            this.lblAntiFlash.Location = new System.Drawing.Point(159, 104);
            this.lblAntiFlash.Name = "lblAntiFlash";
            this.lblAntiFlash.Size = new System.Drawing.Size(63, 17);
            this.lblAntiFlash.TabIndex = 11;
            this.lblAntiFlash.Text = "Disabled";
            // 
            // lblTriggerBot
            // 
            this.lblTriggerBot.AutoSize = true;
            this.lblTriggerBot.Location = new System.Drawing.Point(159, 77);
            this.lblTriggerBot.Name = "lblTriggerBot";
            this.lblTriggerBot.Size = new System.Drawing.Size(63, 17);
            this.lblTriggerBot.TabIndex = 10;
            this.lblTriggerBot.Text = "Disabled";
            // 
            // lblRadarStatus
            // 
            this.lblRadarStatus.AutoSize = true;
            this.lblRadarStatus.Location = new System.Drawing.Point(159, 47);
            this.lblRadarStatus.Name = "lblRadarStatus";
            this.lblRadarStatus.Size = new System.Drawing.Size(63, 17);
            this.lblRadarStatus.TabIndex = 9;
            this.lblRadarStatus.Text = "Disabled";
            // 
            // lblGLowStatus
            // 
            this.lblGLowStatus.AutoSize = true;
            this.lblGLowStatus.Location = new System.Drawing.Point(159, 22);
            this.lblGLowStatus.Name = "lblGLowStatus";
            this.lblGLowStatus.Size = new System.Drawing.Size(63, 17);
            this.lblGLowStatus.TabIndex = 8;
            this.lblGLowStatus.Text = "Disabled";
            // 
            // chbGodMode
            // 
            this.chbGodMode.AutoSize = true;
            this.chbGodMode.Location = new System.Drawing.Point(12, 179);
            this.chbGodMode.Name = "chbGodMode";
            this.chbGodMode.Size = new System.Drawing.Size(96, 21);
            this.chbGodMode.TabIndex = 7;
            this.chbGodMode.Text = "God Mode";
            this.chbGodMode.UseVisualStyleBackColor = true;
            this.chbGodMode.CheckedChanged += new System.EventHandler(this.chbGodMode_CheckedChanged);
            // 
            // chbAimAssist
            // 
            this.chbAimAssist.AutoSize = true;
            this.chbAimAssist.Location = new System.Drawing.Point(12, 152);
            this.chbAimAssist.Name = "chbAimAssist";
            this.chbAimAssist.Size = new System.Drawing.Size(94, 21);
            this.chbAimAssist.TabIndex = 6;
            this.chbAimAssist.Text = "Aim Assist";
            this.chbAimAssist.UseVisualStyleBackColor = true;
            this.chbAimAssist.CheckedChanged += new System.EventHandler(this.chbAimAssist_CheckedChanged);
            // 
            // chbAimBot
            // 
            this.chbAimBot.AutoSize = true;
            this.chbAimBot.Location = new System.Drawing.Point(12, 127);
            this.chbAimBot.Name = "chbAimBot";
            this.chbAimBot.Size = new System.Drawing.Size(78, 21);
            this.chbAimBot.TabIndex = 5;
            this.chbAimBot.Text = "Aim Bot";
            this.chbAimBot.UseVisualStyleBackColor = true;
            this.chbAimBot.CheckedChanged += new System.EventHandler(this.chbAimBot_CheckedChanged);
            // 
            // chbAntiFlash
            // 
            this.chbAntiFlash.AutoSize = true;
            this.chbAntiFlash.Location = new System.Drawing.Point(12, 100);
            this.chbAntiFlash.Name = "chbAntiFlash";
            this.chbAntiFlash.Size = new System.Drawing.Size(92, 21);
            this.chbAntiFlash.TabIndex = 4;
            this.chbAntiFlash.Text = "Anti Flash";
            this.chbAntiFlash.UseVisualStyleBackColor = true;
            this.chbAntiFlash.CheckedChanged += new System.EventHandler(this.chbAntiFlash_CheckedChanged);
            // 
            // chbTriggerBot
            // 
            this.chbTriggerBot.AutoSize = true;
            this.chbTriggerBot.Location = new System.Drawing.Point(12, 73);
            this.chbTriggerBot.Name = "chbTriggerBot";
            this.chbTriggerBot.Size = new System.Drawing.Size(101, 21);
            this.chbTriggerBot.TabIndex = 3;
            this.chbTriggerBot.Text = "Trigger Bot";
            this.chbTriggerBot.UseVisualStyleBackColor = true;
            this.chbTriggerBot.CheckedChanged += new System.EventHandler(this.chbTriggerBot_CheckedChanged);
            // 
            // lblLastActionStatus
            // 
            this.lblLastActionStatus.AutoSize = true;
            this.lblLastActionStatus.Location = new System.Drawing.Point(9, 324);
            this.lblLastActionStatus.Name = "lblLastActionStatus";
            this.lblLastActionStatus.Size = new System.Drawing.Size(115, 17);
            this.lblLastActionStatus.TabIndex = 15;
            this.lblLastActionStatus.Text = "last Action status";
            // 
            // GameSupport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(352, 360);
            this.Controls.Add(this.lblLastActionStatus);
            this.Controls.Add(this.pnlCSGO);
            this.Controls.Add(this.cbGameCSGO);
            this.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.HelpButton = true;
            this.Name = "GameSupport";
            this.Text = "GameSupport";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GameSupport_FormClosing);
            this.pnlCSGO.ResumeLayout(false);
            this.pnlCSGO.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chbGlow;
        private System.Windows.Forms.CheckBox chbRadar;
        private System.Windows.Forms.CheckBox cbGameCSGO;
        private System.Windows.Forms.Panel pnlCSGO;
        private System.Windows.Forms.CheckBox chbTriggerBot;
        private System.Windows.Forms.CheckBox chbAntiFlash;
        private System.Windows.Forms.CheckBox chbAimBot;
        private System.Windows.Forms.CheckBox chbAimAssist;
        private System.Windows.Forms.CheckBox chbGodMode;
        private System.Windows.Forms.Label lblGodMode;
        private System.Windows.Forms.Label lblAimAssist;
        private System.Windows.Forms.Label lblAimBot;
        private System.Windows.Forms.Label lblAntiFlash;
        private System.Windows.Forms.Label lblTriggerBot;
        private System.Windows.Forms.Label lblRadarStatus;
        private System.Windows.Forms.Label lblGLowStatus;
        private System.Windows.Forms.Label lblLastActionStatus;
        private System.Windows.Forms.Label lblBunnyHop;
        private System.Windows.Forms.CheckBox chbBunnyHop;
    }
}

