using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Remoting.Channels;

namespace Doomtrain
{
    public partial class mainForm : Form
    {
        public static bool _loaded = false;
        public mainForm()
        {
            InitializeComponent();


            //for disabling save buttons when no file is open
            saveToolStripMenuItem.Enabled = false;
            saveAsToolStripMenuItem.Enabled = false;
            saveAsToolStripButton.Enabled = false;
            saveToolStripButton.Enabled = false;


            //this is for enabling a cool switch with the listboxes in the gf section :)
            listBoxGFAttacks.Visible = false;
            tabControlGF.SelectedIndexChanged += new EventHandler(tabControlGF_SelectedIndexChanged);


            //MAGIC
            magicIDcomboBox.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(2, magicIDcomboBox.SelectedIndex);
            spellPowerUpDown.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(3, spellPowerUpDown.Value);
            drawResistUpDown.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(4, drawResistUpDown.Value);
            magicElementComboBox.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(5, Magic_GetElement(magicElementComboBox.SelectedIndex)); 
            HPJUpDown.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(7, HPJUpDown.Value);
            STRJUpDown.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(8, STRJUpDown.Value);
            VITJUpDown.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(9, VITJUpDown.Value);
            MAGJUpDown.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(10, MAGJUpDown.Value);
            SPRJUpDown.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(11, SPRJUpDown.Value);
            SPDJUpDown.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(12, SPDJUpDown.Value);
            EVAJUpDown.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(13, EVAJUpDown.Value);
            HITJUpDown.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(14, HITJUpDown.Value);
            LUCKJ.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(15, LUCKJ.Value);
            fireATK.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(16, 0x01);
            iceATK.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(16, 0x01<<1);
            thunderATK.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(16, 0x01<<2);
            earthATK.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(16, 0x01<<3);
            poisonATK.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(16, 0x01<<4);
            windATK.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(16, 0x01<<5);
            waterATK.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(16, 0x01<<6);
            holyATK.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(16, 0x01<<7);
            hitCountUpDown.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(17, hitCountUpDown.Value);
            eleATKtrackBar.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(18, eleATKtrackBar.Value);
            fireDEF.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(19, /*KernelWorker.GetSelectedMagicData.ElemDefenseEN ^ 0x01*/ 0x01);
            iceDEF.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(19, 0x02);
            thunderDEF.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(19, 0x04);
            earthDEF.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(19, 0x08);
            poisonDEF.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(19, 0x10);
            windDEF.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(19, 0x20);
            waterDEF.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(19,0x40);
            holyDEF.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(19, 0x80);
            eleDEFtrackBar.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(20, eleDEFtrackBar.Value);
            deathATK.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(21, 0x0001);
            poisonATKst.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(21, 0x0002);
            petrifyATK.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(21, 0x0004);
            darknessATK.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(21, 0x0008);
            silenceATK.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(21, 0x0010);
            berserkATK.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(21, 0x0020);
            zombieATK.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(21, 0x0040);
            sleepATK.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(21, 0x0080);
            slowATK.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(21, 0x0100);
            stopATK.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(21, 0x0200);
            confusionATK.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(21, 0x0800);
            drainATK.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(21, 0x1000);
            deathDEF.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(22, 0x0001);
            poisonDEFst.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(22, 0x0002);
            petrifyDEF.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(22, 0x0004);
            darknessDEF.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(22, 0x0008);
            silenceDEF.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(22, 0x0010);
            berserkDEF.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(22, 0x0020);
            zombieDEF.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(22, 0x0040);
            sleepDEF.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(22, 0x0080);
            slowDEF.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(22, 0x0100);
            stopDEF.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(22, 0x0200);
            curseDEF.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(22, 0x0400);
            confusionDEF.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(22, 0x0800);
            drainDEF.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(22, 0x1000);
            stATKtrackBar.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(23, stATKtrackBar.Value);
            stDEFtrackBar.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(24, stATKtrackBar.Value);
            sleepcheckBox8.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x01, 0);
            hastecheckBox21.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x02, 0);
            slowcheckBox6.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x04, 0);
            stopcheckBox3.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x08, 0);
            regencheckBox20.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x10, 0);
            protectcheckBox22.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x20, 0);
            shellcheckBox19.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x40, 0);
            reflectcheckBox11.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x80, 0);
            auracheckBox14.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x01, 1);
            cursecheckBox17.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x02, 1);
            doomcheckBox16.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x04, 1);
            invincicheckBox18.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x08, 1);
            petrifyingcheckBox5.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x10, 1);
            floatcheckBox15.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x20, 1);
            confusioncheckBox4.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x40, 1);
            draincheckBox13.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x80, 1);
            ejectcheckBox9.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x01, 2);
            doublecheckBox23.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x02, 2);
            triplcheckBox24.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x04, 2);
            defendcheckBox25.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x08, 2);
            vit0checkBox26.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x01, 3);
            deathcheckBox10.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x01, 4);
            poisoncheckBox27.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x02, 4);
            petrifycheckBox28.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x04, 4);
            darknescheckBox1.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x08, 4);
            silencecheckBox2.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x10, 4);
            berserkcheckBox7.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x20, 4);
            zombiecheckBox12.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x40, 4);



            //GF
            GFIDcomboBox.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(0, GFIDcomboBox.SelectedIndex);
            GFPowerUpDown.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_GF(1, GFPowerUpDown.Value);
            GFHPUpDown.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_GF(2, GFHPUpDown.Value);
            GFPowerModUpDown.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_GF(3, GFPowerModUpDown.Value);
            GFLevelModUpDown.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_GF(4, GFLevelModUpDown.Value);
            GFAbility1ComboBox.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(5, GFAbility1ComboBox.SelectedIndex, 0);
            GFAbility2ComboBox.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(5, GFAbility2ComboBox.SelectedIndex, 1);
            GFAbility3ComboBox.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(5, GFAbility3ComboBox.SelectedIndex, 2);
            GFAbility4ComboBox.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(5, GFAbility4ComboBox.SelectedIndex, 3);
            GFAbility5ComboBox.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(5, GFAbility5ComboBox.SelectedIndex, 4);
            GFAbility6ComboBox.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(5, GFAbility6ComboBox.SelectedIndex, 5);
            GFAbility7ComboBox.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(5, GFAbility7ComboBox.SelectedIndex, 6);
            GFAbility8ComboBox.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(5, GFAbility8ComboBox.SelectedIndex, 7);
            GFAbility9ComboBox.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(5, GFAbility9ComboBox.SelectedIndex, 8);
            GFAbility10ComboBox.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(5, GFAbility10ComboBox.SelectedIndex, 9);
            GFAbility11ComboBox.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(5, GFAbility11ComboBox.SelectedIndex, 10);
            GFAbility12ComboBox.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(5, GFAbility12ComboBox.SelectedIndex, 11);
            GFAbility13ComboBox.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(5, GFAbility13ComboBox.SelectedIndex, 12);
            GFAbility14ComboBox.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(5, GFAbility14ComboBox.SelectedIndex, 13);
            GFAbility15ComboBox.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(5, GFAbility15ComboBox.SelectedIndex, 14);
            GFAbility16ComboBox.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(5, GFAbility16ComboBox.SelectedIndex, 15);
            GFAbility17ComboBox.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(5, GFAbility17ComboBox.SelectedIndex, 16);
            GFAbility18ComboBox.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(5, GFAbility18ComboBox.SelectedIndex, 17);
            GFAbility19ComboBox.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(5, GFAbility19ComboBox.SelectedIndex, 18);
            GFAbility20ComboBox.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(5, GFAbility20ComboBox.SelectedIndex, 19);
            GFAbility21ComboBox.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(5, GFAbility21ComboBox.SelectedIndex, 20);
        }


        
        public string existingFilename; //used for open/save stuff



        //OPEN
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Open FF8 kernel.bin";
            openFileDialog.Filter = "FF8 Kernel File|*.bin";
            openFileDialog.FileName = "kernel.bin";



            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            {
                using (var fileStream = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                {
                    using (var BR = new BinaryReader(fileStream))
                    {
                        KernelWorker.ReadKernel(BR.ReadBytes((int)fileStream.Length));
                    }
                        
                }
                    

                existingFilename = openFileDialog.FileName;
                saveToolStripMenuItem.Enabled = true;
                saveAsToolStripMenuItem.Enabled = true;
                saveToolStripButton.Enabled = true;
                saveAsToolStripButton.Enabled = true;
                return;
            }
        }



        //SAVE
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(string.IsNullOrEmpty(existingFilename)) && KernelWorker.Kernel != null)
            {
                File.WriteAllBytes(existingFilename, KernelWorker.Kernel);
                return;
            }
        }



        // SAVE AS
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveAsDialog = new SaveFileDialog();
            saveAsDialog.Title = "Save FF8 kernel.bin";
            saveAsDialog.Filter = "FF8 Kernel File|*.bin";
            saveAsDialog.FileName = Path.GetFileName(existingFilename);

            if (!(string.IsNullOrEmpty(existingFilename)) && KernelWorker.Kernel != null)
            {
                if (saveAsDialog.ShowDialog() != DialogResult.OK) return;
                {
                    File.WriteAllBytes(saveAsDialog.FileName, KernelWorker.Kernel);
                    return;
                }
            }
        }



        //EXIT
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }



        //TOOLBAR
        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Open FF8 kernel.bin";
            openFileDialog.Filter = "FF8 Kernel File|*.bin";
            openFileDialog.FileName = "kernel.bin";



            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            {
                using (var fileStream = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                {
                    using (var BR = new BinaryReader(fileStream))
                    {
                        KernelWorker.ReadKernel(BR.ReadBytes((int)fileStream.Length));
                    }

                }


                existingFilename = openFileDialog.FileName;
                saveToolStripMenuItem.Enabled = true;
                saveAsToolStripMenuItem.Enabled = true;
                saveToolStripButton.Enabled = true;
                saveAsToolStripButton.Enabled = true;
                return;
            }
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            if (!(string.IsNullOrEmpty(existingFilename)) && KernelWorker.Kernel != null)
            {
                File.WriteAllBytes(existingFilename, KernelWorker.Kernel);
                return;
            }
        }

        private void saveAsToolStripButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveAsDialog = new SaveFileDialog();
            saveAsDialog.Title = "Save FF8 kernel.bin";
            saveAsDialog.Filter = "FF8 Kernel File|*.bin";
            saveAsDialog.FileName = Path.GetFileName(existingFilename);

            if (!(string.IsNullOrEmpty(existingFilename)) && KernelWorker.Kernel != null)
            {
                if (saveAsDialog.ShowDialog() != DialogResult.OK) return;
                {
                    File.WriteAllBytes(saveAsDialog.FileName, KernelWorker.Kernel);
                    return;
                }
            }
        }



        //ABOUT
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog();
        }


        // MAGIC TRACKBARS LABEL VALUES
        private void eleATKtrackBar_Scroll(object sender, EventArgs e)
        {
            eleATKtrackBarValue.Text = eleATKtrackBar.Value + "%".ToString();
        }

        private void eleDEFtrackBar_Scroll(object sender, EventArgs e)
        {
            eleDEFtrackBarValue.Text = eleDEFtrackBar.Value + "%".ToString();
        }

        private void stATKtrackBar_Scroll(object sender, EventArgs e)
        {
            stATKtrackBarValue.Text = stATKtrackBar.Value + "%".ToString();
        }

        private void stDEFtrackBar_Scroll(object sender, EventArgs e)
        {
            stDEFtrackBarValue.Text = stDEFtrackBar.Value + "%".ToString();
        }



        //GF LISTBOXES SWITCH
        private void tabControlGF_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlGF.SelectedIndex == 0)
            {
                listBoxGFAttacks.Visible = false;
                listBoxGF.Visible = true;
            }

            else if (tabControlGF.SelectedIndex == 1)
            {
                listBoxGF.Visible = false;
                listBoxGFAttacks.Visible = true;
            }
        }

        private int Magic_GetElement()
        {
            return KernelWorker.GetSelectedMagicData.Element == KernelWorker.Element.Fire
                        ? 0
                        : KernelWorker.GetSelectedMagicData.Element == KernelWorker.Element.Ice
                            ? 1
                            : KernelWorker.GetSelectedMagicData.Element == KernelWorker.Element.Thunder
                                ? 2
                                : KernelWorker.GetSelectedMagicData.Element == KernelWorker.Element.Earth
                                    ? 3
                                    : KernelWorker.GetSelectedMagicData.Element == KernelWorker.Element.Poison
                                        ? 4
                                        : KernelWorker.GetSelectedMagicData.Element == KernelWorker.Element.Wind
                                            ? 5
                                            : KernelWorker.GetSelectedMagicData.Element ==
                                              KernelWorker.Element.Water
                                                ? 6
                                                : KernelWorker.GetSelectedMagicData.Element ==
                                                  KernelWorker.Element.Holy
                                                    ? 7
                                                    : KernelWorker.GetSelectedMagicData.Element ==
                                                      KernelWorker.Element.NonElemental
                                                        ? magicElementComboBox.Items.Count - 1
                                                        : 0;
        }

        private byte Magic_GetElement(int Index)
        {
            byte elem = (byte) (Index == 8 ? (byte)KernelWorker.Element.NonElemental :
                Index == 0 ? (byte)KernelWorker.Element.Fire :
                Index == 1 ? (byte)KernelWorker.Element.Ice :
                Index == 2 ? (byte)KernelWorker.Element.Thunder :
                Index == 3 ? (byte)KernelWorker.Element.Earth :
                Index == 4 ? (byte)KernelWorker.Element.Poison :
                Index == 5 ? (byte)KernelWorker.Element.Wind :
                Index == 6 ? (byte)KernelWorker.Element.Water :
                Index == 7 ? (byte)KernelWorker.Element.Holy : 
                0x00 /*ErrorHandler*/);
            return elem;
        }

        private void MagicStatusWorker()
        {
            //sleepcheckBox8.Checked =  ? true : false
            sleepcheckBox8.Checked = (KernelWorker.GetSelectedMagicData.Status1 & 0x01) >= 1 ? true : false;
            hastecheckBox21.Checked = (KernelWorker.GetSelectedMagicData.Status1 & 0x02) >= 1 ? true : false;
            slowcheckBox6.Checked = (KernelWorker.GetSelectedMagicData.Status1 & 0x04) >= 1 ? true : false;
            stopcheckBox3.Checked = (KernelWorker.GetSelectedMagicData.Status1 & 0x08) >= 1 ? true : false;
            regencheckBox20.Checked = (KernelWorker.GetSelectedMagicData.Status1 & 0x10) >= 1 ? true : false;
            protectcheckBox22.Checked = (KernelWorker.GetSelectedMagicData.Status1 & 0x20) >= 1 ? true : false;
            shellcheckBox19.Checked = (KernelWorker.GetSelectedMagicData.Status1 & 0x40) >= 1 ? true : false;
            reflectcheckBox11.Checked = (KernelWorker.GetSelectedMagicData.Status1 & 0x80) >= 1 ? true : false;

            auracheckBox14.Checked = (KernelWorker.GetSelectedMagicData.Status2 & 0x01) >= 1 ? true : false;
            cursecheckBox17.Checked = (KernelWorker.GetSelectedMagicData.Status2 & 0x02) >= 1 ? true : false;
            doomcheckBox16.Checked = (KernelWorker.GetSelectedMagicData.Status2 & 0x04) >= 1 ? true : false;
            invincicheckBox18.Checked = (KernelWorker.GetSelectedMagicData.Status2 & 0x08) >= 1 ? true : false;
            petrifyingcheckBox5.Checked = (KernelWorker.GetSelectedMagicData.Status2 & 0x10) >= 1 ? true : false;
            floatcheckBox15.Checked = (KernelWorker.GetSelectedMagicData.Status2 & 0x20) >= 1 ? true : false;
            confusioncheckBox4.Checked = (KernelWorker.GetSelectedMagicData.Status2 & 0x40) >= 1 ? true : false;
            draincheckBox13.Checked = (KernelWorker.GetSelectedMagicData.Status2 & 0x80) >= 1 ? true : false;

            ejectcheckBox9.Checked = (KernelWorker.GetSelectedMagicData.Status3 & 0x01) >= 1 ? true : false;
            doublecheckBox23.Checked = (KernelWorker.GetSelectedMagicData.Status3 & 0x02) >= 1 ? true : false;
            triplcheckBox24.Checked = (KernelWorker.GetSelectedMagicData.Status3 & 0x04) >= 1 ? true : false;
            defendcheckBox25.Checked = (KernelWorker.GetSelectedMagicData.Status3 & 0x08) >= 1 ? true : false;

            vit0checkBox26.Checked = (KernelWorker.GetSelectedMagicData.Status4 & 0x01) >= 1 ? true : false;

            deathcheckBox10.Checked = (KernelWorker.GetSelectedMagicData.Status5 & 0x01) >= 1 ? true : false;
            poisoncheckBox27.Checked = (KernelWorker.GetSelectedMagicData.Status5 & 0x02) >= 1 ? true : false;
            petrifycheckBox28.Checked = (KernelWorker.GetSelectedMagicData.Status5 & 0x04) >= 1 ? true : false;
            darknescheckBox1.Checked = (KernelWorker.GetSelectedMagicData.Status5 & 0x08) >= 1 ? true : false;
            silencecheckBox2.Checked = (KernelWorker.GetSelectedMagicData.Status5 & 0x10) >= 1 ? true : false;
            berserkcheckBox7.Checked = (KernelWorker.GetSelectedMagicData.Status5 & 0x20) >= 1 ? true : false;
            zombiecheckBox12.Checked = (KernelWorker.GetSelectedMagicData.Status5 & 0x40) >= 1 ? true : false;
        }

        //MAGIC
        private void listBoxMagic_SelectedIndexChanged(object sender, EventArgs e)
        {
            _loaded = false;
            if (KernelWorker.Kernel == null)
                return;
            KernelWorker.ReadMagic(listBoxMagic.SelectedIndex);
            try
            {
                magicIDcomboBox.SelectedIndex = KernelWorker.GetSelectedMagicData.MagicID-1; //As in Vanilla FF8.exe: sub ESI, 1
                spellPowerUpDown.Value = KernelWorker.GetSelectedMagicData.SpellPower;
                drawResistUpDown.Value = KernelWorker.GetSelectedMagicData.DrawResist;
                hitCountUpDown.Value = KernelWorker.GetSelectedMagicData.HitCount;
                magicElementComboBox.SelectedIndex = Magic_GetElement();
                MagicStatusWorker();
                HPJUpDown.Value = KernelWorker.GetSelectedMagicData.HP;
                VITJUpDown.Value = KernelWorker.GetSelectedMagicData.VIT;
                SPRJUpDown.Value = KernelWorker.GetSelectedMagicData.SPR;
                STRJUpDown.Value = KernelWorker.GetSelectedMagicData.STR;
                MAGJUpDown.Value = KernelWorker.GetSelectedMagicData.MAG;
                SPDJUpDown.Value = KernelWorker.GetSelectedMagicData.SPD;
                EVAJUpDown.Value = KernelWorker.GetSelectedMagicData.EVA;
                HITJUpDown.Value = KernelWorker.GetSelectedMagicData.HIT;
                LUCKJ.Value = KernelWorker.GetSelectedMagicData.LUCK;
                StatusHoldWorker(0,KernelWorker.GetSelectedMagicData.ElemAttackEN);
                eleATKtrackBar.Value = KernelWorker.GetSelectedMagicData.ElemAttackVAL;
                StatusHoldWorker(1, KernelWorker.GetSelectedMagicData.ElemDefenseEN);
                eleDEFtrackBar.Value = KernelWorker.GetSelectedMagicData.ElemDefenseVAL;
                StatusHoldWorker(2,KernelWorker.GetSelectedMagicData.Status1, KernelWorker.GetSelectedMagicData.StatusATKEN, KernelWorker.GetSelectedMagicData.Status2, KernelWorker.GetSelectedMagicData.Status3, KernelWorker.GetSelectedMagicData.Status4, KernelWorker.GetSelectedMagicData.Status5);
                stATKtrackBar.Value = KernelWorker.GetSelectedMagicData.StatusATKval;
                StatusHoldWorker(3, KernelWorker.GetSelectedMagicData.Status1,KernelWorker.GetSelectedMagicData.StatusDefEN , KernelWorker.GetSelectedMagicData.Status2, KernelWorker.GetSelectedMagicData.Status3, KernelWorker.GetSelectedMagicData.Status4, KernelWorker.GetSelectedMagicData.Status5);
                stDEFtrackBar.Value = KernelWorker.GetSelectedMagicData.StatusDEFval;
            }
            catch(Exception e_)
            {
                Console.WriteLine(e_.ToString());
            }
            _loaded = true;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="State">0= Elemental Attack; 1=Elemental Defense; 2=Status Attack; 3=Status Defense</param>
        /// <param name="Element">Byte or ushort with bitvalue</param>
        private void StatusHoldWorker(byte State, byte Element = 0, ushort Stat=0, byte Status2 = 0, byte Status3 = 0, byte Status4= 0, byte Status5=0)
        {
            if(State==0)
            {
                switch(Element)
                {
                    case 0x00:
                        goto default;
                    case 0x01:
                        fireATK.Checked = true;
                        return;
                    case 0x02:
                        iceATK.Checked = true;
                        return;
                    case 0x04:
                        thunderATK.Checked = true;
                        return;
                    case 0x08:
                        earthATK.Checked = true;
                        return;
                    case 0x10:
                        poisonATK.Checked = true;
                        return;
                    case 0x20:
                        windATK.Checked = true;
                        return;
                    case 0x40:
                        waterATK.Checked = true;
                        return;
                    case 0x80:
                        holyATK.Checked = true;
                        return;
                    default:
                        fireATK.Checked = true; //A trick to not loop every control
                        fireATK.Checked = false;
                        return;
                }
            }
            if (State == 1)
            {
                ResetUI(1);
                if((Element & 0x01) > 0) //If Element AND 01 is bigger than 0 - Classic bitwise logic operation. :)
                {//Extreme better/faster than checking BitArray or making all possible cases (255 cases!)
                    fireDEF.Checked = true;
                }
                if ((Element & 0x02) > 0)
                {
                    iceDEF.Checked = true;
                }
                if ((Element & 0x04) > 0)
                {
                    thunderDEF.Checked = true;
                }
                if ((Element & 0x08) > 0)
                {
                    earthDEF.Checked = true;
                }
                if ((Element & 0x10) > 0)
                {
                    poisonDEF.Checked = true;
                }
                if ((Element & 0x20) > 0)
                {
                    windDEF.Checked = true;
                }
                if ((Element & 0x40) > 0)
                {
                    waterDEF.Checked = true;
                }
                if ((Element & 0x80) > 0)
                {
                    holyDEF.Checked = true;
                }
                if(Element == 0x00) //null case, no magic trick this time. (Although there is one, but it's slow)
                {
                    ResetUI(1);
                }

            }

            if (State == 2)
            {
                ResetUI(2);
                if ((Stat & 0x0001) > 0)
                    deathATK.Checked = true; //DEATH
                if ((Stat & 0x0002) > 0)
                    poisonATKst.Checked = true; //POISON
                if ((Stat & 0x0004) > 0)
                    petrifyATK.Checked = true; //PETRIFY
                if ((Stat & 0x0008) > 0)
                    darknessATK.Checked = true; //DARKNESS
                if ((Stat & 0x0010) > 0)
                    silenceATK.Checked = true; //SILENCE
                if ((Stat & 0x0020) > 0)
                    berserkATK.Checked = true; //BERSERK
                if ((Stat & 0x0040) > 0)
                    zombieATK.Checked = true; //ZOMBIE
                if ((Stat & 0x0080) > 0)
                    sleepATK.Checked = true; //SLEEP
                if ((Stat & 0x0100) > 0)
                    slowATK.Checked = true; //SLOW
                if ((Stat & 0x0200) > 0)
                    stopATK.Checked = true; //STOP
                if ((Stat & 0x0800) > 0)
                    confusionATK.Checked = true; //CONFUSE
                if ((Stat & 0x1000) > 0)
                    drainATK.Checked = true; //DRAIN

                /*  ===UNUSED/IMPOSSIBLE CASE
                if ((Stat & 0x0080 << 6) > 0)
                    Console.WriteLine("UNUSED");
                if ((Stat & 0x0080 << 7) > 0)
                    Console.WriteLine("UNKNOWN! 7");
                if ((Stat & 0x0080 << 8) > 0)
                    Console.WriteLine("UNKNOWN! 8"); */


                if (Stat == 0)
                    ResetUI(2);
            }
            if (State == 3)
            {
                ResetUI(3);
                if ((Stat & 0x01) > 0)
                    deathDEF.Checked = true; //DEATH
                if ((Stat & 0x02) > 0)
                    poisonDEFst.Checked = true; //POISON
                if ((Stat & 0x04) > 0)
                    petrifyDEF.Checked = true; //PETRIFY
                if ((Stat & 0x08) > 0)
                    darknessDEF.Checked = true; //DARKNESS
                if ((Stat & 0x10) > 0)
                    silenceDEF.Checked = true; //SILENCE
                if ((Stat & 0x20) > 0)
                    berserkDEF.Checked = true; //BERSERK
                if ((Stat & 0x40) > 0)
                    zombieDEF.Checked = true; //ZOMBIE
                if ((Stat & 0x80) > 0)
                    sleepDEF.Checked = true; //SLEEP
                if ((Stat & 0x100) > 0)
                    slowDEF.Checked = true; //SLOW
                if ((Stat & 0x0200) > 0)
                    stopDEF.Checked = true; //STOP
                if ((Stat & 0x0400 ) > 0)
                    curseDEF.Checked = true; //PAIN
                if ((Stat & 0x0800) > 0)
                    confusionDEF.Checked = true; //CONFUSE
                if ((Stat & 0x1000) > 0)
                    drainDEF.Checked = true; //DRAIN

                /*
                if ((Stat & 0x0080 << 7) > 0)
                    Console.WriteLine("UNKNOWN! 7");
                if ((Stat & 0x0080 << 8) > 0)
                    Console.WriteLine("UNKNOWN! 8"); */


                if (Stat == 0)
                    ResetUI(3);
            }
        }



        //TRACKBAR VALUES
        private void eleATKtrackBar_ValueChanged(object sender, EventArgs e)
        {
            eleATKtrackBarValue.Text = eleATKtrackBar.Value + "%".ToString();
        }
        private void eleDEFtrackBar_ValueChanged(object sender, EventArgs e)
        {
            eleDEFtrackBarValue.Text = eleDEFtrackBar.Value + "%".ToString();
        }
        private void stATKtrackBar_ValueChanged(object sender, EventArgs e)
        {
            stATKtrackBarValue.Text = stATKtrackBar.Value + "%".ToString();
        }
        private void stDEFtrackBar_ValueChanged(object sender, EventArgs e)
        {
            stDEFtrackBarValue.Text = stDEFtrackBar.Value + "%".ToString();
        }



        //RESET UI
        private void ResetUI(byte State)
        {
            if(State==1)
            {
                iceDEF.Checked = false;
                earthDEF.Checked = false;
                poisonDEF.Checked = false;
                windDEF.Checked = false;
                waterDEF.Checked = false;
                holyDEF.Checked = false;
                fireDEF.Checked = false;
                thunderDEF.Checked = false;
            }
            if(State==2)
            {
                berserkATK.Checked = false;
                confusionATK.Checked = false;
                darknessATK.Checked = false;
                deathATK.Checked = false;
                drainATK.Checked = false;
                petrifyATK.Checked = false;
                poisonATKst.Checked = false;
                silenceATK.Checked = false;
                sleepATK.Checked = false;
                slowATK.Checked = false;
                stopATK.Checked = false;
                zombieATK.Checked = false;

            }
            if(State==3)
            {
                berserkDEF.Checked = false;
                confusionDEF.Checked = false;
                darknessDEF.Checked = false;
                deathDEF.Checked = false;
                drainDEF.Checked = false;
                petrifyDEF.Checked = false;
                poisonDEFst.Checked = false;
                silenceDEF.Checked = false;
                sleepDEF.Checked = false;
                slowDEF.Checked = false;
                stopDEF.Checked = false;
                zombieDEF.Checked = false;
                curseDEF.Checked = false;
                darknessDEF.Checked = false;
            }
        }



        //GFs
        private void listBoxGF_SelectedIndexChanged(object sender, EventArgs e)
        {
            _loaded = false;
            if (KernelWorker.Kernel == null)
                return;
            KernelWorker.ReadGF(listBoxGF.SelectedIndex);

            try
            {
                GFIDcomboBox.SelectedIndex = KernelWorker.GetSelectedGFData.GFMagicID;
                GFPowerUpDown.Value = KernelWorker.GetSelectedGFData.GFPower;
                GFHPUpDown.Value = KernelWorker.GetSelectedGFData.GFHP;
                GFPowerModUpDown.Value = KernelWorker.GetSelectedGFData.GFPowerMod;
                GFLevelModUpDown.Value = KernelWorker.GetSelectedGFData.GFLevelMod;
                GFAbility1ComboBox.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility1;
                GFAbility2ComboBox.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility2;
                GFAbility3ComboBox.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility3;
                GFAbility4ComboBox.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility4;
                GFAbility5ComboBox.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility5;
                GFAbility6ComboBox.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility6;
                GFAbility7ComboBox.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility7;
                GFAbility8ComboBox.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility8;
                GFAbility9ComboBox.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility9;
                GFAbility10ComboBox.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility10;
                GFAbility11ComboBox.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility11;
                GFAbility12ComboBox.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility12;
                GFAbility13ComboBox.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility13;
                GFAbility14ComboBox.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility14;
                GFAbility15ComboBox.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility15;
                GFAbility16ComboBox.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility16;
                GFAbility17ComboBox.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility17;
                GFAbility18ComboBox.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility18;
                GFAbility19ComboBox.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility19;
                GFAbility20ComboBox.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility20;
                GFAbility21ComboBox.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility21;
            }
            catch (Exception eeException)
            {
                MessageBox.Show(eeException.ToString());
            }
            _loaded = true;
        }


    }
}