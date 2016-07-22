using System;
using System.Drawing;
using System.Windows.Forms;

namespace Doomtrain.Characters_Stats_Charts
{
    public partial class CharSPR : Form
    {
        public CharSPR(mainForm mainForm)
        {
            _mainForm = mainForm;
            InitializeComponent();
            SprChartWorker();

            _mainForm.numericUpDownCharSPR1.ValueChanged += new EventHandler(this.numericUpDownCharSPR_ValueChanged);
            _mainForm.numericUpDownCharSPR2.ValueChanged += new EventHandler(this.numericUpDownCharSPR_ValueChanged);
            _mainForm.numericUpDownCharSPR3.ValueChanged += new EventHandler(this.numericUpDownCharSPR_ValueChanged);
            _mainForm.numericUpDownCharSPR4.ValueChanged += new EventHandler(this.numericUpDownCharSPR_ValueChanged);

            numericUpDownMagicValue.ValueChanged += new EventHandler(numericUpDownCharSPR_ValueChanged);
            numericUpDownMagicCount.ValueChanged += new EventHandler(numericUpDownCharSPR_ValueChanged);
            numericUpDownStatBonus.ValueChanged += new EventHandler(numericUpDownCharSPR_ValueChanged);
            numericUpDownPercent.ValueChanged += new EventHandler(numericUpDownCharSPR_ValueChanged);
            numericUpDownX.ValueChanged += new EventHandler(numericUpDownCharSPR_ValueChanged);

            checkBoxDefault.CheckedChanged += new EventHandler(Default_CheckedChanged);
        }

        private readonly mainForm _mainForm;


        #region Moving chart

        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        private void chartSPR_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void chartSPR_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void chartSPR_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        #endregion

        private void buttonInfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The values here are only used for the chart and they have no connection to the kernel file.\n" +
                "You need to change the values manually.\n\n" +
                "X = Currently unknown, probably weapon modifier.\n" +
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

        private void SprChartWorker()
        {
            KernelWorker.ReadCharacters(_mainForm.listBoxCharacters.SelectedIndex, KernelWorker.BackupKernel);
            try
            {
                chartSPR.Series["Default"].Points.AddXY
                    (0, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 +
                    numericUpDownStatBonus.Value + ((1 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 1 / KernelWorker.GetSelectedCharactersData.SPR2 - (1 * 1) /
                    KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * numericUpDownPercent.Value) / 100) * 100 / 100);

                chartSPR.Series["Default"].Points.AddXY
                    (1, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 +
                    numericUpDownStatBonus.Value + ((10 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 10 / KernelWorker.GetSelectedCharactersData.SPR2 - (10 * 10) /
                    KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * numericUpDownPercent.Value) / 100) * 100 / 100);

                chartSPR.Series["Default"].Points.AddXY
                    (2, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 +
                    numericUpDownStatBonus.Value + ((20 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 20 / KernelWorker.GetSelectedCharactersData.SPR2 - (20 * 20) /
                    KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * numericUpDownPercent.Value) / 100) * 100 / 100);

                chartSPR.Series["Default"].Points.AddXY
                    (3, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 +
                    numericUpDownStatBonus.Value + ((30 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 30 / KernelWorker.GetSelectedCharactersData.SPR2 - (30 * 30) /
                    KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * numericUpDownPercent.Value) / 100) * 100 / 100);

                chartSPR.Series["Default"].Points.AddXY
                    (4, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 +
                    numericUpDownStatBonus.Value + ((40 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 40 / KernelWorker.GetSelectedCharactersData.SPR2 - (40 * 40) /
                    KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * numericUpDownPercent.Value) / 100) * 100 / 100);

                chartSPR.Series["Default"].Points.AddXY
                    (5, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 +
                    numericUpDownStatBonus.Value + ((50 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 50 / KernelWorker.GetSelectedCharactersData.SPR2 - (50 * 50) /
                    KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * numericUpDownPercent.Value) / 100) * 100 / 100);

                chartSPR.Series["Default"].Points.AddXY
                    (6, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 +
                    numericUpDownStatBonus.Value + ((60 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 60 / KernelWorker.GetSelectedCharactersData.SPR2 - (60 * 60) /
                    KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * numericUpDownPercent.Value) / 100) * 100 / 100);

                chartSPR.Series["Default"].Points.AddXY
                    (7, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 +
                    numericUpDownStatBonus.Value + ((70 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 70 / KernelWorker.GetSelectedCharactersData.SPR2 - (70 * 70) /
                    KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * numericUpDownPercent.Value) / 100) * 100 / 100);

                chartSPR.Series["Default"].Points.AddXY
                    (8, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 +
                    numericUpDownStatBonus.Value + ((80 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 80 / KernelWorker.GetSelectedCharactersData.SPR2 - (80 * 80) /
                    KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * numericUpDownPercent.Value) / 100) * 100 / 100);

                chartSPR.Series["Default"].Points.AddXY
                    (9, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 +
                    numericUpDownStatBonus.Value + ((90 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 90 / KernelWorker.GetSelectedCharactersData.SPR2 - (90 * 90) /
                    KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * numericUpDownPercent.Value) / 100) * 100 / 100);

                chartSPR.Series["Default"].Points.AddXY
                    (10, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 +
                    numericUpDownStatBonus.Value + ((100 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 100 / KernelWorker.GetSelectedCharactersData.SPR2 - (100 * 100) /
                    KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * numericUpDownPercent.Value) / 100) * 100 / 100);
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }


            KernelWorker.ReadCharacters(_mainForm.listBoxCharacters.SelectedIndex, KernelWorker.Kernel);
            try
            {
                chartSPR.Series["Current"].Points.AddXY
                    (0, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 +
                    numericUpDownStatBonus.Value + ((1 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 1 / KernelWorker.GetSelectedCharactersData.SPR2 - (1 * 1) /
                    KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * numericUpDownPercent.Value) / 100) * 100 / 100);

                chartSPR.Series["Current"].Points.AddXY
                    (1, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 +
                    numericUpDownStatBonus.Value + ((10 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 10 / KernelWorker.GetSelectedCharactersData.SPR2 - (10 * 10) /
                    KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * numericUpDownPercent.Value) / 100) * 100 / 100);

                chartSPR.Series["Current"].Points.AddXY
                    (2, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 +
                    numericUpDownStatBonus.Value + ((20 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 20 / KernelWorker.GetSelectedCharactersData.SPR2 - (20 * 20) /
                    KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * numericUpDownPercent.Value) / 100) * 100 / 100);

                chartSPR.Series["Current"].Points.AddXY
                    (3, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 +
                    numericUpDownStatBonus.Value + ((30 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 30 / KernelWorker.GetSelectedCharactersData.SPR2 - (30 * 30) /
                    KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * numericUpDownPercent.Value) / 100) * 100 / 100);

                chartSPR.Series["Current"].Points.AddXY
                    (4, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 +
                    numericUpDownStatBonus.Value + ((40 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 40 / KernelWorker.GetSelectedCharactersData.SPR2 - (40 * 40) /
                    KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * numericUpDownPercent.Value) / 100) * 100 / 100);

                chartSPR.Series["Current"].Points.AddXY
                    (5, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 +
                    numericUpDownStatBonus.Value + ((50 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 50 / KernelWorker.GetSelectedCharactersData.SPR2 - (50 * 50) /
                    KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * numericUpDownPercent.Value) / 100) * 100 / 100);

                chartSPR.Series["Current"].Points.AddXY
                    (6, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 +
                    numericUpDownStatBonus.Value + ((60 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 60 / KernelWorker.GetSelectedCharactersData.SPR2 - (60 * 60) /
                    KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * numericUpDownPercent.Value) / 100) * 100 / 100);

                chartSPR.Series["Current"].Points.AddXY
                    (7, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 +
                    numericUpDownStatBonus.Value + ((70 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 70 / KernelWorker.GetSelectedCharactersData.SPR2 - (70 * 70) /
                    KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * numericUpDownPercent.Value) / 100) * 100 / 100);

                chartSPR.Series["Current"].Points.AddXY
                    (8, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 +
                    numericUpDownStatBonus.Value + ((80 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 80 / KernelWorker.GetSelectedCharactersData.SPR2 - (80 * 80) /
                    KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * numericUpDownPercent.Value) / 100) * 100 / 100);

                chartSPR.Series["Current"].Points.AddXY
                    (9, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 +
                    numericUpDownStatBonus.Value + ((90 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 90 / KernelWorker.GetSelectedCharactersData.SPR2 - (90 * 90) /
                    KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * numericUpDownPercent.Value) / 100) * 100 / 100);

                chartSPR.Series["Current"].Points.AddXY
                    (10, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 +
                    numericUpDownStatBonus.Value + ((100 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 100 / KernelWorker.GetSelectedCharactersData.SPR2 - (100 * 100) /
                    KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * numericUpDownPercent.Value) / 100) * 100 / 100);
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }
        }

        #endregion

        #region Change chart values

        private void numericUpDownCharSPR_ValueChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                KernelWorker.ReadCharacters(_mainForm.listBoxCharacters.SelectedIndex, KernelWorker.BackupKernel);
                try
                {
                    if (checkBoxDefault.Checked == true)
                    {
                        chartSPR.Series["Default"].Points.Clear();

                        chartSPR.Series["Default"].Points.AddXY
                            (0, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 +
                            numericUpDownStatBonus.Value + ((1 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 1 / KernelWorker.GetSelectedCharactersData.SPR2 - (1 * 1) /
                            KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * numericUpDownPercent.Value) / 100) * 100 / 100);

                        chartSPR.Series["Default"].Points.AddXY
                            (1, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 +
                            numericUpDownStatBonus.Value + ((10 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 10 / KernelWorker.GetSelectedCharactersData.SPR2 - (10 * 10) /
                            KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * numericUpDownPercent.Value) / 100) * 100 / 100);

                        chartSPR.Series["Default"].Points.AddXY
                            (2, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 +
                            numericUpDownStatBonus.Value + ((20 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 20 / KernelWorker.GetSelectedCharactersData.SPR2 - (20 * 20) /
                            KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * numericUpDownPercent.Value) / 100) * 100 / 100);

                        chartSPR.Series["Default"].Points.AddXY
                            (3, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 +
                            numericUpDownStatBonus.Value + ((30 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 30 / KernelWorker.GetSelectedCharactersData.SPR2 - (30 * 30) /
                            KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * numericUpDownPercent.Value) / 100) * 100 / 100);

                        chartSPR.Series["Default"].Points.AddXY
                            (4, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 +
                            numericUpDownStatBonus.Value + ((40 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 40 / KernelWorker.GetSelectedCharactersData.SPR2 - (40 * 40) /
                            KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * numericUpDownPercent.Value) / 100) * 100 / 100);

                        chartSPR.Series["Default"].Points.AddXY
                            (5, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 +
                            numericUpDownStatBonus.Value + ((50 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 50 / KernelWorker.GetSelectedCharactersData.SPR2 - (50 * 50) /
                            KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * numericUpDownPercent.Value) / 100) * 100 / 100);

                        chartSPR.Series["Default"].Points.AddXY
                            (6, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 +
                            numericUpDownStatBonus.Value + ((60 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 60 / KernelWorker.GetSelectedCharactersData.SPR2 - (60 * 60) /
                            KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * numericUpDownPercent.Value) / 100) * 100 / 100);

                        chartSPR.Series["Default"].Points.AddXY
                            (7, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 +
                            numericUpDownStatBonus.Value + ((70 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 70 / KernelWorker.GetSelectedCharactersData.SPR2 - (70 * 70) /
                            KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * numericUpDownPercent.Value) / 100) * 100 / 100);

                        chartSPR.Series["Default"].Points.AddXY
                            (8, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 +
                            numericUpDownStatBonus.Value + ((80 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 80 / KernelWorker.GetSelectedCharactersData.SPR2 - (80 * 80) /
                            KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * numericUpDownPercent.Value) / 100) * 100 / 100);

                        chartSPR.Series["Default"].Points.AddXY
                            (9, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 +
                            numericUpDownStatBonus.Value + ((90 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 90 / KernelWorker.GetSelectedCharactersData.SPR2 - (90 * 90) /
                            KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * numericUpDownPercent.Value) / 100) * 100 / 100);

                        chartSPR.Series["Default"].Points.AddXY
                            (10, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 +
                            numericUpDownStatBonus.Value + ((100 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 100 / KernelWorker.GetSelectedCharactersData.SPR2 - (100 * 100) /
                            KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * numericUpDownPercent.Value) / 100) * 100 / 100);
                    }
                    else if (checkBoxDefault.Checked == false)
                    {
                        chartSPR.Series["Default"].Points.Clear();

                        chartSPR.Series["Default"].Points.AddXY
                            (0, Math.Ceiling(((0.0 + (0 * 0) / 100 +
                            0 + ((1 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 1 / KernelWorker.GetSelectedCharactersData.SPR2 - (1 * 1) /
                            KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * 100) / 100) * 100 / 100);

                        chartSPR.Series["Default"].Points.AddXY
                            (1, Math.Ceiling(((0.0 + (0 * 0) / 100 +
                            0 + ((10 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 10 / KernelWorker.GetSelectedCharactersData.SPR2 - (10 * 10) /
                            KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * 100) / 100) * 100 / 100);

                        chartSPR.Series["Default"].Points.AddXY
                            (2, Math.Ceiling(((0.0 + (0 * 0) / 100 +
                            0 + ((20 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 20 / KernelWorker.GetSelectedCharactersData.SPR2 - (20 * 20) /
                            KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * 100) / 100) * 100 / 100);

                        chartSPR.Series["Default"].Points.AddXY
                            (3, Math.Ceiling(((0.0 + (0 * 0) / 100 +
                            0 + ((30 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 30 / KernelWorker.GetSelectedCharactersData.SPR2 - (30 * 30) /
                            KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * 100) / 100) * 100 / 100);

                        chartSPR.Series["Default"].Points.AddXY
                            (4, Math.Ceiling(((0.0 + (0 * 0) / 100 +
                            0 + ((40 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 40 / KernelWorker.GetSelectedCharactersData.SPR2 - (40 * 40) /
                            KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * 100) / 100) * 100 / 100);

                        chartSPR.Series["Default"].Points.AddXY
                            (5, Math.Ceiling(((0.0 + (0 * 0) / 100 +
                            0 + ((50 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 50 / KernelWorker.GetSelectedCharactersData.SPR2 - (50 * 50) /
                            KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * 100) / 100) * 100 / 100);

                        chartSPR.Series["Default"].Points.AddXY
                            (6, Math.Ceiling(((0.0 + (0 * 0) / 100 +
                            0 + ((60 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 60 / KernelWorker.GetSelectedCharactersData.SPR2 - (60 * 60) /
                            KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * 100) / 100) * 100 / 100);

                        chartSPR.Series["Default"].Points.AddXY
                            (7, Math.Ceiling(((0.0 + (0 * 0) / 100 +
                            0 + ((70 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 70 / KernelWorker.GetSelectedCharactersData.SPR2 - (70 * 70) /
                            KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * 100) / 100) * 100 / 100);

                        chartSPR.Series["Default"].Points.AddXY
                            (8, Math.Ceiling(((0.0 + (0 * 0) / 100 +
                            0 + ((80 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 80 / KernelWorker.GetSelectedCharactersData.SPR2 - (80 * 80) /
                            KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * 100) / 100) * 100 / 100);

                        chartSPR.Series["Default"].Points.AddXY
                            (9, Math.Ceiling(((0.0 + (0 * 0) / 100 +
                            0 + ((90 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 90 / KernelWorker.GetSelectedCharactersData.SPR2 - (90 * 90) /
                            KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * 100) / 100) * 100 / 100);

                        chartSPR.Series["Default"].Points.AddXY
                            (10, Math.Ceiling(((0.0 + (0 * 0) / 100 +
                            0 + ((100 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 100 / KernelWorker.GetSelectedCharactersData.SPR2 - (100 * 100) /
                            KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * 100) / 100) * 100 / 100);
                    }

                }
                catch (Exception Exception)
                {
                    MessageBox.Show(Exception.ToString());
                }


                KernelWorker.ReadCharacters(_mainForm.listBoxCharacters.SelectedIndex, KernelWorker.Kernel);
                try
                {
                    chartSPR.Series["Current"].Points.Clear();

                    chartSPR.Series["Current"].Points.AddXY
                        (0, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 +
                        numericUpDownStatBonus.Value + ((1 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 1 / KernelWorker.GetSelectedCharactersData.SPR2 - (1 * 1) /
                        KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * numericUpDownPercent.Value) / 100) * 100 / 100);

                    chartSPR.Series["Current"].Points.AddXY
                        (1, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 +
                        numericUpDownStatBonus.Value + ((10 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 10 / KernelWorker.GetSelectedCharactersData.SPR2 - (10 * 10) /
                        KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * numericUpDownPercent.Value) / 100) * 100 / 100);

                    chartSPR.Series["Current"].Points.AddXY
                        (2, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 +
                        numericUpDownStatBonus.Value + ((20 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 20 / KernelWorker.GetSelectedCharactersData.SPR2 - (20 * 20) /
                        KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * numericUpDownPercent.Value) / 100) * 100 / 100);

                    chartSPR.Series["Current"].Points.AddXY
                        (3, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 +
                        numericUpDownStatBonus.Value + ((30 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 30 / KernelWorker.GetSelectedCharactersData.SPR2 - (30 * 30) /
                        KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * numericUpDownPercent.Value) / 100) * 100 / 100);

                    chartSPR.Series["Current"].Points.AddXY
                        (4, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 +
                        numericUpDownStatBonus.Value + ((40 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 40 / KernelWorker.GetSelectedCharactersData.SPR2 - (40 * 40) /
                        KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * numericUpDownPercent.Value) / 100) * 100 / 100);

                    chartSPR.Series["Current"].Points.AddXY
                        (5, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 +
                        numericUpDownStatBonus.Value + ((50 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 50 / KernelWorker.GetSelectedCharactersData.SPR2 - (50 * 50) /
                        KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * numericUpDownPercent.Value) / 100) * 100 / 100);

                    chartSPR.Series["Current"].Points.AddXY
                        (6, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 +
                        numericUpDownStatBonus.Value + ((60 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 60 / KernelWorker.GetSelectedCharactersData.SPR2 - (60 * 60) /
                        KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * numericUpDownPercent.Value) / 100) * 100 / 100);

                    chartSPR.Series["Current"].Points.AddXY
                        (7, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 +
                        numericUpDownStatBonus.Value + ((70 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 70 / KernelWorker.GetSelectedCharactersData.SPR2 - (70 * 70) /
                        KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * numericUpDownPercent.Value) / 100) * 100 / 100);

                    chartSPR.Series["Current"].Points.AddXY
                        (8, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 +
                        numericUpDownStatBonus.Value + ((80 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 80 / KernelWorker.GetSelectedCharactersData.SPR2 - (80 * 80) /
                        KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * numericUpDownPercent.Value) / 100) * 100 / 100);

                    chartSPR.Series["Current"].Points.AddXY
                        (9, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 +
                        numericUpDownStatBonus.Value + ((90 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 90 / KernelWorker.GetSelectedCharactersData.SPR2 - (90 * 90) /
                        KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * numericUpDownPercent.Value) / 100) * 100 / 100);

                    chartSPR.Series["Current"].Points.AddXY
                        (10, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 +
                        numericUpDownStatBonus.Value + ((100 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 100 / KernelWorker.GetSelectedCharactersData.SPR2 - (100 * 100) /
                        KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * numericUpDownPercent.Value) / 100) * 100 / 100);
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
                        chartSPR.Series["Default"].Points.Clear();

                        chartSPR.Series["Default"].Points.AddXY
                            (0, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 +
                            numericUpDownStatBonus.Value + ((1 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 1 / KernelWorker.GetSelectedCharactersData.SPR2 - (1 * 1) /
                            KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * numericUpDownPercent.Value) / 100) * 100 / 100);

                        chartSPR.Series["Default"].Points.AddXY
                            (1, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 +
                            numericUpDownStatBonus.Value + ((10 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 10 / KernelWorker.GetSelectedCharactersData.SPR2 - (10 * 10) /
                            KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * numericUpDownPercent.Value) / 100) * 100 / 100);

                        chartSPR.Series["Default"].Points.AddXY
                            (2, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 +
                            numericUpDownStatBonus.Value + ((20 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 20 / KernelWorker.GetSelectedCharactersData.SPR2 - (20 * 20) /
                            KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * numericUpDownPercent.Value) / 100) * 100 / 100);

                        chartSPR.Series["Default"].Points.AddXY
                            (3, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 +
                            numericUpDownStatBonus.Value + ((30 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 30 / KernelWorker.GetSelectedCharactersData.SPR2 - (30 * 30) /
                            KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * numericUpDownPercent.Value) / 100) * 100 / 100);

                        chartSPR.Series["Default"].Points.AddXY
                            (4, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 +
                            numericUpDownStatBonus.Value + ((40 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 40 / KernelWorker.GetSelectedCharactersData.SPR2 - (40 * 40) /
                            KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * numericUpDownPercent.Value) / 100) * 100 / 100);

                        chartSPR.Series["Default"].Points.AddXY
                            (5, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 +
                            numericUpDownStatBonus.Value + ((50 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 50 / KernelWorker.GetSelectedCharactersData.SPR2 - (50 * 50) /
                            KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * numericUpDownPercent.Value) / 100) * 100 / 100);

                        chartSPR.Series["Default"].Points.AddXY
                            (6, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 +
                            numericUpDownStatBonus.Value + ((60 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 60 / KernelWorker.GetSelectedCharactersData.SPR2 - (60 * 60) /
                            KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * numericUpDownPercent.Value) / 100) * 100 / 100);

                        chartSPR.Series["Default"].Points.AddXY
                            (7, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 +
                            numericUpDownStatBonus.Value + ((70 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 70 / KernelWorker.GetSelectedCharactersData.SPR2 - (70 * 70) /
                            KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * numericUpDownPercent.Value) / 100) * 100 / 100);

                        chartSPR.Series["Default"].Points.AddXY
                            (8, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 +
                            numericUpDownStatBonus.Value + ((80 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 80 / KernelWorker.GetSelectedCharactersData.SPR2 - (80 * 80) /
                            KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * numericUpDownPercent.Value) / 100) * 100 / 100);

                        chartSPR.Series["Default"].Points.AddXY
                            (9, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 +
                            numericUpDownStatBonus.Value + ((90 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 90 / KernelWorker.GetSelectedCharactersData.SPR2 - (90 * 90) /
                            KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * numericUpDownPercent.Value) / 100) * 100 / 100);

                        chartSPR.Series["Default"].Points.AddXY
                            (10, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 +
                            numericUpDownStatBonus.Value + ((100 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 100 / KernelWorker.GetSelectedCharactersData.SPR2 - (100 * 100) /
                            KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * numericUpDownPercent.Value) / 100) * 100 / 100);
                    }
                    else if (checkBoxDefault.Checked == false)
                    {
                        chartSPR.Series["Default"].Points.Clear();

                        chartSPR.Series["Default"].Points.AddXY
                            (0, Math.Ceiling(((0.0 + (0 * 0) / 100 +
                            0 + ((1 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 1 / KernelWorker.GetSelectedCharactersData.SPR2 - (1 * 1) /
                            KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * 100) / 100) * 100 / 100);

                        chartSPR.Series["Default"].Points.AddXY
                            (1, Math.Ceiling(((0.0 + (0 * 0) / 100 +
                            0 + ((10 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 10 / KernelWorker.GetSelectedCharactersData.SPR2 - (10 * 10) /
                            KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * 100) / 100) * 100 / 100);

                        chartSPR.Series["Default"].Points.AddXY
                            (2, Math.Ceiling(((0.0 + (0 * 0) / 100 +
                            0 + ((20 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 20 / KernelWorker.GetSelectedCharactersData.SPR2 - (20 * 20) /
                            KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * 100) / 100) * 100 / 100);

                        chartSPR.Series["Default"].Points.AddXY
                            (3, Math.Ceiling(((0.0 + (0 * 0) / 100 +
                            0 + ((30 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 30 / KernelWorker.GetSelectedCharactersData.SPR2 - (30 * 30) /
                            KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * 100) / 100) * 100 / 100);

                        chartSPR.Series["Default"].Points.AddXY
                            (4, Math.Ceiling(((0.0 + (0 * 0) / 100 +
                            0 + ((40 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 40 / KernelWorker.GetSelectedCharactersData.SPR2 - (40 * 40) /
                            KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * 100) / 100) * 100 / 100);

                        chartSPR.Series["Default"].Points.AddXY
                            (5, Math.Ceiling(((0.0 + (0 * 0) / 100 +
                            0 + ((50 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 50 / KernelWorker.GetSelectedCharactersData.SPR2 - (50 * 50) /
                            KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * 100) / 100) * 100 / 100);

                        chartSPR.Series["Default"].Points.AddXY
                            (6, Math.Ceiling(((0.0 + (0 * 0) / 100 +
                            0 + ((60 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 60 / KernelWorker.GetSelectedCharactersData.SPR2 - (60 * 60) /
                            KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * 100) / 100) * 100 / 100);

                        chartSPR.Series["Default"].Points.AddXY
                            (7, Math.Ceiling(((0.0 + (0 * 0) / 100 +
                            0 + ((70 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 70 / KernelWorker.GetSelectedCharactersData.SPR2 - (70 * 70) /
                            KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * 100) / 100) * 100 / 100);

                        chartSPR.Series["Default"].Points.AddXY
                            (8, Math.Ceiling(((0.0 + (0 * 0) / 100 +
                            0 + ((80 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 80 / KernelWorker.GetSelectedCharactersData.SPR2 - (80 * 80) /
                            KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * 100) / 100) * 100 / 100);

                        chartSPR.Series["Default"].Points.AddXY
                            (9, Math.Ceiling(((0.0 + (0 * 0) / 100 +
                            0 + ((90 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 90 / KernelWorker.GetSelectedCharactersData.SPR2 - (90 * 90) /
                            KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * 100) / 100) * 100 / 100);

                        chartSPR.Series["Default"].Points.AddXY
                            (10, Math.Ceiling(((0.0 + (0 * 0) / 100 +
                            0 + ((100 * KernelWorker.GetSelectedCharactersData.SPR1) / 10 + 100 / KernelWorker.GetSelectedCharactersData.SPR2 - (100 * 100) /
                            KernelWorker.GetSelectedCharactersData.SPR4 / 2 + KernelWorker.GetSelectedCharactersData.SPR3) / 4) * 100) / 100) * 100 / 100);
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
