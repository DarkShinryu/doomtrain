namespace Doomtrain.Charts
{
    partial class MagicDamage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MagicDamage));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.CustomLabel customLabel4 = new System.Windows.Forms.DataVisualization.Charting.CustomLabel();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint7 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint8 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.Title title4 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.buttonInfo = new System.Windows.Forms.Button();
            this.numericUpDownElemDef = new System.Windows.Forms.NumericUpDown();
            this.labelElemDef = new System.Windows.Forms.Label();
            this.numericUpDownAttMAG = new System.Windows.Forms.NumericUpDown();
            this.labelAttMAG = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.chartMagicDamage = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.labelSPR = new System.Windows.Forms.Label();
            this.numericUpDownSPR = new System.Windows.Forms.NumericUpDown();
            this.labelHealMAG = new System.Windows.Forms.Label();
            this.numericUpDownHealMAG = new System.Windows.Forms.NumericUpDown();
            this.checkBoxDefault = new System.Windows.Forms.CheckBox();
            this.labelHP = new System.Windows.Forms.Label();
            this.numericUpDownHP = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownElemDef)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAttMAG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartMagicDamage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSPR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHealMAG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHP)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonInfo
            // 
            this.buttonInfo.Image = ((System.Drawing.Image)(resources.GetObject("buttonInfo.Image")));
            this.buttonInfo.Location = new System.Drawing.Point(319, 6);
            this.buttonInfo.Name = "buttonInfo";
            this.buttonInfo.Size = new System.Drawing.Size(25, 25);
            this.buttonInfo.TabIndex = 22;
            this.buttonInfo.TabStop = false;
            this.buttonInfo.UseVisualStyleBackColor = true;
            this.buttonInfo.Click += new System.EventHandler(this.buttonInfo_Click);
            // 
            // numericUpDownElemDef
            // 
            this.numericUpDownElemDef.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownElemDef.Location = new System.Drawing.Point(273, 185);
            this.numericUpDownElemDef.Maximum = new decimal(new int[] {
            900,
            0,
            0,
            0});
            this.numericUpDownElemDef.Name = "numericUpDownElemDef";
            this.numericUpDownElemDef.Size = new System.Drawing.Size(47, 25);
            this.numericUpDownElemDef.TabIndex = 2;
            this.numericUpDownElemDef.Value = new decimal(new int[] {
            800,
            0,
            0,
            0});
            // 
            // labelElemDef
            // 
            this.labelElemDef.AutoSize = true;
            this.labelElemDef.BackColor = System.Drawing.Color.White;
            this.labelElemDef.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.labelElemDef.ForeColor = System.Drawing.SystemColors.WindowText;
            this.labelElemDef.Location = new System.Drawing.Point(257, 167);
            this.labelElemDef.Name = "labelElemDef";
            this.labelElemDef.Size = new System.Drawing.Size(79, 15);
            this.labelElemDef.TabIndex = 30;
            this.labelElemDef.Text = "Elem Defense";
            // 
            // numericUpDownAttMAG
            // 
            this.numericUpDownAttMAG.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownAttMAG.Location = new System.Drawing.Point(273, 128);
            this.numericUpDownAttMAG.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownAttMAG.Name = "numericUpDownAttMAG";
            this.numericUpDownAttMAG.Size = new System.Drawing.Size(47, 25);
            this.numericUpDownAttMAG.TabIndex = 0;
            // 
            // labelAttMAG
            // 
            this.labelAttMAG.AutoSize = true;
            this.labelAttMAG.BackColor = System.Drawing.Color.White;
            this.labelAttMAG.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.labelAttMAG.ForeColor = System.Drawing.SystemColors.WindowText;
            this.labelAttMAG.Location = new System.Drawing.Point(257, 110);
            this.labelAttMAG.Name = "labelAttMAG";
            this.labelAttMAG.Size = new System.Drawing.Size(81, 15);
            this.labelAttMAG.TabIndex = 33;
            this.labelAttMAG.Text = "Attacker MAG";
            // 
            // buttonClose
            // 
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Location = new System.Drawing.Point(261, 463);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 4;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // chartMagicDamage
            // 
            customLabel4.FromPosition = -1D;
            customLabel4.Text = "Attack";
            customLabel4.ToPosition = 3D;
            chartArea4.AxisX.CustomLabels.Add(customLabel4);
            chartArea4.AxisX.IsLabelAutoFit = false;
            chartArea4.AxisX.LabelAutoFitMaxFontSize = 9;
            chartArea4.AxisX.LabelAutoFitMinFontSize = 9;
            chartArea4.AxisX.LabelStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea4.AxisX.Title = "Type of Magic";
            chartArea4.AxisX.TitleFont = new System.Drawing.Font("Segoe UI Semibold", 10.5F);
            chartArea4.AxisY.Title = "Damage";
            chartArea4.AxisY.TitleFont = new System.Drawing.Font("Segoe UI Semibold", 10.5F);
            chartArea4.Name = "ChartAreaMagicDamage";
            this.chartMagicDamage.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.chartMagicDamage.Legends.Add(legend4);
            this.chartMagicDamage.Location = new System.Drawing.Point(0, 0);
            this.chartMagicDamage.MaximumSize = new System.Drawing.Size(350, 500);
            this.chartMagicDamage.MinimumSize = new System.Drawing.Size(350, 500);
            this.chartMagicDamage.Name = "chartMagicDamage";
            series7.ChartArea = "ChartAreaMagicDamage";
            series7.Legend = "Legend1";
            series7.Name = "Default";
            series7.Points.Add(dataPoint7);
            series7.ToolTip = "#VALY";
            series8.ChartArea = "ChartAreaMagicDamage";
            series8.Legend = "Legend1";
            series8.Name = "Current";
            series8.Points.Add(dataPoint8);
            series8.ToolTip = "#VALY";
            this.chartMagicDamage.Series.Add(series7);
            this.chartMagicDamage.Series.Add(series8);
            this.chartMagicDamage.Size = new System.Drawing.Size(350, 500);
            this.chartMagicDamage.TabIndex = 29;
            this.chartMagicDamage.TabStop = false;
            title4.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            title4.Name = "TitleMagicDamage";
            title4.Text = "MAGIC DAMAGE CHART";
            this.chartMagicDamage.Titles.Add(title4);
            this.chartMagicDamage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.chartMagicDamage_MouseDown);
            this.chartMagicDamage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.chartMagicDamage_MouseMove);
            this.chartMagicDamage.MouseUp += new System.Windows.Forms.MouseEventHandler(this.chartMagicDamage_MouseUp);
            // 
            // labelSPR
            // 
            this.labelSPR.AutoSize = true;
            this.labelSPR.BackColor = System.Drawing.Color.White;
            this.labelSPR.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.labelSPR.ForeColor = System.Drawing.SystemColors.WindowText;
            this.labelSPR.Location = new System.Drawing.Point(264, 220);
            this.labelSPR.Name = "labelSPR";
            this.labelSPR.Size = new System.Drawing.Size(64, 15);
            this.labelSPR.TabIndex = 33;
            this.labelSPR.Text = "Target SPR";
            // 
            // numericUpDownSPR
            // 
            this.numericUpDownSPR.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownSPR.Location = new System.Drawing.Point(273, 238);
            this.numericUpDownSPR.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownSPR.Name = "numericUpDownSPR";
            this.numericUpDownSPR.Size = new System.Drawing.Size(47, 25);
            this.numericUpDownSPR.TabIndex = 1;
            // 
            // labelHealMAG
            // 
            this.labelHealMAG.AutoSize = true;
            this.labelHealMAG.BackColor = System.Drawing.Color.White;
            this.labelHealMAG.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.labelHealMAG.ForeColor = System.Drawing.SystemColors.WindowText;
            this.labelHealMAG.Location = new System.Drawing.Point(259, 110);
            this.labelHealMAG.Name = "labelHealMAG";
            this.labelHealMAG.Size = new System.Drawing.Size(71, 15);
            this.labelHealMAG.TabIndex = 33;
            this.labelHealMAG.Text = "Healer MAG";
            // 
            // numericUpDownHealMAG
            // 
            this.numericUpDownHealMAG.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownHealMAG.Location = new System.Drawing.Point(273, 128);
            this.numericUpDownHealMAG.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownHealMAG.Name = "numericUpDownHealMAG";
            this.numericUpDownHealMAG.Size = new System.Drawing.Size(47, 25);
            this.numericUpDownHealMAG.TabIndex = 0;
            this.numericUpDownHealMAG.TabStop = false;
            // 
            // checkBoxDefault
            // 
            this.checkBoxDefault.AutoSize = true;
            this.checkBoxDefault.BackColor = System.Drawing.Color.White;
            this.checkBoxDefault.Checked = true;
            this.checkBoxDefault.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxDefault.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.checkBoxDefault.Location = new System.Drawing.Point(256, 388);
            this.checkBoxDefault.Name = "checkBoxDefault";
            this.checkBoxDefault.Size = new System.Drawing.Size(84, 34);
            this.checkBoxDefault.TabIndex = 3;
            this.checkBoxDefault.Text = "Use values \r\nfor default";
            this.checkBoxDefault.UseVisualStyleBackColor = false;
            // 
            // labelHP
            // 
            this.labelHP.AutoSize = true;
            this.labelHP.BackColor = System.Drawing.Color.White;
            this.labelHP.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.labelHP.ForeColor = System.Drawing.SystemColors.WindowText;
            this.labelHP.Location = new System.Drawing.Point(264, 110);
            this.labelHP.Name = "labelHP";
            this.labelHP.Size = new System.Drawing.Size(59, 15);
            this.labelHP.TabIndex = 33;
            this.labelHP.Text = "Target HP";
            // 
            // numericUpDownHP
            // 
            this.numericUpDownHP.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownHP.Location = new System.Drawing.Point(254, 128);
            this.numericUpDownHP.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numericUpDownHP.Name = "numericUpDownHP";
            this.numericUpDownHP.Size = new System.Drawing.Size(84, 25);
            this.numericUpDownHP.TabIndex = 0;
            this.numericUpDownHP.ThousandsSeparator = true;
            this.numericUpDownHP.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // MagicDamage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonClose;
            this.ClientSize = new System.Drawing.Size(348, 498);
            this.ControlBox = false;
            this.Controls.Add(this.checkBoxDefault);
            this.Controls.Add(this.buttonInfo);
            this.Controls.Add(this.numericUpDownElemDef);
            this.Controls.Add(this.labelElemDef);
            this.Controls.Add(this.numericUpDownHP);
            this.Controls.Add(this.labelHP);
            this.Controls.Add(this.numericUpDownHealMAG);
            this.Controls.Add(this.labelHealMAG);
            this.Controls.Add(this.numericUpDownSPR);
            this.Controls.Add(this.labelSPR);
            this.Controls.Add(this.numericUpDownAttMAG);
            this.Controls.Add(this.labelAttMAG);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.chartMagicDamage);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(350, 500);
            this.MinimumSize = new System.Drawing.Size(350, 500);
            this.Name = "MagicDamage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownElemDef)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAttMAG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartMagicDamage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSPR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHealMAG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHP)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonInfo;
        private System.Windows.Forms.NumericUpDown numericUpDownElemDef;
        private System.Windows.Forms.Label labelElemDef;
        private System.Windows.Forms.NumericUpDown numericUpDownAttMAG;
        private System.Windows.Forms.Label labelAttMAG;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartMagicDamage;
        private System.Windows.Forms.Label labelSPR;
        private System.Windows.Forms.NumericUpDown numericUpDownSPR;
        private System.Windows.Forms.Label labelHealMAG;
        private System.Windows.Forms.NumericUpDown numericUpDownHealMAG;
        private System.Windows.Forms.CheckBox checkBoxDefault;
        private System.Windows.Forms.Label labelHP;
        private System.Windows.Forms.NumericUpDown numericUpDownHP;
    }
}