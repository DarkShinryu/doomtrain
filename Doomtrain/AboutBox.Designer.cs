namespace Doomtrain
{
    partial class AboutBox
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
            this.buttonClose = new System.Windows.Forms.Button();
            this.labelDoomtrain = new System.Windows.Forms.Label();
            this.labelCredits = new System.Windows.Forms.Label();
            this.labelThanks = new System.Windows.Forms.Label();
            this.linkLabelGitHub = new System.Windows.Forms.LinkLabel();
            this.linkLabelForum = new System.Windows.Forms.LinkLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonClose.Location = new System.Drawing.Point(93, 145);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(74, 23);
            this.buttonClose.TabIndex = 0;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // labelDoomtrain
            // 
            this.labelDoomtrain.AutoSize = true;
            this.labelDoomtrain.Font = new System.Drawing.Font("SF Slapstick Comic Shaded", 23F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDoomtrain.ForeColor = System.Drawing.Color.Brown;
            this.labelDoomtrain.Location = new System.Drawing.Point(2, 0);
            this.labelDoomtrain.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelDoomtrain.Name = "labelDoomtrain";
            this.labelDoomtrain.Size = new System.Drawing.Size(256, 46);
            this.labelDoomtrain.TabIndex = 1;
            this.labelDoomtrain.Text = "Doomtrain 0.1";
            // 
            // labelCredits
            // 
            this.labelCredits.AutoSize = true;
            this.labelCredits.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCredits.Location = new System.Drawing.Point(53, 42);
            this.labelCredits.Name = "labelCredits";
            this.labelCredits.Size = new System.Drawing.Size(154, 19);
            this.labelCredits.TabIndex = 2;
            this.labelCredits.Text = "by alexfilth && MaKiPL";
            // 
            // labelThanks
            // 
            this.labelThanks.AutoSize = true;
            this.labelThanks.Location = new System.Drawing.Point(9, 124);
            this.labelThanks.Name = "labelThanks";
            this.labelThanks.Size = new System.Drawing.Size(243, 13);
            this.labelThanks.TabIndex = 5;
            this.labelThanks.Text = "Thanks to JWP for documenting the kernel.bin file";
            // 
            // linkLabelGitHub
            // 
            this.linkLabelGitHub.AutoSize = true;
            this.linkLabelGitHub.Location = new System.Drawing.Point(84, 90);
            this.linkLabelGitHub.Name = "linkLabelGitHub";
            this.linkLabelGitHub.Size = new System.Drawing.Size(93, 13);
            this.linkLabelGitHub.TabIndex = 6;
            this.linkLabelGitHub.TabStop = true;
            this.linkLabelGitHub.Text = "GitHub Repository";
            this.linkLabelGitHub.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelGitHub_LinkClicked);
            // 
            // linkLabelForum
            // 
            this.linkLabelForum.AutoSize = true;
            this.linkLabelForum.Location = new System.Drawing.Point(76, 75);
            this.linkLabelForum.Name = "linkLabelForum";
            this.linkLabelForum.Size = new System.Drawing.Size(108, 13);
            this.linkLabelForum.TabIndex = 7;
            this.linkLabelForum.TabStop = true;
            this.linkLabelForum.Text = "Official Forum Thread";
            this.linkLabelForum.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelForum_LinkClicked);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.labelDoomtrain);
            this.panel1.Controls.Add(this.linkLabelGitHub);
            this.panel1.Controls.Add(this.buttonClose);
            this.panel1.Controls.Add(this.linkLabelForum);
            this.panel1.Controls.Add(this.labelCredits);
            this.panel1.Controls.Add(this.labelThanks);
            this.panel1.Location = new System.Drawing.Point(286, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(261, 176);
            this.panel1.TabIndex = 8;
            // 
            // AboutBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.BackgroundImage = global::Doomtrain.Properties.Resources.doomtrainbig;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.CancelButton = this.buttonClose;
            this.ClientSize = new System.Drawing.Size(546, 175);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "AboutBox";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Label labelDoomtrain;
        private System.Windows.Forms.Label labelCredits;
        private System.Windows.Forms.Label labelThanks;
        private System.Windows.Forms.LinkLabel linkLabelGitHub;
        private System.Windows.Forms.LinkLabel linkLabelForum;
        private System.Windows.Forms.Panel panel1;
    }
}