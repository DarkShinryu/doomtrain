using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Doomtrain
{
    public partial class AboutBox : Form
    {
        public AboutBox()
        {
            InitializeComponent();
        }


        //Link Forum
        private void linkLabelForum_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://forums.qhimm.com/index.php?topic=XXXXX.0");
        }


        //Link GitHub
        private void linkLabelGitHub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/alexfilth/doomtrain");
        }
    }
}
