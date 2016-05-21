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
            saveToolStripMenuItem.Enabled = false;
            saveAsToolStripMenuItem.Enabled = false;

            //MAGIC
            magicIDcomboBox.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(2, magicIDcomboBox.SelectedIndex);
            spellPowerUpDown.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(3, spellPowerUpDown.Value);
            drawResistUpDown.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(4, drawResistUpDown.Value);
            magicElementUpDown.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(5, magicElementUpDown.Value);
            statusUpDown.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, statusUpDown.Value);
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
            //GF
            JGFIDcomboBox.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(0, JGFIDcomboBox.SelectedIndex);
            JGFPowerUpDown.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_GF(1, JGFPowerUpDown.Value);
            JGFHPUpDown.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_GF(2, JGFHPUpDown.Value);
            JGFPowerModUpDown.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_GF(3, JGFPowerModUpDown.Value);
            JGFLevelModUpDown.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_GF(4, JGFLevelModUpDown.Value);
            ability1ComboBox.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(5, ability1ComboBox.SelectedIndex, 0);
            ability2ComboBox.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(5, ability2ComboBox.SelectedIndex, 1);
            ability3ComboBox.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(5, ability3ComboBox.SelectedIndex, 2);
            ability4ComboBox.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(5, ability4ComboBox.SelectedIndex, 3);
            ability5ComboBox.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(5, ability5ComboBox.SelectedIndex, 4);
            ability6ComboBox.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(5, ability6ComboBox.SelectedIndex, 5);
            ability7ComboBox.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(5, ability7ComboBox.SelectedIndex, 6);
            ability8ComboBox.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(5, ability8ComboBox.SelectedIndex, 7);
            ability9ComboBox.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(5, ability9ComboBox.SelectedIndex, 8);
            ability10ComboBox.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(5, ability10ComboBox.SelectedIndex, 9);
            ability11ComboBox.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(5, ability11ComboBox.SelectedIndex, 10);
            ability12ComboBox.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(5, ability12ComboBox.SelectedIndex, 11);
            ability13ComboBox.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(5, ability13ComboBox.SelectedIndex, 12);
            ability14ComboBox.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(5, ability14ComboBox.SelectedIndex, 13);
            ability15ComboBox.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(5, ability15ComboBox.SelectedIndex, 14);
            ability16ComboBox.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(5, ability16ComboBox.SelectedIndex, 15);
            ability17ComboBox.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(5, ability17ComboBox.SelectedIndex, 16);
            ability18ComboBox.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(5, ability18ComboBox.SelectedIndex, 17);
            ability19ComboBox.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(5, ability19ComboBox.SelectedIndex, 18);
            ability20ComboBox.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(5, ability20ComboBox.SelectedIndex, 19);
            ability21ComboBox.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(5, ability21ComboBox.SelectedIndex, 20);
        }




        //used for open/save stuff
        public string existingFilename;



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
            //else
            //{
            //    MessageBox.Show("Please open a file first", "Error");
            //    return;
            //}
        }



        //EXIT
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }



        //ABOUT
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog();
        }


        // MAGIC TRACKBARS LABEL VALUES
        private void eleATKtrackBar_Scroll(object sender, EventArgs e)
        {
            eleATKtrackBarValue.Text = eleATKtrackBar.Value+"%".ToString();
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
                magicElementUpDown.Value = KernelWorker.GetSelectedMagicData.Element;
                statusUpDown.Value = KernelWorker.GetSelectedMagicData.Status1;

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

        private void listBoxJGF_SelectedIndexChanged(object sender, EventArgs e)
        {
            _loaded = false;
            if (KernelWorker.Kernel == null)
                return;
            KernelWorker.ReadGF(listBoxJGF.SelectedIndex);

            try
            {
                JGFIDcomboBox.SelectedIndex = KernelWorker.GetSelectedGFData.GFMagicID;
                JGFPowerUpDown.Value = KernelWorker.GetSelectedGFData.GFPower;
                JGFHPUpDown.Value = KernelWorker.GetSelectedGFData.GFHP;
                JGFPowerModUpDown.Value = KernelWorker.GetSelectedGFData.GFPowerMod;
                JGFLevelModUpDown.Value = KernelWorker.GetSelectedGFData.GFLevelMod;
                ability1ComboBox.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility1;
                ability2ComboBox.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility2;
                ability3ComboBox.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility3;
                ability4ComboBox.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility4;
                ability5ComboBox.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility5;
                ability6ComboBox.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility6;
                ability7ComboBox.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility7;
                ability8ComboBox.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility8;
                ability9ComboBox.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility9;
                ability10ComboBox.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility10;
                ability11ComboBox.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility11;
                ability12ComboBox.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility12;
                ability13ComboBox.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility13;
                ability14ComboBox.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility14;
                ability15ComboBox.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility15;
                ability16ComboBox.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility16;
                ability17ComboBox.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility17;
                ability18ComboBox.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility18;
                ability19ComboBox.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility19;
                ability20ComboBox.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility20;
                ability21ComboBox.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility21;
            }
            catch (Exception eeException)
            {
                MessageBox.Show(eeException.ToString());
            }
            _loaded = true;
        }
    }
}
