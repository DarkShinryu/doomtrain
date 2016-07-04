namespace Doomtrain.Characters_Stats_Charts
{
    partial class CharHP
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint21 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 400D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint22 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(1D, 800D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint23 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(2D, 1200D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint24 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(3D, 1600D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint25 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(4D, 2000D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint26 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(5D, 2500D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint27 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(6D, 3000D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint28 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(7D, 3500D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint29 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(8D, 4000D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint30 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(9D, 4500D);
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint31 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 9000D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint32 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(1D, 255D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint33 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(2D, 255D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint34 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(3D, 255D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint35 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(4D, 255D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint36 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(5D, 255D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint37 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(6D, 255D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint38 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(7D, 255D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint39 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(8D, 255D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint40 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(9D, 255D);
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.chartHP = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.buttonHPClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chartHP)).BeginInit();
            this.SuspendLayout();
            // 
            // chartHP
            // 
            chartArea2.AxisX.Title = "Level";
            chartArea2.AxisX.TitleFont = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea2.AxisY.Title = "HP";
            chartArea2.AxisY.TitleFont = new System.Drawing.Font("Segoe UI Semibold", 10F);
            chartArea2.Name = "ChartAreaHP";
            this.chartHP.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chartHP.Legends.Add(legend2);
            this.chartHP.Location = new System.Drawing.Point(0, 0);
            this.chartHP.Name = "chartHP";
            series3.ChartArea = "ChartAreaHP";
            series3.Legend = "Legend1";
            series3.Name = "Default";
            dataPoint21.AxisLabel = "";
            dataPoint22.AxisLabel = "";
            dataPoint23.AxisLabel = "";
            dataPoint24.AxisLabel = "";
            dataPoint25.AxisLabel = "";
            dataPoint26.AxisLabel = "";
            dataPoint27.AxisLabel = "";
            dataPoint28.AxisLabel = "";
            dataPoint29.AxisLabel = "";
            dataPoint30.AxisLabel = "";
            dataPoint30.Label = "";
            series3.Points.Add(dataPoint21);
            series3.Points.Add(dataPoint22);
            series3.Points.Add(dataPoint23);
            series3.Points.Add(dataPoint24);
            series3.Points.Add(dataPoint25);
            series3.Points.Add(dataPoint26);
            series3.Points.Add(dataPoint27);
            series3.Points.Add(dataPoint28);
            series3.Points.Add(dataPoint29);
            series3.Points.Add(dataPoint30);
            series4.ChartArea = "ChartAreaHP";
            series4.Legend = "Legend1";
            series4.Name = "Current";
            dataPoint31.AxisLabel = "10";
            dataPoint31.IsValueShownAsLabel = false;
            dataPoint32.AxisLabel = "20";
            dataPoint33.AxisLabel = "30";
            dataPoint34.AxisLabel = "40";
            dataPoint35.AxisLabel = "50";
            dataPoint36.AxisLabel = "60";
            dataPoint37.AxisLabel = "70";
            dataPoint38.AxisLabel = "80";
            dataPoint39.AxisLabel = "90";
            dataPoint40.AxisLabel = "100";
            dataPoint40.IsValueShownAsLabel = false;
            dataPoint40.Label = "";
            series4.Points.Add(dataPoint31);
            series4.Points.Add(dataPoint32);
            series4.Points.Add(dataPoint33);
            series4.Points.Add(dataPoint34);
            series4.Points.Add(dataPoint35);
            series4.Points.Add(dataPoint36);
            series4.Points.Add(dataPoint37);
            series4.Points.Add(dataPoint38);
            series4.Points.Add(dataPoint39);
            series4.Points.Add(dataPoint40);
            this.chartHP.Series.Add(series3);
            this.chartHP.Series.Add(series4);
            this.chartHP.Size = new System.Drawing.Size(640, 450);
            this.chartHP.TabIndex = 6;
            title2.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            title2.Name = "TitleHP";
            title2.Text = "HP CHART";
            this.chartHP.Titles.Add(title2);
            this.chartHP.MouseDown += new System.Windows.Forms.MouseEventHandler(this.chartHP_MouseDown);
            this.chartHP.MouseMove += new System.Windows.Forms.MouseEventHandler(this.chartHP_MouseMove);
            this.chartHP.MouseUp += new System.Windows.Forms.MouseEventHandler(this.chartHP_MouseUp);
            // 
            // buttonHPClose
            // 
            this.buttonHPClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonHPClose.Location = new System.Drawing.Point(551, 413);
            this.buttonHPClose.Name = "buttonHPClose";
            this.buttonHPClose.Size = new System.Drawing.Size(75, 23);
            this.buttonHPClose.TabIndex = 7;
            this.buttonHPClose.Text = "Close";
            this.buttonHPClose.UseVisualStyleBackColor = true;
            this.buttonHPClose.Click += new System.EventHandler(this.buttonHPClose_Click);
            // 
            // CharHP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonHPClose;
            this.ClientSize = new System.Drawing.Size(638, 448);
            this.ControlBox = false;
            this.Controls.Add(this.buttonHPClose);
            this.Controls.Add(this.chartHP);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximumSize = new System.Drawing.Size(640, 450);
            this.MinimumSize = new System.Drawing.Size(640, 450);
            this.Name = "CharHP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.chartHP)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartHP;
        private System.Windows.Forms.Button buttonHPClose;
    }
}