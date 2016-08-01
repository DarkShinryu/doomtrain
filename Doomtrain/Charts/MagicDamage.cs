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
            MagicDamageChartWorker();

            _mainForm.numericUpDownMagicSpellPower.ValueChanged += new EventHandler(this.numericUpDownMagicDamage_ValueChanged);
            numericUpDownAttMAG.ValueChanged += new EventHandler(numericUpDownMagicDamage_ValueChanged);
            numericUpDownSPR.ValueChanged += new EventHandler(numericUpDownMagicDamage_ValueChanged);
            numericUpDownHealMAG.ValueChanged += new EventHandler(numericUpDownMagicDamage_ValueChanged);
            numericUpDownElemDef.ValueChanged += new EventHandler(numericUpDownMagicDamage_ValueChanged);
            numericUpDownHP.ValueChanged += new EventHandler(numericUpDownMagicDamage_ValueChanged);
            checkBoxDefault.CheckedChanged += new EventHandler(Default_CheckedChanged);
        }

        private readonly mainForm _mainForm;


        #region Moving chart

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

        private void buttonInfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The values here are only used for the chart and they have no connection to the kernel file. " +
                "You need to change the values manually.\n\n" +
                "Attacker MAG = The MAG value of the offensive magic caster.\n" +
                "Target SPR = The SPR value of the target.\n" +
                "Healer MAG = The MAG value of the healing magic caster.\n" +
                "Elem Defense = The elemental defense of the target.", "Info",
                MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }



        #region Calculation variables declaration

        private int a, b, c;

        #endregion

        #region Read chart values        

        private void MagicDamageChartWorker()
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


                    chartMagicDamage.ChartAreas["ChartAreaMagicDamage"].AxisY.Title = "Damage";

                    chartMagicDamage.Series["Default"].Points.AddXY
                        (0, c * (900 - numericUpDownElemDef.Value) / 100);
                    chartMagicDamage.Series["Default"].Points.AddXY
                        (1, 0);
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


                    chartMagicDamage.ChartAreas["ChartAreaMagicDamage"].AxisY.Title = "Damage";

                    chartMagicDamage.Series["Default"].Points.AddXY
                        (0, numericUpDownHP.Value * KernelWorker.GetSelectedMagicData.SpellPower / 16);
                    chartMagicDamage.Series["Default"].Points.AddXY
                        (1, 0);
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


                    chartMagicDamage.ChartAreas["ChartAreaMagicDamage"].AxisY.Title = "Healing Amount";

                    chartMagicDamage.Series["Default"].Points.AddXY
                        (0, 0);
                    chartMagicDamage.Series["Default"].Points.AddXY
                        (1, (KernelWorker.GetSelectedMagicData.SpellPower + numericUpDownHealMAG.Value) * KernelWorker.GetSelectedMagicData.SpellPower / 2);
                }
                else
                {
                    MessageBox.Show("The magic chart only supports the following attack types:\n" +
                        "Magic Attack;\n" +
                        "Demi;\n" +
                        "Curative.\n", "Unsupported attack type", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                }
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }


            KernelWorker.ReadMagic(_mainForm.listBoxMagic.SelectedIndex, KernelWorker.Kernel);
            try
            {
                a = (int)numericUpDownAttMAG.Value + KernelWorker.GetSelectedMagicData.SpellPower;
                b = a * (265 - (int)numericUpDownSPR.Value) / 4;
                c = b * KernelWorker.GetSelectedMagicData.SpellPower / 256;
                if (_mainForm.comboBoxMagicAttackType.SelectedIndex == 3)
                {
                    chartMagicDamage.Series["Current"].Points.AddXY
                        (0, 0);
                    chartMagicDamage.Series["Current"].Points.AddXY
                        (1, (KernelWorker.GetSelectedMagicData.SpellPower + (int)numericUpDownHealMAG.Value) * KernelWorker.GetSelectedMagicData.SpellPower / 2);
                }
                else
                {
                    chartMagicDamage.Series["Current"].Points.AddXY
                        (0, c * (900 - numericUpDownElemDef.Value) / 100);
                    chartMagicDamage.Series["Current"].Points.AddXY
                        (1, 0);
                }

            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }
        }


        #endregion

        #region Change chart values

        private void numericUpDownMagicDamage_ValueChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                KernelWorker.ReadMagic(_mainForm.listBoxMagic.SelectedIndex, KernelWorker.BackupKernel);
                try
                {
                    if (checkBoxDefault.Checked == true)
                    {
                        a = (int)numericUpDownAttMAG.Value + KernelWorker.GetSelectedMagicData.SpellPower;
                        b = a * (265 - (int)numericUpDownSPR.Value) / 4;
                        c = b * KernelWorker.GetSelectedMagicData.SpellPower / 256;


                        chartMagicDamage.Series["Default"].Points.Clear();

                        chartMagicDamage.Series["Default"].Points.AddXY
                            (0, c * (900 - numericUpDownElemDef.Value) / 100);
                        chartMagicDamage.Series["Default"].Points.AddXY
                            (1, KernelWorker.GetSelectedMagicData.SpellPower * KernelWorker.GetSelectedMagicData.SpellPower / 2);
                    }

                    else if (checkBoxDefault.Checked == false)
                    {
                        a = KernelWorker.GetSelectedMagicData.SpellPower;
                        b = a * 265 / 4;
                        c = b * KernelWorker.GetSelectedMagicData.SpellPower / 256;


                        chartMagicDamage.Series["Default"].Points.Clear();

                        chartMagicDamage.Series["Default"].Points.AddXY
                            (0, c);
                        chartMagicDamage.Series["Default"].Points.AddXY
                            (1, KernelWorker.GetSelectedMagicData.SpellPower * KernelWorker.GetSelectedMagicData.SpellPower / 2);
                    }
                }
                catch (Exception Exception)
                {
                    MessageBox.Show(Exception.ToString());
                }


                KernelWorker.ReadMagic(_mainForm.listBoxMagic.SelectedIndex, KernelWorker.Kernel);
                try
                {
                    a = (int)numericUpDownAttMAG.Value + KernelWorker.GetSelectedMagicData.SpellPower;
                    b = a * (265 - (int)numericUpDownSPR.Value) / 4;
                    c = b * KernelWorker.GetSelectedMagicData.SpellPower / 256;


                    chartMagicDamage.Series["Current"].Points.Clear();

                    chartMagicDamage.Series["Current"].Points.AddXY
                        (0, c * (900 - numericUpDownElemDef.Value) / 100);
                    chartMagicDamage.Series["Current"].Points.AddXY
                        (1, (KernelWorker.GetSelectedMagicData.SpellPower + (int)numericUpDownHealMAG.Value) * KernelWorker.GetSelectedMagicData.SpellPower / 2);
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
                KernelWorker.ReadMagic(_mainForm.listBoxMagic.SelectedIndex, KernelWorker.BackupKernel);
                try
                {
                    if (checkBoxDefault.Checked == true)
                    {
                        a = (int)numericUpDownAttMAG.Value + KernelWorker.GetSelectedMagicData.SpellPower;
                        b = a * (265 - (int)numericUpDownSPR.Value) / 4;
                        c = b * KernelWorker.GetSelectedMagicData.SpellPower / 256;


                        chartMagicDamage.Series["Default"].Points.Clear();

                        chartMagicDamage.Series["Default"].Points.AddXY
                            (0, c * (900 - numericUpDownElemDef.Value) / 100);
                        chartMagicDamage.Series["Default"].Points.AddXY
                            (1, KernelWorker.GetSelectedMagicData.SpellPower * KernelWorker.GetSelectedMagicData.SpellPower / 2);
                    }
                    else if (checkBoxDefault.Checked == false)
                    {
                        a = KernelWorker.GetSelectedMagicData.SpellPower;
                        b = a * 265 / 4;
                        c = b * KernelWorker.GetSelectedMagicData.SpellPower / 256;


                        chartMagicDamage.Series["Default"].Points.Clear();

                        chartMagicDamage.Series["Default"].Points.AddXY
                            (0, c);
                        chartMagicDamage.Series["Default"].Points.AddXY
                            (1, KernelWorker.GetSelectedMagicData.SpellPower * KernelWorker.GetSelectedMagicData.SpellPower / 2);
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
