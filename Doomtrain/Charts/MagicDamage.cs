using System;
using System.Drawing;
using System.Windows.Forms;

namespace Doomtrain.Charts
{
    public partial class MagicDamage : Form
    {
        public MagicDamage(mainForm mainForm)
        {
            _mainForm = mainForm;
            InitializeComponent();

            #region Disable objects

            labelAttMAG.Visible = false;
            labelSPR.Visible = false;
            labelHealMAG.Visible = false;
            labelElemDef.Visible = false;
            labelHP.Visible = false;
            numericUpDownAttMAG.Visible = false;
            numericUpDownSPR.Visible = false;
            numericUpDownHealMAG.Visible = false;
            numericUpDownElemDef.Visible = false;
            numericUpDownHP.Visible = false;
            checkBoxDefault.Visible = false;

            #endregion

            #region Event handlers

            _mainForm.numericUpDownMagicSpellPower.ValueChanged += new EventHandler(this.numericUpDownMagicDamage_ValueChanged);
            _mainForm.listBoxMagic.SelectedIndexChanged += new EventHandler(this.numericUpDownMagicDamage_ValueChanged);
            _mainForm.comboBoxMagicAttackType.SelectedIndexChanged += new EventHandler(this.numericUpDownMagicDamage_ValueChanged);
            _mainForm.numericUpDownMagicHitCount.ValueChanged += new EventHandler(this.numericUpDownMagicDamage_ValueChanged);
            numericUpDownAttMAG.ValueChanged += new EventHandler(numericUpDownMagicDamage_ValueChanged);
            numericUpDownSPR.ValueChanged += new EventHandler(numericUpDownMagicDamage_ValueChanged);
            numericUpDownHealMAG.ValueChanged += new EventHandler(numericUpDownMagicDamage_ValueChanged);
            numericUpDownElemDef.ValueChanged += new EventHandler(numericUpDownMagicDamage_ValueChanged);
            numericUpDownHP.ValueChanged += new EventHandler(numericUpDownMagicDamage_ValueChanged);
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

        private void chartMagicDamage_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void chartMagicDamage_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void chartMagicDamage_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        #endregion

        #region Buttons

        private void buttonInfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The values here are only used for the chart and they have no connection to the kernel file. " +
                "You need to change the values manually.\n\n" +
                "Attacker MAG = The MAG value of the offensive magic caster.\n" +
                "Target SPR = The SPR value of the target.\n" +
                "Healer MAG = The MAG value of the healing magic caster.\n" +
                "Target HP = The current HP value of the target, used in Demi.\n" +
                "Elem Defense = The elemental defense of the target.", "Info",
                MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion


        #region Formulas variables declaration

        private int a, b, c;

        #endregion

        #region Read/Write values

        private void DefaultChartWorker()
        {
            KernelWorker.ReadMagic(_mainForm.listBoxMagic.SelectedIndex, KernelWorker.BackupKernel);
            try
            {
                a = (int)numericUpDownAttMAG.Value + KernelWorker.GetSelectedMagicData.SpellPower;
                b = a * (265 - (int)numericUpDownSPR.Value) / 4;
                c = b * KernelWorker.GetSelectedMagicData.SpellPower / 256;

                if (_mainForm.comboBoxMagicAttackType.SelectedIndex == 2) // Attack type "Magic Attack"
                {
                    labelAttMAG.Visible = true;
                    labelSPR.Visible = true;
                    labelHealMAG.Visible = false;
                    labelElemDef.Visible = true;
                    labelHP.Visible = false;
                    numericUpDownAttMAG.Visible = true;
                    numericUpDownSPR.Visible = true;
                    numericUpDownHealMAG.Visible = false;
                    numericUpDownElemDef.Visible = true;
                    numericUpDownHP.Visible = false;
                    checkBoxDefault.Visible = true;

                    chartMagicDamage.ChartAreas["ChartAreaMagicDamage"].AxisX.CustomLabels.Clear();
                    chartMagicDamage.ChartAreas["ChartAreaMagicDamage"].AxisX.CustomLabels.Add(-1D, 3D, "Magic Attack");

                    chartMagicDamage.Series["Default"].Points.Clear();
                    chartMagicDamage.Series["Default"].Points.AddXY
                        (0, Math.Floor(c * (900 - numericUpDownElemDef.Value) / 100) * KernelWorker.GetSelectedMagicData.HitCount);
                }
                else if (_mainForm.comboBoxMagicAttackType.SelectedIndex == 8) //Attack type "Demi"
                {
                    labelAttMAG.Visible = false;
                    labelSPR.Visible = false;
                    labelHealMAG.Visible = false;
                    labelElemDef.Visible = false;
                    labelHP.Visible = true;
                    numericUpDownAttMAG.Visible = false;
                    numericUpDownSPR.Visible = false;
                    numericUpDownHealMAG.Visible = false;
                    numericUpDownElemDef.Visible = false;
                    numericUpDownHP.Visible = true;
                    checkBoxDefault.Visible = false;

                    chartMagicDamage.ChartAreas["ChartAreaMagicDamage"].AxisX.CustomLabels.Clear();
                    chartMagicDamage.ChartAreas["ChartAreaMagicDamage"].AxisX.CustomLabels.Add(-1D, 3D, "Demi Attack");

                    chartMagicDamage.Series["Default"].Points.Clear();
                    chartMagicDamage.Series["Default"].Points.AddXY
                        (0, Math.Floor(numericUpDownHP.Value * KernelWorker.GetSelectedMagicData.SpellPower / 16) * KernelWorker.GetSelectedMagicData.HitCount);
                }
                else if (_mainForm.comboBoxMagicAttackType.SelectedIndex == 3) //Attack type "Curative"
                {
                    labelAttMAG.Visible = false;
                    labelSPR.Visible = false;
                    labelHealMAG.Visible = true;
                    labelElemDef.Visible = false;
                    labelHP.Visible = false;
                    numericUpDownAttMAG.Visible = false;
                    numericUpDownSPR.Visible = false;
                    numericUpDownHealMAG.Visible = true;
                    numericUpDownElemDef.Visible = false;
                    numericUpDownHP.Visible = false;
                    checkBoxDefault.Visible = true;

                    chartMagicDamage.ChartAreas["ChartAreaMagicDamage"].AxisX.CustomLabels.Clear();
                    chartMagicDamage.ChartAreas["ChartAreaMagicDamage"].AxisX.CustomLabels.Add(-1D, 3D, "Curative Magic");

                    chartMagicDamage.Series["Default"].Points.Clear();
                    chartMagicDamage.Series["Default"].Points.AddXY
                        (0, Math.Floor((KernelWorker.GetSelectedMagicData.SpellPower + numericUpDownHealMAG.Value) * KernelWorker.GetSelectedMagicData.SpellPower / 2) *
                        KernelWorker.GetSelectedMagicData.HitCount);
                }
                else if (_mainForm.comboBoxMagicAttackType.SelectedIndex == 15) // Attack type "Ignore SPR"
                {
                    labelAttMAG.Visible = true;
                    labelSPR.Visible = false;
                    labelHealMAG.Visible = false;
                    labelElemDef.Visible = true;
                    labelHP.Visible = false;
                    numericUpDownAttMAG.Visible = true;
                    numericUpDownSPR.Visible = false;
                    numericUpDownHealMAG.Visible = false;
                    numericUpDownElemDef.Visible = true;
                    numericUpDownHP.Visible = false;
                    checkBoxDefault.Visible = true;

                    a = (int)numericUpDownAttMAG.Value + KernelWorker.GetSelectedMagicData.SpellPower;
                    b = a * 265 / 4;
                    c = b * KernelWorker.GetSelectedMagicData.SpellPower / 256;

                    chartMagicDamage.ChartAreas["ChartAreaMagicDamage"].AxisX.CustomLabels.Clear();
                    chartMagicDamage.ChartAreas["ChartAreaMagicDamage"].AxisX.CustomLabels.Add(-1D, 3D, "Ignore SPR Attack");

                    chartMagicDamage.Series["Default"].Points.Clear();
                    chartMagicDamage.Series["Default"].Points.AddXY
                        (0, Math.Floor(c * (900 - numericUpDownElemDef.Value) / 100) * KernelWorker.GetSelectedMagicData.HitCount);
                }
                else
                {
                    labelAttMAG.Visible = false;
                    labelSPR.Visible = false;
                    labelHealMAG.Visible = false;
                    labelElemDef.Visible = false;
                    labelHP.Visible = false;
                    numericUpDownAttMAG.Visible = false;
                    numericUpDownSPR.Visible = false;
                    numericUpDownHealMAG.Visible = false;
                    numericUpDownElemDef.Visible = false;
                    numericUpDownHP.Visible = false;
                    checkBoxDefault.Visible = false;

                    chartMagicDamage.ChartAreas["ChartAreaMagicDamage"].AxisX.CustomLabels.Clear();
                    chartMagicDamage.ChartAreas["ChartAreaMagicDamage"].AxisX.CustomLabels.Add(-1D, 3D, "Unsupported Attack Type");

                    chartMagicDamage.Series["Default"].Points.Clear();
                }
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }
        }

        private void DefaultChartWorkerValuesUnchecked()
        {
            KernelWorker.ReadMagic(_mainForm.listBoxMagic.SelectedIndex, KernelWorker.BackupKernel);
            try
            {
                a = KernelWorker.GetSelectedMagicData.SpellPower;
                b = a * 265 / 4;
                c = b * KernelWorker.GetSelectedMagicData.SpellPower / 256;

                if (_mainForm.comboBoxMagicAttackType.SelectedIndex == 2) // Attack type "Magic Attack"
                {
                    labelAttMAG.Visible = true;
                    labelSPR.Visible = true;
                    labelHealMAG.Visible = false;
                    labelElemDef.Visible = true;
                    labelHP.Visible = false;
                    numericUpDownAttMAG.Visible = true;
                    numericUpDownSPR.Visible = true;
                    numericUpDownHealMAG.Visible = false;
                    numericUpDownElemDef.Visible = true;
                    numericUpDownHP.Visible = false;
                    checkBoxDefault.Visible = true;

                    chartMagicDamage.ChartAreas["ChartAreaMagicDamage"].AxisX.CustomLabels.Clear();
                    chartMagicDamage.ChartAreas["ChartAreaMagicDamage"].AxisX.CustomLabels.Add(-1D, 3D, "Magic Attack");

                    chartMagicDamage.Series["Default"].Points.Clear();
                    chartMagicDamage.Series["Default"].Points.AddXY
                        (0, c * KernelWorker.GetSelectedMagicData.HitCount);
                }
                else if (_mainForm.comboBoxMagicAttackType.SelectedIndex == 8) //Attack type "Demi"
                {
                    labelAttMAG.Visible = false;
                    labelSPR.Visible = false;
                    labelHealMAG.Visible = false;
                    labelElemDef.Visible = false;
                    labelHP.Visible = true;
                    numericUpDownAttMAG.Visible = false;
                    numericUpDownSPR.Visible = false;
                    numericUpDownHealMAG.Visible = false;
                    numericUpDownElemDef.Visible = false;
                    numericUpDownHP.Visible = true;
                    checkBoxDefault.Visible = false;

                    chartMagicDamage.ChartAreas["ChartAreaMagicDamage"].AxisX.CustomLabels.Clear();
                    chartMagicDamage.ChartAreas["ChartAreaMagicDamage"].AxisX.CustomLabels.Add(-1D, 3D, "Demi Attack");

                    chartMagicDamage.Series["Default"].Points.Clear();
                    chartMagicDamage.Series["Default"].Points.AddXY
                        (0, (numericUpDownHP.Value * KernelWorker.GetSelectedMagicData.SpellPower / 16) * KernelWorker.GetSelectedMagicData.HitCount);
                }
                else if (_mainForm.comboBoxMagicAttackType.SelectedIndex == 3) //Attack type "Curative"
                {
                    labelAttMAG.Visible = false;
                    labelSPR.Visible = false;
                    labelHealMAG.Visible = true;
                    labelElemDef.Visible = false;
                    labelHP.Visible = false;
                    numericUpDownAttMAG.Visible = false;
                    numericUpDownSPR.Visible = false;
                    numericUpDownHealMAG.Visible = true;
                    numericUpDownElemDef.Visible = false;
                    numericUpDownHP.Visible = false;
                    checkBoxDefault.Visible = true;

                    chartMagicDamage.ChartAreas["ChartAreaMagicDamage"].AxisX.CustomLabels.Clear();
                    chartMagicDamage.ChartAreas["ChartAreaMagicDamage"].AxisX.CustomLabels.Add(-1D, 3D, "Curative Magic");

                    chartMagicDamage.Series["Default"].Points.Clear();
                    chartMagicDamage.Series["Default"].Points.AddXY
                        (0, (KernelWorker.GetSelectedMagicData.SpellPower * KernelWorker.GetSelectedMagicData.SpellPower / 2.0) *
                        KernelWorker.GetSelectedMagicData.HitCount);
                }
                else if (_mainForm.comboBoxMagicAttackType.SelectedIndex == 15) // Attack type "Ignore SPR"
                {
                    labelAttMAG.Visible = true;
                    labelSPR.Visible = false;
                    labelHealMAG.Visible = false;
                    labelElemDef.Visible = true;
                    labelHP.Visible = false;
                    numericUpDownAttMAG.Visible = true;
                    numericUpDownSPR.Visible = false;
                    numericUpDownHealMAG.Visible = false;
                    numericUpDownElemDef.Visible = true;
                    numericUpDownHP.Visible = false;
                    checkBoxDefault.Visible = true;

                    chartMagicDamage.ChartAreas["ChartAreaMagicDamage"].AxisX.CustomLabels.Clear();
                    chartMagicDamage.ChartAreas["ChartAreaMagicDamage"].AxisX.CustomLabels.Add(-1D, 3D, "Ignore SPR Attack");

                    chartMagicDamage.Series["Default"].Points.Clear();
                    chartMagicDamage.Series["Default"].Points.AddXY
                        (0, c * KernelWorker.GetSelectedMagicData.HitCount);
                }
                else
                {
                    labelAttMAG.Visible = false;
                    labelSPR.Visible = false;
                    labelHealMAG.Visible = false;
                    labelElemDef.Visible = false;
                    labelHP.Visible = false;
                    numericUpDownAttMAG.Visible = false;
                    numericUpDownSPR.Visible = false;
                    numericUpDownHealMAG.Visible = false;
                    numericUpDownElemDef.Visible = false;
                    numericUpDownHP.Visible = false;
                    checkBoxDefault.Visible = false;

                    chartMagicDamage.ChartAreas["ChartAreaMagicDamage"].AxisX.CustomLabels.Clear();
                    chartMagicDamage.ChartAreas["ChartAreaMagicDamage"].AxisX.CustomLabels.Add(-1D, 3D, "Unsupported Attack Type");

                    chartMagicDamage.Series["Default"].Points.Clear();
                }
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }
        }

        private void CurrentChartWorker()
        {
            KernelWorker.ReadMagic(_mainForm.listBoxMagic.SelectedIndex, KernelWorker.Kernel);
            try
            {
                a = (int)numericUpDownAttMAG.Value + KernelWorker.GetSelectedMagicData.SpellPower;
                b = a * (265 - (int)numericUpDownSPR.Value) / 4;
                c = b * KernelWorker.GetSelectedMagicData.SpellPower / 256;

                if (_mainForm.comboBoxMagicAttackType.SelectedIndex == 2) // Attack type "Magic Attack"
                {
                    labelAttMAG.Visible = true;
                    labelSPR.Visible = true;
                    labelHealMAG.Visible = false;
                    labelElemDef.Visible = true;
                    labelHP.Visible = false;
                    numericUpDownAttMAG.Visible = true;
                    numericUpDownSPR.Visible = true;
                    numericUpDownHealMAG.Visible = false;
                    numericUpDownElemDef.Visible = true;
                    numericUpDownHP.Visible = false;
                    checkBoxDefault.Visible = true;

                    chartMagicDamage.ChartAreas["ChartAreaMagicDamage"].AxisY.Title = "Damage";
                    chartMagicDamage.ChartAreas["ChartAreaMagicDamage"].AxisX.CustomLabels.Clear();
                    chartMagicDamage.ChartAreas["ChartAreaMagicDamage"].AxisX.CustomLabels.Add(-1D, 3D, "Magic Attack");

                    chartMagicDamage.Series["Current"].Points.Clear();
                    chartMagicDamage.Series["Current"].Points.AddXY
                        (0, Math.Floor(c * (900 - numericUpDownElemDef.Value) / 100) * KernelWorker.GetSelectedMagicData.HitCount);
                }
                else if (_mainForm.comboBoxMagicAttackType.SelectedIndex == 8) //Attack type "Demi"
                {
                    labelAttMAG.Visible = false;
                    labelSPR.Visible = false;
                    labelHealMAG.Visible = false;
                    labelElemDef.Visible = false;
                    labelHP.Visible = true;
                    numericUpDownAttMAG.Visible = false;
                    numericUpDownSPR.Visible = false;
                    numericUpDownHealMAG.Visible = false;
                    numericUpDownElemDef.Visible = false;
                    numericUpDownHP.Visible = true;
                    checkBoxDefault.Visible = false;

                    chartMagicDamage.ChartAreas["ChartAreaMagicDamage"].AxisY.Title = "Damage";
                    chartMagicDamage.ChartAreas["ChartAreaMagicDamage"].AxisX.CustomLabels.Clear();
                    chartMagicDamage.ChartAreas["ChartAreaMagicDamage"].AxisX.CustomLabels.Add(-1D, 3D, "Demi Attack");

                    chartMagicDamage.Series["Current"].Points.Clear();
                    chartMagicDamage.Series["Current"].Points.AddXY
                        (0, Math.Floor(numericUpDownHP.Value * KernelWorker.GetSelectedMagicData.SpellPower / 16) * KernelWorker.GetSelectedMagicData.HitCount);
                }
                else if (_mainForm.comboBoxMagicAttackType.SelectedIndex == 3) //Attack type "Curative"
                {
                    labelAttMAG.Visible = false;
                    labelSPR.Visible = false;
                    labelHealMAG.Visible = true;
                    labelElemDef.Visible = false;
                    labelHP.Visible = false;
                    numericUpDownAttMAG.Visible = false;
                    numericUpDownSPR.Visible = false;
                    numericUpDownHealMAG.Visible = true;
                    numericUpDownElemDef.Visible = false;
                    numericUpDownHP.Visible = false;
                    checkBoxDefault.Visible = true;

                    chartMagicDamage.ChartAreas["ChartAreaMagicDamage"].AxisY.Title = "Healing Amount";
                    chartMagicDamage.ChartAreas["ChartAreaMagicDamage"].AxisX.CustomLabels.Clear();
                    chartMagicDamage.ChartAreas["ChartAreaMagicDamage"].AxisX.CustomLabels.Add(-1D, 3D, "Curative Magic");

                    chartMagicDamage.Series["Current"].Points.Clear();
                    chartMagicDamage.Series["Current"].Points.AddXY
                        (0, Math.Floor((KernelWorker.GetSelectedMagicData.SpellPower + numericUpDownHealMAG.Value) * KernelWorker.GetSelectedMagicData.SpellPower / 2) *
                        KernelWorker.GetSelectedMagicData.HitCount);
                }
                else if (_mainForm.comboBoxMagicAttackType.SelectedIndex == 15) // Attack type "Ignore SPR"
                {
                    labelAttMAG.Visible = true;
                    labelSPR.Visible = false;
                    labelHealMAG.Visible = false;
                    labelElemDef.Visible = true;
                    labelHP.Visible = false;
                    numericUpDownAttMAG.Visible = true;
                    numericUpDownSPR.Visible = false;
                    numericUpDownHealMAG.Visible = false;
                    numericUpDownElemDef.Visible = true;
                    numericUpDownHP.Visible = false;
                    checkBoxDefault.Visible = true;

                    a = (int)numericUpDownAttMAG.Value + KernelWorker.GetSelectedMagicData.SpellPower;
                    b = a * 265 / 4;
                    c = b * KernelWorker.GetSelectedMagicData.SpellPower / 256;

                    chartMagicDamage.ChartAreas["ChartAreaMagicDamage"].AxisY.Title = "Damage";
                    chartMagicDamage.ChartAreas["ChartAreaMagicDamage"].AxisX.CustomLabels.Clear();
                    chartMagicDamage.ChartAreas["ChartAreaMagicDamage"].AxisX.CustomLabels.Add(-1D, 3D, "Ignore SPR Attack");

                    chartMagicDamage.Series["Current"].Points.Clear();
                    chartMagicDamage.Series["Current"].Points.AddXY
                        (0, Math.Floor(c * (900 - numericUpDownElemDef.Value) / 100) * KernelWorker.GetSelectedMagicData.HitCount);
                }
                else
                {
                    labelAttMAG.Visible = false;
                    labelSPR.Visible = false;
                    labelHealMAG.Visible = false;
                    labelElemDef.Visible = false;
                    labelHP.Visible = false;
                    numericUpDownAttMAG.Visible = false;
                    numericUpDownSPR.Visible = false;
                    numericUpDownHealMAG.Visible = false;
                    numericUpDownElemDef.Visible = false;
                    numericUpDownHP.Visible = false;
                    checkBoxDefault.Visible = false;

                    chartMagicDamage.ChartAreas["ChartAreaMagicDamage"].AxisY.Title = "Damage";
                    chartMagicDamage.ChartAreas["ChartAreaMagicDamage"].AxisX.CustomLabels.Clear();
                    chartMagicDamage.ChartAreas["ChartAreaMagicDamage"].AxisX.CustomLabels.Add(-1D, 3D, "Unsupported Attack Type");

                    chartMagicDamage.Series["Current"].Points.Clear();
                }
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }
        }

        #endregion

        #region Changed values

        private void numericUpDownMagicDamage_ValueChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                if (checkBoxDefault.Checked == true)
                {
                    DefaultChartWorker();
                    CurrentChartWorker();
                }
                if (checkBoxDefault.Checked == false)
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
                if (checkBoxDefault.Checked == false)
                {
                    DefaultChartWorkerValuesUnchecked();
                    CurrentChartWorker();
                }
            }
        }

        #endregion
    }
}
