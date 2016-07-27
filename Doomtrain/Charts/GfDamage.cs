using System;
using System.Drawing;
using System.Windows.Forms;

namespace Doomtrain.Charts
{
    public partial class GfDamage : Form
    {
        public GfDamage(mainForm mainForm)
        {
            _mainForm = mainForm;
            InitializeComponent();
            GfDamageChartWorker();

            _mainForm.numericUpDownGFPower.ValueChanged += new EventHandler(this.numericUpDownGfDamage_ValueChanged);
            _mainForm.numericUpDownGFPowerMod.ValueChanged += new EventHandler(this.numericUpDownGfDamage_ValueChanged);
            _mainForm.numericUpDownGFLevelMod.ValueChanged += new EventHandler(this.numericUpDownGfDamage_ValueChanged);
            numericUpDownSPR.ValueChanged += new EventHandler(numericUpDownGfDamage_ValueChanged);
            numericUpDownBoost.ValueChanged += new EventHandler(numericUpDownGfDamage_ValueChanged);
            numericUpDownSumMag.ValueChanged += new EventHandler(numericUpDownGfDamage_ValueChanged);
            numericUpDownElemDef.ValueChanged += new EventHandler(numericUpDownGfDamage_ValueChanged);

            checkBoxDefault.CheckedChanged += new EventHandler(Default_CheckedChanged);
        }

        private readonly mainForm _mainForm;


        #region Moving chart

        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        private void chartGfDamage_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void chartGfDamage_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void chartGfDamage_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        #endregion

        private void buttonInfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The values here are only used for the chart and they have no connection to the kernel file.\n" +
                "You need to change the values manually.\n\n" +
                "Target SPR = The SPR value of the target.\n" +
                "Boost = GF boost, when not used is 100.\n" +
                "SumMagBonus = The % of Sum Mag Bonus.\n" + 
                "Elem Defense = The elemental defense of the target.", "Info",
                MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }



        #region Calculation variables declaration

        private int a1, a10, a20, a30, a40, a50, a60, a70, a80, a90, a100; //a = first calculation
        private int b1, b10, b20, b30, b40, b50, b60, b70, b80, b90, b100; //b = second calculation
        private int c1, c10, c20, c30, c40, c50, c60, c70, c80, c90, c100; //c = third calculation
        private int d1, d10, d20, d30, d40, d50, d60, d70, d80, d90, d100; //d = fourth calculation
        private int e1, e10, e20, e30, e40, e50, e60, e70, e80, e90, e100; //e = fifth calculation

        #endregion

        #region Read chart values        

        private void GfDamageChartWorker()
        {
            KernelWorker.ReadGF(_mainForm.listBoxGF.SelectedIndex, KernelWorker.BackupKernel);
            try
            {
                a1 = KernelWorker.GetSelectedGFData.GFLevelMod * 1 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                a10 = KernelWorker.GetSelectedGFData.GFLevelMod * 10 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                a20 = KernelWorker.GetSelectedGFData.GFLevelMod * 20 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                a30 = KernelWorker.GetSelectedGFData.GFLevelMod * 30 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                a40 = KernelWorker.GetSelectedGFData.GFLevelMod * 40 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                a50 = KernelWorker.GetSelectedGFData.GFLevelMod * 50 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                a60 = KernelWorker.GetSelectedGFData.GFLevelMod * 60 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                a70 = KernelWorker.GetSelectedGFData.GFLevelMod * 70 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                a80 = KernelWorker.GetSelectedGFData.GFLevelMod * 80 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                a90 = KernelWorker.GetSelectedGFData.GFLevelMod * 90 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                a100 = KernelWorker.GetSelectedGFData.GFLevelMod * 100 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;

                b1 = a1 * (265 - (int)numericUpDownSPR.Value) / 8;
                b10 = a10 * (265 - (int)numericUpDownSPR.Value) / 8;
                b20 = a20 * (265 - (int)numericUpDownSPR.Value) / 8;
                b30 = a30 * (265 - (int)numericUpDownSPR.Value) / 8;
                b40 = a40 * (265 - (int)numericUpDownSPR.Value) / 8;
                b50 = a50 * (265 - (int)numericUpDownSPR.Value) / 8;
                b60 = a60 * (265 - (int)numericUpDownSPR.Value) / 8;
                b70 = a70 * (265 - (int)numericUpDownSPR.Value) / 8;
                b80 = a80 * (265 - (int)numericUpDownSPR.Value) / 8;
                b90 = a90 * (265 - (int)numericUpDownSPR.Value) / 8;
                b100 = a100 * (265 - (int)numericUpDownSPR.Value) / 8;

                c1 = b1 * KernelWorker.GetSelectedGFData.GFPower / 256;
                c10 = b10 * KernelWorker.GetSelectedGFData.GFPower / 256;
                c20 = b20 * KernelWorker.GetSelectedGFData.GFPower / 256;
                c30 = b30 * KernelWorker.GetSelectedGFData.GFPower / 256;
                c40 = b40 * KernelWorker.GetSelectedGFData.GFPower / 256;
                c50 = b50 * KernelWorker.GetSelectedGFData.GFPower / 256;
                c60 = b60 * KernelWorker.GetSelectedGFData.GFPower / 256;
                c70 = b70 * KernelWorker.GetSelectedGFData.GFPower / 256;
                c80 = b80 * KernelWorker.GetSelectedGFData.GFPower / 256;
                c90 = b90 * KernelWorker.GetSelectedGFData.GFPower / 256;
                c100 = b100 * KernelWorker.GetSelectedGFData.GFPower / 256;

                d1 = c1 * (int)numericUpDownBoost.Value / 100;
                d10 = c10 * (int)numericUpDownBoost.Value / 100;
                d20 = c20 * (int)numericUpDownBoost.Value / 100;
                d30 = c30 * (int)numericUpDownBoost.Value / 100;
                d40 = c40 * (int)numericUpDownBoost.Value / 100;
                d50 = c50 * (int)numericUpDownBoost.Value / 100;
                d60 = c60 * (int)numericUpDownBoost.Value / 100;
                d70 = c70 * (int)numericUpDownBoost.Value / 100;
                d80 = c80 * (int)numericUpDownBoost.Value / 100;
                d90 = c90 * (int)numericUpDownBoost.Value / 100;
                d100 = c100 * (int)numericUpDownBoost.Value / 100;

                e1 = d1 * (100 + (int)numericUpDownSumMag.Value) / 100;
                e10 = d10 * (100 + (int)numericUpDownSumMag.Value) / 100;
                e20 = d20 * (100 + (int)numericUpDownSumMag.Value) / 100;
                e30 = d30 * (100 + (int)numericUpDownSumMag.Value) / 100;
                e40 = d40 * (100 + (int)numericUpDownSumMag.Value) / 100;
                e50 = d50 * (100 + (int)numericUpDownSumMag.Value) / 100;
                e60 = d60 * (100 + (int)numericUpDownSumMag.Value) / 100;
                e70 = d70 * (100 + (int)numericUpDownSumMag.Value) / 100;
                e80 = d80 * (100 + (int)numericUpDownSumMag.Value) / 100;
                e90 = d90 * (100 + (int)numericUpDownSumMag.Value) / 100;
                e100 = d100 * (100 + (int)numericUpDownSumMag.Value) / 100;

                chartGfDamage.Series["Default"].Points.AddXY
                    (0, e1 * (900 - numericUpDownElemDef.Value) / 100);

                chartGfDamage.Series["Default"].Points.AddXY
                    (1, e10 * (900 - numericUpDownElemDef.Value) / 100);
                                                                               
                chartGfDamage.Series["Default"].Points.AddXY                    
                    (2, e20 * (900 - numericUpDownElemDef.Value) / 100);
                                                                                
                chartGfDamage.Series["Default"].Points.AddXY                    
                    (3, e30 * (900 - numericUpDownElemDef.Value) / 100);
                                                                                
                chartGfDamage.Series["Default"].Points.AddXY                    
                    (4, e40 * (900 - numericUpDownElemDef.Value) / 100);
                                                                                
                chartGfDamage.Series["Default"].Points.AddXY                    
                    (5, e50 * (900 - numericUpDownElemDef.Value) / 100);
                                                                                
                chartGfDamage.Series["Default"].Points.AddXY                    
                    (6, e60 * (900 - numericUpDownElemDef.Value) / 100);
                                                                                
                chartGfDamage.Series["Default"].Points.AddXY                    
                    (7, e70 * (900 - numericUpDownElemDef.Value) / 100);
                                                                                
                chartGfDamage.Series["Default"].Points.AddXY                    
                    (8, e80 * (900 - numericUpDownElemDef.Value) / 100);
                                                                                
                chartGfDamage.Series["Default"].Points.AddXY                    
                    (9, e90 * (900 - numericUpDownElemDef.Value) / 100);
                                                                       
                chartGfDamage.Series["Default"].Points.AddXY
                    (10, e100 * (900 - numericUpDownElemDef.Value) / 100);
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }


            KernelWorker.ReadGF(_mainForm.listBoxGF.SelectedIndex, KernelWorker.Kernel);
            try
            {
                a1 = KernelWorker.GetSelectedGFData.GFLevelMod * 1 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                a10 = KernelWorker.GetSelectedGFData.GFLevelMod * 10 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                a20 = KernelWorker.GetSelectedGFData.GFLevelMod * 20 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                a30 = KernelWorker.GetSelectedGFData.GFLevelMod * 30 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                a40 = KernelWorker.GetSelectedGFData.GFLevelMod * 40 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                a50 = KernelWorker.GetSelectedGFData.GFLevelMod * 50 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                a60 = KernelWorker.GetSelectedGFData.GFLevelMod * 60 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                a70 = KernelWorker.GetSelectedGFData.GFLevelMod * 70 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                a80 = KernelWorker.GetSelectedGFData.GFLevelMod * 80 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                a90 = KernelWorker.GetSelectedGFData.GFLevelMod * 90 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                a100 = KernelWorker.GetSelectedGFData.GFLevelMod * 100 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;

                b1 = a1 * (265 - (int)numericUpDownSPR.Value) / 8;
                b10 = a10 * (265 - (int)numericUpDownSPR.Value) / 8;
                b20 = a20 * (265 - (int)numericUpDownSPR.Value) / 8;
                b30 = a30 * (265 - (int)numericUpDownSPR.Value) / 8;
                b40 = a40 * (265 - (int)numericUpDownSPR.Value) / 8;
                b50 = a50 * (265 - (int)numericUpDownSPR.Value) / 8;
                b60 = a60 * (265 - (int)numericUpDownSPR.Value) / 8;
                b70 = a70 * (265 - (int)numericUpDownSPR.Value) / 8;
                b80 = a80 * (265 - (int)numericUpDownSPR.Value) / 8;
                b90 = a90 * (265 - (int)numericUpDownSPR.Value) / 8;
                b100 = a100 * (265 - (int)numericUpDownSPR.Value) / 8;

                c1 = b1 * KernelWorker.GetSelectedGFData.GFPower / 256;
                c10 = b10 * KernelWorker.GetSelectedGFData.GFPower / 256;
                c20 = b20 * KernelWorker.GetSelectedGFData.GFPower / 256;
                c30 = b30 * KernelWorker.GetSelectedGFData.GFPower / 256;
                c40 = b40 * KernelWorker.GetSelectedGFData.GFPower / 256;
                c50 = b50 * KernelWorker.GetSelectedGFData.GFPower / 256;
                c60 = b60 * KernelWorker.GetSelectedGFData.GFPower / 256;
                c70 = b70 * KernelWorker.GetSelectedGFData.GFPower / 256;
                c80 = b80 * KernelWorker.GetSelectedGFData.GFPower / 256;
                c90 = b90 * KernelWorker.GetSelectedGFData.GFPower / 256;
                c100 = b100 * KernelWorker.GetSelectedGFData.GFPower / 256;

                d1 = c1 * (int)numericUpDownBoost.Value / 100;
                d10 = c10 * (int)numericUpDownBoost.Value / 100;
                d20 = c20 * (int)numericUpDownBoost.Value / 100;
                d30 = c30 * (int)numericUpDownBoost.Value / 100;
                d40 = c40 * (int)numericUpDownBoost.Value / 100;
                d50 = c50 * (int)numericUpDownBoost.Value / 100;
                d60 = c60 * (int)numericUpDownBoost.Value / 100;
                d70 = c70 * (int)numericUpDownBoost.Value / 100;
                d80 = c80 * (int)numericUpDownBoost.Value / 100;
                d90 = c90 * (int)numericUpDownBoost.Value / 100;
                d100 = c100 * (int)numericUpDownBoost.Value / 100;

                e1 = d1 * (100 + (int)numericUpDownSumMag.Value) / 100;
                e10 = d10 * (100 + (int)numericUpDownSumMag.Value) / 100;
                e20 = d20 * (100 + (int)numericUpDownSumMag.Value) / 100;
                e30 = d30 * (100 + (int)numericUpDownSumMag.Value) / 100;
                e40 = d40 * (100 + (int)numericUpDownSumMag.Value) / 100;
                e50 = d50 * (100 + (int)numericUpDownSumMag.Value) / 100;
                e60 = d60 * (100 + (int)numericUpDownSumMag.Value) / 100;
                e70 = d70 * (100 + (int)numericUpDownSumMag.Value) / 100;
                e80 = d80 * (100 + (int)numericUpDownSumMag.Value) / 100;
                e90 = d90 * (100 + (int)numericUpDownSumMag.Value) / 100;
                e100 = d100 * (100 + (int)numericUpDownSumMag.Value) / 100;

                chartGfDamage.Series["Current"].Points.AddXY
                    (0, e1 * (900 - numericUpDownElemDef.Value) / 100);

                chartGfDamage.Series["Current"].Points.AddXY
                    (1, e10 * (900 - numericUpDownElemDef.Value) / 100);

                chartGfDamage.Series["Current"].Points.AddXY
                    (2, e20 * (900 - numericUpDownElemDef.Value) / 100);

                chartGfDamage.Series["Current"].Points.AddXY
                    (3, e30 * (900 - numericUpDownElemDef.Value) / 100);

                chartGfDamage.Series["Current"].Points.AddXY
                    (4, e40 * (900 - numericUpDownElemDef.Value) / 100);

                chartGfDamage.Series["Current"].Points.AddXY
                    (5, e50 * (900 - numericUpDownElemDef.Value) / 100);

                chartGfDamage.Series["Current"].Points.AddXY
                    (6, e60 * (900 - numericUpDownElemDef.Value) / 100);

                chartGfDamage.Series["Current"].Points.AddXY
                    (7, e70 * (900 - numericUpDownElemDef.Value) / 100);

                chartGfDamage.Series["Current"].Points.AddXY
                    (8, e80 * (900 - numericUpDownElemDef.Value) / 100);

                chartGfDamage.Series["Current"].Points.AddXY
                    (9, e90 * (900 - numericUpDownElemDef.Value) / 100);

                chartGfDamage.Series["Current"].Points.AddXY
                    (10, e100 * (900 - numericUpDownElemDef.Value) / 100);
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }
        }

        #endregion

        #region Change chart values

        private void numericUpDownGfDamage_ValueChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                KernelWorker.ReadGF(_mainForm.listBoxGF.SelectedIndex, KernelWorker.BackupKernel);
                try
                {
                    if (checkBoxDefault.Checked == true)
                    {
                        a1 = KernelWorker.GetSelectedGFData.GFLevelMod * 1 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                        a10 = KernelWorker.GetSelectedGFData.GFLevelMod * 10 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                        a20 = KernelWorker.GetSelectedGFData.GFLevelMod * 20 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                        a30 = KernelWorker.GetSelectedGFData.GFLevelMod * 30 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                        a40 = KernelWorker.GetSelectedGFData.GFLevelMod * 40 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                        a50 = KernelWorker.GetSelectedGFData.GFLevelMod * 50 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                        a60 = KernelWorker.GetSelectedGFData.GFLevelMod * 60 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                        a70 = KernelWorker.GetSelectedGFData.GFLevelMod * 70 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                        a80 = KernelWorker.GetSelectedGFData.GFLevelMod * 80 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                        a90 = KernelWorker.GetSelectedGFData.GFLevelMod * 90 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                        a100 = KernelWorker.GetSelectedGFData.GFLevelMod * 100 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;

                        b1 = a1 * (265 - (int)numericUpDownSPR.Value) / 8;
                        b10 = a10 * (265 - (int)numericUpDownSPR.Value) / 8;
                        b20 = a20 * (265 - (int)numericUpDownSPR.Value) / 8;
                        b30 = a30 * (265 - (int)numericUpDownSPR.Value) / 8;
                        b40 = a40 * (265 - (int)numericUpDownSPR.Value) / 8;
                        b50 = a50 * (265 - (int)numericUpDownSPR.Value) / 8;
                        b60 = a60 * (265 - (int)numericUpDownSPR.Value) / 8;
                        b70 = a70 * (265 - (int)numericUpDownSPR.Value) / 8;
                        b80 = a80 * (265 - (int)numericUpDownSPR.Value) / 8;
                        b90 = a90 * (265 - (int)numericUpDownSPR.Value) / 8;
                        b100 = a100 * (265 - (int)numericUpDownSPR.Value) / 8;

                        c1 = b1 * KernelWorker.GetSelectedGFData.GFPower / 256;
                        c10 = b10 * KernelWorker.GetSelectedGFData.GFPower / 256;
                        c20 = b20 * KernelWorker.GetSelectedGFData.GFPower / 256;
                        c30 = b30 * KernelWorker.GetSelectedGFData.GFPower / 256;
                        c40 = b40 * KernelWorker.GetSelectedGFData.GFPower / 256;
                        c50 = b50 * KernelWorker.GetSelectedGFData.GFPower / 256;
                        c60 = b60 * KernelWorker.GetSelectedGFData.GFPower / 256;
                        c70 = b70 * KernelWorker.GetSelectedGFData.GFPower / 256;
                        c80 = b80 * KernelWorker.GetSelectedGFData.GFPower / 256;
                        c90 = b90 * KernelWorker.GetSelectedGFData.GFPower / 256;
                        c100 = b100 * KernelWorker.GetSelectedGFData.GFPower / 256;

                        d1 = c1 * (int)numericUpDownBoost.Value / 100;
                        d10 = c10 * (int)numericUpDownBoost.Value / 100;
                        d20 = c20 * (int)numericUpDownBoost.Value / 100;
                        d30 = c30 * (int)numericUpDownBoost.Value / 100;
                        d40 = c40 * (int)numericUpDownBoost.Value / 100;
                        d50 = c50 * (int)numericUpDownBoost.Value / 100;
                        d60 = c60 * (int)numericUpDownBoost.Value / 100;
                        d70 = c70 * (int)numericUpDownBoost.Value / 100;
                        d80 = c80 * (int)numericUpDownBoost.Value / 100;
                        d90 = c90 * (int)numericUpDownBoost.Value / 100;
                        d100 = c100 * (int)numericUpDownBoost.Value / 100;

                        e1 = d1 * (100 + (int)numericUpDownSumMag.Value) / 100;
                        e10 = d10 * (100 + (int)numericUpDownSumMag.Value) / 100;
                        e20 = d20 * (100 + (int)numericUpDownSumMag.Value) / 100;
                        e30 = d30 * (100 + (int)numericUpDownSumMag.Value) / 100;
                        e40 = d40 * (100 + (int)numericUpDownSumMag.Value) / 100;
                        e50 = d50 * (100 + (int)numericUpDownSumMag.Value) / 100;
                        e60 = d60 * (100 + (int)numericUpDownSumMag.Value) / 100;
                        e70 = d70 * (100 + (int)numericUpDownSumMag.Value) / 100;
                        e80 = d80 * (100 + (int)numericUpDownSumMag.Value) / 100;
                        e90 = d90 * (100 + (int)numericUpDownSumMag.Value) / 100;
                        e100 = d100 * (100 + (int)numericUpDownSumMag.Value) / 100;


                        chartGfDamage.Series["Default"].Points.Clear();

                        chartGfDamage.Series["Default"].Points.AddXY
                            (0, e1 * (900 - numericUpDownElemDef.Value) / 100);

                        chartGfDamage.Series["Default"].Points.AddXY
                            (1, e10 * (900 - numericUpDownElemDef.Value) / 100);

                        chartGfDamage.Series["Default"].Points.AddXY
                            (2, e20 * (900 - numericUpDownElemDef.Value) / 100);

                        chartGfDamage.Series["Default"].Points.AddXY
                            (3, e30 * (900 - numericUpDownElemDef.Value) / 100);

                        chartGfDamage.Series["Default"].Points.AddXY
                            (4, e40 * (900 - numericUpDownElemDef.Value) / 100);

                        chartGfDamage.Series["Default"].Points.AddXY
                            (5, e50 * (900 - numericUpDownElemDef.Value) / 100);

                        chartGfDamage.Series["Default"].Points.AddXY
                            (6, e60 * (900 - numericUpDownElemDef.Value) / 100);

                        chartGfDamage.Series["Default"].Points.AddXY
                            (7, e70 * (900 - numericUpDownElemDef.Value) / 100);

                        chartGfDamage.Series["Default"].Points.AddXY
                            (8, e80 * (900 - numericUpDownElemDef.Value) / 100);

                        chartGfDamage.Series["Default"].Points.AddXY
                            (9, e90 * (900 - numericUpDownElemDef.Value) / 100);

                        chartGfDamage.Series["Default"].Points.AddXY
                            (10, e100 * (900 - numericUpDownElemDef.Value) / 100);
                    }

                    else if (checkBoxDefault.Checked == false)
                    {
                        a1 = KernelWorker.GetSelectedGFData.GFLevelMod * 1 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                        a10 = KernelWorker.GetSelectedGFData.GFLevelMod * 10 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                        a20 = KernelWorker.GetSelectedGFData.GFLevelMod * 20 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                        a30 = KernelWorker.GetSelectedGFData.GFLevelMod * 30 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                        a40 = KernelWorker.GetSelectedGFData.GFLevelMod * 40 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                        a50 = KernelWorker.GetSelectedGFData.GFLevelMod * 50 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                        a60 = KernelWorker.GetSelectedGFData.GFLevelMod * 60 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                        a70 = KernelWorker.GetSelectedGFData.GFLevelMod * 70 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                        a80 = KernelWorker.GetSelectedGFData.GFLevelMod * 80 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                        a90 = KernelWorker.GetSelectedGFData.GFLevelMod * 90 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                        a100 = KernelWorker.GetSelectedGFData.GFLevelMod * 100 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;

                        b1 = a1 * (265 - 0) / 8;
                        b10 = a10 * (265 - 0) / 8;
                        b20 = a20 * (265 - 0) / 8;
                        b30 = a30 * (265 - 0) / 8;
                        b40 = a40 * (265 - 0) / 8;
                        b50 = a50 * (265 - 0) / 8;
                        b60 = a60 * (265 - 0) / 8;
                        b70 = a70 * (265 - 0) / 8;
                        b80 = a80 * (265 - 0) / 8;
                        b90 = a90 * (265 - 0) / 8;
                        b100 = a100 * (265 - 0) / 8;

                        c1 = b1 * KernelWorker.GetSelectedGFData.GFPower / 256;
                        c10 = b10 * KernelWorker.GetSelectedGFData.GFPower / 256;
                        c20 = b20 * KernelWorker.GetSelectedGFData.GFPower / 256;
                        c30 = b30 * KernelWorker.GetSelectedGFData.GFPower / 256;
                        c40 = b40 * KernelWorker.GetSelectedGFData.GFPower / 256;
                        c50 = b50 * KernelWorker.GetSelectedGFData.GFPower / 256;
                        c60 = b60 * KernelWorker.GetSelectedGFData.GFPower / 256;
                        c70 = b70 * KernelWorker.GetSelectedGFData.GFPower / 256;
                        c80 = b80 * KernelWorker.GetSelectedGFData.GFPower / 256;
                        c90 = b90 * KernelWorker.GetSelectedGFData.GFPower / 256;
                        c100 = b100 * KernelWorker.GetSelectedGFData.GFPower / 256;

                        d1 = c1 * 100 / 100;
                        d10 = c10 * 100 / 100;
                        d20 = c20 * 100 / 100;
                        d30 = c30 * 100 / 100;
                        d40 = c40 * 100 / 100;
                        d50 = c50 * 100 / 100;
                        d60 = c60 * 100 / 100;
                        d70 = c70 * 100 / 100;
                        d80 = c80 * 100 / 100;
                        d90 = c90 * 100 / 100;
                        d100 = c100 * 100 / 100;

                        e1 = d1 * (100 + 0) / 100;
                        e10 = d10 * (100 + 0) / 100;
                        e20 = d20 * (100 + 0) / 100;
                        e30 = d30 * (100 + 0) / 100;
                        e40 = d40 * (100 + 0) / 100;
                        e50 = d50 * (100 + 0) / 100;
                        e60 = d60 * (100 + 0) / 100;
                        e70 = d70 * (100 + 0) / 100;
                        e80 = d80 * (100 + 0) / 100;
                        e90 = d90 * (100 + 0) / 100;
                        e100 = d100 * (100 + 0) / 100;


                        chartGfDamage.Series["Default"].Points.Clear();

                        chartGfDamage.Series["Default"].Points.AddXY
                            (0, e1 * (900 - 800) / 100);

                        chartGfDamage.Series["Default"].Points.AddXY
                            (1, e10 * (900 - 800) / 100);

                        chartGfDamage.Series["Default"].Points.AddXY
                            (2, e20 * (900 - 800) / 100);

                        chartGfDamage.Series["Default"].Points.AddXY
                            (3, e30 * (900 - 800) / 100);

                        chartGfDamage.Series["Default"].Points.AddXY
                            (4, e40 * (900 - 800) / 100);

                        chartGfDamage.Series["Default"].Points.AddXY
                            (5, e50 * (900 - 800) / 100);

                        chartGfDamage.Series["Default"].Points.AddXY
                            (6, e60 * (900 - 800) / 100);

                        chartGfDamage.Series["Default"].Points.AddXY
                            (7, e70 * (900 - 800) / 100);

                        chartGfDamage.Series["Default"].Points.AddXY
                            (8, e80 * (900 - 800) / 100);

                        chartGfDamage.Series["Default"].Points.AddXY
                            (9, e90 * (900 - 800) / 100);

                        chartGfDamage.Series["Default"].Points.AddXY
                            (10, e100 * (900 - 800) / 100);
                    }
                }
                catch (Exception Exception)
                {
                    MessageBox.Show(Exception.ToString());
                }


                KernelWorker.ReadGF(_mainForm.listBoxGF.SelectedIndex, KernelWorker.Kernel);
                try
                {
                    a1 = KernelWorker.GetSelectedGFData.GFLevelMod * 1 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                    a10 = KernelWorker.GetSelectedGFData.GFLevelMod * 10 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                    a20 = KernelWorker.GetSelectedGFData.GFLevelMod * 20 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                    a30 = KernelWorker.GetSelectedGFData.GFLevelMod * 30 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                    a40 = KernelWorker.GetSelectedGFData.GFLevelMod * 40 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                    a50 = KernelWorker.GetSelectedGFData.GFLevelMod * 50 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                    a60 = KernelWorker.GetSelectedGFData.GFLevelMod * 60 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                    a70 = KernelWorker.GetSelectedGFData.GFLevelMod * 70 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                    a80 = KernelWorker.GetSelectedGFData.GFLevelMod * 80 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                    a90 = KernelWorker.GetSelectedGFData.GFLevelMod * 90 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                    a100 = KernelWorker.GetSelectedGFData.GFLevelMod * 100 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;

                    b1 = a1 * (265 - (int)numericUpDownSPR.Value) / 8;
                    b10 = a10 * (265 - (int)numericUpDownSPR.Value) / 8;
                    b20 = a20 * (265 - (int)numericUpDownSPR.Value) / 8;
                    b30 = a30 * (265 - (int)numericUpDownSPR.Value) / 8;
                    b40 = a40 * (265 - (int)numericUpDownSPR.Value) / 8;
                    b50 = a50 * (265 - (int)numericUpDownSPR.Value) / 8;
                    b60 = a60 * (265 - (int)numericUpDownSPR.Value) / 8;
                    b70 = a70 * (265 - (int)numericUpDownSPR.Value) / 8;
                    b80 = a80 * (265 - (int)numericUpDownSPR.Value) / 8;
                    b90 = a90 * (265 - (int)numericUpDownSPR.Value) / 8;
                    b100 = a100 * (265 - (int)numericUpDownSPR.Value) / 8;

                    c1 = b1 * KernelWorker.GetSelectedGFData.GFPower / 256;
                    c10 = b10 * KernelWorker.GetSelectedGFData.GFPower / 256;
                    c20 = b20 * KernelWorker.GetSelectedGFData.GFPower / 256;
                    c30 = b30 * KernelWorker.GetSelectedGFData.GFPower / 256;
                    c40 = b40 * KernelWorker.GetSelectedGFData.GFPower / 256;
                    c50 = b50 * KernelWorker.GetSelectedGFData.GFPower / 256;
                    c60 = b60 * KernelWorker.GetSelectedGFData.GFPower / 256;
                    c70 = b70 * KernelWorker.GetSelectedGFData.GFPower / 256;
                    c80 = b80 * KernelWorker.GetSelectedGFData.GFPower / 256;
                    c90 = b90 * KernelWorker.GetSelectedGFData.GFPower / 256;
                    c100 = b100 * KernelWorker.GetSelectedGFData.GFPower / 256;

                    d1 = c1 * (int)numericUpDownBoost.Value / 100;
                    d10 = c10 * (int)numericUpDownBoost.Value / 100;
                    d20 = c20 * (int)numericUpDownBoost.Value / 100;
                    d30 = c30 * (int)numericUpDownBoost.Value / 100;
                    d40 = c40 * (int)numericUpDownBoost.Value / 100;
                    d50 = c50 * (int)numericUpDownBoost.Value / 100;
                    d60 = c60 * (int)numericUpDownBoost.Value / 100;
                    d70 = c70 * (int)numericUpDownBoost.Value / 100;
                    d80 = c80 * (int)numericUpDownBoost.Value / 100;
                    d90 = c90 * (int)numericUpDownBoost.Value / 100;
                    d100 = c100 * (int)numericUpDownBoost.Value / 100;

                    e1 = d1 * (100 + (int)numericUpDownSumMag.Value) / 100;
                    e10 = d10 * (100 + (int)numericUpDownSumMag.Value) / 100;
                    e20 = d20 * (100 + (int)numericUpDownSumMag.Value) / 100;
                    e30 = d30 * (100 + (int)numericUpDownSumMag.Value) / 100;
                    e40 = d40 * (100 + (int)numericUpDownSumMag.Value) / 100;
                    e50 = d50 * (100 + (int)numericUpDownSumMag.Value) / 100;
                    e60 = d60 * (100 + (int)numericUpDownSumMag.Value) / 100;
                    e70 = d70 * (100 + (int)numericUpDownSumMag.Value) / 100;
                    e80 = d80 * (100 + (int)numericUpDownSumMag.Value) / 100;
                    e90 = d90 * (100 + (int)numericUpDownSumMag.Value) / 100;
                    e100 = d100 * (100 + (int)numericUpDownSumMag.Value) / 100;


                    chartGfDamage.Series["Current"].Points.Clear();

                    chartGfDamage.Series["Current"].Points.AddXY
                        (0, e1 * (900 - numericUpDownElemDef.Value) / 100);

                    chartGfDamage.Series["Current"].Points.AddXY
                        (1, e10 * (900 - numericUpDownElemDef.Value) / 100);

                    chartGfDamage.Series["Current"].Points.AddXY
                        (2, e20 * (900 - numericUpDownElemDef.Value) / 100);

                    chartGfDamage.Series["Current"].Points.AddXY
                        (3, e30 * (900 - numericUpDownElemDef.Value) / 100);

                    chartGfDamage.Series["Current"].Points.AddXY
                        (4, e40 * (900 - numericUpDownElemDef.Value) / 100);

                    chartGfDamage.Series["Current"].Points.AddXY
                        (5, e50 * (900 - numericUpDownElemDef.Value) / 100);

                    chartGfDamage.Series["Current"].Points.AddXY
                        (6, e60 * (900 - numericUpDownElemDef.Value) / 100);

                    chartGfDamage.Series["Current"].Points.AddXY
                        (7, e70 * (900 - numericUpDownElemDef.Value) / 100);

                    chartGfDamage.Series["Current"].Points.AddXY
                        (8, e80 * (900 - numericUpDownElemDef.Value) / 100);

                    chartGfDamage.Series["Current"].Points.AddXY
                        (9, e90 * (900 - numericUpDownElemDef.Value) / 100);

                    chartGfDamage.Series["Current"].Points.AddXY
                        (10, e100 * (900 - numericUpDownElemDef.Value) / 100);
                }
                catch (Exception Exception)
                {
                    MessageBox.Show(Exception.ToString());
                }
            }
        }

        #endregion

        #region Default checked changed

        private void Default_CheckedChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                KernelWorker.ReadGF(_mainForm.listBoxGF.SelectedIndex, KernelWorker.BackupKernel);
                try
                {
                    if (checkBoxDefault.Checked == true)
                    {
                        a1 = KernelWorker.GetSelectedGFData.GFLevelMod * 1 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                        a10 = KernelWorker.GetSelectedGFData.GFLevelMod * 10 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                        a20 = KernelWorker.GetSelectedGFData.GFLevelMod * 20 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                        a30 = KernelWorker.GetSelectedGFData.GFLevelMod * 30 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                        a40 = KernelWorker.GetSelectedGFData.GFLevelMod * 40 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                        a50 = KernelWorker.GetSelectedGFData.GFLevelMod * 50 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                        a60 = KernelWorker.GetSelectedGFData.GFLevelMod * 60 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                        a70 = KernelWorker.GetSelectedGFData.GFLevelMod * 70 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                        a80 = KernelWorker.GetSelectedGFData.GFLevelMod * 80 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                        a90 = KernelWorker.GetSelectedGFData.GFLevelMod * 90 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                        a100 = KernelWorker.GetSelectedGFData.GFLevelMod * 100 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;

                        b1 = a1 * (265 - (int)numericUpDownSPR.Value) / 8;
                        b10 = a10 * (265 - (int)numericUpDownSPR.Value) / 8;
                        b20 = a20 * (265 - (int)numericUpDownSPR.Value) / 8;
                        b30 = a30 * (265 - (int)numericUpDownSPR.Value) / 8;
                        b40 = a40 * (265 - (int)numericUpDownSPR.Value) / 8;
                        b50 = a50 * (265 - (int)numericUpDownSPR.Value) / 8;
                        b60 = a60 * (265 - (int)numericUpDownSPR.Value) / 8;
                        b70 = a70 * (265 - (int)numericUpDownSPR.Value) / 8;
                        b80 = a80 * (265 - (int)numericUpDownSPR.Value) / 8;
                        b90 = a90 * (265 - (int)numericUpDownSPR.Value) / 8;
                        b100 = a100 * (265 - (int)numericUpDownSPR.Value) / 8;

                        c1 = b1 * KernelWorker.GetSelectedGFData.GFPower / 256;
                        c10 = b10 * KernelWorker.GetSelectedGFData.GFPower / 256;
                        c20 = b20 * KernelWorker.GetSelectedGFData.GFPower / 256;
                        c30 = b30 * KernelWorker.GetSelectedGFData.GFPower / 256;
                        c40 = b40 * KernelWorker.GetSelectedGFData.GFPower / 256;
                        c50 = b50 * KernelWorker.GetSelectedGFData.GFPower / 256;
                        c60 = b60 * KernelWorker.GetSelectedGFData.GFPower / 256;
                        c70 = b70 * KernelWorker.GetSelectedGFData.GFPower / 256;
                        c80 = b80 * KernelWorker.GetSelectedGFData.GFPower / 256;
                        c90 = b90 * KernelWorker.GetSelectedGFData.GFPower / 256;
                        c100 = b100 * KernelWorker.GetSelectedGFData.GFPower / 256;

                        d1 = c1 * (int)numericUpDownBoost.Value / 100;
                        d10 = c10 * (int)numericUpDownBoost.Value / 100;
                        d20 = c20 * (int)numericUpDownBoost.Value / 100;
                        d30 = c30 * (int)numericUpDownBoost.Value / 100;
                        d40 = c40 * (int)numericUpDownBoost.Value / 100;
                        d50 = c50 * (int)numericUpDownBoost.Value / 100;
                        d60 = c60 * (int)numericUpDownBoost.Value / 100;
                        d70 = c70 * (int)numericUpDownBoost.Value / 100;
                        d80 = c80 * (int)numericUpDownBoost.Value / 100;
                        d90 = c90 * (int)numericUpDownBoost.Value / 100;
                        d100 = c100 * (int)numericUpDownBoost.Value / 100;

                        e1 = d1 * (100 + (int)numericUpDownSumMag.Value) / 100;
                        e10 = d10 * (100 + (int)numericUpDownSumMag.Value) / 100;
                        e20 = d20 * (100 + (int)numericUpDownSumMag.Value) / 100;
                        e30 = d30 * (100 + (int)numericUpDownSumMag.Value) / 100;
                        e40 = d40 * (100 + (int)numericUpDownSumMag.Value) / 100;
                        e50 = d50 * (100 + (int)numericUpDownSumMag.Value) / 100;
                        e60 = d60 * (100 + (int)numericUpDownSumMag.Value) / 100;
                        e70 = d70 * (100 + (int)numericUpDownSumMag.Value) / 100;
                        e80 = d80 * (100 + (int)numericUpDownSumMag.Value) / 100;
                        e90 = d90 * (100 + (int)numericUpDownSumMag.Value) / 100;
                        e100 = d100 * (100 + (int)numericUpDownSumMag.Value) / 100;


                        chartGfDamage.Series["Default"].Points.Clear();

                        chartGfDamage.Series["Default"].Points.AddXY
                            (0, e1 * (900 - numericUpDownElemDef.Value) / 100);

                        chartGfDamage.Series["Default"].Points.AddXY
                            (1, e10 * (900 - numericUpDownElemDef.Value) / 100);

                        chartGfDamage.Series["Default"].Points.AddXY
                            (2, e20 * (900 - numericUpDownElemDef.Value) / 100);

                        chartGfDamage.Series["Default"].Points.AddXY
                            (3, e30 * (900 - numericUpDownElemDef.Value) / 100);

                        chartGfDamage.Series["Default"].Points.AddXY
                            (4, e40 * (900 - numericUpDownElemDef.Value) / 100);

                        chartGfDamage.Series["Default"].Points.AddXY
                            (5, e50 * (900 - numericUpDownElemDef.Value) / 100);

                        chartGfDamage.Series["Default"].Points.AddXY
                            (6, e60 * (900 - numericUpDownElemDef.Value) / 100);

                        chartGfDamage.Series["Default"].Points.AddXY
                            (7, e70 * (900 - numericUpDownElemDef.Value) / 100);

                        chartGfDamage.Series["Default"].Points.AddXY
                            (8, e80 * (900 - numericUpDownElemDef.Value) / 100);

                        chartGfDamage.Series["Default"].Points.AddXY
                            (9, e90 * (900 - numericUpDownElemDef.Value) / 100);

                        chartGfDamage.Series["Default"].Points.AddXY
                            (10, e100 * (900 - numericUpDownElemDef.Value) / 100);
                    }
                    else if (checkBoxDefault.Checked == false)
                    {
                        a1 = KernelWorker.GetSelectedGFData.GFLevelMod * 1 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                        a10 = KernelWorker.GetSelectedGFData.GFLevelMod * 10 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                        a20 = KernelWorker.GetSelectedGFData.GFLevelMod * 20 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                        a30 = KernelWorker.GetSelectedGFData.GFLevelMod * 30 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                        a40 = KernelWorker.GetSelectedGFData.GFLevelMod * 40 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                        a50 = KernelWorker.GetSelectedGFData.GFLevelMod * 50 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                        a60 = KernelWorker.GetSelectedGFData.GFLevelMod * 60 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                        a70 = KernelWorker.GetSelectedGFData.GFLevelMod * 70 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                        a80 = KernelWorker.GetSelectedGFData.GFLevelMod * 80 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                        a90 = KernelWorker.GetSelectedGFData.GFLevelMod * 90 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;
                        a100 = KernelWorker.GetSelectedGFData.GFLevelMod * 100 / 10 + KernelWorker.GetSelectedGFData.GFPower + KernelWorker.GetSelectedGFData.GFPowerMod;

                        b1 = a1 * (265 - 0) / 8;
                        b10 = a10 * (265 - 0) / 8;
                        b20 = a20 * (265 - 0) / 8;
                        b30 = a30 * (265 - 0) / 8;
                        b40 = a40 * (265 - 0) / 8;
                        b50 = a50 * (265 - 0) / 8;
                        b60 = a60 * (265 - 0) / 8;
                        b70 = a70 * (265 - 0) / 8;
                        b80 = a80 * (265 - 0) / 8;
                        b90 = a90 * (265 - 0) / 8;
                        b100 = a100 * (265 - 0) / 8;

                        c1 = b1 * KernelWorker.GetSelectedGFData.GFPower / 256;
                        c10 = b10 * KernelWorker.GetSelectedGFData.GFPower / 256;
                        c20 = b20 * KernelWorker.GetSelectedGFData.GFPower / 256;
                        c30 = b30 * KernelWorker.GetSelectedGFData.GFPower / 256;
                        c40 = b40 * KernelWorker.GetSelectedGFData.GFPower / 256;
                        c50 = b50 * KernelWorker.GetSelectedGFData.GFPower / 256;
                        c60 = b60 * KernelWorker.GetSelectedGFData.GFPower / 256;
                        c70 = b70 * KernelWorker.GetSelectedGFData.GFPower / 256;
                        c80 = b80 * KernelWorker.GetSelectedGFData.GFPower / 256;
                        c90 = b90 * KernelWorker.GetSelectedGFData.GFPower / 256;
                        c100 = b100 * KernelWorker.GetSelectedGFData.GFPower / 256;

                        d1 = c1 * 100 / 100;
                        d10 = c10 * 100 / 100;
                        d20 = c20 * 100 / 100;
                        d30 = c30 * 100 / 100;
                        d40 = c40 * 100 / 100;
                        d50 = c50 * 100 / 100;
                        d60 = c60 * 100 / 100;
                        d70 = c70 * 100 / 100;
                        d80 = c80 * 100 / 100;
                        d90 = c90 * 100 / 100;
                        d100 = c100 * 100 / 100;

                        e1 = d1 * (100 + 0) / 100;
                        e10 = d10 * (100 + 0) / 100;
                        e20 = d20 * (100 + 0) / 100;
                        e30 = d30 * (100 + 0) / 100;
                        e40 = d40 * (100 + 0) / 100;
                        e50 = d50 * (100 + 0) / 100;
                        e60 = d60 * (100 + 0) / 100;
                        e70 = d70 * (100 + 0) / 100;
                        e80 = d80 * (100 + 0) / 100;
                        e90 = d90 * (100 + 0) / 100;
                        e100 = d100 * (100 + 0) / 100;


                        chartGfDamage.Series["Default"].Points.Clear();

                        chartGfDamage.Series["Default"].Points.AddXY
                            (0, e1 * (900 - 800) / 100);

                        chartGfDamage.Series["Default"].Points.AddXY
                            (1, e10 * (900 - 800) / 100);

                        chartGfDamage.Series["Default"].Points.AddXY
                            (2, e20 * (900 - 800) / 100);

                        chartGfDamage.Series["Default"].Points.AddXY
                            (3, e30 * (900 - 800) / 100);

                        chartGfDamage.Series["Default"].Points.AddXY
                            (4, e40 * (900 - 800) / 100);

                        chartGfDamage.Series["Default"].Points.AddXY
                            (5, e50 * (900 - 800) / 100);

                        chartGfDamage.Series["Default"].Points.AddXY
                            (6, e60 * (900 - 800) / 100);

                        chartGfDamage.Series["Default"].Points.AddXY
                            (7, e70 * (900 - 800) / 100);

                        chartGfDamage.Series["Default"].Points.AddXY
                            (8, e80 * (900 - 800) / 100);

                        chartGfDamage.Series["Default"].Points.AddXY
                            (9, e90 * (900 - 800) / 100);

                        chartGfDamage.Series["Default"].Points.AddXY
                            (10, e100 * (900 - 800) / 100);
                    }
                }
                catch (Exception Exception)
                {
                    MessageBox.Show(Exception.ToString());
                }
            }
        }

        #endregion
    }
}
