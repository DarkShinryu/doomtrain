using System;
using System.Drawing;
using System.Windows.Forms;

namespace Doomtrain.Characters_Stats_Charts
{
    public partial class CharSPD : Form
    {
        public CharSPD(mainForm mainForm)
        {
            _mainForm = mainForm;
            InitializeComponent();
            SpdChartWorker();

            _mainForm.numericUpDownCharSPD1.ValueChanged += new EventHandler(this.numericUpDownCharSPD_ValueChanged);
            _mainForm.numericUpDownCharSPD2.ValueChanged += new EventHandler(this.numericUpDownCharSPD_ValueChanged);
            _mainForm.numericUpDownCharSPD3.ValueChanged += new EventHandler(this.numericUpDownCharSPD_ValueChanged);
            _mainForm.numericUpDownCharSPD4.ValueChanged += new EventHandler(this.numericUpDownCharSPD_ValueChanged);

            numericUpDownMagicValue.ValueChanged += new EventHandler(numericUpDownCharSPD_ValueChanged);
            numericUpDownMagicCount.ValueChanged += new EventHandler(numericUpDownCharSPD_ValueChanged);
            numericUpDownStatBonus.ValueChanged += new EventHandler(numericUpDownCharSPD_ValueChanged);
            numericUpDownPercent.ValueChanged += new EventHandler(numericUpDownCharSPD_ValueChanged);
            numericUpDownX.ValueChanged += new EventHandler(numericUpDownCharSPD_ValueChanged);

            checkBoxDefault.CheckedChanged += new EventHandler(Default_CheckedChanged);
        }

        private readonly mainForm _mainForm;


        #region Moving chart

        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        private void chartSPD_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void chartSPD_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void chartSPD_MouseUp(object sender, MouseEventArgs e)
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

        private void SpdChartWorker()
        {
            KernelWorker.ReadCharacters(_mainForm.listBoxCharacters.SelectedIndex, KernelWorker.BackupKernel);
            try
            {
                chartSPD.Series["Default"].Points.AddXY
                    (0, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value + 
                    1 / KernelWorker.GetSelectedCharactersData.SPD2 - 1 / KernelWorker.GetSelectedCharactersData.SPD4 + 
                    1 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * numericUpDownPercent.Value) / 100 ) * 100 / 100);

                chartSPD.Series["Default"].Points.AddXY
                    (1, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                    10 / KernelWorker.GetSelectedCharactersData.SPD2 - 10 / KernelWorker.GetSelectedCharactersData.SPD4 + 
                    10 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                chartSPD.Series["Default"].Points.AddXY
                    (2, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                    20 / KernelWorker.GetSelectedCharactersData.SPD2 - 20 / KernelWorker.GetSelectedCharactersData.SPD4 + 
                    20 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                chartSPD.Series["Default"].Points.AddXY
                    (3, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                    30 / KernelWorker.GetSelectedCharactersData.SPD2 - 30 / KernelWorker.GetSelectedCharactersData.SPD4 + 
                    30 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                chartSPD.Series["Default"].Points.AddXY
                    (4, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                    40 / KernelWorker.GetSelectedCharactersData.SPD2 - 40 / KernelWorker.GetSelectedCharactersData.SPD4 + 
                    40 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                chartSPD.Series["Default"].Points.AddXY
                    (5, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                    50 / KernelWorker.GetSelectedCharactersData.SPD2 - 50 / KernelWorker.GetSelectedCharactersData.SPD4 + 
                    50 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                chartSPD.Series["Default"].Points.AddXY
                    (6, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                    60 / KernelWorker.GetSelectedCharactersData.SPD2 - 60 / KernelWorker.GetSelectedCharactersData.SPD4 + 
                    60 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                chartSPD.Series["Default"].Points.AddXY
                    (7, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                    70 / KernelWorker.GetSelectedCharactersData.SPD2 - 70 / KernelWorker.GetSelectedCharactersData.SPD4 + 
                    70 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                chartSPD.Series["Default"].Points.AddXY
                    (8, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                    80 / KernelWorker.GetSelectedCharactersData.SPD2 - 80 / KernelWorker.GetSelectedCharactersData.SPD4 + 
                    80 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                chartSPD.Series["Default"].Points.AddXY
                    (9, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                    90 / KernelWorker.GetSelectedCharactersData.SPD2 - 90 / KernelWorker.GetSelectedCharactersData.SPD4 + 
                    90 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                chartSPD.Series["Default"].Points.AddXY
                    (10, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                    100 / KernelWorker.GetSelectedCharactersData.SPD2 - 100 / KernelWorker.GetSelectedCharactersData.SPD4 + 
                    100 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * numericUpDownPercent.Value) / 100) * 100 / 100);
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }


            KernelWorker.ReadCharacters(_mainForm.listBoxCharacters.SelectedIndex, KernelWorker.Kernel);
            try
            {
                chartSPD.Series["Current"].Points.AddXY
                    (0, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                    1 / KernelWorker.GetSelectedCharactersData.SPD2 - 1 / KernelWorker.GetSelectedCharactersData.SPD4 +
                    1 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                chartSPD.Series["Current"].Points.AddXY
                    (1, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                    10 / KernelWorker.GetSelectedCharactersData.SPD2 - 10 / KernelWorker.GetSelectedCharactersData.SPD4 +
                    10 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                chartSPD.Series["Current"].Points.AddXY
                    (2, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                    20 / KernelWorker.GetSelectedCharactersData.SPD2 - 20 / KernelWorker.GetSelectedCharactersData.SPD4 +
                    20 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                chartSPD.Series["Current"].Points.AddXY
                    (3, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                    30 / KernelWorker.GetSelectedCharactersData.SPD2 - 30 / KernelWorker.GetSelectedCharactersData.SPD4 +
                    30 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                chartSPD.Series["Current"].Points.AddXY
                    (4, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                    40 / KernelWorker.GetSelectedCharactersData.SPD2 - 40 / KernelWorker.GetSelectedCharactersData.SPD4 +
                    40 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                chartSPD.Series["Current"].Points.AddXY
                    (5, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                    50 / KernelWorker.GetSelectedCharactersData.SPD2 - 50 / KernelWorker.GetSelectedCharactersData.SPD4 +
                    50 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                chartSPD.Series["Current"].Points.AddXY
                    (6, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                    60 / KernelWorker.GetSelectedCharactersData.SPD2 - 60 / KernelWorker.GetSelectedCharactersData.SPD4 +
                    60 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                chartSPD.Series["Current"].Points.AddXY
                    (7, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                    70 / KernelWorker.GetSelectedCharactersData.SPD2 - 70 / KernelWorker.GetSelectedCharactersData.SPD4 +
                    70 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                chartSPD.Series["Current"].Points.AddXY
                    (8, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                    80 / KernelWorker.GetSelectedCharactersData.SPD2 - 80 / KernelWorker.GetSelectedCharactersData.SPD4 +
                    80 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                chartSPD.Series["Current"].Points.AddXY
                    (9, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                    90 / KernelWorker.GetSelectedCharactersData.SPD2 - 90 / KernelWorker.GetSelectedCharactersData.SPD4 +
                    90 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                chartSPD.Series["Current"].Points.AddXY
                    (10, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                    100 / KernelWorker.GetSelectedCharactersData.SPD2 - 100 / KernelWorker.GetSelectedCharactersData.SPD4 +
                    100 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * numericUpDownPercent.Value) / 100) * 100 / 100);
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }
        }

        #endregion

        #region Change chart values

        private void numericUpDownCharSPD_ValueChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                KernelWorker.ReadCharacters(_mainForm.listBoxCharacters.SelectedIndex, KernelWorker.BackupKernel);
                try
                {
                    if (checkBoxDefault.Checked == true)
                    {
                        chartSPD.Series["Default"].Points.Clear();

                        chartSPD.Series["Default"].Points.AddXY
                            (0, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                            1 / KernelWorker.GetSelectedCharactersData.SPD2 - 1 / KernelWorker.GetSelectedCharactersData.SPD4 +
                            1 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                        chartSPD.Series["Default"].Points.AddXY
                            (1, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                            10 / KernelWorker.GetSelectedCharactersData.SPD2 - 10 / KernelWorker.GetSelectedCharactersData.SPD4 +
                            10 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                        chartSPD.Series["Default"].Points.AddXY
                            (2, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                            20 / KernelWorker.GetSelectedCharactersData.SPD2 - 20 / KernelWorker.GetSelectedCharactersData.SPD4 +
                            20 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                        chartSPD.Series["Default"].Points.AddXY
                            (3, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                            30 / KernelWorker.GetSelectedCharactersData.SPD2 - 30 / KernelWorker.GetSelectedCharactersData.SPD4 +
                            30 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                        chartSPD.Series["Default"].Points.AddXY
                            (4, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                            40 / KernelWorker.GetSelectedCharactersData.SPD2 - 40 / KernelWorker.GetSelectedCharactersData.SPD4 +
                            40 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                        chartSPD.Series["Default"].Points.AddXY
                            (5, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                            50 / KernelWorker.GetSelectedCharactersData.SPD2 - 50 / KernelWorker.GetSelectedCharactersData.SPD4 +
                            50 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                        chartSPD.Series["Default"].Points.AddXY
                            (6, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                            60 / KernelWorker.GetSelectedCharactersData.SPD2 - 60 / KernelWorker.GetSelectedCharactersData.SPD4 +
                            60 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                        chartSPD.Series["Default"].Points.AddXY
                            (7, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                            70 / KernelWorker.GetSelectedCharactersData.SPD2 - 70 / KernelWorker.GetSelectedCharactersData.SPD4 +
                            70 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                        chartSPD.Series["Default"].Points.AddXY
                            (8, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                            80 / KernelWorker.GetSelectedCharactersData.SPD2 - 80 / KernelWorker.GetSelectedCharactersData.SPD4 +
                            80 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                        chartSPD.Series["Default"].Points.AddXY
                            (9, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                            90 / KernelWorker.GetSelectedCharactersData.SPD2 - 90 / KernelWorker.GetSelectedCharactersData.SPD4 +
                            90 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                        chartSPD.Series["Default"].Points.AddXY
                            (10, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                            100 / KernelWorker.GetSelectedCharactersData.SPD2 - 100 / KernelWorker.GetSelectedCharactersData.SPD4 +
                            100 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * numericUpDownPercent.Value) / 100) * 100 / 100);
                    }
                    else if (checkBoxDefault.Checked == false)
                    {
                        chartSPD.Series["Default"].Points.Clear();

                        chartSPD.Series["Default"].Points.AddXY
                            (0, Math.Ceiling(((0.0 + (0 * 0) / 100 + 0 +
                            1 / KernelWorker.GetSelectedCharactersData.SPD2 - 1 / KernelWorker.GetSelectedCharactersData.SPD4 +
                            1 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * 100) / 100) * 100 / 100);

                        chartSPD.Series["Default"].Points.AddXY
                            (1, Math.Ceiling(((0.0 + (0 * 0) / 100 + 0 +
                            10 / KernelWorker.GetSelectedCharactersData.SPD2 - 10 / KernelWorker.GetSelectedCharactersData.SPD4 +
                            10 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * 100) / 100) * 100 / 100);

                        chartSPD.Series["Default"].Points.AddXY
                            (2, Math.Ceiling(((0.0 + (0 * 0) / 100 + 0 +
                            20 / KernelWorker.GetSelectedCharactersData.SPD2 - 20 / KernelWorker.GetSelectedCharactersData.SPD4 +
                            20 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * 100) / 100) * 100 / 100);

                        chartSPD.Series["Default"].Points.AddXY
                            (3, Math.Ceiling(((0.0 + (0 * 0) / 100 + 0 +
                            30 / KernelWorker.GetSelectedCharactersData.SPD2 - 30 / KernelWorker.GetSelectedCharactersData.SPD4 +
                            30 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * 100) / 100) * 100 / 100);

                        chartSPD.Series["Default"].Points.AddXY
                            (4, Math.Ceiling(((0.0 + (0 * 0) / 100 + 0 +
                            40 / KernelWorker.GetSelectedCharactersData.SPD2 - 40 / KernelWorker.GetSelectedCharactersData.SPD4 +
                            40 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * 100) / 100) * 100 / 100);

                        chartSPD.Series["Default"].Points.AddXY
                            (5, Math.Ceiling(((0.0 + (0 * 0) / 100 + 0 +
                            50 / KernelWorker.GetSelectedCharactersData.SPD2 - 50 / KernelWorker.GetSelectedCharactersData.SPD4 +
                            50 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * 100) / 100) * 100 / 100);

                        chartSPD.Series["Default"].Points.AddXY
                            (6, Math.Ceiling(((0.0 + (0 * 0) / 100 + 0 +
                            60 / KernelWorker.GetSelectedCharactersData.SPD2 - 60 / KernelWorker.GetSelectedCharactersData.SPD4 +
                            60 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * 100) / 100) * 100 / 100);

                        chartSPD.Series["Default"].Points.AddXY
                            (7, Math.Ceiling(((0.0 + (0 * 0) / 100 + 0 +
                            70 / KernelWorker.GetSelectedCharactersData.SPD2 - 70 / KernelWorker.GetSelectedCharactersData.SPD4 +
                            70 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * 100) / 100) * 100 / 100);

                        chartSPD.Series["Default"].Points.AddXY
                            (8, Math.Ceiling(((0.0 + (0 * 0) / 100 + 0 +
                            80 / KernelWorker.GetSelectedCharactersData.SPD2 - 80 / KernelWorker.GetSelectedCharactersData.SPD4 +
                            80 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * 100) / 100) * 100 / 100);

                        chartSPD.Series["Default"].Points.AddXY
                            (9, Math.Ceiling(((0.0 + (0 * 0) / 100 + 0 +
                            90 / KernelWorker.GetSelectedCharactersData.SPD2 - 90 / KernelWorker.GetSelectedCharactersData.SPD4 +
                            90 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * 100) / 100) * 100 / 100);

                        chartSPD.Series["Default"].Points.AddXY
                            (10, Math.Ceiling(((0.0 + (0 * 0) / 100 + 0 +
                            100 / KernelWorker.GetSelectedCharactersData.SPD2 - 100 / KernelWorker.GetSelectedCharactersData.SPD4 +
                            100 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * 100) / 100) * 100 / 100);
                    }

                }
                catch (Exception Exception)
                {
                    MessageBox.Show(Exception.ToString());
                }


                KernelWorker.ReadCharacters(_mainForm.listBoxCharacters.SelectedIndex, KernelWorker.Kernel);
                try
                {
                    chartSPD.Series["Current"].Points.Clear();

                    chartSPD.Series["Current"].Points.AddXY
                        (0, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                        1 / KernelWorker.GetSelectedCharactersData.SPD2 - 1 / KernelWorker.GetSelectedCharactersData.SPD4 +
                        1 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                    chartSPD.Series["Current"].Points.AddXY
                        (1, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                        10 / KernelWorker.GetSelectedCharactersData.SPD2 - 10 / KernelWorker.GetSelectedCharactersData.SPD4 +
                        10 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                    chartSPD.Series["Current"].Points.AddXY
                        (2, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                        20 / KernelWorker.GetSelectedCharactersData.SPD2 - 20 / KernelWorker.GetSelectedCharactersData.SPD4 +
                        20 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                    chartSPD.Series["Current"].Points.AddXY
                        (3, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                        30 / KernelWorker.GetSelectedCharactersData.SPD2 - 30 / KernelWorker.GetSelectedCharactersData.SPD4 +
                        30 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                    chartSPD.Series["Current"].Points.AddXY
                        (4, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                        40 / KernelWorker.GetSelectedCharactersData.SPD2 - 40 / KernelWorker.GetSelectedCharactersData.SPD4 +
                        40 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                    chartSPD.Series["Current"].Points.AddXY
                        (5, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                        50 / KernelWorker.GetSelectedCharactersData.SPD2 - 50 / KernelWorker.GetSelectedCharactersData.SPD4 +
                        50 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                    chartSPD.Series["Current"].Points.AddXY
                        (6, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                        60 / KernelWorker.GetSelectedCharactersData.SPD2 - 60 / KernelWorker.GetSelectedCharactersData.SPD4 +
                        60 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                    chartSPD.Series["Current"].Points.AddXY
                        (7, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                        70 / KernelWorker.GetSelectedCharactersData.SPD2 - 70 / KernelWorker.GetSelectedCharactersData.SPD4 +
                        70 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                    chartSPD.Series["Current"].Points.AddXY
                        (8, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                        80 / KernelWorker.GetSelectedCharactersData.SPD2 - 80 / KernelWorker.GetSelectedCharactersData.SPD4 +
                        80 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                    chartSPD.Series["Current"].Points.AddXY
                        (9, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                        90 / KernelWorker.GetSelectedCharactersData.SPD2 - 90 / KernelWorker.GetSelectedCharactersData.SPD4 +
                        90 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                    chartSPD.Series["Current"].Points.AddXY
                        (10, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                        100 / KernelWorker.GetSelectedCharactersData.SPD2 - 100 / KernelWorker.GetSelectedCharactersData.SPD4 +
                        100 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * numericUpDownPercent.Value) / 100) * 100 / 100);
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
                        chartSPD.Series["Default"].Points.Clear();

                        chartSPD.Series["Default"].Points.AddXY
                            (0, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                            1 / KernelWorker.GetSelectedCharactersData.SPD2 - 1 / KernelWorker.GetSelectedCharactersData.SPD4 +
                            1 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                        chartSPD.Series["Default"].Points.AddXY
                            (1, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                            10 / KernelWorker.GetSelectedCharactersData.SPD2 - 10 / KernelWorker.GetSelectedCharactersData.SPD4 +
                            10 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                        chartSPD.Series["Default"].Points.AddXY
                            (2, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                            20 / KernelWorker.GetSelectedCharactersData.SPD2 - 20 / KernelWorker.GetSelectedCharactersData.SPD4 +
                            20 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                        chartSPD.Series["Default"].Points.AddXY
                            (3, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                            30 / KernelWorker.GetSelectedCharactersData.SPD2 - 30 / KernelWorker.GetSelectedCharactersData.SPD4 +
                            30 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                        chartSPD.Series["Default"].Points.AddXY
                            (4, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                            40 / KernelWorker.GetSelectedCharactersData.SPD2 - 40 / KernelWorker.GetSelectedCharactersData.SPD4 +
                            40 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                        chartSPD.Series["Default"].Points.AddXY
                            (5, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                            50 / KernelWorker.GetSelectedCharactersData.SPD2 - 50 / KernelWorker.GetSelectedCharactersData.SPD4 +
                            50 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                        chartSPD.Series["Default"].Points.AddXY
                            (6, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                            60 / KernelWorker.GetSelectedCharactersData.SPD2 - 60 / KernelWorker.GetSelectedCharactersData.SPD4 +
                            60 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                        chartSPD.Series["Default"].Points.AddXY
                            (7, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                            70 / KernelWorker.GetSelectedCharactersData.SPD2 - 70 / KernelWorker.GetSelectedCharactersData.SPD4 +
                            70 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                        chartSPD.Series["Default"].Points.AddXY
                            (8, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                            80 / KernelWorker.GetSelectedCharactersData.SPD2 - 80 / KernelWorker.GetSelectedCharactersData.SPD4 +
                            80 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                        chartSPD.Series["Default"].Points.AddXY
                            (9, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                            90 / KernelWorker.GetSelectedCharactersData.SPD2 - 90 / KernelWorker.GetSelectedCharactersData.SPD4 +
                            90 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * numericUpDownPercent.Value) / 100) * 100 / 100);

                        chartSPD.Series["Default"].Points.AddXY
                            (10, Math.Ceiling(((numericUpDownX.Value + (numericUpDownMagicValue.Value * numericUpDownMagicCount.Value) / 100 + numericUpDownStatBonus.Value +
                            100 / KernelWorker.GetSelectedCharactersData.SPD2 - 100 / KernelWorker.GetSelectedCharactersData.SPD4 +
                            100 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * numericUpDownPercent.Value) / 100) * 100 / 100);
                    }
                    else if (checkBoxDefault.Checked == false)
                    {
                        chartSPD.Series["Default"].Points.Clear();

                        chartSPD.Series["Default"].Points.AddXY
                            (0, Math.Ceiling(((0.0 + (0 * 0) / 100 + 0 +
                            1 / KernelWorker.GetSelectedCharactersData.SPD2 - 1 / KernelWorker.GetSelectedCharactersData.SPD4 +
                            1 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * 100) / 100) * 100 / 100);

                        chartSPD.Series["Default"].Points.AddXY
                            (1, Math.Ceiling(((0.0 + (0 * 0) / 100 + 0 +
                            10 / KernelWorker.GetSelectedCharactersData.SPD2 - 10 / KernelWorker.GetSelectedCharactersData.SPD4 +
                            10 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * 100) / 100) * 100 / 100);

                        chartSPD.Series["Default"].Points.AddXY
                            (2, Math.Ceiling(((0.0 + (0 * 0) / 100 + 0 +
                            20 / KernelWorker.GetSelectedCharactersData.SPD2 - 20 / KernelWorker.GetSelectedCharactersData.SPD4 +
                            20 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * 100) / 100) * 100 / 100);

                        chartSPD.Series["Default"].Points.AddXY
                            (3, Math.Ceiling(((0.0 + (0 * 0) / 100 + 0 +
                            30 / KernelWorker.GetSelectedCharactersData.SPD2 - 30 / KernelWorker.GetSelectedCharactersData.SPD4 +
                            30 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * 100) / 100) * 100 / 100);

                        chartSPD.Series["Default"].Points.AddXY
                            (4, Math.Ceiling(((0.0 + (0 * 0) / 100 + 0 +
                            40 / KernelWorker.GetSelectedCharactersData.SPD2 - 40 / KernelWorker.GetSelectedCharactersData.SPD4 +
                            40 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * 100) / 100) * 100 / 100);

                        chartSPD.Series["Default"].Points.AddXY
                            (5, Math.Ceiling(((0.0 + (0 * 0) / 100 + 0 +
                            50 / KernelWorker.GetSelectedCharactersData.SPD2 - 50 / KernelWorker.GetSelectedCharactersData.SPD4 +
                            50 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * 100) / 100) * 100 / 100);

                        chartSPD.Series["Default"].Points.AddXY
                            (6, Math.Ceiling(((0.0 + (0 * 0) / 100 + 0 +
                            60 / KernelWorker.GetSelectedCharactersData.SPD2 - 60 / KernelWorker.GetSelectedCharactersData.SPD4 +
                            60 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * 100) / 100) * 100 / 100);

                        chartSPD.Series["Default"].Points.AddXY
                            (7, Math.Ceiling(((0.0 + (0 * 0) / 100 + 0 +
                            70 / KernelWorker.GetSelectedCharactersData.SPD2 - 70 / KernelWorker.GetSelectedCharactersData.SPD4 +
                            70 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * 100) / 100) * 100 / 100);

                        chartSPD.Series["Default"].Points.AddXY
                            (8, Math.Ceiling(((0.0 + (0 * 0) / 100 + 0 +
                            80 / KernelWorker.GetSelectedCharactersData.SPD2 - 80 / KernelWorker.GetSelectedCharactersData.SPD4 +
                            80 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * 100) / 100) * 100 / 100);

                        chartSPD.Series["Default"].Points.AddXY
                            (9, Math.Ceiling(((0.0 + (0 * 0) / 100 + 0 +
                            90 / KernelWorker.GetSelectedCharactersData.SPD2 - 90 / KernelWorker.GetSelectedCharactersData.SPD4 +
                            90 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * 100) / 100) * 100 / 100);

                        chartSPD.Series["Default"].Points.AddXY
                            (10, Math.Ceiling(((0.0 + (0 * 0) / 100 + 0 +
                            100 / KernelWorker.GetSelectedCharactersData.SPD2 - 100 / KernelWorker.GetSelectedCharactersData.SPD4 +
                            100 * KernelWorker.GetSelectedCharactersData.SPD1 + KernelWorker.GetSelectedCharactersData.SPD3) * 100) / 100) * 100 / 100);
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
