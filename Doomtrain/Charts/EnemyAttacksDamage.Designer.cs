namespace Doomtrain.Charts
{
    partial class EnemyAttacksDamage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EnemyAttacksDamage));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.CustomLabel customLabel5 = new System.Windows.Forms.DataVisualization.Charting.CustomLabel();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series9 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint9 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.Series series10 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint10 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.Title title5 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.checkBoxDefault = new System.Windows.Forms.CheckBox();
            this.buttonInfo = new System.Windows.Forms.Button();
            this.numericUpDownElemDef = new System.Windows.Forms.NumericUpDown();
            this.labelElemDef = new System.Windows.Forms.Label();
            this.numericUpDownHP = new System.Windows.Forms.NumericUpDown();
            this.labelHP = new System.Windows.Forms.Label();
            this.numericUpDownSPR = new System.Windows.Forms.NumericUpDown();
            this.labelSPR = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.chartEADamage = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.numericUpDownHealMAG = new System.Windows.Forms.NumericUpDown();
            this.labelHealMAG = new System.Windows.Forms.Label();
            this.labelVIT = new System.Windows.Forms.Label();
            this.numericUpDownVIT = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownMAG = new System.Windows.Forms.NumericUpDown();
            this.labelMAG = new System.Windows.Forms.Label();
            this.labelSTR = new System.Windows.Forms.Label();
            this.numericUpDownSTR = new System.Windows.Forms.NumericUpDown();
            this.labelKilled = new System.Windows.Forms.Label();
            this.numericUpDownKilled = new System.Windows.Forms.NumericUpDown();
            this.labelElemAtt = new System.Windows.Forms.Label();
            this.numericUpDownElemAtt = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownElemDef)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSPR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartEADamage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHealMAG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVIT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMAG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSTR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownKilled)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownElemAtt)).BeginInit();
            this.SuspendLayout();
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
            this.checkBoxDefault.TabIndex = 37;
            this.checkBoxDefault.Text = "Use values \r\nfor default";
            this.checkBoxDefault.UseVisualStyleBackColor = false;
            // 
            // buttonInfo
            // 
            this.buttonInfo.Image = ((System.Drawing.Image)(resources.GetObject("buttonInfo.Image")));
            this.buttonInfo.Location = new System.Drawing.Point(319, 6);
            this.buttonInfo.Name = "buttonInfo";
            this.buttonInfo.Size = new System.Drawing.Size(25, 25);
            this.buttonInfo.TabIndex = 39;
            this.buttonInfo.TabStop = false;
            this.buttonInfo.UseVisualStyleBackColor = true;
            this.buttonInfo.Click += new System.EventHandler(this.buttonInfo_Click);
            // 
            // numericUpDownElemDef
            // 
            this.numericUpDownElemDef.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownElemDef.Location = new System.Drawing.Point(273, 239);
            this.numericUpDownElemDef.Maximum = new decimal(new int[] {
            900,
            0,
            0,
            0});
            this.numericUpDownElemDef.Name = "numericUpDownElemDef";
            this.numericUpDownElemDef.Size = new System.Drawing.Size(47, 25);
            this.numericUpDownElemDef.TabIndex = 36;
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
            this.labelElemDef.Location = new System.Drawing.Point(257, 221);
            this.labelElemDef.Name = "labelElemDef";
            this.labelElemDef.Size = new System.Drawing.Size(79, 15);
            this.labelElemDef.TabIndex = 41;
            this.labelElemDef.Text = "Elem Defense";
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
            this.numericUpDownHP.TabIndex = 34;
            this.numericUpDownHP.ThousandsSeparator = true;
            this.numericUpDownHP.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
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
            this.labelHP.TabIndex = 42;
            this.labelHP.Text = "Target HP";
            // 
            // numericUpDownSPR
            // 
            this.numericUpDownSPR.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownSPR.Location = new System.Drawing.Point(273, 185);
            this.numericUpDownSPR.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownSPR.Name = "numericUpDownSPR";
            this.numericUpDownSPR.Size = new System.Drawing.Size(47, 25);
            this.numericUpDownSPR.TabIndex = 35;
            // 
            // labelSPR
            // 
            this.labelSPR.AutoSize = true;
            this.labelSPR.BackColor = System.Drawing.Color.White;
            this.labelSPR.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.labelSPR.ForeColor = System.Drawing.SystemColors.WindowText;
            this.labelSPR.Location = new System.Drawing.Point(264, 167);
            this.labelSPR.Name = "labelSPR";
            this.labelSPR.Size = new System.Drawing.Size(64, 15);
            this.labelSPR.TabIndex = 43;
            this.labelSPR.Text = "Target SPR";
            // 
            // buttonClose
            // 
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Location = new System.Drawing.Point(261, 463);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 38;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // chartEADamage
            // 
            customLabel5.FromPosition = -1D;
            customLabel5.Text = "Basic Attack";
            customLabel5.ToPosition = 3D;
            chartArea5.AxisX.CustomLabels.Add(customLabel5);
            chartArea5.AxisX.IsLabelAutoFit = false;
            chartArea5.AxisX.LabelAutoFitMaxFontSize = 9;
            chartArea5.AxisX.LabelAutoFitMinFontSize = 9;
            chartArea5.AxisX.LabelStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea5.AxisX.Title = "Attack Type";
            chartArea5.AxisX.TitleFont = new System.Drawing.Font("Segoe UI Semibold", 10.5F);
            chartArea5.AxisY.Title = "Damage";
            chartArea5.AxisY.TitleFont = new System.Drawing.Font("Segoe UI Semibold", 10.5F);
            chartArea5.Name = "ChartAreaEADamage";
            this.chartEADamage.ChartAreas.Add(chartArea5);
            legend5.Name = "Legend1";
            this.chartEADamage.Legends.Add(legend5);
            this.chartEADamage.Location = new System.Drawing.Point(0, 0);
            this.chartEADamage.MaximumSize = new System.Drawing.Size(350, 500);
            this.chartEADamage.MinimumSize = new System.Drawing.Size(350, 500);
            this.chartEADamage.Name = "chartEADamage";
            series9.ChartArea = "ChartAreaEADamage";
            series9.Legend = "Legend1";
            series9.Name = "Default";
            series9.Points.Add(dataPoint9);
            series9.ToolTip = "#VALY";
            series10.ChartArea = "ChartAreaEADamage";
            series10.Legend = "Legend1";
            series10.Name = "Current";
            series10.Points.Add(dataPoint10);
            series10.ToolTip = "#VALY";
            this.chartEADamage.Series.Add(series9);
            this.chartEADamage.Series.Add(series10);
            this.chartEADamage.Size = new System.Drawing.Size(350, 500);
            this.chartEADamage.TabIndex = 40;
            this.chartEADamage.TabStop = false;
            title5.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            title5.Name = "TitleEADamage";
            title5.Text = "ENEMY A. DAMAGE CHART";
            this.chartEADamage.Titles.Add(title5);
            this.chartEADamage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.chartEADamage_MouseDown);
            this.chartEADamage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.chartEADamage_MouseMove);
            this.chartEADamage.MouseUp += new System.Windows.Forms.MouseEventHandler(this.chartEADamage_MouseUp);
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
            this.numericUpDownHealMAG.TabIndex = 44;
            this.numericUpDownHealMAG.TabStop = false;
            // 
            // labelHealMAG
            // 
            this.labelHealMAG.AutoSize = true;
            this.labelHealMAG.BackColor = System.Drawing.Color.White;
            this.labelHealMAG.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.labelHealMAG.ForeColor = System.Drawing.SystemColors.WindowText;
            this.labelHealMAG.Location = new System.Drawing.Point(261, 110);
            this.labelHealMAG.Name = "labelHealMAG";
            this.labelHealMAG.Size = new System.Drawing.Size(71, 15);
            this.labelHealMAG.TabIndex = 45;
            this.labelHealMAG.Text = "Healer MAG";
            // 
            // labelVIT
            // 
            this.labelVIT.AutoSize = true;
            this.labelVIT.BackColor = System.Drawing.Color.White;
            this.labelVIT.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.labelVIT.ForeColor = System.Drawing.SystemColors.WindowText;
            this.labelVIT.Location = new System.Drawing.Point(264, 271);
            this.labelVIT.Name = "labelVIT";
            this.labelVIT.Size = new System.Drawing.Size(62, 15);
            this.labelVIT.TabIndex = 43;
            this.labelVIT.Text = "Target VIT";
            // 
            // numericUpDownVIT
            // 
            this.numericUpDownVIT.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownVIT.Location = new System.Drawing.Point(273, 289);
            this.numericUpDownVIT.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownVIT.Name = "numericUpDownVIT";
            this.numericUpDownVIT.Size = new System.Drawing.Size(47, 25);
            this.numericUpDownVIT.TabIndex = 35;
            // 
            // numericUpDownMAG
            // 
            this.numericUpDownMAG.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownMAG.Location = new System.Drawing.Point(273, 128);
            this.numericUpDownMAG.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownMAG.Name = "numericUpDownMAG";
            this.numericUpDownMAG.Size = new System.Drawing.Size(47, 25);
            this.numericUpDownMAG.TabIndex = 46;
            // 
            // labelMAG
            // 
            this.labelMAG.AutoSize = true;
            this.labelMAG.BackColor = System.Drawing.Color.White;
            this.labelMAG.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.labelMAG.ForeColor = System.Drawing.SystemColors.WindowText;
            this.labelMAG.Location = new System.Drawing.Point(257, 110);
            this.labelMAG.Name = "labelMAG";
            this.labelMAG.Size = new System.Drawing.Size(81, 15);
            this.labelMAG.TabIndex = 47;
            this.labelMAG.Text = "Attacker MAG";
            // 
            // labelSTR
            // 
            this.labelSTR.AutoSize = true;
            this.labelSTR.BackColor = System.Drawing.Color.White;
            this.labelSTR.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.labelSTR.ForeColor = System.Drawing.SystemColors.WindowText;
            this.labelSTR.Location = new System.Drawing.Point(257, 110);
            this.labelSTR.Name = "labelSTR";
            this.labelSTR.Size = new System.Drawing.Size(75, 15);
            this.labelSTR.TabIndex = 47;
            this.labelSTR.Text = "Attacker STR";
            // 
            // numericUpDownSTR
            // 
            this.numericUpDownSTR.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownSTR.Location = new System.Drawing.Point(273, 128);
            this.numericUpDownSTR.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownSTR.Name = "numericUpDownSTR";
            this.numericUpDownSTR.Size = new System.Drawing.Size(47, 25);
            this.numericUpDownSTR.TabIndex = 46;
            this.numericUpDownSTR.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // labelKilled
            // 
            this.labelKilled.AutoSize = true;
            this.labelKilled.BackColor = System.Drawing.Color.White;
            this.labelKilled.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.labelKilled.ForeColor = System.Drawing.SystemColors.WindowText;
            this.labelKilled.Location = new System.Drawing.Point(255, 110);
            this.labelKilled.Name = "labelKilled";
            this.labelKilled.Size = new System.Drawing.Size(83, 15);
            this.labelKilled.TabIndex = 45;
            this.labelKilled.Text = "Enemies Killed";
            // 
            // numericUpDownKilled
            // 
            this.numericUpDownKilled.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownKilled.Location = new System.Drawing.Point(269, 128);
            this.numericUpDownKilled.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDownKilled.Name = "numericUpDownKilled";
            this.numericUpDownKilled.Size = new System.Drawing.Size(56, 25);
            this.numericUpDownKilled.TabIndex = 44;
            this.numericUpDownKilled.TabStop = false;
            this.numericUpDownKilled.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // labelElemAtt
            // 
            this.labelElemAtt.AutoSize = true;
            this.labelElemAtt.BackColor = System.Drawing.Color.White;
            this.labelElemAtt.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.labelElemAtt.ForeColor = System.Drawing.SystemColors.WindowText;
            this.labelElemAtt.Location = new System.Drawing.Point(261, 167);
            this.labelElemAtt.Name = "labelElemAtt";
            this.labelElemAtt.Size = new System.Drawing.Size(70, 15);
            this.labelElemAtt.TabIndex = 41;
            this.labelElemAtt.Text = "Elem Attack";
            // 
            // numericUpDownElemAtt
            // 
            this.numericUpDownElemAtt.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownElemAtt.Location = new System.Drawing.Point(273, 185);
            this.numericUpDownElemAtt.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownElemAtt.Name = "numericUpDownElemAtt";
            this.numericUpDownElemAtt.Size = new System.Drawing.Size(47, 25);
            this.numericUpDownElemAtt.TabIndex = 36;
            // 
            // EnemyAttacksDamage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonClose;
            this.ClientSize = new System.Drawing.Size(348, 498);
            this.ControlBox = false;
            this.Controls.Add(this.numericUpDownSTR);
            this.Controls.Add(this.labelSTR);
            this.Controls.Add(this.numericUpDownMAG);
            this.Controls.Add(this.labelMAG);
            this.Controls.Add(this.numericUpDownKilled);
            this.Controls.Add(this.labelKilled);
            this.Controls.Add(this.numericUpDownHealMAG);
            this.Controls.Add(this.labelHealMAG);
            this.Controls.Add(this.checkBoxDefault);
            this.Controls.Add(this.buttonInfo);
            this.Controls.Add(this.numericUpDownElemAtt);
            this.Controls.Add(this.labelElemAtt);
            this.Controls.Add(this.numericUpDownElemDef);
            this.Controls.Add(this.labelElemDef);
            this.Controls.Add(this.numericUpDownHP);
            this.Controls.Add(this.labelHP);
            this.Controls.Add(this.numericUpDownVIT);
            this.Controls.Add(this.labelVIT);
            this.Controls.Add(this.numericUpDownSPR);
            this.Controls.Add(this.labelSPR);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.chartEADamage);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(350, 500);
            this.MinimumSize = new System.Drawing.Size(350, 500);
            this.Name = "EnemyAttacksDamage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownElemDef)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSPR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartEADamage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHealMAG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVIT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMAG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSTR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownKilled)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownElemAtt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxDefault;
        private System.Windows.Forms.Button buttonInfo;
        private System.Windows.Forms.NumericUpDown numericUpDownElemDef;
        private System.Windows.Forms.Label labelElemDef;
        private System.Windows.Forms.NumericUpDown numericUpDownHP;
        private System.Windows.Forms.Label labelHP;
        private System.Windows.Forms.NumericUpDown numericUpDownSPR;
        private System.Windows.Forms.Label labelSPR;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartEADamage;
        private System.Windows.Forms.NumericUpDown numericUpDownHealMAG;
        private System.Windows.Forms.Label labelHealMAG;
        private System.Windows.Forms.Label labelVIT;
        private System.Windows.Forms.NumericUpDown numericUpDownVIT;
        private System.Windows.Forms.NumericUpDown numericUpDownMAG;
        private System.Windows.Forms.Label labelMAG;
        private System.Windows.Forms.Label labelSTR;
        private System.Windows.Forms.NumericUpDown numericUpDownSTR;
        private System.Windows.Forms.Label labelKilled;
        private System.Windows.Forms.NumericUpDown numericUpDownKilled;
        private System.Windows.Forms.Label labelElemAtt;
        private System.Windows.Forms.NumericUpDown numericUpDownElemAtt;
    }
}