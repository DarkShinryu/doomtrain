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
        public CharEXP()
        {
            InitializeComponent();
        }

        //this is to move the chart form around
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
        //end moving chart



        private void buttonExpClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
