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
    public partial class CharHP : Form
    {
        public CharHP()
        {
            InitializeComponent();
        }

        //this is to move the chart form around
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        private void chartHP_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void chartHP_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void chartHP_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }
        //end moving chart




        private void buttonHPClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
