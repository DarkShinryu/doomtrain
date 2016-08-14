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

            #region Disable objects

            labelSPR.Visible = false;
            labelBoost.Visible = false;
            labelElemDef.Visible = false;
            labelSumMag.Visible = false;
            labelHP.Visible = false;
            numericUpDownSPR.Visible = false;
            numericUpDownBoost.Visible = false;
            numericUpDownSumMag.Visible = false;
            numericUpDownElemDef.Visible = false;
            numericUpDownHP.Visible = false;
            checkBoxDefault.Visible = false;

            #endregion

            #region Event handlers

            _mainForm.numericUpDownGFPower.ValueChanged += new EventHandler(this.numericUpDownGfDamage_ValueChanged);
            _mainForm.numericUpDownGFPowerMod.ValueChanged += new EventHandler(this.numericUpDownGfDamage_ValueChanged);
            _mainForm.numericUpDownGFLevelMod.ValueChanged += new EventHandler(this.numericUpDownGfDamage_ValueChanged);
            _mainForm.comboBoxGFAttackType.SelectedIndexChanged += new EventHandler(this.numericUpDownGfDamage_ValueChanged);
            numericUpDownSPR.ValueChanged += new EventHandler(numericUpDownGfDamage_ValueChanged);
            numericUpDownBoost.ValueChanged += new EventHandler(numericUpDownGfDamage_ValueChanged);
            numericUpDownSumMag.ValueChanged += new EventHandler(numericUpDownGfDamage_ValueChanged);
            numericUpDownElemDef.ValueChanged += new EventHandler(numericUpDownGfDamage_ValueChanged);
            numericUpDownHP.ValueChanged += new EventHandler(numericUpDownGfDamage_ValueChanged);
            checkBoxDefault.CheckedChanged += new EventHandler(Default_CheckedChanged);

            #endregion

            DefaultChartWorker();
            CurrentChartWorker();
        }
        private readonly mainForm _mainForm;


        #region Movable chart

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

        #region Buttons

        private void buttonInfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The values here are only used for the chart and they have no connection to the kernel file. " + 
                "Also ignore the chart for attack type Diablos and Cactuar, they use  two different formulas.\n" +
                "You need to change the values manually.\n\n" +
                "Target SPR = The SPR value of the target.\n" +
                "Boost = GF boost, when not used is 100.\n" +
                "SumMagBonus = The % of Sum Mag Bonus.\n" + 
                "Elem Defense = The elemental defense of the target.\n" + 
                "Target Max HP = The max HP value of the target.", "Info",
                MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion


        #region Formulas variables declaration

        private int a1, a10, a20, a30, a40, a50, a60, a70, a80, a90, a100; //a = first calculation
        private int b1, b10, b20, b30, b40, b50, b60, b70, b80, b90, b100; //b = second calculation
        private int c1, c10, c20, c30, c40, c50, c60, c70, c80, c90, c100; //c = third calculation
        private int d1, d10, d20, d30, d40, d50, d60, d70, d80, d90, d100; //d = fourth calculation
        private int e1, e10, e20, e30, e40, e50, e60, e70, e80, e90, e100; //e = fifth calculation

        #endregion

        #region Read/Write values

        private void DefaultChartWorker()
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


                if (_mainForm.comboBoxGFAttackType.SelectedIndex == 11) //Attack type "GF"
                {
                    labelSPR.Visible = true;
                    labelBoost.Visible = true;
                    labelElemDef.Visible = true;
                    labelSumMag.Visible = true;
                    labelHP.Visible = false;
                    numericUpDownSPR.Visible = true;
                    numericUpDownBoost.Visible = true;
                    numericUpDownSumMag.Visible = true;
                    numericUpDownElemDef.Visible = true;
                    numericUpDownHP.Visible = false;
                    checkBoxDefault.Visible = true;

                    chartGfDamage.Series["Default"].Points.Clear();
                    chartGfDamage.Series["Default"].Points.AddXY
                        (0, Math.Floor(e1 * (900 - numericUpDownElemDef.Value) / 100));
                    chartGfDamage.Series["Default"].Points.AddXY
                        (1, Math.Floor(e10 * (900 - numericUpDownElemDef.Value) / 100));
                    chartGfDamage.Series["Default"].Points.AddXY
                        (2, Math.Floor(e20 * (900 - numericUpDownElemDef.Value) / 100));
                    chartGfDamage.Series["Default"].Points.AddXY
                        (3, Math.Floor(e30 * (900 - numericUpDownElemDef.Value) / 100));                
                    chartGfDamage.Series["Default"].Points.AddXY
                        (4, Math.Floor(e40 * (900 - numericUpDownElemDef.Value) / 100));
                    chartGfDamage.Series["Default"].Points.AddXY
                        (5, Math.Floor(e50 * (900 - numericUpDownElemDef.Value) / 100));
                    chartGfDamage.Series["Default"].Points.AddXY
                        (6, Math.Floor(e60 * (900 - numericUpDownElemDef.Value) / 100));
                    chartGfDamage.Series["Default"].Points.AddXY
                        (7, Math.Floor(e70 * (900 - numericUpDownElemDef.Value) / 100));
                    chartGfDamage.Series["Default"].Points.AddXY
                        (8, Math.Floor(e80 * (900 - numericUpDownElemDef.Value) / 100));
                    chartGfDamage.Series["Default"].Points.AddXY
                        (9, Math.Floor(e90 * (900 - numericUpDownElemDef.Value) / 100));
                    chartGfDamage.Series["Default"].Points.AddXY
                        (10, Math.Floor(e100 * (900 - numericUpDownElemDef.Value) / 100));
                }
                else if (_mainForm.comboBoxGFAttackType.SelectedIndex == 20) //Attack type "Diablos"
                {
                    labelSPR.Visible = false;
                    labelBoost.Visible = false;
                    labelElemDef.Visible = false;
                    labelSumMag.Visible = false;
                    labelHP.Visible = true;
                    numericUpDownSPR.Visible = false;
                    numericUpDownBoost.Visible = false;
                    numericUpDownSumMag.Visible = false;
                    numericUpDownElemDef.Visible = false;
                    numericUpDownHP.Visible = true;
                    checkBoxDefault.Visible = false;

                    chartGfDamage.Series["Default"].Points.Clear();
                    chartGfDamage.Series["Default"].Points.AddXY
                        (0, Math.Floor(numericUpDownHP.Value * 1 / 100));
                    chartGfDamage.Series["Default"].Points.AddXY
                        (1, Math.Floor(numericUpDownHP.Value * 10 / 100));
                    chartGfDamage.Series["Default"].Points.AddXY
                        (2, Math.Floor(numericUpDownHP.Value * 20 / 100));
                    chartGfDamage.Series["Default"].Points.AddXY
                        (3, Math.Floor(numericUpDownHP.Value * 30 / 100));                    
                    chartGfDamage.Series["Default"].Points.AddXY
                        (4, Math.Floor(numericUpDownHP.Value * 40 / 100));
                    chartGfDamage.Series["Default"].Points.AddXY
                        (5, Math.Floor(numericUpDownHP.Value * 50 / 100));
                    chartGfDamage.Series["Default"].Points.AddXY
                        (6, Math.Floor(numericUpDownHP.Value * 60 / 100));                
                    chartGfDamage.Series["Default"].Points.AddXY
                        (7, Math.Floor(numericUpDownHP.Value * 70 / 100));
                    chartGfDamage.Series["Default"].Points.AddXY
                        (8, Math.Floor(numericUpDownHP.Value * 80 / 100));
                    chartGfDamage.Series["Default"].Points.AddXY
                        (9, Math.Floor(numericUpDownHP.Value * 90 / 100));
                    chartGfDamage.Series["Default"].Points.AddXY
                        (10, Math.Floor(numericUpDownHP.Value * 100 / 100));
                }
                else if (_mainForm.comboBoxGFAttackType.SelectedIndex == 29) //Attack type "Cactuar"
                {
                    labelSPR.Visible = false;
                    labelBoost.Visible = false;
                    labelElemDef.Visible = false;
                    labelSumMag.Visible = false;
                    labelHP.Visible = false;
                    numericUpDownSPR.Visible = false;
                    numericUpDownBoost.Visible = false;
                    numericUpDownSumMag.Visible = false;
                    numericUpDownElemDef.Visible = false;
                    numericUpDownHP.Visible = false;
                    checkBoxDefault.Visible = false;

                    chartGfDamage.Series["Default"].Points.Clear();
                    chartGfDamage.Series["Default"].Points.AddXY
                        (0, (((1 * KernelWorker.GetSelectedGFData.GFPower) / 1000) + 1) * 1000);
                    chartGfDamage.Series["Default"].Points.AddXY
                        (1, (((10 * KernelWorker.GetSelectedGFData.GFPower) / 1000) + 1) * 1000);
                    chartGfDamage.Series["Default"].Points.AddXY
                        (2, (((20 * KernelWorker.GetSelectedGFData.GFPower) / 1000) + 1) * 1000);                
                    chartGfDamage.Series["Default"].Points.AddXY
                        (3, (((30 * KernelWorker.GetSelectedGFData.GFPower) / 1000) + 1) * 1000);
                    chartGfDamage.Series["Default"].Points.AddXY
                        (4, (((40 * KernelWorker.GetSelectedGFData.GFPower) / 1000) + 1) * 1000);
                    chartGfDamage.Series["Default"].Points.AddXY
                        (5, (((50 * KernelWorker.GetSelectedGFData.GFPower) / 1000) + 1) * 1000);
                    chartGfDamage.Series["Default"].Points.AddXY
                        (6, (((60 * KernelWorker.GetSelectedGFData.GFPower) / 1000) + 1) * 1000);
                    chartGfDamage.Series["Default"].Points.AddXY
                        (7, (((70 * KernelWorker.GetSelectedGFData.GFPower) / 1000) + 1) * 1000);
                    chartGfDamage.Series["Default"].Points.AddXY
                        (8, (((80 * KernelWorker.GetSelectedGFData.GFPower) / 1000) + 1) * 1000);
                    chartGfDamage.Series["Default"].Points.AddXY
                        (9, (((90 * KernelWorker.GetSelectedGFData.GFPower) / 1000) + 1) * 1000);
                    chartGfDamage.Series["Default"].Points.AddXY
                        (10, (((100 * KernelWorker.GetSelectedGFData.GFPower) / 1000) + 1) * 1000);
                }
                else if (_mainForm.comboBoxGFAttackType.SelectedIndex == 15) //Attack type "Ignore SPR"
                {
                    b1 = a1 * 265 / 8;
                    b10 = a10 * 265 / 8;
                    b20 = a20 * 265 / 8;
                    b30 = a30 * 265 / 8;
                    b40 = a40 * 265 / 8;
                    b50 = a50 * 265 / 8;
                    b60 = a60 * 265 / 8;
                    b70 = a70 * 265 / 8;
                    b80 = a80 * 265 / 8;
                    b90 = a90 * 265 / 8;
                    b100 = a100 * 265 / 8;

                    labelSPR.Visible = true;
                    labelBoost.Visible = true;
                    labelElemDef.Visible = true;
                    labelSumMag.Visible = true;
                    labelHP.Visible = false;
                    numericUpDownSPR.Visible = true;
                    numericUpDownBoost.Visible = true;
                    numericUpDownSumMag.Visible = true;
                    numericUpDownElemDef.Visible = true;
                    numericUpDownHP.Visible = false;
                    checkBoxDefault.Visible = true;

                    chartGfDamage.Series["Default"].Points.Clear();
                    chartGfDamage.Series["Default"].Points.AddXY
                        (0, Math.Floor(e1 * (900 - numericUpDownElemDef.Value) / 100));
                    chartGfDamage.Series["Default"].Points.AddXY
                        (1, Math.Floor(e10 * (900 - numericUpDownElemDef.Value) / 100));
                    chartGfDamage.Series["Default"].Points.AddXY
                        (2, Math.Floor(e20 * (900 - numericUpDownElemDef.Value) / 100));
                    chartGfDamage.Series["Default"].Points.AddXY
                        (3, Math.Floor(e30 * (900 - numericUpDownElemDef.Value) / 100));
                    chartGfDamage.Series["Default"].Points.AddXY
                        (4, Math.Floor(e40 * (900 - numericUpDownElemDef.Value) / 100));
                    chartGfDamage.Series["Default"].Points.AddXY
                        (5, Math.Floor(e50 * (900 - numericUpDownElemDef.Value) / 100));
                    chartGfDamage.Series["Default"].Points.AddXY
                        (6, Math.Floor(e60 * (900 - numericUpDownElemDef.Value) / 100));
                    chartGfDamage.Series["Default"].Points.AddXY
                        (7, Math.Floor(e70 * (900 - numericUpDownElemDef.Value) / 100));
                    chartGfDamage.Series["Default"].Points.AddXY
                        (8, Math.Floor(e80 * (900 - numericUpDownElemDef.Value) / 100));
                    chartGfDamage.Series["Default"].Points.AddXY
                        (9, Math.Floor(e90 * (900 - numericUpDownElemDef.Value) / 100));
                    chartGfDamage.Series["Default"].Points.AddXY
                        (10, Math.Floor(e100 * (900 - numericUpDownElemDef.Value) / 100));
                }
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }
        }

        private void DefaultChartWorkerValuesUnchecked()
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

                b1 = a1 * 265 / 8;
                b10 = a10 * 265 / 8;
                b20 = a20 * 265 / 8;
                b30 = a30 * 265 / 8;
                b40 = a40 * 265 / 8;
                b50 = a50 * 265 / 8;
                b60 = a60 * 265 / 8;
                b70 = a70 * 265 / 8;
                b80 = a80 * 265 / 8;
                b90 = a90 * 265 / 8;
                b100 = a100 * 265 / 8;

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


                if (_mainForm.comboBoxGFAttackType.SelectedIndex == 11) //Attack type "GF"
                {
                    labelSPR.Visible = true;
                    labelBoost.Visible = true;
                    labelElemDef.Visible = true;
                    labelSumMag.Visible = true;
                    labelHP.Visible = false;
                    numericUpDownSPR.Visible = true;
                    numericUpDownBoost.Visible = true;
                    numericUpDownSumMag.Visible = true;
                    numericUpDownElemDef.Visible = true;
                    numericUpDownHP.Visible = false;
                    checkBoxDefault.Visible = true;

                    chartGfDamage.Series["Default"].Points.Clear();
                    chartGfDamage.Series["Default"].Points.AddXY(0, c1);
                    chartGfDamage.Series["Default"].Points.AddXY(1, c10);
                    chartGfDamage.Series["Default"].Points.AddXY(2, c20);
                    chartGfDamage.Series["Default"].Points.AddXY(3, c30);
                    chartGfDamage.Series["Default"].Points.AddXY(4, c40);
                    chartGfDamage.Series["Default"].Points.AddXY(5, c50);
                    chartGfDamage.Series["Default"].Points.AddXY(6, c60);
                    chartGfDamage.Series["Default"].Points.AddXY(7, c70);
                    chartGfDamage.Series["Default"].Points.AddXY(8, c80);
                    chartGfDamage.Series["Default"].Points.AddXY(9, c90);
                    chartGfDamage.Series["Default"].Points.AddXY(10, c100);
                }
                else if (_mainForm.comboBoxGFAttackType.SelectedIndex == 20) //Attack type "Diablos"
                {
                    labelSPR.Visible = false;
                    labelBoost.Visible = false;
                    labelElemDef.Visible = false;
                    labelSumMag.Visible = false;
                    labelHP.Visible = true;
                    numericUpDownSPR.Visible = false;
                    numericUpDownBoost.Visible = false;
                    numericUpDownSumMag.Visible = false;
                    numericUpDownElemDef.Visible = false;
                    numericUpDownHP.Visible = true;
                    checkBoxDefault.Visible = false;

                    chartGfDamage.Series["Default"].Points.Clear();
                    chartGfDamage.Series["Default"].Points.AddXY
                        (0, Math.Floor(numericUpDownHP.Value * 1 / 100));
                    chartGfDamage.Series["Default"].Points.AddXY
                        (1, Math.Floor(numericUpDownHP.Value * 10 / 100));
                    chartGfDamage.Series["Default"].Points.AddXY
                        (2, Math.Floor(numericUpDownHP.Value * 20 / 100));
                    chartGfDamage.Series["Default"].Points.AddXY
                        (3, Math.Floor(numericUpDownHP.Value * 30 / 100));
                    chartGfDamage.Series["Default"].Points.AddXY
                        (4, Math.Floor(numericUpDownHP.Value * 40 / 100));
                    chartGfDamage.Series["Default"].Points.AddXY
                        (5, Math.Floor(numericUpDownHP.Value * 50 / 100));
                    chartGfDamage.Series["Default"].Points.AddXY
                        (6, Math.Floor(numericUpDownHP.Value * 60 / 100));
                    chartGfDamage.Series["Default"].Points.AddXY
                        (7, Math.Floor(numericUpDownHP.Value * 70 / 100));
                    chartGfDamage.Series["Default"].Points.AddXY
                        (8, Math.Floor(numericUpDownHP.Value * 80 / 100));
                    chartGfDamage.Series["Default"].Points.AddXY
                        (9, Math.Floor(numericUpDownHP.Value * 90 / 100));
                    chartGfDamage.Series["Default"].Points.AddXY
                        (10, Math.Floor(numericUpDownHP.Value * 100 / 100));
                }
                else if (_mainForm.comboBoxGFAttackType.SelectedIndex == 29) //Attack type "Cactuar"
                {
                    labelSPR.Visible = false;
                    labelBoost.Visible = false;
                    labelElemDef.Visible = false;
                    labelSumMag.Visible = false;
                    labelHP.Visible = false;
                    numericUpDownSPR.Visible = false;
                    numericUpDownBoost.Visible = false;
                    numericUpDownSumMag.Visible = false;
                    numericUpDownElemDef.Visible = false;
                    numericUpDownHP.Visible = false;
                    checkBoxDefault.Visible = false;

                    chartGfDamage.Series["Default"].Points.AddXY
                        (0, (((1 * KernelWorker.GetSelectedGFData.GFPower) / 1000) + 1) * 1000);
                    chartGfDamage.Series["Default"].Points.AddXY
                        (1, (((10 * KernelWorker.GetSelectedGFData.GFPower) / 1000) + 1) * 1000);
                    chartGfDamage.Series["Default"].Points.AddXY
                        (2, (((20 * KernelWorker.GetSelectedGFData.GFPower) / 1000) + 1) * 1000);
                    chartGfDamage.Series["Default"].Points.AddXY
                        (3, (((30 * KernelWorker.GetSelectedGFData.GFPower) / 1000) + 1) * 1000);
                    chartGfDamage.Series["Default"].Points.AddXY
                        (4, (((40 * KernelWorker.GetSelectedGFData.GFPower) / 1000) + 1) * 1000);
                    chartGfDamage.Series["Default"].Points.AddXY
                        (5, (((50 * KernelWorker.GetSelectedGFData.GFPower) / 1000) + 1) * 1000);
                    chartGfDamage.Series["Default"].Points.AddXY
                        (6, (((60 * KernelWorker.GetSelectedGFData.GFPower) / 1000) + 1) * 1000);
                    chartGfDamage.Series["Default"].Points.AddXY
                        (7, (((70 * KernelWorker.GetSelectedGFData.GFPower) / 1000) + 1) * 1000);
                    chartGfDamage.Series["Default"].Points.AddXY
                        (8, (((80 * KernelWorker.GetSelectedGFData.GFPower) / 1000) + 1) * 1000);
                    chartGfDamage.Series["Default"].Points.AddXY
                        (9, (((90 * KernelWorker.GetSelectedGFData.GFPower) / 1000) + 1) * 1000);
                    chartGfDamage.Series["Default"].Points.AddXY
                        (10, (((100 * KernelWorker.GetSelectedGFData.GFPower) / 1000) + 1) * 1000);
                }
                else if (_mainForm.comboBoxGFAttackType.SelectedIndex == 15) //Attack type "Ignore SPR"
                {
                    b1 = a1 * 265 / 8;
                    b10 = a10 * 265 / 8;
                    b20 = a20 * 265 / 8;
                    b30 = a30 * 265 / 8;
                    b40 = a40 * 265 / 8;
                    b50 = a50 * 265 / 8;
                    b60 = a60 * 265 / 8;
                    b70 = a70 * 265 / 8;
                    b80 = a80 * 265 / 8;
                    b90 = a90 * 265 / 8;
                    b100 = a100 * 265 / 8;

                    labelSPR.Visible = true;
                    labelBoost.Visible = true;
                    labelElemDef.Visible = true;
                    labelSumMag.Visible = true;
                    labelHP.Visible = false;
                    numericUpDownSPR.Visible = true;
                    numericUpDownBoost.Visible = true;
                    numericUpDownSumMag.Visible = true;
                    numericUpDownElemDef.Visible = true;
                    numericUpDownHP.Visible = false;
                    checkBoxDefault.Visible = true;

                    chartGfDamage.Series["Default"].Points.Clear();
                    chartGfDamage.Series["Default"].Points.AddXY
                        (0, c1);
                    chartGfDamage.Series["Default"].Points.AddXY
                        (1, c10);
                    chartGfDamage.Series["Default"].Points.AddXY
                        (2, c20);
                    chartGfDamage.Series["Default"].Points.AddXY
                        (3, c30);
                    chartGfDamage.Series["Default"].Points.AddXY
                        (4, c40);
                    chartGfDamage.Series["Default"].Points.AddXY
                        (5, c50);
                    chartGfDamage.Series["Default"].Points.AddXY
                        (6, c60);
                    chartGfDamage.Series["Default"].Points.AddXY
                        (7, c70);
                    chartGfDamage.Series["Default"].Points.AddXY
                        (8, c80);
                    chartGfDamage.Series["Default"].Points.AddXY
                        (9, c90);
                    chartGfDamage.Series["Default"].Points.AddXY
                        (10, c100);
                }
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }
        }

        private void CurrentChartWorker()
        {
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


                if (_mainForm.comboBoxGFAttackType.SelectedIndex == 11) //Attack type "GF"
                {
                    chartGfDamage.Series["Current"].Points.Clear();
                    chartGfDamage.Series["Current"].Points.AddXY
                        (0, Math.Floor(e1 * (900 - numericUpDownElemDef.Value) / 100));
                    chartGfDamage.Series["Current"].Points.AddXY
                        (1, Math.Floor(e10 * (900 - numericUpDownElemDef.Value) / 100));
                    chartGfDamage.Series["Current"].Points.AddXY
                        (2, Math.Floor(e20 * (900 - numericUpDownElemDef.Value) / 100));
                    chartGfDamage.Series["Current"].Points.AddXY
                        (3, Math.Floor(e30 * (900 - numericUpDownElemDef.Value) / 100));
                    chartGfDamage.Series["Current"].Points.AddXY
                        (4, Math.Floor(e40 * (900 - numericUpDownElemDef.Value) / 100));
                    chartGfDamage.Series["Current"].Points.AddXY
                        (5, Math.Floor(e50 * (900 - numericUpDownElemDef.Value) / 100));
                    chartGfDamage.Series["Current"].Points.AddXY
                        (6, Math.Floor(e60 * (900 - numericUpDownElemDef.Value) / 100));
                    chartGfDamage.Series["Current"].Points.AddXY
                        (7, Math.Floor(e70 * (900 - numericUpDownElemDef.Value) / 100));
                    chartGfDamage.Series["Current"].Points.AddXY
                        (8, Math.Floor(e80 * (900 - numericUpDownElemDef.Value) / 100));
                    chartGfDamage.Series["Current"].Points.AddXY
                        (9, Math.Floor(e90 * (900 - numericUpDownElemDef.Value) / 100));
                    chartGfDamage.Series["Current"].Points.AddXY
                        (10, Math.Floor(e100 * (900 - numericUpDownElemDef.Value) / 100));

                    labelAttackType.Text = "Attack Type: GF";
                }
                else if (_mainForm.comboBoxGFAttackType.SelectedIndex == 20) //Attack type "Diablos"
                {
                    chartGfDamage.Series["Current"].Points.Clear();
                    chartGfDamage.Series["Current"].Points.AddXY
                        (0, Math.Floor(numericUpDownHP.Value * 1 / 100));
                    chartGfDamage.Series["Current"].Points.AddXY
                        (1, Math.Floor(numericUpDownHP.Value * 10 / 100));
                    chartGfDamage.Series["Current"].Points.AddXY
                        (2, Math.Floor(numericUpDownHP.Value * 20 / 100));
                    chartGfDamage.Series["Current"].Points.AddXY
                        (3, Math.Floor(numericUpDownHP.Value * 30 / 100));
                    chartGfDamage.Series["Current"].Points.AddXY
                        (4, Math.Floor(numericUpDownHP.Value * 40 / 100));
                    chartGfDamage.Series["Current"].Points.AddXY
                        (5, Math.Floor(numericUpDownHP.Value * 50 / 100));
                    chartGfDamage.Series["Current"].Points.AddXY
                        (6, Math.Floor(numericUpDownHP.Value * 60 / 100));
                    chartGfDamage.Series["Current"].Points.AddXY
                        (7, Math.Floor(numericUpDownHP.Value * 70 / 100));
                    chartGfDamage.Series["Current"].Points.AddXY
                        (8, Math.Floor(numericUpDownHP.Value * 80 / 100));
                    chartGfDamage.Series["Current"].Points.AddXY
                        (9, Math.Floor(numericUpDownHP.Value * 90 / 100));
                    chartGfDamage.Series["Current"].Points.AddXY
                        (10, Math.Floor(numericUpDownHP.Value * 100 / 100));

                    labelAttackType.Text = "Attack Type: Diablos";
                }
                else if (_mainForm.comboBoxGFAttackType.SelectedIndex == 29) //Attack type "Cactuar"
                {
                    chartGfDamage.Series["Current"].Points.Clear();
                    chartGfDamage.Series["Current"].Points.AddXY
                        (0, (((1 * KernelWorker.GetSelectedGFData.GFPower) / 1000) + 1) * 1000);
                    chartGfDamage.Series["Current"].Points.AddXY
                        (1, (((10 * KernelWorker.GetSelectedGFData.GFPower) / 1000) + 1) * 1000);
                    chartGfDamage.Series["Current"].Points.AddXY
                        (2, (((20 * KernelWorker.GetSelectedGFData.GFPower) / 1000) + 1) * 1000);
                    chartGfDamage.Series["Current"].Points.AddXY
                        (3, (((30 * KernelWorker.GetSelectedGFData.GFPower) / 1000) + 1) * 1000);
                    chartGfDamage.Series["Current"].Points.AddXY
                        (4, (((40 * KernelWorker.GetSelectedGFData.GFPower) / 1000) + 1) * 1000);
                    chartGfDamage.Series["Current"].Points.AddXY
                        (5, (((50 * KernelWorker.GetSelectedGFData.GFPower) / 1000) + 1) * 1000);
                    chartGfDamage.Series["Current"].Points.AddXY
                        (6, (((60 * KernelWorker.GetSelectedGFData.GFPower) / 1000) + 1) * 1000);
                    chartGfDamage.Series["Current"].Points.AddXY
                        (7, (((70 * KernelWorker.GetSelectedGFData.GFPower) / 1000) + 1) * 1000);
                    chartGfDamage.Series["Current"].Points.AddXY
                        (8, (((80 * KernelWorker.GetSelectedGFData.GFPower) / 1000) + 1) * 1000);
                    chartGfDamage.Series["Current"].Points.AddXY
                        (9, (((90 * KernelWorker.GetSelectedGFData.GFPower) / 1000) + 1) * 1000);
                    chartGfDamage.Series["Current"].Points.AddXY
                        (10, (((100 * KernelWorker.GetSelectedGFData.GFPower) / 1000) + 1) * 1000);

                    labelAttackType.Text = "Attack Type: Cactuar";
                }
                else if (_mainForm.comboBoxGFAttackType.SelectedIndex == 15) //Attack type "Ignore SPR"
                {
                    b1 = a1 * 265 / 8;
                    b10 = a10 * 265 / 8;
                    b20 = a20 * 265 / 8;
                    b30 = a30 * 265 / 8;
                    b40 = a40 * 265 / 8;
                    b50 = a50 * 265 / 8;
                    b60 = a60 * 265 / 8;
                    b70 = a70 * 265 / 8;
                    b80 = a80 * 265 / 8;
                    b90 = a90 * 265 / 8;
                    b100 = a100 * 265 / 8;

                    labelSPR.Visible = true;
                    labelBoost.Visible = true;
                    labelElemDef.Visible = true;
                    labelSumMag.Visible = true;
                    labelHP.Visible = false;
                    numericUpDownSPR.Visible = true;
                    numericUpDownBoost.Visible = true;
                    numericUpDownSumMag.Visible = true;
                    numericUpDownElemDef.Visible = true;
                    numericUpDownHP.Visible = false;
                    checkBoxDefault.Visible = true;

                    chartGfDamage.Series["Current"].Points.Clear();
                    chartGfDamage.Series["Current"].Points.AddXY
                        (0, Math.Floor(e1 * (900 - numericUpDownElemDef.Value) / 100));
                    chartGfDamage.Series["Current"].Points.AddXY
                        (1, Math.Floor(e10 * (900 - numericUpDownElemDef.Value) / 100));
                    chartGfDamage.Series["Current"].Points.AddXY
                        (2, Math.Floor(e20 * (900 - numericUpDownElemDef.Value) / 100));
                    chartGfDamage.Series["Current"].Points.AddXY
                        (3, Math.Floor(e30 * (900 - numericUpDownElemDef.Value) / 100));
                    chartGfDamage.Series["Current"].Points.AddXY
                        (4, Math.Floor(e40 * (900 - numericUpDownElemDef.Value) / 100));
                    chartGfDamage.Series["Current"].Points.AddXY
                        (5, Math.Floor(e50 * (900 - numericUpDownElemDef.Value) / 100));
                    chartGfDamage.Series["Current"].Points.AddXY
                        (6, Math.Floor(e60 * (900 - numericUpDownElemDef.Value) / 100));
                    chartGfDamage.Series["Current"].Points.AddXY
                        (7, Math.Floor(e70 * (900 - numericUpDownElemDef.Value) / 100));
                    chartGfDamage.Series["Current"].Points.AddXY
                        (8, Math.Floor(e80 * (900 - numericUpDownElemDef.Value) / 100));
                    chartGfDamage.Series["Current"].Points.AddXY
                        (9, Math.Floor(e90 * (900 - numericUpDownElemDef.Value) / 100));
                    chartGfDamage.Series["Current"].Points.AddXY
                        (10, Math.Floor(e100 * (900 - numericUpDownElemDef.Value) / 100));

                    labelAttackType.Text = "Attack Type: Ignore SPR";
                }
                else
                {
                    chartGfDamage.Series["Current"].Points.Clear();
                    labelAttackType.Text = "Attack Type: Unsupported";
                }
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }
        }

        #endregion

        #region Changed values

        private void numericUpDownGfDamage_ValueChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                if (checkBoxDefault.Checked == true)
                {
                    DefaultChartWorker();
                    CurrentChartWorker();
                }

                else if (checkBoxDefault.Checked == false)
                {
                    DefaultChartWorkerValuesUnchecked();
                    CurrentChartWorker();
                }
            }
        }

        private void Default_CheckedChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                if (checkBoxDefault.Checked == true)
                {
                    DefaultChartWorker();
                    CurrentChartWorker();
                }
                else if (checkBoxDefault.Checked == false)
                {
                    DefaultChartWorkerValuesUnchecked();
                    CurrentChartWorker();
                }
            }
        }

        #endregion
    }
}
