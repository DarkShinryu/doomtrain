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



            //MAGIC
            comboBoxMagicMagicID.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(2, comboBoxMagicMagicID.SelectedIndex);
            numericUpDownMagicSpellPower.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(3, numericUpDownMagicSpellPower.Value);
            numericUpDownMagicDrawResist.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(4, numericUpDownMagicDrawResist.Value);
            comboBoxMagicElement.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(5, Magic_GetElement(comboBoxMagicElement.SelectedIndex));
            numericUpDownMagicHPJ.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(7, numericUpDownMagicHPJ.Value);
            numericUpDownMagicSTRJ.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(8, numericUpDownMagicSTRJ.Value);
            numericUpDownMagicVITJ.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(9, numericUpDownMagicVITJ.Value);
            numericUpDownMagicMAGJ.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(10, numericUpDownMagicMAGJ.Value);
            numericUpDownMagicSPRJ.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(11, numericUpDownMagicSPRJ.Value);
            numericUpDownMagicSPDJ.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(12, numericUpDownMagicSPDJ.Value);
            numericUpDownMagicEVAJ.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(13, numericUpDownMagicEVAJ.Value);
            numericUpDownMagicHITJ.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(14, numericUpDownMagicHITJ.Value);
            numericUpDownMagicLUCKJ.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(15, numericUpDownMagicLUCKJ.Value);
            radioButtonJElemAttackNElem.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(16, 0x00);
            radioButtonJElemAttackFire.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(16, 0x01);
            radioButtonJElemAttackIce.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(16, 0x01 << 1);
            radioButtonJElemAttackThunder.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(16, 0x01 << 2);
            radioButtonJElemAttackEarth.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(16, 0x01 << 3);
            radioButtonJElemAttackPoison.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(16, 0x01 << 4);
            radioButtonJElemAttackWind.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(16, 0x01 << 5);
            radioButtonJElemAttackWater.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(16, 0x01 << 6);
            radioButtonJElemAttackHoly.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(16, 0x01 << 7);
            numericUpDownMagicHitCount.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(17, numericUpDownMagicHitCount.Value);
            trackBarJElemAttack.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(18, trackBarJElemAttack.Value);
            checkBoxJElemDefenseFire.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(19, /*KernelWorker.GetSelectedMagicData.ElemDefenseEN ^ 0x01*/ 0x01);
            checkBoxJElemDefenseIce.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(19, 0x02);
            checkBoxJElemDefenseThunder.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(19, 0x04);
            checkBoxJElemDefenseEarth.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(19, 0x08);
            checkBoxJElemDefensePoison.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(19, 0x10);
            checkBoxJElemDefenseWind.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(19, 0x20);
            checkBoxJElemDefenseWater.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(19, 0x40);
            checkBoxJElemDefenseHoly.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(19, 0x80);
            trackBarJElemDefense.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(20, trackBarJElemDefense.Value);
            checkBoxJStatAttackDeath.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(21, 0x0001);
            checkBoxJStatAttackPoison.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(21, 0x0002);
            checkBoxJStatAttackPetrify.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(21, 0x0004);
            checkBoxJStatAttackDarkness.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(21, 0x0008);
            checkBoxJStatAttackSilence.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(21, 0x0010);
            checkBoxJStatAttackBerserk.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(21, 0x0020);
            checkBoxJStatAttackZombie.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(21, 0x0040);
            checkBoxJStatAttackSleep.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(21, 0x0080);
            checkBoxJStatAttackSlow.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(21, 0x0100);
            checkBoxJStatAttackStop.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(21, 0x0200);
            checkBoxJStatAttackConfusion.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(21, 0x0800);
            checkBoxJStatAttackDrain.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(21, 0x1000);
            checkBoxJStatDefenseDeath.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(22, 0x0001);
            checkBoxJStatDefensePoison.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(22, 0x0002);
            checkBoxJStatDefensePetrify.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(22, 0x0004);
            checkBoxJStatDefenseDarnkess.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(22, 0x0008);
            checkBoxJStatDefenseSilence.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(22, 0x0010);
            checkBoxJStatDefenseBerserk.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(22, 0x0020);
            checkBoxJStatDefenseZombie.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(22, 0x0040);
            checkBoxJStatDefenseSleep.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(22, 0x0080);
            checkBoxJStatDefenseSlow.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(22, 0x0100);
            checkBoxJStatDefenseStop.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(22, 0x0200);
            checkBoxJStatDefenseCurse.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(22, 0x0400);
            checkBoxJStatDefenseConfusion.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(22, 0x0800);
            checkBoxJStatDefenseDrain.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(22, 0x1000);
            trackBarJStatAttack.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(23, trackBarJStatAttack.Value);
            trackBarJStatDefense.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(24, trackBarJStatAttack.Value);
            checkBoxMagicSleep.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x01, 0);
            checkBoxMagicHaste.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x02, 0);
            checkBoxMagicSlow.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x04, 0);
            checkBoxMagicStop.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x08, 0);
            checkBoxMagicRegen.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x10, 0);
            checkBoxMagicProtect.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x20, 0);
            checkBoxMagicShell.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x40, 0);
            checkBoxMagicReflect.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x80, 0);
            checkBoxMagicAura.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x01, 1);
            checkBoxMagicCurse.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x02, 1);
            checkBoxMagicDoom.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x04, 1);
            checkBoxMagicInvincible.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x08, 1);
            checkBoxMagicPetrifying.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x10, 1);
            checkBoxMagicFloat.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x20, 1);
            checkBoxMagicConfusion.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x40, 1);
            checkBoxMagicDrain.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x80, 1);
            checkBoxMagicEject.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x01, 2);
            checkBoxMagicDouble.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x02, 2);
            checkBoxMagicTriple.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x04, 2);
            checkBoxMagicDefend.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x08, 2);
            checkBoxMagicVit0.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x01, 3);
            checkBoxMagicDeath.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x01, 4);
            checkBoxMagicPoison.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x02, 4);
            checkBoxMagicPetrify.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x04, 4);
            checkBoxMagicDarkness.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x08, 4);
            checkBoxMagicSilence.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x10, 4);
            checkBoxMagicBerserk.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x20, 4);
            checkBoxMagicZombie.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x40, 4);
            numericUpDownMagicStatusAttackEnabler.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(25, numericUpDownMagicStatusAttackEnabler.Value);
            numericUpDownMagicDefaultTarget.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(42, numericUpDownMagicDefaultTarget.Value);
            numericUpDownMagicQuezacoltComp.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(26, numericUpDownMagicQuezacoltComp.Value);
            numericUpDownMagicShivaComp.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(27, numericUpDownMagicShivaComp.Value);
            numericUpDownMagicIfritComp.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(28, numericUpDownMagicIfritComp.Value);
            numericUpDownMagicSirenComp.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(29, numericUpDownMagicSirenComp.Value);
            numericUpDownMagicBrothersComp.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(30, numericUpDownMagicBrothersComp.Value);
            numericUpDownMagicDiablosComp.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(31, numericUpDownMagicDiablosComp.Value);
            numericUpDownMagicCarbuncleComp.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(32, numericUpDownMagicCarbuncleComp.Value);
            numericUpDownMagicLeviathanComp.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(33, numericUpDownMagicLeviathanComp.Value);
            numericUpDownMagicPandemonaComp.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(34, numericUpDownMagicPandemonaComp.Value);
            numericUpDownMagicCerberusComp.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(35, numericUpDownMagicCerberusComp.Value);
            numericUpDownMagicAlexanderComp.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(36, numericUpDownMagicAlexanderComp.Value);
            numericUpDownMagicDoomtrainComp.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(37, numericUpDownMagicDoomtrainComp.Value);
            numericUpDownMagicBahamutComp.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(38, numericUpDownMagicBahamutComp.Value);
            numericUpDownMagicCactuarComp.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(39, numericUpDownMagicCactuarComp.Value);
            numericUpDownMagicTonberryComp.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(40, numericUpDownMagicTonberryComp.Value);
            numericUpDownMagicEdenComp.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(41, numericUpDownMagicEdenComp.Value);


            //GF
            comboBoxGFMagicID.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(0, comboBoxGFMagicID.SelectedIndex);
            numericUpDownGFPower.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_GF(1, numericUpDownGFPower.Value);
            comboBoxGFElement.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(2, GF_GetElement(comboBoxGFElement.SelectedIndex));
            numericUpDownGFHP.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_GF(3, numericUpDownGFHP.Value);
            numericUpDownGFPowerMod.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_GF(4, numericUpDownGFPowerMod.Value);
            numericUpDownGFLevelMod.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_GF(5, numericUpDownGFLevelMod.Value);
            comboBoxGFAbility1.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(6, comboBoxGFAbility1.SelectedIndex, 0);
            comboBoxGFAbility2.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(6, comboBoxGFAbility2.SelectedIndex, 1);
            comboBoxGFAbility3.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(6, comboBoxGFAbility3.SelectedIndex, 2);
            comboBoxGFAbility4.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(6, comboBoxGFAbility4.SelectedIndex, 3);
            comboBoxGFAbility5.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(6, comboBoxGFAbility5.SelectedIndex, 4);
            comboBoxGFAbility6.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(6, comboBoxGFAbility6.SelectedIndex, 5);
            comboBoxGFAbility7.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(6, comboBoxGFAbility7.SelectedIndex, 6);
            comboBoxGFAbility8.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(6, comboBoxGFAbility8.SelectedIndex, 7);
            comboBoxGFAbility9.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(6, comboBoxGFAbility9.SelectedIndex, 8);
            comboBoxGFAbility10.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(6, comboBoxGFAbility10.SelectedIndex, 9);
            comboBoxGFAbility11.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(6, comboBoxGFAbility11.SelectedIndex, 10);
            comboBoxGFAbility12.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(6, comboBoxGFAbility12.SelectedIndex, 11);
            comboBoxGFAbility13.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(6, comboBoxGFAbility13.SelectedIndex, 12);
            comboBoxGFAbility14.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(6, comboBoxGFAbility14.SelectedIndex, 13);
            comboBoxGFAbility15.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(6, comboBoxGFAbility15.SelectedIndex, 14);
            comboBoxGFAbility16.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(6, comboBoxGFAbility16.SelectedIndex, 15);
            comboBoxGFAbility17.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(6, comboBoxGFAbility17.SelectedIndex, 16);
            comboBoxGFAbility18.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(6, comboBoxGFAbility18.SelectedIndex, 17);
            comboBoxGFAbility19.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(6, comboBoxGFAbility19.SelectedIndex, 18);
            comboBoxGFAbility20.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(6, comboBoxGFAbility20.SelectedIndex, 19);
            comboBoxGFAbility21.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(6, comboBoxGFAbility21.SelectedIndex, 20);
            checkBoxGFSleep.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x01,0,1);
            checkBoxGFHaste.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x02,0,1);
            checkBoxGFSlow.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x04,0,1);
            checkBoxGFStop.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x08,0,1);
            checkBoxGFRegen.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x10,0,1);
            checkBoxGFProtect.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x20,0,1);
            checkBoxGFShell.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x40,0, 1);
            checkBoxGFReflect.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x80,0, 1);
            checkBoxGFAura.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x01, 0,2);
            checkBoxGFCurse.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x02, 0,2);
            checkBoxGFDoom.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x04, 0,2);
            checkBoxGFInvincible.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x08,0 ,2);
            checkBoxGFPetrifying.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x10,0 ,2);
            checkBoxGFFloat.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x20,0 ,2);
            checkBoxGFConfusion.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(6, 0x40,0 ,2);
            checkBoxGFDrain.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x80, 0,2);
            checkBoxGFEject.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x01,0 ,3);
            checkBoxGFDouble.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x02,0 ,3);
            checkBoxGFTriple.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x04, 0,3);
            checkBoxGFDefend.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x08, 0,3);
            checkBoxGFVit0.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x01, 0,4);
            checkBoxGFDeath.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x01,0 ,0);
            checkBoxGFPoison.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x02,0 ,0);
            checkBoxGFPetrify.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x04,0 ,0);
            checkBoxGFDarkness.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x08,0 ,0);
            checkBoxGFSilence.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x10, 0,0);
            checkBoxGFBerserk.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x20, 0,0);
            checkBoxGFZombie.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x40, 0, 0);
            numericUpDownGFStatusAttackEnabler.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_GF(8, numericUpDownGFStatusAttackEnabler.Value);
            numericUpDownGFQuezacoltComp.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_GF(10, numericUpDownGFQuezacoltComp.Value);
            numericUpDownGFShivaComp.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_GF(11, numericUpDownGFShivaComp.Value);
            numericUpDownGFIfritComp.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_GF(12, numericUpDownGFIfritComp.Value);
            numericUpDownGFSirenComp.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_GF(13, numericUpDownGFSirenComp.Value);
            numericUpDownGFBrothersComp.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_GF(14, numericUpDownGFBrothersComp.Value);
            numericUpDownGFDiablosComp.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_GF(15, numericUpDownGFDiablosComp.Value);
            numericUpDownGFCarbuncleComp.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_GF(16, numericUpDownGFCarbuncleComp.Value);
            numericUpDownGFLeviathanComp.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_GF(17, numericUpDownGFLeviathanComp.Value);
            numericUpDownGFPandemonaComp.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_GF(18, numericUpDownGFPandemonaComp.Value);
            numericUpDownGFCerberusComp.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_GF(19, numericUpDownGFCerberusComp.Value);
            numericUpDownGFAlexanderComp.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_GF(20, numericUpDownGFAlexanderComp.Value);
            numericUpDownGFDoomtrainComp.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_GF(21, numericUpDownGFDoomtrainComp.Value);
            numericUpDownGFBahamutComp.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_GF(22, numericUpDownGFBahamutComp.Value);
            numericUpDownGFCactuarComp.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_GF(23, numericUpDownGFCactuarComp.Value);
            numericUpDownGFTonberryComp.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_GF(24, numericUpDownGFTonberryComp.Value);
            numericUpDownGFEdenComp.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_GF(25, numericUpDownGFEdenComp.Value);

            //Non-Junctionable GFs Attacks
            comboBoxGFAttacksMagicID.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(0, comboBoxGFAttacksMagicID.SelectedIndex);
            numericUpDownGFAttacksPower.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(1, numericUpDownGFAttacksPower.Value);
            numericUpDownGFAttacksStatusAttackEnabler.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(2, numericUpDownGFAttacksStatusAttackEnabler.Value);
            comboBoxGFAttacksElement.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(3, GFAttacks_GetElement(comboBoxGFAttacksElement.SelectedIndex));
            checkBoxGFAttacksSleep.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(4, 0x01, 0);
            checkBoxGFAttacksHaste.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(4, 0x02, 0);
            checkBoxGFAttacksSlow.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(4, 0x04, 0);
            checkBoxGFAttacksStop.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(4, 0x08, 0);
            checkBoxGFAttacksRegen.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(4, 0x10, 0);
            checkBoxGFAttacksProtect.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(4, 0x20, 0);
            checkBoxGFAttacksShell.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(4, 0x40, 0);
            checkBoxGFAttacksReflect.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(4, 0x80, 0);
            checkBoxGFAttacksAura.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(4, 0x01, 1);
            checkBoxGFAttacksCurse.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(4, 0x02, 1);
            checkBoxGFAttacksDoom.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(4, 0x04, 1);
            checkBoxGFAttacksInvincible.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(4, 0x08, 1);
            checkBoxGFAttacksPetrifying.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(4, 0x10, 1);
            checkBoxGFAttacksFloat.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(4, 0x20, 1);
            checkBoxGFAttacksConfusion.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(4, 0x40, 1);
            checkBoxGFAttacksDrain.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(4, 0x80, 1);
            checkBoxGFAttacksEject.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(4, 0x01, 2);
            checkBoxGFAttacksDouble.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(4, 0x02, 2);
            checkBoxGFAttacksTriple.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(4, 0x04, 2);
            checkBoxGFAttacksDefend.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(4, 0x08, 2);
            checkBoxGFAttacksVit0.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(4, 0x01, 3);
            checkBoxGFAttacksDeath.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(4, 0x01, 4);
            checkBoxGFAttacksPoison.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(4, 0x02, 4);
            checkBoxGFAttacksPetrify.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(4, 0x04, 4);
            checkBoxGFAttacksDarkness.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(4, 0x08, 4);
            checkBoxGFAttacksSilence.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(4, 0x10, 4);
            checkBoxGFAttacksBerserk.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(4, 0x20, 4);
            checkBoxGFAttacksZombie.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(4, 0x40, 4);
            numericUpDownGFAttacksPowerMod.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(5, numericUpDownGFAttacksPowerMod.Value);
            numericUpDownGFAttacksLevelMod.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(6, numericUpDownGFAttacksLevelMod.Value);            
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




        // MAGIC TRACKBARS LABELS VALUE
        //I added this.trackBar.ValueChanged += (this.trackBarJXY_Scroll) to MainForm.Designer, only this 4 are necessary here now
        private void trackBarJElemAttack_Scroll(object sender, EventArgs e)
        {
            labelValueElemAttackTrackBar.Text = trackBarJElemAttack.Value + "%".ToString();
        }

        private void trackBarJElemDefense_Scroll(object sender, EventArgs e)
        {
            labelValueElemDefenseTrackBar.Text = trackBarJElemDefense.Value + "%".ToString();
        }

        private void trackBarJStatAttack_Scroll(object sender, EventArgs e)
        {
            labelValueStatAttackTrackBar.Text = trackBarJStatAttack.Value + "%".ToString();
        }

        private void trackBarJStatDefense_Scroll(object sender, EventArgs e)
        {
            labelValueStatDefenseTrackBar.Text = trackBarJStatDefense.Value + "%".ToString();
        }



        //ABILITIES TRACKBARS LABELS VALUE
        private void trackBarStatsIncrementValue_Scroll(object sender, EventArgs e)
        {
            labelStatsIncrementValueTrackBar.Text = trackBarStatsIncrementValue.Value + "%".ToString();
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



        //MAGIC
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
                                                        ? comboBoxMagicElement.Items.Count - 1
                                                        : 0;
        }

        private byte Magic_GetElement(int Index)
        {
            byte elem = (byte)(Index == 8 ? (byte)KernelWorker.Element.NonElemental :
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
            //checkBoxMagicSleep.Checked =  ? true : false
            checkBoxMagicSleep.Checked = (KernelWorker.GetSelectedMagicData.StatusMagic1 & 0x01) >= 1 ? true : false;
            checkBoxMagicHaste.Checked = (KernelWorker.GetSelectedMagicData.StatusMagic1 & 0x02) >= 1 ? true : false;
            checkBoxMagicSlow.Checked = (KernelWorker.GetSelectedMagicData.StatusMagic1 & 0x04) >= 1 ? true : false;
            checkBoxMagicStop.Checked = (KernelWorker.GetSelectedMagicData.StatusMagic1 & 0x08) >= 1 ? true : false;
            checkBoxMagicRegen.Checked = (KernelWorker.GetSelectedMagicData.StatusMagic1 & 0x10) >= 1 ? true : false;
            checkBoxMagicProtect.Checked = (KernelWorker.GetSelectedMagicData.StatusMagic1 & 0x20) >= 1 ? true : false;
            checkBoxMagicShell.Checked = (KernelWorker.GetSelectedMagicData.StatusMagic1 & 0x40) >= 1 ? true : false;
            checkBoxMagicReflect.Checked = (KernelWorker.GetSelectedMagicData.StatusMagic1 & 0x80) >= 1 ? true : false;

            checkBoxMagicAura.Checked = (KernelWorker.GetSelectedMagicData.StatusMagic2 & 0x01) >= 1 ? true : false;
            checkBoxMagicCurse.Checked = (KernelWorker.GetSelectedMagicData.StatusMagic2 & 0x02) >= 1 ? true : false;
            checkBoxMagicDoom.Checked = (KernelWorker.GetSelectedMagicData.StatusMagic2 & 0x04) >= 1 ? true : false;
            checkBoxMagicInvincible.Checked = (KernelWorker.GetSelectedMagicData.StatusMagic2 & 0x08) >= 1 ? true : false;
            checkBoxMagicPetrifying.Checked = (KernelWorker.GetSelectedMagicData.StatusMagic2 & 0x10) >= 1 ? true : false;
            checkBoxMagicFloat.Checked = (KernelWorker.GetSelectedMagicData.StatusMagic2 & 0x20) >= 1 ? true : false;
            checkBoxMagicConfusion.Checked = (KernelWorker.GetSelectedMagicData.StatusMagic2 & 0x40) >= 1 ? true : false;
            checkBoxMagicDrain.Checked = (KernelWorker.GetSelectedMagicData.StatusMagic2 & 0x80) >= 1 ? true : false;

            checkBoxMagicEject.Checked = (KernelWorker.GetSelectedMagicData.StatusMagic3 & 0x01) >= 1 ? true : false;
            checkBoxMagicDouble.Checked = (KernelWorker.GetSelectedMagicData.StatusMagic3 & 0x02) >= 1 ? true : false;
            checkBoxMagicTriple.Checked = (KernelWorker.GetSelectedMagicData.StatusMagic3 & 0x04) >= 1 ? true : false;
            checkBoxMagicDefend.Checked = (KernelWorker.GetSelectedMagicData.StatusMagic3 & 0x08) >= 1 ? true : false;

            checkBoxMagicVit0.Checked = (KernelWorker.GetSelectedMagicData.StatusMagic4 & 0x01) >= 1 ? true : false;

            checkBoxMagicDeath.Checked = (KernelWorker.GetSelectedMagicData.StatusMagic5 & 0x01) >= 1 ? true : false;
            checkBoxMagicPoison.Checked = (KernelWorker.GetSelectedMagicData.StatusMagic5 & 0x02) >= 1 ? true : false;
            checkBoxMagicPetrify.Checked = (KernelWorker.GetSelectedMagicData.StatusMagic5 & 0x04) >= 1 ? true : false;
            checkBoxMagicDarkness.Checked = (KernelWorker.GetSelectedMagicData.StatusMagic5 & 0x08) >= 1 ? true : false;
            checkBoxMagicSilence.Checked = (KernelWorker.GetSelectedMagicData.StatusMagic5 & 0x10) >= 1 ? true : false;
            checkBoxMagicBerserk.Checked = (KernelWorker.GetSelectedMagicData.StatusMagic5 & 0x20) >= 1 ? true : false;
            checkBoxMagicZombie.Checked = (KernelWorker.GetSelectedMagicData.StatusMagic5 & 0x40) >= 1 ? true : false;
        }


        private void listBoxMagic_SelectedIndexChanged(object sender, EventArgs e)
        {
            _loaded = false;
            if (KernelWorker.Kernel == null)
                return;
            KernelWorker.ReadMagic(listBoxMagic.SelectedIndex);
            try
            {
                comboBoxMagicMagicID.SelectedIndex = KernelWorker.GetSelectedMagicData.MagicID-1; //As in Vanilla FF8.exe: sub ESI, 1
                numericUpDownMagicSpellPower.Value = KernelWorker.GetSelectedMagicData.SpellPower;
                numericUpDownMagicDefaultTarget.Value = KernelWorker.GetSelectedMagicData.DefaultTarget;
                numericUpDownMagicDrawResist.Value = KernelWorker.GetSelectedMagicData.DrawResist;
                numericUpDownMagicHitCount.Value = KernelWorker.GetSelectedMagicData.HitCount;
                comboBoxMagicElement.SelectedIndex = Magic_GetElement();
                MagicStatusWorker();
                numericUpDownMagicHPJ.Value = KernelWorker.GetSelectedMagicData.HP;
                numericUpDownMagicVITJ.Value = KernelWorker.GetSelectedMagicData.VIT;
                numericUpDownMagicSPRJ.Value = KernelWorker.GetSelectedMagicData.SPR;
                numericUpDownMagicSTRJ.Value = KernelWorker.GetSelectedMagicData.STR;
                numericUpDownMagicMAGJ.Value = KernelWorker.GetSelectedMagicData.MAG;
                numericUpDownMagicSPDJ.Value = KernelWorker.GetSelectedMagicData.SPD;
                numericUpDownMagicEVAJ.Value = KernelWorker.GetSelectedMagicData.EVA;
                numericUpDownMagicHITJ.Value = KernelWorker.GetSelectedMagicData.HIT;
                numericUpDownMagicLUCKJ.Value = KernelWorker.GetSelectedMagicData.LUCK;
                StatusHoldWorker(0,KernelWorker.GetSelectedMagicData.ElemAttackEN);
                trackBarJElemAttack.Value = KernelWorker.GetSelectedMagicData.ElemAttackVAL;
                StatusHoldWorker(1, KernelWorker.GetSelectedMagicData.ElemDefenseEN);
                trackBarJElemDefense.Value = KernelWorker.GetSelectedMagicData.ElemDefenseVAL;
                StatusHoldWorker(2,KernelWorker.GetSelectedMagicData.StatusMagic1, KernelWorker.GetSelectedMagicData.StatusATKEN, KernelWorker.GetSelectedMagicData.StatusMagic2, KernelWorker.GetSelectedMagicData.StatusMagic3, KernelWorker.GetSelectedMagicData.StatusMagic4, KernelWorker.GetSelectedMagicData.StatusMagic5);
                trackBarJStatAttack.Value = KernelWorker.GetSelectedMagicData.StatusATKval;
                StatusHoldWorker(3, KernelWorker.GetSelectedMagicData.StatusMagic1,KernelWorker.GetSelectedMagicData.StatusDefEN , KernelWorker.GetSelectedMagicData.StatusMagic2, KernelWorker.GetSelectedMagicData.StatusMagic3, KernelWorker.GetSelectedMagicData.StatusMagic4, KernelWorker.GetSelectedMagicData.StatusMagic5);
                trackBarJStatDefense.Value = KernelWorker.GetSelectedMagicData.StatusDEFval;
                numericUpDownMagicStatusAttackEnabler.Value = KernelWorker.GetSelectedMagicData.StatusAttackEnabler;
                numericUpDownMagicQuezacoltComp.Value = KernelWorker.GetSelectedMagicData.QuezacoltCompatibility;
                numericUpDownMagicShivaComp.Value = KernelWorker.GetSelectedMagicData.ShivaCompatibility;
                numericUpDownMagicIfritComp.Value = KernelWorker.GetSelectedMagicData.IfritCompatibility;
                numericUpDownMagicSirenComp.Value = KernelWorker.GetSelectedMagicData.SirenCompatibility;
                numericUpDownMagicBrothersComp.Value = KernelWorker.GetSelectedMagicData.BrothersCompatibility;
                numericUpDownMagicDiablosComp.Value = KernelWorker.GetSelectedMagicData.DiablosCompatibility;
                numericUpDownMagicCarbuncleComp.Value = KernelWorker.GetSelectedMagicData.CarbuncleCompatibility;
                numericUpDownMagicLeviathanComp.Value = KernelWorker.GetSelectedMagicData.LeviathanCompatibility;
                numericUpDownMagicPandemonaComp.Value = KernelWorker.GetSelectedMagicData.PandemonaCompatibility;
                numericUpDownMagicCerberusComp.Value = KernelWorker.GetSelectedMagicData.CerberusCompatibility;
                numericUpDownMagicAlexanderComp.Value = KernelWorker.GetSelectedMagicData.AlexanderCompatibility;
                numericUpDownMagicDoomtrainComp.Value = KernelWorker.GetSelectedMagicData.DoomtrainCompatibility;
                numericUpDownMagicBahamutComp.Value = KernelWorker.GetSelectedMagicData.BahamutCompatibility;
                numericUpDownMagicCactuarComp.Value = KernelWorker.GetSelectedMagicData.CactuarCompatibility;
                numericUpDownMagicTonberryComp.Value = KernelWorker.GetSelectedMagicData.TonberryCompatibility;
                numericUpDownMagicEdenComp.Value = KernelWorker.GetSelectedMagicData.EdenCompatibility;

            }
            catch (Exception e_)
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
                        radioButtonJElemAttackFire.Checked = true;
                        return;
                    case 0x02:
                        radioButtonJElemAttackIce.Checked = true;
                        return;
                    case 0x04:
                        radioButtonJElemAttackThunder.Checked = true;
                        return;
                    case 0x08:
                        radioButtonJElemAttackEarth.Checked = true;
                        return;
                    case 0x10:
                        radioButtonJElemAttackPoison.Checked = true;
                        return;
                    case 0x20:
                        radioButtonJElemAttackWind.Checked = true;
                        return;
                    case 0x40:
                        radioButtonJElemAttackWater.Checked = true;
                        return;
                    case 0x80:
                        radioButtonJElemAttackHoly.Checked = true;
                        return;
                    default:
                        //radioButtonJElemAttackFire.Checked = true; //A trick to not loop every control
                        //radioButtonJElemAttackFire.Checked = false;
                        radioButtonJElemAttackNElem.Checked = true;
                        return;
                }
            }
            if (State == 1)
            {
                ResetUI(1);
                if((Element & 0x01) > 0) //If Element AND 01 is bigger than 0 - Classic bitwise logic operation. :)
                {//Extreme better/faster than checking BitArray or making all possible cases (255 cases!)
                    checkBoxJElemDefenseFire.Checked = true;
                }
                if ((Element & 0x02) > 0)
                {
                    checkBoxJElemDefenseIce.Checked = true;
                }
                if ((Element & 0x04) > 0)
                {
                    checkBoxJElemDefenseThunder.Checked = true;
                }
                if ((Element & 0x08) > 0)
                {
                    checkBoxJElemDefenseEarth.Checked = true;
                }
                if ((Element & 0x10) > 0)
                {
                    checkBoxJElemDefensePoison.Checked = true;
                }
                if ((Element & 0x20) > 0)
                {
                    checkBoxJElemDefenseWind.Checked = true;
                }
                if ((Element & 0x40) > 0)
                {
                    checkBoxJElemDefenseWater.Checked = true;
                }
                if ((Element & 0x80) > 0)
                {
                    checkBoxJElemDefenseHoly.Checked = true;
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
                    checkBoxJStatAttackDeath.Checked = true; //DEATH
                if ((Stat & 0x0002) > 0)
                    checkBoxJStatAttackPoison.Checked = true; //POISON
                if ((Stat & 0x0004) > 0)
                    checkBoxJStatAttackPetrify.Checked = true; //PETRIFY
                if ((Stat & 0x0008) > 0)
                    checkBoxJStatAttackDarkness.Checked = true; //DARKNESS
                if ((Stat & 0x0010) > 0)
                    checkBoxJStatAttackSilence.Checked = true; //SILENCE
                if ((Stat & 0x0020) > 0)
                    checkBoxJStatAttackBerserk.Checked = true; //BERSERK
                if ((Stat & 0x0040) > 0)
                    checkBoxJStatAttackZombie.Checked = true; //ZOMBIE
                if ((Stat & 0x0080) > 0)
                    checkBoxJStatAttackSleep.Checked = true; //SLEEP
                if ((Stat & 0x0100) > 0)
                    checkBoxJStatAttackSlow.Checked = true; //SLOW
                if ((Stat & 0x0200) > 0)
                    checkBoxJStatAttackStop.Checked = true; //STOP
                if ((Stat & 0x0800) > 0)
                    checkBoxJStatAttackConfusion.Checked = true; //CONFUSE
                if ((Stat & 0x1000) > 0)
                    checkBoxJStatAttackDrain.Checked = true; //DRAIN

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
                    checkBoxJStatDefenseDeath.Checked = true; //DEATH
                if ((Stat & 0x02) > 0)
                    checkBoxJStatDefensePoison.Checked = true; //POISON
                if ((Stat & 0x04) > 0)
                    checkBoxJStatDefensePetrify.Checked = true; //PETRIFY
                if ((Stat & 0x08) > 0)
                    checkBoxJStatDefenseDarnkess.Checked = true; //DARKNESS
                if ((Stat & 0x10) > 0)
                    checkBoxJStatDefenseSilence.Checked = true; //SILENCE
                if ((Stat & 0x20) > 0)
                    checkBoxJStatDefenseBerserk.Checked = true; //BERSERK
                if ((Stat & 0x40) > 0)
                    checkBoxJStatDefenseZombie.Checked = true; //ZOMBIE
                if ((Stat & 0x80) > 0)
                    checkBoxJStatDefenseSleep.Checked = true; //SLEEP
                if ((Stat & 0x100) > 0)
                    checkBoxJStatDefenseSlow.Checked = true; //SLOW
                if ((Stat & 0x0200) > 0)
                    checkBoxJStatDefenseStop.Checked = true; //STOP
                if ((Stat & 0x0400 ) > 0)
                    checkBoxJStatDefenseCurse.Checked = true; //PAIN
                if ((Stat & 0x0800) > 0)
                    checkBoxJStatDefenseConfusion.Checked = true; //CONFUSE
                if ((Stat & 0x1000) > 0)
                    checkBoxJStatDefenseDrain.Checked = true; //DRAIN

                /*
                if ((Stat & 0x0080 << 7) > 0)
                    Console.WriteLine("UNKNOWN! 7");
                if ((Stat & 0x0080 << 8) > 0)
                    Console.WriteLine("UNKNOWN! 8"); */


                if (Stat == 0)
                    ResetUI(3);
            }
        }




        //RESET UI
        private void ResetUI(byte State)
        {
            if(State==1)
            {
                checkBoxJElemDefenseIce.Checked = false;
                checkBoxJElemDefenseEarth.Checked = false;
                checkBoxJElemDefensePoison.Checked = false;
                checkBoxJElemDefenseWind.Checked = false;
                checkBoxJElemDefenseWater.Checked = false;
                checkBoxJElemDefenseHoly.Checked = false;
                checkBoxJElemDefenseFire.Checked = false;
                checkBoxJElemDefenseThunder.Checked = false;
            }
            if(State==2)
            {
                checkBoxJStatAttackBerserk.Checked = false;
                checkBoxJStatAttackConfusion.Checked = false;
                checkBoxJStatAttackDarkness.Checked = false;
                checkBoxJStatAttackDeath.Checked = false;
                checkBoxJStatAttackDrain.Checked = false;
                checkBoxJStatAttackPetrify.Checked = false;
                checkBoxJStatAttackPoison.Checked = false;
                checkBoxJStatAttackSilence.Checked = false;
                checkBoxJStatAttackSleep.Checked = false;
                checkBoxJStatAttackSlow.Checked = false;
                checkBoxJStatAttackStop.Checked = false;
                checkBoxJStatAttackZombie.Checked = false;

            }
            if(State==3)
            {
                checkBoxJStatDefenseBerserk.Checked = false;
                checkBoxJStatDefenseConfusion.Checked = false;
                checkBoxJStatDefenseDarnkess.Checked = false;
                checkBoxJStatDefenseDeath.Checked = false;
                checkBoxJStatDefenseDrain.Checked = false;
                checkBoxJStatDefensePetrify.Checked = false;
                checkBoxJStatDefensePoison.Checked = false;
                checkBoxJStatDefenseSilence.Checked = false;
                checkBoxJStatDefenseSleep.Checked = false;
                checkBoxJStatDefenseSlow.Checked = false;
                checkBoxJStatDefenseStop.Checked = false;
                checkBoxJStatDefenseZombie.Checked = false;
                checkBoxJStatDefenseCurse.Checked = false;
                checkBoxJStatDefenseDarnkess.Checked = false;
            }
        }



        //GFs
        /*private void checkBoxGFStatus_Checked(object sender, EventArgs e)
        {
            if (checkBoxGFStatus.Checked == true)
            {
                checkBoxGFSleep.Enabled = true;
                checkBoxGFHaste.Enabled = true;
                checkBoxGFSlow.Enabled = true;
                checkBoxGFStop.Enabled = true;
                checkBoxGFRegen.Enabled = true;
                checkBoxGFProtect.Enabled = true;
                checkBoxGFShell.Enabled = true;
                checkBoxGFReflect.Enabled = true;
                checkBoxGFAura.Enabled = true;
                checkBoxGFCurse.Enabled = true;
                checkBoxGFDoom.Enabled = true;
                checkBoxGFInvincible.Enabled = true;
                checkBoxGFPetrifying.Enabled = true;
                checkBoxGFFloat.Enabled = true;
                checkBoxGFConfusion.Enabled = true;
                checkBoxGFDrain.Enabled = true;
                checkBoxGFEject.Enabled = true;
                checkBoxGFDouble.Enabled = true;
                checkBoxGFTriple.Enabled = true;
                checkBoxGFDefend.Enabled = true;
                checkBoxGFVit0.Enabled = true;
                checkBoxGFDeath.Enabled = true;
                checkBoxGFPoison.Enabled = true;
                checkBoxGFPetrify.Enabled = true;
                checkBoxGFDarkness.Enabled = true;
                checkBoxGFSilence.Enabled = true;
                checkBoxGFBerserk.Enabled = true;
                checkBoxGFZombie.Enabled = true;
            }

            else if (checkBoxGFStatus.Checked == false)
            {
                checkBoxGFSleep.Checked = false;
                checkBoxGFHaste.Checked = false;
                checkBoxGFSlow.Checked = false;
                checkBoxGFStop.Checked = false;
                checkBoxGFRegen.Checked = false;
                checkBoxGFProtect.Checked = false;
                checkBoxGFShell.Checked = false;
                checkBoxGFReflect.Checked = false;
                checkBoxGFAura.Checked = false;
                checkBoxGFCurse.Checked = false;
                checkBoxGFDoom.Checked = false;
                checkBoxGFInvincible.Checked = false;
                checkBoxGFPetrifying.Checked = false;
                checkBoxGFFloat.Checked = false;
                checkBoxGFConfusion.Checked = false;
                checkBoxGFDrain.Checked = false;
                checkBoxGFEject.Checked = false;
                checkBoxGFDouble.Checked = false;
                checkBoxGFTriple.Checked = false;
                checkBoxGFDefend.Checked = false;
                checkBoxGFVit0.Checked = false;
                checkBoxGFDeath.Checked = false;
                checkBoxGFPoison.Checked = false;
                checkBoxGFPetrify.Checked = false;
                checkBoxGFDarkness.Checked = false;
                checkBoxGFSilence.Checked = false;
                checkBoxGFBerserk.Checked = false;
                checkBoxGFZombie.Checked = false;
                //when unchecking the enabler the following also unchecks all the statuses
                checkBoxGFSleep.Enabled = false;
                checkBoxGFHaste.Enabled = false;
                checkBoxGFSlow.Enabled = false;
                checkBoxGFStop.Enabled = false;
                checkBoxGFRegen.Enabled = false;
                checkBoxGFProtect.Enabled = false;
                checkBoxGFShell.Enabled = false;
                checkBoxGFReflect.Enabled = false;
                checkBoxGFAura.Enabled = false;
                checkBoxGFCurse.Enabled = false;
                checkBoxGFDoom.Enabled = false;
                checkBoxGFInvincible.Enabled = false;
                checkBoxGFPetrifying.Enabled = false;
                checkBoxGFFloat.Enabled = false;
                checkBoxGFConfusion.Enabled = false;
                checkBoxGFDrain.Enabled = false;
                checkBoxGFEject.Enabled = false;
                checkBoxGFDouble.Enabled = false;
                checkBoxGFTriple.Enabled = false;
                checkBoxGFDefend.Enabled = false;
                checkBoxGFVit0.Enabled = false;
                checkBoxGFDeath.Enabled = false;
                checkBoxGFPoison.Enabled = false;
                checkBoxGFPetrify.Enabled = false;
                checkBoxGFDarkness.Enabled = false;
                checkBoxGFSilence.Enabled = false;
                checkBoxGFBerserk.Enabled = false;
                checkBoxGFZombie.Enabled = false;
                
                KernelWorker.UpdateVariable_GF(9, 0);
            }
            KernelWorker.UpdateVariable_GF(8, checkBoxGFStatus.Checked ? 0xFF : 0x00); 
        }*/
        //opted out for now

        

        private int GF_GetElement()
        {
            return KernelWorker.GetSelectedGFData.ElementGF == KernelWorker.Element.Fire
                        ? 0
                        : KernelWorker.GetSelectedGFData.ElementGF == KernelWorker.Element.Ice
                            ? 1
                            : KernelWorker.GetSelectedGFData.ElementGF == KernelWorker.Element.Thunder
                                ? 2
                                : KernelWorker.GetSelectedGFData.ElementGF == KernelWorker.Element.Earth
                                    ? 3
                                    : KernelWorker.GetSelectedGFData.ElementGF == KernelWorker.Element.Poison
                                        ? 4
                                        : KernelWorker.GetSelectedGFData.ElementGF == KernelWorker.Element.Wind
                                            ? 5
                                            : KernelWorker.GetSelectedGFData.ElementGF ==
                                              KernelWorker.Element.Water
                                                ? 6
                                                : KernelWorker.GetSelectedGFData.ElementGF ==
                                                  KernelWorker.Element.Holy
                                                    ? 7
                                                    : KernelWorker.GetSelectedGFData.ElementGF ==
                                                      KernelWorker.Element.NonElemental
                                                        ? comboBoxGFElement.Items.Count - 1
                                                        : 0;
        }

        private byte GF_GetElement(int Index)
        {
            byte elem = (byte)(Index == 8 ? (byte)KernelWorker.Element.NonElemental :
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

        private void GFStatusWorker()
        {
            //checkBoxMagicSleep.Checked =  ? true : false
            checkBoxGFSleep.Checked = (KernelWorker.GetSelectedGFData.StatusGF2 & 0x01) >= 1 ? true : false;
            checkBoxGFHaste.Checked = (KernelWorker.GetSelectedGFData.StatusGF2 & 0x02) >= 1 ? true : false;
            checkBoxGFSlow.Checked = (KernelWorker.GetSelectedGFData.StatusGF2 & 0x04) >= 1 ? true : false;
            checkBoxGFStop.Checked = (KernelWorker.GetSelectedGFData.StatusGF2 & 0x08) >= 1 ? true : false;
            checkBoxGFRegen.Checked = (KernelWorker.GetSelectedGFData.StatusGF2 & 0x10) >= 1 ? true : false;
            checkBoxGFProtect.Checked = (KernelWorker.GetSelectedGFData.StatusGF2 & 0x20) >= 1 ? true : false;
            checkBoxGFShell.Checked = (KernelWorker.GetSelectedGFData.StatusGF2 & 0x40) >= 1 ? true : false;
            checkBoxGFReflect.Checked = (KernelWorker.GetSelectedGFData.StatusGF2 & 0x80) >= 1 ? true : false;
                  
            checkBoxGFAura.Checked = (KernelWorker.GetSelectedGFData.StatusGF3 & 0x01) >= 1 ? true : false;
            checkBoxGFCurse.Checked = (KernelWorker.GetSelectedGFData.StatusGF3 & 0x02) >= 1 ? true : false;
            checkBoxGFDoom.Checked = (KernelWorker.GetSelectedGFData.StatusGF3 & 0x04) >= 1 ? true : false;
            checkBoxGFInvincible.Checked = (KernelWorker.GetSelectedGFData.StatusGF3 & 0x08) >= 1 ? true : false;
            checkBoxGFPetrifying.Checked = (KernelWorker.GetSelectedGFData.StatusGF3 & 0x10) >= 1 ? true : false;
            checkBoxGFFloat.Checked = (KernelWorker.GetSelectedGFData.StatusGF3 & 0x20) >= 1 ? true : false;
            checkBoxGFConfusion.Checked = (KernelWorker.GetSelectedGFData.StatusGF3 & 0x40) >= 1 ? true : false;
            checkBoxGFDrain.Checked = (KernelWorker.GetSelectedGFData.StatusGF3 & 0x80) >= 1 ? true : false;
                   
            checkBoxGFEject.Checked = (KernelWorker.GetSelectedGFData.StatusGF4 & 0x01) >= 1 ? true : false;
            checkBoxGFDouble.Checked = (KernelWorker.GetSelectedGFData.StatusGF4 & 0x02) >= 1 ? true : false;
            checkBoxGFTriple.Checked = (KernelWorker.GetSelectedGFData.StatusGF4 & 0x04) >= 1 ? true : false;
            checkBoxGFDefend.Checked = (KernelWorker.GetSelectedGFData.StatusGF4 & 0x08) >= 1 ? true : false;
                    
            checkBoxGFVit0.Checked = (KernelWorker.GetSelectedGFData.StatusGF5 & 0x01) >= 1 ? true : false;
                   
            checkBoxGFDeath.Checked = (KernelWorker.GetSelectedGFData.StatusGF1 & 0x01) >= 1 ? true : false;
            checkBoxGFPoison.Checked = (KernelWorker.GetSelectedGFData.StatusGF1 & 0x02) >= 1 ? true : false;
            checkBoxGFPetrify.Checked = (KernelWorker.GetSelectedGFData.StatusGF1 & 0x04) >= 1 ? true : false;
            checkBoxGFDarkness.Checked = (KernelWorker.GetSelectedGFData.StatusGF1 & 0x08) >= 1 ? true : false;
            checkBoxGFSilence.Checked = (KernelWorker.GetSelectedGFData.StatusGF1 & 0x10) >= 1 ? true : false;
            checkBoxGFBerserk.Checked = (KernelWorker.GetSelectedGFData.StatusGF1 & 0x20) >= 1 ? true : false;
            checkBoxGFZombie.Checked = (KernelWorker.GetSelectedGFData.StatusGF1 & 0x40) >= 1 ? true : false;
        }


        private void listBoxGF_SelectedIndexChanged(object sender, EventArgs e)
        {
            _loaded = false;
            if (KernelWorker.Kernel == null)
                return;
            KernelWorker.ReadGF(listBoxGF.SelectedIndex);

            try
            {
                comboBoxGFMagicID.SelectedIndex = KernelWorker.GetSelectedGFData.GFMagicID;
                numericUpDownGFPower.Value = KernelWorker.GetSelectedGFData.GFPower;
                numericUpDownGFHP.Value = KernelWorker.GetSelectedGFData.GFHP;
                numericUpDownGFPowerMod.Value = KernelWorker.GetSelectedGFData.GFPowerMod;
                numericUpDownGFLevelMod.Value = KernelWorker.GetSelectedGFData.GFLevelMod;
                comboBoxGFAbility1.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility1;
                comboBoxGFAbility2.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility2;
                comboBoxGFAbility3.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility3;
                comboBoxGFAbility4.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility4;
                comboBoxGFAbility5.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility5;
                comboBoxGFAbility6.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility6;
                comboBoxGFAbility7.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility7;
                comboBoxGFAbility8.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility8;
                comboBoxGFAbility9.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility9;
                comboBoxGFAbility10.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility10;
                comboBoxGFAbility11.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility11;
                comboBoxGFAbility12.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility12;
                comboBoxGFAbility13.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility13;
                comboBoxGFAbility14.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility14;
                comboBoxGFAbility15.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility15;
                comboBoxGFAbility16.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility16;
                comboBoxGFAbility17.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility17;
                comboBoxGFAbility18.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility18;
                comboBoxGFAbility19.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility19;
                comboBoxGFAbility20.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility20;
                comboBoxGFAbility21.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbility21;
                comboBoxGFElement.SelectedIndex = GF_GetElement();
                //checkBoxGFStatus.Checked = KernelWorker.GetSelectedGFData.GFStatusEnabler > 0x00 ? true : false;
                GFStatusWorker();
                numericUpDownGFStatusAttackEnabler.Value = KernelWorker.GetSelectedGFData.GFStatusAttackEnabler;
                numericUpDownGFQuezacoltComp.Value = KernelWorker.GetSelectedGFData.GFQuezacoltCompatibility;
                numericUpDownGFShivaComp.Value = KernelWorker.GetSelectedGFData.GFShivaCompatibility;
                numericUpDownGFIfritComp.Value = KernelWorker.GetSelectedGFData.GFIfritCompatibility;
                numericUpDownGFSirenComp.Value = KernelWorker.GetSelectedGFData.GFSirenCompatibility;
                numericUpDownGFBrothersComp.Value = KernelWorker.GetSelectedGFData.GFBrothersCompatibility;
                numericUpDownGFDiablosComp.Value = KernelWorker.GetSelectedGFData.GFDiablosCompatibility;
                numericUpDownGFCarbuncleComp.Value = KernelWorker.GetSelectedGFData.GFCarbuncleCompatibility;
                numericUpDownGFLeviathanComp.Value = KernelWorker.GetSelectedGFData.GFLeviathanCompatibility;
                numericUpDownGFPandemonaComp.Value = KernelWorker.GetSelectedGFData.GFPandemonaCompatibility;
                numericUpDownGFCerberusComp.Value = KernelWorker.GetSelectedGFData.GFCerberusCompatibility;
                numericUpDownGFAlexanderComp.Value = KernelWorker.GetSelectedGFData.GFAlexanderCompatibility;
                numericUpDownGFDoomtrainComp.Value = KernelWorker.GetSelectedGFData.GFDoomtrainCompatibility;
                numericUpDownGFBahamutComp.Value = KernelWorker.GetSelectedGFData.GFBahamutCompatibility;
                numericUpDownGFCactuarComp.Value = KernelWorker.GetSelectedGFData.GFCactuarCompatibility;
                numericUpDownGFTonberryComp.Value = KernelWorker.GetSelectedGFData.GFTonberryCompatibility;
                numericUpDownGFEdenComp.Value = KernelWorker.GetSelectedGFData.GFEdenCompatibility;
            }
            catch (Exception eeException)
            {
                MessageBox.Show(eeException.ToString());
            }
            _loaded = true;
        }


        //Non Junctionable GFs Attacks
        /*private void checkBoxGFAttacksStatus_Checked(object sender, EventArgs e)
        {
            if (checkBoxGFAttacksStatus.Checked == true)
            {
                checkBoxGFAttacksSleep.Enabled = true;
                checkBoxGFAttacksHaste.Enabled = true;
                checkBoxGFAttacksSlow.Enabled = true;
                checkBoxGFAttacksStop.Enabled = true;
                checkBoxGFAttacksRegen.Enabled = true;
                checkBoxGFAttacksProtect.Enabled = true;
                checkBoxGFAttacksShell.Enabled = true;
                checkBoxGFAttacksReflect.Enabled = true;
                checkBoxGFAttacksAura.Enabled = true;
                checkBoxGFAttacksCurse.Enabled = true;
                checkBoxGFAttacksDoom.Enabled = true;
                checkBoxGFAttacksInvincible.Enabled = true;
                checkBoxGFAttacksPetrifying.Enabled = true;
                checkBoxGFAttacksFloat.Enabled = true;
                checkBoxGFAttacksConfusion.Enabled = true;
                checkBoxGFAttacksDrain.Enabled = true;
                checkBoxGFAttacksEject.Enabled = true;
                checkBoxGFAttacksDouble.Enabled = true;
                checkBoxGFAttacksTriple.Enabled = true;
                checkBoxGFAttacksDefend.Enabled = true;
                checkBoxGFAttacksVit0.Enabled = true;
                checkBoxGFAttacksDeath.Enabled = true;
                checkBoxGFAttacksPoison.Enabled = true;
                checkBoxGFAttacksPetrify.Enabled = true;
                checkBoxGFAttacksDarkness.Enabled = true;
                checkBoxGFAttacksSilence.Enabled = true;
                checkBoxGFAttacksBerserk.Enabled = true;
                checkBoxGFAttacksZombie.Enabled = true;
            }

            else if (checkBoxGFAttacksStatus.Checked == false)
            {
                checkBoxGFAttacksSleep.Checked = false;
                checkBoxGFAttacksHaste.Checked = false;
                checkBoxGFAttacksSlow.Checked = false;
                checkBoxGFAttacksStop.Checked = false;
                checkBoxGFAttacksRegen.Checked = false;
                checkBoxGFAttacksProtect.Checked = false;
                checkBoxGFAttacksShell.Checked = false;
                checkBoxGFAttacksReflect.Checked = false;
                checkBoxGFAttacksAura.Checked = false;
                checkBoxGFAttacksCurse.Checked = false;
                checkBoxGFAttacksDoom.Checked = false;
                checkBoxGFAttacksInvincible.Checked = false;
                checkBoxGFAttacksPetrifying.Checked = false;
                checkBoxGFAttacksFloat.Checked = false;
                checkBoxGFAttacksConfusion.Checked = false;
                checkBoxGFAttacksDrain.Checked = false;
                checkBoxGFAttacksEject.Checked = false;
                checkBoxGFAttacksDouble.Checked = false;
                checkBoxGFAttacksTriple.Checked = false;
                checkBoxGFAttacksDefend.Checked = false;
                checkBoxGFAttacksVit0.Checked = false;
                checkBoxGFAttacksDeath.Checked = false;
                checkBoxGFAttacksPoison.Checked = false;
                checkBoxGFAttacksPetrify.Checked = false;
                checkBoxGFAttacksDarkness.Checked = false;
                checkBoxGFAttacksSilence.Checked = false;
                checkBoxGFAttacksBerserk.Checked = false;
                checkBoxGFAttacksZombie.Checked = false;
                //when unchecking the enabler the following also unchecks all the statuses
                checkBoxGFAttacksSleep.Enabled = false;
                checkBoxGFAttacksHaste.Enabled = false;
                checkBoxGFAttacksSlow.Enabled = false;
                checkBoxGFAttacksStop.Enabled = false;
                checkBoxGFAttacksRegen.Enabled = false;
                checkBoxGFAttacksProtect.Enabled = false;
                checkBoxGFAttacksShell.Enabled = false;
                checkBoxGFAttacksReflect.Enabled = false;
                checkBoxGFAttacksAura.Enabled = false;
                checkBoxGFAttacksCurse.Enabled = false;
                checkBoxGFAttacksDoom.Enabled = false;
                checkBoxGFAttacksInvincible.Enabled = false;
                checkBoxGFAttacksPetrifying.Enabled = false;
                checkBoxGFAttacksFloat.Enabled = false;
                checkBoxGFAttacksConfusion.Enabled = false;
                checkBoxGFAttacksDrain.Enabled = false;
                checkBoxGFAttacksEject.Enabled = false;
                checkBoxGFAttacksDouble.Enabled = false;
                checkBoxGFAttacksTriple.Enabled = false;
                checkBoxGFAttacksDefend.Enabled = false;
                checkBoxGFAttacksVit0.Enabled = false;
                checkBoxGFAttacksDeath.Enabled = false;
                checkBoxGFAttacksPoison.Enabled = false;
                checkBoxGFAttacksPetrify.Enabled = false;
                checkBoxGFAttacksDarkness.Enabled = false;
                checkBoxGFAttacksSilence.Enabled = false;
                checkBoxGFAttacksBerserk.Enabled = false;
                checkBoxGFAttacksZombie.Enabled = false;

                KernelWorker.UpdateVariable_GFAttacks(7, 0);
            }
            KernelWorker.UpdateVariable_GFAttacks(2, checkBoxGFAttacksStatus.Checked ? 0xFF : 0x00);
        }*/
        //opted out for now



        private int GFAttacks_GetElement()
        {
            return KernelWorker.GetSelectedGFAttacksData.ElementGFAttacks == KernelWorker.Element.Fire
                        ? 0
                        : KernelWorker.GetSelectedGFAttacksData.ElementGFAttacks == KernelWorker.Element.Ice
                            ? 1
                            : KernelWorker.GetSelectedGFAttacksData.ElementGFAttacks == KernelWorker.Element.Thunder
                                ? 2
                                : KernelWorker.GetSelectedGFAttacksData.ElementGFAttacks == KernelWorker.Element.Earth
                                    ? 3
                                    : KernelWorker.GetSelectedGFAttacksData.ElementGFAttacks == KernelWorker.Element.Poison
                                        ? 4
                                        : KernelWorker.GetSelectedGFAttacksData.ElementGFAttacks == KernelWorker.Element.Wind
                                            ? 5
                                            : KernelWorker.GetSelectedGFAttacksData.ElementGFAttacks ==
                                              KernelWorker.Element.Water
                                                ? 6
                                                : KernelWorker.GetSelectedGFAttacksData.ElementGFAttacks ==
                                                  KernelWorker.Element.Holy
                                                    ? 7
                                                    : KernelWorker.GetSelectedGFAttacksData.ElementGFAttacks ==
                                                      KernelWorker.Element.NonElemental
                                                        ? comboBoxGFAttacksElement.Items.Count - 1
                                                        : 0;
        }

        private byte GFAttacks_GetElement(int Index)
        {
            byte elem = (byte)(Index == 8 ? (byte)KernelWorker.Element.NonElemental :
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

        private void GFAttacksStatusWorker()
        {
            //checkBoxMagicSleep.Checked =  ? true : false
            checkBoxGFAttacksSleep.Checked = (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks1 & 0x01) >= 1 ? true : false;
            checkBoxGFAttacksHaste.Checked = (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks1 & 0x02) >= 1 ? true : false;
            checkBoxGFAttacksSlow.Checked = (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks1 & 0x04) >= 1 ? true : false;
            checkBoxGFAttacksStop.Checked = (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks1 & 0x08) >= 1 ? true : false;
            checkBoxGFAttacksRegen.Checked = (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks1 & 0x10) >= 1 ? true : false;
            checkBoxGFAttacksProtect.Checked = (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks1 & 0x20) >= 1 ? true : false;
            checkBoxGFAttacksShell.Checked = (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks1 & 0x40) >= 1 ? true : false;
            checkBoxGFAttacksReflect.Checked = (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks1 & 0x80) >= 1 ? true : false;

            checkBoxGFAttacksAura.Checked = (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks2 & 0x01) >= 1 ? true : false;
            checkBoxGFAttacksCurse.Checked = (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks2 & 0x02) >= 1 ? true : false;
            checkBoxGFAttacksDoom.Checked = (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks2 & 0x04) >= 1 ? true : false;
            checkBoxGFAttacksInvincible.Checked = (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks2 & 0x08) >= 1 ? true : false;
            checkBoxGFAttacksPetrifying.Checked = (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks2 & 0x10) >= 1 ? true : false;
            checkBoxGFAttacksFloat.Checked = (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks2 & 0x20) >= 1 ? true : false;
            checkBoxGFAttacksConfusion.Checked = (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks2 & 0x40) >= 1 ? true : false;
            checkBoxGFAttacksDrain.Checked = (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks2 & 0x80) >= 1 ? true : false;

            checkBoxGFAttacksEject.Checked = (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks3 & 0x01) >= 1 ? true : false;
            checkBoxGFAttacksDouble.Checked = (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks3 & 0x02) >= 1 ? true : false;
            checkBoxGFAttacksTriple.Checked = (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks3 & 0x04) >= 1 ? true : false;
            checkBoxGFAttacksDefend.Checked = (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks3 & 0x08) >= 1 ? true : false;

            checkBoxGFAttacksVit0.Checked = (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks4 & 0x01) >= 1 ? true : false;

            checkBoxGFAttacksDeath.Checked = (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks5 & 0x01) >= 1 ? true : false;
            checkBoxGFAttacksPoison.Checked = (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks5 & 0x02) >= 1 ? true : false;
            checkBoxGFAttacksPetrify.Checked = (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks5 & 0x04) >= 1 ? true : false;
            checkBoxGFAttacksDarkness.Checked = (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks5 & 0x08) >= 1 ? true : false;
            checkBoxGFAttacksSilence.Checked = (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks5 & 0x10) >= 1 ? true : false;
            checkBoxGFAttacksBerserk.Checked = (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks5 & 0x20) >= 1 ? true : false;
            checkBoxGFAttacksZombie.Checked = (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks5 & 0x40) >= 1 ? true : false;
        }

        private void listBoxGFAttacks_SelectedIndexChanged(object sender, EventArgs e)
        {
            _loaded = false;
            if (KernelWorker.Kernel == null)
                return;
            KernelWorker.ReadGFAttacks(listBoxGFAttacks.SelectedIndex);

            try
            {
                comboBoxGFAttacksMagicID.SelectedIndex = KernelWorker.GetSelectedGFAttacksData.GFAttacksMagicID;
                numericUpDownGFAttacksPower.Value = KernelWorker.GetSelectedGFAttacksData.GFAttacksPower;
                //checkBoxGFAttacksStatus.Checked = KernelWorker.GetSelectedGFAttacksData.GFAttacksStatusEnabler > 0x00 ? true : false;
                numericUpDownGFAttacksStatusAttackEnabler.Value = KernelWorker.GetSelectedGFAttacksData.GFAttacksStatusEnabler;
                comboBoxGFAttacksElement.SelectedIndex = GFAttacks_GetElement();
                GFAttacksStatusWorker();
                numericUpDownGFAttacksPowerMod.Value = KernelWorker.GetSelectedGFAttacksData.GFAttacksPowerMod;
                numericUpDownGFAttacksLevelMod.Value = KernelWorker.GetSelectedGFAttacksData.GFAttacksLevelMod;
            }
            catch (Exception eeeException)
            {
                MessageBox.Show(eeeException.ToString());
            }
            _loaded = true;
        }
    }
}