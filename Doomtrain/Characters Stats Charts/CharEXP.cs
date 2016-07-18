using Doomtrain.Characters_Stats_Charts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Doomtrain.Characters_Stats_Charts
{
    public partial class CharEXP : Form
    {
        public CharEXP(mainForm mainForm)
        {
            _mainForm = mainForm;
            InitializeComponent();
            ExpChartWorker();

            _mainForm.numericUpDownCharEXP1.ValueChanged += new EventHandler(this.numericUpDownCharEXP_ValueChanged);
            _mainForm.numericUpDownCharEXP2.ValueChanged += new EventHandler(this.numericUpDownCharEXP_ValueChanged);
        }

        private readonly mainForm _mainForm;


        private void buttonExpClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        #region Moving chart

        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        private void chartEXP_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void chartEXP_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void chartEXP_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        #endregion


        #region Chart Values

        private void ExpChartWorker()
        {
            mainForm._loaded = false;
            if (KernelWorker.Kernel == null || KernelWorker.BackupKernel == null)
                return;

            KernelWorker.ReadCharacters(_mainForm.listBoxCharacters.SelectedIndex, KernelWorker.BackupKernel);
            try
            {
                chartEXP.Series["Default"].Points.AddXY
                    (0, (Math.Pow((10 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (10 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10);
                chartEXP.Series["Default"].Points.AddXY
                    (1, (Math.Pow((20 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (20 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10);
                chartEXP.Series["Default"].Points.AddXY
                    (2, (Math.Pow((30 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (30 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10);
                chartEXP.Series["Default"].Points.AddXY
                    (3, (Math.Pow((40 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (40 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10);
                chartEXP.Series["Default"].Points.AddXY
                    (4, (Math.Pow((50 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (50 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10);
                chartEXP.Series["Default"].Points.AddXY
                    (5, (Math.Pow((60 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (60 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10);
                chartEXP.Series["Default"].Points.AddXY
                    (6, (Math.Pow((70 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (70 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10);
                chartEXP.Series["Default"].Points.AddXY
                    (7, (Math.Pow((80 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (80 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10);
                chartEXP.Series["Default"].Points.AddXY
                    (8, (Math.Pow((90 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (90 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10);
                chartEXP.Series["Default"].Points.AddXY
                    (9, (Math.Pow((100 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (100 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10);
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }

            KernelWorker.ReadCharacters(_mainForm.listBoxCharacters.SelectedIndex, KernelWorker.Kernel);
            try
            {
                chartEXP.Series["Current"].Points.AddXY
                    (0, (Math.Pow((10 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (10 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10);
                chartEXP.Series["Current"].Points.AddXY
                    (1, (Math.Pow((20 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (20 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10);
                chartEXP.Series["Current"].Points.AddXY
                    (2, (Math.Pow((30 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (30 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10);
                chartEXP.Series["Current"].Points.AddXY
                    (3, (Math.Pow((40 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (40 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10);
                chartEXP.Series["Current"].Points.AddXY
                    (4, (Math.Pow((50 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (50 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10);
                chartEXP.Series["Current"].Points.AddXY
                    (5, (Math.Pow((60 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (60 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10);
                chartEXP.Series["Current"].Points.AddXY
                    (6, (Math.Pow((70 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (70 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10);
                chartEXP.Series["Current"].Points.AddXY
                    (7, (Math.Pow((80 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (80 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10);
                chartEXP.Series["Current"].Points.AddXY
                    (8, (Math.Pow((90 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (90 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10);
                chartEXP.Series["Current"].Points.AddXY
                    (9, (Math.Pow((100 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (100 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10);
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }
        }

        private void numericUpDownCharEXP_ValueChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                KernelWorker.ReadCharacters(_mainForm.listBoxCharacters.SelectedIndex, KernelWorker.BackupKernel);
                try
                {
                    chartEXP.Series["Default"].Points.Clear();

                    chartEXP.Series["Default"].Points.AddXY
                        (0, (Math.Pow((10 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (10 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10);
                    chartEXP.Series["Default"].Points.AddXY
                        (1, (Math.Pow((20 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (20 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10);
                    chartEXP.Series["Default"].Points.AddXY
                        (2, (Math.Pow((30 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (30 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10);
                    chartEXP.Series["Default"].Points.AddXY
                        (3, (Math.Pow((40 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (40 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10);
                    chartEXP.Series["Default"].Points.AddXY
                        (4, (Math.Pow((50 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (50 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10);
                    chartEXP.Series["Default"].Points.AddXY
                        (5, (Math.Pow((60 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (60 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10);
                    chartEXP.Series["Default"].Points.AddXY
                        (6, (Math.Pow((70 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (70 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10);
                    chartEXP.Series["Default"].Points.AddXY
                        (7, (Math.Pow((80 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (80 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10);
                    chartEXP.Series["Default"].Points.AddXY
                        (8, (Math.Pow((90 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (90 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10);
                    chartEXP.Series["Default"].Points.AddXY
                        (9, (Math.Pow((100 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (100 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10);
                }
                catch (Exception Exception)
                {
                    MessageBox.Show(Exception.ToString());
                }

                KernelWorker.ReadCharacters(_mainForm.listBoxCharacters.SelectedIndex, KernelWorker.Kernel);
                try
                {
                    chartEXP.Series["Current"].Points.Clear();

                    chartEXP.Series["Current"].Points.AddXY
                        (0, (Math.Pow((10 - 1), 2) * (int)_mainForm.numericUpDownCharEXP2.Value) / 256 + (10 - 1) * (int)_mainForm.numericUpDownCharEXP1.Value * 10);
                    chartEXP.Series["Current"].Points.AddXY
                        (1, (Math.Pow((20 - 1), 2) * (int)_mainForm.numericUpDownCharEXP2.Value) / 256 + (20 - 1) * (int)_mainForm.numericUpDownCharEXP1.Value * 10);
                    chartEXP.Series["Current"].Points.AddXY
                        (2, (Math.Pow((30 - 1), 2) * (int)_mainForm.numericUpDownCharEXP2.Value) / 256 + (30 - 1) * (int)_mainForm.numericUpDownCharEXP1.Value * 10);
                    chartEXP.Series["Current"].Points.AddXY
                        (3, (Math.Pow((40 - 1), 2) * (int)_mainForm.numericUpDownCharEXP2.Value) / 256 + (40 - 1) * (int)_mainForm.numericUpDownCharEXP1.Value * 10);
                    chartEXP.Series["Current"].Points.AddXY
                        (4, (Math.Pow((50 - 1), 2) * (int)_mainForm.numericUpDownCharEXP2.Value) / 256 + (50 - 1) * (int)_mainForm.numericUpDownCharEXP1.Value * 10);
                    chartEXP.Series["Current"].Points.AddXY
                        (5, (Math.Pow((60 - 1), 2) * (int)_mainForm.numericUpDownCharEXP2.Value) / 256 + (60 - 1) * (int)_mainForm.numericUpDownCharEXP1.Value * 10);
                    chartEXP.Series["Current"].Points.AddXY
                        (6, (Math.Pow((70 - 1), 2) * (int)_mainForm.numericUpDownCharEXP2.Value) / 256 + (70 - 1) * (int)_mainForm.numericUpDownCharEXP1.Value * 10);
                    chartEXP.Series["Current"].Points.AddXY
                        (7, (Math.Pow((80 - 1), 2) * (int)_mainForm.numericUpDownCharEXP2.Value) / 256 + (80 - 1) * (int)_mainForm.numericUpDownCharEXP1.Value * 10);
                    chartEXP.Series["Current"].Points.AddXY
                        (8, (Math.Pow((90 - 1), 2) * (int)_mainForm.numericUpDownCharEXP2.Value) / 256 + (90 - 1) * (int)_mainForm.numericUpDownCharEXP1.Value * 10);
                    chartEXP.Series["Current"].Points.AddXY
                        (9, (Math.Pow((100 - 1), 2) * (int)_mainForm.numericUpDownCharEXP2.Value) / 256 + (100 - 1) * (int)_mainForm.numericUpDownCharEXP1.Value * 10);
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