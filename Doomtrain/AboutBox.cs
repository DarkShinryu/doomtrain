using System;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Doomtrain
{
    public partial class AboutBox : Form
    {
        //For loading fonts
        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);
        FontFamily ff;
        Font font;

        public AboutBox()
        {
            InitializeComponent();
            labelDoomtrain.Text = $"DOOMTRAIN {Application.ProductVersion.Split('+')[0]}";
            backgroundImage = Properties.Resources.doomtrainbig;

        }
        private Image backgroundImage;

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            var rc = new Rectangle(0,
                this.ClientSize.Height - backgroundImage.Height,
                backgroundImage.Width, backgroundImage.Height);
            e.Graphics.DrawImage(backgroundImage, rc);
        }


        #region Links

        //Link Forum
        private void linkLabelForum_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://forums.qhimm.com/index.php?topic=17090.0");
        }


        //Links GitHub
        private void linkLabelGitHub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/alexfilth/doomtrain");
        }

        private void linkLabelWiki_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/alexfilth/doomtrain/wiki");
        }

        #endregion


        #region Fonts

        private void LoadPrivateFontCollection()
        {
            // Create the byte array and get its length
            byte[] fontArray = Properties.Resources.labelDoomtrain;
            int dataLength = Properties.Resources.labelDoomtrain.Length;

            // ASSIGN MEMORY AND COPY  BYTE[] ON THAT MEMORY ADDRESS
            IntPtr ptrData = Marshal.AllocCoTaskMem(dataLength);
            Marshal.Copy(fontArray, 0, ptrData, dataLength);

            uint cFonts = 0;
            AddFontMemResourceEx(ptrData, (uint)fontArray.Length, IntPtr.Zero, ref cFonts);

            PrivateFontCollection pfc = new PrivateFontCollection();
            //PASS THE FONT TO THE  PRIVATEFONTCOLLECTION OBJECT
            pfc.AddMemoryFont(ptrData, dataLength);

            //FREE THE "UNSAFE" MEMORY
            Marshal.FreeCoTaskMem(ptrData);

            ff = pfc.Families[0];
            font = new Font(ff, 23f, FontStyle.Regular);
        }

        private void LoadLabelDoomtrainFont(Font font)
        {
            FontStyle fontStyle = FontStyle.Regular;
            labelDoomtrain.Font = new Font(ff, 35, fontStyle);
        }

        private void AboutBox_Load(object sender, EventArgs e)
        {
            LoadPrivateFontCollection();
            LoadLabelDoomtrainFont(font);
        }

        #endregion


    }
}
