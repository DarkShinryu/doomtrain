﻿namespace Doomtrain.Charts
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.CustomLabel customLabel1 = new System.Windows.Forms.DataVisualization.Charting.CustomLabel();
            System.Windows.Forms.DataVisualization.Charting.CustomLabel customLabel2 = new System.Windows.Forms.DataVisualization.Charting.CustomLabel();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint1 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint2 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(1D, 0D);
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint3 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint4 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(1D, 0D);
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
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
            this.buttonInfo.Location = new System.Drawing.Point(457, 12);
            this.buttonInfo.Name = "buttonInfo";
            this.buttonInfo.Size = new System.Drawing.Size(29, 24);
            this.buttonInfo.TabIndex = 22;
            this.buttonInfo.UseVisualStyleBackColor = true;
            this.buttonInfo.Click += new System.EventHandler(this.buttonInfo_Click);
            // 
            // numericUpDownElemDef
            // 
            this.numericUpDownElemDef.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownElemDef.Location = new System.Drawing.Point(419, 238);
            this.numericUpDownElemDef.Maximum = new decimal(new int[] {
            900,
            0,
            0,
            0});
            this.numericUpDownElemDef.Name = "numericUpDownElemDef";
            this.numericUpDownElemDef.Size = new System.Drawing.Size(47, 25);
            this.numericUpDownElemDef.TabIndex = 25;
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
            this.labelElemDef.Location = new System.Drawing.Point(403, 220);
            this.labelElemDef.Name = "labelElemDef";
            this.labelElemDef.Size = new System.Drawing.Size(79, 15);
            this.labelElemDef.TabIndex = 30;
            this.labelElemDef.Text = "Elem Defense";
            // 
            // numericUpDownAttMAG
            // 
            this.numericUpDownAttMAG.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownAttMAG.Location = new System.Drawing.Point(419, 128);
            this.numericUpDownAttMAG.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownAttMAG.Name = "numericUpDownAttMAG";
            this.numericUpDownAttMAG.Size = new System.Drawing.Size(47, 25);
            this.numericUpDownAttMAG.TabIndex = 23;
            // 
            // labelAttMAG
            // 
            this.labelAttMAG.AutoSize = true;
            this.labelAttMAG.BackColor = System.Drawing.Color.White;
            this.labelAttMAG.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.labelAttMAG.ForeColor = System.Drawing.SystemColors.WindowText;
            this.labelAttMAG.Location = new System.Drawing.Point(403, 110);
            this.labelAttMAG.Name = "labelAttMAG";
            this.labelAttMAG.Size = new System.Drawing.Size(81, 15);
            this.labelAttMAG.TabIndex = 33;
            this.labelAttMAG.Text = "Attacker MAG";
            // 
            // buttonClose
            // 
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Location = new System.Drawing.Point(411, 463);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 28;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // chartMagicDamage
            // 
            customLabel1.FromPosition = -0.5D;
            customLabel1.Text = "Attack";
            customLabel1.ToPosition = 0.5D;
            customLabel2.FromPosition = 0.5D;
            customLabel2.Text = "Curative";
            customLabel2.ToPosition = 1.5D;
            chartArea1.AxisX.CustomLabels.Add(customLabel1);
            chartArea1.AxisX.CustomLabels.Add(customLabel2);
            chartArea1.AxisX.IsLabelAutoFit = false;
            chartArea1.AxisX.LabelAutoFitMaxFontSize = 9;
            chartArea1.AxisX.LabelAutoFitMinFontSize = 9;
            chartArea1.AxisX.LabelStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisX.Title = "Type of Magic";
            chartArea1.AxisX.TitleFont = new System.Drawing.Font("Segoe UI Semibold", 10.5F);
            chartArea1.AxisY.Title = "Damage";
            chartArea1.AxisY.TitleFont = new System.Drawing.Font("Segoe UI Semibold", 10.5F);
            chartArea1.Name = "ChartAreaMagicDamage";
            this.chartMagicDamage.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartMagicDamage.Legends.Add(legend1);
            this.chartMagicDamage.Location = new System.Drawing.Point(0, 0);
            this.chartMagicDamage.MaximumSize = new System.Drawing.Size(400, 500);
            this.chartMagicDamage.MinimumSize = new System.Drawing.Size(500, 500);
            this.chartMagicDamage.Name = "chartMagicDamage";
            series1.ChartArea = "ChartAreaMagicDamage";
            series1.Legend = "Legend1";
            series1.Name = "Default";
            dataPoint2.AxisLabel = "";
            dataPoint2.IsValueShownAsLabel = false;
            dataPoint2.MarkerSize = 5;
            series1.Points.Add(dataPoint1);
            series1.Points.Add(dataPoint2);
            series1.ToolTip = "#VALY";
            series2.ChartArea = "ChartAreaMagicDamage";
            series2.Legend = "Legend1";
            series2.Name = "Current";
            series2.Points.Add(dataPoint3);
            series2.Points.Add(dataPoint4);
            series2.ToolTip = "#VALY";
            this.chartMagicDamage.Series.Add(series1);
            this.chartMagicDamage.Series.Add(series2);
            this.chartMagicDamage.Size = new System.Drawing.Size(500, 500);
            this.chartMagicDamage.TabIndex = 29;
            title1.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            title1.Name = "TitleMagicDamage";
            title1.Text = "MAGIC DAMAGE CHART";
            this.chartMagicDamage.Titles.Add(title1);
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
            this.labelSPR.Location = new System.Drawing.Point(410, 167);
            this.labelSPR.Name = "labelSPR";
            this.labelSPR.Size = new System.Drawing.Size(64, 15);
            this.labelSPR.TabIndex = 33;
            this.labelSPR.Text = "Target SPR";
            // 
            // numericUpDownSPR
            // 
            this.numericUpDownSPR.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownSPR.Location = new System.Drawing.Point(419, 185);
            this.numericUpDownSPR.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownSPR.Name = "numericUpDownSPR";
            this.numericUpDownSPR.Size = new System.Drawing.Size(47, 25);
            this.numericUpDownSPR.TabIndex = 23;
            // 
            // labelHealMAG
            // 
            this.labelHealMAG.AutoSize = true;
            this.labelHealMAG.BackColor = System.Drawing.Color.White;
            this.labelHealMAG.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.labelHealMAG.ForeColor = System.Drawing.SystemColors.WindowText;
            this.labelHealMAG.Location = new System.Drawing.Point(407, 108);
            this.labelHealMAG.Name = "labelHealMAG";
            this.labelHealMAG.Size = new System.Drawing.Size(71, 15);
            this.labelHealMAG.TabIndex = 33;
            this.labelHealMAG.Text = "Healer MAG";
            // 
            // numericUpDownHealMAG
            // 
            this.numericUpDownHealMAG.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownHealMAG.Location = new System.Drawing.Point(419, 126);
            this.numericUpDownHealMAG.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownHealMAG.Name = "numericUpDownHealMAG";
            this.numericUpDownHealMAG.Size = new System.Drawing.Size(47, 25);
            this.numericUpDownHealMAG.TabIndex = 23;
            // 
            // checkBoxDefault
            // 
            this.checkBoxDefault.AutoSize = true;
            this.checkBoxDefault.BackColor = System.Drawing.Color.White;
            this.checkBoxDefault.Checked = true;
            this.checkBoxDefault.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxDefault.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.checkBoxDefault.Location = new System.Drawing.Point(406, 388);
            this.checkBoxDefault.Name = "checkBoxDefault";
            this.checkBoxDefault.Size = new System.Drawing.Size(84, 34);
            this.checkBoxDefault.TabIndex = 34;
            this.checkBoxDefault.Text = "Use values \r\nfor default";
            this.checkBoxDefault.UseVisualStyleBackColor = false;
            // 
            // labelHP
            // 
            this.labelHP.AutoSize = true;
            this.labelHP.BackColor = System.Drawing.Color.White;
            this.labelHP.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.labelHP.ForeColor = System.Drawing.SystemColors.WindowText;
            this.labelHP.Location = new System.Drawing.Point(411, 111);
            this.labelHP.Name = "labelHP";
            this.labelHP.Size = new System.Drawing.Size(59, 15);
            this.labelHP.TabIndex = 33;
            this.labelHP.Text = "Target HP";
            // 
            // numericUpDownHP
            // 
            this.numericUpDownHP.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownHP.Location = new System.Drawing.Point(400, 129);
            this.numericUpDownHP.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numericUpDownHP.Name = "numericUpDownHP";
            this.numericUpDownHP.Size = new System.Drawing.Size(84, 25);
            this.numericUpDownHP.TabIndex = 23;
            this.numericUpDownHP.ThousandsSeparator = true;
            this.numericUpDownHP.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // MagicDamage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 498);
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
            this.MaximumSize = new System.Drawing.Size(500, 500);
            this.MinimumSize = new System.Drawing.Size(500, 500);
            this.Name = "MagicDamage";
            this.ShowIcon = false;
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