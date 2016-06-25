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

            #region DISABLING OBJECTS

            //for disabling save buttons when no file is open
            saveToolStripMenuItem.Enabled = false;
            saveAsToolStripMenuItem.Enabled = false;
            saveAsToolStripButton.Enabled = false;
            saveToolStripButton.Enabled = false;

            //this is for enabling the switching of listboxes in the ability section
            listBoxAbStats.Visible = false;
            listBoxAbJunction.Visible = false;
            listBoxAbCommand.Visible = false;
            listBoxAbGF.Visible = false;
            listBoxAbParty.Visible = false;
            listBoxAbMenu.Visible = false;
            tabControlAbilities.SelectedIndexChanged += new EventHandler(tabControlAbilities_SelectedIndexChanged);

            #endregion

            #region EVENT HANDLERS MAGIC

            comboBoxMagicMagicID.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(2, comboBoxMagicMagicID.SelectedIndex);
            comboBoxMagicAttackType.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(43, comboBoxMagicAttackType.SelectedIndex);
            numericUpDownMagicSpellPower.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(3, numericUpDownMagicSpellPower.Value);
            checkBoxMagicFlag1.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(44, 0x01);
            checkBoxMagicFlag2.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(44, 0x02);
            checkBoxMagicFlag3.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(44, 0x04);
            checkBoxMagicBreakDamageLimit.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(44, 0x08);
            checkBoxMagicFlag5.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(44, 0x10);
            checkBoxMagicFlag6.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(44, 0x20);
            checkBoxMagicFlag7.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(44, 0x40);
            checkBoxMagicFlag8.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(44, 0x80);
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
            checkBoxMagicUnk1.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x10, 2);
            checkBoxMagicUnk2.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x20, 2);
            checkBoxMagicCharged.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x40, 2);
            checkBoxMagicBackAttack.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x80, 2);
            checkBoxMagicVit0.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x01, 3);
            checkBoxMagicAngelWing.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x02, 3);
            checkBoxMagicUnk3.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x04, 3);
            checkBoxMagicUnk4.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x08, 3);
            checkBoxMagicUnk5.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x10, 3);
            checkBoxMagicUnk6.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x20, 3);
            checkBoxMagicHasMagic.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x40, 3);
            checkBoxMagicSummonGF.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x80, 3);
            checkBoxMagicDeath.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x01, 4);
            checkBoxMagicPoison.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x02, 4);
            checkBoxMagicPetrify.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x04, 4);
            checkBoxMagicDarkness.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x08, 4);
            checkBoxMagicSilence.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x10, 4);
            checkBoxMagicBerserk.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x20, 4);
            checkBoxMagicZombie.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x40, 4);
            checkBoxMagicUnk7.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(6, 0x80, 4);
            numericUpDownMagicStatusAttack.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(25, numericUpDownMagicStatusAttack.Value);
            checkBoxMagicTarget1.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(42, 0x01);
            checkBoxMagicTarget2.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(42, 0x02);
            checkBoxMagicTarget3.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(42, 0x04);
            checkBoxMagicTarget4.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(42, 0x08);
            checkBoxMagicTarget5.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(42, 0x10);
            checkBoxMagicTarget6.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(42, 0x20);
            checkBoxMagicTarget7.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(42, 0x40);
            checkBoxMagicTarget8.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(42, 0x80);
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

            #endregion

            #region EVENT HANDLERS J-GFs

            comboBoxGFMagicID.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(0, comboBoxGFMagicID.SelectedIndex);
            comboBoxGFAttackType.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(26, comboBoxGFAttackType.SelectedIndex);
            numericUpDownGFPower.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_GF(1, numericUpDownGFPower.Value);
            checkBoxGFFlagShelled.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(27, 0x01);
            checkBoxGFFlag2.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(27, 0x02);
            checkBoxGFFlag3.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(27, 0x04);
            checkBoxGFFlagBreakDamageLimit.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(27, 0x08);
            checkBoxGFFlagReflected.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(27, 0x10);
            checkBoxGFFlag6.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(27, 0x20);
            checkBoxGFFlag7.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(27, 0x40);
            checkBoxGFFlag8.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(27, 0x80);
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
            comboBoxGFAbilityUnlock1.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(28, comboBoxGFAbilityUnlock1.SelectedIndex, 0);
            comboBoxGFAbilityUnlock2.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(28, comboBoxGFAbilityUnlock2.SelectedIndex, 1);
            comboBoxGFAbilityUnlock3.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(28, comboBoxGFAbilityUnlock3.SelectedIndex, 2);
            comboBoxGFAbilityUnlock4.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(28, comboBoxGFAbilityUnlock4.SelectedIndex, 3);
            comboBoxGFAbilityUnlock5.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(28, comboBoxGFAbilityUnlock5.SelectedIndex, 4);
            comboBoxGFAbilityUnlock6.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(28, comboBoxGFAbilityUnlock6.SelectedIndex, 5);
            comboBoxGFAbilityUnlock7.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(28, comboBoxGFAbilityUnlock7.SelectedIndex, 6);
            comboBoxGFAbilityUnlock8.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(28, comboBoxGFAbilityUnlock8.SelectedIndex, 7);
            comboBoxGFAbilityUnlock9.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(28, comboBoxGFAbilityUnlock9.SelectedIndex, 8);
            comboBoxGFAbilityUnlock10.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(28, comboBoxGFAbilityUnlock10.SelectedIndex, 9);
            comboBoxGFAbilityUnlock11.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(28, comboBoxGFAbilityUnlock11.SelectedIndex, 10);
            comboBoxGFAbilityUnlock12.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(28, comboBoxGFAbilityUnlock12.SelectedIndex, 11);
            comboBoxGFAbilityUnlock13.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(28, comboBoxGFAbilityUnlock13.SelectedIndex, 12);
            comboBoxGFAbilityUnlock14.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(28, comboBoxGFAbilityUnlock14.SelectedIndex, 13);
            comboBoxGFAbilityUnlock15.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(28, comboBoxGFAbilityUnlock15.SelectedIndex, 14);
            comboBoxGFAbilityUnlock16.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(28, comboBoxGFAbilityUnlock16.SelectedIndex, 15);
            comboBoxGFAbilityUnlock17.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(28, comboBoxGFAbilityUnlock17.SelectedIndex, 16);
            comboBoxGFAbilityUnlock18.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(28, comboBoxGFAbilityUnlock18.SelectedIndex, 17);
            comboBoxGFAbilityUnlock19.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(28, comboBoxGFAbilityUnlock19.SelectedIndex, 18);
            comboBoxGFAbilityUnlock20.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(28, comboBoxGFAbilityUnlock20.SelectedIndex, 19);
            comboBoxGFAbilityUnlock21.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GF(28, comboBoxGFAbilityUnlock21.SelectedIndex, 20);
            checkBoxGFSleep.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x01, 0, 1);
            checkBoxGFHaste.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x02, 0, 1);
            checkBoxGFSlow.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x04, 0, 1);
            checkBoxGFStop.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x08, 0, 1);
            checkBoxGFRegen.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x10, 0, 1);
            checkBoxGFProtect.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x20, 0, 1);
            checkBoxGFShell.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x40, 0, 1);
            checkBoxGFReflect.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x80, 0, 1);
            checkBoxGFAura.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x01, 0, 2);
            checkBoxGFCurse.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x02, 0, 2);
            checkBoxGFDoom.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x04, 0, 2);
            checkBoxGFInvincible.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x08, 0, 2);
            checkBoxGFPetrifying.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x10, 0, 2);
            checkBoxGFFloat.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x20, 0, 2);
            checkBoxGFConfusion.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(6, 0x40, 0, 2);
            checkBoxGFDrain.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x80, 0, 2);
            checkBoxGFEject.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x01, 0, 3);
            checkBoxGFDouble.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x02, 0, 3);
            checkBoxGFTriple.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x04, 0, 3);
            checkBoxGFDefend.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x08, 0, 3);
            checkBoxGFUnk1.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x10, 0, 3);
            checkBoxGFUnk2.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x20, 0, 3);
            checkBoxGFCharged.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x40, 0, 3);
            checkBoxGFBackAttack.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x80, 0, 3);
            checkBoxGFVit0.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x01, 0, 4);
            checkBoxGFAngelWing.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x02, 0, 4);
            checkBoxGFUnk3.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x04, 0, 4);
            checkBoxGFUnk4.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x08, 0, 4);
            checkBoxGFUnk5.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x10, 0, 4);
            checkBoxGFUnk6.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x20, 0, 4);
            checkBoxGFHasMagic.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x40, 0, 4);
            checkBoxGFSummonGF.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x80, 0, 4);
            checkBoxGFDeath.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x01, 0, 0);
            checkBoxGFPoison.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x02, 0, 0);
            checkBoxGFPetrify.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x04, 0, 0);
            checkBoxGFDarkness.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x08, 0, 0);
            checkBoxGFSilence.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x10, 0, 0);
            checkBoxGFBerserk.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x20, 0, 0);
            checkBoxGFZombie.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x40, 0, 0);
            checkBoxGFUnk7.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x80, 0, 0);
            numericUpDownGFStatusAttack.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_GF(8, numericUpDownGFStatusAttack.Value);
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

            #endregion

            #region EVENT HANDLERS NON-J GFs ATTACK

            comboBoxGFAttacksMagicID.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(0, comboBoxGFAttacksMagicID.SelectedIndex);
            comboBoxGFAttacksAttackType.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(8, comboBoxGFAttacksAttackType.SelectedIndex);
            numericUpDownGFAttacksPower.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(1, numericUpDownGFAttacksPower.Value);
            numericUpDownGFAttacksStatusAttack.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(2, numericUpDownGFAttacksStatusAttack.Value);
            checkBoxGFAttacksFlagShelled.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(9, 0x01);
            checkBoxGFAttacksFlag2.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(9, 0x02);
            checkBoxGFAttacksFlag3.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(9, 0x04);
            checkBoxGFAttacksFlagBreakDamageLimit.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(9, 0x08);
            checkBoxGFAttacksFlagReflected.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(9, 0x10);
            checkBoxGFAttacksFlag6.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(9, 0x20);
            checkBoxGFAttacksFlag7.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(9, 0x40);
            checkBoxGFAttacksFlag8.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(9, 0x80);
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
            checkBoxGFAttacksUnk1.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(4, 0x10, 2);
            checkBoxGFAttacksUnk2.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(4, 0x20, 2);
            checkBoxGFAttacksCharged.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(4, 0x40, 2);
            checkBoxGFAttacksBackAttack.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(4, 0x80, 2);
            checkBoxGFAttacksVit0.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(4, 0x01, 3);
            checkBoxGFAttacksAngelWing.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(4, 0x02, 3);
            checkBoxGFAttacksUnk3.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(4, 0x04, 3);
            checkBoxGFAttacksUnk4.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(4, 0x08, 3);
            checkBoxGFAttacksUnk5.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(4, 0x10, 3);
            checkBoxGFAttacksUnk6.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(4, 0x20, 3);
            checkBoxGFAttacksHasMagic.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(4, 0x40, 3);
            checkBoxGFAttacksSummonGF.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(4, 0x80, 3);
            checkBoxGFAttacksDeath.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(4, 0x01, 4);
            checkBoxGFAttacksPoison.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(4, 0x02, 4);
            checkBoxGFAttacksPetrify.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(4, 0x04, 4);
            checkBoxGFAttacksDarkness.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(4, 0x08, 4);
            checkBoxGFAttacksSilence.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(4, 0x10, 4);
            checkBoxGFAttacksBerserk.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(4, 0x20, 4);
            checkBoxGFAttacksZombie.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(4, 0x40, 4);
            checkBoxGFAttacksUnk7.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(4, 0x80, 4);
            numericUpDownGFAttacksPowerMod.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(5, numericUpDownGFAttacksPowerMod.Value);
            numericUpDownGFAttacksLevelMod.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_GFAttacks(6, numericUpDownGFAttacksLevelMod.Value);

            #endregion

            #region EVENT HANDLERS WEAPONS
            checkBoxWeaponsRenzoFinRough.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Weapons(0, 0x01, 0);
            checkBoxWeaponsRenzoFinFated.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Weapons(0, 0x02, 0);
            checkBoxWeaponsRenzoFinBlasting.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Weapons(0, 0x04, 0);
            checkBoxWeaponsRenzoFinLion.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Weapons(0, 0x08, 0);
            comboBoxWeaponsCharacterID.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_Weapons(1, comboBoxWeaponsCharacterID.SelectedIndex);
            numericUpDownWeaponsAttackPower.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Weapons(2, numericUpDownWeaponsAttackPower.Value);
            numericUpDownWeaponsHITBonus.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Weapons(3, numericUpDownWeaponsHITBonus.Value);
            numericUpDownWeaponsSTRBonus.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Weapons(4, numericUpDownWeaponsSTRBonus.Value);
            numericUpDownWeaponsTier.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Weapons(5, numericUpDownWeaponsTier.Value);

            #endregion

            #region EVENT HANDLERS CHARACTERS
            numericUpDownCharCrisisLevelHP.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Characters(0, numericUpDownCharCrisisLevelHP.Value);
            comboBoxCharGender.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_Characters(1, comboBoxCharGender.SelectedIndex);
            numericUpDownCharLimitID.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Characters(2, numericUpDownCharLimitID.Value);
            numericUpDownCharLimitParam.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Characters(3, numericUpDownCharLimitParam.Value);
            numericUpDownCharEXP1.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Characters(4, numericUpDownCharEXP1.Value);
            numericUpDownCharEXP2.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Characters(5, numericUpDownCharEXP2.Value);
            numericUpDownCharHP1.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Characters(6, numericUpDownCharHP1.Value);
            numericUpDownCharHP2.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Characters(7, numericUpDownCharHP2.Value);
            numericUpDownCharHP3.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Characters(8, numericUpDownCharHP3.Value);
            numericUpDownCharHP4.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Characters(9, numericUpDownCharHP4.Value);
            numericUpDownCharSTR1.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Characters(10, numericUpDownCharSTR1.Value);
            numericUpDownCharSTR2.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Characters(11, numericUpDownCharSTR2.Value);
            numericUpDownCharSTR3.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Characters(12, numericUpDownCharSTR3.Value);
            numericUpDownCharSTR4.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Characters(13, numericUpDownCharSTR4.Value);
            numericUpDownCharVIT1.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Characters(14, numericUpDownCharVIT1.Value);
            numericUpDownCharVIT2.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Characters(15, numericUpDownCharVIT2.Value);
            numericUpDownCharVIT3.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Characters(16, numericUpDownCharVIT3.Value);
            numericUpDownCharVIT4.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Characters(17, numericUpDownCharVIT4.Value);
            numericUpDownCharMAG1.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Characters(18, numericUpDownCharMAG1.Value);
            numericUpDownCharMAG2.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Characters(19, numericUpDownCharMAG2.Value);
            numericUpDownCharMAG3.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Characters(20, numericUpDownCharMAG3.Value);
            numericUpDownCharMAG4.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Characters(21, numericUpDownCharMAG4.Value);
            numericUpDownCharSPR1.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Characters(22, numericUpDownCharSPR1.Value);
            numericUpDownCharSPR2.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Characters(23, numericUpDownCharSPR2.Value);
            numericUpDownCharSPR3.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Characters(24, numericUpDownCharSPR3.Value);
            numericUpDownCharSPR4.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Characters(25, numericUpDownCharSPR4.Value);
            numericUpDownCharSPD1.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Characters(26, numericUpDownCharSPD1.Value);
            numericUpDownCharSPD2.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Characters(27, numericUpDownCharSPD2.Value);
            numericUpDownCharSPD3.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Characters(28, numericUpDownCharSPD3.Value);
            numericUpDownCharSPD4.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Characters(29, numericUpDownCharSPD4.Value);
            numericUpDownCharLUCK1.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Characters(30, numericUpDownCharLUCK1.Value);
            numericUpDownCharLUCK2.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Characters(31, numericUpDownCharLUCK2.Value);
            numericUpDownCharLUCK3.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Characters(32, numericUpDownCharLUCK3.Value);
            numericUpDownCharLUCK4.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Characters(33, numericUpDownCharLUCK4.Value);

            #endregion

            #region EVENT HANDLERS ENEMY ATTACKS

            comboBoxEnemyAttacksMagicID.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(0, comboBoxEnemyAttacksMagicID.SelectedIndex);
            comboBoxEnemyAttacksAttackType.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(1, comboBoxEnemyAttacksAttackType.SelectedIndex);
            numericUpDownEnemyAttacksAttackPower.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(2, numericUpDownEnemyAttacksAttackPower.Value);
            checkBoxEnemyAttacksFlagShelled.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(3, 0x01);
            checkBoxEnemyAttacksFlag2.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(3, 0x02);
            checkBoxEnemyAttacksFlag3.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(3, 0x04);
            checkBoxEnemyAttacksFlagBreakDamageLimit.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(3, 0x08);
            checkBoxEnemyAttacksFlagReflected.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(3, 0x10);
            checkBoxEnemyAttacksFlag6.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(3, 0x20);
            checkBoxEnemyAttacksFlag7.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(3, 0x40);
            checkBoxEnemyAttacksFlag8.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(3, 0x80);
            comboBoxEnemyAttacksElement.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(4, EnemyAttacks_GetElement(comboBoxEnemyAttacksElement.SelectedIndex));
            numericUpDownEnemyAttacksStatusAttack.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(5, numericUpDownEnemyAttacksStatusAttack.Value);         
            checkBoxEnemyAttacksSleep.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(6, 0x01, 1);
            checkBoxEnemyAttacksHaste.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(6, 0x02, 1);
            checkBoxEnemyAttacksSlow.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(6, 0x04, 1);
            checkBoxEnemyAttacksStop.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(6, 0x08, 1);
            checkBoxEnemyAttacksRegen.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(6, 0x10, 1);
            checkBoxEnemyAttacksProtect.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(6, 0x20, 1);
            checkBoxEnemyAttacksShell.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(6, 0x40, 1);
            checkBoxEnemyAttacksReflect.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(6, 0x80, 1);
            checkBoxEnemyAttacksAura.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(6, 0x01, 2);
            checkBoxEnemyAttacksCurse.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(6, 0x02, 2);
            checkBoxEnemyAttacksDoom.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(6, 0x04, 2);
            checkBoxEnemyAttacksInvincible.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(6, 0x08, 2);
            checkBoxEnemyAttacksPetrifying.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(6, 0x10, 2);
            checkBoxEnemyAttacksFloat.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(6, 0x20, 2);
            checkBoxEnemyAttacksConfusion.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(6, 0x40, 2);
            checkBoxEnemyAttacksDrain.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(6, 0x80, 2);
            checkBoxEnemyAttacksEject.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(6, 0x01, 3);
            checkBoxEnemyAttacksDouble.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(6, 0x02, 3);
            checkBoxEnemyAttacksTriple.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(6, 0x04, 3);
            checkBoxEnemyAttacksDefend.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(6, 0x08, 3);
            checkBoxEnemyAttacksUnk1.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(6, 0x10, 3);
            checkBoxEnemyAttacksUnk2.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(6, 0x20, 3);
            checkBoxEnemyAttacksCharged.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(6, 0x40, 3);
            checkBoxEnemyAttacksBackAttack.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(6, 0x80, 3);
            checkBoxEnemyAttacksVit0.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(6, 0x01, 4);
            checkBoxEnemyAttacksAngelWing.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(6, 0x02, 4);
            checkBoxEnemyAttacksUnk3.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(6, 0x04, 4);
            checkBoxEnemyAttacksUnk4.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(6, 0x08, 4);
            checkBoxEnemyAttacksUnk5.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(6, 0x10, 4);
            checkBoxEnemyAttacksUnk6.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(6, 0x20, 4);
            checkBoxEnemyAttacksHasMagic.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(6, 0x40, 4);
            checkBoxEnemyAttacksSummonGF.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(6, 0x80, 4);
            checkBoxEnemyAttacksDeath.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(6, 0x01, 0);
            checkBoxEnemyAttacksPoison.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(6, 0x02, 0);
            checkBoxEnemyAttacksPetrify.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(6, 0x04, 0);
            checkBoxEnemyAttacksDarkness.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(6, 0x08, 0);
            checkBoxEnemyAttacksSilence.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(6, 0x10, 0);
            checkBoxEnemyAttacksBerserk.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(6, 0x20, 0);
            checkBoxEnemyAttacksZombie.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(6, 0x40, 0);
            checkBoxEnemyAttacksUnk7.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(6, 0x80, 0);

            #endregion

            #region EVENT HANDLERS BLUE MAGIC

            comboBoxBlueMagicMagicID.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagic(0, comboBoxBlueMagicMagicID.SelectedIndex);
            comboBoxBlueMagicAttackType.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagic(1, comboBoxBlueMagicAttackType.SelectedIndex);
            checkBoxBlueMagicFlag1.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagic(2, 0x01);
            checkBoxBlueMagicFlag2.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagic(2, 0x02);
            checkBoxBlueMagicFlag3.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagic(2, 0x04);
            checkBoxBlueMagicFlag4.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagic(2, 0x08);
            checkBoxBlueMagicFlag5.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagic(2, 0x10);
            checkBoxBlueMagicFlag6.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagic(2, 0x20);
            checkBoxBlueMagicFlag7.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagic(2, 0x40);
            checkBoxBlueMagicFlag8.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagic(2, 0x80);
            comboBoxBlueMagicElement.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagic(3, BlueMagic_GetElement(comboBoxBlueMagicElement.SelectedIndex));
            numericUpDownBlueMagicStatusAttack.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagic(4, numericUpDownBlueMagicStatusAttack.Value);

            #endregion

            #region EVENT HANDLERS BLUE MAGIC PARAM

            checkBoxBlueMagicCL1Sleep.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x01, 0);
            checkBoxBlueMagicCL1Haste.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x02, 0);
            checkBoxBlueMagicCL1Slow.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x04, 0);
            checkBoxBlueMagicCL1Stop.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x08, 0);
            checkBoxBlueMagicCL1Regen.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x10, 0);
            checkBoxBlueMagicCL1Protect.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x20, 0);
            checkBoxBlueMagicCL1Shell.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x40, 0);
            checkBoxBlueMagicCL1Reflect.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x80, 0);
            checkBoxBlueMagicCL1Aura.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x01, 1);
            checkBoxBlueMagicCL1Curse.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x02, 1);
            checkBoxBlueMagicCL1Doom.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x04, 1);
            checkBoxBlueMagicCL1Invincible.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x08, 1);
            checkBoxBlueMagicCL1Petrifying.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x10, 1);
            checkBoxBlueMagicCL1Float.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x20, 1);
            checkBoxBlueMagicCL1Confusion.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x40, 1);
            checkBoxBlueMagicCL1Drain.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x80, 1);
            checkBoxBlueMagicCL1Eject.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x01, 2);
            checkBoxBlueMagicCL1Double.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x02, 2);
            checkBoxBlueMagicCL1Triple.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x04, 2);
            checkBoxBlueMagicCL1Defend.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x08, 2);
            checkBoxBlueMagicCL1Unk1.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x10, 2);
            checkBoxBlueMagicCL1Unk2.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x20, 2);
            checkBoxBlueMagicCL1Charged.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x40, 2);
            checkBoxBlueMagicCL1BackAttack.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x80, 2);
            checkBoxBlueMagicCL1Vit0.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x01, 3);
            checkBoxBlueMagicCL1AngelWing.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x02, 3);
            checkBoxBlueMagicCL1Unk3.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x04, 3);
            checkBoxBlueMagicCL1Unk4.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x08, 3);
            checkBoxBlueMagicCL1Unk5.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x10, 3);
            checkBoxBlueMagicCL1Unk6.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x20, 3);
            checkBoxBlueMagicCL1HasMagic.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x40, 3);
            checkBoxBlueMagicCL1SummonGF.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x80, 3);
            checkBoxBlueMagicCL1Death.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x01, 4);
            checkBoxBlueMagicCL1Poison.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x02, 4);
            checkBoxBlueMagicCL1Petrify.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x04, 4);
            checkBoxBlueMagicCL1Darkness.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x08, 4);
            checkBoxBlueMagicCL1Silence.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x10, 4);
            checkBoxBlueMagicCL1Berserk.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x20, 4);
            checkBoxBlueMagicCL1Zombie.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x40, 4);
            checkBoxBlueMagicCL1Unk7.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x80, 4);
            numericUpDownBlueMagicCL1AttackPower.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(1, numericUpDownBlueMagicCL1AttackPower.Value);
            numericUpDownBlueMagicCL1DeathLevel.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(2, numericUpDownBlueMagicCL1DeathLevel.Value);

            checkBoxBlueMagicCL2Sleep.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x01, 5);
            checkBoxBlueMagicCL2Haste.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x02, 5);
            checkBoxBlueMagicCL2Slow.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x04, 5);
            checkBoxBlueMagicCL2Stop.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x08, 5);
            checkBoxBlueMagicCL2Regen.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x10, 5);
            checkBoxBlueMagicCL2Protect.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x20, 5);
            checkBoxBlueMagicCL2Shell.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x40, 5);
            checkBoxBlueMagicCL2Reflect.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x80, 5);
            checkBoxBlueMagicCL2Aura.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x01, 6);
            checkBoxBlueMagicCL2Curse.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x02, 6);
            checkBoxBlueMagicCL2Doom.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x04, 6);
            checkBoxBlueMagicCL2Invincible.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x08, 6);
            checkBoxBlueMagicCL2Petrifying.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x10, 6);
            checkBoxBlueMagicCL2Float.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x20, 6);
            checkBoxBlueMagicCL2Confusion.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x40, 6);
            checkBoxBlueMagicCL2Drain.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x80, 6);
            checkBoxBlueMagicCL2Eject.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x01, 7);
            checkBoxBlueMagicCL2Double.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x02, 7);
            checkBoxBlueMagicCL2Triple.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x04, 7);
            checkBoxBlueMagicCL2Defend.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x08, 7);
            checkBoxBlueMagicCL2Unk1.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x10, 7);
            checkBoxBlueMagicCL2Unk2.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x20, 7);
            checkBoxBlueMagicCL2Charged.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x40, 7);
            checkBoxBlueMagicCL2BackAttack.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x80, 7);
            checkBoxBlueMagicCL2Vit0.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x01, 8);
            checkBoxBlueMagicCL2AngelWing.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x02, 8);
            checkBoxBlueMagicCL2Unk3.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x04, 8);
            checkBoxBlueMagicCL2Unk4.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x08, 8);
            checkBoxBlueMagicCL2Unk5.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x10, 8);
            checkBoxBlueMagicCL2Unk6.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x20, 8);
            checkBoxBlueMagicCL2HasMagic.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x40, 8);
            checkBoxBlueMagicCL2SummonGF.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x80, 8);
            checkBoxBlueMagicCL2Death.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x01, 9);
            checkBoxBlueMagicCL2Poison.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x02, 9);
            checkBoxBlueMagicCL2Petrify.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x04, 9);
            checkBoxBlueMagicCL2Darkness.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x08, 9);
            checkBoxBlueMagicCL2Silence.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x10, 9);
            checkBoxBlueMagicCL2Berserk.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x20, 9);
            checkBoxBlueMagicCL2Zombie.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x40, 9);
            checkBoxBlueMagicCL2Unk7.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x80, 9);
            numericUpDownBlueMagicCL2AttackPower.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(3, numericUpDownBlueMagicCL2AttackPower.Value);
            numericUpDownBlueMagicCL2DeathLevel.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(4, numericUpDownBlueMagicCL2DeathLevel.Value);

            checkBoxBlueMagicCL3Sleep.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x01, 10);
            checkBoxBlueMagicCL3Haste.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x02, 10);
            checkBoxBlueMagicCL3Slow.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x04, 10);
            checkBoxBlueMagicCL3Stop.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x08, 10);
            checkBoxBlueMagicCL3Regen.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x10, 10);
            checkBoxBlueMagicCL3Protect.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x20, 10);
            checkBoxBlueMagicCL3Shell.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x40, 10);
            checkBoxBlueMagicCL3Reflect.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x80, 10);
            checkBoxBlueMagicCL3Aura.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x01, 11);
            checkBoxBlueMagicCL3Curse.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x02, 11);
            checkBoxBlueMagicCL3Doom.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x04, 11);
            checkBoxBlueMagicCL3Invincible.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x08, 11);
            checkBoxBlueMagicCL3Petrifying.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x10, 11);
            checkBoxBlueMagicCL3Float.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x20, 11);
            checkBoxBlueMagicCL3Confusion.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x40, 11);
            checkBoxBlueMagicCL3Drain.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x80, 11);
            checkBoxBlueMagicCL3Eject.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x01, 12);
            checkBoxBlueMagicCL3Double.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x02, 12);
            checkBoxBlueMagicCL3Triple.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x04, 12);
            checkBoxBlueMagicCL3Defend.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x08, 12);
            checkBoxBlueMagicCL3Unk1.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x10, 12);
            checkBoxBlueMagicCL3Unk2.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x20, 12);
            checkBoxBlueMagicCL3Charged.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x40, 12);
            checkBoxBlueMagicCL3BackAttack.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x80, 12);
            checkBoxBlueMagicCL3Vit0.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x01, 13);
            checkBoxBlueMagicCL3AngelWing.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x02, 13);
            checkBoxBlueMagicCL3Unk3.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x04, 13);
            checkBoxBlueMagicCL3Unk4.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x08, 13);
            checkBoxBlueMagicCL3Unk5.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x10, 13);
            checkBoxBlueMagicCL3Unk6.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x20, 13);
            checkBoxBlueMagicCL3HasMagic.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x40, 13);
            checkBoxBlueMagicCL3SummonGF.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x80, 13);
            checkBoxBlueMagicCL3Death.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x01, 14);
            checkBoxBlueMagicCL3Poison.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x02, 14);
            checkBoxBlueMagicCL3Petrify.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x04, 14);
            checkBoxBlueMagicCL3Darkness.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x08, 14);
            checkBoxBlueMagicCL3Silence.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x10, 14);
            checkBoxBlueMagicCL3Berserk.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x20, 14);
            checkBoxBlueMagicCL3Zombie.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x40, 14);
            checkBoxBlueMagicCL3Unk7.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x80, 14);
            numericUpDownBlueMagicCL3AttackPower.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(5, numericUpDownBlueMagicCL3AttackPower.Value);
            numericUpDownBlueMagicCL3DeathLevel.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(6, numericUpDownBlueMagicCL3DeathLevel.Value);

            checkBoxBlueMagicCL4Sleep.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x01, 15);
            checkBoxBlueMagicCL4Haste.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x02, 15);
            checkBoxBlueMagicCL4Slow.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x04, 15);
            checkBoxBlueMagicCL4Stop.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x08, 15);
            checkBoxBlueMagicCL4Regen.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x10, 15);
            checkBoxBlueMagicCL4Protect.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x20, 15);
            checkBoxBlueMagicCL4Shell.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x40, 15);
            checkBoxBlueMagicCL4Reflect.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x80, 15);
            checkBoxBlueMagicCL4Aura.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x01, 16);
            checkBoxBlueMagicCL4Curse.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x02, 16);
            checkBoxBlueMagicCL4Doom.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x04, 16);
            checkBoxBlueMagicCL4Invincible.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x08, 16);
            checkBoxBlueMagicCL4Petrifying.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x10, 16);
            checkBoxBlueMagicCL4Float.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x20, 16);
            checkBoxBlueMagicCL4Confusion.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x40, 16);
            checkBoxBlueMagicCL4Drain.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x80, 16);
            checkBoxBlueMagicCL4Eject.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x01, 17);
            checkBoxBlueMagicCL4Double.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x02, 17);
            checkBoxBlueMagicCL4Triple.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x04, 17);
            checkBoxBlueMagicCL4Defend.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x08, 17);
            checkBoxBlueMagicCL4Unk1.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x10, 17);
            checkBoxBlueMagicCL4Unk2.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x20, 17);
            checkBoxBlueMagicCL4Charged.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x40, 17);
            checkBoxBlueMagicCL4BackAttack.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x80, 17);
            checkBoxBlueMagicCL4Vit0.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x01, 18);
            checkBoxBlueMagicCL4AngelWing.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x02, 18);
            checkBoxBlueMagicCL4Unk3.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x04, 18);
            checkBoxBlueMagicCL4Unk4.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x08, 18);
            checkBoxBlueMagicCL4Unk5.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x10, 18);
            checkBoxBlueMagicCL4Unk6.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x20, 18);
            checkBoxBlueMagicCL4HasMagic.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x40, 18);
            checkBoxBlueMagicCL4SummonGF.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x80, 18);
            checkBoxBlueMagicCL4Death.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x01, 19);
            checkBoxBlueMagicCL4Poison.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x02, 19);
            checkBoxBlueMagicCL4Petrify.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x04, 19);
            checkBoxBlueMagicCL4Darkness.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x08, 19);
            checkBoxBlueMagicCL4Silence.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x10, 19);
            checkBoxBlueMagicCL4Berserk.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x20, 19);
            checkBoxBlueMagicCL4Zombie.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x40, 19);
            checkBoxBlueMagicCL4Unk7.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(0, 0x80, 19);
            numericUpDownBlueMagicCL4AttackPower.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(7, numericUpDownBlueMagicCL4AttackPower.Value);
            numericUpDownBlueMagicCL4DeathLevel.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(8, numericUpDownBlueMagicCL4DeathLevel.Value);

            #endregion

            #region EVENT HANDLERS STATS INCREASE ABILITIES

            comboBoxAbStatsStatToIncrease.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_StatPercentageAbilities(0, comboBoxAbStatsStatToIncrease.SelectedIndex);
            trackBarAbStatsIncrementValue.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_StatPercentageAbilities(1, trackBarAbStatsIncrementValue.Value);

            #endregion

            #region EVENT HANDLERS RENZOKUKEN FINISHERS

            comboBoxRenzoFinMagicID.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(0, comboBoxRenzoFinMagicID.SelectedIndex);
            comboBoxRenzoFinAttackType.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(1, comboBoxRenzoFinAttackType.SelectedIndex);
            numericUpDownRenzoFinAttackPower.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(2, numericUpDownRenzoFinAttackPower.Value);
            checkBoxRenzoFinTarget1.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(3, 0x01);
            checkBoxRenzoFinTarget2.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(3, 0x02);
            checkBoxRenzoFinTarget3.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(3, 0x04);
            checkBoxRenzoFinTarget4.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(3, 0x08);
            checkBoxRenzoFinTarget5.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(3, 0x10);
            checkBoxRenzoFinTarget6.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(3, 0x20);
            checkBoxRenzoFinTarget7.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(3, 0x40);
            checkBoxRenzoFinTarget8.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(3, 0x80);
            checkBoxRenzoFinFlag1.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(4, 0x01);
            checkBoxRenzoFinFlag2.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(4, 0x02);
            checkBoxRenzoFinFlag3.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(4, 0x04);
            checkBoxRenzoFinFlag4.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(4, 0x08);
            checkBoxRenzoFinFlag5.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(4, 0x10);
            checkBoxRenzoFinFlag6.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(4, 0x20);
            checkBoxRenzoFinFlag7.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(4, 0x40);
            checkBoxRenzoFinFlag8.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(4, 0x80);
            numericUpDownRenzoFinHitCount.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(5, numericUpDownRenzoFinHitCount.Value);
            comboBoxRenzoFinElement.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(6, RenzoFin_GetElement(comboBoxRenzoFinElement.SelectedIndex));
            numericUpDownRenzoFinElementPerc.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(7, numericUpDownRenzoFinElementPerc.Value);
            numericUpDownRenzoFinStatusAttack.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(8, numericUpDownRenzoFinStatusAttack.Value);
            checkBoxRenzoFinDeath.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(9, 0x01, 0);
            checkBoxRenzoFinPoison.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(9, 0x02, 0);
            checkBoxRenzoFinPetrify.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(9, 0x04, 0);
            checkBoxRenzoFinDarkness.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(9, 0x08, 0);
            checkBoxRenzoFinSilence.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(9, 0x10, 0);
            checkBoxRenzoFinBerserk.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(9, 0x20, 0);
            checkBoxRenzoFinZombie.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(9, 0x40, 0);
            checkBoxRenzoFinUnk7.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(9, 0x80, 0);
            checkBoxRenzoFinSleep.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(9, 0x01, 1);
            checkBoxRenzoFinHaste.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(9, 0x02, 1);
            checkBoxRenzoFinSlow.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(9, 0x04, 1);
            checkBoxRenzoFinStop.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(9, 0x08, 1);
            checkBoxRenzoFinRegen.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(9, 0x10, 1);
            checkBoxRenzoFinProtect.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(9, 0x20, 1);
            checkBoxRenzoFinShell.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(9, 0x40, 1);
            checkBoxRenzoFinReflect.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(9, 0x80, 1);
            checkBoxRenzoFinAura.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(9, 0x01, 2);
            checkBoxRenzoFinCurse.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(9, 0x02, 2);
            checkBoxRenzoFinDoom.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(9, 0x04, 2);
            checkBoxRenzoFinInvincible.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(9, 0x08, 2);
            checkBoxRenzoFinPetrifying.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(9, 0x10, 2);
            checkBoxRenzoFinFloat.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(9, 0x20, 2);
            checkBoxRenzoFinConfusion.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(9, 0x40, 2);
            checkBoxRenzoFinDrain.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(9, 0x80, 2);
            checkBoxRenzoFinEject.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(9, 0x01, 3);
            checkBoxRenzoFinDouble.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(9, 0x02, 3);
            checkBoxRenzoFinTriple.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(9, 0x04, 3);
            checkBoxRenzoFinDefend.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(9, 0x08, 3);
            checkBoxRenzoFinUnk1.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(9, 0x10, 3);
            checkBoxRenzoFinUnk2.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(9, 0x20, 3);
            checkBoxRenzoFinCharged.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(9, 0x40, 3);
            checkBoxRenzoFinBackAttack.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(9, 0x80, 3);
            checkBoxRenzoFinVit0.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(9, 0x01, 4);
            checkBoxRenzoFinAngelWing.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(9, 0x02, 4);
            checkBoxRenzoFinUnk3.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(9, 0x04, 4);
            checkBoxRenzoFinUnk4.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(9, 0x08, 4);
            checkBoxRenzoFinUnk5.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(9, 0x10, 4);
            checkBoxRenzoFinUnk6.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(9, 0x20, 4);
            checkBoxRenzoFinHasMagic.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(9, 0x40, 4);
            checkBoxRenzoFinSummonGF.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(9, 0x80, 4);

            #endregion

            #region EVENT HANDLERS TEMPORARY CHARACTERS LIMIT BREAKS

            comboBoxTempCharLBMagicID.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(0, comboBoxTempCharLBMagicID.SelectedIndex);
            comboBoxTempCharLBAttackType.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(1, comboBoxTempCharLBAttackType.SelectedIndex);
            numericUpDownTempCharLBAttackPower.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(2, numericUpDownTempCharLBAttackPower.Value);
            checkBoxTempCharLBTarget1.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(3, 0x01);
            checkBoxTempCharLBTarget2.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(3, 0x02);
            checkBoxTempCharLBTarget3.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(3, 0x04);
            checkBoxTempCharLBTarget4.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(3, 0x08);
            checkBoxTempCharLBTarget5.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(3, 0x10);
            checkBoxTempCharLBTarget6.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(3, 0x20);
            checkBoxTempCharLBTarget7.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(3, 0x40);
            checkBoxTempCharLBTarget8.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(3, 0x80);
            checkBoxTempCharLBFlag1.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(4, 0x01);
            checkBoxTempCharLBFlag2.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(4, 0x02);
            checkBoxTempCharLBFlag3.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(4, 0x04);
            checkBoxTempCharLBFlag4.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(4, 0x08);
            checkBoxTempCharLBFlag5.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(4, 0x10);
            checkBoxTempCharLBFlag6.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(4, 0x20);
            checkBoxTempCharLBFlag7.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(4, 0x40);
            checkBoxTempCharLBFlag8.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(4, 0x80);
            numericUpDownTempCharLBHitCount.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(5, numericUpDownTempCharLBHitCount.Value);
            comboBoxTempCharLBElement.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(6, TempCharLB_GetElement(comboBoxTempCharLBElement.SelectedIndex));
            numericUpDownTempCharLBElementPerc.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(7, numericUpDownTempCharLBElementPerc.Value);
            numericUpDownTempCharLBStatusAttack.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(8, numericUpDownTempCharLBStatusAttack.Value);
            checkBoxTempCharLBDeath.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(9, 0x01, 0);
            checkBoxTempCharLBPoison.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(9, 0x02, 0);
            checkBoxTempCharLBPetrify.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(9, 0x04, 0);
            checkBoxTempCharLBDarkness.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(9, 0x08, 0);
            checkBoxTempCharLBSilence.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(9, 0x10, 0);
            checkBoxTempCharLBBerserk.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(9, 0x20, 0);
            checkBoxTempCharLBZombie.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(9, 0x40, 0);
            checkBoxTempCharLBUnk7.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(9, 0x80, 0);          
            checkBoxTempCharLBSleep.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(9, 0x01, 1);
            checkBoxTempCharLBHaste.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(9, 0x02, 1);
            checkBoxTempCharLBSlow.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(9, 0x04, 1);
            checkBoxTempCharLBStop.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(9, 0x08, 1);
            checkBoxTempCharLBRegen.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(9, 0x10, 1);
            checkBoxTempCharLBProtect.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(9, 0x20, 1);
            checkBoxTempCharLBShell.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(9, 0x40, 1);
            checkBoxTempCharLBReflect.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(9, 0x80, 1);
            checkBoxTempCharLBAura.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(9, 0x01, 2);
            checkBoxTempCharLBCurse.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(9, 0x02, 2);
            checkBoxTempCharLBDoom.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(9, 0x04, 2);
            checkBoxTempCharLBInvincible.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(9, 0x08, 2);
            checkBoxTempCharLBPetrifying.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(9, 0x10, 2);
            checkBoxTempCharLBFloat.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(9, 0x20, 2);
            checkBoxTempCharLBConfusion.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(9, 0x40, 2);
            checkBoxTempCharLBDrain.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(9, 0x80, 2);
            checkBoxTempCharLBEject.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(9, 0x01, 3);
            checkBoxTempCharLBDouble.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(9, 0x02, 3);
            checkBoxTempCharLBTriple.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(9, 0x04, 3);
            checkBoxTempCharLBDefend.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(9, 0x08, 3);
            checkBoxTempCharLBUnk1.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(9, 0x10, 3);
            checkBoxTempCharLBUnk2.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(9, 0x20, 3);
            checkBoxTempCharLBCharged.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(9, 0x40, 3);
            checkBoxTempCharLBBackAttack.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(9, 0x80, 3);
            checkBoxTempCharLBVit0.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(9, 0x01, 4);
            checkBoxTempCharLBAngelWing.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(9, 0x02, 4);
            checkBoxTempCharLBUnk3.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(9, 0x04, 4);
            checkBoxTempCharLBUnk4.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(9, 0x08, 4);
            checkBoxTempCharLBUnk5.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(9, 0x10, 4);
            checkBoxTempCharLBUnk6.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(9, 0x20, 4);
            checkBoxTempCharLBHasMagic.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(9, 0x40, 4);
            checkBoxTempCharLBSummonGF.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_TempCharLB(9, 0x80, 4);

            #endregion

            #region EVENT HANDLERS SHOT

            comboBoxShotMagicID.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(0, comboBoxShotMagicID.SelectedIndex);
            comboBoxShotAttackType.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(1, comboBoxShotAttackType.SelectedIndex);
            numericUpDownShotAttackPower.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(2, numericUpDownShotAttackPower.Value);
            checkBoxShotTarget1.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(3, 0x01);
            checkBoxShotTarget2.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(3, 0x02);
            checkBoxShotTarget3.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(3, 0x04);
            checkBoxShotTarget4.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(3, 0x08);
            checkBoxShotTarget5.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(3, 0x10);
            checkBoxShotTarget6.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(3, 0x20);
            checkBoxShotTarget7.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(3, 0x40);
            checkBoxShotTarget8.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(3, 0x80);
            checkBoxShotFlag1.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(4, 0x01);
            checkBoxShotFlag2.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(4, 0x02);
            checkBoxShotFlag3.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(4, 0x04);
            checkBoxShotFlag4.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(4, 0x08);
            checkBoxShotFlag5.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(4, 0x10);
            checkBoxShotFlag6.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(4, 0x20);
            checkBoxShotFlag7.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(4, 0x40);
            checkBoxShotFlag8.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(4, 0x80);
            numericUpDownShotHitCount.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(5, numericUpDownShotHitCount.Value);
            comboBoxShotElement.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(6, Shot_GetElement(comboBoxShotElement.SelectedIndex));
            numericUpDownShotElementPerc.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(7, numericUpDownShotElementPerc.Value);
            numericUpDownShotStatusAttack.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(8, numericUpDownShotStatusAttack.Value);
            checkBoxShotDeath.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(10, 0x01, 0);
            checkBoxShotPoison.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(10, 0x02, 0);
            checkBoxShotPetrify.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(10, 0x04, 0);
            checkBoxShotDarkness.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(10, 0x08, 0);
            checkBoxShotSilence.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(10, 0x10, 0);
            checkBoxShotBerserk.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(10, 0x20, 0);
            checkBoxShotZombie.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(10, 0x40, 0);
            checkBoxShotUnk7.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(10, 0x80, 0);
            comboBoxShotItem.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(8, comboBoxShotItem.SelectedIndex);
            checkBoxShotSleep.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(10, 0x01, 1);
            checkBoxShotHaste.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(10, 0x02, 1);
            checkBoxShotSlow.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(10, 0x04, 1);
            checkBoxShotStop.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(10, 0x08, 1);
            checkBoxShotRegen.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(10, 0x10, 1);
            checkBoxShotProtect.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(10, 0x20, 1);
            checkBoxShotShell.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(10, 0x40, 1);
            checkBoxShotReflect.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(10, 0x80, 1);
            checkBoxShotAura.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(10, 0x01, 2);
            checkBoxShotCurse.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(10, 0x02, 2);
            checkBoxShotDoom.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(10, 0x04, 2);
            checkBoxShotInvincible.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(10, 0x08, 2);
            checkBoxShotPetrifying.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(10, 0x10, 2);
            checkBoxShotFloat.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(10, 0x20, 2);
            checkBoxShotConfusion.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(10, 0x40, 2);
            checkBoxShotDrain.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(10, 0x80, 2);
            checkBoxShotEject.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(10, 0x01, 3);
            checkBoxShotDouble.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(10, 0x02, 3);
            checkBoxShotTriple.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(10, 0x04, 3);
            checkBoxShotDefend.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(10, 0x08, 3);
            checkBoxShotUnk1.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(10, 0x10, 3);
            checkBoxShotUnk2.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(10, 0x20, 3);
            checkBoxShotCharged.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(10, 0x40, 3);
            checkBoxShotBackAttack.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(10, 0x80, 3);
            checkBoxShotVit0.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(10, 0x01, 4);
            checkBoxShotAngelWing.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(10, 0x02, 4);
            checkBoxShotUnk3.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(10, 0x04, 4);
            checkBoxShotUnk4.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(10, 0x08, 4);
            checkBoxShotUnk5.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(10, 0x10, 4);
            checkBoxShotUnk6.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(10, 0x20, 4);
            checkBoxShotHasMagic.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(10, 0x40, 4);
            checkBoxShotSummonGF.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(10, 0x80, 4);

            #endregion

            #region EVENT HANDLERS DUEL

            comboBoxDuelMagicID.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(0, comboBoxDuelMagicID.SelectedIndex);
            comboBoxDuelAttackType.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(1, comboBoxDuelAttackType.SelectedIndex);
            numericUpDownDuelAttackPower.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(2, numericUpDownDuelAttackPower.Value);
            checkBoxDuelFlag1.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(3, 0x01);
            checkBoxDuelFlag2.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(3, 0x02);
            checkBoxDuelFlag3.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(3, 0x04);
            checkBoxDuelFlag4.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(3, 0x08);
            checkBoxDuelFlag5.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(3, 0x10);
            checkBoxDuelFlag6.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(3, 0x20);
            checkBoxDuelFlag7.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(3, 0x40);
            checkBoxDuelFlag8.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(3, 0x80);
            checkBoxDuelTarget1.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(4, 0x01);
            checkBoxDuelTarget2.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(4, 0x02);
            checkBoxDuelTarget3.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(4, 0x04);
            checkBoxDuelTarget4.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(4, 0x08);
            checkBoxDuelTarget5.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(4, 0x10);
            checkBoxDuelTarget6.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(4, 0x20);
            checkBoxDuelTarget7.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(4, 0x40);
            checkBoxDuelTarget8.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(4, 0x80);
            numericUpDownDuelHitCount.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(5, numericUpDownDuelHitCount.Value);
            comboBoxDuelElement.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(6, Duel_GetElement(comboBoxDuelElement.SelectedIndex));            
            numericUpDownDuelElementPerc.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(7, numericUpDownDuelElementPerc.Value);
            numericUpDownDuelStatusAttack.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(8, numericUpDownDuelStatusAttack.Value);

            /* not sure if correct
            checkBoxDuelFinisher.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(9, 0x0001);
            radioButtonDuelUp1.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(9, 0x0010);
            radioButtonDuelRight1.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(9, 0x0020);
            radioButtonDuelDown1.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(9, 0x0040);
            radioButtonDuelLeft1.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(9, 0x0080);
            radioButtonDuelL2_1.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(9, 0x0100);
            radioButtonDuelR2_1.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(9, 0x0200);
            radioButtonDuelL1_1.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(9, 0x0400);
            radioButtonDuelR1_1.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(9, 0x0800);
            radioButtonDuelTriangle1.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(9, 0x1000);
            radioButtonDuelCircle1.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(9, 0x2000);
            radioButtonDuelX1.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(9, 0x4000);
            radioButtonDuelSquare1.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(9, 0x8000);
            radioButtonDuelDeac1.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(9, 0xFFFF);
            radioButtonDuelUp2.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(10, 0x0010);
            radioButtonDuelRight2.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(10, 0x0020);
            radioButtonDuelDown2.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(10, 0x0040);
            radioButtonDuelLeft2.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(10, 0x0080);
            radioButtonDuelL2_2.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(10, 0x0100);
            radioButtonDuelR2_2.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(10, 0x0200);
            radioButtonDuelL1_2.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(10, 0x0400);
            radioButtonDuelR1_2.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(10, 0x0800);
            radioButtonDuelTriangle2.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(10, 0x1000);
            radioButtonDuelCircle2.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(10, 0x2000);
            radioButtonDuelX2.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(10, 0x4000);
            radioButtonDuelSquare2.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(10, 0x8000);
            radioButtonDuelDeac2.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(10, 0xFFFF);
            radioButtonDuelUp3.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(11, 0x0010);
            radioButtonDuelRight3.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(11, 0x0020);
            radioButtonDuelDown3.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(11, 0x0040);
            radioButtonDuelLeft3.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(11, 0x0080);
            radioButtonDuelL2_3.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(11, 0x0100);
            radioButtonDuelR2_3.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(11, 0x0200);
            radioButtonDuelL1_3.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(11, 0x0400);
            radioButtonDuelR1_3.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(11, 0x0800);
            radioButtonDuelTriangle3.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(11, 0x1000);
            radioButtonDuelCircle3.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(11, 0x2000);
            radioButtonDuelX3.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(11, 0x4000);
            radioButtonDuelSquare3.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(11, 0x8000);
            radioButtonDuelDeac3.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(11, 0xFFFF);
            radioButtonDuelUp4.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(12, 0x0010);
            radioButtonDuelRight4.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(12, 0x0020);
            radioButtonDuelDown4.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(12, 0x0040);
            radioButtonDuelLeft4.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(12, 0x0080);
            radioButtonDuelL2_4.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(12, 0x0100);
            radioButtonDuelR2_4.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(12, 0x0200);
            radioButtonDuelL1_4.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(12, 0x0400);
            radioButtonDuelR1_4.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(12, 0x0800);
            radioButtonDuelTriangle4.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(12, 0x1000);
            radioButtonDuelCircle4.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(12, 0x2000);
            radioButtonDuelX4.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(12, 0x4000);
            radioButtonDuelSquare4.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(12, 0x8000);
            radioButtonDuelDeac4.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(12, 0xFFFF);
            radioButtonDuelUp5.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(13, 0x0010);
            radioButtonDuelRight5.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(13, 0x0020);
            radioButtonDuelDown5.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(13, 0x0040);
            radioButtonDuelLeft5.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(13, 0x0080);
            radioButtonDuelL2_5.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(13, 0x0100);
            radioButtonDuelR2_5.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(13, 0x0200);
            radioButtonDuelL1_5.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(13, 0x0400);
            radioButtonDuelR1_5.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(13, 0x0800);
            radioButtonDuelTriangle5.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(13, 0x1000);
            radioButtonDuelCircle5.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(13, 0x2000);
            radioButtonDuelX5.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(13, 0x4000);
            radioButtonDuelSquare5.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(13, 0x8000);
            radioButtonDuelDeac5.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(13, 0xFFFF);
            */

            checkBoxDuelDeath.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(14, 0x01, 0);
            checkBoxDuelPoison.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(14, 0x02, 0);
            checkBoxDuelPetrify.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(14, 0x04, 0);
            checkBoxDuelDarkness.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(14, 0x08, 0);
            checkBoxDuelSilence.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(14, 0x10, 0);
            checkBoxDuelBerserk.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(14, 0x20, 0);
            checkBoxDuelZombie.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(14, 0x40, 0);
            checkBoxDuelUnk7.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(14, 0x80, 0);
            checkBoxDuelSleep.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(14, 0x01, 1);
            checkBoxDuelHaste.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(14, 0x02, 1);
            checkBoxDuelSlow.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(14, 0x04, 1);
            checkBoxDuelStop.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(14, 0x08, 1);
            checkBoxDuelRegen.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(14, 0x10, 1);
            checkBoxDuelProtect.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(14, 0x20, 1);
            checkBoxDuelShell.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(14, 0x40, 1);
            checkBoxDuelReflect.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(14, 0x80, 1);
            checkBoxDuelAura.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(14, 0x01, 2);
            checkBoxDuelCurse.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(14, 0x02, 2);
            checkBoxDuelDoom.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(14, 0x04, 2);
            checkBoxDuelInvincible.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(14, 0x08, 2);
            checkBoxDuelPetrifying.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(14, 0x10, 2);
            checkBoxDuelFloat.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(14, 0x20, 2);
            checkBoxDuelConfusion.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(14, 0x40, 2);
            checkBoxDuelDrain.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(14, 0x80, 2);
            checkBoxDuelEject.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(14, 0x01, 3);
            checkBoxDuelDouble.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(14, 0x02, 3);
            checkBoxDuelTriple.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(14, 0x04, 3);
            checkBoxDuelDefend.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(14, 0x08, 3);
            checkBoxDuelUnk1.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(14, 0x10, 3);
            checkBoxDuelUnk2.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(14, 0x20, 3);
            checkBoxDuelCharged.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(14, 0x40, 3);
            checkBoxDuelBackAttack.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(14, 0x80, 3);
            checkBoxDuelVit0.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(14, 0x01, 4);
            checkBoxDuelAngelWing.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(14, 0x02, 4);
            checkBoxDuelUnk3.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(14, 0x04, 4);
            checkBoxDuelUnk4.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(14, 0x08, 4);
            checkBoxDuelUnk5.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(14, 0x10, 4);
            checkBoxDuelUnk6.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(14, 0x20, 4);
            checkBoxDuelHasMagic.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(14, 0x40, 4);
            checkBoxDuelSummonGF.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(14, 0x80, 4);

            #endregion

            #region EVENT HANDLERS COMBINE

            comboBoxCombineMagicID.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(0, comboBoxCombineMagicID.SelectedIndex);
            comboBoxCombineAttackType.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(1, comboBoxCombineAttackType.SelectedIndex);
            numericUpDownCombineAttackPower.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(2, numericUpDownCombineAttackPower.Value);
            checkBoxCombineFlag1.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(3, 0x01);
            checkBoxCombineFlag2.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(3, 0x02);
            checkBoxCombineFlag3.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(3, 0x04);
            checkBoxCombineFlag4.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(3, 0x08);
            checkBoxCombineFlag5.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(3, 0x10);
            checkBoxCombineFlag6.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(3, 0x20);
            checkBoxCombineFlag7.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(3, 0x40);
            checkBoxCombineFlag8.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(3, 0x80);
            checkBoxCombineTarget1.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(4, 0x01);
            checkBoxCombineTarget2.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(4, 0x02);
            checkBoxCombineTarget3.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(4, 0x04);
            checkBoxCombineTarget4.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(4, 0x08);
            checkBoxCombineTarget5.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(4, 0x10);
            checkBoxCombineTarget6.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(4, 0x20);
            checkBoxCombineTarget7.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(4, 0x40);
            checkBoxCombineTarget8.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(4, 0x80);
            numericUpDownCombineHitCount.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(5, numericUpDownCombineHitCount.Value);
            comboBoxCombineElement.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(6, Combine_GetElement(comboBoxCombineElement.SelectedIndex));
            numericUpDownCombineElementPerc.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(7, numericUpDownCombineElementPerc.Value);
            numericUpDownCombineStatusAttack.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(8, numericUpDownCombineStatusAttack.Value);
            checkBoxCombineDeath.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(9, 0x01, 0);
            checkBoxCombinePoison.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(9, 0x02, 0);
            checkBoxCombinePetrify.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(9, 0x04, 0);
            checkBoxCombineDarkness.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(9, 0x08, 0);
            checkBoxCombineSilence.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(9, 0x10, 0);
            checkBoxCombineBerserk.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(9, 0x20, 0);
            checkBoxCombineZombie.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(9, 0x40, 0);
            checkBoxCombineUnk7.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(9, 0x80, 0);
            checkBoxCombineSleep.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(9, 0x01, 1);
            checkBoxCombineHaste.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(9, 0x02, 1);
            checkBoxCombineSlow.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(9, 0x04, 1);
            checkBoxCombineStop.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(9, 0x08, 1);
            checkBoxCombineRegen.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(9, 0x10, 1);
            checkBoxCombineProtect.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(9, 0x20, 1);
            checkBoxCombineShell.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(9, 0x40, 1);
            checkBoxCombineReflect.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(9, 0x80, 1);
            checkBoxCombineAura.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(9, 0x01, 2);
            checkBoxCombineCurse.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(9, 0x02, 2);
            checkBoxCombineDoom.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(9, 0x04, 2);
            checkBoxCombineInvincible.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(9, 0x08, 2);
            checkBoxCombinePetrifying.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(9, 0x10, 2);
            checkBoxCombineFloat.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(9, 0x20, 2);
            checkBoxCombineConfusion.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(9, 0x40, 2);
            checkBoxCombineDrain.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(9, 0x80, 2);
            checkBoxCombineEject.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(9, 0x01, 3);
            checkBoxCombineDouble.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(9, 0x02, 3);
            checkBoxCombineTriple.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(9, 0x04, 3);
            checkBoxCombineDefend.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(9, 0x08, 3);
            checkBoxCombineUnk1.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(9, 0x10, 3);
            checkBoxCombineUnk2.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(9, 0x20, 3);
            checkBoxCombineCharged.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(9, 0x40, 3);
            checkBoxCombineBackAttack.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(9, 0x80, 3);
            checkBoxCombineVit0.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(9, 0x01, 4);
            checkBoxCombineAngelWing.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(9, 0x02, 4);
            checkBoxCombineUnk3.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(9, 0x04, 4);
            checkBoxCombineUnk4.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(9, 0x08, 4);
            checkBoxCombineUnk5.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(9, 0x10, 4);
            checkBoxCombineUnk6.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(9, 0x20, 4);
            checkBoxCombineHasMagic.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(9, 0x40, 4);
            checkBoxCombineSummonGF.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Combine(9, 0x80, 4);

            #endregion

            #region EVENT HANDLERS BATTLE ITEMS

            comboBoxBattleItemsMagicID.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(0, comboBoxBattleItemsMagicID.SelectedIndex);
            comboBoxBattleItemsAttackType.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(1, comboBoxBattleItemsAttackType.SelectedIndex);
            numericUpDownBattleItemsAttackPower.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(2, numericUpDownBattleItemsAttackPower.Value);
            checkBoxBattleItemsTarget1.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(3, 0x01);
            checkBoxBattleItemsTarget2.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(3, 0x02);
            checkBoxBattleItemsTarget3.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(3, 0x04);
            checkBoxBattleItemsTarget4.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(3, 0x08);
            checkBoxBattleItemsTarget5.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(3, 0x10);
            checkBoxBattleItemsTarget6.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(3, 0x20);
            checkBoxBattleItemsTarget7.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(3, 0x40);
            checkBoxBattleItemsTarget8.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(3, 0x80);
            checkBoxBattleItemsFlag1.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(4, 0x01);
            checkBoxBattleItemsFlag2.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(4, 0x02);
            checkBoxBattleItemsFlag3.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(4, 0x04);
            checkBoxBattleItemsFlag4.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(4, 0x08);
            checkBoxBattleItemsFlag5.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(4, 0x10);
            checkBoxBattleItemsFlag6.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(4, 0x20);
            checkBoxBattleItemsFlag7.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(4, 0x40);
            checkBoxBattleItemsFlag8.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(4, 0x80);
            numericUpDownBattleItemsStatusAttack.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(5, numericUpDownBattleItemsStatusAttack.Value);
            checkBoxBattleItemsDeath.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(6, 0x01, 0);
            checkBoxBattleItemsPoison.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(6, 0x02, 0);
            checkBoxBattleItemsPetrify.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(6, 0x04, 0);
            checkBoxBattleItemsDarkness.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(6, 0x08, 0);
            checkBoxBattleItemsSilence.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(6, 0x10, 0);
            checkBoxBattleItemsBerserk.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(6, 0x20, 0);
            checkBoxBattleItemsZombie.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(6, 0x40, 0);
            checkBoxBattleItemsUnk7.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(6, 0x80, 0);
            checkBoxBattleItemsSleep.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(6, 0x01, 1);
            checkBoxBattleItemsHaste.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(6, 0x02, 1);
            checkBoxBattleItemsSlow.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(6, 0x04, 1);
            checkBoxBattleItemsStop.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(6, 0x08, 1);
            checkBoxBattleItemsRegen.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(6, 0x10, 1);
            checkBoxBattleItemsProtect.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(6, 0x20, 1);
            checkBoxBattleItemsShell.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(6, 0x40, 1);
            checkBoxBattleItemsReflect.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(6, 0x80, 1);
            checkBoxBattleItemsAura.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(6, 0x01, 2);
            checkBoxBattleItemsCurse.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(6, 0x02, 2);
            checkBoxBattleItemsDoom.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(6, 0x04, 2);
            checkBoxBattleItemsInvincible.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(6, 0x08, 2);
            checkBoxBattleItemsPetrifying.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(6, 0x10, 2);
            checkBoxBattleItemsFloat.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(6, 0x20, 2);
            checkBoxBattleItemsConfusion.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(6, 0x40, 2);
            checkBoxBattleItemsDrain.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(6, 0x80, 2);
            checkBoxBattleItemsEject.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(6, 0x01, 3);
            checkBoxBattleItemsDouble.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(6, 0x02, 3);
            checkBoxBattleItemsTriple.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(6, 0x04, 3);
            checkBoxBattleItemsDefend.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(6, 0x08, 3);
            checkBoxBattleItemsUnk1.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(6, 0x10, 3);
            checkBoxBattleItemsUnk2.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(6, 0x20, 3);
            checkBoxBattleItemsCharged.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(6, 0x40, 3);
            checkBoxBattleItemsBackAttack.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(6, 0x80, 3);
            checkBoxBattleItemsVit0.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(6, 0x01, 4);
            checkBoxBattleItemsAngelWing.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(6, 0x02, 4);
            checkBoxBattleItemsUnk3.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(6, 0x04, 4);
            checkBoxBattleItemsUnk4.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(6, 0x08, 4);
            checkBoxBattleItemsUnk5.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(6, 0x10, 4);
            checkBoxBattleItemsUnk6.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(6, 0x20, 4);
            checkBoxBattleItemsHasMagic.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(6, 0x40, 4);
            checkBoxBattleItemsSummonGF.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(6, 0x80, 4);
            numericUpDownBattleItemsHitCount.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(7, numericUpDownBattleItemsHitCount.Value);
            comboBoxBattleItemsElement.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(8, BattleItems_GetElement(comboBoxBattleItemsElement.SelectedIndex));

            #endregion

            #region EVENT HANDLERS SLOT SETS

            comboBoxSlotsMagic1.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_SlotsSets(0, comboBoxSlotsMagic1.SelectedIndex);
            numericUpDownSlotsCount1.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotsSets(1, numericUpDownSlotsCount1.Value);
            comboBoxSlotsMagic2.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_SlotsSets(2, comboBoxSlotsMagic2.SelectedIndex);
            numericUpDownSlotsCount2.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotsSets(3, numericUpDownSlotsCount2.Value);
            comboBoxSlotsMagic3.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_SlotsSets(4, comboBoxSlotsMagic3.SelectedIndex);
            numericUpDownSlotsCount3.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotsSets(5, numericUpDownSlotsCount3.Value);
            comboBoxSlotsMagic4.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_SlotsSets(6, comboBoxSlotsMagic4.SelectedIndex);
            numericUpDownSlotsCount4.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotsSets(7, numericUpDownSlotsCount4.Value);
            comboBoxSlotsMagic5.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_SlotsSets(8, comboBoxSlotsMagic5.SelectedIndex);
            numericUpDownSlotsCount5.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotsSets(9, numericUpDownSlotsCount5.Value);
            comboBoxSlotsMagic6.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_SlotsSets(10, comboBoxSlotsMagic6.SelectedIndex);
            numericUpDownSlotsCount6.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotsSets(11, numericUpDownSlotsCount6.Value);
            comboBoxSlotsMagic7.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_SlotsSets(12, comboBoxSlotsMagic7.SelectedIndex);
            numericUpDownSlotsCount7.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotsSets(13, numericUpDownSlotsCount7.Value);
            comboBoxSlotsMagic8.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_SlotsSets(14, comboBoxSlotsMagic8.SelectedIndex);
            numericUpDownSlotsCount8.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotsSets(15, numericUpDownSlotsCount8.Value);

            #endregion
        }


        #region OPEN, SAVE, CLOSE, EXIT, TOOLBAR, ABOUT

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

        #endregion

        #region CHARTS, FORMULAS, TRACKBARS, LISTBOXES SWITCH

        private void buttonCharHPFormula_Click(object sender, EventArgs e)
        {
            MessageBox.Show("HP = ((stat_magic_J_value*magic_count + stat_bonus + lvl*a - (10*lvl^2)/b +c)*percent_modifier)/100", "HP Formula");
        }

        private void buttonCharSTRFormula_Click(object sender, EventArgs e)
        {
            MessageBox.Show("STR = ((X + (stat_magic_J_value*magic_count)/100 + stat_bonus + ((lvl*a)/10 + lvl/b - (lvl*lvl)/d/2 + c)/4)*percent_modifier)/100", "STR Formula");
        }

        private void buttonCharVITFormula_Click(object sender, EventArgs e)
        {
            MessageBox.Show("VIT = ((X + (stat_magic_J_value*magic_count)/100 + stat_bonus + ((lvl*a)/10 + lvl/b - (lvl*lvl)/d/2 + c)/4)*percent_modifier)/100", "VIT Formula");
        }

        private void buttonCharMAGFormula_Click(object sender, EventArgs e)
        {
            MessageBox.Show("MAG = ((X + (stat_magic_J_value * magic_count) / 100 + stat_bonus + ((lvl * a) / 10 + lvl / b - (lvl * lvl) / d / 2 + c) / 4) * percent_modifier) / 100", "MAG Formula");
        }

        private void buttonCharSPRFormula_Click(object sender, EventArgs e)
        {
            MessageBox.Show("SPR = ((X + (stat_magic_J_value*magic_count)/100 + stat_bonus + ((lvl*a)/10 + lvl/b - (lvl*lvl)/d/2 + c)/4)*percent_modifier)/100", "SPR Formula");
        }

        private void buttonCharSPDFormula_Click(object sender, EventArgs e)
        {
            MessageBox.Show("SPD = ((X + (stat_magic_J_value*magic_count)/100 + stat_bonus + lvl/b - lvl/d + lvl*a +c)*percent_modifier)/100", "SPD Formula");
        }

        private void buttonCharLUCKFormula_Click(object sender, EventArgs e)
        {
            MessageBox.Show("LUCK = ((X + (stat_magic_J_value*magic_count)/100 + stat_bonus + lvl/b - lvl/d + lvl*a +c)*percent_modifier)/100", "LUCK Formula");
        }


        private void buttonCharEXPFormula_Click(object sender, EventArgs e)
        {
            MessageBox.Show("EXP for level x = ((lvl-1)^2*exp_b)/256 + (lvl-1)*exp_a*10", "EXP Formula");
        }

        private void buttonCharHPChart_Click(object sender, EventArgs e)
        {
            new CharChartHP().ShowDialog();
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
            labelAbStatsValueTrackBar.Text = trackBarAbStatsIncrementValue.Value + "%".ToString();
        }



        //ABILITIES LISTBOXES SWITCH
        private void tabControlAbilities_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlAbilities.SelectedIndex == 0)
            {
                listBoxAbCharacters.Visible = true;
                listBoxAbStats.Visible = false;
                listBoxAbJunction.Visible = false;
                listBoxAbCommand.Visible = false;
                listBoxAbGF.Visible = false;
                listBoxAbParty.Visible = false;
                listBoxAbMenu.Visible = false;
            }

            if (tabControlAbilities.SelectedIndex == 1)
            {
                listBoxAbCharacters.Visible = false;
                listBoxAbStats.Visible = true;
                listBoxAbJunction.Visible = false;
                listBoxAbCommand.Visible = false;
                listBoxAbGF.Visible = false;
                listBoxAbParty.Visible = false;
                listBoxAbMenu.Visible = false;
            }

            if (tabControlAbilities.SelectedIndex == 2)
            {
                listBoxAbCharacters.Visible = false;
                listBoxAbStats.Visible = false;
                listBoxAbJunction.Visible = true;
                listBoxAbCommand.Visible = false;
                listBoxAbGF.Visible = false;
                listBoxAbParty.Visible = false;
                listBoxAbMenu.Visible = false;
            }

            if (tabControlAbilities.SelectedIndex == 3)
            {
                listBoxAbCharacters.Visible = false;
                listBoxAbStats.Visible = false;
                listBoxAbJunction.Visible = false;
                listBoxAbCommand.Visible = true;
                listBoxAbGF.Visible = false;
                listBoxAbParty.Visible = false;
                listBoxAbMenu.Visible = false;
            }

            if (tabControlAbilities.SelectedIndex == 4)
            {
                listBoxAbCharacters.Visible = false;
                listBoxAbStats.Visible = false;
                listBoxAbJunction.Visible = false;
                listBoxAbCommand.Visible = false;
                listBoxAbGF.Visible = true;
                listBoxAbParty.Visible = false;
                listBoxAbMenu.Visible = false;
            }

            if (tabControlAbilities.SelectedIndex == 5)
            {
                listBoxAbCharacters.Visible = false;
                listBoxAbStats.Visible = false;
                listBoxAbJunction.Visible = false;
                listBoxAbCommand.Visible = false;
                listBoxAbGF.Visible = false;
                listBoxAbParty.Visible = true;
                listBoxAbMenu.Visible = false;
            }

            if (tabControlAbilities.SelectedIndex == 6)
            {
                listBoxAbCharacters.Visible = false;
                listBoxAbStats.Visible = false;
                listBoxAbJunction.Visible = false;
                listBoxAbCommand.Visible = false;
                listBoxAbGF.Visible = false;
                listBoxAbParty.Visible = false;
                listBoxAbMenu.Visible = true;
            }
        }


        /*DISABLE ELEMENT% WHEN NON-ELEMENTAL IS SELECTED                                    --- opted out for now
        private void comboBoxShotElement_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBoxShotElement.SelectedIndex == 8)
            {
                numericUpDownShotElementPerc.Value = 0;
                numericUpDownShotElementPerc.Enabled = false;
            }
            else
            {
                numericUpDownShotElementPerc.Enabled = true;
            }           
        }
        */
        #endregion



        #region MAGIC

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
            checkBoxMagicUnk1.Checked = (KernelWorker.GetSelectedMagicData.StatusMagic3 & 0x10) >= 1 ? true : false;
            checkBoxMagicUnk2.Checked = (KernelWorker.GetSelectedMagicData.StatusMagic3 & 0x20) >= 1 ? true : false;
            checkBoxMagicCharged.Checked = (KernelWorker.GetSelectedMagicData.StatusMagic3 & 0x40) >= 1 ? true : false;
            checkBoxMagicBackAttack.Checked = (KernelWorker.GetSelectedMagicData.StatusMagic3 & 0x80) >= 1 ? true : false;

            checkBoxMagicVit0.Checked = (KernelWorker.GetSelectedMagicData.StatusMagic4 & 0x01) >= 1 ? true : false;
            checkBoxMagicAngelWing.Checked = (KernelWorker.GetSelectedMagicData.StatusMagic4 & 0x02) >= 1 ? true : false;
            checkBoxMagicUnk3.Checked = (KernelWorker.GetSelectedMagicData.StatusMagic4 & 0x04) >= 1 ? true : false;
            checkBoxMagicUnk4.Checked = (KernelWorker.GetSelectedMagicData.StatusMagic4 & 0x08) >= 1 ? true : false;
            checkBoxMagicUnk5.Checked = (KernelWorker.GetSelectedMagicData.StatusMagic4 & 0x10) >= 1 ? true : false;
            checkBoxMagicUnk6.Checked = (KernelWorker.GetSelectedMagicData.StatusMagic4 & 0x20) >= 1 ? true : false;
            checkBoxMagicHasMagic.Checked = (KernelWorker.GetSelectedMagicData.StatusMagic4 & 0x40) >= 1 ? true : false;
            checkBoxMagicSummonGF.Checked = (KernelWorker.GetSelectedMagicData.StatusMagic4 & 0x80) >= 1 ? true : false;

            checkBoxMagicDeath.Checked = (KernelWorker.GetSelectedMagicData.StatusMagic5 & 0x01) >= 1 ? true : false;
            checkBoxMagicPoison.Checked = (KernelWorker.GetSelectedMagicData.StatusMagic5 & 0x02) >= 1 ? true : false;
            checkBoxMagicPetrify.Checked = (KernelWorker.GetSelectedMagicData.StatusMagic5 & 0x04) >= 1 ? true : false;
            checkBoxMagicDarkness.Checked = (KernelWorker.GetSelectedMagicData.StatusMagic5 & 0x08) >= 1 ? true : false;
            checkBoxMagicSilence.Checked = (KernelWorker.GetSelectedMagicData.StatusMagic5 & 0x10) >= 1 ? true : false;
            checkBoxMagicBerserk.Checked = (KernelWorker.GetSelectedMagicData.StatusMagic5 & 0x20) >= 1 ? true : false;
            checkBoxMagicZombie.Checked = (KernelWorker.GetSelectedMagicData.StatusMagic5 & 0x40) >= 1 ? true : false;
            checkBoxMagicUnk7.Checked = (KernelWorker.GetSelectedMagicData.StatusMagic5 & 0x80) >= 1 ? true : false;
        }


        private void listBoxMagic_SelectedIndexChanged(object sender, EventArgs e)
        {
            _loaded = false;
            if (KernelWorker.Kernel == null)
                return;
            KernelWorker.ReadMagic(listBoxMagic.SelectedIndex);
            try
            {
                comboBoxMagicMagicID.SelectedIndex = KernelWorker.GetSelectedMagicData.MagicID; //As in Vanilla FF8.exe: sub ESI, 1
                numericUpDownMagicSpellPower.Value = KernelWorker.GetSelectedMagicData.SpellPower;
                comboBoxMagicAttackType.SelectedIndex = KernelWorker.GetSelectedMagicData.AttackType;
                checkBoxMagicTarget1.Checked = (KernelWorker.GetSelectedMagicData.DefaultTarget & 0x01) >= 1 ? true : false;
                checkBoxMagicTarget2.Checked = (KernelWorker.GetSelectedMagicData.DefaultTarget & 0x02) >= 1 ? true : false;
                checkBoxMagicTarget3.Checked = (KernelWorker.GetSelectedMagicData.DefaultTarget & 0x04) >= 1 ? true : false;
                checkBoxMagicTarget4.Checked = (KernelWorker.GetSelectedMagicData.DefaultTarget & 0x08) >= 1 ? true : false;
                checkBoxMagicTarget5.Checked = (KernelWorker.GetSelectedMagicData.DefaultTarget & 0x10) >= 1 ? true : false;
                checkBoxMagicTarget6.Checked = (KernelWorker.GetSelectedMagicData.DefaultTarget & 0x20) >= 1 ? true : false;
                checkBoxMagicTarget7.Checked = (KernelWorker.GetSelectedMagicData.DefaultTarget & 0x40) >= 1 ? true : false;
                checkBoxMagicTarget8.Checked = (KernelWorker.GetSelectedMagicData.DefaultTarget & 0x80) >= 1 ? true : false;
                checkBoxMagicFlag1.Checked = (KernelWorker.GetSelectedMagicData.Flags & 0x01) >= 1 ? true : false;
                checkBoxMagicFlag2.Checked = (KernelWorker.GetSelectedMagicData.Flags & 0x02) >= 1 ? true : false;
                checkBoxMagicFlag3.Checked = (KernelWorker.GetSelectedMagicData.Flags & 0x04) >= 1 ? true : false;
                checkBoxMagicBreakDamageLimit.Checked = (KernelWorker.GetSelectedMagicData.Flags & 0x08) >= 1 ? true : false;
                checkBoxMagicFlag5.Checked = (KernelWorker.GetSelectedMagicData.Flags & 0x10) >= 1 ? true : false;
                checkBoxMagicFlag6.Checked = (KernelWorker.GetSelectedMagicData.Flags & 0x20) >= 1 ? true : false;
                checkBoxMagicFlag7.Checked = (KernelWorker.GetSelectedMagicData.Flags & 0x40) >= 1 ? true : false;
                checkBoxMagicFlag8.Checked = (KernelWorker.GetSelectedMagicData.Flags & 0x80) >= 1 ? true : false;
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
                StatusHoldWorker(0, KernelWorker.GetSelectedMagicData.ElemAttackEN);
                trackBarJElemAttack.Value = KernelWorker.GetSelectedMagicData.ElemAttackVAL;
                StatusHoldWorker(1, KernelWorker.GetSelectedMagicData.ElemDefenseEN);
                trackBarJElemDefense.Value = KernelWorker.GetSelectedMagicData.ElemDefenseVAL;
                StatusHoldWorker(2, KernelWorker.GetSelectedMagicData.StatusMagic1, KernelWorker.GetSelectedMagicData.StatusATKEN, KernelWorker.GetSelectedMagicData.StatusMagic2, KernelWorker.GetSelectedMagicData.StatusMagic3, KernelWorker.GetSelectedMagicData.StatusMagic4, KernelWorker.GetSelectedMagicData.StatusMagic5);
                trackBarJStatAttack.Value = KernelWorker.GetSelectedMagicData.StatusATKval;
                StatusHoldWorker(3, KernelWorker.GetSelectedMagicData.StatusMagic1, KernelWorker.GetSelectedMagicData.StatusDefEN, KernelWorker.GetSelectedMagicData.StatusMagic2, KernelWorker.GetSelectedMagicData.StatusMagic3, KernelWorker.GetSelectedMagicData.StatusMagic4, KernelWorker.GetSelectedMagicData.StatusMagic5);
                trackBarJStatDefense.Value = KernelWorker.GetSelectedMagicData.StatusDEFval;
                numericUpDownMagicStatusAttack.Value = KernelWorker.GetSelectedMagicData.StatusAttack;
                numericUpDownMagicQuezacoltComp.Value = (100 - Convert.ToDecimal(KernelWorker.GetSelectedMagicData.QuezacoltCompatibility)) / 5;
                numericUpDownMagicShivaComp.Value = (100 - Convert.ToDecimal(KernelWorker.GetSelectedMagicData.ShivaCompatibility)) / 5;
                numericUpDownMagicIfritComp.Value = (100 - Convert.ToDecimal(KernelWorker.GetSelectedMagicData.IfritCompatibility)) / 5;
                numericUpDownMagicSirenComp.Value = (100 - Convert.ToDecimal(KernelWorker.GetSelectedMagicData.SirenCompatibility)) / 5;
                numericUpDownMagicBrothersComp.Value = (100 - Convert.ToDecimal(KernelWorker.GetSelectedMagicData.BrothersCompatibility)) / 5;
                numericUpDownMagicDiablosComp.Value = (100 - Convert.ToDecimal(KernelWorker.GetSelectedMagicData.DiablosCompatibility)) / 5;
                numericUpDownMagicCarbuncleComp.Value = (100 - Convert.ToDecimal(KernelWorker.GetSelectedMagicData.CarbuncleCompatibility)) / 5;
                numericUpDownMagicLeviathanComp.Value = (100 - Convert.ToDecimal(KernelWorker.GetSelectedMagicData.LeviathanCompatibility)) / 5;
                numericUpDownMagicPandemonaComp.Value = (100 - Convert.ToDecimal(KernelWorker.GetSelectedMagicData.PandemonaCompatibility)) / 5;
                numericUpDownMagicCerberusComp.Value = (100 - Convert.ToDecimal(KernelWorker.GetSelectedMagicData.CerberusCompatibility)) / 5;
                numericUpDownMagicAlexanderComp.Value = (100 - Convert.ToDecimal(KernelWorker.GetSelectedMagicData.AlexanderCompatibility)) / 5;
                numericUpDownMagicDoomtrainComp.Value = (100 - Convert.ToDecimal(KernelWorker.GetSelectedMagicData.DoomtrainCompatibility)) / 5;
                numericUpDownMagicBahamutComp.Value = (100 - Convert.ToDecimal(KernelWorker.GetSelectedMagicData.BahamutCompatibility)) / 5;
                numericUpDownMagicCactuarComp.Value = (100 - Convert.ToDecimal(KernelWorker.GetSelectedMagicData.CactuarCompatibility)) / 5;
                numericUpDownMagicTonberryComp.Value = (100 - Convert.ToDecimal(KernelWorker.GetSelectedMagicData.TonberryCompatibility)) / 5;
                numericUpDownMagicEdenComp.Value = (100 - Convert.ToDecimal(KernelWorker.GetSelectedMagicData.EdenCompatibility)) / 5;

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
        private void StatusHoldWorker(byte State, byte Element = 0, ushort Stat = 0, byte Status2 = 0, byte Status3 = 0, byte Status4 = 0, byte Status5 = 0)
        {
            if (State == 0)
            {
                switch (Element)
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
                if ((Element & 0x01) > 0) //If Element AND 01 is bigger than 0 - Classic bitwise logic operation. :)
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
                if (Element == 0x00) //null case, no magic trick this time. (Although there is one, but it's slow)
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
                if ((Stat & 0x0400) > 0)
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
            if (State == 1)
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
            if (State == 2)
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
            if (State == 3)
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

        #endregion

        #region J-GFs

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
            checkBoxGFUnk1.Checked = (KernelWorker.GetSelectedGFData.StatusGF4 & 0x10) >= 1 ? true : false;
            checkBoxGFUnk2.Checked = (KernelWorker.GetSelectedGFData.StatusGF4 & 0x20) >= 1 ? true : false;
            checkBoxGFCharged.Checked = (KernelWorker.GetSelectedGFData.StatusGF4 & 0x40) >= 1 ? true : false;
            checkBoxGFBackAttack.Checked = (KernelWorker.GetSelectedGFData.StatusGF4 & 0x80) >= 1 ? true : false;

            checkBoxGFVit0.Checked = (KernelWorker.GetSelectedGFData.StatusGF5 & 0x01) >= 1 ? true : false;
            checkBoxGFAngelWing.Checked = (KernelWorker.GetSelectedGFData.StatusGF5 & 0x02) >= 1 ? true : false;
            checkBoxGFUnk3.Checked = (KernelWorker.GetSelectedGFData.StatusGF5 & 0x04) >= 1 ? true : false;
            checkBoxGFUnk4.Checked = (KernelWorker.GetSelectedGFData.StatusGF5 & 0x08) >= 1 ? true : false;
            checkBoxGFUnk5.Checked = (KernelWorker.GetSelectedGFData.StatusGF5 & 0x10) >= 1 ? true : false;
            checkBoxGFUnk6.Checked = (KernelWorker.GetSelectedGFData.StatusGF5 & 0x20) >= 1 ? true : false;
            checkBoxGFHasMagic.Checked = (KernelWorker.GetSelectedGFData.StatusGF5 & 0x40) >= 1 ? true : false;
            checkBoxGFSummonGF.Checked = (KernelWorker.GetSelectedGFData.StatusGF5 & 0x80) >= 1 ? true : false;

            checkBoxGFDeath.Checked = (KernelWorker.GetSelectedGFData.StatusGF1 & 0x01) >= 1 ? true : false;
            checkBoxGFPoison.Checked = (KernelWorker.GetSelectedGFData.StatusGF1 & 0x02) >= 1 ? true : false;
            checkBoxGFPetrify.Checked = (KernelWorker.GetSelectedGFData.StatusGF1 & 0x04) >= 1 ? true : false;
            checkBoxGFDarkness.Checked = (KernelWorker.GetSelectedGFData.StatusGF1 & 0x08) >= 1 ? true : false;
            checkBoxGFSilence.Checked = (KernelWorker.GetSelectedGFData.StatusGF1 & 0x10) >= 1 ? true : false;
            checkBoxGFBerserk.Checked = (KernelWorker.GetSelectedGFData.StatusGF1 & 0x20) >= 1 ? true : false;
            checkBoxGFZombie.Checked = (KernelWorker.GetSelectedGFData.StatusGF1 & 0x40) >= 1 ? true : false;
            checkBoxGFUnk7.Checked = (KernelWorker.GetSelectedGFData.StatusGF1 & 0x80) >= 1 ? true : false;
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
                comboBoxGFAttackType.SelectedIndex = KernelWorker.GetSelectedGFData.GFAttackType;
                numericUpDownGFPower.Value = KernelWorker.GetSelectedGFData.GFPower;
                checkBoxGFFlagShelled.Checked = (KernelWorker.GetSelectedGFData.GFFlags & 0x01) >= 1 ? true : false;
                checkBoxGFFlag2.Checked = (KernelWorker.GetSelectedGFData.GFFlags & 0x02) >= 1 ? true : false;
                checkBoxGFFlag3.Checked = (KernelWorker.GetSelectedGFData.GFFlags & 0x04) >= 1 ? true : false;
                checkBoxGFFlagBreakDamageLimit.Checked = (KernelWorker.GetSelectedGFData.GFFlags & 0x08) >= 1 ? true : false;
                checkBoxGFFlagReflected.Checked = (KernelWorker.GetSelectedGFData.GFFlags & 0x10) >= 1 ? true : false;
                checkBoxGFFlag6.Checked = (KernelWorker.GetSelectedGFData.GFFlags & 0x20) >= 1 ? true : false;
                checkBoxGFFlag7.Checked = (KernelWorker.GetSelectedGFData.GFFlags & 0x40) >= 1 ? true : false;
                checkBoxGFFlag8.Checked = (KernelWorker.GetSelectedGFData.GFFlags & 0x80) >= 1 ? true : false;
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
                comboBoxGFAbilityUnlock1.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbilityUnlock1;
                comboBoxGFAbilityUnlock2.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbilityUnlock2;
                comboBoxGFAbilityUnlock3.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbilityUnlock3;
                comboBoxGFAbilityUnlock4.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbilityUnlock4;
                comboBoxGFAbilityUnlock5.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbilityUnlock5;
                comboBoxGFAbilityUnlock6.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbilityUnlock6;
                comboBoxGFAbilityUnlock7.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbilityUnlock7;
                comboBoxGFAbilityUnlock8.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbilityUnlock8;
                comboBoxGFAbilityUnlock9.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbilityUnlock9;
                comboBoxGFAbilityUnlock10.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbilityUnlock10;
                comboBoxGFAbilityUnlock11.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbilityUnlock11;
                comboBoxGFAbilityUnlock12.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbilityUnlock12;
                comboBoxGFAbilityUnlock13.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbilityUnlock13;
                comboBoxGFAbilityUnlock14.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbilityUnlock14;
                comboBoxGFAbilityUnlock15.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbilityUnlock15;
                comboBoxGFAbilityUnlock16.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbilityUnlock16;
                comboBoxGFAbilityUnlock17.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbilityUnlock17;
                comboBoxGFAbilityUnlock18.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbilityUnlock18;
                comboBoxGFAbilityUnlock19.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbilityUnlock19;
                comboBoxGFAbilityUnlock20.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbilityUnlock20;
                comboBoxGFAbilityUnlock21.SelectedIndex = KernelWorker.GetSelectedGFData.GFAbilityUnlock21;
                comboBoxGFElement.SelectedIndex = GF_GetElement();
                //checkBoxGFStatus.Checked = KernelWorker.GetSelectedGFData.GFStatusEnabler > 0x00 ? true : false;
                GFStatusWorker();
                numericUpDownGFStatusAttack.Value = KernelWorker.GetSelectedGFData.GFStatusAttack;
                numericUpDownGFQuezacoltComp.Value = (100 - Convert.ToDecimal(KernelWorker.GetSelectedGFData.GFQuezacoltCompatibility)) / 5;
                numericUpDownGFShivaComp.Value = (100 - Convert.ToDecimal(KernelWorker.GetSelectedGFData.GFShivaCompatibility)) / 5;
                numericUpDownGFIfritComp.Value = (100 - Convert.ToDecimal(KernelWorker.GetSelectedGFData.GFIfritCompatibility)) / 5;
                numericUpDownGFSirenComp.Value = (100 - Convert.ToDecimal(KernelWorker.GetSelectedGFData.GFSirenCompatibility)) / 5;
                numericUpDownGFBrothersComp.Value = (100 - Convert.ToDecimal(KernelWorker.GetSelectedGFData.GFBrothersCompatibility)) / 5;
                numericUpDownGFDiablosComp.Value = (100 - Convert.ToDecimal(KernelWorker.GetSelectedGFData.GFDiablosCompatibility)) / 5;
                numericUpDownGFCarbuncleComp.Value = (100 - Convert.ToDecimal(KernelWorker.GetSelectedGFData.GFCarbuncleCompatibility)) / 5;
                numericUpDownGFLeviathanComp.Value = (100 - Convert.ToDecimal(KernelWorker.GetSelectedGFData.GFLeviathanCompatibility)) / 5;
                numericUpDownGFPandemonaComp.Value = (100 - Convert.ToDecimal(KernelWorker.GetSelectedGFData.GFPandemonaCompatibility)) / 5;
                numericUpDownGFCerberusComp.Value = (100 - Convert.ToDecimal(KernelWorker.GetSelectedGFData.GFCerberusCompatibility)) / 5;
                numericUpDownGFAlexanderComp.Value = (100 - Convert.ToDecimal(KernelWorker.GetSelectedGFData.GFAlexanderCompatibility)) / 5;
                numericUpDownGFDoomtrainComp.Value = (100 - Convert.ToDecimal(KernelWorker.GetSelectedGFData.GFDoomtrainCompatibility)) / 5;
                numericUpDownGFBahamutComp.Value = (100 - Convert.ToDecimal(KernelWorker.GetSelectedGFData.GFBahamutCompatibility)) / 5;
                numericUpDownGFCactuarComp.Value = (100 - Convert.ToDecimal(KernelWorker.GetSelectedGFData.GFCactuarCompatibility)) / 5;
                numericUpDownGFTonberryComp.Value = (100 - Convert.ToDecimal(KernelWorker.GetSelectedGFData.GFTonberryCompatibility)) / 5;
                numericUpDownGFEdenComp.Value = (100 - Convert.ToDecimal(KernelWorker.GetSelectedGFData.GFEdenCompatibility)) / 5;
            }
            catch (Exception eeException)
            {
                MessageBox.Show(eeException.ToString());
            }
            _loaded = true;
        }

        #endregion

        #region NON-J GFs ATTACK

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
            checkBoxGFAttacksUnk1.Checked = (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks3 & 0x10) >= 1 ? true : false;
            checkBoxGFAttacksUnk2.Checked = (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks3 & 0x20) >= 1 ? true : false;
            checkBoxGFAttacksCharged.Checked = (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks3 & 0x40) >= 1 ? true : false;
            checkBoxGFAttacksBackAttack.Checked = (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks3 & 0x80) >= 1 ? true : false;

            checkBoxGFAttacksVit0.Checked = (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks4 & 0x01) >= 1 ? true : false;
            checkBoxGFAttacksAngelWing.Checked = (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks4 & 0x02) >= 1 ? true : false;
            checkBoxGFAttacksUnk3.Checked = (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks4 & 0x04) >= 1 ? true : false;
            checkBoxGFAttacksUnk4.Checked = (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks4 & 0x08) >= 1 ? true : false;
            checkBoxGFAttacksUnk5.Checked = (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks4 & 0x10) >= 1 ? true : false;
            checkBoxGFAttacksUnk6.Checked = (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks4 & 0x20) >= 1 ? true : false;
            checkBoxGFAttacksHasMagic.Checked = (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks4 & 0x40) >= 1 ? true : false;
            checkBoxGFAttacksSummonGF.Checked = (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks4 & 0x80) >= 1 ? true : false;

            checkBoxGFAttacksDeath.Checked = (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks5 & 0x01) >= 1 ? true : false;
            checkBoxGFAttacksPoison.Checked = (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks5 & 0x02) >= 1 ? true : false;
            checkBoxGFAttacksPetrify.Checked = (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks5 & 0x04) >= 1 ? true : false;
            checkBoxGFAttacksDarkness.Checked = (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks5 & 0x08) >= 1 ? true : false;
            checkBoxGFAttacksSilence.Checked = (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks5 & 0x10) >= 1 ? true : false;
            checkBoxGFAttacksBerserk.Checked = (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks5 & 0x20) >= 1 ? true : false;
            checkBoxGFAttacksZombie.Checked = (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks5 & 0x40) >= 1 ? true : false;
            checkBoxGFAttacksUnk7.Checked = (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks5 & 0x80) >= 1 ? true : false;
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
                comboBoxGFAttacksAttackType.SelectedIndex = KernelWorker.GetSelectedGFAttacksData.GFAttacksAttackType;
                numericUpDownGFAttacksPower.Value = KernelWorker.GetSelectedGFAttacksData.GFAttacksPower;
                numericUpDownGFAttacksStatusAttack.Value = KernelWorker.GetSelectedGFAttacksData.GFAttacksStatus;
                checkBoxGFAttacksFlagShelled.Checked = (KernelWorker.GetSelectedGFAttacksData.GFAttacksFlags & 0x01) >= 1 ? true : false;
                checkBoxGFAttacksFlag2.Checked = (KernelWorker.GetSelectedGFAttacksData.GFAttacksFlags & 0x02) >= 1 ? true : false;
                checkBoxGFAttacksFlag3.Checked = (KernelWorker.GetSelectedGFAttacksData.GFAttacksFlags & 0x04) >= 1 ? true : false;
                checkBoxGFAttacksFlagBreakDamageLimit.Checked = (KernelWorker.GetSelectedGFAttacksData.GFAttacksFlags & 0x08) >= 1 ? true : false;
                checkBoxGFAttacksFlagReflected.Checked = (KernelWorker.GetSelectedGFAttacksData.GFAttacksFlags & 0x10) >= 1 ? true : false;
                checkBoxGFAttacksFlag6.Checked = (KernelWorker.GetSelectedGFAttacksData.GFAttacksFlags & 0x20) >= 1 ? true : false;
                checkBoxGFAttacksFlag7.Checked = (KernelWorker.GetSelectedGFAttacksData.GFAttacksFlags & 0x40) >= 1 ? true : false;
                checkBoxGFAttacksFlag8.Checked = (KernelWorker.GetSelectedGFAttacksData.GFAttacksFlags & 0x80) >= 1 ? true : false;
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

        #endregion

        #region WEAPONS

        private void RenzokukenFinishersWorker()
        {
            checkBoxWeaponsRenzoFinRough.Checked = (KernelWorker.GetSelectedWeaponsData.RenzokukenFinishers & 0x01) >= 1 ? true : false;
            checkBoxWeaponsRenzoFinFated.Checked = (KernelWorker.GetSelectedWeaponsData.RenzokukenFinishers & 0x02) >= 1 ? true : false;
            checkBoxWeaponsRenzoFinBlasting.Checked = (KernelWorker.GetSelectedWeaponsData.RenzokukenFinishers & 0x04) >= 1 ? true : false;
            checkBoxWeaponsRenzoFinLion.Checked = (KernelWorker.GetSelectedWeaponsData.RenzokukenFinishers & 0x08) >= 1 ? true : false;
        }


        private void listBoxWeapons_SelectedIndexChanged(object sender, EventArgs e)
        {
            _loaded = false;
            if (KernelWorker.Kernel == null)
                return;
            KernelWorker.ReadWeapons(listBoxWeapons.SelectedIndex);

            try
            {
                RenzokukenFinishersWorker();
                comboBoxWeaponsCharacterID.SelectedIndex = KernelWorker.GetSelectedWeaponsData.CharacterID;
                numericUpDownWeaponsAttackPower.Value = KernelWorker.GetSelectedWeaponsData.AttackPower;
                numericUpDownWeaponsHITBonus.Value = KernelWorker.GetSelectedWeaponsData.HITBonus;
                numericUpDownWeaponsSTRBonus.Value = KernelWorker.GetSelectedWeaponsData.STRBonus;
                numericUpDownWeaponsTier.Value = KernelWorker.GetSelectedWeaponsData.Tier;
            }
            catch (Exception eeeeException)
            {
                MessageBox.Show(eeeeException.ToString());
            }
            _loaded = true;
        }

        #endregion

        #region CHARACTERS

        private void listBoxCharacters_SelectedIndexChanged(object sender, EventArgs e)
        {
            _loaded = false;
            if (KernelWorker.Kernel == null)
                return;
            KernelWorker.ReadCharacters(listBoxCharacters.SelectedIndex);

            try
            {
                numericUpDownCharCrisisLevelHP.Value = KernelWorker.GetSelectedCharactersData.CrisisLevel;
                comboBoxCharGender.SelectedIndex = KernelWorker.GetSelectedCharactersData.Gender;
                numericUpDownCharLimitID.Value = KernelWorker.GetSelectedCharactersData.LimitID;
                numericUpDownCharLimitParam.Value = KernelWorker.GetSelectedCharactersData.LimitParam;
                numericUpDownCharEXP1.Value = KernelWorker.GetSelectedCharactersData.EXP1;
                numericUpDownCharEXP2.Value = KernelWorker.GetSelectedCharactersData.EXP2;
                numericUpDownCharHP1.Value = KernelWorker.GetSelectedCharactersData.HP1;
                numericUpDownCharHP2.Value = KernelWorker.GetSelectedCharactersData.HP2;
                numericUpDownCharHP3.Value = KernelWorker.GetSelectedCharactersData.HP3;
                numericUpDownCharHP4.Value = KernelWorker.GetSelectedCharactersData.HP4;
                numericUpDownCharSTR1.Value = KernelWorker.GetSelectedCharactersData.STR1;
                numericUpDownCharSTR2.Value = KernelWorker.GetSelectedCharactersData.STR2;
                numericUpDownCharSTR3.Value = KernelWorker.GetSelectedCharactersData.STR3;
                numericUpDownCharSTR4.Value = KernelWorker.GetSelectedCharactersData.STR4;
                numericUpDownCharVIT1.Value = KernelWorker.GetSelectedCharactersData.VIT1;
                numericUpDownCharVIT2.Value = KernelWorker.GetSelectedCharactersData.VIT2;
                numericUpDownCharVIT3.Value = KernelWorker.GetSelectedCharactersData.VIT3;
                numericUpDownCharVIT4.Value = KernelWorker.GetSelectedCharactersData.VIT4;
                numericUpDownCharMAG1.Value = KernelWorker.GetSelectedCharactersData.MAG1;
                numericUpDownCharMAG2.Value = KernelWorker.GetSelectedCharactersData.MAG2;
                numericUpDownCharMAG3.Value = KernelWorker.GetSelectedCharactersData.MAG3;
                numericUpDownCharMAG4.Value = KernelWorker.GetSelectedCharactersData.MAG4;
                numericUpDownCharSPR1.Value = KernelWorker.GetSelectedCharactersData.SPR1;
                numericUpDownCharSPR2.Value = KernelWorker.GetSelectedCharactersData.SPR2;
                numericUpDownCharSPR3.Value = KernelWorker.GetSelectedCharactersData.SPR3;
                numericUpDownCharSPR4.Value = KernelWorker.GetSelectedCharactersData.SPR4;
                numericUpDownCharSPD1.Value = KernelWorker.GetSelectedCharactersData.SPD1;
                numericUpDownCharSPD2.Value = KernelWorker.GetSelectedCharactersData.SPD2;
                numericUpDownCharSPD3.Value = KernelWorker.GetSelectedCharactersData.SPD3;
                numericUpDownCharSPD4.Value = KernelWorker.GetSelectedCharactersData.SPD4;
                numericUpDownCharLUCK1.Value = KernelWorker.GetSelectedCharactersData.LUCK1;
                numericUpDownCharLUCK2.Value = KernelWorker.GetSelectedCharactersData.LUCK2;
                numericUpDownCharLUCK3.Value = KernelWorker.GetSelectedCharactersData.LUCK3;
                numericUpDownCharLUCK4.Value = KernelWorker.GetSelectedCharactersData.LUCK4;
            }
            catch (Exception eeeeeException)
            {
                MessageBox.Show(eeeeeException.ToString());
            }
            _loaded = true;
        }

        #endregion

        #region ENEMY ATTACKS

        private int EnemyAttacks_GetElement()
        {
            return KernelWorker.GetSelectedEnemyAttacksData.Element == KernelWorker.Element.Fire
                        ? 0
                        : KernelWorker.GetSelectedEnemyAttacksData.Element == KernelWorker.Element.Ice
                            ? 1
                            : KernelWorker.GetSelectedEnemyAttacksData.Element == KernelWorker.Element.Thunder
                                ? 2
                                : KernelWorker.GetSelectedEnemyAttacksData.Element == KernelWorker.Element.Earth
                                    ? 3
                                    : KernelWorker.GetSelectedEnemyAttacksData.Element == KernelWorker.Element.Poison
                                        ? 4
                                        : KernelWorker.GetSelectedEnemyAttacksData.Element == KernelWorker.Element.Wind
                                            ? 5
                                            : KernelWorker.GetSelectedEnemyAttacksData.Element ==
                                              KernelWorker.Element.Water
                                                ? 6
                                                : KernelWorker.GetSelectedEnemyAttacksData.Element ==
                                                  KernelWorker.Element.Holy
                                                    ? 7
                                                    : KernelWorker.GetSelectedEnemyAttacksData.Element ==
                                                      KernelWorker.Element.NonElemental
                                                        ? comboBoxEnemyAttacksElement.Items.Count - 1
                                                        : 0;
        }

        private byte EnemyAttacks_GetElement(int Index)
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

        private void EnemyAttacksStatusWorker()
        {
            checkBoxEnemyAttacksSleep.Checked = (KernelWorker.GetSelectedEnemyAttacksData.Status2 & 0x01) >= 1 ? true : false;
            checkBoxEnemyAttacksHaste.Checked = (KernelWorker.GetSelectedEnemyAttacksData.Status2 & 0x02) >= 1 ? true : false;
            checkBoxEnemyAttacksSlow.Checked = (KernelWorker.GetSelectedEnemyAttacksData.Status2 & 0x04) >= 1 ? true : false;
            checkBoxEnemyAttacksStop.Checked = (KernelWorker.GetSelectedEnemyAttacksData.Status2 & 0x08) >= 1 ? true : false;
            checkBoxEnemyAttacksRegen.Checked = (KernelWorker.GetSelectedEnemyAttacksData.Status2 & 0x10) >= 1 ? true : false;
            checkBoxEnemyAttacksProtect.Checked = (KernelWorker.GetSelectedEnemyAttacksData.Status2 & 0x20) >= 1 ? true : false;
            checkBoxEnemyAttacksShell.Checked = (KernelWorker.GetSelectedEnemyAttacksData.Status2 & 0x40) >= 1 ? true : false;
            checkBoxEnemyAttacksReflect.Checked = (KernelWorker.GetSelectedEnemyAttacksData.Status2 & 0x80) >= 1 ? true : false;

            checkBoxEnemyAttacksAura.Checked = (KernelWorker.GetSelectedEnemyAttacksData.Status3 & 0x01) >= 1 ? true : false;
            checkBoxEnemyAttacksCurse.Checked = (KernelWorker.GetSelectedEnemyAttacksData.Status3 & 0x02) >= 1 ? true : false;
            checkBoxEnemyAttacksDoom.Checked = (KernelWorker.GetSelectedEnemyAttacksData.Status3 & 0x04) >= 1 ? true : false;
            checkBoxEnemyAttacksInvincible.Checked = (KernelWorker.GetSelectedEnemyAttacksData.Status3 & 0x08) >= 1 ? true : false;
            checkBoxEnemyAttacksPetrifying.Checked = (KernelWorker.GetSelectedEnemyAttacksData.Status3 & 0x10) >= 1 ? true : false;
            checkBoxEnemyAttacksFloat.Checked = (KernelWorker.GetSelectedEnemyAttacksData.Status3 & 0x20) >= 1 ? true : false;
            checkBoxEnemyAttacksConfusion.Checked = (KernelWorker.GetSelectedEnemyAttacksData.Status3 & 0x40) >= 1 ? true : false;
            checkBoxEnemyAttacksDrain.Checked = (KernelWorker.GetSelectedEnemyAttacksData.Status3 & 0x80) >= 1 ? true : false;

            checkBoxEnemyAttacksEject.Checked = (KernelWorker.GetSelectedEnemyAttacksData.Status4 & 0x01) >= 1 ? true : false;
            checkBoxEnemyAttacksDouble.Checked = (KernelWorker.GetSelectedEnemyAttacksData.Status4 & 0x02) >= 1 ? true : false;
            checkBoxEnemyAttacksTriple.Checked = (KernelWorker.GetSelectedEnemyAttacksData.Status4 & 0x04) >= 1 ? true : false;
            checkBoxEnemyAttacksDefend.Checked = (KernelWorker.GetSelectedEnemyAttacksData.Status4 & 0x08) >= 1 ? true : false;
            checkBoxEnemyAttacksUnk1.Checked = (KernelWorker.GetSelectedEnemyAttacksData.Status4 & 0x10) >= 1 ? true : false;
            checkBoxEnemyAttacksUnk2.Checked = (KernelWorker.GetSelectedEnemyAttacksData.Status4 & 0x20) >= 1 ? true : false;
            checkBoxEnemyAttacksCharged.Checked = (KernelWorker.GetSelectedEnemyAttacksData.Status4 & 0x40) >= 1 ? true : false;
            checkBoxEnemyAttacksBackAttack.Checked = (KernelWorker.GetSelectedEnemyAttacksData.Status4 & 0x80) >= 1 ? true : false;

            checkBoxEnemyAttacksVit0.Checked = (KernelWorker.GetSelectedEnemyAttacksData.Status5 & 0x01) >= 1 ? true : false;
            checkBoxEnemyAttacksAngelWing.Checked = (KernelWorker.GetSelectedEnemyAttacksData.Status5 & 0x02) >= 1 ? true : false;
            checkBoxEnemyAttacksUnk3.Checked = (KernelWorker.GetSelectedEnemyAttacksData.Status5 & 0x04) >= 1 ? true : false;
            checkBoxEnemyAttacksUnk4.Checked = (KernelWorker.GetSelectedEnemyAttacksData.Status5 & 0x08) >= 1 ? true : false;
            checkBoxEnemyAttacksUnk5.Checked = (KernelWorker.GetSelectedEnemyAttacksData.Status5 & 0x10) >= 1 ? true : false;
            checkBoxEnemyAttacksUnk6.Checked = (KernelWorker.GetSelectedEnemyAttacksData.Status5 & 0x20) >= 1 ? true : false;
            checkBoxEnemyAttacksHasMagic.Checked = (KernelWorker.GetSelectedEnemyAttacksData.Status5 & 0x40) >= 1 ? true : false;
            checkBoxEnemyAttacksSummonGF.Checked = (KernelWorker.GetSelectedEnemyAttacksData.Status5 & 0x80) >= 1 ? true : false;

            checkBoxEnemyAttacksDeath.Checked = (KernelWorker.GetSelectedEnemyAttacksData.Status1 & 0x01) >= 1 ? true : false;
            checkBoxEnemyAttacksPoison.Checked = (KernelWorker.GetSelectedEnemyAttacksData.Status1 & 0x02) >= 1 ? true : false;
            checkBoxEnemyAttacksPetrify.Checked = (KernelWorker.GetSelectedEnemyAttacksData.Status1 & 0x04) >= 1 ? true : false;
            checkBoxEnemyAttacksDarkness.Checked = (KernelWorker.GetSelectedEnemyAttacksData.Status1 & 0x08) >= 1 ? true : false;
            checkBoxEnemyAttacksSilence.Checked = (KernelWorker.GetSelectedEnemyAttacksData.Status1 & 0x10) >= 1 ? true : false;
            checkBoxEnemyAttacksBerserk.Checked = (KernelWorker.GetSelectedEnemyAttacksData.Status1 & 0x20) >= 1 ? true : false;
            checkBoxEnemyAttacksZombie.Checked = (KernelWorker.GetSelectedEnemyAttacksData.Status1 & 0x40) >= 1 ? true : false;
            checkBoxEnemyAttacksUnk7.Checked = (KernelWorker.GetSelectedEnemyAttacksData.Status1 & 0x80) >= 1 ? true : false;
        }


        private void listBoxEnemyAttacks_SelectedIndexChanged(object sender, EventArgs e)
        {
            _loaded = false;
            if (KernelWorker.Kernel == null)
                return;
            KernelWorker.ReadEnemyAttacks(listBoxEnemyAttacks.SelectedIndex);

            try
            {
                comboBoxEnemyAttacksMagicID.SelectedIndex = KernelWorker.GetSelectedEnemyAttacksData.MagicID;
                comboBoxEnemyAttacksAttackType.SelectedIndex = KernelWorker.GetSelectedEnemyAttacksData.AttackType;
                numericUpDownEnemyAttacksAttackPower.Value = KernelWorker.GetSelectedEnemyAttacksData.AttackPower;
                checkBoxEnemyAttacksFlagShelled.Checked = (KernelWorker.GetSelectedEnemyAttacksData.AttackFlags & 0x01) >= 1 ? true : false;
                checkBoxEnemyAttacksFlag2.Checked = (KernelWorker.GetSelectedEnemyAttacksData.AttackFlags & 0x02) >= 1 ? true : false;
                checkBoxEnemyAttacksFlag3.Checked = (KernelWorker.GetSelectedEnemyAttacksData.AttackFlags & 0x04) >= 1 ? true : false;
                checkBoxEnemyAttacksFlagBreakDamageLimit.Checked = (KernelWorker.GetSelectedEnemyAttacksData.AttackFlags & 0x08) >= 1 ? true : false;
                checkBoxEnemyAttacksFlagReflected.Checked = (KernelWorker.GetSelectedEnemyAttacksData.AttackFlags & 0x10) >= 1 ? true : false;
                checkBoxEnemyAttacksFlag6.Checked = (KernelWorker.GetSelectedEnemyAttacksData.AttackFlags & 0x20) >= 1 ? true : false;
                checkBoxEnemyAttacksFlag7.Checked = (KernelWorker.GetSelectedEnemyAttacksData.AttackFlags & 0x40) >= 1 ? true : false;
                checkBoxEnemyAttacksFlag8.Checked = (KernelWorker.GetSelectedEnemyAttacksData.AttackFlags & 0x80) >= 1 ? true : false;
                comboBoxEnemyAttacksElement.SelectedIndex = EnemyAttacks_GetElement();
                numericUpDownEnemyAttacksStatusAttack.Value = KernelWorker.GetSelectedEnemyAttacksData.StatusAttack;
                EnemyAttacksStatusWorker();
            }
            catch (Exception eeeeeeException)
            {
                MessageBox.Show(eeeeeeException.ToString());
            }
            _loaded = true;
        }

        #endregion

        #region BLUE MAGIC + PARAM

        private int BlueMagic_GetElement()
        {
            return KernelWorker.GetSelectedBlueMagicData.Element == KernelWorker.Element.Fire
                        ? 0
                        : KernelWorker.GetSelectedBlueMagicData.Element == KernelWorker.Element.Ice
                            ? 1
                            : KernelWorker.GetSelectedBlueMagicData.Element == KernelWorker.Element.Thunder
                                ? 2
                                : KernelWorker.GetSelectedBlueMagicData.Element == KernelWorker.Element.Earth
                                    ? 3
                                    : KernelWorker.GetSelectedBlueMagicData.Element == KernelWorker.Element.Poison
                                        ? 4
                                        : KernelWorker.GetSelectedBlueMagicData.Element == KernelWorker.Element.Wind
                                            ? 5
                                            : KernelWorker.GetSelectedBlueMagicData.Element ==
                                              KernelWorker.Element.Water
                                                ? 6
                                                : KernelWorker.GetSelectedBlueMagicData.Element ==
                                                  KernelWorker.Element.Holy
                                                    ? 7
                                                    : KernelWorker.GetSelectedBlueMagicData.Element ==
                                                      KernelWorker.Element.NonElemental
                                                        ? comboBoxBlueMagicElement.Items.Count - 1
                                                        : 0;
        }

        private byte BlueMagic_GetElement(int Index)
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

        private void BlueMagicParamStatusWorker()
        {
            checkBoxBlueMagicCL1Sleep.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status1CL1 & 0x01) >= 1 ? true : false;
            checkBoxBlueMagicCL1Haste.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status1CL1 & 0x02) >= 1 ? true : false;
            checkBoxBlueMagicCL1Slow.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status1CL1 & 0x04) >= 1 ? true : false;
            checkBoxBlueMagicCL1Stop.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status1CL1 & 0x08) >= 1 ? true : false;
            checkBoxBlueMagicCL1Regen.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status1CL1 & 0x10) >= 1 ? true : false;
            checkBoxBlueMagicCL1Protect.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status1CL1 & 0x20) >= 1 ? true : false;
            checkBoxBlueMagicCL1Shell.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status1CL1 & 0x40) >= 1 ? true : false;
            checkBoxBlueMagicCL1Reflect.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status1CL1 & 0x80) >= 1 ? true : false;

            checkBoxBlueMagicCL1Aura.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status2CL1 & 0x01) >= 1 ? true : false;
            checkBoxBlueMagicCL1Curse.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status2CL1 & 0x02) >= 1 ? true : false;
            checkBoxBlueMagicCL1Doom.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status2CL1 & 0x04) >= 1 ? true : false;
            checkBoxBlueMagicCL1Invincible.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status2CL1 & 0x08) >= 1 ? true : false;
            checkBoxBlueMagicCL1Petrifying.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status2CL1 & 0x10) >= 1 ? true : false;
            checkBoxBlueMagicCL1Float.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status2CL1 & 0x20) >= 1 ? true : false;
            checkBoxBlueMagicCL1Confusion.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status2CL1 & 0x40) >= 1 ? true : false;
            checkBoxBlueMagicCL1Drain.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status2CL1 & 0x80) >= 1 ? true : false;

            checkBoxBlueMagicCL1Eject.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status3CL1 & 0x01) >= 1 ? true : false;
            checkBoxBlueMagicCL1Double.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status3CL1 & 0x02) >= 1 ? true : false;
            checkBoxBlueMagicCL1Triple.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status3CL1 & 0x04) >= 1 ? true : false;
            checkBoxBlueMagicCL1Defend.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status3CL1 & 0x08) >= 1 ? true : false;
            checkBoxBlueMagicCL1Unk1.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status3CL1 & 0x10) >= 1 ? true : false;
            checkBoxBlueMagicCL1Unk2.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status3CL1 & 0x20) >= 1 ? true : false;
            checkBoxBlueMagicCL1Charged.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status3CL1 & 0x40) >= 1 ? true : false;
            checkBoxBlueMagicCL1BackAttack.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status3CL1 & 0x80) >= 1 ? true : false;

            checkBoxBlueMagicCL1Vit0.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status4CL1 & 0x01) >= 1 ? true : false;
            checkBoxBlueMagicCL1AngelWing.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status4CL1 & 0x02) >= 1 ? true : false;
            checkBoxBlueMagicCL1Unk3.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status4CL1 & 0x04) >= 1 ? true : false;
            checkBoxBlueMagicCL1Unk4.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status4CL1 & 0x08) >= 1 ? true : false;
            checkBoxBlueMagicCL1Unk5.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status4CL1 & 0x10) >= 1 ? true : false;
            checkBoxBlueMagicCL1Unk6.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status4CL1 & 0x20) >= 1 ? true : false;
            checkBoxBlueMagicCL1HasMagic.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status4CL1 & 0x40) >= 1 ? true : false;
            checkBoxBlueMagicCL1SummonGF.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status4CL1 & 0x80) >= 1 ? true : false;

            checkBoxBlueMagicCL1Death.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status5CL1 & 0x01) >= 1 ? true : false;
            checkBoxBlueMagicCL1Poison.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status5CL1 & 0x02) >= 1 ? true : false;
            checkBoxBlueMagicCL1Petrify.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status5CL1 & 0x04) >= 1 ? true : false;
            checkBoxBlueMagicCL1Darkness.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status5CL1 & 0x08) >= 1 ? true : false;
            checkBoxBlueMagicCL1Silence.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status5CL1 & 0x10) >= 1 ? true : false;
            checkBoxBlueMagicCL1Berserk.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status5CL1 & 0x20) >= 1 ? true : false;
            checkBoxBlueMagicCL1Zombie.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status5CL1 & 0x40) >= 1 ? true : false;
            checkBoxBlueMagicCL1Unk7.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status5CL1 & 0x80) >= 1 ? true : false;


            checkBoxBlueMagicCL2Sleep.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status1CL2 & 0x01) >= 1 ? true : false;
            checkBoxBlueMagicCL2Haste.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status1CL2 & 0x02) >= 1 ? true : false;
            checkBoxBlueMagicCL2Slow.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status1CL2 & 0x04) >= 1 ? true : false;
            checkBoxBlueMagicCL2Stop.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status1CL2 & 0x08) >= 1 ? true : false;
            checkBoxBlueMagicCL2Regen.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status1CL2 & 0x10) >= 1 ? true : false;
            checkBoxBlueMagicCL2Protect.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status1CL2 & 0x20) >= 1 ? true : false;
            checkBoxBlueMagicCL2Shell.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status1CL2 & 0x40) >= 1 ? true : false;
            checkBoxBlueMagicCL2Reflect.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status1CL2 & 0x80) >= 1 ? true : false;

            checkBoxBlueMagicCL2Aura.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status2CL2 & 0x01) >= 1 ? true : false;
            checkBoxBlueMagicCL2Curse.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status2CL2 & 0x02) >= 1 ? true : false;
            checkBoxBlueMagicCL2Doom.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status2CL2 & 0x04) >= 1 ? true : false;
            checkBoxBlueMagicCL2Invincible.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status2CL2 & 0x08) >= 1 ? true : false;
            checkBoxBlueMagicCL2Petrifying.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status2CL2 & 0x10) >= 1 ? true : false;
            checkBoxBlueMagicCL2Float.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status2CL2 & 0x20) >= 1 ? true : false;
            checkBoxBlueMagicCL2Confusion.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status2CL2 & 0x40) >= 1 ? true : false;
            checkBoxBlueMagicCL2Drain.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status2CL2 & 0x80) >= 1 ? true : false;

            checkBoxBlueMagicCL2Eject.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status3CL2 & 0x01) >= 1 ? true : false;
            checkBoxBlueMagicCL2Double.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status3CL2 & 0x02) >= 1 ? true : false;
            checkBoxBlueMagicCL2Triple.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status3CL2 & 0x04) >= 1 ? true : false;
            checkBoxBlueMagicCL2Defend.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status3CL2 & 0x08) >= 1 ? true : false;
            checkBoxBlueMagicCL2Unk1.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status3CL2 & 0x10) >= 1 ? true : false;
            checkBoxBlueMagicCL2Unk2.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status3CL2 & 0x20) >= 1 ? true : false;
            checkBoxBlueMagicCL2Charged.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status3CL2 & 0x40) >= 1 ? true : false;
            checkBoxBlueMagicCL2BackAttack.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status3CL2 & 0x80) >= 1 ? true : false;

            checkBoxBlueMagicCL2Vit0.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status4CL2 & 0x01) >= 1 ? true : false;
            checkBoxBlueMagicCL2AngelWing.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status4CL2 & 0x02) >= 1 ? true : false;
            checkBoxBlueMagicCL2Unk3.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status4CL2 & 0x04) >= 1 ? true : false;
            checkBoxBlueMagicCL2Unk4.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status4CL2 & 0x08) >= 1 ? true : false;
            checkBoxBlueMagicCL2Unk5.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status4CL2 & 0x10) >= 1 ? true : false;
            checkBoxBlueMagicCL2Unk6.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status4CL2 & 0x20) >= 1 ? true : false;
            checkBoxBlueMagicCL2HasMagic.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status4CL2 & 0x40) >= 1 ? true : false;
            checkBoxBlueMagicCL2SummonGF.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status4CL2 & 0x80) >= 1 ? true : false;

            checkBoxBlueMagicCL2Death.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status5CL2 & 0x01) >= 1 ? true : false;
            checkBoxBlueMagicCL2Poison.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status5CL2 & 0x02) >= 1 ? true : false;
            checkBoxBlueMagicCL2Petrify.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status5CL2 & 0x04) >= 1 ? true : false;
            checkBoxBlueMagicCL2Darkness.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status5CL2 & 0x08) >= 1 ? true : false;
            checkBoxBlueMagicCL2Silence.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status5CL2 & 0x10) >= 1 ? true : false;
            checkBoxBlueMagicCL2Berserk.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status5CL2 & 0x20) >= 1 ? true : false;
            checkBoxBlueMagicCL2Zombie.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status5CL2 & 0x40) >= 1 ? true : false;
            checkBoxBlueMagicCL2Unk7.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status5CL2 & 0x80) >= 1 ? true : false;


            checkBoxBlueMagicCL3Sleep.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status1CL3 & 0x01) >= 1 ? true : false;
            checkBoxBlueMagicCL3Haste.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status1CL3 & 0x02) >= 1 ? true : false;
            checkBoxBlueMagicCL3Slow.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status1CL3 & 0x04) >= 1 ? true : false;
            checkBoxBlueMagicCL3Stop.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status1CL3 & 0x08) >= 1 ? true : false;
            checkBoxBlueMagicCL3Regen.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status1CL3 & 0x10) >= 1 ? true : false;
            checkBoxBlueMagicCL3Protect.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status1CL3 & 0x20) >= 1 ? true : false;
            checkBoxBlueMagicCL3Shell.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status1CL3 & 0x40) >= 1 ? true : false;
            checkBoxBlueMagicCL3Reflect.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status1CL3 & 0x80) >= 1 ? true : false;

            checkBoxBlueMagicCL3Aura.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status2CL3 & 0x01) >= 1 ? true : false;
            checkBoxBlueMagicCL3Curse.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status2CL3 & 0x02) >= 1 ? true : false;
            checkBoxBlueMagicCL3Doom.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status2CL3 & 0x04) >= 1 ? true : false;
            checkBoxBlueMagicCL3Invincible.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status2CL3 & 0x08) >= 1 ? true : false;
            checkBoxBlueMagicCL3Petrifying.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status2CL3 & 0x10) >= 1 ? true : false;
            checkBoxBlueMagicCL3Float.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status2CL3 & 0x20) >= 1 ? true : false;
            checkBoxBlueMagicCL3Confusion.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status2CL3 & 0x40) >= 1 ? true : false;
            checkBoxBlueMagicCL3Drain.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status2CL3 & 0x80) >= 1 ? true : false;

            checkBoxBlueMagicCL3Eject.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status3CL3 & 0x01) >= 1 ? true : false;
            checkBoxBlueMagicCL3Double.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status3CL3 & 0x02) >= 1 ? true : false;
            checkBoxBlueMagicCL3Triple.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status3CL3 & 0x04) >= 1 ? true : false;
            checkBoxBlueMagicCL3Defend.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status3CL3 & 0x08) >= 1 ? true : false;
            checkBoxBlueMagicCL3Unk1.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status3CL3 & 0x10) >= 1 ? true : false;
            checkBoxBlueMagicCL3Unk2.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status3CL3 & 0x20) >= 1 ? true : false;
            checkBoxBlueMagicCL3Charged.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status3CL3 & 0x40) >= 1 ? true : false;
            checkBoxBlueMagicCL3BackAttack.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status3CL3 & 0x80) >= 1 ? true : false;

            checkBoxBlueMagicCL3Vit0.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status4CL3 & 0x01) >= 1 ? true : false;
            checkBoxBlueMagicCL3AngelWing.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status4CL3 & 0x02) >= 1 ? true : false;
            checkBoxBlueMagicCL3Unk3.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status4CL3 & 0x04) >= 1 ? true : false;
            checkBoxBlueMagicCL3Unk4.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status4CL3 & 0x08) >= 1 ? true : false;
            checkBoxBlueMagicCL3Unk5.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status4CL3 & 0x10) >= 1 ? true : false;
            checkBoxBlueMagicCL3Unk6.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status4CL3 & 0x20) >= 1 ? true : false;
            checkBoxBlueMagicCL3HasMagic.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status4CL3 & 0x40) >= 1 ? true : false;
            checkBoxBlueMagicCL3SummonGF.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status4CL3 & 0x80) >= 1 ? true : false;

            checkBoxBlueMagicCL3Death.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status5CL3 & 0x01) >= 1 ? true : false;
            checkBoxBlueMagicCL3Poison.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status5CL3 & 0x02) >= 1 ? true : false;
            checkBoxBlueMagicCL3Petrify.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status5CL3 & 0x04) >= 1 ? true : false;
            checkBoxBlueMagicCL3Darkness.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status5CL3 & 0x08) >= 1 ? true : false;
            checkBoxBlueMagicCL3Silence.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status5CL3 & 0x10) >= 1 ? true : false;
            checkBoxBlueMagicCL3Berserk.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status5CL3 & 0x20) >= 1 ? true : false;
            checkBoxBlueMagicCL3Zombie.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status5CL3 & 0x40) >= 1 ? true : false;
            checkBoxBlueMagicCL3Unk7.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status5CL3 & 0x80) >= 1 ? true : false;


            checkBoxBlueMagicCL4Sleep.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status1CL4 & 0x01) >= 1 ? true : false;
            checkBoxBlueMagicCL4Haste.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status1CL4 & 0x02) >= 1 ? true : false;
            checkBoxBlueMagicCL4Slow.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status1CL4 & 0x04) >= 1 ? true : false;
            checkBoxBlueMagicCL4Stop.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status1CL4 & 0x08) >= 1 ? true : false;
            checkBoxBlueMagicCL4Regen.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status1CL4 & 0x10) >= 1 ? true : false;
            checkBoxBlueMagicCL4Protect.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status1CL4 & 0x20) >= 1 ? true : false;
            checkBoxBlueMagicCL4Shell.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status1CL4 & 0x40) >= 1 ? true : false;
            checkBoxBlueMagicCL4Reflect.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status1CL4 & 0x80) >= 1 ? true : false;

            checkBoxBlueMagicCL4Aura.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status2CL4 & 0x01) >= 1 ? true : false;
            checkBoxBlueMagicCL4Curse.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status2CL4 & 0x02) >= 1 ? true : false;
            checkBoxBlueMagicCL4Doom.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status2CL4 & 0x04) >= 1 ? true : false;
            checkBoxBlueMagicCL4Invincible.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status2CL4 & 0x08) >= 1 ? true : false;
            checkBoxBlueMagicCL4Petrifying.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status2CL4 & 0x10) >= 1 ? true : false;
            checkBoxBlueMagicCL4Float.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status2CL4 & 0x20) >= 1 ? true : false;
            checkBoxBlueMagicCL4Confusion.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status2CL4 & 0x40) >= 1 ? true : false;
            checkBoxBlueMagicCL4Drain.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status2CL4 & 0x80) >= 1 ? true : false;

            checkBoxBlueMagicCL4Eject.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status3CL4 & 0x01) >= 1 ? true : false;
            checkBoxBlueMagicCL4Double.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status3CL4 & 0x02) >= 1 ? true : false;
            checkBoxBlueMagicCL4Triple.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status3CL4 & 0x04) >= 1 ? true : false;
            checkBoxBlueMagicCL4Defend.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status3CL4 & 0x08) >= 1 ? true : false;
            checkBoxBlueMagicCL4Unk1.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status3CL4 & 0x10) >= 1 ? true : false;
            checkBoxBlueMagicCL4Unk2.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status3CL4 & 0x20) >= 1 ? true : false;
            checkBoxBlueMagicCL4Charged.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status3CL4 & 0x40) >= 1 ? true : false;
            checkBoxBlueMagicCL4BackAttack.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status3CL4 & 0x80) >= 1 ? true : false;

            checkBoxBlueMagicCL4Vit0.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status4CL4 & 0x01) >= 1 ? true : false;
            checkBoxBlueMagicCL4AngelWing.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status4CL4 & 0x02) >= 1 ? true : false;
            checkBoxBlueMagicCL4Unk3.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status4CL4 & 0x04) >= 1 ? true : false;
            checkBoxBlueMagicCL4Unk4.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status4CL4 & 0x08) >= 1 ? true : false;
            checkBoxBlueMagicCL4Unk5.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status4CL4 & 0x10) >= 1 ? true : false;
            checkBoxBlueMagicCL4Unk6.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status4CL4 & 0x20) >= 1 ? true : false;
            checkBoxBlueMagicCL4HasMagic.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status4CL4 & 0x40) >= 1 ? true : false;
            checkBoxBlueMagicCL4SummonGF.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status4CL4 & 0x80) >= 1 ? true : false;

            checkBoxBlueMagicCL4Death.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status5CL4 & 0x01) >= 1 ? true : false;
            checkBoxBlueMagicCL4Poison.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status5CL4 & 0x02) >= 1 ? true : false;
            checkBoxBlueMagicCL4Petrify.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status5CL4 & 0x04) >= 1 ? true : false;
            checkBoxBlueMagicCL4Darkness.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status5CL4 & 0x08) >= 1 ? true : false;
            checkBoxBlueMagicCL4Silence.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status5CL4 & 0x10) >= 1 ? true : false;
            checkBoxBlueMagicCL4Berserk.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status5CL4 & 0x20) >= 1 ? true : false;
            checkBoxBlueMagicCL4Zombie.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status5CL4 & 0x40) >= 1 ? true : false;
            checkBoxBlueMagicCL4Unk7.Checked = (KernelWorker.GetSelectedBlueMagicParamData.Status5CL4 & 0x80) >= 1 ? true : false;
        }

        private void listBoxBlueMagic_SelectedIndexChanged(object sender, EventArgs e)
        {
            _loaded = false;
            if (KernelWorker.Kernel == null)
                return;

            KernelWorker.ReadBlueMagic(listBoxBlueMagic.SelectedIndex);
            KernelWorker.ReadBlueMagicParam(listBoxBlueMagic.SelectedIndex);

            try
            {
                comboBoxBlueMagicMagicID.SelectedIndex = KernelWorker.GetSelectedBlueMagicData.MagicID;
                comboBoxBlueMagicAttackType.SelectedIndex = KernelWorker.GetSelectedBlueMagicData.AttackType;
                checkBoxBlueMagicFlag1.Checked = (KernelWorker.GetSelectedBlueMagicData.AttackFlags & 0x01) >= 1 ? true : false;
                checkBoxBlueMagicFlag2.Checked = (KernelWorker.GetSelectedBlueMagicData.AttackFlags & 0x02) >= 1 ? true : false;
                checkBoxBlueMagicFlag3.Checked = (KernelWorker.GetSelectedBlueMagicData.AttackFlags & 0x04) >= 1 ? true : false;
                checkBoxBlueMagicFlag4.Checked = (KernelWorker.GetSelectedBlueMagicData.AttackFlags & 0x08) >= 1 ? true : false;
                checkBoxBlueMagicFlag5.Checked = (KernelWorker.GetSelectedBlueMagicData.AttackFlags & 0x10) >= 1 ? true : false;
                checkBoxBlueMagicFlag6.Checked = (KernelWorker.GetSelectedBlueMagicData.AttackFlags & 0x20) >= 1 ? true : false;
                checkBoxBlueMagicFlag7.Checked = (KernelWorker.GetSelectedBlueMagicData.AttackFlags & 0x40) >= 1 ? true : false;
                checkBoxBlueMagicFlag8.Checked = (KernelWorker.GetSelectedBlueMagicData.AttackFlags & 0x80) >= 1 ? true : false;
                comboBoxBlueMagicElement.SelectedIndex = BlueMagic_GetElement();
                numericUpDownBlueMagicStatusAttack.Value = KernelWorker.GetSelectedBlueMagicData.StatusAttack;


                BlueMagicParamStatusWorker();

                numericUpDownBlueMagicCL1AttackPower.Value = KernelWorker.GetSelectedBlueMagicParamData.AttackPowerCL1;
                numericUpDownBlueMagicCL1DeathLevel.Value = KernelWorker.GetSelectedBlueMagicParamData.DeathLevelCL1;

                numericUpDownBlueMagicCL2AttackPower.Value = KernelWorker.GetSelectedBlueMagicParamData.AttackPowerCL2;
                numericUpDownBlueMagicCL2DeathLevel.Value = KernelWorker.GetSelectedBlueMagicParamData.DeathLevelCL2;

                numericUpDownBlueMagicCL3AttackPower.Value = KernelWorker.GetSelectedBlueMagicParamData.AttackPowerCL3;
                numericUpDownBlueMagicCL3DeathLevel.Value = KernelWorker.GetSelectedBlueMagicParamData.DeathLevelCL3;

                numericUpDownBlueMagicCL4AttackPower.Value = KernelWorker.GetSelectedBlueMagicParamData.AttackPowerCL4;
                numericUpDownBlueMagicCL4DeathLevel.Value = KernelWorker.GetSelectedBlueMagicParamData.DeathLevelCL4;
            }

            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }
            _loaded = true;         
        }

        #endregion

        #region STATS INCREASE ABILITIES

        private void listBoxAbStats_SelectedIndexChanged(object sender, EventArgs e)
        {
            _loaded = false;
            if (KernelWorker.Kernel == null)
                return;
            KernelWorker.ReadStatPercentageAbilities(listBoxAbStats.SelectedIndex);

            try
            {
                comboBoxAbStatsStatToIncrease.SelectedIndex = KernelWorker.GetSelectedStatPercentageAbilitiesData.StatToincrease;
                trackBarAbStatsIncrementValue.Value = KernelWorker.GetSelectedStatPercentageAbilitiesData.IncreasementValue;
            }
            catch (Exception eeeeeeeException)
            {
                Console.WriteLine(eeeeeeeException.ToString());
            }
            _loaded = true;
        }

        #endregion

        #region RENZOKUKEN FINISHERS

        private int RenzoFin_GetElement()
        {
            return KernelWorker.GetSelectedRenzoFinData.Element == KernelWorker.Element.Fire
                        ? 0
                        : KernelWorker.GetSelectedRenzoFinData.Element == KernelWorker.Element.Ice
                            ? 1
                            : KernelWorker.GetSelectedRenzoFinData.Element == KernelWorker.Element.Thunder
                                ? 2
                                : KernelWorker.GetSelectedRenzoFinData.Element == KernelWorker.Element.Earth
                                    ? 3
                                    : KernelWorker.GetSelectedRenzoFinData.Element == KernelWorker.Element.Poison
                                        ? 4
                                        : KernelWorker.GetSelectedRenzoFinData.Element == KernelWorker.Element.Wind
                                            ? 5
                                            : KernelWorker.GetSelectedRenzoFinData.Element ==
                                              KernelWorker.Element.Water
                                                ? 6
                                                : KernelWorker.GetSelectedRenzoFinData.Element ==
                                                  KernelWorker.Element.Holy
                                                    ? 7
                                                    : KernelWorker.GetSelectedRenzoFinData.Element ==
                                                      KernelWorker.Element.NonElemental
                                                        ? comboBoxRenzoFinElement.Items.Count - 1
                                                        : 0;
        }

        private byte RenzoFin_GetElement(int Index)
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

        private void RenzoFinStatusWorker()
        {
            checkBoxRenzoFinDeath.Checked = (KernelWorker.GetSelectedRenzoFinData.Status1 & 0x01) >= 1 ? true : false;
            checkBoxRenzoFinPoison.Checked = (KernelWorker.GetSelectedRenzoFinData.Status1 & 0x02) >= 1 ? true : false;
            checkBoxRenzoFinPetrify.Checked = (KernelWorker.GetSelectedRenzoFinData.Status1 & 0x04) >= 1 ? true : false;
            checkBoxRenzoFinDarkness.Checked = (KernelWorker.GetSelectedRenzoFinData.Status1 & 0x08) >= 1 ? true : false;
            checkBoxRenzoFinSilence.Checked = (KernelWorker.GetSelectedRenzoFinData.Status1 & 0x10) >= 1 ? true : false;
            checkBoxRenzoFinBerserk.Checked = (KernelWorker.GetSelectedRenzoFinData.Status1 & 0x20) >= 1 ? true : false;
            checkBoxRenzoFinZombie.Checked = (KernelWorker.GetSelectedRenzoFinData.Status1 & 0x40) >= 1 ? true : false;
            checkBoxRenzoFinUnk7.Checked = (KernelWorker.GetSelectedRenzoFinData.Status1 & 0x80) >= 1 ? true : false;

            checkBoxRenzoFinSleep.Checked = (KernelWorker.GetSelectedRenzoFinData.Status2 & 0x01) >= 1 ? true : false;
            checkBoxRenzoFinHaste.Checked = (KernelWorker.GetSelectedRenzoFinData.Status2 & 0x02) >= 1 ? true : false;
            checkBoxRenzoFinSlow.Checked = (KernelWorker.GetSelectedRenzoFinData.Status2 & 0x04) >= 1 ? true : false;
            checkBoxRenzoFinStop.Checked = (KernelWorker.GetSelectedRenzoFinData.Status2 & 0x08) >= 1 ? true : false;
            checkBoxRenzoFinRegen.Checked = (KernelWorker.GetSelectedRenzoFinData.Status2 & 0x10) >= 1 ? true : false;
            checkBoxRenzoFinProtect.Checked = (KernelWorker.GetSelectedRenzoFinData.Status2 & 0x20) >= 1 ? true : false;
            checkBoxRenzoFinShell.Checked = (KernelWorker.GetSelectedRenzoFinData.Status2 & 0x40) >= 1 ? true : false;
            checkBoxRenzoFinReflect.Checked = (KernelWorker.GetSelectedRenzoFinData.Status2 & 0x80) >= 1 ? true : false;

            checkBoxRenzoFinAura.Checked = (KernelWorker.GetSelectedRenzoFinData.Status3 & 0x01) >= 1 ? true : false;
            checkBoxRenzoFinCurse.Checked = (KernelWorker.GetSelectedRenzoFinData.Status3 & 0x02) >= 1 ? true : false;
            checkBoxRenzoFinDoom.Checked = (KernelWorker.GetSelectedRenzoFinData.Status3 & 0x04) >= 1 ? true : false;
            checkBoxRenzoFinInvincible.Checked = (KernelWorker.GetSelectedRenzoFinData.Status3 & 0x08) >= 1 ? true : false;
            checkBoxRenzoFinPetrifying.Checked = (KernelWorker.GetSelectedRenzoFinData.Status3 & 0x10) >= 1 ? true : false;
            checkBoxRenzoFinFloat.Checked = (KernelWorker.GetSelectedRenzoFinData.Status3 & 0x20) >= 1 ? true : false;
            checkBoxRenzoFinConfusion.Checked = (KernelWorker.GetSelectedRenzoFinData.Status3 & 0x40) >= 1 ? true : false;
            checkBoxRenzoFinDrain.Checked = (KernelWorker.GetSelectedRenzoFinData.Status3 & 0x80) >= 1 ? true : false;

            checkBoxRenzoFinEject.Checked = (KernelWorker.GetSelectedRenzoFinData.Status4 & 0x01) >= 1 ? true : false;
            checkBoxRenzoFinDouble.Checked = (KernelWorker.GetSelectedRenzoFinData.Status4 & 0x02) >= 1 ? true : false;
            checkBoxRenzoFinTriple.Checked = (KernelWorker.GetSelectedRenzoFinData.Status4 & 0x04) >= 1 ? true : false;
            checkBoxRenzoFinDefend.Checked = (KernelWorker.GetSelectedRenzoFinData.Status4 & 0x08) >= 1 ? true : false;
            checkBoxRenzoFinUnk1.Checked = (KernelWorker.GetSelectedRenzoFinData.Status4 & 0x10) >= 1 ? true : false;
            checkBoxRenzoFinUnk2.Checked = (KernelWorker.GetSelectedRenzoFinData.Status4 & 0x20) >= 1 ? true : false;
            checkBoxRenzoFinCharged.Checked = (KernelWorker.GetSelectedRenzoFinData.Status4 & 0x40) >= 1 ? true : false;
            checkBoxRenzoFinBackAttack.Checked = (KernelWorker.GetSelectedRenzoFinData.Status4 & 0x80) >= 1 ? true : false;

            checkBoxRenzoFinVit0.Checked = (KernelWorker.GetSelectedRenzoFinData.Status5 & 0x01) >= 1 ? true : false;
            checkBoxRenzoFinAngelWing.Checked = (KernelWorker.GetSelectedRenzoFinData.Status5 & 0x02) >= 1 ? true : false;
            checkBoxRenzoFinUnk3.Checked = (KernelWorker.GetSelectedRenzoFinData.Status5 & 0x04) >= 1 ? true : false;
            checkBoxRenzoFinUnk4.Checked = (KernelWorker.GetSelectedRenzoFinData.Status5 & 0x08) >= 1 ? true : false;
            checkBoxRenzoFinUnk5.Checked = (KernelWorker.GetSelectedRenzoFinData.Status5 & 0x10) >= 1 ? true : false;
            checkBoxRenzoFinUnk6.Checked = (KernelWorker.GetSelectedRenzoFinData.Status5 & 0x20) >= 1 ? true : false;
            checkBoxRenzoFinHasMagic.Checked = (KernelWorker.GetSelectedRenzoFinData.Status5 & 0x40) >= 1 ? true : false;
            checkBoxRenzoFinSummonGF.Checked = (KernelWorker.GetSelectedRenzoFinData.Status5 & 0x80) >= 1 ? true : false;
        }

        private void listBoxRenzoFin_SelectedIndexChanged(object sender, EventArgs e)
        {
            _loaded = false;
            if (KernelWorker.Kernel == null)
                return;
            KernelWorker.ReadRenzoFin(listBoxRenzoFin.SelectedIndex);
            try
            {
                comboBoxRenzoFinMagicID.SelectedIndex = KernelWorker.GetSelectedRenzoFinData.MagicID;                
                comboBoxRenzoFinAttackType.SelectedIndex = KernelWorker.GetSelectedRenzoFinData.AttackType;
                numericUpDownRenzoFinAttackPower.Value = KernelWorker.GetSelectedRenzoFinData.AttackPower;
                checkBoxRenzoFinTarget1.Checked = (KernelWorker.GetSelectedRenzoFinData.Target & 0x01) >= 1 ? true : false;
                checkBoxRenzoFinTarget2.Checked = (KernelWorker.GetSelectedRenzoFinData.Target & 0x02) >= 1 ? true : false;
                checkBoxRenzoFinTarget3.Checked = (KernelWorker.GetSelectedRenzoFinData.Target & 0x04) >= 1 ? true : false;
                checkBoxRenzoFinTarget4.Checked = (KernelWorker.GetSelectedRenzoFinData.Target & 0x08) >= 1 ? true : false;
                checkBoxRenzoFinTarget5.Checked = (KernelWorker.GetSelectedRenzoFinData.Target & 0x10) >= 1 ? true : false;
                checkBoxRenzoFinTarget6.Checked = (KernelWorker.GetSelectedRenzoFinData.Target & 0x20) >= 1 ? true : false;
                checkBoxRenzoFinTarget7.Checked = (KernelWorker.GetSelectedRenzoFinData.Target & 0x40) >= 1 ? true : false;
                checkBoxRenzoFinTarget8.Checked = (KernelWorker.GetSelectedRenzoFinData.Target & 0x80) >= 1 ? true : false;
                checkBoxRenzoFinFlag1.Checked = (KernelWorker.GetSelectedRenzoFinData.AttackFlags & 0x01) >= 1 ? true : false;
                checkBoxRenzoFinFlag2.Checked = (KernelWorker.GetSelectedRenzoFinData.AttackFlags & 0x02) >= 1 ? true : false;
                checkBoxRenzoFinFlag3.Checked = (KernelWorker.GetSelectedRenzoFinData.AttackFlags & 0x04) >= 1 ? true : false;
                checkBoxRenzoFinFlag4.Checked = (KernelWorker.GetSelectedRenzoFinData.AttackFlags & 0x08) >= 1 ? true : false;
                checkBoxRenzoFinFlag5.Checked = (KernelWorker.GetSelectedRenzoFinData.AttackFlags & 0x10) >= 1 ? true : false;
                checkBoxRenzoFinFlag6.Checked = (KernelWorker.GetSelectedRenzoFinData.AttackFlags & 0x20) >= 1 ? true : false;
                checkBoxRenzoFinFlag7.Checked = (KernelWorker.GetSelectedRenzoFinData.AttackFlags & 0x40) >= 1 ? true : false;
                checkBoxRenzoFinFlag8.Checked = (KernelWorker.GetSelectedRenzoFinData.AttackFlags & 0x80) >= 1 ? true : false;
                numericUpDownRenzoFinHitCount.Value = KernelWorker.GetSelectedRenzoFinData.HitCount;
                comboBoxRenzoFinElement.SelectedIndex = RenzoFin_GetElement();
                numericUpDownRenzoFinElementPerc.Value = KernelWorker.GetSelectedRenzoFinData.ElementPerc;
                numericUpDownRenzoFinStatusAttack.Value = KernelWorker.GetSelectedRenzoFinData.StatusAttack;
                RenzoFinStatusWorker();
            }

            catch (Exception eeeeeeeeException)
            {
                Console.WriteLine(eeeeeeeeException.ToString());
            }
            _loaded = true;
        }

        #endregion

        #region TEMPORARY CHARACTERS LIMIT BREAKS

        private int TempCharLB_GetElement()
        {
            return KernelWorker.GetSelectedTempCharLBData.Element == KernelWorker.Element.Fire
                        ? 0
                        : KernelWorker.GetSelectedTempCharLBData.Element == KernelWorker.Element.Ice
                            ? 1
                            : KernelWorker.GetSelectedTempCharLBData.Element == KernelWorker.Element.Thunder
                                ? 2
                                : KernelWorker.GetSelectedTempCharLBData.Element == KernelWorker.Element.Earth
                                    ? 3
                                    : KernelWorker.GetSelectedTempCharLBData.Element == KernelWorker.Element.Poison
                                        ? 4
                                        : KernelWorker.GetSelectedTempCharLBData.Element == KernelWorker.Element.Wind
                                            ? 5
                                            : KernelWorker.GetSelectedTempCharLBData.Element ==
                                              KernelWorker.Element.Water
                                                ? 6
                                                : KernelWorker.GetSelectedTempCharLBData.Element ==
                                                  KernelWorker.Element.Holy
                                                    ? 7
                                                    : KernelWorker.GetSelectedTempCharLBData.Element ==
                                                      KernelWorker.Element.NonElemental
                                                        ? comboBoxTempCharLBElement.Items.Count - 1
                                                        : 0;
        }

        private byte TempCharLB_GetElement(int Index)
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

        private void TempCharLBStatusWorker()
        {
            checkBoxTempCharLBDeath.Checked = (KernelWorker.GetSelectedTempCharLBData.Status1 & 0x01) >= 1 ? true : false;
            checkBoxTempCharLBPoison.Checked = (KernelWorker.GetSelectedTempCharLBData.Status1 & 0x02) >= 1 ? true : false;
            checkBoxTempCharLBPetrify.Checked = (KernelWorker.GetSelectedTempCharLBData.Status1 & 0x04) >= 1 ? true : false;
            checkBoxTempCharLBDarkness.Checked = (KernelWorker.GetSelectedTempCharLBData.Status1 & 0x08) >= 1 ? true : false;
            checkBoxTempCharLBSilence.Checked = (KernelWorker.GetSelectedTempCharLBData.Status1 & 0x10) >= 1 ? true : false;
            checkBoxTempCharLBBerserk.Checked = (KernelWorker.GetSelectedTempCharLBData.Status1 & 0x20) >= 1 ? true : false;
            checkBoxTempCharLBZombie.Checked = (KernelWorker.GetSelectedTempCharLBData.Status1 & 0x40) >= 1 ? true : false;
            checkBoxTempCharLBUnk7.Checked = (KernelWorker.GetSelectedTempCharLBData.Status1 & 0x80) >= 1 ? true : false;

            checkBoxTempCharLBSleep.Checked = (KernelWorker.GetSelectedTempCharLBData.Status2 & 0x01) >= 1 ? true : false;
            checkBoxTempCharLBHaste.Checked = (KernelWorker.GetSelectedTempCharLBData.Status2 & 0x02) >= 1 ? true : false;
            checkBoxTempCharLBSlow.Checked = (KernelWorker.GetSelectedTempCharLBData.Status2 & 0x04) >= 1 ? true : false;
            checkBoxTempCharLBStop.Checked = (KernelWorker.GetSelectedTempCharLBData.Status2 & 0x08) >= 1 ? true : false;
            checkBoxTempCharLBRegen.Checked = (KernelWorker.GetSelectedTempCharLBData.Status2 & 0x10) >= 1 ? true : false;
            checkBoxTempCharLBProtect.Checked = (KernelWorker.GetSelectedTempCharLBData.Status2 & 0x20) >= 1 ? true : false;
            checkBoxTempCharLBShell.Checked = (KernelWorker.GetSelectedTempCharLBData.Status2 & 0x40) >= 1 ? true : false;
            checkBoxTempCharLBReflect.Checked = (KernelWorker.GetSelectedTempCharLBData.Status2 & 0x80) >= 1 ? true : false;

            checkBoxTempCharLBAura.Checked = (KernelWorker.GetSelectedTempCharLBData.Status3 & 0x01) >= 1 ? true : false;
            checkBoxTempCharLBCurse.Checked = (KernelWorker.GetSelectedTempCharLBData.Status3 & 0x02) >= 1 ? true : false;
            checkBoxTempCharLBDoom.Checked = (KernelWorker.GetSelectedTempCharLBData.Status3 & 0x04) >= 1 ? true : false;
            checkBoxTempCharLBInvincible.Checked = (KernelWorker.GetSelectedTempCharLBData.Status3 & 0x08) >= 1 ? true : false;
            checkBoxTempCharLBPetrifying.Checked = (KernelWorker.GetSelectedTempCharLBData.Status3 & 0x10) >= 1 ? true : false;
            checkBoxTempCharLBFloat.Checked = (KernelWorker.GetSelectedTempCharLBData.Status3 & 0x20) >= 1 ? true : false;
            checkBoxTempCharLBConfusion.Checked = (KernelWorker.GetSelectedTempCharLBData.Status3 & 0x40) >= 1 ? true : false;
            checkBoxTempCharLBDrain.Checked = (KernelWorker.GetSelectedTempCharLBData.Status3 & 0x80) >= 1 ? true : false;

            checkBoxTempCharLBEject.Checked = (KernelWorker.GetSelectedTempCharLBData.Status4 & 0x01) >= 1 ? true : false;
            checkBoxTempCharLBDouble.Checked = (KernelWorker.GetSelectedTempCharLBData.Status4 & 0x02) >= 1 ? true : false;
            checkBoxTempCharLBTriple.Checked = (KernelWorker.GetSelectedTempCharLBData.Status4 & 0x04) >= 1 ? true : false;
            checkBoxTempCharLBDefend.Checked = (KernelWorker.GetSelectedTempCharLBData.Status4 & 0x08) >= 1 ? true : false;
            checkBoxTempCharLBUnk1.Checked = (KernelWorker.GetSelectedTempCharLBData.Status4 & 0x10) >= 1 ? true : false;
            checkBoxTempCharLBUnk2.Checked = (KernelWorker.GetSelectedTempCharLBData.Status4 & 0x20) >= 1 ? true : false;
            checkBoxTempCharLBCharged.Checked = (KernelWorker.GetSelectedTempCharLBData.Status4 & 0x40) >= 1 ? true : false;
            checkBoxTempCharLBBackAttack.Checked = (KernelWorker.GetSelectedTempCharLBData.Status4 & 0x80) >= 1 ? true : false;

            checkBoxTempCharLBVit0.Checked = (KernelWorker.GetSelectedTempCharLBData.Status5 & 0x01) >= 1 ? true : false;
            checkBoxTempCharLBAngelWing.Checked = (KernelWorker.GetSelectedTempCharLBData.Status5 & 0x02) >= 1 ? true : false;
            checkBoxTempCharLBUnk3.Checked = (KernelWorker.GetSelectedTempCharLBData.Status5 & 0x04) >= 1 ? true : false;
            checkBoxTempCharLBUnk4.Checked = (KernelWorker.GetSelectedTempCharLBData.Status5 & 0x08) >= 1 ? true : false;
            checkBoxTempCharLBUnk5.Checked = (KernelWorker.GetSelectedTempCharLBData.Status5 & 0x10) >= 1 ? true : false;
            checkBoxTempCharLBUnk6.Checked = (KernelWorker.GetSelectedTempCharLBData.Status5 & 0x20) >= 1 ? true : false;
            checkBoxTempCharLBHasMagic.Checked = (KernelWorker.GetSelectedTempCharLBData.Status5 & 0x40) >= 1 ? true : false;
            checkBoxTempCharLBSummonGF.Checked = (KernelWorker.GetSelectedTempCharLBData.Status5 & 0x80) >= 1 ? true : false;
        }

        private void listBoxTempCharLB_SelectedIndexChanged(object sender, EventArgs e)
        {

            _loaded = false;
            if (KernelWorker.Kernel == null)
                return;
            KernelWorker.ReadTempCharLB(listBoxTempCharLB.SelectedIndex);
            try
            {
                comboBoxTempCharLBMagicID.SelectedIndex = KernelWorker.GetSelectedTempCharLBData.MagicID;
                comboBoxTempCharLBAttackType.SelectedIndex = KernelWorker.GetSelectedTempCharLBData.AttackType;
                numericUpDownTempCharLBAttackPower.Value = KernelWorker.GetSelectedTempCharLBData.AttackPower;
                checkBoxTempCharLBTarget1.Checked = (KernelWorker.GetSelectedTempCharLBData.Target & 0x01) >= 1 ? true : false;
                checkBoxTempCharLBTarget2.Checked = (KernelWorker.GetSelectedTempCharLBData.Target & 0x02) >= 1 ? true : false;
                checkBoxTempCharLBTarget3.Checked = (KernelWorker.GetSelectedTempCharLBData.Target & 0x04) >= 1 ? true : false;
                checkBoxTempCharLBTarget4.Checked = (KernelWorker.GetSelectedTempCharLBData.Target & 0x08) >= 1 ? true : false;
                checkBoxTempCharLBTarget5.Checked = (KernelWorker.GetSelectedTempCharLBData.Target & 0x10) >= 1 ? true : false;
                checkBoxTempCharLBTarget6.Checked = (KernelWorker.GetSelectedTempCharLBData.Target & 0x20) >= 1 ? true : false;
                checkBoxTempCharLBTarget7.Checked = (KernelWorker.GetSelectedTempCharLBData.Target & 0x40) >= 1 ? true : false;
                checkBoxTempCharLBTarget8.Checked = (KernelWorker.GetSelectedTempCharLBData.Target & 0x80) >= 1 ? true : false;
                checkBoxTempCharLBFlag1.Checked = (KernelWorker.GetSelectedTempCharLBData.AttackFlags & 0x01) >= 1 ? true : false;
                checkBoxTempCharLBFlag2.Checked = (KernelWorker.GetSelectedTempCharLBData.AttackFlags & 0x02) >= 1 ? true : false;
                checkBoxTempCharLBFlag3.Checked = (KernelWorker.GetSelectedTempCharLBData.AttackFlags & 0x04) >= 1 ? true : false;
                checkBoxTempCharLBFlag4.Checked = (KernelWorker.GetSelectedTempCharLBData.AttackFlags & 0x08) >= 1 ? true : false;
                checkBoxTempCharLBFlag5.Checked = (KernelWorker.GetSelectedTempCharLBData.AttackFlags & 0x10) >= 1 ? true : false;
                checkBoxTempCharLBFlag6.Checked = (KernelWorker.GetSelectedTempCharLBData.AttackFlags & 0x20) >= 1 ? true : false;
                checkBoxTempCharLBFlag7.Checked = (KernelWorker.GetSelectedTempCharLBData.AttackFlags & 0x40) >= 1 ? true : false;
                checkBoxTempCharLBFlag8.Checked = (KernelWorker.GetSelectedTempCharLBData.AttackFlags & 0x80) >= 1 ? true : false;
                numericUpDownTempCharLBHitCount.Value = KernelWorker.GetSelectedTempCharLBData.HitCount;
                comboBoxTempCharLBElement.SelectedIndex = TempCharLB_GetElement();
                numericUpDownTempCharLBElementPerc.Value = KernelWorker.GetSelectedTempCharLBData.ElementPerc;
                numericUpDownTempCharLBStatusAttack.Value = KernelWorker.GetSelectedTempCharLBData.StatusAttack;
                TempCharLBStatusWorker();
            }

            catch (Exception Exception)
            {
                Console.WriteLine(Exception.ToString());
            }
            _loaded = true;
        }

        #endregion

        #region SHOT

        private int Shot_GetElement()
        {
            return KernelWorker.GetSelectedShotData.Element == KernelWorker.Element.Fire
                        ? 0
                        : KernelWorker.GetSelectedShotData.Element == KernelWorker.Element.Ice
                            ? 1
                            : KernelWorker.GetSelectedShotData.Element == KernelWorker.Element.Thunder
                                ? 2
                                : KernelWorker.GetSelectedShotData.Element == KernelWorker.Element.Earth
                                    ? 3
                                    : KernelWorker.GetSelectedShotData.Element == KernelWorker.Element.Poison
                                        ? 4
                                        : KernelWorker.GetSelectedShotData.Element == KernelWorker.Element.Wind
                                            ? 5
                                            : KernelWorker.GetSelectedShotData.Element ==
                                              KernelWorker.Element.Water
                                                ? 6
                                                : KernelWorker.GetSelectedShotData.Element ==
                                                  KernelWorker.Element.Holy
                                                    ? 7
                                                    : KernelWorker.GetSelectedShotData.Element ==
                                                      KernelWorker.Element.NonElemental
                                                        ? comboBoxShotElement.Items.Count - 1
                                                        : 0;
        }

        private byte Shot_GetElement(int Index)
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

        private void ShotStatusWorker()
        {
            checkBoxShotDeath.Checked = (KernelWorker.GetSelectedShotData.Status1 & 0x01) >= 1 ? true : false;
            checkBoxShotPoison.Checked = (KernelWorker.GetSelectedShotData.Status1 & 0x02) >= 1 ? true : false;
            checkBoxShotPetrify.Checked = (KernelWorker.GetSelectedShotData.Status1 & 0x04) >= 1 ? true : false;
            checkBoxShotDarkness.Checked = (KernelWorker.GetSelectedShotData.Status1 & 0x08) >= 1 ? true : false;
            checkBoxShotSilence.Checked = (KernelWorker.GetSelectedShotData.Status1 & 0x10) >= 1 ? true : false;
            checkBoxShotBerserk.Checked = (KernelWorker.GetSelectedShotData.Status1 & 0x20) >= 1 ? true : false;
            checkBoxShotZombie.Checked = (KernelWorker.GetSelectedShotData.Status1 & 0x40) >= 1 ? true : false;
            checkBoxShotUnk7.Checked = (KernelWorker.GetSelectedShotData.Status1 & 0x80) >= 1 ? true : false;

            checkBoxShotSleep.Checked = (KernelWorker.GetSelectedShotData.Status2 & 0x01) >= 1 ? true : false;
            checkBoxShotHaste.Checked = (KernelWorker.GetSelectedShotData.Status2 & 0x02) >= 1 ? true : false;
            checkBoxShotSlow.Checked = (KernelWorker.GetSelectedShotData.Status2 & 0x04) >= 1 ? true : false;
            checkBoxShotStop.Checked = (KernelWorker.GetSelectedShotData.Status2 & 0x08) >= 1 ? true : false;
            checkBoxShotRegen.Checked = (KernelWorker.GetSelectedShotData.Status2 & 0x10) >= 1 ? true : false;
            checkBoxShotProtect.Checked = (KernelWorker.GetSelectedShotData.Status2 & 0x20) >= 1 ? true : false;
            checkBoxShotShell.Checked = (KernelWorker.GetSelectedShotData.Status2 & 0x40) >= 1 ? true : false;
            checkBoxShotReflect.Checked = (KernelWorker.GetSelectedShotData.Status2 & 0x80) >= 1 ? true : false;

            checkBoxShotAura.Checked = (KernelWorker.GetSelectedShotData.Status3 & 0x01) >= 1 ? true : false;
            checkBoxShotCurse.Checked = (KernelWorker.GetSelectedShotData.Status3 & 0x02) >= 1 ? true : false;
            checkBoxShotDoom.Checked = (KernelWorker.GetSelectedShotData.Status3 & 0x04) >= 1 ? true : false;
            checkBoxShotInvincible.Checked = (KernelWorker.GetSelectedShotData.Status3 & 0x08) >= 1 ? true : false;
            checkBoxShotPetrifying.Checked = (KernelWorker.GetSelectedShotData.Status3 & 0x10) >= 1 ? true : false;
            checkBoxShotFloat.Checked = (KernelWorker.GetSelectedShotData.Status3 & 0x20) >= 1 ? true : false;
            checkBoxShotConfusion.Checked = (KernelWorker.GetSelectedShotData.Status3 & 0x40) >= 1 ? true : false;
            checkBoxShotDrain.Checked = (KernelWorker.GetSelectedShotData.Status3 & 0x80) >= 1 ? true : false;

            checkBoxShotEject.Checked = (KernelWorker.GetSelectedShotData.Status4 & 0x01) >= 1 ? true : false;
            checkBoxShotDouble.Checked = (KernelWorker.GetSelectedShotData.Status4 & 0x02) >= 1 ? true : false;
            checkBoxShotTriple.Checked = (KernelWorker.GetSelectedShotData.Status4 & 0x04) >= 1 ? true : false;
            checkBoxShotDefend.Checked = (KernelWorker.GetSelectedShotData.Status4 & 0x08) >= 1 ? true : false;
            checkBoxShotUnk1.Checked = (KernelWorker.GetSelectedShotData.Status4 & 0x10) >= 1 ? true : false;
            checkBoxShotUnk2.Checked = (KernelWorker.GetSelectedShotData.Status4 & 0x20) >= 1 ? true : false;
            checkBoxShotCharged.Checked = (KernelWorker.GetSelectedShotData.Status4 & 0x40) >= 1 ? true : false;
            checkBoxShotBackAttack.Checked = (KernelWorker.GetSelectedShotData.Status4 & 0x80) >= 1 ? true : false;

            checkBoxShotVit0.Checked = (KernelWorker.GetSelectedShotData.Status5 & 0x01) >= 1 ? true : false;
            checkBoxShotAngelWing.Checked = (KernelWorker.GetSelectedShotData.Status5 & 0x02) >= 1 ? true : false;
            checkBoxShotUnk3.Checked = (KernelWorker.GetSelectedShotData.Status5 & 0x04) >= 1 ? true : false;
            checkBoxShotUnk4.Checked = (KernelWorker.GetSelectedShotData.Status5 & 0x08) >= 1 ? true : false;
            checkBoxShotUnk5.Checked = (KernelWorker.GetSelectedShotData.Status5 & 0x10) >= 1 ? true : false;
            checkBoxShotUnk6.Checked = (KernelWorker.GetSelectedShotData.Status5 & 0x20) >= 1 ? true : false;
            checkBoxShotHasMagic.Checked = (KernelWorker.GetSelectedShotData.Status5 & 0x40) >= 1 ? true : false;
            checkBoxShotSummonGF.Checked = (KernelWorker.GetSelectedShotData.Status5 & 0x80) >= 1 ? true : false;
        }

        private void listBoxShot_SelectedIndexChanged(object sender, EventArgs e)
        {
            _loaded = false;
            if (KernelWorker.Kernel == null)
                return;
            KernelWorker.ReadShot(listBoxShot.SelectedIndex);

            try
            {
                comboBoxShotMagicID.SelectedIndex = KernelWorker.GetSelectedShotData.MagicID;
                comboBoxShotAttackType.SelectedIndex = KernelWorker.GetSelectedShotData.AttackType;
                numericUpDownShotAttackPower.Value = KernelWorker.GetSelectedShotData.AttackPower;
                checkBoxShotTarget1.Checked = (KernelWorker.GetSelectedShotData.Target & 0x01) >= 1 ? true : false;
                checkBoxShotTarget2.Checked = (KernelWorker.GetSelectedShotData.Target & 0x02) >= 1 ? true : false;
                checkBoxShotTarget3.Checked = (KernelWorker.GetSelectedShotData.Target & 0x04) >= 1 ? true : false;
                checkBoxShotTarget4.Checked = (KernelWorker.GetSelectedShotData.Target & 0x08) >= 1 ? true : false;
                checkBoxShotTarget5.Checked = (KernelWorker.GetSelectedShotData.Target & 0x10) >= 1 ? true : false;
                checkBoxShotTarget6.Checked = (KernelWorker.GetSelectedShotData.Target & 0x20) >= 1 ? true : false;
                checkBoxShotTarget7.Checked = (KernelWorker.GetSelectedShotData.Target & 0x40) >= 1 ? true : false;
                checkBoxShotTarget8.Checked = (KernelWorker.GetSelectedShotData.Target & 0x80) >= 1 ? true : false;                
                checkBoxShotFlag1.Checked = (KernelWorker.GetSelectedShotData.AttackFlags & 0x01) >= 1 ? true : false;
                checkBoxShotFlag2.Checked = (KernelWorker.GetSelectedShotData.AttackFlags & 0x02) >= 1 ? true : false;
                checkBoxShotFlag3.Checked = (KernelWorker.GetSelectedShotData.AttackFlags & 0x04) >= 1 ? true : false;
                checkBoxShotFlag4.Checked = (KernelWorker.GetSelectedShotData.AttackFlags & 0x08) >= 1 ? true : false;
                checkBoxShotFlag5.Checked = (KernelWorker.GetSelectedShotData.AttackFlags & 0x10) >= 1 ? true : false;
                checkBoxShotFlag6.Checked = (KernelWorker.GetSelectedShotData.AttackFlags & 0x20) >= 1 ? true : false;
                checkBoxShotFlag7.Checked = (KernelWorker.GetSelectedShotData.AttackFlags & 0x40) >= 1 ? true : false;
                checkBoxShotFlag8.Checked = (KernelWorker.GetSelectedShotData.AttackFlags & 0x80) >= 1 ? true : false;
                numericUpDownShotHitCount.Value = KernelWorker.GetSelectedShotData.HitCount;
                comboBoxShotElement.SelectedIndex = Shot_GetElement();
                numericUpDownShotElementPerc.Value = KernelWorker.GetSelectedShotData.ElementPerc;
                numericUpDownShotStatusAttack.Value = KernelWorker.GetSelectedShotData.StatusAttack;
                ShotStatusWorker();
                comboBoxShotItem.SelectedIndex = KernelWorker.GetSelectedShotData.UsedItem;

            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }
            _loaded = true;
        }

        #endregion

        #region DUEL

        private int Duel_GetElement()
        {
            return KernelWorker.GetSelectedDuelData.Element == KernelWorker.Element.Fire
                        ? 0
                        : KernelWorker.GetSelectedDuelData.Element == KernelWorker.Element.Ice
                            ? 1
                            : KernelWorker.GetSelectedDuelData.Element == KernelWorker.Element.Thunder
                                ? 2
                                : KernelWorker.GetSelectedDuelData.Element == KernelWorker.Element.Earth
                                    ? 3
                                    : KernelWorker.GetSelectedDuelData.Element == KernelWorker.Element.Poison
                                        ? 4
                                        : KernelWorker.GetSelectedDuelData.Element == KernelWorker.Element.Wind
                                            ? 5
                                            : KernelWorker.GetSelectedDuelData.Element ==
                                              KernelWorker.Element.Water
                                                ? 6
                                                : KernelWorker.GetSelectedDuelData.Element ==
                                                  KernelWorker.Element.Holy
                                                    ? 7
                                                    : KernelWorker.GetSelectedDuelData.Element ==
                                                      KernelWorker.Element.NonElemental
                                                        ? comboBoxDuelElement.Items.Count - 1
                                                        : 0;
        }

        private byte Duel_GetElement(int Index)
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


        private void DuelStatusWorker()
        {
            checkBoxDuelDeath.Checked = (KernelWorker.GetSelectedDuelData.Status1 & 0x01) >= 1 ? true : false;
            checkBoxDuelPoison.Checked = (KernelWorker.GetSelectedDuelData.Status1 & 0x02) >= 1 ? true : false;
            checkBoxDuelPetrify.Checked = (KernelWorker.GetSelectedDuelData.Status1 & 0x04) >= 1 ? true : false;
            checkBoxDuelDarkness.Checked = (KernelWorker.GetSelectedDuelData.Status1 & 0x08) >= 1 ? true : false;
            checkBoxDuelSilence.Checked = (KernelWorker.GetSelectedDuelData.Status1 & 0x10) >= 1 ? true : false;
            checkBoxDuelBerserk.Checked = (KernelWorker.GetSelectedDuelData.Status1 & 0x20) >= 1 ? true : false;
            checkBoxDuelZombie.Checked = (KernelWorker.GetSelectedDuelData.Status1 & 0x40) >= 1 ? true : false;
            checkBoxDuelUnk7.Checked = (KernelWorker.GetSelectedDuelData.Status1 & 0x80) >= 1 ? true : false;

            checkBoxDuelSleep.Checked = (KernelWorker.GetSelectedDuelData.Status2 & 0x01) >= 1 ? true : false;
            checkBoxDuelHaste.Checked = (KernelWorker.GetSelectedDuelData.Status2 & 0x02) >= 1 ? true : false;
            checkBoxDuelSlow.Checked = (KernelWorker.GetSelectedDuelData.Status2 & 0x04) >= 1 ? true : false;
            checkBoxDuelStop.Checked = (KernelWorker.GetSelectedDuelData.Status2 & 0x08) >= 1 ? true : false;
            checkBoxDuelRegen.Checked = (KernelWorker.GetSelectedDuelData.Status2 & 0x10) >= 1 ? true : false;
            checkBoxDuelProtect.Checked = (KernelWorker.GetSelectedDuelData.Status2 & 0x20) >= 1 ? true : false;
            checkBoxDuelShell.Checked = (KernelWorker.GetSelectedDuelData.Status2 & 0x40) >= 1 ? true : false;
            checkBoxDuelReflect.Checked = (KernelWorker.GetSelectedDuelData.Status2 & 0x80) >= 1 ? true : false;

            checkBoxDuelAura.Checked = (KernelWorker.GetSelectedDuelData.Status3 & 0x01) >= 1 ? true : false;
            checkBoxDuelCurse.Checked = (KernelWorker.GetSelectedDuelData.Status3 & 0x02) >= 1 ? true : false;
            checkBoxDuelDoom.Checked = (KernelWorker.GetSelectedDuelData.Status3 & 0x04) >= 1 ? true : false;
            checkBoxDuelInvincible.Checked = (KernelWorker.GetSelectedDuelData.Status3 & 0x08) >= 1 ? true : false;
            checkBoxDuelPetrifying.Checked = (KernelWorker.GetSelectedDuelData.Status3 & 0x10) >= 1 ? true : false;
            checkBoxDuelFloat.Checked = (KernelWorker.GetSelectedDuelData.Status3 & 0x20) >= 1 ? true : false;
            checkBoxDuelConfusion.Checked = (KernelWorker.GetSelectedDuelData.Status3 & 0x40) >= 1 ? true : false;
            checkBoxDuelDrain.Checked = (KernelWorker.GetSelectedDuelData.Status3 & 0x80) >= 1 ? true : false;

            checkBoxDuelEject.Checked = (KernelWorker.GetSelectedDuelData.Status4 & 0x01) >= 1 ? true : false;
            checkBoxDuelDouble.Checked = (KernelWorker.GetSelectedDuelData.Status4 & 0x02) >= 1 ? true : false;
            checkBoxDuelTriple.Checked = (KernelWorker.GetSelectedDuelData.Status4 & 0x04) >= 1 ? true : false;
            checkBoxDuelDefend.Checked = (KernelWorker.GetSelectedDuelData.Status4 & 0x08) >= 1 ? true : false;
            checkBoxDuelUnk1.Checked = (KernelWorker.GetSelectedDuelData.Status4 & 0x10) >= 1 ? true : false;
            checkBoxDuelUnk2.Checked = (KernelWorker.GetSelectedDuelData.Status4 & 0x20) >= 1 ? true : false;
            checkBoxDuelCharged.Checked = (KernelWorker.GetSelectedDuelData.Status4 & 0x40) >= 1 ? true : false;
            checkBoxDuelBackAttack.Checked = (KernelWorker.GetSelectedDuelData.Status4 & 0x80) >= 1 ? true : false;

            checkBoxDuelVit0.Checked = (KernelWorker.GetSelectedDuelData.Status5 & 0x01) >= 1 ? true : false;
            checkBoxDuelAngelWing.Checked = (KernelWorker.GetSelectedDuelData.Status5 & 0x02) >= 1 ? true : false;
            checkBoxDuelUnk3.Checked = (KernelWorker.GetSelectedDuelData.Status5 & 0x04) >= 1 ? true : false;
            checkBoxDuelUnk4.Checked = (KernelWorker.GetSelectedDuelData.Status5 & 0x08) >= 1 ? true : false;
            checkBoxDuelUnk5.Checked = (KernelWorker.GetSelectedDuelData.Status5 & 0x10) >= 1 ? true : false;
            checkBoxDuelUnk6.Checked = (KernelWorker.GetSelectedDuelData.Status5 & 0x20) >= 1 ? true : false;
            checkBoxDuelHasMagic.Checked = (KernelWorker.GetSelectedDuelData.Status5 & 0x40) >= 1 ? true : false;
            checkBoxDuelSummonGF.Checked = (KernelWorker.GetSelectedDuelData.Status5 & 0x80) >= 1 ? true : false;
        }


        private void listBoxDuel_SelectedIndexChanged(object sender, EventArgs e)
        {
            _loaded = false;
            if (KernelWorker.Kernel == null)
                return;
            KernelWorker.ReadDuel(listBoxDuel.SelectedIndex);

            try
            {
                comboBoxDuelMagicID.SelectedIndex = KernelWorker.GetSelectedDuelData.MagicID;
                comboBoxDuelAttackType.SelectedIndex = KernelWorker.GetSelectedDuelData.AttackType;
                numericUpDownDuelAttackPower.Value = KernelWorker.GetSelectedDuelData.AttackPower;
                checkBoxDuelFlag1.Checked = (KernelWorker.GetSelectedDuelData.AttackFlags & 0x01) >= 1 ? true : false;
                checkBoxDuelFlag2.Checked = (KernelWorker.GetSelectedDuelData.AttackFlags & 0x02) >= 1 ? true : false;
                checkBoxDuelFlag3.Checked = (KernelWorker.GetSelectedDuelData.AttackFlags & 0x04) >= 1 ? true : false;
                checkBoxDuelFlag4.Checked = (KernelWorker.GetSelectedDuelData.AttackFlags & 0x08) >= 1 ? true : false;
                checkBoxDuelFlag5.Checked = (KernelWorker.GetSelectedDuelData.AttackFlags & 0x10) >= 1 ? true : false;
                checkBoxDuelFlag6.Checked = (KernelWorker.GetSelectedDuelData.AttackFlags & 0x20) >= 1 ? true : false;
                checkBoxDuelFlag7.Checked = (KernelWorker.GetSelectedDuelData.AttackFlags & 0x40) >= 1 ? true : false;
                checkBoxDuelFlag8.Checked = (KernelWorker.GetSelectedDuelData.AttackFlags & 0x80) >= 1 ? true : false;
                checkBoxDuelTarget1.Checked = (KernelWorker.GetSelectedDuelData.Target & 0x01) >= 1 ? true : false;
                checkBoxDuelTarget2.Checked = (KernelWorker.GetSelectedDuelData.Target & 0x02) >= 1 ? true : false;
                checkBoxDuelTarget3.Checked = (KernelWorker.GetSelectedDuelData.Target & 0x04) >= 1 ? true : false;
                checkBoxDuelTarget4.Checked = (KernelWorker.GetSelectedDuelData.Target & 0x08) >= 1 ? true : false;
                checkBoxDuelTarget5.Checked = (KernelWorker.GetSelectedDuelData.Target & 0x10) >= 1 ? true : false;
                checkBoxDuelTarget6.Checked = (KernelWorker.GetSelectedDuelData.Target & 0x20) >= 1 ? true : false;
                checkBoxDuelTarget7.Checked = (KernelWorker.GetSelectedDuelData.Target & 0x40) >= 1 ? true : false;
                checkBoxDuelTarget8.Checked = (KernelWorker.GetSelectedDuelData.Target & 0x80) >= 1 ? true : false;
                numericUpDownDuelHitCount.Value = KernelWorker.GetSelectedDuelData.HitCount;
                comboBoxDuelElement.SelectedIndex = Duel_GetElement();
                numericUpDownDuelElementPerc.Value = KernelWorker.GetSelectedDuelData.ElementPerc;
                numericUpDownDuelStatusAttack.Value = KernelWorker.GetSelectedDuelData.StatusAttack;
                DuelStatusWorker();
                //to do buttons

            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }
            _loaded = true;
        }




        #endregion

        #region COMBINE

        private int Combine_GetElement()
        {
            return KernelWorker.GetSelectedCombineData.Element == KernelWorker.Element.Fire
                        ? 0
                        : KernelWorker.GetSelectedCombineData.Element == KernelWorker.Element.Ice
                            ? 1
                            : KernelWorker.GetSelectedCombineData.Element == KernelWorker.Element.Thunder
                                ? 2
                                : KernelWorker.GetSelectedCombineData.Element == KernelWorker.Element.Earth
                                    ? 3
                                    : KernelWorker.GetSelectedCombineData.Element == KernelWorker.Element.Poison
                                        ? 4
                                        : KernelWorker.GetSelectedCombineData.Element == KernelWorker.Element.Wind
                                            ? 5
                                            : KernelWorker.GetSelectedCombineData.Element ==
                                              KernelWorker.Element.Water
                                                ? 6
                                                : KernelWorker.GetSelectedCombineData.Element ==
                                                  KernelWorker.Element.Holy
                                                    ? 7
                                                    : KernelWorker.GetSelectedCombineData.Element ==
                                                      KernelWorker.Element.NonElemental
                                                        ? comboBoxCombineElement.Items.Count - 1
                                                        : 0;
        }

        private byte Combine_GetElement(int Index)
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

        private void CombineStatusWorker()
        {
            checkBoxCombineDeath.Checked = (KernelWorker.GetSelectedCombineData.Status1 & 0x01) >= 1 ? true : false;
            checkBoxCombinePoison.Checked = (KernelWorker.GetSelectedCombineData.Status1 & 0x02) >= 1 ? true : false;
            checkBoxCombinePetrify.Checked = (KernelWorker.GetSelectedCombineData.Status1 & 0x04) >= 1 ? true : false;
            checkBoxCombineDarkness.Checked = (KernelWorker.GetSelectedCombineData.Status1 & 0x08) >= 1 ? true : false;
            checkBoxCombineSilence.Checked = (KernelWorker.GetSelectedCombineData.Status1 & 0x10) >= 1 ? true : false;
            checkBoxCombineBerserk.Checked = (KernelWorker.GetSelectedCombineData.Status1 & 0x20) >= 1 ? true : false;
            checkBoxCombineZombie.Checked = (KernelWorker.GetSelectedCombineData.Status1 & 0x40) >= 1 ? true : false;
            checkBoxCombineUnk7.Checked = (KernelWorker.GetSelectedCombineData.Status1 & 0x80) >= 1 ? true : false;

            checkBoxCombineSleep.Checked = (KernelWorker.GetSelectedCombineData.Status2 & 0x01) >= 1 ? true : false;
            checkBoxCombineHaste.Checked = (KernelWorker.GetSelectedCombineData.Status2 & 0x02) >= 1 ? true : false;
            checkBoxCombineSlow.Checked = (KernelWorker.GetSelectedCombineData.Status2 & 0x04) >= 1 ? true : false;
            checkBoxCombineStop.Checked = (KernelWorker.GetSelectedCombineData.Status2 & 0x08) >= 1 ? true : false;
            checkBoxCombineRegen.Checked = (KernelWorker.GetSelectedCombineData.Status2 & 0x10) >= 1 ? true : false;
            checkBoxCombineProtect.Checked = (KernelWorker.GetSelectedCombineData.Status2 & 0x20) >= 1 ? true : false;
            checkBoxCombineShell.Checked = (KernelWorker.GetSelectedCombineData.Status2 & 0x40) >= 1 ? true : false;
            checkBoxCombineReflect.Checked = (KernelWorker.GetSelectedCombineData.Status2 & 0x80) >= 1 ? true : false;

            checkBoxCombineAura.Checked = (KernelWorker.GetSelectedCombineData.Status3 & 0x01) >= 1 ? true : false;
            checkBoxCombineCurse.Checked = (KernelWorker.GetSelectedCombineData.Status3 & 0x02) >= 1 ? true : false;
            checkBoxCombineDoom.Checked = (KernelWorker.GetSelectedCombineData.Status3 & 0x04) >= 1 ? true : false;
            checkBoxCombineInvincible.Checked = (KernelWorker.GetSelectedCombineData.Status3 & 0x08) >= 1 ? true : false;
            checkBoxCombinePetrifying.Checked = (KernelWorker.GetSelectedCombineData.Status3 & 0x10) >= 1 ? true : false;
            checkBoxCombineFloat.Checked = (KernelWorker.GetSelectedCombineData.Status3 & 0x20) >= 1 ? true : false;
            checkBoxCombineConfusion.Checked = (KernelWorker.GetSelectedCombineData.Status3 & 0x40) >= 1 ? true : false;
            checkBoxCombineDrain.Checked = (KernelWorker.GetSelectedCombineData.Status3 & 0x80) >= 1 ? true : false;

            checkBoxCombineEject.Checked = (KernelWorker.GetSelectedCombineData.Status4 & 0x01) >= 1 ? true : false;
            checkBoxCombineDouble.Checked = (KernelWorker.GetSelectedCombineData.Status4 & 0x02) >= 1 ? true : false;
            checkBoxCombineTriple.Checked = (KernelWorker.GetSelectedCombineData.Status4 & 0x04) >= 1 ? true : false;
            checkBoxCombineDefend.Checked = (KernelWorker.GetSelectedCombineData.Status4 & 0x08) >= 1 ? true : false;
            checkBoxCombineUnk1.Checked = (KernelWorker.GetSelectedCombineData.Status4 & 0x10) >= 1 ? true : false;
            checkBoxCombineUnk2.Checked = (KernelWorker.GetSelectedCombineData.Status4 & 0x20) >= 1 ? true : false;
            checkBoxCombineCharged.Checked = (KernelWorker.GetSelectedCombineData.Status4 & 0x40) >= 1 ? true : false;
            checkBoxCombineBackAttack.Checked = (KernelWorker.GetSelectedCombineData.Status4 & 0x80) >= 1 ? true : false;

            checkBoxCombineVit0.Checked = (KernelWorker.GetSelectedCombineData.Status5 & 0x01) >= 1 ? true : false;
            checkBoxCombineAngelWing.Checked = (KernelWorker.GetSelectedCombineData.Status5 & 0x02) >= 1 ? true : false;
            checkBoxCombineUnk3.Checked = (KernelWorker.GetSelectedCombineData.Status5 & 0x04) >= 1 ? true : false;
            checkBoxCombineUnk4.Checked = (KernelWorker.GetSelectedCombineData.Status5 & 0x08) >= 1 ? true : false;
            checkBoxCombineUnk5.Checked = (KernelWorker.GetSelectedCombineData.Status5 & 0x10) >= 1 ? true : false;
            checkBoxCombineUnk6.Checked = (KernelWorker.GetSelectedCombineData.Status5 & 0x20) >= 1 ? true : false;
            checkBoxCombineHasMagic.Checked = (KernelWorker.GetSelectedCombineData.Status5 & 0x40) >= 1 ? true : false;
            checkBoxCombineSummonGF.Checked = (KernelWorker.GetSelectedCombineData.Status5 & 0x80) >= 1 ? true : false;
        }

        private void listBoxCombine_SelectedIndexChanged(object sender, EventArgs e)
        {
            _loaded = false;
            if (KernelWorker.Kernel == null)
                return;
            KernelWorker.ReadCombine(listBoxCombine.SelectedIndex);

            try
            {
                comboBoxCombineMagicID.SelectedIndex = KernelWorker.GetSelectedCombineData.MagicID;
                comboBoxCombineAttackType.SelectedIndex = KernelWorker.GetSelectedCombineData.AttackType;
                numericUpDownCombineAttackPower.Value = KernelWorker.GetSelectedCombineData.AttackPower;
                checkBoxCombineFlag1.Checked = (KernelWorker.GetSelectedCombineData.AttackFlags & 0x01) >= 1 ? true : false;
                checkBoxCombineFlag2.Checked = (KernelWorker.GetSelectedCombineData.AttackFlags & 0x02) >= 1 ? true : false;
                checkBoxCombineFlag3.Checked = (KernelWorker.GetSelectedCombineData.AttackFlags & 0x04) >= 1 ? true : false;
                checkBoxCombineFlag4.Checked = (KernelWorker.GetSelectedCombineData.AttackFlags & 0x08) >= 1 ? true : false;
                checkBoxCombineFlag5.Checked = (KernelWorker.GetSelectedCombineData.AttackFlags & 0x10) >= 1 ? true : false;
                checkBoxCombineFlag6.Checked = (KernelWorker.GetSelectedCombineData.AttackFlags & 0x20) >= 1 ? true : false;
                checkBoxCombineFlag7.Checked = (KernelWorker.GetSelectedCombineData.AttackFlags & 0x40) >= 1 ? true : false;
                checkBoxCombineFlag8.Checked = (KernelWorker.GetSelectedCombineData.AttackFlags & 0x80) >= 1 ? true : false;
                checkBoxCombineTarget1.Checked = (KernelWorker.GetSelectedCombineData.Target & 0x01) >= 1 ? true : false;
                checkBoxCombineTarget2.Checked = (KernelWorker.GetSelectedCombineData.Target & 0x02) >= 1 ? true : false;
                checkBoxCombineTarget3.Checked = (KernelWorker.GetSelectedCombineData.Target & 0x04) >= 1 ? true : false;
                checkBoxCombineTarget4.Checked = (KernelWorker.GetSelectedCombineData.Target & 0x08) >= 1 ? true : false;
                checkBoxCombineTarget5.Checked = (KernelWorker.GetSelectedCombineData.Target & 0x10) >= 1 ? true : false;
                checkBoxCombineTarget6.Checked = (KernelWorker.GetSelectedCombineData.Target & 0x20) >= 1 ? true : false;
                checkBoxCombineTarget7.Checked = (KernelWorker.GetSelectedCombineData.Target & 0x40) >= 1 ? true : false;
                checkBoxCombineTarget8.Checked = (KernelWorker.GetSelectedCombineData.Target & 0x80) >= 1 ? true : false;
                numericUpDownCombineHitCount.Value = KernelWorker.GetSelectedCombineData.HitCount;
                comboBoxCombineElement.SelectedIndex = Combine_GetElement();
                numericUpDownCombineElementPerc.Value = KernelWorker.GetSelectedCombineData.ElementPerc;
                numericUpDownCombineStatusAttack.Value = KernelWorker.GetSelectedCombineData.StatusAttack;
                CombineStatusWorker();

            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }
            _loaded = true;
        }







        #endregion

        #region BATTLE ITEMS

        private int BattleItems_GetElement()
        {
            return KernelWorker.GetSelectedBattleItemsData.Element == KernelWorker.Element.Fire
                        ? 0
                        : KernelWorker.GetSelectedBattleItemsData.Element == KernelWorker.Element.Ice
                            ? 1
                            : KernelWorker.GetSelectedBattleItemsData.Element == KernelWorker.Element.Thunder
                                ? 2
                                : KernelWorker.GetSelectedBattleItemsData.Element == KernelWorker.Element.Earth
                                    ? 3
                                    : KernelWorker.GetSelectedBattleItemsData.Element == KernelWorker.Element.Poison
                                        ? 4
                                        : KernelWorker.GetSelectedBattleItemsData.Element == KernelWorker.Element.Wind
                                            ? 5
                                            : KernelWorker.GetSelectedBattleItemsData.Element ==
                                              KernelWorker.Element.Water
                                                ? 6
                                                : KernelWorker.GetSelectedBattleItemsData.Element ==
                                                  KernelWorker.Element.Holy
                                                    ? 7
                                                    : KernelWorker.GetSelectedBattleItemsData.Element ==
                                                      KernelWorker.Element.NonElemental
                                                        ? comboBoxBattleItemsElement.Items.Count - 1
                                                        : 0;
        }

        private byte BattleItems_GetElement(int Index)
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

        private void BattleItemsStatusWorker()
        {
            checkBoxBattleItemsDeath.Checked = (KernelWorker.GetSelectedBattleItemsData.Status1 & 0x01) >= 1 ? true : false;
            checkBoxBattleItemsPoison.Checked = (KernelWorker.GetSelectedBattleItemsData.Status1 & 0x02) >= 1 ? true : false;
            checkBoxBattleItemsPetrify.Checked = (KernelWorker.GetSelectedBattleItemsData.Status1 & 0x04) >= 1 ? true : false;
            checkBoxBattleItemsDarkness.Checked = (KernelWorker.GetSelectedBattleItemsData.Status1 & 0x08) >= 1 ? true : false;
            checkBoxBattleItemsSilence.Checked = (KernelWorker.GetSelectedBattleItemsData.Status1 & 0x10) >= 1 ? true : false;
            checkBoxBattleItemsBerserk.Checked = (KernelWorker.GetSelectedBattleItemsData.Status1 & 0x20) >= 1 ? true : false;
            checkBoxBattleItemsZombie.Checked = (KernelWorker.GetSelectedBattleItemsData.Status1 & 0x40) >= 1 ? true : false;
            checkBoxBattleItemsUnk7.Checked = (KernelWorker.GetSelectedBattleItemsData.Status1 & 0x80) >= 1 ? true : false;

            checkBoxBattleItemsSleep.Checked = (KernelWorker.GetSelectedBattleItemsData.Status2 & 0x01) >= 1 ? true : false;
            checkBoxBattleItemsHaste.Checked = (KernelWorker.GetSelectedBattleItemsData.Status2 & 0x02) >= 1 ? true : false;
            checkBoxBattleItemsSlow.Checked = (KernelWorker.GetSelectedBattleItemsData.Status2 & 0x04) >= 1 ? true : false;
            checkBoxBattleItemsStop.Checked = (KernelWorker.GetSelectedBattleItemsData.Status2 & 0x08) >= 1 ? true : false;
            checkBoxBattleItemsRegen.Checked = (KernelWorker.GetSelectedBattleItemsData.Status2 & 0x10) >= 1 ? true : false;
            checkBoxBattleItemsProtect.Checked = (KernelWorker.GetSelectedBattleItemsData.Status2 & 0x20) >= 1 ? true : false;
            checkBoxBattleItemsShell.Checked = (KernelWorker.GetSelectedBattleItemsData.Status2 & 0x40) >= 1 ? true : false;
            checkBoxBattleItemsReflect.Checked = (KernelWorker.GetSelectedBattleItemsData.Status2 & 0x80) >= 1 ? true : false;

            checkBoxBattleItemsAura.Checked = (KernelWorker.GetSelectedBattleItemsData.Status3 & 0x01) >= 1 ? true : false;
            checkBoxBattleItemsCurse.Checked = (KernelWorker.GetSelectedBattleItemsData.Status3 & 0x02) >= 1 ? true : false;
            checkBoxBattleItemsDoom.Checked = (KernelWorker.GetSelectedBattleItemsData.Status3 & 0x04) >= 1 ? true : false;
            checkBoxBattleItemsInvincible.Checked = (KernelWorker.GetSelectedBattleItemsData.Status3 & 0x08) >= 1 ? true : false;
            checkBoxBattleItemsPetrifying.Checked = (KernelWorker.GetSelectedBattleItemsData.Status3 & 0x10) >= 1 ? true : false;
            checkBoxBattleItemsFloat.Checked = (KernelWorker.GetSelectedBattleItemsData.Status3 & 0x20) >= 1 ? true : false;
            checkBoxBattleItemsConfusion.Checked = (KernelWorker.GetSelectedBattleItemsData.Status3 & 0x40) >= 1 ? true : false;
            checkBoxBattleItemsDrain.Checked = (KernelWorker.GetSelectedBattleItemsData.Status3 & 0x80) >= 1 ? true : false;

            checkBoxBattleItemsEject.Checked = (KernelWorker.GetSelectedBattleItemsData.Status4 & 0x01) >= 1 ? true : false;
            checkBoxBattleItemsDouble.Checked = (KernelWorker.GetSelectedBattleItemsData.Status4 & 0x02) >= 1 ? true : false;
            checkBoxBattleItemsTriple.Checked = (KernelWorker.GetSelectedBattleItemsData.Status4 & 0x04) >= 1 ? true : false;
            checkBoxBattleItemsDefend.Checked = (KernelWorker.GetSelectedBattleItemsData.Status4 & 0x08) >= 1 ? true : false;
            checkBoxBattleItemsUnk1.Checked = (KernelWorker.GetSelectedBattleItemsData.Status4 & 0x10) >= 1 ? true : false;
            checkBoxBattleItemsUnk2.Checked = (KernelWorker.GetSelectedBattleItemsData.Status4 & 0x20) >= 1 ? true : false;
            checkBoxBattleItemsCharged.Checked = (KernelWorker.GetSelectedBattleItemsData.Status4 & 0x40) >= 1 ? true : false;
            checkBoxBattleItemsBackAttack.Checked = (KernelWorker.GetSelectedBattleItemsData.Status4 & 0x80) >= 1 ? true : false;

            checkBoxBattleItemsVit0.Checked = (KernelWorker.GetSelectedBattleItemsData.Status5 & 0x01) >= 1 ? true : false;
            checkBoxBattleItemsAngelWing.Checked = (KernelWorker.GetSelectedBattleItemsData.Status5 & 0x02) >= 1 ? true : false;
            checkBoxBattleItemsUnk3.Checked = (KernelWorker.GetSelectedBattleItemsData.Status5 & 0x04) >= 1 ? true : false;
            checkBoxBattleItemsUnk4.Checked = (KernelWorker.GetSelectedBattleItemsData.Status5 & 0x08) >= 1 ? true : false;
            checkBoxBattleItemsUnk5.Checked = (KernelWorker.GetSelectedBattleItemsData.Status5 & 0x10) >= 1 ? true : false;
            checkBoxBattleItemsUnk6.Checked = (KernelWorker.GetSelectedBattleItemsData.Status5 & 0x20) >= 1 ? true : false;
            checkBoxBattleItemsHasMagic.Checked = (KernelWorker.GetSelectedBattleItemsData.Status5 & 0x40) >= 1 ? true : false;
            checkBoxBattleItemsSummonGF.Checked = (KernelWorker.GetSelectedBattleItemsData.Status5 & 0x80) >= 1 ? true : false;
        }

        private void listBoxBattleItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            _loaded = false;
            if (KernelWorker.Kernel == null)
                return;
            KernelWorker.ReadBattleItems(listBoxBattleItems.SelectedIndex);
            try
            {
                comboBoxBattleItemsMagicID.SelectedIndex = KernelWorker.GetSelectedBattleItemsData.MagicID;
                comboBoxBattleItemsAttackType.SelectedIndex = KernelWorker.GetSelectedBattleItemsData.AttackType;
                numericUpDownBattleItemsAttackPower.Value = KernelWorker.GetSelectedBattleItemsData.AttackPower;
                checkBoxBattleItemsTarget1.Checked = (KernelWorker.GetSelectedBattleItemsData.Target & 0x01) >= 1 ? true : false;
                checkBoxBattleItemsTarget2.Checked = (KernelWorker.GetSelectedBattleItemsData.Target & 0x02) >= 1 ? true : false;
                checkBoxBattleItemsTarget3.Checked = (KernelWorker.GetSelectedBattleItemsData.Target & 0x04) >= 1 ? true : false;
                checkBoxBattleItemsTarget4.Checked = (KernelWorker.GetSelectedBattleItemsData.Target & 0x08) >= 1 ? true : false;
                checkBoxBattleItemsTarget5.Checked = (KernelWorker.GetSelectedBattleItemsData.Target & 0x10) >= 1 ? true : false;
                checkBoxBattleItemsTarget6.Checked = (KernelWorker.GetSelectedBattleItemsData.Target & 0x20) >= 1 ? true : false;
                checkBoxBattleItemsTarget7.Checked = (KernelWorker.GetSelectedBattleItemsData.Target & 0x40) >= 1 ? true : false;
                checkBoxBattleItemsTarget8.Checked = (KernelWorker.GetSelectedBattleItemsData.Target & 0x80) >= 1 ? true : false;
                checkBoxBattleItemsFlag1.Checked = (KernelWorker.GetSelectedBattleItemsData.AttackFlags & 0x01) >= 1 ? true : false;
                checkBoxBattleItemsFlag2.Checked = (KernelWorker.GetSelectedBattleItemsData.AttackFlags & 0x02) >= 1 ? true : false;
                checkBoxBattleItemsFlag3.Checked = (KernelWorker.GetSelectedBattleItemsData.AttackFlags & 0x04) >= 1 ? true : false;
                checkBoxBattleItemsFlag4.Checked = (KernelWorker.GetSelectedBattleItemsData.AttackFlags & 0x08) >= 1 ? true : false;
                checkBoxBattleItemsFlag5.Checked = (KernelWorker.GetSelectedBattleItemsData.AttackFlags & 0x10) >= 1 ? true : false;
                checkBoxBattleItemsFlag6.Checked = (KernelWorker.GetSelectedBattleItemsData.AttackFlags & 0x20) >= 1 ? true : false;
                checkBoxBattleItemsFlag7.Checked = (KernelWorker.GetSelectedBattleItemsData.AttackFlags & 0x40) >= 1 ? true : false;
                checkBoxBattleItemsFlag8.Checked = (KernelWorker.GetSelectedBattleItemsData.AttackFlags & 0x80) >= 1 ? true : false;
                numericUpDownBattleItemsStatusAttack.Value = KernelWorker.GetSelectedBattleItemsData.StatusAttack;
                BattleItemsStatusWorker();
                numericUpDownBattleItemsHitCount.Value = KernelWorker.GetSelectedBattleItemsData.HitCount;
                comboBoxBattleItemsElement.SelectedIndex = BattleItems_GetElement();
            }

            catch (Exception Exception)
            {
                Console.WriteLine(Exception.ToString());
            }
            _loaded = true;
        }

        #endregion

        #region SLOT SETS

        private void listBoxSlotSets_SelectedIndexChanged(object sender, EventArgs e)
        {
            _loaded = false;
            if (KernelWorker.Kernel == null)
                return;
            KernelWorker.ReadSlotsSets(listBoxSlotsSets.SelectedIndex);
            try
            {
                comboBoxSlotsMagic1.SelectedIndex = KernelWorker.GetSelectedSlotsSetsData.Magic1;
                comboBoxSlotsMagic2.SelectedIndex = KernelWorker.GetSelectedSlotsSetsData.Magic2;
                comboBoxSlotsMagic3.SelectedIndex = KernelWorker.GetSelectedSlotsSetsData.Magic3;
                comboBoxSlotsMagic4.SelectedIndex = KernelWorker.GetSelectedSlotsSetsData.Magic4;
                comboBoxSlotsMagic5.SelectedIndex = KernelWorker.GetSelectedSlotsSetsData.Magic5;
                comboBoxSlotsMagic6.SelectedIndex = KernelWorker.GetSelectedSlotsSetsData.Magic6;
                comboBoxSlotsMagic7.SelectedIndex = KernelWorker.GetSelectedSlotsSetsData.Magic7;
                comboBoxSlotsMagic8.SelectedIndex = KernelWorker.GetSelectedSlotsSetsData.Magic8;
                numericUpDownSlotsCount1.Value = KernelWorker.GetSelectedSlotsSetsData.Count1;
                numericUpDownSlotsCount2.Value = KernelWorker.GetSelectedSlotsSetsData.Count2;
                numericUpDownSlotsCount3.Value = KernelWorker.GetSelectedSlotsSetsData.Count3;
                numericUpDownSlotsCount4.Value = KernelWorker.GetSelectedSlotsSetsData.Count4;
                numericUpDownSlotsCount5.Value = KernelWorker.GetSelectedSlotsSetsData.Count5;
                numericUpDownSlotsCount6.Value = KernelWorker.GetSelectedSlotsSetsData.Count6;
                numericUpDownSlotsCount7.Value = KernelWorker.GetSelectedSlotsSetsData.Count7;
                numericUpDownSlotsCount8.Value = KernelWorker.GetSelectedSlotsSetsData.Count8;
            }

            catch (Exception Exception)
            {
                Console.WriteLine(Exception.ToString());
            }
            _loaded = true;
        }

        #endregion


    }
}