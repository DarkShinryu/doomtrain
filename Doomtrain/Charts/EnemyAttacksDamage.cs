using System;
using System.Drawing;
using System.Windows.Forms;

namespace Doomtrain.Charts
{
    public partial class EnemyAttacksDamage : Form
    {
        public EnemyAttacksDamage(mainForm mainForm)
        {
            _mainForm = mainForm;
            InitializeComponent();

            #region Disable objects

            labelMAG.Visible = false;
            labelSPR.Visible = false;
            labelHealMAG.Visible = false;
            labelElemDef.Visible = false;
            labelHP.Visible = false;
            labelVIT.Visible = false;
            labelSTR.Visible = false;
            labelKilled.Visible = false;
            labelElemAtt.Visible = false;
            numericUpDownMAG.Visible = false;
            numericUpDownSPR.Visible = false;
            numericUpDownHealMAG.Visible = false;
            numericUpDownElemDef.Visible = false;
            numericUpDownHP.Visible = false;
            numericUpDownVIT.Visible = false;
            numericUpDownSTR.Visible = false;
            numericUpDownKilled.Visible = false;
            numericUpDownElemAtt.Visible = false;
            checkBoxDefault.Visible = false;

            #endregion

            #region Event handlers

            _mainForm.numericUpDownEnemyAttacksAttackPower.ValueChanged += new EventHandler(this.numericUpDownEADamage_ValueChanged);
            _mainForm.numericUpDownEnemyAttacksAttackParam.ValueChanged += new EventHandler(this.numericUpDownEADamage_ValueChanged);
            _mainForm.listBoxEnemyAttacks.SelectedIndexChanged += new EventHandler(this.numericUpDownEADamage_ValueChanged);
            _mainForm.comboBoxEnemyAttacksAttackType.SelectedIndexChanged += new EventHandler(this.numericUpDownEADamage_ValueChanged);
            numericUpDownMAG.ValueChanged += new EventHandler(numericUpDownEADamage_ValueChanged);
            numericUpDownSPR.ValueChanged += new EventHandler(numericUpDownEADamage_ValueChanged);
            numericUpDownSTR.ValueChanged += new EventHandler(numericUpDownEADamage_ValueChanged);
            numericUpDownVIT.ValueChanged += new EventHandler(numericUpDownEADamage_ValueChanged);
            numericUpDownHealMAG.ValueChanged += new EventHandler(numericUpDownEADamage_ValueChanged);
            numericUpDownElemDef.ValueChanged += new EventHandler(numericUpDownEADamage_ValueChanged);
            numericUpDownHP.ValueChanged += new EventHandler(numericUpDownEADamage_ValueChanged);
            numericUpDownKilled.ValueChanged += new EventHandler(numericUpDownEADamage_ValueChanged);
            numericUpDownElemAtt.ValueChanged += new EventHandler(numericUpDownEADamage_ValueChanged);
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

        private void chartEADamage_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void chartEADamage_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void chartEADamage_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        #endregion

        #region Buttons

        private void buttonInfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The values here are only used for the chart and they have no connection to the kernel file. " +
                "You need to change the values manually.\n\n" +
                "Attacker STR = The STR value of the attacker.\n" +
                "Attacker MAG = The MAG value of the offensive magic caster.\n" +
                "Target VIT = The VIT value of the target.\n" +
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

        private int a, b, c, d, e, f;

        #endregion

        #region Read/Write values

        private void DefaultChartWorker()
        {
            {
                KernelWorker.ReadEnemyAttacks(_mainForm.listBoxEnemyAttacks.SelectedIndex, KernelWorker.BackupKernel);
                try
                {
                    //Basic Attack
                    a = (int)Math.Pow((int)numericUpDownSTR.Value, 2) / 16 + (int)numericUpDownSTR.Value;
                    b = a * (265 - (int)numericUpDownVIT.Value) / 256;
                    c = b * KernelWorker.GetSelectedEnemyAttacksData.AttackPower / 16;

                    //Magic Attack
                    d = (int)numericUpDownMAG.Value + KernelWorker.GetSelectedEnemyAttacksData.AttackPower;
                    e = d * (265 - (int)numericUpDownSPR.Value) / 4;
                    f = e * KernelWorker.GetSelectedEnemyAttacksData.AttackPower / 256;

                    if (_mainForm.comboBoxEnemyAttacksAttackType.SelectedIndex == 1) // Attack type "Basic Attack"
                    {
                        chartEADamage.Series["Default"].Points.Clear();
                        if (numericUpDownElemAtt.Value == 0)
                        {
                            chartEADamage.Series["Default"].Points.AddXY(0, c);
                        }
                        else
                        {
                            chartEADamage.Series["Default"].Points.AddXY
                                (0, Math.Floor(c + (c * numericUpDownElemAtt.Value * (900 - numericUpDownElemDef.Value) / 10000)));
                        }
                    }
                    else if (_mainForm.comboBoxEnemyAttacksAttackType.SelectedIndex == 2) // Attack type "Magic Attack"
                    {
                        chartEADamage.Series["Default"].Points.Clear();
                        chartEADamage.Series["Default"].Points.AddXY
                            (0, Math.Floor(f * (900 - numericUpDownElemDef.Value) / 100));
                    }
                    else if (_mainForm.comboBoxEnemyAttacksAttackType.SelectedIndex == 8) //Attack type "Demi"
                    {
                        chartEADamage.Series["Default"].Points.Clear();
                        chartEADamage.Series["Default"].Points.AddXY
                            (0, Math.Floor(numericUpDownHP.Value * KernelWorker.GetSelectedEnemyAttacksData.AttackPower / 16));
                    }
                    else if (_mainForm.comboBoxEnemyAttacksAttackType.SelectedIndex == 3) //Attack type "Curative"
                    {
                        chartEADamage.Series["Default"].Points.Clear();
                        chartEADamage.Series["Default"].Points.AddXY
                            (0, Math.Floor((KernelWorker.GetSelectedEnemyAttacksData.AttackPower + numericUpDownHealMAG.Value) * KernelWorker.GetSelectedEnemyAttacksData.AttackPower / 2));
                    }
                    else if (_mainForm.comboBoxEnemyAttacksAttackType.SelectedIndex == 22) // Attack type "Magic Attack (Ignore Target SPR)"
                    {
                        d = (int)numericUpDownMAG.Value + KernelWorker.GetSelectedEnemyAttacksData.AttackPower;
                        e = d * 265 / 4;
                        f = e * KernelWorker.GetSelectedEnemyAttacksData.AttackPower / 256;

                        chartEADamage.Series["Default"].Points.Clear();
                        chartEADamage.Series["Default"].Points.AddXY
                            (0, Math.Floor(f * (900 - numericUpDownElemDef.Value) / 100));
                    }
                    else if (_mainForm.comboBoxEnemyAttacksAttackType.SelectedIndex == 27) // Attack type "Fixed Damage)"
                    {
                        chartEADamage.Series["Default"].Points.Clear();
                        chartEADamage.Series["Default"].Points.AddXY
                            (0, KernelWorker.GetSelectedEnemyAttacksData.AttackPower * 100 - KernelWorker.GetSelectedEnemyAttacksData.AttackParam);
                    }
                    else if (_mainForm.comboBoxEnemyAttacksAttackType.SelectedIndex == 28) // Attack type "Target Current HP - 1)"
                    {
                        chartEADamage.Series["Default"].Points.Clear();
                        chartEADamage.Series["Default"].Points.AddXY
                            (0, numericUpDownHP.Value - 1);
                    }
                    else if (_mainForm.comboBoxEnemyAttacksAttackType.SelectedIndex == 7) // Attack type "% Damage)"
                    {
                        chartEADamage.Series["Default"].Points.Clear();
                        chartEADamage.Series["Default"].Points.AddXY
                            (0, numericUpDownHP.Value * KernelWorker.GetSelectedEnemyAttacksData.AttackPower / 16);
                    }
                    else if (_mainForm.comboBoxEnemyAttacksAttackType.SelectedIndex == 34) // Attack type "Everyone's Grudge)"
                    {
                        chartEADamage.Series["Default"].Points.Clear();
                        chartEADamage.Series["Default"].Points.AddXY
                            (0, KernelWorker.GetSelectedEnemyAttacksData.AttackPower * numericUpDownKilled.Value);
                    }
                    else if (_mainForm.comboBoxEnemyAttacksAttackType.SelectedIndex == 36) // Attack type "Physical Attack (Ignore VIT)"
                    {
                        chartEADamage.Series["Default"].Points.Clear();

                        a = (int)Math.Pow((int)numericUpDownSTR.Value, 2) / 16 + (int)numericUpDownSTR.Value;
                        b = a * (265 - 0) / 256;
                        c = b * KernelWorker.GetSelectedEnemyAttacksData.AttackPower / 16;

                        if (numericUpDownElemAtt.Value == 0)
                        {
                            chartEADamage.Series["Default"].Points.AddXY(0, c);
                        }
                        else
                        {
                            chartEADamage.Series["Default"].Points.AddXY
                                (0, Math.Floor(c + (c * numericUpDownElemAtt.Value * (900 - numericUpDownElemDef.Value) / 10000)));
                        }
                    }
                    else
                    {
                        chartEADamage.Series["Default"].Points.Clear();
                        chartEADamage.Series["Default"].Points.AddXY(0, 0);
                    }
                }
                catch (Exception Exception)
                {
                    MessageBox.Show(Exception.ToString());
                }
            }
        }

        private void DefaultChartWorkerValuesUnchecked()
        {
            {
                KernelWorker.ReadEnemyAttacks(_mainForm.listBoxEnemyAttacks.SelectedIndex, KernelWorker.BackupKernel);
                try
                {
                    //Basic Attack
                    a = (int)Math.Pow((int)numericUpDownSTR.Value, 2) / 16 + (int)numericUpDownSTR.Value;
                    b = a * 265 / 256;
                    c = b * KernelWorker.GetSelectedEnemyAttacksData.AttackPower / 16;

                    //Magic Attack
                    d = KernelWorker.GetSelectedEnemyAttacksData.AttackPower;
                    e = d * 265 / 4;
                    f = e * KernelWorker.GetSelectedEnemyAttacksData.AttackPower / 256;

                    if (_mainForm.comboBoxEnemyAttacksAttackType.SelectedIndex == 1) // Attack type "Basic Attack"
                    {
                        chartEADamage.Series["Default"].Points.Clear();
                        if (numericUpDownElemAtt.Value == 0)
                        {
                            chartEADamage.Series["Default"].Points.AddXY(0, c);
                        }
                        else
                        {
                            chartEADamage.Series["Default"].Points.AddXY
                                (0, Math.Floor(c + (c * numericUpDownElemAtt.Value / 10000)));
                        }
                    }
                    else if (_mainForm.comboBoxEnemyAttacksAttackType.SelectedIndex == 2) // Attack type "Magic Attack"
                    {
                        chartEADamage.Series["Default"].Points.Clear();
                        chartEADamage.Series["Default"].Points.AddXY
                            (0, f);
                    }
                    else if (_mainForm.comboBoxEnemyAttacksAttackType.SelectedIndex == 8) //Attack type "Demi"
                    {
                        chartEADamage.Series["Default"].Points.Clear();
                        chartEADamage.Series["Default"].Points.AddXY
                            (0, Math.Floor(numericUpDownHP.Value * KernelWorker.GetSelectedEnemyAttacksData.AttackPower / 16));
                    }
                    else if (_mainForm.comboBoxEnemyAttacksAttackType.SelectedIndex == 3) //Attack type "Curative"
                    {
                        chartEADamage.Series["Default"].Points.Clear();
                        chartEADamage.Series["Default"].Points.AddXY
                            (0, Math.Floor(KernelWorker.GetSelectedEnemyAttacksData.AttackPower * KernelWorker.GetSelectedEnemyAttacksData.AttackPower / 2.0));
                    }
                    else if (_mainForm.comboBoxEnemyAttacksAttackType.SelectedIndex == 22) // Attack type "Magic Attack (Ignore Target SPR)"
                    {
                        d = KernelWorker.GetSelectedEnemyAttacksData.AttackPower;
                        e = d * 265 / 4;
                        f = e * KernelWorker.GetSelectedEnemyAttacksData.AttackPower / 256;

                        chartEADamage.Series["Default"].Points.Clear();
                        chartEADamage.Series["Default"].Points.AddXY
                            (0, f);
                    }
                    else if (_mainForm.comboBoxEnemyAttacksAttackType.SelectedIndex == 27) // Attack type "Fixed Damage)"
                    {
                        chartEADamage.Series["Default"].Points.Clear();
                        chartEADamage.Series["Default"].Points.AddXY
                            (0, KernelWorker.GetSelectedEnemyAttacksData.AttackPower * 100 - KernelWorker.GetSelectedEnemyAttacksData.AttackParam);
                    }
                    else if (_mainForm.comboBoxEnemyAttacksAttackType.SelectedIndex == 28) // Attack type "Target Current HP - 1)"
                    {
                        chartEADamage.Series["Default"].Points.Clear();
                        chartEADamage.Series["Default"].Points.AddXY
                            (0, numericUpDownHP.Value - 1);
                    }
                    else if (_mainForm.comboBoxEnemyAttacksAttackType.SelectedIndex == 7) // Attack type "% Damage)"
                    {
                        chartEADamage.Series["Default"].Points.Clear();
                        chartEADamage.Series["Default"].Points.AddXY
                            (0, numericUpDownHP.Value * KernelWorker.GetSelectedEnemyAttacksData.AttackPower / 16);
                    }
                    else if (_mainForm.comboBoxEnemyAttacksAttackType.SelectedIndex == 34) // Attack type "Everyone's Grudge)"
                    {
                        chartEADamage.Series["Default"].Points.Clear();
                        chartEADamage.Series["Default"].Points.AddXY
                            (0, KernelWorker.GetSelectedEnemyAttacksData.AttackPower * numericUpDownKilled.Value);
                    }
                    else if (_mainForm.comboBoxEnemyAttacksAttackType.SelectedIndex == 36) // Attack type "Physical Attack (Ignore VIT)"
                    {
                        chartEADamage.Series["Default"].Points.Clear();

                        a = (int)Math.Pow((int)numericUpDownSTR.Value, 2) / 16 + (int)numericUpDownSTR.Value;
                        b = a * 265 / 256;
                        c = b * KernelWorker.GetSelectedEnemyAttacksData.AttackPower / 16;

                        if (numericUpDownElemAtt.Value == 0)
                        {
                            chartEADamage.Series["Default"].Points.AddXY(0, c);
                        }
                        else
                        {
                            chartEADamage.Series["Default"].Points.AddXY
                                (0, Math.Floor(c + (c * numericUpDownElemAtt.Value / 10000)));
                        }
                    }
                    else
                    {
                        chartEADamage.Series["Default"].Points.Clear();
                        chartEADamage.Series["Default"].Points.AddXY(0, 0);
                    }
                }
                catch (Exception Exception)
                {
                    MessageBox.Show(Exception.ToString());
                }
            }
        }

        private void CurrentChartWorker()
        {
            KernelWorker.ReadEnemyAttacks(_mainForm.listBoxEnemyAttacks.SelectedIndex, KernelWorker.Kernel);
            try
            {
                //Basic Attack
                a = (int)Math.Pow((int)numericUpDownSTR.Value, 2) / 16 + (int)numericUpDownSTR.Value;
                b = a * (265 - (int)numericUpDownVIT.Value) / 256;
                c = b * KernelWorker.GetSelectedEnemyAttacksData.AttackPower / 16;

                //Magic Attack
                d = (int)numericUpDownMAG.Value + KernelWorker.GetSelectedEnemyAttacksData.AttackPower;
                e = d * (265 - (int)numericUpDownSPR.Value) / 4;
                f = e * KernelWorker.GetSelectedEnemyAttacksData.AttackPower / 256;

                if (_mainForm.comboBoxEnemyAttacksAttackType.SelectedIndex == 1) // Attack type "Basic Attack"
                {

                    chartEADamage.ChartAreas["ChartAreaEADamage"].AxisY.Title = "Damage";
                    chartEADamage.ChartAreas["ChartAreaEADamage"].AxisX.CustomLabels.Clear();
                    chartEADamage.ChartAreas["ChartAreaEADamage"].AxisX.CustomLabels.Add(-1D, 3D, "Physical Attack");
                    
                    if (numericUpDownElemAtt.Value == 0)
                    {
                        labelMAG.Visible = false;
                        labelHealMAG.Visible = false;
                        labelSPR.Visible = false;
                        labelElemDef.Visible = false;
                        labelHP.Visible = false;
                        labelVIT.Visible = true;
                        labelSTR.Visible = true;
                        labelKilled.Visible = false;
                        labelElemAtt.Visible = true;
                        numericUpDownMAG.Visible = false;
                        numericUpDownSPR.Visible = false;
                        numericUpDownHealMAG.Visible = false;
                        numericUpDownElemDef.Visible = false;
                        numericUpDownHP.Visible = false;
                        numericUpDownVIT.Visible = true;
                        numericUpDownSTR.Visible = true;
                        numericUpDownKilled.Visible = false;
                        numericUpDownElemAtt.Visible = true;
                        checkBoxDefault.Visible = true;

                        chartEADamage.Series["Current"].Points.Clear();
                        chartEADamage.Series["Current"].Points.AddXY(0, c);
                    }
                    else
                    {
                        labelMAG.Visible = false;
                        labelHealMAG.Visible = false;
                        labelSPR.Visible = false;
                        labelElemDef.Visible = true;
                        labelHP.Visible = false;
                        labelVIT.Visible = true;
                        labelSTR.Visible = true;
                        labelKilled.Visible = false;
                        labelElemAtt.Visible = true;
                        numericUpDownMAG.Visible = false;
                        numericUpDownSPR.Visible = false;
                        numericUpDownHealMAG.Visible = false;
                        numericUpDownElemDef.Visible = true;
                        numericUpDownHP.Visible = false;
                        numericUpDownVIT.Visible = true;
                        numericUpDownSTR.Visible = true;
                        numericUpDownKilled.Visible = false;
                        numericUpDownElemAtt.Visible = true;
                        checkBoxDefault.Visible = true;

                        chartEADamage.Series["Current"].Points.Clear();
                        chartEADamage.Series["Current"].Points.AddXY
                            (0, Math.Floor(c + (c * numericUpDownElemAtt.Value * (900 - numericUpDownElemDef.Value) / 10000)));
                    }
                }
                else if (_mainForm.comboBoxEnemyAttacksAttackType.SelectedIndex == 2) // Attack type "Magic Attack"
                {
                    labelMAG.Visible = true;
                    labelHealMAG.Visible = true;
                    labelSPR.Visible = true;
                    labelElemDef.Visible = true;
                    labelHP.Visible = false;
                    labelVIT.Visible = false;
                    labelSTR.Visible = false;
                    labelKilled.Visible = false;
                    labelElemAtt.Visible = false;
                    numericUpDownMAG.Visible = true;
                    numericUpDownSPR.Visible = true;
                    numericUpDownHealMAG.Visible = false;
                    numericUpDownElemDef.Visible = true;
                    numericUpDownHP.Visible = false;
                    numericUpDownVIT.Visible = false;
                    numericUpDownSTR.Visible = false;
                    numericUpDownKilled.Visible = false;
                    numericUpDownElemAtt.Visible = false;
                    checkBoxDefault.Visible = true;

                    chartEADamage.ChartAreas["ChartAreaEADamage"].AxisY.Title = "Damage";
                    chartEADamage.ChartAreas["ChartAreaEADamage"].AxisX.CustomLabels.Clear();
                    chartEADamage.ChartAreas["ChartAreaEADamage"].AxisX.CustomLabels.Add(-1D, 3D, "Magic Attack");

                    chartEADamage.Series["Current"].Points.Clear();
                    chartEADamage.Series["Current"].Points.AddXY
                        (0, Math.Floor(f * (900 - numericUpDownElemDef.Value) / 100));
                }
                else if (_mainForm.comboBoxEnemyAttacksAttackType.SelectedIndex == 8) //Attack type "Demi"
                {
                    labelMAG.Visible = false;
                    labelHealMAG.Visible = false;
                    labelSPR.Visible = false;
                    labelElemDef.Visible = false;
                    labelHP.Visible = true;
                    labelVIT.Visible = false;
                    labelSTR.Visible = false;
                    labelKilled.Visible = false;
                    labelElemAtt.Visible = false;
                    numericUpDownMAG.Visible = false;
                    numericUpDownSPR.Visible = false;
                    numericUpDownHealMAG.Visible = false;
                    numericUpDownElemDef.Visible = false;
                    numericUpDownHP.Visible = true;
                    numericUpDownVIT.Visible = false;
                    numericUpDownSTR.Visible = false;
                    numericUpDownKilled.Visible = false;
                    numericUpDownElemAtt.Visible = false;
                    checkBoxDefault.Visible = false;

                    chartEADamage.ChartAreas["ChartAreaEADamage"].AxisY.Title = "Damage";
                    chartEADamage.ChartAreas["ChartAreaEADamage"].AxisX.CustomLabels.Clear();
                    chartEADamage.ChartAreas["ChartAreaEADamage"].AxisX.CustomLabels.Add(-1D, 3D, "$ Magic Damage");

                    chartEADamage.Series["Current"].Points.Clear();
                    chartEADamage.Series["Current"].Points.AddXY
                        (0, Math.Floor(numericUpDownHP.Value * KernelWorker.GetSelectedEnemyAttacksData.AttackPower / 16));
                }
                else if (_mainForm.comboBoxEnemyAttacksAttackType.SelectedIndex == 3) //Attack type "Curative"
                {
                    labelMAG.Visible = false;
                    labelHealMAG.Visible = true;
                    labelSPR.Visible = false;
                    labelElemDef.Visible = false;
                    labelHP.Visible = false;
                    labelVIT.Visible = false;
                    labelSTR.Visible = false;
                    labelKilled.Visible = false;
                    labelElemAtt.Visible = false;
                    numericUpDownMAG.Visible = false;
                    numericUpDownSPR.Visible = false;
                    numericUpDownHealMAG.Visible = true;
                    numericUpDownElemDef.Visible = false;
                    numericUpDownHP.Visible = false;
                    numericUpDownVIT.Visible = false;
                    numericUpDownSTR.Visible = false;
                    numericUpDownKilled.Visible = false;
                    numericUpDownElemAtt.Visible = false;
                    checkBoxDefault.Visible = true;

                    chartEADamage.ChartAreas["ChartAreaEADamage"].AxisY.Title = "Healing Amount";
                    chartEADamage.ChartAreas["ChartAreaEADamage"].AxisX.CustomLabels.Clear();
                    chartEADamage.ChartAreas["ChartAreaEADamage"].AxisX.CustomLabels.Add(-1D, 3D, "Curative Magic");

                    chartEADamage.Series["Current"].Points.Clear();
                    chartEADamage.Series["Current"].Points.AddXY
                        (0, Math.Floor((KernelWorker.GetSelectedEnemyAttacksData.AttackPower + numericUpDownHealMAG.Value) * KernelWorker.GetSelectedEnemyAttacksData.AttackPower / 2));
                }
                else if (_mainForm.comboBoxEnemyAttacksAttackType.SelectedIndex == 22) // Attack type "Magic Attack (Ignore Target SPR)"
                {
                    d = (int)numericUpDownMAG.Value + KernelWorker.GetSelectedEnemyAttacksData.AttackPower;
                    e = d * 265 / 4;
                    f = e * KernelWorker.GetSelectedEnemyAttacksData.AttackPower / 256;

                    labelMAG.Visible = true;
                    labelHealMAG.Visible = true;
                    labelSPR.Visible = true;
                    labelElemDef.Visible = true;
                    labelHP.Visible = false;
                    labelVIT.Visible = false;
                    labelSTR.Visible = false;
                    labelKilled.Visible = false;
                    labelElemAtt.Visible = false;
                    numericUpDownMAG.Visible = true;
                    numericUpDownSPR.Visible = true;
                    numericUpDownHealMAG.Visible = false;
                    numericUpDownElemDef.Visible = true;
                    numericUpDownHP.Visible = false;
                    numericUpDownVIT.Visible = false;
                    numericUpDownSTR.Visible = false;
                    numericUpDownKilled.Visible = false;
                    numericUpDownElemAtt.Visible = false;
                    checkBoxDefault.Visible = true;

                    chartEADamage.ChartAreas["ChartAreaEADamage"].AxisY.Title = "Damage";
                    chartEADamage.ChartAreas["ChartAreaEADamage"].AxisX.CustomLabels.Clear();
                    chartEADamage.ChartAreas["ChartAreaEADamage"].AxisX.CustomLabels.Add(-1D, 3D, "Magic Attack (Ignore SPR)");

                    chartEADamage.Series["Current"].Points.Clear();
                    chartEADamage.Series["Current"].Points.AddXY
                        (0, Math.Floor(f * (900 - numericUpDownElemDef.Value) / 100));
                }
                else if (_mainForm.comboBoxEnemyAttacksAttackType.SelectedIndex == 27) // Attack type "Fixed Damage)"
                {
                    labelMAG.Visible = false;
                    labelHealMAG.Visible = false;
                    labelSPR.Visible = false;
                    labelElemDef.Visible = false;
                    labelHP.Visible = false;
                    labelVIT.Visible = false;
                    labelSTR.Visible = false;
                    labelKilled.Visible = false;
                    labelElemAtt.Visible = false;
                    numericUpDownMAG.Visible = false;
                    numericUpDownSPR.Visible = false;
                    numericUpDownHealMAG.Visible = false;
                    numericUpDownElemDef.Visible = false;
                    numericUpDownHP.Visible = false;
                    numericUpDownVIT.Visible = false;
                    numericUpDownSTR.Visible = false;
                    numericUpDownKilled.Visible = false;
                    numericUpDownElemAtt.Visible = false;
                    checkBoxDefault.Visible = false;

                    chartEADamage.ChartAreas["ChartAreaEADamage"].AxisY.Title = "Damage";
                    chartEADamage.ChartAreas["ChartAreaEADamage"].AxisX.CustomLabels.Clear();
                    chartEADamage.ChartAreas["ChartAreaEADamage"].AxisX.CustomLabels.Add(-1D, 3D, "Fixed Damage");

                    chartEADamage.Series["Current"].Points.Clear();
                    chartEADamage.Series["Current"].Points.AddXY
                        (0, KernelWorker.GetSelectedEnemyAttacksData.AttackPower * 100 - KernelWorker.GetSelectedEnemyAttacksData.AttackParam);
                }
                else if (_mainForm.comboBoxEnemyAttacksAttackType.SelectedIndex == 28) // Attack type "Target Current HP - 1)"
                {
                    labelMAG.Visible = false;
                    labelHealMAG.Visible = false;
                    labelSPR.Visible = false;
                    labelElemDef.Visible = false;
                    labelHP.Visible = true;
                    labelVIT.Visible = false;
                    labelSTR.Visible = false;
                    labelKilled.Visible = false;
                    labelElemAtt.Visible = false;
                    numericUpDownMAG.Visible = false;
                    numericUpDownSPR.Visible = false;
                    numericUpDownHealMAG.Visible = false;
                    numericUpDownElemDef.Visible = false;
                    numericUpDownHP.Visible = true;
                    numericUpDownVIT.Visible = false;
                    numericUpDownSTR.Visible = false;
                    numericUpDownKilled.Visible = false;
                    numericUpDownElemAtt.Visible = false;
                    checkBoxDefault.Visible = false;

                    chartEADamage.ChartAreas["ChartAreaEADamage"].AxisY.Title = "Damage";
                    chartEADamage.ChartAreas["ChartAreaEADamage"].AxisX.CustomLabels.Clear();
                    chartEADamage.ChartAreas["ChartAreaEADamage"].AxisX.CustomLabels.Add(-1D, 3D, "Target Current HP - 1");

                    chartEADamage.Series["Current"].Points.Clear();
                    chartEADamage.Series["Current"].Points.AddXY
                        (0, numericUpDownHP.Value - 1);
                }
                else if (_mainForm.comboBoxEnemyAttacksAttackType.SelectedIndex == 7) // Attack type "% Damage)"
                {
                    labelMAG.Visible = false;
                    labelHealMAG.Visible = false;
                    labelSPR.Visible = false;
                    labelElemDef.Visible = false;
                    labelHP.Visible = true;
                    labelVIT.Visible = false;
                    labelSTR.Visible = false;
                    labelKilled.Visible = false;
                    labelElemAtt.Visible = false;
                    numericUpDownMAG.Visible = false;
                    numericUpDownSPR.Visible = false;
                    numericUpDownHealMAG.Visible = false;
                    numericUpDownElemDef.Visible = false;
                    numericUpDownHP.Visible = true;
                    numericUpDownVIT.Visible = false;
                    numericUpDownSTR.Visible = false;
                    numericUpDownKilled.Visible = false;
                    numericUpDownElemAtt.Visible = false;
                    checkBoxDefault.Visible = false;

                    chartEADamage.ChartAreas["ChartAreaEADamage"].AxisY.Title = "Damage";
                    chartEADamage.ChartAreas["ChartAreaEADamage"].AxisX.CustomLabels.Clear();
                    chartEADamage.ChartAreas["ChartAreaEADamage"].AxisX.CustomLabels.Add(-1D, 3D, "% Physical Damage");

                    chartEADamage.Series["Current"].Points.Clear();
                    chartEADamage.Series["Current"].Points.AddXY
                        (0, numericUpDownHP.Value * KernelWorker.GetSelectedEnemyAttacksData.AttackPower / 16);
                }
                else if (_mainForm.comboBoxEnemyAttacksAttackType.SelectedIndex == 34) // Attack type "Everyone's Grudge)"
                {
                    labelMAG.Visible = false;
                    labelHealMAG.Visible = false;
                    labelSPR.Visible = false;
                    labelElemDef.Visible = false;
                    labelHP.Visible = true;
                    labelVIT.Visible = false;
                    labelSTR.Visible = false;
                    labelKilled.Visible = true;
                    labelElemAtt.Visible = false;
                    numericUpDownMAG.Visible = false;
                    numericUpDownSPR.Visible = false;
                    numericUpDownHealMAG.Visible = false;
                    numericUpDownElemDef.Visible = false;
                    numericUpDownHP.Visible = true;
                    numericUpDownVIT.Visible = false;
                    numericUpDownSTR.Visible = false;
                    numericUpDownKilled.Visible = true;
                    numericUpDownElemAtt.Visible = false;
                    checkBoxDefault.Visible = false;

                    chartEADamage.ChartAreas["ChartAreaEADamage"].AxisY.Title = "Damage";
                    chartEADamage.ChartAreas["ChartAreaEADamage"].AxisX.CustomLabels.Clear();
                    chartEADamage.ChartAreas["ChartAreaEADamage"].AxisX.CustomLabels.Add(-1D, 3D, "Everyone's Grudge");

                    chartEADamage.Series["Current"].Points.Clear();
                    chartEADamage.Series["Current"].Points.AddXY
                        (0, KernelWorker.GetSelectedEnemyAttacksData.AttackPower * numericUpDownKilled.Value);
                }
                else if (_mainForm.comboBoxEnemyAttacksAttackType.SelectedIndex == 36) // Attack type "Physical Attack (Ignore VIT)"
                {
                    chartEADamage.ChartAreas["ChartAreaEADamage"].AxisY.Title = "Damage";
                    chartEADamage.ChartAreas["ChartAreaEADamage"].AxisX.CustomLabels.Clear();
                    chartEADamage.ChartAreas["ChartAreaEADamage"].AxisX.CustomLabels.Add(-1D, 3D, "Physical Attack (Ignore VIT)");

                    a = (int)Math.Pow((int)numericUpDownSTR.Value, 2) / 16 + (int)numericUpDownSTR.Value;
                    b = a * (265 - 0) / 256;
                    c = b * KernelWorker.GetSelectedEnemyAttacksData.AttackPower / 16;

                    if (numericUpDownElemAtt.Value == 0)
                    {
                        labelMAG.Visible = false;
                        labelHealMAG.Visible = false;
                        labelSPR.Visible = false;
                        labelElemDef.Visible = false;
                        labelHP.Visible = false;
                        labelVIT.Visible = false;
                        labelSTR.Visible = true;
                        labelKilled.Visible = false;
                        labelElemAtt.Visible = true;
                        numericUpDownMAG.Visible = false;
                        numericUpDownSPR.Visible = false;
                        numericUpDownHealMAG.Visible = false;
                        numericUpDownElemDef.Visible = false;
                        numericUpDownHP.Visible = false;
                        numericUpDownVIT.Visible = false;
                        numericUpDownSTR.Visible = true;
                        numericUpDownKilled.Visible = false;
                        numericUpDownElemAtt.Visible = true;
                        checkBoxDefault.Visible = true;

                        chartEADamage.Series["Current"].Points.Clear();
                        chartEADamage.Series["Current"].Points.AddXY(0, c);
                    }
                    else
                    {
                        labelMAG.Visible = false;
                        labelHealMAG.Visible = false;
                        labelSPR.Visible = false;
                        labelElemDef.Visible = true;
                        labelHP.Visible = false;
                        labelVIT.Visible = false;
                        labelSTR.Visible = true;
                        labelKilled.Visible = false;
                        labelElemAtt.Visible = true;
                        numericUpDownMAG.Visible = false;
                        numericUpDownSPR.Visible = false;
                        numericUpDownHealMAG.Visible = false;
                        numericUpDownElemDef.Visible = true;
                        numericUpDownHP.Visible = false;
                        numericUpDownVIT.Visible = false;
                        numericUpDownSTR.Visible = true;
                        numericUpDownKilled.Visible = false;
                        numericUpDownElemAtt.Visible = true;
                        checkBoxDefault.Visible = true;

                        chartEADamage.Series["Current"].Points.Clear();
                        chartEADamage.Series["Current"].Points.AddXY
                            (0, Math.Floor(c + (c * numericUpDownElemAtt.Value * (900 - numericUpDownElemDef.Value) / 10000)));
                    }
                }
                else
                {
                    labelMAG.Visible = false;
                    labelHealMAG.Visible = false;
                    labelSPR.Visible = false;
                    labelElemDef.Visible = false;
                    labelHP.Visible = false;
                    labelVIT.Visible = false;
                    labelSTR.Visible = false;
                    labelKilled.Visible = false;
                    labelElemAtt.Visible = false;
                    numericUpDownMAG.Visible = false;
                    numericUpDownSPR.Visible = false;
                    numericUpDownHealMAG.Visible = false;
                    numericUpDownElemDef.Visible = false;
                    numericUpDownHP.Visible = false;
                    numericUpDownVIT.Visible = false;
                    numericUpDownSTR.Visible = false;
                    numericUpDownKilled.Visible = false;
                    numericUpDownElemAtt.Visible = false;
                    checkBoxDefault.Visible = false;

                    chartEADamage.ChartAreas["ChartAreaEADamage"].AxisY.Title = "Damage";
                    chartEADamage.ChartAreas["ChartAreaEADamage"].AxisX.CustomLabels.Clear();
                    chartEADamage.ChartAreas["ChartAreaEADamage"].AxisX.CustomLabels.Add(-1D, 3D, "Unsupported Attack Type");

                    chartEADamage.Series["Current"].Points.Clear();
                    chartEADamage.Series["Current"].Points.AddXY(0, 0);
                }
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }
        }

        #endregion

        #region Changed values

        private void numericUpDownEADamage_ValueChanged(object sender, EventArgs e)
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
