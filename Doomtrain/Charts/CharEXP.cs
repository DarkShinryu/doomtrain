using System;
using System.Drawing;
using System.Windows.Forms;

namespace Doomtrain.Charts
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

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        


        #region Read Chart values
        
        private void ExpChartWorker()
        {
            KernelWorker.ReadCharacters(_mainForm.listBoxCharacters.SelectedIndex, KernelWorker.BackupKernel);
            try
            {
                chartEXP.Series["Default"].Points.AddXY
                    (0, Math.Floor((Math.Pow((2 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (2 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10) * 100 / 100);

                chartEXP.Series["Default"].Points.AddXY
                    (1, Math.Floor((Math.Pow((10 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (10 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10) * 100 / 100);

                chartEXP.Series["Default"].Points.AddXY
                    (2, Math.Floor((Math.Pow((20 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (20 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10) * 100 / 100);

                chartEXP.Series["Default"].Points.AddXY
                    (3, Math.Floor((Math.Pow((30 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (30 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10) * 100 / 100);

                chartEXP.Series["Default"].Points.AddXY
                    (4, Math.Floor((Math.Pow((40 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (40 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10) * 100 / 100);

                chartEXP.Series["Default"].Points.AddXY
                    (5, Math.Floor((Math.Pow((50 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (50 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10) * 100 / 100);

                chartEXP.Series["Default"].Points.AddXY
                    (6, Math.Floor((Math.Pow((60 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (60 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10) * 100 / 100);

                chartEXP.Series["Default"].Points.AddXY
                    (7, Math.Floor((Math.Pow((70 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (70 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10) * 100 / 100);

                chartEXP.Series["Default"].Points.AddXY
                    (8, Math.Floor((Math.Pow((80 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (80 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10) * 100 / 100);

                chartEXP.Series["Default"].Points.AddXY
                    (9, Math.Floor((Math.Pow((90 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (90 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10) * 100 / 100);

                chartEXP.Series["Default"].Points.AddXY
                    (10, Math.Floor((Math.Pow((100 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (100 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10) * 100 / 100);
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }


            KernelWorker.ReadCharacters(_mainForm.listBoxCharacters.SelectedIndex, KernelWorker.Kernel);
            try
            {
                chartEXP.Series["Current"].Points.AddXY
                    (0, Math.Floor((Math.Pow((2 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (2 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10) * 100 / 100);

                chartEXP.Series["Current"].Points.AddXY
                    (1, Math.Floor((Math.Pow((10 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (10 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 *10) * 100 /100);

                chartEXP.Series["Current"].Points.AddXY
                    (2, Math.Floor((Math.Pow((20 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (20 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 *10) * 100 /100);

                chartEXP.Series["Current"].Points.AddXY
                    (3, Math.Floor((Math.Pow((30 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (30 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 *10) * 100 /100);

                chartEXP.Series["Current"].Points.AddXY
                    (4, Math.Floor((Math.Pow((40 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (40 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 *10) * 100 /100);

                chartEXP.Series["Current"].Points.AddXY
                    (5, Math.Floor((Math.Pow((50 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (50 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 *10) * 100 /100);

                chartEXP.Series["Current"].Points.AddXY
                    (6, Math.Floor((Math.Pow((60 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (60 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 *10) * 100 /100);

                chartEXP.Series["Current"].Points.AddXY
                    (7, Math.Floor((Math.Pow((70 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (70 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 *10) * 100 /100);

                chartEXP.Series["Current"].Points.AddXY
                    (8, Math.Floor((Math.Pow((80 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (80 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 *10) * 100 /100);

                chartEXP.Series["Current"].Points.AddXY
                    (9, Math.Floor((Math.Pow((90 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (90 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 *10) * 100 /100);

                chartEXP.Series["Current"].Points.AddXY
                    (10, Math.Floor((Math.Pow((100 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (100 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 *10) * 100 /100);
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }
        }

        #endregion

        #region Change chart values

        private void numericUpDownCharEXP_ValueChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                KernelWorker.ReadCharacters(_mainForm.listBoxCharacters.SelectedIndex, KernelWorker.BackupKernel);
                try
                {
                    chartEXP.Series["Default"].Points.Clear();

                    chartEXP.Series["Default"].Points.AddXY
                        (0, Math.Floor((Math.Pow((2 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (2 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10) * 100 / 100);

                    chartEXP.Series["Default"].Points.AddXY
                        (1, Math.Floor((Math.Pow((10 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (10 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10) * 100 / 100);

                    chartEXP.Series["Default"].Points.AddXY
                        (2, Math.Floor((Math.Pow((20 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (20 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10) * 100 / 100);

                    chartEXP.Series["Default"].Points.AddXY
                        (3, Math.Floor((Math.Pow((30 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (30 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10) * 100 / 100);

                    chartEXP.Series["Default"].Points.AddXY
                        (4, Math.Floor((Math.Pow((40 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (40 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10) * 100 / 100);

                    chartEXP.Series["Default"].Points.AddXY
                        (5, Math.Floor((Math.Pow((50 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (50 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10) * 100 / 100);

                    chartEXP.Series["Default"].Points.AddXY
                        (6, Math.Floor((Math.Pow((60 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (60 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10) * 100 / 100);

                    chartEXP.Series["Default"].Points.AddXY
                        (7, Math.Floor((Math.Pow((70 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (70 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10) * 100 / 100);

                    chartEXP.Series["Default"].Points.AddXY
                        (8, Math.Floor((Math.Pow((80 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (80 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10) * 100 / 100);

                    chartEXP.Series["Default"].Points.AddXY
                        (9, Math.Floor((Math.Pow((90 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (90 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10) * 100 / 100);

                    chartEXP.Series["Default"].Points.AddXY
                        (10, Math.Floor((Math.Pow((100 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (100 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10) * 100 / 100);
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
                        (0, Math.Floor((Math.Pow((2 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (2 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10) * 100 / 100);

                    chartEXP.Series["Current"].Points.AddXY
                        (1, Math.Floor((Math.Pow((10 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (10 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10) * 100 / 100);

                    chartEXP.Series["Current"].Points.AddXY
                        (2, Math.Floor((Math.Pow((20 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (20 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10) * 100 / 100);

                    chartEXP.Series["Current"].Points.AddXY
                        (3, Math.Floor((Math.Pow((30 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (30 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10) * 100 / 100);

                    chartEXP.Series["Current"].Points.AddXY
                        (4, Math.Floor((Math.Pow((40 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (40 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10) * 100 / 100);

                    chartEXP.Series["Current"].Points.AddXY
                        (5, Math.Floor((Math.Pow((50 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (50 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10) * 100 / 100);

                    chartEXP.Series["Current"].Points.AddXY
                        (6, Math.Floor((Math.Pow((60 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (60 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10) * 100 / 100);

                    chartEXP.Series["Current"].Points.AddXY
                        (7, Math.Floor((Math.Pow((70 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (70 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10) * 100 / 100);

                    chartEXP.Series["Current"].Points.AddXY
                        (8, Math.Floor((Math.Pow((80 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (80 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10) * 100 / 100);

                    chartEXP.Series["Current"].Points.AddXY
                        (9, Math.Floor((Math.Pow((90 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (90 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10) * 100 / 100);

                    chartEXP.Series["Current"].Points.AddXY
                        (10, Math.Floor((Math.Pow((100 - 1), 2) * KernelWorker.GetSelectedCharactersData.EXP2) / 256 + (100 - 1) * KernelWorker.GetSelectedCharactersData.EXP1 * 10) * 100 / 100);
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