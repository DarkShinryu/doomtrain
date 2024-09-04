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
            this.labelDoomtrain = new System.Windows.Forms.Label();
            this.linkLabelWiki = new System.Windows.Forms.LinkLabel();
            this.linkLabelGitHub = new System.Windows.Forms.LinkLabel();
            this.buttonClose = new System.Windows.Forms.Button();
            this.linkLabelForum = new System.Windows.Forms.LinkLabel();
            this.labelCredits = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelDoomtrain
            // 
            this.labelDoomtrain.AutoSize = true;
            this.labelDoomtrain.Font = new System.Drawing.Font("calendar note tfb", 35F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDoomtrain.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(77)))), ((int)(((byte)(0)))));
            this.labelDoomtrain.Location = new System.Drawing.Point(46, 7);
            this.labelDoomtrain.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelDoomtrain.Name = "labelDoomtrain";
            this.labelDoomtrain.Size = new System.Drawing.Size(441, 49);
            this.labelDoomtrain.TabIndex = 1;
            // 
            // linkLabelWiki
            // 
            this.linkLabelWiki.AutoSize = true;
            this.linkLabelWiki.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelWiki.Location = new System.Drawing.Point(382, 141);
            this.linkLabelWiki.Name = "linkLabelWiki";
            this.linkLabelWiki.Size = new System.Drawing.Size(34, 17);
            this.linkLabelWiki.TabIndex = 13;
            this.linkLabelWiki.TabStop = true;
            this.linkLabelWiki.Text = "Wiki";
            this.linkLabelWiki.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelWiki_LinkClicked);
            // 
            // linkLabelGitHub
            // 
            this.linkLabelGitHub.AutoSize = true;
            this.linkLabelGitHub.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelGitHub.Location = new System.Drawing.Point(337, 122);
            this.linkLabelGitHub.Name = "linkLabelGitHub";
            this.linkLabelGitHub.Size = new System.Drawing.Size(124, 17);
            this.linkLabelGitHub.TabIndex = 11;
            this.linkLabelGitHub.TabStop = true;
            this.linkLabelGitHub.Text = "GitHub Repository";
            this.linkLabelGitHub.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelGitHub_LinkClicked);
            // 
            // buttonClose
            // 
            this.buttonClose.BackColor = System.Drawing.Color.Linen;
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(77)))), ((int)(((byte)(0)))));
            this.buttonClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.PeachPuff;
            this.buttonClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Bisque;
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonClose.Location = new System.Drawing.Point(352, 192);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(94, 25);
            this.buttonClose.TabIndex = 9;
            this.buttonClose.Text = "OK";
            this.buttonClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonClose.UseVisualStyleBackColor = false;
            // 
            // linkLabelForum
            // 
            this.linkLabelForum.AutoSize = true;
            this.linkLabelForum.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelForum.Location = new System.Drawing.Point(327, 103);
            this.linkLabelForum.Name = "linkLabelForum";
            this.linkLabelForum.Size = new System.Drawing.Size(145, 17);
            this.linkLabelForum.TabIndex = 12;
            this.linkLabelForum.TabStop = true;
            this.linkLabelForum.Text = "Official Forum Thread";
            this.linkLabelForum.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelForum_LinkClicked);
            // 
            // labelCredits
            // 
            this.labelCredits.AutoSize = true;
            this.labelCredits.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCredits.Location = new System.Drawing.Point(302, 75);
            this.labelCredits.Name = "labelCredits";
            this.labelCredits.Size = new System.Drawing.Size(198, 21);
            this.labelCredits.TabIndex = 10;
            this.labelCredits.Text = "by alexfilth, Maki && JWP";
            // 
            // AboutBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(532, 229);
            this.ControlBox = false;
            this.Controls.Add(this.linkLabelWiki);
            this.Controls.Add(this.linkLabelGitHub);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.linkLabelForum);
            this.Controls.Add(this.labelCredits);
            this.Controls.Add(this.labelDoomtrain);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "AboutBox";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.AboutBox_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelDoomtrain;
        private System.Windows.Forms.LinkLabel linkLabelWiki;
        private System.Windows.Forms.LinkLabel linkLabelGitHub;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.LinkLabel linkLabelForum;
        private System.Windows.Forms.Label labelCredits;
    }
}