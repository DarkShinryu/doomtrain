namespace Doomtrain.Characters_Stats_Charts
{
    partial class CharVIT
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint1 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 400D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint2 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(1D, 800D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint3 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(2D, 1200D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint4 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(3D, 1600D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint5 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(4D, 2000D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint6 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(5D, 2500D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint7 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(6D, 3000D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint8 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(7D, 3500D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint9 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(8D, 4000D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint10 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(9D, 4500D);
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint11 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 9000D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint12 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(1D, 255D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint13 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(2D, 255D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint14 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(3D, 255D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint15 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(4D, 255D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint16 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(5D, 255D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint17 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(6D, 255D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint18 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(7D, 255D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint19 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(8D, 255D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint20 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(9D, 255D);
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.buttonVITClose = new System.Windows.Forms.Button();
            this.chartVIT = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chartVIT)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonVITClose
            // 
            this.buttonVITClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonVITClose.Location = new System.Drawing.Point(551, 413);
            this.buttonVITClose.Name = "buttonVITClose";
            this.buttonVITClose.Size = new System.Drawing.Size(75, 23);
            this.buttonVITClose.TabIndex = 11;
            this.buttonVITClose.Text = "Close";
            this.buttonVITClose.UseVisualStyleBackColor = true;
            this.buttonVITClose.Click += new System.EventHandler(this.buttonVITClose_Click);
            // 
            // chartVIT
            // 
            chartArea1.AxisX.Title = "Level";
            chartArea1.AxisX.TitleFont = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisY.Title = "VIT";
            chartArea1.AxisY.TitleFont = new System.Drawing.Font("Segoe UI Semibold", 10F);
            chartArea1.Name = "ChartAreaVIT";
            this.chartVIT.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartVIT.Legends.Add(legend1);
            this.chartVIT.Location = new System.Drawing.Point(0, 0);
            this.chartVIT.Name = "chartVIT";
            series1.ChartArea = "ChartAreaVIT";
            series1.Legend = "Legend1";
            series1.Name = "Default";
            dataPoint1.AxisLabel = "";
            dataPoint2.AxisLabel = "";
            dataPoint3.AxisLabel = "";
            dataPoint4.AxisLabel = "";
            dataPoint5.AxisLabel = "";
            dataPoint6.AxisLabel = "";
            dataPoint7.AxisLabel = "";
            dataPoint8.AxisLabel = "";
            dataPoint9.AxisLabel = "";
            dataPoint10.AxisLabel = "";
            dataPoint10.Label = "";
            series1.Points.Add(dataPoint1);
            series1.Points.Add(dataPoint2);
            series1.Points.Add(dataPoint3);
            series1.Points.Add(dataPoint4);
            series1.Points.Add(dataPoint5);
            series1.Points.Add(dataPoint6);
            series1.Points.Add(dataPoint7);
            series1.Points.Add(dataPoint8);
            series1.Points.Add(dataPoint9);
            series1.Points.Add(dataPoint10);
            series2.ChartArea = "ChartAreaVIT";
            series2.Legend = "Legend1";
            series2.Name = "Current";
            dataPoint11.AxisLabel = "10";
            dataPoint11.IsValueShownAsLabel = false;
            dataPoint12.AxisLabel = "20";
            dataPoint13.AxisLabel = "30";
            dataPoint14.AxisLabel = "40";
            dataPoint15.AxisLabel = "50";
            dataPoint16.AxisLabel = "60";
            dataPoint17.AxisLabel = "70";
            dataPoint18.AxisLabel = "80";
            dataPoint19.AxisLabel = "90";
            dataPoint20.AxisLabel = "100";
            dataPoint20.IsValueShownAsLabel = false;
            dataPoint20.Label = "";
            series2.Points.Add(dataPoint11);
            series2.Points.Add(dataPoint12);
            series2.Points.Add(dataPoint13);
            series2.Points.Add(dataPoint14);
            series2.Points.Add(dataPoint15);
            series2.Points.Add(dataPoint16);
            series2.Points.Add(dataPoint17);
            series2.Points.Add(dataPoint18);
            series2.Points.Add(dataPoint19);
            series2.Points.Add(dataPoint20);
            this.chartVIT.Series.Add(series1);
            this.chartVIT.Series.Add(series2);
            this.chartVIT.Size = new System.Drawing.Size(640, 450);
            this.chartVIT.TabIndex = 10;
            title1.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            title1.Name = "TitleVIT";
            title1.Text = "VIT CHART";
            this.chartVIT.Titles.Add(title1);
            this.chartVIT.MouseDown += new System.Windows.Forms.MouseEventHandler(this.chartVIT_MouseDown);
            this.chartVIT.MouseMove += new System.Windows.Forms.MouseEventHandler(this.chartVIT_MouseMove);
            this.chartVIT.MouseUp += new System.Windows.Forms.MouseEventHandler(this.chartVIT_MouseUp);
            // 
            // CharVIT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonVITClose;
            this.ClientSize = new System.Drawing.Size(638, 448);
            this.ControlBox = false;
            this.Controls.Add(this.buttonVITClose);
            this.Controls.Add(this.chartVIT);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximumSize = new System.Drawing.Size(640, 450);
            this.MinimumSize = new System.Drawing.Size(640, 450);
            this.Name = "CharVIT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.chartVIT)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonVITClose;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartVIT;
    }
}