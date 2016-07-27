using System;
using System.Drawing;
using System.Windows.Forms;

namespace Doomtrain.Charts
{
    public partial class CharLUCK : Form
    {
        public CharLUCK(mainForm mainForm)
        {
            _mainForm = mainForm;
            InitializeComponent();
            LuckChartWorker();

            _mainForm.numericUpDownCharLUCK1.ValueChanged += new EventHandler(this.numericUpDownCharLUCK_ValueChanged);
            _mainForm.numericUpDownCharLUCK2.ValueChanged += new EventHandler(this.numericUpDownCharLUCK_ValueChanged);
            _mainForm.numericUpDownCharLUCK3.ValueChanged += new EventHandler(this.numericUpDownCharLUCK_ValueChanged);
            _mainForm.numericUpDownCharLUCK4.ValueChanged += new EventHandler(this.numericUpDownCharLUCK_ValueChanged);

            numericUpDownMagicValue.ValueChanged += new EventHandler(numericUpDownCharLUCK_ValueChanged);
            numericUpDownMagicCount.ValueChanged += new EventHandler(numericUpDownCharLUCK_ValueChanged);
            numericUpDownStatBonus.ValueChanged += new EventHandler(numericUpDownCharLUCK_ValueChanged);
            numericUpDownPercent.ValueChanged += new EventHandler(numericUpDownCharLUCK_ValueChanged);
            numericUpDownX.ValueChanged += new EventHandler(numericUpDownCharLUCK_ValueChanged);

            checkBoxDefault.CheckedChanged += new EventHandler(Default_CheckedChanged);
        }

        private readonly mainForm _mainForm;


        #region Moving chart

        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        private void chartLUCK_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void chartLUCK_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void chartLUCK_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        #endregion

        private void buttonInfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The values here are only used for the chart and they have no connection to the kernel file.\n" +
                "You need to change the values manually.\n\n" +
                "X = Currently unknown, probably 0.\n" +
                "Magic J-Value = The junction value of the junctioned magic.\n" +
                "Magic Count = The amount of j-magic that the character holds.\n" +
                "Stat Bonus = Permanent bonus gained from X bonus skills and Devour.\n" +
                "Percent Modifier = Is 100 + any % bonuses added.", "Info",
                MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }


        #region Read Chart Values        

        private void LuckChartWorker()
        {
            KernelWorker.ReadCharacters(_mainForm.listBoxCharacters.SelectedIndex, KernelWorker.BackupKernel);
            try
            {
                chartLUCK.Series["Default"].Points.AddXY
                    (0, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                    1 / KernelWorker.GetSelectedCharactersData.LUCK2 - 1 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                    1 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                chartLUCK.Series["Default"].Points.AddXY
                    (1, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                    10 / KernelWorker.GetSelectedCharactersData.LUCK2 - 10 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                    10 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                chartLUCK.Series["Default"].Points.AddXY
                    (2, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                    20 / KernelWorker.GetSelectedCharactersData.LUCK2 - 20 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                    20 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                chartLUCK.Series["Default"].Points.AddXY
                    (3, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                    30 / KernelWorker.GetSelectedCharactersData.LUCK2 - 30 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                    30 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                chartLUCK.Series["Default"].Points.AddXY
                    (4, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                    40 / KernelWorker.GetSelectedCharactersData.LUCK2 - 40 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                    40 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                chartLUCK.Series["Default"].Points.AddXY
                    (5, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                    50 / KernelWorker.GetSelectedCharactersData.LUCK2 - 50 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                    50 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                chartLUCK.Series["Default"].Points.AddXY
                    (6, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                    60 / KernelWorker.GetSelectedCharactersData.LUCK2 - 60 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                    60 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                chartLUCK.Series["Default"].Points.AddXY
                    (7, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                    70 / KernelWorker.GetSelectedCharactersData.LUCK2 - 70 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                    70 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                chartLUCK.Series["Default"].Points.AddXY
                    (8, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                    80 / KernelWorker.GetSelectedCharactersData.LUCK2 - 80 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                    80 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                chartLUCK.Series["Default"].Points.AddXY
                    (9, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                    90 / KernelWorker.GetSelectedCharactersData.LUCK2 - 90 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                    90 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                chartLUCK.Series["Default"].Points.AddXY
                    (10, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                    100 / KernelWorker.GetSelectedCharactersData.LUCK2 - 100 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                    100 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * numericUpDownPercent.Value) / 100) * 100 / 100);
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }


            KernelWorker.ReadCharacters(_mainForm.listBoxCharacters.SelectedIndex, KernelWorker.Kernel);
            try
            {
                chartLUCK.Series["Current"].Points.AddXY
                    (0, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                    1 / KernelWorker.GetSelectedCharactersData.LUCK2 - 1 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                    1 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                chartLUCK.Series["Current"].Points.AddXY
                    (1, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                    10 / KernelWorker.GetSelectedCharactersData.LUCK2 - 10 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                    10 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                chartLUCK.Series["Current"].Points.AddXY
                    (2, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                    20 / KernelWorker.GetSelectedCharactersData.LUCK2 - 20 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                    20 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                chartLUCK.Series["Current"].Points.AddXY
                    (3, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                    30 / KernelWorker.GetSelectedCharactersData.LUCK2 - 30 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                    30 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                chartLUCK.Series["Current"].Points.AddXY
                    (4, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                    40 / KernelWorker.GetSelectedCharactersData.LUCK2 - 40 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                    40 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                chartLUCK.Series["Current"].Points.AddXY
                    (5, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                    50 / KernelWorker.GetSelectedCharactersData.LUCK2 - 50 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                    50 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                chartLUCK.Series["Current"].Points.AddXY
                    (6, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                    60 / KernelWorker.GetSelectedCharactersData.LUCK2 - 60 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                    60 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                chartLUCK.Series["Current"].Points.AddXY
                    (7, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                    70 / KernelWorker.GetSelectedCharactersData.LUCK2 - 70 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                    70 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                chartLUCK.Series["Current"].Points.AddXY
                    (8, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                    80 / KernelWorker.GetSelectedCharactersData.LUCK2 - 80 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                    80 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                chartLUCK.Series["Current"].Points.AddXY
                    (9, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                    90 / KernelWorker.GetSelectedCharactersData.LUCK2 - 90 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                    90 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                chartLUCK.Series["Current"].Points.AddXY
                    (10, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                    100 / KernelWorker.GetSelectedCharactersData.LUCK2 - 100 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                    100 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * numericUpDownPercent.Value) / 100) * 100 / 100);
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }
        }

        #endregion

        #region Change chart values

        private void numericUpDownCharLUCK_ValueChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                KernelWorker.ReadCharacters(_mainForm.listBoxCharacters.SelectedIndex, KernelWorker.BackupKernel);
                try
                {
                    if (checkBoxDefault.Checked == true)
                    {
                        chartLUCK.Series["Default"].Points.Clear();

                        chartLUCK.Series["Default"].Points.AddXY
                            (0, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                            1 / KernelWorker.GetSelectedCharactersData.LUCK2 - 1 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                            1 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                        chartLUCK.Series["Default"].Points.AddXY
                            (1, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                            10 / KernelWorker.GetSelectedCharactersData.LUCK2 - 10 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                            10 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                        chartLUCK.Series["Default"].Points.AddXY
                            (2, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                            20 / KernelWorker.GetSelectedCharactersData.LUCK2 - 20 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                            20 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                        chartLUCK.Series["Default"].Points.AddXY
                            (3, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                            30 / KernelWorker.GetSelectedCharactersData.LUCK2 - 30 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                            30 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                        chartLUCK.Series["Default"].Points.AddXY
                            (4, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                            40 / KernelWorker.GetSelectedCharactersData.LUCK2 - 40 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                            40 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                        chartLUCK.Series["Default"].Points.AddXY
                            (5, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                            50 / KernelWorker.GetSelectedCharactersData.LUCK2 - 50 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                            50 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                        chartLUCK.Series["Default"].Points.AddXY
                            (6, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                            60 / KernelWorker.GetSelectedCharactersData.LUCK2 - 60 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                            60 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                        chartLUCK.Series["Default"].Points.AddXY
                            (7, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                            70 / KernelWorker.GetSelectedCharactersData.LUCK2 - 70 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                            70 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                        chartLUCK.Series["Default"].Points.AddXY
                            (8, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                            80 / KernelWorker.GetSelectedCharactersData.LUCK2 - 80 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                            80 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                        chartLUCK.Series["Default"].Points.AddXY
                            (9, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                            90 / KernelWorker.GetSelectedCharactersData.LUCK2 - 90 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                            90 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                        chartLUCK.Series["Default"].Points.AddXY
                            (10, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                            100 / KernelWorker.GetSelectedCharactersData.LUCK2 - 100 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                            100 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * numericUpDownPercent.Value) / 100) * 100 / 100);
                    }
                    else if (checkBoxDefault.Checked == false)
                    {
                        chartLUCK.Series["Default"].Points.Clear();

                        chartLUCK.Series["Default"].Points.AddXY
                            (0, Math.Ceiling(((0.0 + (0 * 0) / 100 + 0 +
                            1 / KernelWorker.GetSelectedCharactersData.LUCK2 - 1 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                            1 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * 100) / 100) * 100 / 100);

                        chartLUCK.Series["Default"].Points.AddXY
                            (1, Math.Ceiling(((0.0 + (0 * 0) / 100 + 0 +
                            10 / KernelWorker.GetSelectedCharactersData.LUCK2 - 10 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                            10 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * 100) / 100) * 100 / 100);

                        chartLUCK.Series["Default"].Points.AddXY
                            (2, Math.Ceiling(((0.0 + (0 * 0) / 100 + 0 +
                            20 / KernelWorker.GetSelectedCharactersData.LUCK2 - 20 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                            20 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * 100) / 100) * 100 / 100);

                        chartLUCK.Series["Default"].Points.AddXY
                            (3, Math.Ceiling(((0.0 + (0 * 0) / 100 + 0 +
                            30 / KernelWorker.GetSelectedCharactersData.LUCK2 - 30 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                            30 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * 100) / 100) * 100 / 100);

                        chartLUCK.Series["Default"].Points.AddXY
                            (4, Math.Ceiling(((0.0 + (0 * 0) / 100 + 0 +
                            40 / KernelWorker.GetSelectedCharactersData.LUCK2 - 40 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                            40 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * 100) / 100) * 100 / 100);

                        chartLUCK.Series["Default"].Points.AddXY
                            (5, Math.Ceiling(((0.0 + (0 * 0) / 100 + 0 +
                            50 / KernelWorker.GetSelectedCharactersData.LUCK2 - 50 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                            50 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * 100) / 100) * 100 / 100);

                        chartLUCK.Series["Default"].Points.AddXY
                            (6, Math.Ceiling(((0.0 + (0 * 0) / 100 + 0 +
                            60 / KernelWorker.GetSelectedCharactersData.LUCK2 - 60 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                            60 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * 100) / 100) * 100 / 100);

                        chartLUCK.Series["Default"].Points.AddXY
                            (7, Math.Ceiling(((0.0 + (0 * 0) / 100 + 0 +
                            70 / KernelWorker.GetSelectedCharactersData.LUCK2 - 70 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                            70 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * 100) / 100) * 100 / 100);

                        chartLUCK.Series["Default"].Points.AddXY
                            (8, Math.Ceiling(((0.0 + (0 * 0) / 100 + 0 +
                            80 / KernelWorker.GetSelectedCharactersData.LUCK2 - 80 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                            80 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * 100) / 100) * 100 / 100);

                        chartLUCK.Series["Default"].Points.AddXY
                            (9, Math.Ceiling(((0.0 + (0 * 0) / 100 + 0 +
                            90 / KernelWorker.GetSelectedCharactersData.LUCK2 - 90 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                            90 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * 100) / 100) * 100 / 100);

                        chartLUCK.Series["Default"].Points.AddXY
                            (10, Math.Ceiling(((0.0 + (0 * 0) / 100 + 0 +
                            100 / KernelWorker.GetSelectedCharactersData.LUCK2 - 100 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                            100 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * 100) / 100) * 100 / 100);
                    }

                }
                catch (Exception Exception)
                {
                    MessageBox.Show(Exception.ToString());
                }


                KernelWorker.ReadCharacters(_mainForm.listBoxCharacters.SelectedIndex, KernelWorker.Kernel);
                try
                {
                    chartLUCK.Series["Current"].Points.Clear();

                    chartLUCK.Series["Current"].Points.AddXY
                        (0, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                        1 / KernelWorker.GetSelectedCharactersData.LUCK2 - 1 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                        1 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                    chartLUCK.Series["Current"].Points.AddXY
                        (1, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                        10 / KernelWorker.GetSelectedCharactersData.LUCK2 - 10 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                        10 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                    chartLUCK.Series["Current"].Points.AddXY
                        (2, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                        20 / KernelWorker.GetSelectedCharactersData.LUCK2 - 20 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                        20 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                    chartLUCK.Series["Current"].Points.AddXY
                        (3, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                        30 / KernelWorker.GetSelectedCharactersData.LUCK2 - 30 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                        30 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                    chartLUCK.Series["Current"].Points.AddXY
                        (4, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                        40 / KernelWorker.GetSelectedCharactersData.LUCK2 - 40 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                        40 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                    chartLUCK.Series["Current"].Points.AddXY
                        (5, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                        50 / KernelWorker.GetSelectedCharactersData.LUCK2 - 50 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                        50 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                    chartLUCK.Series["Current"].Points.AddXY
                        (6, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                        60 / KernelWorker.GetSelectedCharactersData.LUCK2 - 60 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                        60 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                    chartLUCK.Series["Current"].Points.AddXY
                        (7, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                        70 / KernelWorker.GetSelectedCharactersData.LUCK2 - 70 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                        70 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                    chartLUCK.Series["Current"].Points.AddXY
                        (8, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                        80 / KernelWorker.GetSelectedCharactersData.LUCK2 - 80 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                        80 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                    chartLUCK.Series["Current"].Points.AddXY
                        (9, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                        90 / KernelWorker.GetSelectedCharactersData.LUCK2 - 90 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                        90 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                    chartLUCK.Series["Current"].Points.AddXY
                        (10, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                        100 / KernelWorker.GetSelectedCharactersData.LUCK2 - 100 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                        100 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * numericUpDownPercent.Value) / 100) * 100 / 100);
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
                KernelWorker.ReadCharacters(_mainForm.listBoxCharacters.SelectedIndex, KernelWorker.BackupKernel);
                try
                {
                    if (checkBoxDefault.Checked == true)
                    {
                        chartLUCK.Series["Default"].Points.Clear();

                        chartLUCK.Series["Default"].Points.AddXY
                            (0, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                            1 / KernelWorker.GetSelectedCharactersData.LUCK2 - 1 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                            1 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                        chartLUCK.Series["Default"].Points.AddXY
                            (1, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                            10 / KernelWorker.GetSelectedCharactersData.LUCK2 - 10 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                            10 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                        chartLUCK.Series["Default"].Points.AddXY
                            (2, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                            20 / KernelWorker.GetSelectedCharactersData.LUCK2 - 20 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                            20 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                        chartLUCK.Series["Default"].Points.AddXY
                            (3, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                            30 / KernelWorker.GetSelectedCharactersData.LUCK2 - 30 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                            30 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                        chartLUCK.Series["Default"].Points.AddXY
                            (4, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                            40 / KernelWorker.GetSelectedCharactersData.LUCK2 - 40 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                            40 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                        chartLUCK.Series["Default"].Points.AddXY
                            (5, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                            50 / KernelWorker.GetSelectedCharactersData.LUCK2 - 50 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                            50 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                        chartLUCK.Series["Default"].Points.AddXY
                            (6, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                            60 / KernelWorker.GetSelectedCharactersData.LUCK2 - 60 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                            60 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                        chartLUCK.Series["Default"].Points.AddXY
                            (7, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                            70 / KernelWorker.GetSelectedCharactersData.LUCK2 - 70 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                            70 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                        chartLUCK.Series["Default"].Points.AddXY
                            (8, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                            80 / KernelWorker.GetSelectedCharactersData.LUCK2 - 80 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                            80 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                        chartLUCK.Series["Default"].Points.AddXY
                            (9, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                            90 / KernelWorker.GetSelectedCharactersData.LUCK2 - 90 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                            90 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                        chartLUCK.Series["Default"].Points.AddXY
                            (10, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                            100 / KernelWorker.GetSelectedCharactersData.LUCK2 - 100 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                            100 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * numericUpDownPercent.Value) / 100) * 100 / 100);
                    }
                    else if (checkBoxDefault.Checked == false)
                    {
                        chartLUCK.Series["Default"].Points.Clear();

                        chartLUCK.Series["Default"].Points.AddXY
                            (0, Math.Ceiling(((0.0 + (0 * 0) / 100 + 0 +
                            1 / KernelWorker.GetSelectedCharactersData.LUCK2 - 1 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                            1 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * 100) / 100) * 100 / 100);

                        chartLUCK.Series["Default"].Points.AddXY
                            (1, Math.Ceiling(((0.0 + (0 * 0) / 100 + 0 +
                            10 / KernelWorker.GetSelectedCharactersData.LUCK2 - 10 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                            10 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * 100) / 100) * 100 / 100);

                        chartLUCK.Series["Default"].Points.AddXY
                            (2, Math.Ceiling(((0.0 + (0 * 0) / 100 + 0 +
                            20 / KernelWorker.GetSelectedCharactersData.LUCK2 - 20 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                            20 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * 100) / 100) * 100 / 100);

                        chartLUCK.Series["Default"].Points.AddXY
                            (3, Math.Ceiling(((0.0 + (0 * 0) / 100 + 0 +
                            30 / KernelWorker.GetSelectedCharactersData.LUCK2 - 30 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                            30 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * 100) / 100) * 100 / 100);

                        chartLUCK.Series["Default"].Points.AddXY
                            (4, Math.Ceiling(((0.0 + (0 * 0) / 100 + 0 +
                            40 / KernelWorker.GetSelectedCharactersData.LUCK2 - 40 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                            40 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * 100) / 100) * 100 / 100);

                        chartLUCK.Series["Default"].Points.AddXY
                            (5, Math.Ceiling(((0.0 + (0 * 0) / 100 + 0 +
                            50 / KernelWorker.GetSelectedCharactersData.LUCK2 - 50 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                            50 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * 100) / 100) * 100 / 100);

                        chartLUCK.Series["Default"].Points.AddXY
                            (6, Math.Ceiling(((0.0 + (0 * 0) / 100 + 0 +
                            60 / KernelWorker.GetSelectedCharactersData.LUCK2 - 60 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                            60 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * 100) / 100) * 100 / 100);

                        chartLUCK.Series["Default"].Points.AddXY
                            (7, Math.Ceiling(((0.0 + (0 * 0) / 100 + 0 +
                            70 / KernelWorker.GetSelectedCharactersData.LUCK2 - 70 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                            70 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * 100) / 100) * 100 / 100);

                        chartLUCK.Series["Default"].Points.AddXY
                            (8, Math.Ceiling(((0.0 + (0 * 0) / 100 + 0 +
                            80 / KernelWorker.GetSelectedCharactersData.LUCK2 - 80 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                            80 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * 100) / 100) * 100 / 100);

                        chartLUCK.Series["Default"].Points.AddXY
                            (9, Math.Ceiling(((0.0 + (0 * 0) / 100 + 0 +
                            90 / KernelWorker.GetSelectedCharactersData.LUCK2 - 90 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                            90 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * 100) / 100) * 100 / 100);

                        chartLUCK.Series["Default"].Points.AddXY
                            (10, Math.Ceiling(((0.0 + (0 * 0) / 100 + 0 +
                            100 / KernelWorker.GetSelectedCharactersData.LUCK2 - 100 / KernelWorker.GetSelectedCharactersData.LUCK4 +
                            100 * KernelWorker.GetSelectedCharactersData.LUCK1 + KernelWorker.GetSelectedCharactersData.LUCK3) * 100) / 100) * 100 / 100);
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
