using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Doomtrain
{
    public partial class CharChartHP : Form
    {
        public CharChartHP()
        {
            InitializeComponent();
        }

        //this is to move the chart form around
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        private void ChartCharStats_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void ChartCharStats_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void ChartCharStats_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }
        //end moving chart
    }
}
