using System;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using Doomtrain.Charts;

namespace Doomtrain
{
    public partial class mainForm : Form
    {
        public static bool _loaded = false;
        private string _existingFilename;
        private string _backup;
        private const byte _bp_numerical = 0x00;
        private const byte _bp_checked = 0x01;
        private const byte _bp_string = 0x02;

        //For loading fonts
        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);
        FontFamily ff;
        Font font;

        public mainForm()
        {
            InitializeComponent();

            #region Load MagicID List

            string[] magicIdList = Properties.Resources.Magic_ID_List.Split('\n');
            foreach (var line in magicIdList)
            {
                comboBoxRenzoFinMagicID.Items.Add(line);
                comboBoxBlueMagicMagicID.Items.Add(line);
                comboBoxDuelMagicID.Items.Add(line);
                comboBoxCombineMagicID.Items.Add(line);
                comboBoxShotMagicID.Items.Add(line);
                comboBoxTempCharLBMagicID.Items.Add(line);
                comboBoxBattleItemsMagicID.Items.Add(line);
                comboBoxAbComDataMagicID.Items.Add(line);
                comboBoxMagicMagicID.Items.Add(line);
                comboBoxGFMagicID.Items.Add(line);
                comboBoxGFAttacksMagicID.Items.Add(line);
                comboBoxEnemyAttacksMagicID.Items.Add(line);
            }

            #endregion

            #region Load Attack Type List

            string[] attackTypeList = Properties.Resources.Attack_Type_List.Split('\n');
            foreach (var line in attackTypeList)
            {
                comboBoxRenzoFinAttackType.Items.Add(line);
                comboBoxBlueMagicAttackType.Items.Add(line);
                comboBoxDuelAttackType.Items.Add(line);
                comboBoxCombineAttackType.Items.Add(line);
                comboBoxShotAttackType.Items.Add(line);
                comboBoxTempCharLBAttackType.Items.Add(line);
                comboBoxBattleItemsAttackType.Items.Add(line);
                comboBoxWeaponsAttackType.Items.Add(line);
                comboBoxAbComDataAttackType.Items.Add(line);
                comboBoxMagicAttackType.Items.Add(line);
                comboBoxGFAttackType.Items.Add(line);
                comboBoxGFAttacksAttackType.Items.Add(line);
                comboBoxEnemyAttacksAttackType.Items.Add(line);
            }

            #endregion

            #region Disable Objects

            //for disabling save and tooltips buttons when no file is open
            saveToolStripMenuItem.Enabled = false;
            saveAsToolStripMenuItem.Enabled = false;
            saveAsToolStripButton.Enabled = false;
            saveToolStripButton.Enabled = false;

            //disable charts buttons when no file is open
            buttonCharEXPChart.Enabled = false;
            buttonCharHPChart.Enabled = false;
            buttonCharSTRChart.Enabled = false;
            buttonCharVITChart.Enabled = false;
            buttonCharMAGChart.Enabled = false;
            buttonCharSPRChart.Enabled = false;
            buttonCharSPDChart.Enabled = false;
            buttonCharLUCKChart.Enabled = false;
            buttonGFDamageChart.Enabled = false;
            buttonMagicDamageChart.Enabled = false;
            buttonEnemyAttacksDamageChart.Enabled = false;
            buttonCharEXPFormula.Enabled = false;
            buttonCharHPFormula.Enabled = false;
            buttonCharSTRFormula.Enabled = false;
            buttonCharVITFormula.Enabled = false;
            buttonCharMAGFormula.Enabled = false;
            buttonCharSPRFormula.Enabled = false;
            buttonCharSPDFormula.Enabled = false;
            buttonCharLUCKFormula.Enabled = false;
            buttonGFDamageFormula.Enabled = false;
            buttonMagicDamageFormula.Enabled = false;
            buttonEnemyAttacksDamageFormula.Enabled = false;

            //this is for enabling the switching of listboxes in the ability section
            listBoxAbStats.Visible = false;
            listBoxAbJun.Visible = false;
            listBoxAbComData.Visible = false;
            listBoxAbCom.Visible = false;
            listBoxAbGF.Visible = false;
            listBoxAbParty.Visible = false;
            listBoxAbMenu.Visible = false;

            tabControlAbilities.SelectedIndexChanged += new EventHandler(tabControlAbilities_SelectedIndexChanged);
            FormClosing += new FormClosingEventHandler(MainForm_FormClosing);

            #endregion

            DisableTabStop(this);

            _backup = $"{AppDomain.CurrentDomain.BaseDirectory}\\tooltips.bin";
            if (File.Exists(_backup))
            {
                deleteTooltipsToolStripMenuItem.Enabled = true;
                deleteTooltipsToolStripButton.Enabled = true;
            }

            #region EVENT HANDLERS

            #region Magic

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
            trackBarJStatDefense.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Magic(24, trackBarJStatDefense.Value);
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

            #region J-GFs

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
            numericUpDownGFEXP.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_GF(29, numericUpDownGFEXP.Value);
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
            checkBoxGFConfusion.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GF(7, 0x40, 0, 2);
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

            #region Non-J GFs attacks

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

            #region Weapons

            checkBoxWeaponsRenzoFinRough.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Weapons(0, 0x01, 0);
            checkBoxWeaponsRenzoFinFated.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Weapons(0, 0x02, 0);
            checkBoxWeaponsRenzoFinBlasting.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Weapons(0, 0x04, 0);
            checkBoxWeaponsRenzoFinLion.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Weapons(0, 0x08, 0);
            comboBoxWeaponsCharacterID.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_Weapons(1, comboBoxWeaponsCharacterID.SelectedIndex);
            comboBoxWeaponsAttackType.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_Weapons(2, comboBoxWeaponsAttackType.SelectedIndex);
            numericUpDownWeaponsAttackPower.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Weapons(3, numericUpDownWeaponsAttackPower.Value);
            numericUpDownWeaponsAttackParam.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Weapons(4, numericUpDownWeaponsAttackParam.Value);
            numericUpDownWeaponsSTRBonus.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Weapons(5, numericUpDownWeaponsSTRBonus.Value);
            numericUpDownWeaponsTier.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Weapons(6, numericUpDownWeaponsTier.Value);
            numericUpDownWeaponsCrit.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Weapons(7, numericUpDownWeaponsCrit.Value);
            numericUpDownWeaponsMelee.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Weapons(8, numericUpDownWeaponsMelee.Value);

            #endregion

            #region Characters

            textBoxCharName.TextChanged += (sender, args) => KernelWorker.UpdateVariable_Characters(34, textBoxCharName.Text);
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

            #region Enemy attacks

            comboBoxEnemyAttacksMagicID.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(0, comboBoxEnemyAttacksMagicID.SelectedIndex);
            numericUpDownEnemyAttacksCamera.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(1, numericUpDownEnemyAttacksCamera.Value);
            checkBoxEnemyAttacksCamera.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(2, checkBoxEnemyAttacksCamera.Checked);
            comboBoxEnemyAttacksAttackType.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(3, comboBoxEnemyAttacksAttackType.SelectedIndex);
            numericUpDownEnemyAttacksAttackPower.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(4, numericUpDownEnemyAttacksAttackPower.Value);
            checkBoxEnemyAttacksFlagShelled.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(5, 0x01);
            checkBoxEnemyAttacksFlag2.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(5, 0x02);
            checkBoxEnemyAttacksFlag3.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(5, 0x04);
            checkBoxEnemyAttacksFlagBreakDamageLimit.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(5, 0x08);
            checkBoxEnemyAttacksFlagReflected.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(5, 0x10);
            checkBoxEnemyAttacksFlag6.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(5, 0x20);
            checkBoxEnemyAttacksFlag7.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(5, 0x40);
            checkBoxEnemyAttacksFlag8.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(5, 0x80);
            comboBoxEnemyAttacksElement.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(6, EnemyAttacks_GetElement(comboBoxEnemyAttacksElement.SelectedIndex));
            numericUpDownEnemyAttacksStatusAttack.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(7, numericUpDownEnemyAttacksStatusAttack.Value);
            numericUpDownEnemyAttacksAttackParam.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(8, numericUpDownEnemyAttacksAttackParam.Value);
            checkBoxEnemyAttacksSleep.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(9, 0x01, 1);
            checkBoxEnemyAttacksHaste.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(9, 0x02, 1);
            checkBoxEnemyAttacksSlow.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(9, 0x04, 1);
            checkBoxEnemyAttacksStop.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(9, 0x08, 1);
            checkBoxEnemyAttacksRegen.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(9, 0x10, 1);
            checkBoxEnemyAttacksProtect.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(9, 0x20, 1);
            checkBoxEnemyAttacksShell.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(9, 0x40, 1);
            checkBoxEnemyAttacksReflect.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(9, 0x80, 1);
            checkBoxEnemyAttacksAura.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(9, 0x01, 2);
            checkBoxEnemyAttacksCurse.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(9, 0x02, 2);
            checkBoxEnemyAttacksDoom.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(9, 0x04, 2);
            checkBoxEnemyAttacksInvincible.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(9, 0x08, 2);
            checkBoxEnemyAttacksPetrifying.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(9, 0x10, 2);
            checkBoxEnemyAttacksFloat.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(9, 0x20, 2);
            checkBoxEnemyAttacksConfusion.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(9, 0x40, 2);
            checkBoxEnemyAttacksDrain.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(9, 0x80, 2);
            checkBoxEnemyAttacksEject.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(9, 0x01, 3);
            checkBoxEnemyAttacksDouble.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(9, 0x02, 3);
            checkBoxEnemyAttacksTriple.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(9, 0x04, 3);
            checkBoxEnemyAttacksDefend.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(9, 0x08, 3);
            checkBoxEnemyAttacksUnk1.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(9, 0x10, 3);
            checkBoxEnemyAttacksUnk2.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(9, 0x20, 3);
            checkBoxEnemyAttacksCharged.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(9, 0x40, 3);
            checkBoxEnemyAttacksBackAttack.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(9, 0x80, 3);
            checkBoxEnemyAttacksVit0.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(9, 0x01, 4);
            checkBoxEnemyAttacksAngelWing.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(9, 0x02, 4);
            checkBoxEnemyAttacksUnk3.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(9, 0x04, 4);
            checkBoxEnemyAttacksUnk4.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(9, 0x08, 4);
            checkBoxEnemyAttacksUnk5.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(9, 0x10, 4);
            checkBoxEnemyAttacksUnk6.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(9, 0x20, 4);
            checkBoxEnemyAttacksHasMagic.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(9, 0x40, 4);
            checkBoxEnemyAttacksSummonGF.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(9, 0x80, 4);
            checkBoxEnemyAttacksDeath.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(9, 0x01, 0);
            checkBoxEnemyAttacksPoison.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(9, 0x02, 0);
            checkBoxEnemyAttacksPetrify.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(9, 0x04, 0);
            checkBoxEnemyAttacksDarkness.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(9, 0x08, 0);
            checkBoxEnemyAttacksSilence.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(9, 0x10, 0);
            checkBoxEnemyAttacksBerserk.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(9, 0x20, 0);
            checkBoxEnemyAttacksZombie.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(9, 0x40, 0);
            checkBoxEnemyAttacksUnk7.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_EnemyAttacks(9, 0x80, 0);

            #endregion

            #region Blue magic

            textBoxBlueMagicName.TextChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagic(6, textBoxBlueMagicName.Text);
            textBoxBlueMagicDes.TextChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagic(7, textBoxBlueMagicDes.Text);
            numericUpDownCharCrisisLevelHP.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagic(0, numericUpDownCharCrisisLevelHP.Value);
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
            numericUpDownBlueMagicCrit.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagic(5, numericUpDownBlueMagicCrit.Value);

            #endregion

            #region Blue magic params

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
            numericUpDownBlueMagicCL1AttackParam.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(2, numericUpDownBlueMagicCL1AttackParam.Value);

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
            numericUpDownBlueMagicCL2AttackParam.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(4, numericUpDownBlueMagicCL2AttackParam.Value);

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
            numericUpDownBlueMagicCL3AttackParam.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(6, numericUpDownBlueMagicCL3AttackParam.Value);

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
            numericUpDownBlueMagicCL4AttackParam.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_BlueMagicParam(8, numericUpDownBlueMagicCL4AttackParam.Value);

            #endregion

            #region Stats increment abilities

            numericUpDownAbStatsAP.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_StatPercentageAbilities(0, numericUpDownAbStatsAP.Value);
            comboBoxAbStatsStatToIncrease.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_StatPercentageAbilities(1, comboBoxAbStatsStatToIncrease.SelectedIndex);
            trackBarAbStatsIncrementValue.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_StatPercentageAbilities(2, trackBarAbStatsIncrementValue.Value);

            #endregion

            #region Renzokuken finishers

            textBoxRenzoFinName.TextChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(10, textBoxRenzoFinName.Text);
            textBoxRenzoFinDes.TextChanged += (sender, args) => KernelWorker.UpdateVariable_RenzoFin(11, textBoxRenzoFinDes.Text);
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

            #region Temporary characters limit breaks

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

            #region Shot

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
            comboBoxShotItem.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(9, comboBoxShotItem.SelectedIndex);
            numericUpDownShotCrit.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(10, numericUpDownShotCrit.Value);
            checkBoxShotDeath.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(11, 0x01, 0);
            checkBoxShotPoison.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(11, 0x02, 0);
            checkBoxShotPetrify.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(11, 0x04, 0);
            checkBoxShotDarkness.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(11, 0x08, 0);
            checkBoxShotSilence.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(11, 0x10, 0);
            checkBoxShotBerserk.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(11, 0x20, 0);
            checkBoxShotZombie.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(11, 0x40, 0);
            checkBoxShotUnk7.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(11, 0x80, 0);
            checkBoxShotSleep.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(11, 0x01, 1);
            checkBoxShotHaste.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(11, 0x02, 1);
            checkBoxShotSlow.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(11, 0x04, 1);
            checkBoxShotStop.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(11, 0x08, 1);
            checkBoxShotRegen.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(11, 0x10, 1);
            checkBoxShotProtect.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(11, 0x20, 1);
            checkBoxShotShell.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(11, 0x40, 1);
            checkBoxShotReflect.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(11, 0x80, 1);
            checkBoxShotAura.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(11, 0x01, 2);
            checkBoxShotCurse.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(11, 0x02, 2);
            checkBoxShotDoom.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(11, 0x04, 2);
            checkBoxShotInvincible.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(11, 0x08, 2);
            checkBoxShotPetrifying.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(11, 0x10, 2);
            checkBoxShotFloat.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(11, 0x20, 2);
            checkBoxShotConfusion.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(11, 0x40, 2);
            checkBoxShotDrain.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(11, 0x80, 2);
            checkBoxShotEject.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(11, 0x01, 3);
            checkBoxShotDouble.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(11, 0x02, 3);
            checkBoxShotTriple.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(11, 0x04, 3);
            checkBoxShotDefend.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(11, 0x08, 3);
            checkBoxShotUnk1.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(11, 0x10, 3);
            checkBoxShotUnk2.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(11, 0x20, 3);
            checkBoxShotCharged.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(11, 0x40, 3);
            checkBoxShotBackAttack.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(11, 0x80, 3);
            checkBoxShotVit0.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(11, 0x01, 4);
            checkBoxShotAngelWing.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(11, 0x02, 4);
            checkBoxShotUnk3.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(11, 0x04, 4);
            checkBoxShotUnk4.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(11, 0x08, 4);
            checkBoxShotUnk5.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(11, 0x10, 4);
            checkBoxShotUnk6.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(11, 0x20, 4);
            checkBoxShotHasMagic.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(11, 0x40, 4);
            checkBoxShotSummonGF.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Shot(11, 0x80, 4);

            #endregion

            #region Duel

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
            comboBoxDuelButton1.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(9, Duel_GetButton1(comboBoxDuelButton1.SelectedIndex));
            checkBoxDuelFinisher.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(10, checkBoxDuelFinisher.Checked);
            comboBoxDuelButton2.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(11, Duel_GetButtonOthers(comboBoxDuelButton2.SelectedIndex));
            comboBoxDuelButton3.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(12, Duel_GetButtonOthers(comboBoxDuelButton3.SelectedIndex));
            comboBoxDuelButton4.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(13, Duel_GetButtonOthers(comboBoxDuelButton4.SelectedIndex));
            comboBoxDuelButton5.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(14, Duel_GetButtonOthers(comboBoxDuelButton5.SelectedIndex));
            checkBoxDuelDeath.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(15, 0x01, 0);
            checkBoxDuelPoison.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(15, 0x02, 0);
            checkBoxDuelPetrify.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(15, 0x04, 0);
            checkBoxDuelDarkness.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(15, 0x08, 0);
            checkBoxDuelSilence.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(15, 0x10, 0);
            checkBoxDuelBerserk.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(15, 0x20, 0);
            checkBoxDuelZombie.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(15, 0x40, 0);
            checkBoxDuelUnk7.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(15, 0x80, 0);
            checkBoxDuelSleep.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(15, 0x01, 1);
            checkBoxDuelHaste.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(15, 0x02, 1);
            checkBoxDuelSlow.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(15, 0x04, 1);
            checkBoxDuelStop.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(15, 0x08, 1);
            checkBoxDuelRegen.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(15, 0x10, 1);
            checkBoxDuelProtect.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(15, 0x20, 1);
            checkBoxDuelShell.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(15, 0x40, 1);
            checkBoxDuelReflect.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(15, 0x80, 1);
            checkBoxDuelAura.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(15, 0x01, 2);
            checkBoxDuelCurse.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(15, 0x02, 2);
            checkBoxDuelDoom.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(15, 0x04, 2);
            checkBoxDuelInvincible.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(15, 0x08, 2);
            checkBoxDuelPetrifying.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(15, 0x10, 2);
            checkBoxDuelFloat.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(15, 0x20, 2);
            checkBoxDuelConfusion.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(15, 0x40, 2);
            checkBoxDuelDrain.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(15, 0x80, 2);
            checkBoxDuelEject.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(15, 0x01, 3);
            checkBoxDuelDouble.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(15, 0x02, 3);
            checkBoxDuelTriple.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(15, 0x04, 3);
            checkBoxDuelDefend.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(15, 0x08, 3);
            checkBoxDuelUnk1.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(15, 0x10, 3);
            checkBoxDuelUnk2.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(15, 0x20, 3);
            checkBoxDuelCharged.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(15, 0x40, 3);
            checkBoxDuelBackAttack.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(15, 0x80, 3);
            checkBoxDuelVit0.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(15, 0x01, 4);
            checkBoxDuelAngelWing.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(15, 0x02, 4);
            checkBoxDuelUnk3.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(15, 0x04, 4);
            checkBoxDuelUnk4.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(15, 0x08, 4);
            checkBoxDuelUnk5.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(15, 0x10, 4);
            checkBoxDuelUnk6.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(15, 0x20, 4);
            checkBoxDuelHasMagic.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(15, 0x40, 4);
            checkBoxDuelSummonGF.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Duel(15, 0x80, 4);

            //duel params
            comboBoxDuelMove0.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(0, comboBoxDuelMove0.SelectedIndex);
            comboBoxDuelMove1.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(1, comboBoxDuelMove1.SelectedIndex);
            comboBoxDuelMove2.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(2, comboBoxDuelMove2.SelectedIndex);
            comboBoxDuelMove3.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(3, comboBoxDuelMove3.SelectedIndex);
            comboBoxDuelMove4.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(4, comboBoxDuelMove4.SelectedIndex);
            comboBoxDuelMove5.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(5, comboBoxDuelMove5.SelectedIndex);
            comboBoxDuelMove6.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(6, comboBoxDuelMove6.SelectedIndex);
            comboBoxDuelMove7.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(7, comboBoxDuelMove7.SelectedIndex);
            comboBoxDuelMove8.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(8, comboBoxDuelMove8.SelectedIndex);
            comboBoxDuelMove9.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(9, comboBoxDuelMove9.SelectedIndex);
            comboBoxDuelMove10.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(10, comboBoxDuelMove10.SelectedIndex);
            comboBoxDuelMove11.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(11, comboBoxDuelMove11.SelectedIndex);
            comboBoxDuelMove12.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(12, comboBoxDuelMove12.SelectedIndex);
            comboBoxDuelMove13.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(13, comboBoxDuelMove13.SelectedIndex);
            comboBoxDuelMove14.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(14, comboBoxDuelMove14.SelectedIndex);
            comboBoxDuelMove15.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(15, comboBoxDuelMove15.SelectedIndex);
            comboBoxDuelMove16.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(16, comboBoxDuelMove16.SelectedIndex);
            comboBoxDuelMove17.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(17, comboBoxDuelMove17.SelectedIndex);
            comboBoxDuelMove18.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(18, comboBoxDuelMove18.SelectedIndex);
            comboBoxDuelMove19.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(19, comboBoxDuelMove19.SelectedIndex);
            comboBoxDuelMove20.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(20, comboBoxDuelMove20.SelectedIndex);
            comboBoxDuelMove21.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(21, comboBoxDuelMove21.SelectedIndex);
            comboBoxDuelMove22.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(22, comboBoxDuelMove22.SelectedIndex);
            comboBoxDuelMove23.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(23, comboBoxDuelMove23.SelectedIndex);
            comboBoxDuelMove24.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(24, comboBoxDuelMove24.SelectedIndex);
            numericUpDownDuelNextSeq0_1.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(25, numericUpDownDuelNextSeq0_1.Value);
            numericUpDownDuelNextSeq0_2.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(26, numericUpDownDuelNextSeq0_2.Value);
            numericUpDownDuelNextSeq0_3.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(27, numericUpDownDuelNextSeq0_3.Value);
            numericUpDownDuelNextSeq1_1.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(28, numericUpDownDuelNextSeq1_1.Value);
            numericUpDownDuelNextSeq1_2.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(29, numericUpDownDuelNextSeq1_2.Value);
            numericUpDownDuelNextSeq1_3.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(30, numericUpDownDuelNextSeq1_3.Value);
            numericUpDownDuelNextSeq2_1.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(31, numericUpDownDuelNextSeq2_1.Value);
            numericUpDownDuelNextSeq2_2.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(32, numericUpDownDuelNextSeq2_2.Value);
            numericUpDownDuelNextSeq2_3.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(33, numericUpDownDuelNextSeq2_3.Value);
            numericUpDownDuelNextSeq3_1.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(34, numericUpDownDuelNextSeq3_1.Value);
            numericUpDownDuelNextSeq3_2.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(35, numericUpDownDuelNextSeq3_2.Value);
            numericUpDownDuelNextSeq3_3.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(36, numericUpDownDuelNextSeq3_3.Value);
            numericUpDownDuelNextSeq4_1.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(37, numericUpDownDuelNextSeq4_1.Value);
            numericUpDownDuelNextSeq4_2.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(38, numericUpDownDuelNextSeq4_2.Value);
            numericUpDownDuelNextSeq4_3.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(39, numericUpDownDuelNextSeq4_3.Value);
            numericUpDownDuelNextSeq5_1.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(40, numericUpDownDuelNextSeq5_1.Value);
            numericUpDownDuelNextSeq5_2.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(41, numericUpDownDuelNextSeq5_2.Value);
            numericUpDownDuelNextSeq5_3.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(42, numericUpDownDuelNextSeq5_3.Value);
            numericUpDownDuelNextSeq6_1.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(43, numericUpDownDuelNextSeq6_1.Value);
            numericUpDownDuelNextSeq6_2.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(44, numericUpDownDuelNextSeq6_2.Value);
            numericUpDownDuelNextSeq6_3.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(45, numericUpDownDuelNextSeq6_3.Value);
            numericUpDownDuelNextSeq7_1.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(46, numericUpDownDuelNextSeq7_1.Value);
            numericUpDownDuelNextSeq7_2.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(47, numericUpDownDuelNextSeq7_2.Value);
            numericUpDownDuelNextSeq7_3.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(48, numericUpDownDuelNextSeq7_3.Value);
            numericUpDownDuelNextSeq8_1.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(49, numericUpDownDuelNextSeq8_1.Value);
            numericUpDownDuelNextSeq8_2.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(50, numericUpDownDuelNextSeq8_2.Value);
            numericUpDownDuelNextSeq8_3.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(51, numericUpDownDuelNextSeq8_3.Value);
            numericUpDownDuelNextSeq9_1.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(52, numericUpDownDuelNextSeq9_1.Value);
            numericUpDownDuelNextSeq9_2.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(53, numericUpDownDuelNextSeq9_2.Value);
            numericUpDownDuelNextSeq9_3.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(54, numericUpDownDuelNextSeq9_3.Value);
            numericUpDownDuelNextSeq10_1.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(55, numericUpDownDuelNextSeq10_1.Value);
            numericUpDownDuelNextSeq10_2.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(56, numericUpDownDuelNextSeq10_2.Value);
            numericUpDownDuelNextSeq10_3.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(57, numericUpDownDuelNextSeq10_3.Value);
            numericUpDownDuelNextSeq11_1.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(58, numericUpDownDuelNextSeq11_1.Value);
            numericUpDownDuelNextSeq11_2.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(59, numericUpDownDuelNextSeq11_2.Value);
            numericUpDownDuelNextSeq11_3.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(60, numericUpDownDuelNextSeq11_3.Value);
            numericUpDownDuelNextSeq12_1.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(61, numericUpDownDuelNextSeq12_1.Value);
            numericUpDownDuelNextSeq12_2.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(62, numericUpDownDuelNextSeq12_2.Value);
            numericUpDownDuelNextSeq12_3.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(63, numericUpDownDuelNextSeq12_3.Value);
            numericUpDownDuelNextSeq13_1.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(64, numericUpDownDuelNextSeq13_1.Value);
            numericUpDownDuelNextSeq13_2.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(65, numericUpDownDuelNextSeq13_2.Value);
            numericUpDownDuelNextSeq13_3.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(66, numericUpDownDuelNextSeq13_3.Value);
            numericUpDownDuelNextSeq14_1.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(67, numericUpDownDuelNextSeq14_1.Value);
            numericUpDownDuelNextSeq14_2.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(68, numericUpDownDuelNextSeq14_2.Value);
            numericUpDownDuelNextSeq14_3.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(69, numericUpDownDuelNextSeq14_3.Value);
            numericUpDownDuelNextSeq15_1.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(70, numericUpDownDuelNextSeq15_1.Value);
            numericUpDownDuelNextSeq15_2.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(71, numericUpDownDuelNextSeq15_2.Value);
            numericUpDownDuelNextSeq15_3.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(72, numericUpDownDuelNextSeq15_3.Value);
            numericUpDownDuelNextSeq16_1.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(73, numericUpDownDuelNextSeq16_1.Value);
            numericUpDownDuelNextSeq16_2.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(74, numericUpDownDuelNextSeq16_2.Value);
            numericUpDownDuelNextSeq16_3.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(75, numericUpDownDuelNextSeq16_3.Value);
            numericUpDownDuelNextSeq17_1.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(76, numericUpDownDuelNextSeq17_1.Value);
            numericUpDownDuelNextSeq17_2.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(77, numericUpDownDuelNextSeq17_2.Value);
            numericUpDownDuelNextSeq17_3.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(78, numericUpDownDuelNextSeq17_3.Value);
            numericUpDownDuelNextSeq18_1.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(79, numericUpDownDuelNextSeq18_1.Value);
            numericUpDownDuelNextSeq18_2.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(80, numericUpDownDuelNextSeq18_2.Value);
            numericUpDownDuelNextSeq18_3.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(81, numericUpDownDuelNextSeq18_3.Value);
            numericUpDownDuelNextSeq19_1.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(82, numericUpDownDuelNextSeq19_1.Value);
            numericUpDownDuelNextSeq19_2.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(83, numericUpDownDuelNextSeq19_2.Value);
            numericUpDownDuelNextSeq19_3.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(84, numericUpDownDuelNextSeq19_3.Value);
            numericUpDownDuelNextSeq20_1.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(85, numericUpDownDuelNextSeq20_1.Value);
            numericUpDownDuelNextSeq20_2.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(86, numericUpDownDuelNextSeq20_2.Value);
            numericUpDownDuelNextSeq20_3.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(87, numericUpDownDuelNextSeq20_3.Value);
            numericUpDownDuelNextSeq21_1.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(88, numericUpDownDuelNextSeq21_1.Value);
            numericUpDownDuelNextSeq21_2.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(89, numericUpDownDuelNextSeq21_2.Value);
            numericUpDownDuelNextSeq21_2.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(90, numericUpDownDuelNextSeq21_2.Value);
            numericUpDownDuelNextSeq22_1.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(91, numericUpDownDuelNextSeq22_1.Value);
            numericUpDownDuelNextSeq22_2.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(92, numericUpDownDuelNextSeq22_2.Value);
            numericUpDownDuelNextSeq22_3.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(93, numericUpDownDuelNextSeq22_3.Value);
            numericUpDownDuelNextSeq23_1.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(94, numericUpDownDuelNextSeq23_1.Value);
            numericUpDownDuelNextSeq23_2.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(95, numericUpDownDuelNextSeq23_2.Value);
            numericUpDownDuelNextSeq23_3.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(96, numericUpDownDuelNextSeq23_3.Value);
            numericUpDownDuelNextSeq24_1.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(97, numericUpDownDuelNextSeq24_1.Value);
            numericUpDownDuelNextSeq24_2.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(98, numericUpDownDuelNextSeq24_2.Value);
            numericUpDownDuelNextSeq24_3.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_DuelParams(99, numericUpDownDuelNextSeq24_3.Value);

            #endregion

            #region Combine

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

            #region Battle items

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
            numericUpDownBattleItemsAttackParam.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(7, numericUpDownBattleItemsAttackParam.Value);
            numericUpDownBattleItemsHitCount.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(8, numericUpDownBattleItemsHitCount.Value);
            comboBoxBattleItemsElement.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_BattleItems(9, BattleItems_GetElement(comboBoxBattleItemsElement.SelectedIndex));

            #endregion

            #region Slot array

            numericUpDownSlotsArray1.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotArray(0, numericUpDownSlotsArray1.Value);
            numericUpDownSlotsArray2.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotArray(1, numericUpDownSlotsArray2.Value);
            numericUpDownSlotsArray3.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotArray(2, numericUpDownSlotsArray3.Value);
            numericUpDownSlotsArray4.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotArray(3, numericUpDownSlotsArray4.Value);
            numericUpDownSlotsArray5.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotArray(4, numericUpDownSlotsArray5.Value);
            numericUpDownSlotsArray6.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotArray(5, numericUpDownSlotsArray6.Value);
            numericUpDownSlotsArray7.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotArray(6, numericUpDownSlotsArray7.Value);
            numericUpDownSlotsArray8.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotArray(7, numericUpDownSlotsArray8.Value);
            numericUpDownSlotsArray9.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotArray(8, numericUpDownSlotsArray9.Value);
            numericUpDownSlotsArray10.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotArray(9, numericUpDownSlotsArray10.Value);
            numericUpDownSlotsArray11.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotArray(10, numericUpDownSlotsArray11.Value);
            numericUpDownSlotsArray12.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotArray(11, numericUpDownSlotsArray12.Value);
            numericUpDownSlotsArray13.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotArray(12, numericUpDownSlotsArray13.Value);
            numericUpDownSlotsArray14.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotArray(13, numericUpDownSlotsArray14.Value);
            numericUpDownSlotsArray15.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotArray(14, numericUpDownSlotsArray15.Value);
            numericUpDownSlotsArray16.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotArray(15, numericUpDownSlotsArray16.Value);
            numericUpDownSlotsArray17.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotArray(16, numericUpDownSlotsArray17.Value);
            numericUpDownSlotsArray18.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotArray(17, numericUpDownSlotsArray18.Value);
            numericUpDownSlotsArray19.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotArray(18, numericUpDownSlotsArray19.Value);
            numericUpDownSlotsArray20.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotArray(19, numericUpDownSlotsArray20.Value);
            numericUpDownSlotsArray21.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotArray(20, numericUpDownSlotsArray21.Value);
            numericUpDownSlotsArray22.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotArray(21, numericUpDownSlotsArray22.Value);
            numericUpDownSlotsArray23.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotArray(22, numericUpDownSlotsArray23.Value);
            numericUpDownSlotsArray24.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotArray(23, numericUpDownSlotsArray24.Value);
            numericUpDownSlotsArray25.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotArray(24, numericUpDownSlotsArray25.Value);
            numericUpDownSlotsArray26.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotArray(25, numericUpDownSlotsArray26.Value);
            numericUpDownSlotsArray27.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotArray(26, numericUpDownSlotsArray27.Value);
            numericUpDownSlotsArray28.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotArray(27, numericUpDownSlotsArray28.Value);
            numericUpDownSlotsArray29.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotArray(28, numericUpDownSlotsArray29.Value);
            numericUpDownSlotsArray30.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotArray(29, numericUpDownSlotsArray30.Value);
            numericUpDownSlotsArray31.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotArray(30, numericUpDownSlotsArray31.Value);
            numericUpDownSlotsArray32.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotArray(31, numericUpDownSlotsArray32.Value);
            numericUpDownSlotsArray33.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotArray(32, numericUpDownSlotsArray33.Value);
            numericUpDownSlotsArray34.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotArray(33, numericUpDownSlotsArray34.Value);
            numericUpDownSlotsArray35.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotArray(34, numericUpDownSlotsArray35.Value);
            numericUpDownSlotsArray36.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotArray(35, numericUpDownSlotsArray36.Value);
            numericUpDownSlotsArray37.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotArray(36, numericUpDownSlotsArray37.Value);
            numericUpDownSlotsArray38.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotArray(37, numericUpDownSlotsArray38.Value);
            numericUpDownSlotsArray39.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotArray(38, numericUpDownSlotsArray39.Value);
            numericUpDownSlotsArray40.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotArray(39, numericUpDownSlotsArray40.Value);
            numericUpDownSlotsArray41.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotArray(40, numericUpDownSlotsArray41.Value);
            numericUpDownSlotsArray42.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotArray(41, numericUpDownSlotsArray42.Value);
            numericUpDownSlotsArray43.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotArray(42, numericUpDownSlotsArray43.Value);
            numericUpDownSlotsArray44.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotArray(43, numericUpDownSlotsArray44.Value);
            numericUpDownSlotsArray45.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotArray(44, numericUpDownSlotsArray45.Value);
            numericUpDownSlotsArray46.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotArray(45, numericUpDownSlotsArray46.Value);
            numericUpDownSlotsArray47.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotArray(46, numericUpDownSlotsArray47.Value);
            numericUpDownSlotsArray48.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotArray(47, numericUpDownSlotsArray48.Value);
            numericUpDownSlotsArray49.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotArray(48, numericUpDownSlotsArray49.Value);
            numericUpDownSlotsArray50.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotArray(49, numericUpDownSlotsArray50.Value);
            numericUpDownSlotsArray51.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotArray(50, numericUpDownSlotsArray51.Value);
            numericUpDownSlotsArray52.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotArray(51, numericUpDownSlotsArray52.Value);
            numericUpDownSlotsArray53.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotArray(52, numericUpDownSlotsArray53.Value);
            numericUpDownSlotsArray54.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotArray(53, numericUpDownSlotsArray54.Value);
            numericUpDownSlotsArray55.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotArray(54, numericUpDownSlotsArray55.Value);
            numericUpDownSlotsArray56.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotArray(55, numericUpDownSlotsArray56.Value);
            numericUpDownSlotsArray57.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotArray(56, numericUpDownSlotsArray57.Value);
            numericUpDownSlotsArray58.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotArray(57, numericUpDownSlotsArray58.Value);
            numericUpDownSlotsArray59.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotArray(58, numericUpDownSlotsArray59.Value);
            numericUpDownSlotsArray60.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_SlotArray(59, numericUpDownSlotsArray60.Value);

            #endregion

            #region Slot sets

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

            #region Devour

            comboBoxDevourHealDmg.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_Devour(0, Devour_GetHealDmg(comboBoxDevourHealDmg.SelectedIndex));
            checkBoxDevourHP1.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Devour(1, 0x01);
            checkBoxDevourHP2.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Devour(1, 0x02);
            checkBoxDevourHP3.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Devour(1, 0x04);
            checkBoxDevourHP4.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Devour(1, 0x08);
            checkBoxDevourHP5.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Devour(1, 0x10);
            checkBoxDevourSleep.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Devour(2, 0x01, 1);
            checkBoxDevourHaste.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Devour(2, 0x02, 1);
            checkBoxDevourSlow.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Devour(2, 0x04, 1);
            checkBoxDevourStop.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Devour(2, 0x08, 1);
            checkBoxDevourRegen.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Devour(2, 0x10, 1);
            checkBoxDevourProtect.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Devour(2, 0x20, 1);
            checkBoxDevourShell.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Devour(2, 0x40, 1);
            checkBoxDevourReflect.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Devour(2, 0x80, 1);
            checkBoxDevourAura.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Devour(2, 0x01, 2);
            checkBoxDevourCurse.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Devour(2, 0x02, 2);
            checkBoxDevourDoom.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Devour(2, 0x04, 2);
            checkBoxDevourInvincible.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Devour(2, 0x08, 2);
            checkBoxDevourPetrifying.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Devour(2, 0x10, 2);
            checkBoxDevourFloat.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Devour(2, 0x20, 2);
            checkBoxDevourConfusion.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Devour(2, 0x40, 2);
            checkBoxDevourDrain.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Devour(2, 0x80, 2);
            checkBoxDevourEject.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Devour(2, 0x01, 3);
            checkBoxDevourDouble.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Devour(2, 0x02, 3);
            checkBoxDevourTriple.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Devour(2, 0x04, 3);
            checkBoxDevourDefend.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Devour(2, 0x08, 3);
            checkBoxDevourUnk1.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Devour(2, 0x10, 3);
            checkBoxDevourUnk2.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Devour(2, 0x20, 3);
            checkBoxDevourCharged.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Devour(2, 0x40, 3);
            checkBoxDevourBackAttack.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Devour(2, 0x80, 3);
            checkBoxDevourVit0.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Devour(2, 0x01, 4);
            checkBoxDevourAngelWing.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Devour(2, 0x02, 4);
            checkBoxDevourUnk3.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Devour(2, 0x04, 4);
            checkBoxDevourUnk4.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Devour(2, 0x08, 4);
            checkBoxDevourUnk5.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Devour(2, 0x10, 4);
            checkBoxDevourUnk6.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Devour(2, 0x20, 4);
            checkBoxDevourHasMagic.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Devour(2, 0x40, 4);
            checkBoxDevourSummonGF.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Devour(2, 0x80, 4);
            checkBoxDevourDeath.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Devour(2, 0x01, 0);
            checkBoxDevourPoison.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Devour(2, 0x02, 0);
            checkBoxDevourPetrify.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Devour(2, 0x04, 0);
            checkBoxDevourDarkness.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Devour(2, 0x08, 0);
            checkBoxDevourSilence.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Devour(2, 0x10, 0);
            checkBoxDevourBerserk.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Devour(2, 0x20, 0);
            checkBoxDevourZombie.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Devour(2, 0x40, 0);
            checkBoxDevourUnk7.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Devour(2, 0x80, 0);
            checkBoxDevourStat1.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Devour(3, 0x01);
            checkBoxDevourStat2.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Devour(3, 0x02);
            checkBoxDevourStat3.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Devour(3, 0x04);
            checkBoxDevourStat4.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Devour(3, 0x08);
            checkBoxDevourStat5.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Devour(3, 0x10);
            checkBoxDevourStat6.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_Devour(3, 0x20);
            numericUpDownDevourHP.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Devour(4, numericUpDownDevourHP.Value);

            #endregion

            #region Misc

            numericUpDownStatusTimer1.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(0, numericUpDownStatusTimer1.Value);
            numericUpDownStatusTimer2.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(1, numericUpDownStatusTimer2.Value);
            numericUpDownStatusTimer3.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(2, numericUpDownStatusTimer3.Value);
            numericUpDownStatusTimer4.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(3, numericUpDownStatusTimer4.Value);
            numericUpDownStatusTimer5.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(4, numericUpDownStatusTimer5.Value);
            numericUpDownStatusTimer6.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(5, numericUpDownStatusTimer6.Value);
            numericUpDownStatusTimer7.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(6, numericUpDownStatusTimer7.Value);
            numericUpDownStatusTimer8.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(7, numericUpDownStatusTimer8.Value);
            numericUpDownStatusTimer9.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(8, numericUpDownStatusTimer9.Value);
            numericUpDownStatusTimer10.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(9, numericUpDownStatusTimer10.Value);
            numericUpDownStatusTimer11.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(10, numericUpDownStatusTimer11.Value);
            numericUpDownStatusTimer12.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(11, numericUpDownStatusTimer12.Value);
            numericUpDownStatusTimer13.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(12, numericUpDownStatusTimer13.Value);
            numericUpDownStatusTimer14.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(13, numericUpDownStatusTimer14.Value);
            numericUpDownATBMult.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(14, numericUpDownATBMult.Value);
            numericUpDownDeadTimer.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(15, numericUpDownDeadTimer.Value);
            numericUpDownStatusLimit1.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(16, numericUpDownStatusLimit1.Value);
            numericUpDownStatusLimit1.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(16, numericUpDownStatusLimit1.Value);
            numericUpDownStatusLimit2.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(17, numericUpDownStatusLimit2.Value);
            numericUpDownStatusLimit3.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(18, numericUpDownStatusLimit3.Value);
            numericUpDownStatusLimit4.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(19, numericUpDownStatusLimit4.Value);
            numericUpDownStatusLimit5.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(20, numericUpDownStatusLimit5.Value);
            numericUpDownStatusLimit6.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(21, numericUpDownStatusLimit6.Value);
            numericUpDownStatusLimit7.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(22, numericUpDownStatusLimit7.Value);
            numericUpDownStatusLimit8.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(23, numericUpDownStatusLimit8.Value);
            numericUpDownStatusLimit9.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(24, numericUpDownStatusLimit9.Value);
            numericUpDownStatusLimit10.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(25, numericUpDownStatusLimit10.Value);
            numericUpDownStatusLimit11.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(26, numericUpDownStatusLimit11.Value);
            numericUpDownStatusLimit12.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(27, numericUpDownStatusLimit12.Value);
            numericUpDownStatusLimit13.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(28, numericUpDownStatusLimit13.Value);
            numericUpDownStatusLimit14.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(29, numericUpDownStatusLimit14.Value);
            numericUpDownStatusLimit15.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(30, numericUpDownStatusLimit15.Value);
            numericUpDownStatusLimit16.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(31, numericUpDownStatusLimit16.Value);
            numericUpDownStatusLimit17.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(32, numericUpDownStatusLimit17.Value);
            numericUpDownStatusLimit18.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(33, numericUpDownStatusLimit18.Value);
            numericUpDownStatusLimit19.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(34, numericUpDownStatusLimit19.Value);
            numericUpDownStatusLimit20.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(35, numericUpDownStatusLimit20.Value);
            numericUpDownStatusLimit21.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(36, numericUpDownStatusLimit21.Value);
            numericUpDownStatusLimit22.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(37, numericUpDownStatusLimit22.Value);
            numericUpDownStatusLimit23.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(38, numericUpDownStatusLimit23.Value);
            numericUpDownStatusLimit24.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(39, numericUpDownStatusLimit24.Value);
            numericUpDownStatusLimit25.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(40, numericUpDownStatusLimit25.Value);
            numericUpDownStatusLimit26.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(41, numericUpDownStatusLimit26.Value);
            numericUpDownStatusLimit27.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(42, numericUpDownStatusLimit27.Value);
            numericUpDownStatusLimit28.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(43, numericUpDownStatusLimit28.Value);
            numericUpDownStatusLimit29.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(44, numericUpDownStatusLimit29.Value);
            numericUpDownStatusLimit30.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(45, numericUpDownStatusLimit30.Value);
            numericUpDownStatusLimit31.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(46, numericUpDownStatusLimit31.Value);
            numericUpDownStatusLimit32.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(47, numericUpDownStatusLimit32.Value);
            numericUpDownDuelTimer1.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(48, numericUpDownDuelTimer1.Value);
            numericUpDownDuelStart1.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(49, numericUpDownDuelStart1.Value);
            numericUpDownDuelTimer2.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(50, numericUpDownDuelTimer2.Value);
            numericUpDownDuelStart2.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(51, numericUpDownDuelStart2.Value);
            numericUpDownDuelTimer3.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(52, numericUpDownDuelTimer3.Value);
            numericUpDownDuelStart3.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(53, numericUpDownDuelStart3.Value);
            numericUpDownDuelTimer4.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(54, numericUpDownDuelTimer4.Value);
            numericUpDownDuelStart4.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(55, numericUpDownDuelStart4.Value);
            numericUpDownShotTimer1.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(56, numericUpDownShotTimer1.Value);
            numericUpDownShotTimer2.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(57, numericUpDownShotTimer2.Value);
            numericUpDownShotTimer3.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(58, numericUpDownShotTimer3.Value);
            numericUpDownShotTimer4.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_Misc(59, numericUpDownShotTimer4.Value);

            #endregion

            #region Command abilities data

            comboBoxAbComDataMagicID.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_CommandAbilityData(0, comboBoxAbComDataMagicID.SelectedIndex);
            comboBoxAbComDataAttackType.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_CommandAbilityData(1, comboBoxAbComDataAttackType.SelectedIndex);
            numericUpDownAbComDataAttackPower.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_CommandAbilityData(2, numericUpDownAbComDataAttackPower.Value);
            checkBoxAbComDataFlag1.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CommandAbilityData(3, 0x01);
            checkBoxAbComDataFlag2.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CommandAbilityData(3, 0x02);
            checkBoxAbComDataFlag3.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CommandAbilityData(3, 0x04);
            checkBoxAbComDataFlag4.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CommandAbilityData(3, 0x08);
            checkBoxAbComDataFlag5.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CommandAbilityData(3, 0x10);
            checkBoxAbComDataFlag6.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CommandAbilityData(3, 0x20);
            checkBoxAbComDataFlag7.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CommandAbilityData(3, 0x40);
            checkBoxAbComDataFlag8.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CommandAbilityData(3, 0x80);
            numericUpDownAbComDataHitCount.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_CommandAbilityData(4, numericUpDownAbComDataHitCount.Value);
            comboBoxAbComDataElement.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_CommandAbilityData(5, CommandAbilityData_GetElement(comboBoxAbComDataElement.SelectedIndex));
            numericUpDownAbComDataStatusAttack.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_CommandAbilityData(6, numericUpDownAbComDataStatusAttack.Value);
            checkBoxAbComDataDeath.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CommandAbilityData(7, 0x01, 0);
            checkBoxAbComDataPoison.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CommandAbilityData(7, 0x02, 0);
            checkBoxAbComDataPetrify.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CommandAbilityData(7, 0x04, 0);
            checkBoxAbComDataDarkness.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CommandAbilityData(7, 0x08, 0);
            checkBoxAbComDataSilence.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CommandAbilityData(7, 0x10, 0);
            checkBoxAbComDataBerserk.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CommandAbilityData(7, 0x20, 0);
            checkBoxAbComDataZombie.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CommandAbilityData(7, 0x40, 0);
            checkBoxAbComDataUnk7.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CommandAbilityData(7, 0x80, 0);
            checkBoxAbComDataSleep.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CommandAbilityData(7, 0x01, 1);
            checkBoxAbComDataHaste.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CommandAbilityData(7, 0x02, 1);
            checkBoxAbComDataSlow.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CommandAbilityData(7, 0x04, 1);
            checkBoxAbComDataStop.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CommandAbilityData(7, 0x08, 1);
            checkBoxAbComDataRegen.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CommandAbilityData(7, 0x10, 1);
            checkBoxAbComDataProtect.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CommandAbilityData(7, 0x20, 1);
            checkBoxAbComDataShell.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CommandAbilityData(7, 0x40, 1);
            checkBoxAbComDataReflect.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CommandAbilityData(7, 0x80, 1);
            checkBoxAbComDataAura.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CommandAbilityData(7, 0x01, 2);
            checkBoxAbComDataCurse.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CommandAbilityData(7, 0x02, 2);
            checkBoxAbComDataDoom.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CommandAbilityData(7, 0x04, 2);
            checkBoxAbComDataInvincible.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CommandAbilityData(7, 0x08, 2);
            checkBoxAbComDataPetrifying.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CommandAbilityData(7, 0x10, 2);
            checkBoxAbComDataFloat.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CommandAbilityData(7, 0x20, 2);
            checkBoxAbComDataConfusion.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CommandAbilityData(7, 0x40, 2);
            checkBoxAbComDataDrain.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CommandAbilityData(7, 0x80, 2);
            checkBoxAbComDataEject.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CommandAbilityData(7, 0x01, 3);
            checkBoxAbComDataDouble.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CommandAbilityData(7, 0x02, 3);
            checkBoxAbComDataTriple.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CommandAbilityData(7, 0x04, 3);
            checkBoxAbComDataDefend.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CommandAbilityData(7, 0x08, 3);
            checkBoxAbComDataUnk1.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CommandAbilityData(7, 0x10, 3);
            checkBoxAbComDataUnk2.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CommandAbilityData(7, 0x20, 3);
            checkBoxAbComDataCharged.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CommandAbilityData(7, 0x40, 3);
            checkBoxAbComDataBackAttack.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CommandAbilityData(7, 0x80, 3);
            checkBoxAbComDataVit0.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CommandAbilityData(7, 0x01, 4);
            checkBoxAbComDataAngelWing.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CommandAbilityData(7, 0x02, 4);
            checkBoxAbComDataUnk3.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CommandAbilityData(7, 0x04, 4);
            checkBoxAbComDataUnk4.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CommandAbilityData(7, 0x08, 4);
            checkBoxAbComDataUnk5.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CommandAbilityData(7, 0x10, 4);
            checkBoxAbComDataUnk6.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CommandAbilityData(7, 0x20, 4);
            checkBoxAbComDataHasMagic.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CommandAbilityData(7, 0x40, 4);
            checkBoxAbComDataSummonGF.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CommandAbilityData(7, 0x80, 4);

            #endregion

            #region Command abilities

            numericUpDownAbComAP.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_CommandAbility(0, numericUpDownAbComAP.Value);
            comboBoxAbComBattleCommand.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_CommandAbility(1, comboBoxAbComBattleCommand.SelectedIndex);

            #endregion

            #region Junction abilities

            numericUpDownAbJunAP.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_JunctionAbilities(0, numericUpDownAbJunAP.Value);
            checkBoxAbJunFlag1.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_JunctionAbilities(1, 0x01);
            checkBoxAbJunFlag2.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_JunctionAbilities(1, 0x02);
            checkBoxAbJunFlag3.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_JunctionAbilities(1, 0x04);
            checkBoxAbJunFlag4.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_JunctionAbilities(1, 0x08);
            checkBoxAbJunFlag5.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_JunctionAbilities(1, 0x10);
            checkBoxAbJunFlag6.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_JunctionAbilities(1, 0x20);
            checkBoxAbJunFlag7.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_JunctionAbilities(1, 0x40);
            checkBoxAbJunFlag8.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_JunctionAbilities(1, 0x80);
            checkBoxAbJunFlag9.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_JunctionAbilities(2, 0x01);
            checkBoxAbJunFlag10.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_JunctionAbilities(2, 0x02);
            checkBoxAbJunFlag11.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_JunctionAbilities(2, 0x04);
            checkBoxAbJunFlag12.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_JunctionAbilities(2, 0x08);
            checkBoxAbJunFlag13.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_JunctionAbilities(2, 0x10);
            checkBoxAbJunFlag14.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_JunctionAbilities(2, 0x20);
            checkBoxAbJunFlag15.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_JunctionAbilities(2, 0x40);
            checkBoxAbJunFlag16.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_JunctionAbilities(2, 0x80);
            checkBoxAbJunFlag17.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_JunctionAbilities(3, 0x01);
            checkBoxAbJunFlag18.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_JunctionAbilities(3, 0x02);
            checkBoxAbJunFlag19.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_JunctionAbilities(3, 0x04);

            #endregion

            #region Party abilities

            numericUpDownAbPartyAP.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_PartyAbilities(0, numericUpDownAbPartyAP.Value);
            checkBoxAbPartyFlag1.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_PartyAbilities(1, 0x01);
            checkBoxAbPartyFlag2.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_PartyAbilities(1, 0x02);
            checkBoxAbPartyFlag3.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_PartyAbilities(1, 0x04);
            checkBoxAbPartyFlag4.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_PartyAbilities(1, 0x08);
            checkBoxAbPartyFlag5.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_PartyAbilities(1, 0x10);
            checkBoxAbPartyFlag6.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_PartyAbilities(1, 0x20);
            checkBoxAbPartyFlag7.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_PartyAbilities(1, 0x40);
            checkBoxAbPartyFlag8.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_PartyAbilities(1, 0x80);

            #endregion

            #region GFs abilities

            numericUpDownAbGFAP.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_GFAbilities(0, numericUpDownAbGFAP.Value);
            checkBoxAbGFBoost.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_GFAbilities(1, 0x01);
            comboBoxAbGFStatToIncrease.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_GFAbilities(2, GFAbilities_GetStat(comboBoxAbGFStatToIncrease.SelectedIndex));
            trackBarAbGFIncrementValue.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_GFAbilities(3, trackBarAbGFIncrementValue.Value);

            #endregion

            #region Characters abilities

            numericUpDownAbCharAP.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_CharacterAbilities(0, numericUpDownAbCharAP.Value);
            checkBoxAbCharFlag1.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CharacterAbilities(1, 0x01);
            checkBoxAbCharFlag2.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CharacterAbilities(1, 0x02);
            checkBoxAbCharFlag3.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CharacterAbilities(1, 0x04);
            checkBoxAbCharFlag4.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CharacterAbilities(1, 0x08);
            checkBoxAbCharFlag5.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CharacterAbilities(1, 0x10);
            checkBoxAbCharFlag6.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CharacterAbilities(1, 0x20);
            checkBoxAbCharFlag7.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CharacterAbilities(1, 0x40);
            checkBoxAbCharFlag8.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CharacterAbilities(1, 0x80);
            checkBoxAbCharFlag9.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CharacterAbilities(2, 0x01);
            checkBoxAbCharFlag10.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CharacterAbilities(2, 0x02);
            checkBoxAbCharFlag11.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CharacterAbilities(2, 0x04);
            checkBoxAbCharFlag12.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CharacterAbilities(2, 0x08);
            checkBoxAbCharFlag13.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CharacterAbilities(2, 0x10);
            checkBoxAbCharFlag14.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CharacterAbilities(2, 0x20);
            checkBoxAbCharFlag15.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CharacterAbilities(2, 0x40);
            checkBoxAbCharFlag16.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CharacterAbilities(2, 0x80);
            checkBoxAbCharFlag17.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CharacterAbilities(3, 0x01);
            checkBoxAbCharFlag18.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CharacterAbilities(3, 0x02);
            checkBoxAbCharFlag19.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CharacterAbilities(3, 0x04);
            checkBoxAbCharFlag20.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CharacterAbilities(3, 0x08);
            checkBoxAbCharFlag21.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CharacterAbilities(3, 0x10);
            checkBoxAbCharFlag22.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CharacterAbilities(3, 0x20);
            checkBoxAbCharFlag23.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CharacterAbilities(3, 0x40);
            checkBoxAbCharFlag24.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_CharacterAbilities(3, 0x80);

            #endregion

            #region Menu abilities

            numericUpDownAbMenuAP.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_MenuAbilities(0, numericUpDownAbMenuAP.Value);
            numericUpDownAbMenuIndex.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_MenuAbilities(1, numericUpDownAbMenuIndex.Value);
            numericUpDownAbMenuStartEntry.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_MenuAbilities(2, numericUpDownAbMenuStartEntry.Value);
            numericUpDownAbMenuEndEntry.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_MenuAbilities(3, numericUpDownAbMenuEndEntry.Value);

            #endregion

            #region Battle commmands

            comboBoxBatComAbilityID.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_BattleCommands(0, comboBoxBatComAbilityID.SelectedIndex);
            checkBoxBatComFlag1.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleCommands(1, 0x01);
            checkBoxBatComFlag2.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleCommands(1, 0x02);
            checkBoxBatComFlag3.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleCommands(1, 0x04);
            checkBoxBatComFlag4.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleCommands(1, 0x08);
            checkBoxBatComFlag5.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleCommands(1, 0x10);
            checkBoxBatComFlag6.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleCommands(1, 0x20);
            checkBoxBatComFlag7.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleCommands(1, 0x40);
            checkBoxBatComFlag8.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_BattleCommands(1, 0x80);
            numericUpDownBatComTarget.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_BattleCommands(2, numericUpDownBatComTarget.Value);

            #endregion

            #region Rinoa commands
            
            checkBoxBatComRinoaFlag1.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RinoaCommands(0, 0x01);
            checkBoxBatComRinoaFlag2.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RinoaCommands(0, 0x02);
            checkBoxBatComRinoaFlag3.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RinoaCommands(0, 0x04);
            checkBoxBatComRinoaFlag4.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RinoaCommands(0, 0x08);
            checkBoxBatComRinoaFlag5.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RinoaCommands(0, 0x10);
            checkBoxBatComRinoaFlag6.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RinoaCommands(0, 0x20);
            checkBoxBatComRinoaFlag7.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RinoaCommands(0, 0x40);
            checkBoxBatComRinoaFlag8.CheckedChanged += (sender, args) => KernelWorker.UpdateVariable_RinoaCommands(0, 0x80);
            numericUpDownBatComRinoaTarget.ValueChanged += (sender, args) => KernelWorker.UpdateVariable_RinoaCommands(1, numericUpDownBatComRinoaTarget.Value);
            comboBoxBatComRinoaID.SelectedIndexChanged += (sender, args) => KernelWorker.UpdateVariable_RinoaCommands(2, comboBoxBatComRinoaID.SelectedIndex);

            #endregion

            #endregion
        }

        private void DisableTabStop(Control ctrl)
        {
            ctrl.TabStop = false;
            foreach (Control item in ctrl.Controls)
            {
                DisableTabStop(item);
            }
        } //To disable tab stop, temporary code until i order them

        #region OPEN, SAVE, TOOLBAR, TOOLTIPS FILE, ABOUT, CLOSE

        #region Open

        private async void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Open FF8 kernel.bin";
            openFileDialog.Filter = "FF8 Kernel File|*.bin";
            openFileDialog.FileName = "kernel.bin";

            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            {
                try
                {
                    using (var fileStream = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                    {
                        using (var BR = new BinaryReader(fileStream))
                        {
                            KernelWorker.ReadKernel(BR.ReadBytes((int)fileStream.Length));
                        }
                        CreateTooltipsFile();

                        SlotArray();
                        DuelParams();
                        Misc();
                    }

                    _existingFilename = openFileDialog.FileName;

                    saveToolStripMenuItem.Enabled = true;
                    saveAsToolStripMenuItem.Enabled = true;
                    saveToolStripButton.Enabled = true;
                    saveAsToolStripButton.Enabled = true;
                    buttonCharEXPChart.Enabled = true;
                    buttonCharHPChart.Enabled = true;
                    buttonCharSTRChart.Enabled = true;
                    buttonCharVITChart.Enabled = true;
                    buttonCharMAGChart.Enabled = true;
                    buttonCharSPRChart.Enabled = true;
                    buttonCharSPDChart.Enabled = true;
                    buttonCharLUCKChart.Enabled = true;
                    buttonGFDamageChart.Enabled = true;
                    buttonMagicDamageChart.Enabled = true;
                    buttonEnemyAttacksDamageChart.Enabled = true;
                    buttonCharEXPFormula.Enabled = true;
                    buttonCharHPFormula.Enabled = true;
                    buttonCharSTRFormula.Enabled = true;
                    buttonCharVITFormula.Enabled = true;
                    buttonCharMAGFormula.Enabled = true;
                    buttonCharSPRFormula.Enabled = true;
                    buttonCharSPDFormula.Enabled = true;
                    buttonCharLUCKFormula.Enabled = true;
                    buttonGFDamageFormula.Enabled = true;
                    buttonMagicDamageFormula.Enabled = true;
                    buttonEnemyAttacksDamageFormula.Enabled = true;

                    listBoxCharacters.SelectedIndex = 0;
                    listBoxRenzoFin.SelectedIndex = 0;
                    listBoxBlueMagic.SelectedIndex = 0;
                    listBoxDuel.SelectedIndex = 0;
                    listBoxSlotsSets.SelectedIndex = 0;
                    listBoxCombine.SelectedIndex = 0;
                    listBoxBatComRinoa.SelectedIndex = 0;
                    listBoxShot.SelectedIndex = 0;
                    listBoxTempCharLB.SelectedIndex = 0;
                    listBoxBattleItems.SelectedIndex = 0;
                    listBoxWeapons.SelectedIndex = 0;
                    listBoxAbChar.SelectedIndex = 0;
                    listBoxAbStats.SelectedIndex = 0;
                    listBoxAbJun.SelectedIndex = 0;
                    listBoxAbCom.SelectedIndex = 0;
                    listBoxAbComData.SelectedIndex = 0;
                    listBoxAbGF.SelectedIndex = 0;
                    listBoxAbParty.SelectedIndex = 0;
                    listBoxAbMenu.SelectedIndex = 0;
                    listBoxMagic.SelectedIndex = 0;
                    listBoxGF.SelectedIndex = 0;
                    listBoxGFAttacks.SelectedIndex = 0;
                    listBoxDevour.SelectedIndex = 0;
                    listBoxBatCom.SelectedIndex = 0;
                    listBoxEnemyAttacks.SelectedIndex = 0;

                    toolStripStatusLabelStatus.Text = Path.GetFileName(_existingFilename) + " loaded successfully";
                    toolStripStatusLabelKernel.Text = Path.GetFileName(_existingFilename) + " loaded";
                    statusStrip1.BackColor = Color.FromArgb(255, 237, 110, 0);
                    toolStripStatusLabelStatus.BackColor = Color.FromArgb(255, 237, 110, 0);
                    await Task.Delay(3000);
                    statusStrip1.BackColor = Color.Gray;
                    toolStripStatusLabelStatus.BackColor = Color.Gray;
                    toolStripStatusLabelStatus.Text = "Ready";
                }
                catch (Exception)
                {
                    MessageBox.Show
                        (String.Format("I cannot open the file {0}, maybe it's locked by another software?", Path.GetFileName(openFileDialog.FileName)), "Error Opening File",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
        }


        #endregion

        #region Save

        private async void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(string.IsNullOrEmpty(_existingFilename)) && KernelWorker.Kernel != null)
            {
                try
                {
                    File.WriteAllBytes(_existingFilename, KernelWorker.Kernel);

                    statusStrip1.BackColor = Color.FromArgb(255, 237, 110, 0);
                    toolStripStatusLabelStatus.BackColor = Color.FromArgb(255, 237, 110, 0);
                    toolStripStatusLabelStatus.Text = Path.GetFileName(_existingFilename) + " saved successfully";
                    await Task.Delay(3000);
                    statusStrip1.BackColor = Color.Gray;
                    toolStripStatusLabelStatus.BackColor = Color.Gray;
                    toolStripStatusLabelStatus.Text = "Ready";
                }
                catch (Exception)
                {
                    MessageBox.Show
                        (String.Format("I cannot save the file {0}, maybe it's locked by another software?", Path.GetFileName(_existingFilename)), "Error Saving File",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
        }

        private async void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveAsDialog = new SaveFileDialog();
            saveAsDialog.Title = "Save FF8 kernel.bin";
            saveAsDialog.Filter = "FF8 Kernel File|*.bin";
            saveAsDialog.FileName = Path.GetFileName(_existingFilename);

            if (!(string.IsNullOrEmpty(_existingFilename)) && KernelWorker.Kernel != null)
            {
                try
                {
                    if (saveAsDialog.ShowDialog() != DialogResult.OK) return;
                    {
                        File.WriteAllBytes(saveAsDialog.FileName, KernelWorker.Kernel);

                        toolStripStatusLabelStatus.Text = Path.GetFileName(saveAsDialog.FileName) + " saved successfully";
                        statusStrip1.BackColor = Color.FromArgb(255, 237, 110, 0);
                        toolStripStatusLabelStatus.BackColor = Color.FromArgb(255, 237, 110, 0);
                        await Task.Delay(3000);
                        statusStrip1.BackColor = Color.Gray;
                        toolStripStatusLabelStatus.BackColor = Color.Gray;
                        toolStripStatusLabelStatus.Text = "Ready";
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show
                        (String.Format("I cannot save the file {0}, maybe it's locked by another software?", Path.GetFileName(saveAsDialog.FileName)), "Error Saving File",
                        MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
        }

        #endregion

        #region Toolbar

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            openToolStripMenuItem_Click(sender, e);
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            saveToolStripMenuItem_Click(sender, e);
        }

        private void saveAsToolStripButton_Click(object sender, EventArgs e)
        {
            saveAsToolStripMenuItem_Click(sender, e);
        }

        private void deleteTooltipsToolStripButton_Click(object sender, EventArgs e)
        {
            deleteTooltipsToolStripMenuItem_Click(sender, e);
        }

        #endregion

        #region Tooltips file

        private void CreateTooltipsFile()
        {
            if (!File.Exists(_backup))
            {
                DialogResult dialogResult = MessageBox.Show("Do you want to create a copy of this kernel.bin file " +
                    "to show default values tooltips?", "Create tooltips file", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                if (dialogResult == DialogResult.Yes)
                {
                    File.WriteAllBytes(_backup, KernelWorker.Kernel);
                    KernelWorker.BackupKernel = File.ReadAllBytes(_backup);
                    File.SetAttributes(_backup, FileAttributes.ReadOnly);
                    MessageBox.Show("The file has been created successfully in the same folder of Doomtrain.exe.",
                        "Tooltips file created", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    toolStripStatusLabelTooltips.Text = Path.GetFileName(_backup) + " loaded";
                    deleteTooltipsToolStripMenuItem.Enabled = true;
                    deleteTooltipsToolStripButton.Enabled = true;
                }

                else if (dialogResult == DialogResult.No)
                {
                    DialogResult dialogResult2 = MessageBox.Show("Do you want to point me to another kernel.bin file?" + 
                        "\nIf you answer no the file will be created from the kernel.bin you opened previously.", "Create tooltips file", 
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (dialogResult2 == DialogResult.Yes)
                    {
                        OpenFileDialog openFileDialog = new OpenFileDialog();
                        openFileDialog.Title = "Open FF8 kernel.bin";
                        openFileDialog.Filter = "FF8 Kernel File|*.bin";
                        openFileDialog.FileName = "kernel.bin";

                        if (openFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            using (var fileStream = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                            {
                                using (var BR = new BinaryReader(fileStream))
                                {
                                    KernelWorker.ReadKernel(BR.ReadBytes((int)fileStream.Length));
                                    File.WriteAllBytes(_backup, KernelWorker.Kernel);
                                    KernelWorker.BackupKernel = File.ReadAllBytes(_backup);
                                    File.SetAttributes(_backup, FileAttributes.ReadOnly & FileAttributes.Hidden);
                                }
                            }
                            MessageBox.Show("The file has been created successfully in the same folder of Doomtrain.exe.\nDoomtrain will now restart.",
                                "Tooltips file created", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                            Process.Start(Application.ExecutablePath);
                            Environment.Exit(0);
                        }
                        else
                        {
                            File.WriteAllBytes(_backup, KernelWorker.Kernel);
                            KernelWorker.BackupKernel = File.ReadAllBytes(_backup);
                            MessageBox.Show("The file has created in the same folder of Doomtrain.exe from the kernel.bin you opened previously.",
                                "Tooltips file created", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                            toolStripStatusLabelTooltips.Text = Path.GetFileName(_backup) + " loaded";
                            deleteTooltipsToolStripMenuItem.Enabled = true;
                            deleteTooltipsToolStripButton.Enabled = true;
                        }
                    }

                    else if (dialogResult2 == DialogResult.No)
                    {
                        File.WriteAllBytes(_backup, KernelWorker.Kernel);
                        KernelWorker.BackupKernel = File.ReadAllBytes(_backup);
                        File.SetAttributes(_backup, FileAttributes.ReadOnly & FileAttributes.Hidden);
                        MessageBox.Show("The file has been created successfully in the same folder of Doomtrain.exe.",
                            "Tooltips file created", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                        toolStripStatusLabelTooltips.Text = Path.GetFileName(_backup) + " loaded";
                        deleteTooltipsToolStripMenuItem.Enabled = true;
                        deleteTooltipsToolStripButton.Enabled = true;
                    }
                }
            }


            else
            {
                KernelWorker.BackupKernel = File.ReadAllBytes(_backup);
                toolStripStatusLabelTooltips.Text = Path.GetFileName(_backup) + " loaded";
            }



        }

        private void deleteTooltipsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("This will delete the tooltips.bin file. " + 
                "Doomtrain will restart and unsaved changes will be lost, do you want to continue?", 
                "Delete tooltips file", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    File.SetAttributes(_backup, FileAttributes.Normal);
                    File.Delete(_backup);

                    DialogResult dialogResult2 = MessageBox.Show("The tooltips.bin file was deleted.\nDoomtrain will now restart.",
                        "Toltips file deleted", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    Process.Start(Application.ExecutablePath);
                    Environment.Exit(0);
                }

                catch
                {
                    DialogResult dialogResult2 = MessageBox.Show("Sorry, i wasn't able to delete the tooltips.bin file.\nTry to delete it manually.",
                        "Failed to delete", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                    if (!File.Exists(_backup))
                    {
                        deleteTooltipsToolStripMenuItem.Enabled = false;
                        deleteTooltipsToolStripButton.Enabled = false;
                    }
                }
            }
        }

        #endregion

        #region About

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog();
        }

        #endregion

        #region Close

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!(string.IsNullOrEmpty(_existingFilename)) && KernelWorker.Kernel != null)
            {
                DialogResult dialogResult = MessageBox.Show("Save changes to " + Path.GetFileName(_existingFilename) + " before closing?", "Close",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

                if (dialogResult == DialogResult.Yes)
                {
                    SaveFileDialog saveAsDialog = new SaveFileDialog();
                    saveAsDialog.Title = "Save FF8 kernel.bin";
                    saveAsDialog.Filter = "FF8 Kernel File|*.bin";
                    saveAsDialog.FileName = Path.GetFileName(_existingFilename);

                    if (saveAsDialog.ShowDialog() != DialogResult.OK) return;
                    File.WriteAllBytes(saveAsDialog.FileName, KernelWorker.Kernel);
                }

                else if (dialogResult == DialogResult.Cancel)
                    e.Cancel = true;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #endregion

        #region FORMULAS, CHARTS, LABELS, LISTBOXES SWITCH, FONTS

        #region Formulas

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

        private void buttonGFDamageFormula_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Regular GF Attack =\n" + 
                "a = AttackerMag + Power\n" +
                "b = a * (265 - TargetSpr) / 4\n" +
                "c = b * Power / 256\n" + 
                "d = c * Boost / 100\n" + 
                "e = d * (100 + SumMagBonus) / 100\n" +
                "Damage = e * (900 - ElemDef) / 100\n\n" +
                "% GF Damage = TargetMaxHP * Level / 100\n\n" +
                "Fixed GF Damage = (((GF level * Power) / 1,000) + 1) * 1,000", "GF Damage Formula");
        }

        private void buttonMagicDamageFormula_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Magic Attack =\n" + 
                "a = AttackerMag + Power\n" +
                "b = a * (265 - TargetSpr) / 4\n" +
                "c = b * Power / 256\n" +
                "Damage = c * (900 - ElemDef) / 100\n\n" +
                "% Magic Damage = TargetCurrentHP * Power / 16\n\n" +
                "Curative Magic = (Power + HealerMag) * Power / 2", "Magic Damage Formula");
        }
        private void buttonEnemyAttacksDamageFormula_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Physical Attack =\n" +
                "a = AttackerStr^2 / 16 + AttackerStr\n" +
                "b = a * (265 - TargetVit) / 256\n" +
                "c = b * Power / 16\n" +
                "Damage = c * ElemAtt * (900 - ElemDef) / 10000\n\n" + 
                "Magic Attack =\n" +
                "a = AttackerMag + Power\n" +
                "b = a * (265 - TargetSpr) / 4\n" +
                "c = b * Power / 256\n" +
                "Damage = c * (900 - ElemDef) / 100\n\n" +
                "% Magic Damage = TargetCurrentHP * Power / 16\n\n" +
                "Target Current HP -1 = TargetCurrentHP - 1\n\n" +
                "% Damage = TargetCurrentHP * Power / 16\n\n" +
                "Fixed Damage = Power * 100 - AttackParam\n\n", "Enemy Attacks Damage Formulas");
        }

        #endregion

        #region Charts

        private CharEXP _CharEXP;
        private void buttonCharEXPChart_Click(object sender, EventArgs e)
        {
            if ((_CharEXP == null) || (_CharEXP.IsDisposed))
            {
                _CharEXP = new CharEXP(this);
            }
            _CharEXP.Show();
            _CharEXP.Focus();
        }

        private CharHP _CharHP;
        private void buttonCharHPChart_Click(object sender, EventArgs e)
        {
            if ((_CharHP == null) || (_CharHP.IsDisposed))
            {
                _CharHP = new CharHP(this);
            }
            _CharHP.Show();
            _CharHP.Focus();


        }
        private charSTR _CharSTR;
        private void buttonCharSTRChart_Click(object sender, EventArgs e)
        {
            if ((_CharSTR == null) || (_CharSTR.IsDisposed))
            {
                _CharSTR = new charSTR(this);
            }
            _CharSTR.Show();
            _CharSTR.Focus();
        }

        private CharVIT _CharVIT;
        private void buttonCharVITChart_Click(object sender, EventArgs e)
        {
            if ((_CharVIT == null) || (_CharVIT.IsDisposed))
            {
                _CharVIT = new CharVIT(this);
            }
            _CharVIT.Show();
            _CharVIT.Focus();
        }

        private CharMAG _CharMAG;
        private void buttonCharMAGChart_Click(object sender, EventArgs e)
        {
            if ((_CharMAG == null) || (_CharMAG.IsDisposed))
            {
                _CharMAG = new CharMAG(this);
            }
            _CharMAG.Show();
            _CharMAG.Focus();
        }

        private CharSPR _CharSPR;
        private void buttonCharSPRChart_Click(object sender, EventArgs e)
        {
            if ((_CharSPR == null) || (_CharSPR.IsDisposed))
            {
                _CharSPR = new CharSPR(this);
            }
            _CharSPR.Show();
            _CharSPR.Focus();
        }

        private CharSPD _CharSPD;
        private void buttonCharSPDChart_Click(object sender, EventArgs e)
        {
            if ((_CharSPD == null) || (_CharSPD.IsDisposed))
            {
                _CharSPD = new CharSPD(this);
            }
            _CharSPD.Show();
            _CharSPD.Focus();
        }

        private CharLUCK _CharLUCK;
        private void buttonCharLUCKChart_Click(object sender, EventArgs e)
        {
            if ((_CharLUCK == null) || (_CharLUCK.IsDisposed))
            {
                _CharLUCK = new CharLUCK(this);
            }
            _CharLUCK.Show();
            _CharLUCK.Focus();
        }

        private GfDamage _GfDamage;
        private void buttonGfDamageChart_Click(object sender, EventArgs e)
        {
            if ((_GfDamage == null) || (_GfDamage.IsDisposed))
            {
                _GfDamage = new GfDamage(this);
            }
            _GfDamage.Show();
            _GfDamage.Focus();
        }

        private MagicDamage _MagicDamage;
        private void buttonMagicDamageChart_Click(object sender, EventArgs e)
        {
            if ((_MagicDamage == null) || (_MagicDamage.IsDisposed))
            {
                _MagicDamage = new MagicDamage(this);
            }
            _MagicDamage.Show();
            _MagicDamage.Focus();
        }

        private EnemyAttacksDamage _EnemyAttacksDamage;
        private void buttonEnemyAttacksDamageChart_Click(object sender, EventArgs e)
        {
            if ((_EnemyAttacksDamage == null) || (_EnemyAttacksDamage.IsDisposed))
            {
                _EnemyAttacksDamage = new EnemyAttacksDamage(this);
            }
            _EnemyAttacksDamage.Show();
            _EnemyAttacksDamage.Focus();
        }

        #endregion

        #region Value labels

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

        private void numericUpDownShotTimer1_ValueChanged(object sender, EventArgs e)
        {
            labelShotTimer1Value.Text = String.Format("{0:0.00}s", Math.Truncate((numericUpDownShotTimer1.Value * 2 / 15) * 100) / 100);
            //labelShotTimer1Value.Text = String.Format("{0:0.00}s", (numericUpDownShotTimer1.Value * 2) / 15);                                this is with rounding
        }

        private void numericUpDownShotTimer2_ValueChanged(object sender, EventArgs e)
        {
            labelShotTimer2Value.Text = String.Format("{0:0.00}s", Math.Truncate((numericUpDownShotTimer2.Value * 2 / 15) * 100) / 100);
        }

        private void numericUpDownShotTimer3_ValueChanged(object sender, EventArgs e)
        {
            labelShotTimer3Value.Text = String.Format("{0:0.00}s", Math.Truncate((numericUpDownShotTimer3.Value * 2 / 15) * 100) / 100);
        }

        private void numericUpDownShotTimer4_ValueChanged(object sender, EventArgs e)
        {
            labelShotTimer4Value.Text = String.Format("{0:0.00}s", Math.Truncate((numericUpDownShotTimer4.Value * 2 / 15) * 100) / 100);
        }

        private void numericUpDownDuelTimer1_ValueChanged(object sender, EventArgs e)
        {
            labelDuelTimer1Value.Text = String.Format("{0:0.00}s", Math.Truncate((numericUpDownDuelTimer1.Value / 15) * 100) / 100);
        }

        private void numericUpDownDuelTimer2_ValueChanged(object sender, EventArgs e)
        {
            labelDuelTimer2Value.Text = String.Format("{0:0.00}s", Math.Truncate((numericUpDownDuelTimer2.Value / 15) * 100) / 100);
        }

        private void numericUpDownDuelTimer3_ValueChanged(object sender, EventArgs e)
        {
            labelDuelTimer3Value.Text = String.Format("{0:0.00}s", Math.Truncate((numericUpDownDuelTimer3.Value / 15) * 100) / 100);
        }

        private void numericUpDownDuelTimer4_ValueChanged(object sender, EventArgs e)
        {
            labelDuelTimer4Value.Text = String.Format("{0:0.00}s", Math.Truncate((numericUpDownDuelTimer4.Value / 15) * 100) / 100);
        }

        private void numericUpDownStatusTimer1_ValueChanged(object sender, EventArgs e)
        {
            labelStatusTimer1Value.Text = String.Format("{0:0.00}s", Math.Truncate((numericUpDownStatusTimer1.Value * 4 / 15) * 100) / 100);
        }

        private void numericUpDownStatusTimer2_ValueChanged(object sender, EventArgs e)
        {
            labelStatusTimer2Value.Text = String.Format("{0:0.00}s", Math.Truncate((numericUpDownStatusTimer2.Value * 4 / 15) * 100) / 100);
        }

        private void numericUpDownStatusTimer3_ValueChanged(object sender, EventArgs e)
        {
            labelStatusTimer3Value.Text = String.Format("{0:0.00}s", Math.Truncate((numericUpDownStatusTimer3.Value * 4 / 15) * 100) / 100);
        }

        private void numericUpDownStatusTimer4_ValueChanged(object sender, EventArgs e)
        {
            labelStatusTimer4Value.Text = String.Format("{0:0.00}s", Math.Truncate((numericUpDownStatusTimer4.Value * 4 / 15) * 100) / 100);
        }

        private void numericUpDownStatusTimer5_ValueChanged(object sender, EventArgs e)
        {
            labelStatusTimer5Value.Text = String.Format("{0:0.00}s", Math.Truncate((numericUpDownStatusTimer5.Value * 4 / 15) * 100) / 100);
        }

        private void numericUpDownStatusTimer6_ValueChanged(object sender, EventArgs e)
        {
            labelStatusTimer6Value.Text = String.Format("{0:0.00}s", Math.Truncate((numericUpDownStatusTimer6.Value * 4 / 15) * 100) / 100);
        }

        private void numericUpDownStatusTimer7_ValueChanged(object sender, EventArgs e)
        {
            labelStatusTimer7Value.Text = String.Format("{0:0.00}s", Math.Truncate((numericUpDownStatusTimer7.Value * 4 / 15) * 100) / 100);
        }

        private void numericUpDownStatusTimer8_ValueChanged(object sender, EventArgs e)
        {
            labelStatusTimer8Value.Text = String.Format("{0:0.00}s", Math.Truncate((numericUpDownStatusTimer8.Value * 4 / 15) * 100) / 100);
        }

        private void numericUpDownStatusTimer9_ValueChanged(object sender, EventArgs e)
        {
            labelStatusTimer9Value.Text = String.Format("{0:0.00}s", Math.Truncate((numericUpDownStatusTimer9.Value * 4 / 15) * 100) / 100);
        }

        private void numericUpDownStatusTimer10_ValueChanged(object sender, EventArgs e)
        {
            labelStatusTimer10Value.Text = String.Format("{0:0.00}s", Math.Truncate((numericUpDownStatusTimer10.Value * 4 / 15) * 100) / 100);
        }

        private void numericUpDownStatusTimer11_ValueChanged(object sender, EventArgs e)
        {
            labelStatusTimer11Value.Text = String.Format("{0:0.00}s", Math.Truncate((numericUpDownStatusTimer11.Value * 4 / 15) * 100) / 100);
        }

        private void numericUpDownStatusTimer12_ValueChanged(object sender, EventArgs e)
        {
            labelStatusTimer12Value.Text = String.Format("{0:0.00}s", Math.Truncate((numericUpDownStatusTimer12.Value * 4 / 15) * 100) / 100);
        }

        private void numericUpDownStatusTimer13_ValueChanged(object sender, EventArgs e)
        {
            labelStatusTimer13Value.Text = String.Format("{0:0.00}s", Math.Truncate((numericUpDownStatusTimer13.Value * 4 / 15) * 100) / 100);
        }

        private void numericUpDownStatusTimer14_ValueChanged(object sender, EventArgs e)
        {
            labelStatusTimer14Value.Text = String.Format("{0:0.00}s", Math.Truncate((numericUpDownStatusTimer14.Value * 4 / 15) * 100) / 100);
        }

        private void trackBarStatsIncrementValue_Scroll(object sender, EventArgs e)
        {
            labelAbStatsValueTrackBar.Text = trackBarAbStatsIncrementValue.Value + "%".ToString();
        }

        private void trackBarAbGFIncrementValue_Scroll(object sender, EventArgs e)
        {
            labelAbGFValueTrackBar.Text = trackBarAbGFIncrementValue.Value + "%".ToString();
        }

        #endregion

        #region Abilities listbox switches

        private void tabControlAbilities_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlAbilities.SelectedIndex == 0)
            {
                listBoxAbChar.Visible = true;
                listBoxAbStats.Visible = false;
                listBoxAbJun.Visible = false;
                listBoxAbCom.Visible = false;
                listBoxAbComData.Visible = false;
                listBoxAbGF.Visible = false;
                listBoxAbParty.Visible = false;
                listBoxAbMenu.Visible = false;
            }

            if (tabControlAbilities.SelectedIndex == 1)
            {
                listBoxAbChar.Visible = false;
                listBoxAbStats.Visible = true;
                listBoxAbJun.Visible = false;
                listBoxAbCom.Visible = false;
                listBoxAbComData.Visible = false;
                listBoxAbGF.Visible = false;
                listBoxAbParty.Visible = false;
                listBoxAbMenu.Visible = false;
            }

            if (tabControlAbilities.SelectedIndex == 2)
            {
                listBoxAbChar.Visible = false;
                listBoxAbStats.Visible = false;
                listBoxAbJun.Visible = true;
                listBoxAbCom.Visible = false;
                listBoxAbComData.Visible = false;
                listBoxAbGF.Visible = false;
                listBoxAbParty.Visible = false;
                listBoxAbMenu.Visible = false;
            }

            if (tabControlAbilities.SelectedIndex == 3)
            {
                listBoxAbChar.Visible = false;
                listBoxAbStats.Visible = false;
                listBoxAbJun.Visible = false;                
                listBoxAbCom.Visible = true;
                listBoxAbComData.Visible = false;
                listBoxAbGF.Visible = false;
                listBoxAbParty.Visible = false;
                listBoxAbMenu.Visible = false;
            }

            if (tabControlAbilities.SelectedIndex == 4)
            {
                listBoxAbChar.Visible = false;
                listBoxAbStats.Visible = false;
                listBoxAbJun.Visible = false;
                listBoxAbCom.Visible = false;
                listBoxAbComData.Visible = true;
                listBoxAbGF.Visible = false;
                listBoxAbParty.Visible = false;
                listBoxAbMenu.Visible = false;
            }

            if (tabControlAbilities.SelectedIndex == 5)
            {
                listBoxAbChar.Visible = false;
                listBoxAbStats.Visible = false;
                listBoxAbJun.Visible = false;
                listBoxAbCom.Visible = false;
                listBoxAbComData.Visible = false;
                listBoxAbGF.Visible = true;
                listBoxAbParty.Visible = false;
                listBoxAbMenu.Visible = false;
            }

            if (tabControlAbilities.SelectedIndex == 6)
            {
                listBoxAbChar.Visible = false;
                listBoxAbStats.Visible = false;
                listBoxAbJun.Visible = false;
                listBoxAbCom.Visible = false;
                listBoxAbComData.Visible = false;
                listBoxAbGF.Visible = false;
                listBoxAbParty.Visible = true;
                listBoxAbMenu.Visible = false;
            }

            if (tabControlAbilities.SelectedIndex == 7)
            {
                listBoxAbChar.Visible = false;
                listBoxAbStats.Visible = false;
                listBoxAbJun.Visible = false;
                listBoxAbCom.Visible = false;
                listBoxAbComData.Visible = false;
                listBoxAbGF.Visible = false;
                listBoxAbParty.Visible = false;
                listBoxAbMenu.Visible = true;
            }
        }

        #endregion

        #region Fonts

        private void LoadPrivateFontCollection()
        {
            // Create the byte array and get its length
            byte[] fontArray = Properties.Resources.mainTabFont;
            int dataLength = Properties.Resources.mainTabFont.Length;

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

        private void LoadTabControlMainFont(Font font)
        {
            FontStyle fontStyle = FontStyle.Regular;
            tabControlMain.Font = new Font(ff, 13, fontStyle);
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            LoadPrivateFontCollection();
            LoadTabControlMainFont(font);
        }

        #endregion

        #endregion


        #region BACKUP ALGORITHM

        private void ToolTip(Control control, byte mode, object value)
        {
            switch(mode)
            {
                case _bp_numerical:
                    {
                        toolTip1.SetToolTip(control, $"Default: {Convert.ToInt32(value)}");
                        break;
                    }
                case _bp_checked:
                    {
                        string check = Convert.ToBoolean(value) ? "Checked" : "Unchecked";
                        toolTip1.SetToolTip(control, $"Default: {check}");
                        break;
                    }
                case _bp_string:
                    {
                        toolTip1.SetToolTip(control, $"Default: {Convert.ToString(value)}");
                        break;
                    }
                case 3:
                    {
                        toolTip1.SetToolTip(control, $"Default: {Convert.ToDouble(value)}");
                        break;
                    }
                default:
                    goto case _bp_numerical;
            }
        }

        #endregion


        #region WRITE TO GUI

        #region Magic

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

        private void MagicStatusWorker(bool bBackup)
        {
            if (!bBackup)
            {
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
                return;
            }
            ToolTip(checkBoxMagicSleep,1, (KernelWorker.GetSelectedMagicData.StatusMagic1 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxMagicHaste, 1, (KernelWorker.GetSelectedMagicData.StatusMagic1 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxMagicSlow, 1, (KernelWorker.GetSelectedMagicData.StatusMagic1 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxMagicStop, 1, (KernelWorker.GetSelectedMagicData.StatusMagic1 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxMagicRegen, 1, (KernelWorker.GetSelectedMagicData.StatusMagic1 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxMagicProtect, 1, (KernelWorker.GetSelectedMagicData.StatusMagic1 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxMagicShell, 1, (KernelWorker.GetSelectedMagicData.StatusMagic1 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxMagicReflect, 1, (KernelWorker.GetSelectedMagicData.StatusMagic1 & 0x80) >= 1 ? true : false);

            ToolTip(checkBoxMagicAura, 1, (KernelWorker.GetSelectedMagicData.StatusMagic2 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxMagicCurse, 1, (KernelWorker.GetSelectedMagicData.StatusMagic2 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxMagicDoom, 1, (KernelWorker.GetSelectedMagicData.StatusMagic2 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxMagicInvincible, 1, (KernelWorker.GetSelectedMagicData.StatusMagic2 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxMagicPetrifying, 1, (KernelWorker.GetSelectedMagicData.StatusMagic2 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxMagicFloat, 1, (KernelWorker.GetSelectedMagicData.StatusMagic2 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxMagicConfusion, 1, (KernelWorker.GetSelectedMagicData.StatusMagic2 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxMagicDrain, 1, (KernelWorker.GetSelectedMagicData.StatusMagic2 & 0x80) >= 1 ? true : false);

            ToolTip(checkBoxMagicEject, 1, (KernelWorker.GetSelectedMagicData.StatusMagic3 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxMagicDouble, 1, (KernelWorker.GetSelectedMagicData.StatusMagic3 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxMagicTriple, 1, (KernelWorker.GetSelectedMagicData.StatusMagic3 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxMagicDefend, 1, (KernelWorker.GetSelectedMagicData.StatusMagic3 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxMagicUnk1, 1, (KernelWorker.GetSelectedMagicData.StatusMagic3 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxMagicUnk2, 1, (KernelWorker.GetSelectedMagicData.StatusMagic3 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxMagicCharged, 1, (KernelWorker.GetSelectedMagicData.StatusMagic3 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxMagicBackAttack, 1, (KernelWorker.GetSelectedMagicData.StatusMagic3 & 0x80) >= 1 ? true : false);

            ToolTip(checkBoxMagicVit0, 1, (KernelWorker.GetSelectedMagicData.StatusMagic4 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxMagicAngelWing, 1, (KernelWorker.GetSelectedMagicData.StatusMagic4 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxMagicUnk3, 1, (KernelWorker.GetSelectedMagicData.StatusMagic4 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxMagicUnk4, 1, (KernelWorker.GetSelectedMagicData.StatusMagic4 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxMagicUnk5, 1, (KernelWorker.GetSelectedMagicData.StatusMagic4 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxMagicUnk6, 1, (KernelWorker.GetSelectedMagicData.StatusMagic4 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxMagicHasMagic, 1, (KernelWorker.GetSelectedMagicData.StatusMagic4 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxMagicSummonGF, 1, (KernelWorker.GetSelectedMagicData.StatusMagic4 & 0x80) >= 1 ? true : false);

            ToolTip(checkBoxMagicDeath, 1, (KernelWorker.GetSelectedMagicData.StatusMagic5 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxMagicPoison, 1, (KernelWorker.GetSelectedMagicData.StatusMagic5 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxMagicPetrify, 1, (KernelWorker.GetSelectedMagicData.StatusMagic5 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxMagicDarkness, 1, (KernelWorker.GetSelectedMagicData.StatusMagic5 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxMagicSilence, 1, (KernelWorker.GetSelectedMagicData.StatusMagic5 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxMagicBerserk, 1, (KernelWorker.GetSelectedMagicData.StatusMagic5 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxMagicZombie, 1, (KernelWorker.GetSelectedMagicData.StatusMagic5 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxMagicUnk7, 1, (KernelWorker.GetSelectedMagicData.StatusMagic5 & 0x80) >= 1 ? true : false);
        }


        private void listBoxMagic_SelectedIndexChanged(object sender, EventArgs e)
        {
            _loaded = false;
            if (KernelWorker.Kernel == null || KernelWorker.BackupKernel == null)
                return;

            KernelWorker.ReadMagic(listBoxMagic.SelectedIndex, KernelWorker.BackupKernel);
            try
            {
                ToolTip(comboBoxMagicMagicID, 2, comboBoxMagicMagicID.Items[KernelWorker.GetSelectedMagicData.MagicID]);
                ToolTip(numericUpDownMagicSpellPower,0,KernelWorker.GetSelectedMagicData.SpellPower);
                ToolTip(comboBoxMagicAttackType,2,comboBoxMagicAttackType.Items[KernelWorker.GetSelectedMagicData.AttackType]);
                ToolTip(checkBoxMagicTarget1,1,(KernelWorker.GetSelectedMagicData.DefaultTarget & 0x01) >= 1 ? true : false);
                ToolTip(checkBoxMagicTarget2, 1, (KernelWorker.GetSelectedMagicData.DefaultTarget & 0x02) >= 1 ? true : false);
                ToolTip(checkBoxMagicTarget3, 1, (KernelWorker.GetSelectedMagicData.DefaultTarget & 0x04) >= 1 ? true : false);
                ToolTip(checkBoxMagicTarget4, 1, (KernelWorker.GetSelectedMagicData.DefaultTarget & 0x08) >= 1 ? true : false);
                ToolTip(checkBoxMagicTarget5, 1, (KernelWorker.GetSelectedMagicData.DefaultTarget & 0x10) >= 1 ? true : false);
                ToolTip(checkBoxMagicTarget6, 1, (KernelWorker.GetSelectedMagicData.DefaultTarget & 0x20) >= 1 ? true : false);
                ToolTip(checkBoxMagicTarget7, 1, (KernelWorker.GetSelectedMagicData.DefaultTarget & 0x40) >= 1 ? true : false);
                ToolTip(checkBoxMagicTarget8, 1, (KernelWorker.GetSelectedMagicData.DefaultTarget & 0x80) >= 1 ? true : false);
                ToolTip(checkBoxMagicFlag1,1,(KernelWorker.GetSelectedMagicData.Flags & 0x01) >= 1 ? true : false);
                ToolTip(checkBoxMagicFlag2, 1, (KernelWorker.GetSelectedMagicData.Flags & 0x02) >= 1 ? true : false);
                ToolTip(checkBoxMagicFlag3, 1, (KernelWorker.GetSelectedMagicData.Flags & 0x04) >= 1 ? true : false);
                ToolTip(checkBoxMagicBreakDamageLimit,1,(KernelWorker.GetSelectedMagicData.Flags & 0x08) >= 1 ? true : false);
                ToolTip(checkBoxMagicFlag5,1,(KernelWorker.GetSelectedMagicData.Flags & 0x10) >= 1 ? true : false);
                ToolTip(checkBoxMagicFlag6, 1, (KernelWorker.GetSelectedMagicData.Flags & 0x20) >= 1 ? true : false);
                ToolTip(checkBoxMagicFlag7, 1, (KernelWorker.GetSelectedMagicData.Flags & 0x40) >= 1 ? true : false);
                ToolTip(checkBoxMagicFlag8, 1, (KernelWorker.GetSelectedMagicData.Flags & 0x80) >= 1 ? true : false);
                ToolTip(numericUpDownMagicDrawResist,0,KernelWorker.GetSelectedMagicData.DrawResist);
                ToolTip(numericUpDownMagicHitCount,0,KernelWorker.GetSelectedMagicData.HitCount);
                ToolTip(comboBoxMagicElement,2,comboBoxMagicElement.Items[ Magic_GetElement()]);
                MagicStatusWorker(true);
                ToolTip(numericUpDownMagicHPJ,0, KernelWorker.GetSelectedMagicData.HP);
                ToolTip(numericUpDownMagicVITJ, 0, KernelWorker.GetSelectedMagicData.VIT);
                ToolTip(numericUpDownMagicSPRJ, 0, KernelWorker.GetSelectedMagicData.SPR);
                ToolTip(numericUpDownMagicSTRJ, 0, KernelWorker.GetSelectedMagicData.STR);
                ToolTip(numericUpDownMagicMAGJ, 0, KernelWorker.GetSelectedMagicData.MAG);
                ToolTip(numericUpDownMagicSPDJ, 0, KernelWorker.GetSelectedMagicData.SPD);
                ToolTip(numericUpDownMagicEVAJ, 0, KernelWorker.GetSelectedMagicData.EVA);
                ToolTip(numericUpDownMagicHITJ, 0, KernelWorker.GetSelectedMagicData.HIT);
                ToolTip(numericUpDownMagicLUCKJ, 0, KernelWorker.GetSelectedMagicData.LUCK);
                BackupStatusHoldWorker(0, KernelWorker.GetSelectedMagicData.ElemAttackEN);
                ToolTip(trackBarJElemAttack,0, KernelWorker.GetSelectedMagicData.ElemAttackVAL);
                BackupStatusHoldWorker(1, KernelWorker.GetSelectedMagicData.ElemDefenseEN);
                ToolTip(trackBarJElemDefense,0, KernelWorker.GetSelectedMagicData.ElemDefenseVAL);
                BackupStatusHoldWorker(2, KernelWorker.GetSelectedMagicData.StatusMagic1, KernelWorker.GetSelectedMagicData.StatusATKEN, KernelWorker.GetSelectedMagicData.StatusMagic2, KernelWorker.GetSelectedMagicData.StatusMagic3, KernelWorker.GetSelectedMagicData.StatusMagic4, KernelWorker.GetSelectedMagicData.StatusMagic5);
                ToolTip(trackBarJStatAttack,0,KernelWorker.GetSelectedMagicData.StatusATKval);
                BackupStatusHoldWorker(3, KernelWorker.GetSelectedMagicData.StatusMagic1, KernelWorker.GetSelectedMagicData.StatusDefEN, KernelWorker.GetSelectedMagicData.StatusMagic2, KernelWorker.GetSelectedMagicData.StatusMagic3, KernelWorker.GetSelectedMagicData.StatusMagic4, KernelWorker.GetSelectedMagicData.StatusMagic5);
                ToolTip(trackBarJStatDefense,0, KernelWorker.GetSelectedMagicData.StatusDEFval);
                ToolTip(numericUpDownMagicStatusAttack,3, KernelWorker.GetSelectedMagicData.StatusAttack);
                ToolTip(numericUpDownMagicQuezacoltComp,3, (100 - Convert.ToDecimal(KernelWorker.GetSelectedMagicData.QuezacoltCompatibility)) / 5);
                ToolTip(numericUpDownMagicShivaComp, 3, (100 - Convert.ToDecimal(KernelWorker.GetSelectedMagicData.ShivaCompatibility)) / 5);
                ToolTip(numericUpDownMagicIfritComp, 3, (100 - Convert.ToDecimal(KernelWorker.GetSelectedMagicData.IfritCompatibility)) / 5);
                ToolTip(numericUpDownMagicSirenComp, 3, (100 - Convert.ToDecimal(KernelWorker.GetSelectedMagicData.SirenCompatibility)) / 5);
                ToolTip(numericUpDownMagicBrothersComp, 3, (100 - Convert.ToDecimal(KernelWorker.GetSelectedMagicData.BrothersCompatibility)) / 5);
                ToolTip(numericUpDownMagicDiablosComp, 3, (100 - Convert.ToDecimal(KernelWorker.GetSelectedMagicData.DiablosCompatibility)) / 5);
                ToolTip(numericUpDownMagicCarbuncleComp, 3, (100 - Convert.ToDecimal(KernelWorker.GetSelectedMagicData.CarbuncleCompatibility)) / 5);
                ToolTip(numericUpDownMagicLeviathanComp, 3, (100 - Convert.ToDecimal(KernelWorker.GetSelectedMagicData.LeviathanCompatibility)) / 5);
                ToolTip(numericUpDownMagicPandemonaComp, 3, (100 - Convert.ToDecimal(KernelWorker.GetSelectedMagicData.PandemonaCompatibility)) / 5);
                ToolTip(numericUpDownMagicCerberusComp, 3, (100 - Convert.ToDecimal(KernelWorker.GetSelectedMagicData.CerberusCompatibility)) / 5);
                ToolTip(numericUpDownMagicAlexanderComp, 3, (100 - Convert.ToDecimal(KernelWorker.GetSelectedMagicData.AlexanderCompatibility)) / 5);
                ToolTip(numericUpDownMagicDoomtrainComp, 3, (100 - Convert.ToDecimal(KernelWorker.GetSelectedMagicData.DoomtrainCompatibility)) / 5);
                ToolTip(numericUpDownMagicBahamutComp, 3, (100 - Convert.ToDecimal(KernelWorker.GetSelectedMagicData.BahamutCompatibility)) / 5);
                ToolTip(numericUpDownMagicCactuarComp, 3, (100 - Convert.ToDecimal(KernelWorker.GetSelectedMagicData.CactuarCompatibility)) / 5);
                ToolTip(numericUpDownMagicTonberryComp, 3, (100 - Convert.ToDecimal(KernelWorker.GetSelectedMagicData.TonberryCompatibility)) / 5);
                ToolTip(numericUpDownMagicEdenComp, 3, (100 - Convert.ToDecimal(KernelWorker.GetSelectedMagicData.EdenCompatibility)) / 5);
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }

            KernelWorker.ReadMagic(listBoxMagic.SelectedIndex, KernelWorker.Kernel);
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
                MagicStatusWorker(false);
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
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
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

        private void BackupStatusHoldWorker(byte State, byte Element = 0, ushort Stat = 0, byte Status2 = 0, byte Status3 = 0, byte Status4 = 0, byte Status5 = 0)
        {
            //List<Control> 
            if (State == 0)
            {
                switch (Element)
                {
                    case 0x00:
                        goto default;
                    case 0x01:
                        ToolTip(radioButtonJElemAttackFire,1,true);
                        ToolTip(radioButtonJElemAttackIce, 1, false);
                        ToolTip(radioButtonJElemAttackThunder, 1, false);
                        ToolTip(radioButtonJElemAttackEarth, 1, false);
                        ToolTip(radioButtonJElemAttackPoison, 1, false);
                        ToolTip(radioButtonJElemAttackWind, 1, false);
                        ToolTip(radioButtonJElemAttackWater, 1, false);
                        ToolTip(radioButtonJElemAttackHoly, 1, false);
                        ToolTip(radioButtonJElemAttackNElem, 1, false);
                        return;
                    case 0x02:
                        ToolTip(radioButtonJElemAttackFire, 1, false);
                        ToolTip(radioButtonJElemAttackIce, 1, true);
                        ToolTip(radioButtonJElemAttackThunder, 1, false);
                        ToolTip(radioButtonJElemAttackEarth, 1, false);
                        ToolTip(radioButtonJElemAttackPoison, 1, false);
                        ToolTip(radioButtonJElemAttackWind, 1, false);
                        ToolTip(radioButtonJElemAttackWater, 1, false);
                        ToolTip(radioButtonJElemAttackHoly, 1, false);
                        ToolTip(radioButtonJElemAttackNElem, 1, false);
                        return;
                    case 0x04:
                        ToolTip(radioButtonJElemAttackFire, 1, false);
                        ToolTip(radioButtonJElemAttackIce, 1, false);
                        ToolTip(radioButtonJElemAttackThunder, 1, true);
                        ToolTip(radioButtonJElemAttackEarth, 1, false);
                        ToolTip(radioButtonJElemAttackPoison, 1, false);
                        ToolTip(radioButtonJElemAttackWind, 1, false);
                        ToolTip(radioButtonJElemAttackWater, 1, false);
                        ToolTip(radioButtonJElemAttackHoly, 1, false);
                        ToolTip(radioButtonJElemAttackNElem, 1, false);
                        return;
                    case 0x08:
                        ToolTip(radioButtonJElemAttackFire, 1, false);
                        ToolTip(radioButtonJElemAttackIce, 1, false);
                        ToolTip(radioButtonJElemAttackThunder, 1, false);
                        ToolTip(radioButtonJElemAttackEarth, 1, true);
                        ToolTip(radioButtonJElemAttackPoison, 1, false);
                        ToolTip(radioButtonJElemAttackWind, 1, false);
                        ToolTip(radioButtonJElemAttackWater, 1, false);
                        ToolTip(radioButtonJElemAttackHoly, 1, false);
                        ToolTip(radioButtonJElemAttackNElem, 1, false);
                        return;
                    case 0x10:
                        ToolTip(radioButtonJElemAttackFire, 1, false);
                        ToolTip(radioButtonJElemAttackIce, 1, false);
                        ToolTip(radioButtonJElemAttackThunder, 1, false);
                        ToolTip(radioButtonJElemAttackEarth, 1, false);
                        ToolTip(radioButtonJElemAttackPoison, 1, true);
                        ToolTip(radioButtonJElemAttackWind, 1, false);
                        ToolTip(radioButtonJElemAttackWater, 1, false);
                        ToolTip(radioButtonJElemAttackHoly, 1, false);
                        ToolTip(radioButtonJElemAttackNElem, 1, false);
                        return;
                    case 0x20:
                        ToolTip(radioButtonJElemAttackFire, 1, false);
                        ToolTip(radioButtonJElemAttackIce, 1, false);
                        ToolTip(radioButtonJElemAttackThunder, 1, false);
                        ToolTip(radioButtonJElemAttackEarth, 1, false);
                        ToolTip(radioButtonJElemAttackPoison, 1, false);
                        ToolTip(radioButtonJElemAttackWind, 1, true);
                        ToolTip(radioButtonJElemAttackWater, 1, false);
                        ToolTip(radioButtonJElemAttackHoly, 1, false);
                        ToolTip(radioButtonJElemAttackNElem, 1, false);
                        return;
                    case 0x40:
                        ToolTip(radioButtonJElemAttackFire, 1, false);
                        ToolTip(radioButtonJElemAttackIce, 1, false);
                        ToolTip(radioButtonJElemAttackThunder, 1, false);
                        ToolTip(radioButtonJElemAttackEarth, 1, false);
                        ToolTip(radioButtonJElemAttackPoison, 1, false);
                        ToolTip(radioButtonJElemAttackWind, 1, false);
                        ToolTip(radioButtonJElemAttackWater, 1, true);
                        ToolTip(radioButtonJElemAttackHoly, 1, false);
                        ToolTip(radioButtonJElemAttackNElem, 1, false);
                        return;
                    case 0x80:
                        ToolTip(radioButtonJElemAttackFire, 1, false);
                        ToolTip(radioButtonJElemAttackIce, 1, false);
                        ToolTip(radioButtonJElemAttackThunder, 1, false);
                        ToolTip(radioButtonJElemAttackEarth, 1, false);
                        ToolTip(radioButtonJElemAttackPoison, 1, false);
                        ToolTip(radioButtonJElemAttackWind, 1, false);
                        ToolTip(radioButtonJElemAttackWater, 1, false);
                        ToolTip(radioButtonJElemAttackHoly, 1, true);
                        ToolTip(radioButtonJElemAttackNElem, 1, false);
                        return;
                    default:
                        ToolTip(radioButtonJElemAttackFire, 1, false);
                        ToolTip(radioButtonJElemAttackIce, 1, false);
                        ToolTip(radioButtonJElemAttackThunder, 1, false);
                        ToolTip(radioButtonJElemAttackEarth, 1, false);
                        ToolTip(radioButtonJElemAttackPoison, 1, false);
                        ToolTip(radioButtonJElemAttackWind, 1, false);
                        ToolTip(radioButtonJElemAttackWater, 1, false);
                        ToolTip(radioButtonJElemAttackHoly, 1, false);
                        ToolTip(radioButtonJElemAttackNElem, 1, true);
                        return;
                }
            }
            if (State == 1)
            {
                if ((Element & 0x01) > 0)
                    ToolTip(checkBoxJElemDefenseFire, 1, true);
                else ToolTip(checkBoxJElemDefenseFire, 1, false);
                if ((Element & 0x02) > 0)
                    ToolTip(checkBoxJElemDefenseIce, 1, true);
                else ToolTip(checkBoxJElemDefenseIce, 1, false);
                if ((Element & 0x04) > 0)
                    ToolTip(checkBoxJElemDefenseThunder, 1, true);
                else ToolTip(checkBoxJElemDefenseThunder, 1, false);
                if ((Element & 0x08) > 0)
                    ToolTip(checkBoxJElemDefenseEarth, 1, true);
                else ToolTip(checkBoxJElemDefenseEarth, 1, false);
                if ((Element & 0x10) > 0)
                    ToolTip(checkBoxJElemDefensePoison, 1, true);
                else ToolTip(checkBoxJElemDefensePoison, 1, false);
                if ((Element & 0x20) > 0)
                    ToolTip(checkBoxJElemDefenseWind, 1, true);
                else ToolTip(checkBoxJElemDefenseWind, 1, false);
                if ((Element & 0x40) > 0)
                    ToolTip(checkBoxJElemDefenseWater, 1, true);
                else ToolTip(checkBoxJElemDefenseWater, 1, false);
                if ((Element & 0x80) > 0)
                    ToolTip(checkBoxJElemDefenseHoly, 1, true);
                else ToolTip(checkBoxJElemDefenseHoly, 1, false);

            }

            if (State == 2)
            {
                if ((Stat & 0x0001) > 0)
                    ToolTip(checkBoxJStatAttackDeath, 1, true); 
                else ToolTip(checkBoxJStatAttackDeath, 1, false);
                if ((Stat & 0x0002) > 0)
                    ToolTip(checkBoxJStatAttackPoison, 1, true); 
                else ToolTip(checkBoxJStatAttackPoison, 1, false);
                if ((Stat & 0x0004) > 0)
                    ToolTip(checkBoxJStatAttackPetrify, 1, true);
                else ToolTip(checkBoxJStatAttackPetrify, 1, false);
                if ((Stat & 0x0008) > 0)
                    ToolTip(checkBoxJStatAttackDarkness, 1, true); 
                else ToolTip(checkBoxJStatAttackDarkness, 1, false);
                if ((Stat & 0x0010) > 0)
                    ToolTip(checkBoxJStatAttackSilence, 1, true);
                else ToolTip(checkBoxJStatAttackSilence, 1, false);
                if ((Stat & 0x0020) > 0)
                    ToolTip(checkBoxJStatAttackBerserk, 1, true);
                else ToolTip(checkBoxJStatAttackBerserk, 1, false);
                if ((Stat & 0x0040) > 0)
                    ToolTip(checkBoxJStatAttackZombie, 1, true);
                else ToolTip(checkBoxJStatAttackZombie, 1, false);
                if ((Stat & 0x0080) > 0)
                    ToolTip(checkBoxJStatAttackSleep, 1, true);
                else ToolTip(checkBoxJStatAttackSleep, 1, false);
                if ((Stat & 0x0100) > 0)
                    ToolTip(checkBoxJStatAttackSlow, 1, true);
                else ToolTip(checkBoxJStatAttackSlow, 1, false);
                if ((Stat & 0x0200) > 0)
                    ToolTip(checkBoxJStatAttackStop, 1, true);
                else ToolTip(checkBoxJStatAttackStop, 1, false);
                if ((Stat & 0x0800) > 0)
                    ToolTip(checkBoxJStatAttackConfusion, 1, true);
                else ToolTip(checkBoxJStatAttackConfusion, 1, false);
                if ((Stat & 0x1000) > 0)
                    ToolTip(checkBoxJStatAttackDrain, 1, true);
                else ToolTip(checkBoxJStatAttackDrain, 1, false);
            }
            if (State == 3)
            {
                if ((Stat & 0x01) > 0)
                    ToolTip(checkBoxJStatDefenseDeath, 1, true);
                else ToolTip(checkBoxJStatDefenseDeath, 1, false);
                if ((Stat & 0x02) > 0)
                    ToolTip(checkBoxJStatDefensePoison, 1, true);
                else ToolTip(checkBoxJStatDefensePoison, 1, false);
                if ((Stat & 0x04) > 0)
                    ToolTip(checkBoxJStatDefensePetrify, 1, true);
                else ToolTip(checkBoxJStatDefensePetrify, 1, false);
                if ((Stat & 0x08) > 0)
                    ToolTip(checkBoxJStatDefenseDarnkess, 1, true); //Darnkess? :D
                else ToolTip(checkBoxJStatDefenseDarnkess, 1, false);
                if ((Stat & 0x10) > 0)
                    ToolTip(checkBoxJStatDefenseSilence, 1, true);
                else ToolTip(checkBoxJStatDefenseSilence, 1, false);
                if ((Stat & 0x20) > 0)
                    ToolTip(checkBoxJStatDefenseBerserk, 1, true);
                else ToolTip(checkBoxJStatDefenseBerserk, 1, false);
                if ((Stat & 0x40) > 0)
                    ToolTip(checkBoxJStatDefenseZombie, 1, true);
                else ToolTip(checkBoxJStatDefenseZombie, 1, false);
                if ((Stat & 0x80) > 0)
                    ToolTip(checkBoxJStatDefenseSleep, 1, true);
                else ToolTip(checkBoxJStatDefenseSleep, 1, false);
                if ((Stat & 0x100) > 0)
                    ToolTip(checkBoxJStatDefenseSlow, 1, true);
                else ToolTip(checkBoxJStatDefenseSlow, 1, false);
                if ((Stat & 0x0200) > 0)
                    ToolTip(checkBoxJStatDefenseStop, 1, true);
                else ToolTip(checkBoxJStatDefenseStop, 1, false);
                if ((Stat & 0x0400) > 0)
                    ToolTip(checkBoxJStatDefenseCurse, 1, true);
                else ToolTip(checkBoxJStatDefenseCurse, 1, false);
                if ((Stat & 0x0800) > 0)
                    ToolTip(checkBoxJStatDefenseConfusion, 1, true);
                else ToolTip(checkBoxJStatDefenseConfusion, 1, false);
                if ((Stat & 0x1000) > 0)
                    ToolTip(checkBoxJStatDefenseDrain, 1, true);
                else ToolTip(checkBoxJStatDefenseDrain, 1, false);
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

        private void GFStatusWorker(bool bBackup)
        {
            if (!bBackup)
            {
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
                return;
            }
            ToolTip(checkBoxGFSleep,1, (KernelWorker.GetSelectedGFData.StatusGF2 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxGFHaste, 1, (KernelWorker.GetSelectedGFData.StatusGF2 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxGFSlow, 1, (KernelWorker.GetSelectedGFData.StatusGF2 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxGFStop, 1, (KernelWorker.GetSelectedGFData.StatusGF2 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxGFRegen, 1, (KernelWorker.GetSelectedGFData.StatusGF2 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxGFProtect, 1, (KernelWorker.GetSelectedGFData.StatusGF2 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxGFShell, 1, (KernelWorker.GetSelectedGFData.StatusGF2 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxGFReflect, 1, (KernelWorker.GetSelectedGFData.StatusGF2 & 0x80) >= 1 ? true : false);

            ToolTip(checkBoxGFAura, 1, (KernelWorker.GetSelectedGFData.StatusGF3 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxGFCurse, 1, (KernelWorker.GetSelectedGFData.StatusGF3 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxGFDoom, 1, (KernelWorker.GetSelectedGFData.StatusGF3 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxGFInvincible, 1, (KernelWorker.GetSelectedGFData.StatusGF3 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxGFPetrifying, 1, (KernelWorker.GetSelectedGFData.StatusGF3 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxGFFloat, 1, (KernelWorker.GetSelectedGFData.StatusGF3 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxGFConfusion, 1, (KernelWorker.GetSelectedGFData.StatusGF3 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxGFDrain, 1, (KernelWorker.GetSelectedGFData.StatusGF3 & 0x80) >= 1 ? true : false);

            ToolTip(checkBoxGFEject, 1, (KernelWorker.GetSelectedGFData.StatusGF4 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxGFDouble, 1, (KernelWorker.GetSelectedGFData.StatusGF4 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxGFTriple, 1, (KernelWorker.GetSelectedGFData.StatusGF4 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxGFDefend, 1, (KernelWorker.GetSelectedGFData.StatusGF4 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxGFUnk1, 1, (KernelWorker.GetSelectedGFData.StatusGF4 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxGFUnk2, 1, (KernelWorker.GetSelectedGFData.StatusGF4 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxGFCharged, 1, (KernelWorker.GetSelectedGFData.StatusGF4 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxGFBackAttack, 1, (KernelWorker.GetSelectedGFData.StatusGF4 & 0x80) >= 1 ? true : false);

            ToolTip(checkBoxGFVit0, 1, (KernelWorker.GetSelectedGFData.StatusGF5 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxGFAngelWing, 1, (KernelWorker.GetSelectedGFData.StatusGF5 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxGFUnk3, 1, (KernelWorker.GetSelectedGFData.StatusGF5 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxGFUnk4, 1, (KernelWorker.GetSelectedGFData.StatusGF5 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxGFUnk5, 1, (KernelWorker.GetSelectedGFData.StatusGF5 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxGFUnk6, 1, (KernelWorker.GetSelectedGFData.StatusGF5 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxGFHasMagic, 1, (KernelWorker.GetSelectedGFData.StatusGF5 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxGFSummonGF, 1, (KernelWorker.GetSelectedGFData.StatusGF5 & 0x80) >= 1 ? true : false);

            ToolTip(checkBoxGFDeath, 1, (KernelWorker.GetSelectedGFData.StatusGF1 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxGFPoison, 1, (KernelWorker.GetSelectedGFData.StatusGF1 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxGFPetrify, 1, (KernelWorker.GetSelectedGFData.StatusGF1 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxGFDarkness, 1, (KernelWorker.GetSelectedGFData.StatusGF1 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxGFSilence, 1, (KernelWorker.GetSelectedGFData.StatusGF1 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxGFBerserk, 1, (KernelWorker.GetSelectedGFData.StatusGF1 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxGFZombie, 1, (KernelWorker.GetSelectedGFData.StatusGF1 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxGFUnk7, 1, (KernelWorker.GetSelectedGFData.StatusGF1 & 0x80) >= 1 ? true : false);
        }

        private void listBoxGF_SelectedIndexChanged(object sender, EventArgs e)
        {
            _loaded = false;
            if (KernelWorker.Kernel == null || KernelWorker.BackupKernel == null)
                return;

            KernelWorker.ReadGF(listBoxGF.SelectedIndex, KernelWorker.BackupKernel);
            try
            {
                ToolTip(comboBoxGFMagicID,2, comboBoxGFMagicID.Items[KernelWorker.GetSelectedGFData.GFMagicID]);
                ToolTip(comboBoxGFAttackType, 2, comboBoxGFAttackType.Items[KernelWorker.GetSelectedGFData.GFAttackType]);
                ToolTip(numericUpDownGFPower,0,KernelWorker.GetSelectedGFData.GFPower);
                ToolTip(checkBoxGFFlagShelled,1, (KernelWorker.GetSelectedGFData.GFFlags & 0x01) >= 1 ? true : false);
                ToolTip(checkBoxGFFlag2, 1, (KernelWorker.GetSelectedGFData.GFFlags & 0x02) >= 1 ? true : false);
                ToolTip(checkBoxGFFlag3, 1, (KernelWorker.GetSelectedGFData.GFFlags & 0x04) >= 1 ? true : false);
                ToolTip(checkBoxGFFlagBreakDamageLimit, 1, (KernelWorker.GetSelectedGFData.GFFlags & 0x08) >= 1 ? true : false);
                ToolTip(checkBoxGFFlagReflected, 1, (KernelWorker.GetSelectedGFData.GFFlags & 0x10) >= 1 ? true : false);
                ToolTip(checkBoxGFFlag6, 1, (KernelWorker.GetSelectedGFData.GFFlags & 0x20) >= 1 ? true : false);
                ToolTip(checkBoxGFFlag7, 1, (KernelWorker.GetSelectedGFData.GFFlags & 0x40) >= 1 ? true : false);
                ToolTip(checkBoxGFFlag8, 1, (KernelWorker.GetSelectedGFData.GFFlags & 0x80) >= 1 ? true : false);
                ToolTip(numericUpDownGFHP, 0, KernelWorker.GetSelectedGFData.GFHP);
                ToolTip(numericUpDownGFEXP, 0, KernelWorker.GetSelectedGFData.GFEXP * 10);
                ToolTip(numericUpDownGFPowerMod, 0, KernelWorker.GetSelectedGFData.GFPowerMod);
                ToolTip(numericUpDownGFLevelMod, 0, KernelWorker.GetSelectedGFData.GFLevelMod);
                ToolTip(comboBoxGFAbility1,2, comboBoxGFAbility1.Items[KernelWorker.GetSelectedGFData.GFAbility1]);
                ToolTip(comboBoxGFAbility2, 2, comboBoxGFAbility2.Items[KernelWorker.GetSelectedGFData.GFAbility2]);
                ToolTip(comboBoxGFAbility3, 2, comboBoxGFAbility3.Items[KernelWorker.GetSelectedGFData.GFAbility3]);
                ToolTip(comboBoxGFAbility4, 2, comboBoxGFAbility4.Items[KernelWorker.GetSelectedGFData.GFAbility4]);
                ToolTip(comboBoxGFAbility5, 2, comboBoxGFAbility5.Items[KernelWorker.GetSelectedGFData.GFAbility5]);
                ToolTip(comboBoxGFAbility6, 2, comboBoxGFAbility6.Items[KernelWorker.GetSelectedGFData.GFAbility6]);
                ToolTip(comboBoxGFAbility7, 2, comboBoxGFAbility7.Items[KernelWorker.GetSelectedGFData.GFAbility7]);
                ToolTip(comboBoxGFAbility8, 2, comboBoxGFAbility8.Items[KernelWorker.GetSelectedGFData.GFAbility8]);
                ToolTip(comboBoxGFAbility9, 2, comboBoxGFAbility9.Items[KernelWorker.GetSelectedGFData.GFAbility9]);
                ToolTip(comboBoxGFAbility10, 2, comboBoxGFAbility10.Items[KernelWorker.GetSelectedGFData.GFAbility10]);
                ToolTip(comboBoxGFAbility11, 2, comboBoxGFAbility11.Items[KernelWorker.GetSelectedGFData.GFAbility11]);
                ToolTip(comboBoxGFAbility12, 2, comboBoxGFAbility12.Items[KernelWorker.GetSelectedGFData.GFAbility12]);
                ToolTip(comboBoxGFAbility13, 2, comboBoxGFAbility13.Items[KernelWorker.GetSelectedGFData.GFAbility13]);
                ToolTip(comboBoxGFAbility14, 2, comboBoxGFAbility14.Items[KernelWorker.GetSelectedGFData.GFAbility14]);
                ToolTip(comboBoxGFAbility15, 2, comboBoxGFAbility15.Items[KernelWorker.GetSelectedGFData.GFAbility15]);
                ToolTip(comboBoxGFAbility16, 2, comboBoxGFAbility16.Items[KernelWorker.GetSelectedGFData.GFAbility16]);
                ToolTip(comboBoxGFAbility17, 2, comboBoxGFAbility17.Items[KernelWorker.GetSelectedGFData.GFAbility17]);
                ToolTip(comboBoxGFAbility18, 2, comboBoxGFAbility18.Items[KernelWorker.GetSelectedGFData.GFAbility18]);
                ToolTip(comboBoxGFAbility19, 2, comboBoxGFAbility19.Items[KernelWorker.GetSelectedGFData.GFAbility19]);
                ToolTip(comboBoxGFAbility20, 2, comboBoxGFAbility20.Items[KernelWorker.GetSelectedGFData.GFAbility20]);
                ToolTip(comboBoxGFAbility21, 2, comboBoxGFAbility21.Items[KernelWorker.GetSelectedGFData.GFAbility21]);
                ToolTip(comboBoxGFAbilityUnlock1,2, comboBoxGFAbilityUnlock1.Items[KernelWorker.GetSelectedGFData.GFAbilityUnlock1]);
                ToolTip(comboBoxGFAbilityUnlock2, 2, comboBoxGFAbilityUnlock2.Items[KernelWorker.GetSelectedGFData.GFAbilityUnlock2]);
                ToolTip(comboBoxGFAbilityUnlock3, 2, comboBoxGFAbilityUnlock3.Items[KernelWorker.GetSelectedGFData.GFAbilityUnlock3]);
                ToolTip(comboBoxGFAbilityUnlock4, 2, comboBoxGFAbilityUnlock4.Items[KernelWorker.GetSelectedGFData.GFAbilityUnlock4]);
                ToolTip(comboBoxGFAbilityUnlock5, 2, comboBoxGFAbilityUnlock5.Items[KernelWorker.GetSelectedGFData.GFAbilityUnlock5]);
                ToolTip(comboBoxGFAbilityUnlock6, 2, comboBoxGFAbilityUnlock6.Items[KernelWorker.GetSelectedGFData.GFAbilityUnlock6]);
                ToolTip(comboBoxGFAbilityUnlock7, 2, comboBoxGFAbilityUnlock7.Items[KernelWorker.GetSelectedGFData.GFAbilityUnlock7]);
                ToolTip(comboBoxGFAbilityUnlock8, 2, comboBoxGFAbilityUnlock8.Items[KernelWorker.GetSelectedGFData.GFAbilityUnlock8]);
                ToolTip(comboBoxGFAbilityUnlock9, 2, comboBoxGFAbilityUnlock9.Items[KernelWorker.GetSelectedGFData.GFAbilityUnlock9]);
                ToolTip(comboBoxGFAbilityUnlock10, 2, comboBoxGFAbilityUnlock10.Items[KernelWorker.GetSelectedGFData.GFAbilityUnlock10]);
                ToolTip(comboBoxGFAbilityUnlock11, 2, comboBoxGFAbilityUnlock11.Items[KernelWorker.GetSelectedGFData.GFAbilityUnlock11]);
                ToolTip(comboBoxGFAbilityUnlock12, 2, comboBoxGFAbilityUnlock12.Items[KernelWorker.GetSelectedGFData.GFAbilityUnlock12]);
                ToolTip(comboBoxGFAbilityUnlock13, 2, comboBoxGFAbilityUnlock13.Items[KernelWorker.GetSelectedGFData.GFAbilityUnlock13]);
                ToolTip(comboBoxGFAbilityUnlock14, 2, comboBoxGFAbilityUnlock14.Items[KernelWorker.GetSelectedGFData.GFAbilityUnlock14]);
                ToolTip(comboBoxGFAbilityUnlock15, 2, comboBoxGFAbilityUnlock15.Items[KernelWorker.GetSelectedGFData.GFAbilityUnlock15]);
                ToolTip(comboBoxGFAbilityUnlock16, 2, comboBoxGFAbilityUnlock16.Items[KernelWorker.GetSelectedGFData.GFAbilityUnlock16]);
                ToolTip(comboBoxGFAbilityUnlock17, 2, comboBoxGFAbilityUnlock17.Items[KernelWorker.GetSelectedGFData.GFAbilityUnlock17]);
                ToolTip(comboBoxGFAbilityUnlock18, 2, comboBoxGFAbilityUnlock18.Items[KernelWorker.GetSelectedGFData.GFAbilityUnlock18]);
                ToolTip(comboBoxGFAbilityUnlock19, 2, comboBoxGFAbilityUnlock19.Items[KernelWorker.GetSelectedGFData.GFAbilityUnlock19]);
                ToolTip(comboBoxGFAbilityUnlock20, 2, comboBoxGFAbilityUnlock20.Items[KernelWorker.GetSelectedGFData.GFAbilityUnlock20]);
                ToolTip(comboBoxGFAbilityUnlock21, 2, comboBoxGFAbilityUnlock21.Items[KernelWorker.GetSelectedGFData.GFAbilityUnlock21]);
                ToolTip(comboBoxGFElement,2, comboBoxGFElement.Items[GF_GetElement()]);
                GFStatusWorker(true);
                ToolTip(numericUpDownGFStatusAttack,0, KernelWorker.GetSelectedGFData.GFStatusAttack);
                ToolTip(numericUpDownGFQuezacoltComp, 3, (100 - Convert.ToDecimal(KernelWorker.GetSelectedGFData.GFQuezacoltCompatibility)) / 5);
                ToolTip(numericUpDownGFShivaComp, 3, (100 - Convert.ToDecimal(KernelWorker.GetSelectedGFData.GFShivaCompatibility)) / 5);
                ToolTip(numericUpDownGFIfritComp, 3, (100 - Convert.ToDecimal(KernelWorker.GetSelectedGFData.GFIfritCompatibility)) / 5);
                ToolTip(numericUpDownGFSirenComp, 3, (100 - Convert.ToDecimal(KernelWorker.GetSelectedGFData.GFSirenCompatibility)) / 5);
                ToolTip(numericUpDownGFBrothersComp, 3, (100 - Convert.ToDecimal(KernelWorker.GetSelectedGFData.GFBrothersCompatibility)) / 5);
                ToolTip(numericUpDownGFDiablosComp, 3, (100 - Convert.ToDecimal(KernelWorker.GetSelectedGFData.GFDiablosCompatibility)) / 5);
                ToolTip(numericUpDownGFCarbuncleComp, 3, (100 - Convert.ToDecimal(KernelWorker.GetSelectedGFData.GFCarbuncleCompatibility)) / 5);
                ToolTip(numericUpDownGFLeviathanComp, 3, (100 - Convert.ToDecimal(KernelWorker.GetSelectedGFData.GFLeviathanCompatibility)) / 5);
                ToolTip(numericUpDownGFPandemonaComp, 3, (100 - Convert.ToDecimal(KernelWorker.GetSelectedGFData.GFPandemonaCompatibility)) / 5);
                ToolTip(numericUpDownGFCerberusComp, 3, (100 - Convert.ToDecimal(KernelWorker.GetSelectedGFData.GFCerberusCompatibility)) / 5);
                ToolTip(numericUpDownGFAlexanderComp, 3, (100 - Convert.ToDecimal(KernelWorker.GetSelectedGFData.GFAlexanderCompatibility)) / 5);
                ToolTip(numericUpDownGFDoomtrainComp, 3, (100 - Convert.ToDecimal(KernelWorker.GetSelectedGFData.GFDoomtrainCompatibility)) / 5);
                ToolTip(numericUpDownGFBahamutComp, 3, (100 - Convert.ToDecimal(KernelWorker.GetSelectedGFData.GFBahamutCompatibility)) / 5);
                ToolTip(numericUpDownGFCactuarComp, 3, (100 - Convert.ToDecimal(KernelWorker.GetSelectedGFData.GFCactuarCompatibility)) / 5);
                ToolTip(numericUpDownGFTonberryComp, 3, (100 - Convert.ToDecimal(KernelWorker.GetSelectedGFData.GFTonberryCompatibility)) / 5);
                ToolTip(numericUpDownGFEdenComp, 3, (100 - Convert.ToDecimal(KernelWorker.GetSelectedGFData.GFEdenCompatibility)) / 5);
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }

            KernelWorker.ReadGF(listBoxGF.SelectedIndex, KernelWorker.Kernel);
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
                numericUpDownGFEXP.Value = KernelWorker.GetSelectedGFData.GFEXP * 10;
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
                GFStatusWorker(false);
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
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }
            _loaded = true;
        }

        #endregion

        #region Non-J GFs Attacks

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

        private void GFAttacksStatusWorker(bool bBackup)
        {
            if(!bBackup)
            { 
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
            return;
            }       
            ToolTip(checkBoxGFAttacksSleep,1, (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks1 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxGFAttacksHaste,1, (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks1 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxGFAttacksSlow,1, (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks1 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxGFAttacksStop,1, (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks1 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxGFAttacksRegen,1, (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks1 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxGFAttacksProtect,1, (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks1 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxGFAttacksShell,1, (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks1 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxGFAttacksReflect,1, (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks1 & 0x80) >= 1 ? true : false);

            ToolTip(checkBoxGFAttacksAura,1, (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks2 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxGFAttacksCurse,1, (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks2 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxGFAttacksDoom,1, (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks2 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxGFAttacksInvincible,1, (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks2 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxGFAttacksPetrifying,1, (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks2 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxGFAttacksFloat,1, (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks2 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxGFAttacksConfusion,1, (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks2 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxGFAttacksDrain,1, (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks2 & 0x80) >= 1 ? true : false);

            ToolTip(checkBoxGFAttacksEject,1, (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks3 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxGFAttacksDouble,1, (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks3 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxGFAttacksTriple,1, (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks3 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxGFAttacksDefend,1, (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks3 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxGFAttacksUnk1,1, (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks3 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxGFAttacksUnk2,1, (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks3 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxGFAttacksCharged,1, (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks3 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxGFAttacksBackAttack,1, (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks3 & 0x80) >= 1 ? true : false);

            ToolTip(checkBoxGFAttacksVit0,1, (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks4 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxGFAttacksAngelWing,1, (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks4 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxGFAttacksUnk3,1, (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks4 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxGFAttacksUnk4,1, (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks4 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxGFAttacksUnk5,1, (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks4 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxGFAttacksUnk6,1, (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks4 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxGFAttacksHasMagic,1, (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks4 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxGFAttacksSummonGF,1, (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks4 & 0x80) >= 1 ? true : false);

            ToolTip(checkBoxGFAttacksDeath,1, (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks5 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxGFAttacksPoison,1, (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks5 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxGFAttacksPetrify,1, (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks5 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxGFAttacksDarkness,1, (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks5 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxGFAttacksSilence,1, (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks5 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxGFAttacksBerserk,1, (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks5 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxGFAttacksZombie,1, (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks5 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxGFAttacksUnk7,1, (KernelWorker.GetSelectedGFAttacksData.StatusGFAttacks5 & 0x80) >= 1 ? true : false);
        }


        private void listBoxGFAttacks_SelectedIndexChanged(object sender, EventArgs e)
        {
            _loaded = false;
            if (KernelWorker.Kernel == null || KernelWorker.BackupKernel == null)
                return;

            KernelWorker.ReadGFAttacks(listBoxGFAttacks.SelectedIndex, KernelWorker.BackupKernel);
            try
            {
                ToolTip(comboBoxGFAttacksMagicID,2, comboBoxGFAttacksMagicID.Items[KernelWorker.GetSelectedGFAttacksData.GFAttacksMagicID]);
                ToolTip(comboBoxGFAttacksAttackType, 2, comboBoxGFAttacksAttackType.Items[KernelWorker.GetSelectedGFAttacksData.GFAttacksAttackType]);
                ToolTip(numericUpDownGFAttacksPower,0,KernelWorker.GetSelectedGFAttacksData.GFAttacksPower);
                ToolTip(numericUpDownGFAttacksStatusAttack,0,KernelWorker.GetSelectedGFAttacksData.GFAttacksStatus);
                ToolTip(checkBoxGFAttacksFlagShelled,1,(KernelWorker.GetSelectedGFAttacksData.GFAttacksFlags & 0x01) >= 1 ? true : false);
                ToolTip(checkBoxGFAttacksFlag2,1,(KernelWorker.GetSelectedGFAttacksData.GFAttacksFlags & 0x02) >= 1 ? true : false);
                ToolTip(checkBoxGFAttacksFlag3, 1, (KernelWorker.GetSelectedGFAttacksData.GFAttacksFlags & 0x04) >= 1 ? true : false);
                ToolTip(checkBoxGFAttacksFlagBreakDamageLimit,1,(KernelWorker.GetSelectedGFAttacksData.GFAttacksFlags & 0x08) >= 1 ? true : false);
                ToolTip(checkBoxGFAttacksFlagReflected,1,(KernelWorker.GetSelectedGFAttacksData.GFAttacksFlags & 0x10) >= 1 ? true : false);
                ToolTip(checkBoxGFAttacksFlag6, 1, (KernelWorker.GetSelectedGFAttacksData.GFAttacksFlags & 0x20) >= 1 ? true : false);
                ToolTip(checkBoxGFAttacksFlag7, 1, (KernelWorker.GetSelectedGFAttacksData.GFAttacksFlags & 0x40) >= 1 ? true : false);
                ToolTip(checkBoxGFAttacksFlag8, 1, (KernelWorker.GetSelectedGFAttacksData.GFAttacksFlags & 0x80) >= 1 ? true : false);
                ToolTip(comboBoxGFAttacksElement,2, comboBoxGFAttacksElement.Items[GFAttacks_GetElement()]);
                GFAttacksStatusWorker(true);
                ToolTip(numericUpDownGFAttacksPowerMod,0,KernelWorker.GetSelectedGFAttacksData.GFAttacksPowerMod);
                ToolTip(numericUpDownGFAttacksLevelMod,0,KernelWorker.GetSelectedGFAttacksData.GFAttacksLevelMod);
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }

            KernelWorker.ReadGFAttacks(listBoxGFAttacks.SelectedIndex, KernelWorker.Kernel);
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
                GFAttacksStatusWorker(false);
                numericUpDownGFAttacksPowerMod.Value = KernelWorker.GetSelectedGFAttacksData.GFAttacksPowerMod;
                numericUpDownGFAttacksLevelMod.Value = KernelWorker.GetSelectedGFAttacksData.GFAttacksLevelMod;
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }
            _loaded = true;
        }

        #endregion

        #region Weapons

        private void RenzokukenFinishersWorker(bool bBackup)
        {
            if (bBackup)
            {
                string check;
                check = (KernelWorker.GetSelectedWeaponsData.RenzokukenFinishers & 0x01) >= 1 ? "Checked" : "Unchecked";
                toolTip1.SetToolTip(checkBoxWeaponsRenzoFinRough, $"Default: {check}");
                check = (KernelWorker.GetSelectedWeaponsData.RenzokukenFinishers & 0x02) >= 1 ? "Checked" : "Unchecked";
                toolTip1.SetToolTip(checkBoxWeaponsRenzoFinFated, $"Default: {check}");
                check = (KernelWorker.GetSelectedWeaponsData.RenzokukenFinishers & 0x04) >= 1 ? "Checked" : "Unchecked";
                toolTip1.SetToolTip(checkBoxWeaponsRenzoFinBlasting, $"Default: {check}");
                check = (KernelWorker.GetSelectedWeaponsData.RenzokukenFinishers & 0x08) >= 1 ? "Checked" : "Unchecked";
                toolTip1.SetToolTip(checkBoxWeaponsRenzoFinLion, $"Default: {check}");
            }
            else
            {
                checkBoxWeaponsRenzoFinRough.Checked = (KernelWorker.GetSelectedWeaponsData.RenzokukenFinishers & 0x01) >= 1 ? true : false;
                checkBoxWeaponsRenzoFinFated.Checked = (KernelWorker.GetSelectedWeaponsData.RenzokukenFinishers & 0x02) >= 1 ? true : false;
                checkBoxWeaponsRenzoFinBlasting.Checked = (KernelWorker.GetSelectedWeaponsData.RenzokukenFinishers & 0x04) >= 1 ? true : false;
                checkBoxWeaponsRenzoFinLion.Checked = (KernelWorker.GetSelectedWeaponsData.RenzokukenFinishers & 0x08) >= 1 ? true : false;
            }
            
        }


        private void listBoxWeapons_SelectedIndexChanged(object sender, EventArgs e)
        {
            _loaded = false;
            if (KernelWorker.Kernel == null || KernelWorker.BackupKernel == null)
                return;

            KernelWorker.ReadWeapons(listBoxWeapons.SelectedIndex, KernelWorker.BackupKernel);
            try
            {
                RenzokukenFinishersWorker(true);
                toolTip1.SetToolTip(comboBoxWeaponsCharacterID, $"Default: {comboBoxWeaponsCharacterID.Items[KernelWorker.GetSelectedWeaponsData.CharacterID]}");
                toolTip1.SetToolTip(comboBoxWeaponsAttackType, $"Default: {comboBoxWeaponsAttackType.Items[KernelWorker.GetSelectedWeaponsData.AttackType]}");
                toolTip1.SetToolTip(numericUpDownWeaponsAttackPower, $"Default: {KernelWorker.GetSelectedWeaponsData.AttackPower}");
                toolTip1.SetToolTip(numericUpDownWeaponsAttackParam, $"Default: {KernelWorker.GetSelectedWeaponsData.AttackParam}");
                toolTip1.SetToolTip(numericUpDownWeaponsSTRBonus, $"Default: {KernelWorker.GetSelectedWeaponsData.STRBonus}");
                toolTip1.SetToolTip(numericUpDownWeaponsTier, $"Default: {KernelWorker.GetSelectedWeaponsData.Tier}");
                toolTip1.SetToolTip(numericUpDownWeaponsCrit, $"Default: {KernelWorker.GetSelectedWeaponsData.CritBonus}");
                toolTip1.SetToolTip(numericUpDownWeaponsMelee, $"Default: {KernelWorker.GetSelectedWeaponsData.Melee}");
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }

            KernelWorker.ReadWeapons(listBoxWeapons.SelectedIndex, KernelWorker.Kernel);
            try
            {
                RenzokukenFinishersWorker(false);
                comboBoxWeaponsCharacterID.SelectedIndex = KernelWorker.GetSelectedWeaponsData.CharacterID;
                comboBoxWeaponsAttackType.SelectedIndex = KernelWorker.GetSelectedWeaponsData.AttackType;
                numericUpDownWeaponsAttackPower.Value = KernelWorker.GetSelectedWeaponsData.AttackPower;
                numericUpDownWeaponsAttackParam.Value = KernelWorker.GetSelectedWeaponsData.AttackParam;
                numericUpDownWeaponsSTRBonus.Value = KernelWorker.GetSelectedWeaponsData.STRBonus;
                numericUpDownWeaponsTier.Value = KernelWorker.GetSelectedWeaponsData.Tier;
                numericUpDownWeaponsCrit.Value = KernelWorker.GetSelectedWeaponsData.CritBonus;
                numericUpDownWeaponsMelee.Value = KernelWorker.GetSelectedWeaponsData.Melee;
            }

            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }
            _loaded = true;
        }

        #endregion

        #region Characters

        private void listBoxCharacters_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxCharacters.SelectedIndex == 0 || listBoxCharacters.SelectedIndex == 4)
            {
                labelCharName.Text = "The name is in FF8.exe";
                labelCharName.Location = new Point(102, 26);
                textBoxCharName.Enabled = false;
            }
            else
            {
                labelCharName.Text = "Name";
                labelCharName.Location = new Point(149, 26);
                textBoxCharName.Enabled = true;
            }

            _loaded = false;
            if (KernelWorker.Kernel == null || KernelWorker.BackupKernel == null)
                return;

            KernelWorker.ReadCharacters(listBoxCharacters.SelectedIndex, KernelWorker.BackupKernel);
            try
            {
                toolTip1.SetToolTip(textBoxCharName, $"Default: {KernelWorker.GetSelectedCharactersData.OffsetToName}");
                toolTip1.SetToolTip(numericUpDownCharCrisisLevelHP, $"Default: {KernelWorker.GetSelectedCharactersData.CrisisLevel}");
                toolTip1.SetToolTip(comboBoxCharGender, $"Default: {comboBoxCharGender.Items[KernelWorker.GetSelectedCharactersData.Gender]}");
                toolTip1.SetToolTip(numericUpDownCharLimitID, $"Default: {KernelWorker.GetSelectedCharactersData.LimitID}");
                toolTip1.SetToolTip(numericUpDownCharLimitParam, $"Default: {KernelWorker.GetSelectedCharactersData.LimitParam}");
                toolTip1.SetToolTip(numericUpDownCharEXP1, $"Default: {KernelWorker.GetSelectedCharactersData.EXP1}");
                toolTip1.SetToolTip(numericUpDownCharEXP2, $"Default: {KernelWorker.GetSelectedCharactersData.EXP2}");
                toolTip1.SetToolTip(numericUpDownCharHP1, $"Default: {KernelWorker.GetSelectedCharactersData.HP1}");
                toolTip1.SetToolTip(numericUpDownCharHP2, $"Default: {KernelWorker.GetSelectedCharactersData.HP2}");
                toolTip1.SetToolTip(numericUpDownCharHP3, $"Default: {KernelWorker.GetSelectedCharactersData.HP3}");
                toolTip1.SetToolTip(numericUpDownCharHP4, $"Default: {KernelWorker.GetSelectedCharactersData.HP4}");
                toolTip1.SetToolTip(numericUpDownCharSTR1, $"Default: {KernelWorker.GetSelectedCharactersData.STR1}");
                toolTip1.SetToolTip(numericUpDownCharSTR2, $"Default: {KernelWorker.GetSelectedCharactersData.STR2}");
                toolTip1.SetToolTip(numericUpDownCharSTR3, $"Default: {KernelWorker.GetSelectedCharactersData.STR3}");
                toolTip1.SetToolTip(numericUpDownCharSTR4, $"Default: {KernelWorker.GetSelectedCharactersData.STR4}");
                toolTip1.SetToolTip(numericUpDownCharVIT1, $"Default: {KernelWorker.GetSelectedCharactersData.VIT1}");
                toolTip1.SetToolTip(numericUpDownCharVIT2, $"Default: {KernelWorker.GetSelectedCharactersData.VIT2}");
                toolTip1.SetToolTip(numericUpDownCharVIT3, $"Default: {KernelWorker.GetSelectedCharactersData.VIT3}");
                toolTip1.SetToolTip(numericUpDownCharVIT4, $"Default: {KernelWorker.GetSelectedCharactersData.VIT4}");
                toolTip1.SetToolTip(numericUpDownCharMAG1, $"Default: {KernelWorker.GetSelectedCharactersData.MAG1}");
                toolTip1.SetToolTip(numericUpDownCharMAG2, $"Default: {KernelWorker.GetSelectedCharactersData.MAG2}");
                toolTip1.SetToolTip(numericUpDownCharMAG3, $"Default: {KernelWorker.GetSelectedCharactersData.MAG3}");
                toolTip1.SetToolTip(numericUpDownCharMAG4, $"Default: {KernelWorker.GetSelectedCharactersData.MAG4}");
                toolTip1.SetToolTip(numericUpDownCharSPR1, $"Default: {KernelWorker.GetSelectedCharactersData.SPR1}");
                toolTip1.SetToolTip(numericUpDownCharSPR2, $"Default: {KernelWorker.GetSelectedCharactersData.SPR2}");
                toolTip1.SetToolTip(numericUpDownCharSPR3, $"Default: {KernelWorker.GetSelectedCharactersData.SPR3}");
                toolTip1.SetToolTip(numericUpDownCharSPR4, $"Default: {KernelWorker.GetSelectedCharactersData.SPR4}");
                toolTip1.SetToolTip(numericUpDownCharSPD1, $"Default: {KernelWorker.GetSelectedCharactersData.SPD1}");
                toolTip1.SetToolTip(numericUpDownCharSPD2, $"Default: {KernelWorker.GetSelectedCharactersData.SPD2}");
                toolTip1.SetToolTip(numericUpDownCharSPD3, $"Default: {KernelWorker.GetSelectedCharactersData.SPD3}");
                toolTip1.SetToolTip(numericUpDownCharSPD4, $"Default: {KernelWorker.GetSelectedCharactersData.SPD4}");
                toolTip1.SetToolTip(numericUpDownCharLUCK1, $"Default: {KernelWorker.GetSelectedCharactersData.LUCK1}");
                toolTip1.SetToolTip(numericUpDownCharLUCK2, $"Default: {KernelWorker.GetSelectedCharactersData.LUCK2}");
                toolTip1.SetToolTip(numericUpDownCharLUCK3, $"Default: {KernelWorker.GetSelectedCharactersData.LUCK3}");
                toolTip1.SetToolTip(numericUpDownCharLUCK4, $"Default: {KernelWorker.GetSelectedCharactersData.LUCK4}");
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }

            KernelWorker.ReadCharacters(listBoxCharacters.SelectedIndex, KernelWorker.Kernel);
            try
            {
                textBoxCharName.Text = KernelWorker.GetSelectedCharactersData.OffsetToName;
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
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }

            _loaded = true;            
        }

        #endregion

        #region Enemy attacks

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

        private void EnemyAttacksStatusWorker(bool bBackup)
        {
            if (!bBackup)
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
                return;
            }
            ToolTip(checkBoxEnemyAttacksSleep,1, (KernelWorker.GetSelectedEnemyAttacksData.Status2 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxEnemyAttacksHaste,1, (KernelWorker.GetSelectedEnemyAttacksData.Status2 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxEnemyAttacksSlow,1, (KernelWorker.GetSelectedEnemyAttacksData.Status2 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxEnemyAttacksStop,1, (KernelWorker.GetSelectedEnemyAttacksData.Status2 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxEnemyAttacksRegen,1, (KernelWorker.GetSelectedEnemyAttacksData.Status2 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxEnemyAttacksProtect,1, (KernelWorker.GetSelectedEnemyAttacksData.Status2 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxEnemyAttacksShell,1, (KernelWorker.GetSelectedEnemyAttacksData.Status2 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxEnemyAttacksReflect,1, (KernelWorker.GetSelectedEnemyAttacksData.Status2 & 0x80) >= 1 ? true : false);

            ToolTip(checkBoxEnemyAttacksAura,1, (KernelWorker.GetSelectedEnemyAttacksData.Status3 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxEnemyAttacksCurse,1, (KernelWorker.GetSelectedEnemyAttacksData.Status3 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxEnemyAttacksDoom,1, (KernelWorker.GetSelectedEnemyAttacksData.Status3 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxEnemyAttacksInvincible,1, (KernelWorker.GetSelectedEnemyAttacksData.Status3 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxEnemyAttacksPetrifying,1, (KernelWorker.GetSelectedEnemyAttacksData.Status3 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxEnemyAttacksFloat,1, (KernelWorker.GetSelectedEnemyAttacksData.Status3 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxEnemyAttacksConfusion,1, (KernelWorker.GetSelectedEnemyAttacksData.Status3 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxEnemyAttacksDrain,1, (KernelWorker.GetSelectedEnemyAttacksData.Status3 & 0x80) >= 1 ? true : false);

            ToolTip(checkBoxEnemyAttacksEject,1, (KernelWorker.GetSelectedEnemyAttacksData.Status4 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxEnemyAttacksDouble,1, (KernelWorker.GetSelectedEnemyAttacksData.Status4 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxEnemyAttacksTriple,1, (KernelWorker.GetSelectedEnemyAttacksData.Status4 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxEnemyAttacksDefend,1, (KernelWorker.GetSelectedEnemyAttacksData.Status4 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxEnemyAttacksUnk1,1, (KernelWorker.GetSelectedEnemyAttacksData.Status4 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxEnemyAttacksUnk2,1, (KernelWorker.GetSelectedEnemyAttacksData.Status4 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxEnemyAttacksCharged,1, (KernelWorker.GetSelectedEnemyAttacksData.Status4 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxEnemyAttacksBackAttack,1, (KernelWorker.GetSelectedEnemyAttacksData.Status4 & 0x80) >= 1 ? true : false);

            ToolTip(checkBoxEnemyAttacksVit0,1, (KernelWorker.GetSelectedEnemyAttacksData.Status5 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxEnemyAttacksAngelWing,1, (KernelWorker.GetSelectedEnemyAttacksData.Status5 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxEnemyAttacksUnk3,1, (KernelWorker.GetSelectedEnemyAttacksData.Status5 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxEnemyAttacksUnk4,1, (KernelWorker.GetSelectedEnemyAttacksData.Status5 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxEnemyAttacksUnk5,1, (KernelWorker.GetSelectedEnemyAttacksData.Status5 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxEnemyAttacksUnk6,1, (KernelWorker.GetSelectedEnemyAttacksData.Status5 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxEnemyAttacksHasMagic,1, (KernelWorker.GetSelectedEnemyAttacksData.Status5 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxEnemyAttacksSummonGF,1, (KernelWorker.GetSelectedEnemyAttacksData.Status5 & 0x80) >= 1 ? true : false);

            ToolTip(checkBoxEnemyAttacksDeath,1, (KernelWorker.GetSelectedEnemyAttacksData.Status1 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxEnemyAttacksPoison,1, (KernelWorker.GetSelectedEnemyAttacksData.Status1 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxEnemyAttacksPetrify,1, (KernelWorker.GetSelectedEnemyAttacksData.Status1 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxEnemyAttacksDarkness,1, (KernelWorker.GetSelectedEnemyAttacksData.Status1 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxEnemyAttacksSilence,1, (KernelWorker.GetSelectedEnemyAttacksData.Status1 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxEnemyAttacksBerserk,1, (KernelWorker.GetSelectedEnemyAttacksData.Status1 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxEnemyAttacksZombie,1, (KernelWorker.GetSelectedEnemyAttacksData.Status1 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxEnemyAttacksUnk7,1, (KernelWorker.GetSelectedEnemyAttacksData.Status1 & 0x80) >= 1 ? true : false);
        }


        private void listBoxEnemyAttacks_SelectedIndexChanged(object sender, EventArgs e)
        {
            _loaded = false;
            if (KernelWorker.Kernel == null || KernelWorker.BackupKernel == null)
                return;

            KernelWorker.ReadEnemyAttacks(listBoxEnemyAttacks.SelectedIndex, KernelWorker.BackupKernel);
            try
            {
                ToolTip(comboBoxEnemyAttacksMagicID,2, comboBoxEnemyAttacksMagicID.Items[KernelWorker.GetSelectedEnemyAttacksData.MagicID]);
                ToolTip(comboBoxEnemyAttacksAttackType,2, comboBoxEnemyAttacksAttackType.Items[KernelWorker.GetSelectedEnemyAttacksData.AttackType]);
                ToolTip(numericUpDownEnemyAttacksCamera, 0, KernelWorker.GetSelectedEnemyAttacksData.CameraChange & 0x7F);
                ToolTip(checkBoxEnemyAttacksCamera, 1, ((KernelWorker.GetSelectedEnemyAttacksData.CameraChange >> 7) & 1) == 1);
                ToolTip(numericUpDownEnemyAttacksAttackPower,0, KernelWorker.GetSelectedEnemyAttacksData.AttackPower);
                ToolTip(checkBoxEnemyAttacksFlagShelled,1, (KernelWorker.GetSelectedEnemyAttacksData.AttackFlags & 0x01) >= 1 ? true : false);
                ToolTip(checkBoxEnemyAttacksFlag2,1, (KernelWorker.GetSelectedEnemyAttacksData.AttackFlags & 0x02) >= 1 ? true : false);
                ToolTip(checkBoxEnemyAttacksFlag3,1, (KernelWorker.GetSelectedEnemyAttacksData.AttackFlags & 0x04) >= 1 ? true : false);
                ToolTip(checkBoxEnemyAttacksFlagBreakDamageLimit,1, (KernelWorker.GetSelectedEnemyAttacksData.AttackFlags & 0x08) >= 1 ? true : false);
                ToolTip(checkBoxEnemyAttacksFlagReflected,1, (KernelWorker.GetSelectedEnemyAttacksData.AttackFlags & 0x10) >= 1 ? true : false);
                ToolTip(checkBoxEnemyAttacksFlag6,1, (KernelWorker.GetSelectedEnemyAttacksData.AttackFlags & 0x20) >= 1 ? true : false);
                ToolTip(checkBoxEnemyAttacksFlag7,1, (KernelWorker.GetSelectedEnemyAttacksData.AttackFlags & 0x40) >= 1 ? true : false);
                ToolTip(checkBoxEnemyAttacksFlag8,1, (KernelWorker.GetSelectedEnemyAttacksData.AttackFlags & 0x80) >= 1 ? true : false);
                ToolTip(comboBoxEnemyAttacksElement,2, comboBoxEnemyAttacksElement.Items[EnemyAttacks_GetElement()]);
                ToolTip(numericUpDownEnemyAttacksStatusAttack,0, KernelWorker.GetSelectedEnemyAttacksData.StatusAttack);
                ToolTip(numericUpDownEnemyAttacksAttackParam, 0, KernelWorker.GetSelectedEnemyAttacksData.AttackParam);
                EnemyAttacksStatusWorker(true);
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }

            KernelWorker.ReadEnemyAttacks(listBoxEnemyAttacks.SelectedIndex, KernelWorker.Kernel);
            try
            {
                comboBoxEnemyAttacksMagicID.SelectedIndex = KernelWorker.GetSelectedEnemyAttacksData.MagicID;
                numericUpDownEnemyAttacksCamera.Value = KernelWorker.GetSelectedEnemyAttacksData.CameraChange & 0x7F;
                checkBoxEnemyAttacksCamera.Checked = ((KernelWorker.GetSelectedEnemyAttacksData.CameraChange >> 7) & 1) == 1;
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
                numericUpDownEnemyAttacksAttackParam.Value = KernelWorker.GetSelectedEnemyAttacksData.AttackParam;
                EnemyAttacksStatusWorker(false);
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }
            _loaded = true;
        }

        #endregion

        #region Blue magic + params

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

        private void BlueMagicParamStatusWorker(bool bBackup)
        {
            if (!bBackup)
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
                return;
            }
            ToolTip(checkBoxBlueMagicCL1Sleep,1, (KernelWorker.GetSelectedBlueMagicParamData.Status1CL1 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL1Haste,1, (KernelWorker.GetSelectedBlueMagicParamData.Status1CL1 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL1Slow,1, (KernelWorker.GetSelectedBlueMagicParamData.Status1CL1 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL1Stop,1, (KernelWorker.GetSelectedBlueMagicParamData.Status1CL1 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL1Regen,1, (KernelWorker.GetSelectedBlueMagicParamData.Status1CL1 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL1Protect,1, (KernelWorker.GetSelectedBlueMagicParamData.Status1CL1 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL1Shell,1, (KernelWorker.GetSelectedBlueMagicParamData.Status1CL1 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL1Reflect,1, (KernelWorker.GetSelectedBlueMagicParamData.Status1CL1 & 0x80) >= 1 ? true : false);

            ToolTip(checkBoxBlueMagicCL1Aura,1, (KernelWorker.GetSelectedBlueMagicParamData.Status2CL1 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL1Curse,1, (KernelWorker.GetSelectedBlueMagicParamData.Status2CL1 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL1Doom,1, (KernelWorker.GetSelectedBlueMagicParamData.Status2CL1 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL1Invincible,1, (KernelWorker.GetSelectedBlueMagicParamData.Status2CL1 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL1Petrifying,1, (KernelWorker.GetSelectedBlueMagicParamData.Status2CL1 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL1Float,1, (KernelWorker.GetSelectedBlueMagicParamData.Status2CL1 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL1Confusion,1, (KernelWorker.GetSelectedBlueMagicParamData.Status2CL1 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL1Drain,1, (KernelWorker.GetSelectedBlueMagicParamData.Status2CL1 & 0x80) >= 1 ? true : false);

            ToolTip(checkBoxBlueMagicCL1Eject,1, (KernelWorker.GetSelectedBlueMagicParamData.Status3CL1 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL1Double,1, (KernelWorker.GetSelectedBlueMagicParamData.Status3CL1 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL1Triple,1, (KernelWorker.GetSelectedBlueMagicParamData.Status3CL1 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL1Defend,1, (KernelWorker.GetSelectedBlueMagicParamData.Status3CL1 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL1Unk1,1, (KernelWorker.GetSelectedBlueMagicParamData.Status3CL1 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL1Unk2,1, (KernelWorker.GetSelectedBlueMagicParamData.Status3CL1 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL1Charged,1, (KernelWorker.GetSelectedBlueMagicParamData.Status3CL1 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL1BackAttack,1, (KernelWorker.GetSelectedBlueMagicParamData.Status3CL1 & 0x80) >= 1 ? true : false);

            ToolTip(checkBoxBlueMagicCL1Vit0,1, (KernelWorker.GetSelectedBlueMagicParamData.Status4CL1 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL1AngelWing,1, (KernelWorker.GetSelectedBlueMagicParamData.Status4CL1 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL1Unk3,1, (KernelWorker.GetSelectedBlueMagicParamData.Status4CL1 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL1Unk4,1, (KernelWorker.GetSelectedBlueMagicParamData.Status4CL1 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL1Unk5,1, (KernelWorker.GetSelectedBlueMagicParamData.Status4CL1 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL1Unk6,1, (KernelWorker.GetSelectedBlueMagicParamData.Status4CL1 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL1HasMagic,1, (KernelWorker.GetSelectedBlueMagicParamData.Status4CL1 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL1SummonGF,1, (KernelWorker.GetSelectedBlueMagicParamData.Status4CL1 & 0x80) >= 1 ? true : false);

            ToolTip(checkBoxBlueMagicCL1Death,1, (KernelWorker.GetSelectedBlueMagicParamData.Status5CL1 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL1Poison,1, (KernelWorker.GetSelectedBlueMagicParamData.Status5CL1 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL1Petrify,1, (KernelWorker.GetSelectedBlueMagicParamData.Status5CL1 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL1Darkness,1, (KernelWorker.GetSelectedBlueMagicParamData.Status5CL1 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL1Silence,1, (KernelWorker.GetSelectedBlueMagicParamData.Status5CL1 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL1Berserk,1, (KernelWorker.GetSelectedBlueMagicParamData.Status5CL1 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL1Zombie,1, (KernelWorker.GetSelectedBlueMagicParamData.Status5CL1 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL1Unk7,1, (KernelWorker.GetSelectedBlueMagicParamData.Status5CL1 & 0x80) >= 1 ? true : false);


            ToolTip(checkBoxBlueMagicCL2Sleep,1, (KernelWorker.GetSelectedBlueMagicParamData.Status1CL2 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL2Haste,1, (KernelWorker.GetSelectedBlueMagicParamData.Status1CL2 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL2Slow,1, (KernelWorker.GetSelectedBlueMagicParamData.Status1CL2 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL2Stop,1, (KernelWorker.GetSelectedBlueMagicParamData.Status1CL2 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL2Regen,1, (KernelWorker.GetSelectedBlueMagicParamData.Status1CL2 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL2Protect,1, (KernelWorker.GetSelectedBlueMagicParamData.Status1CL2 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL2Shell,1, (KernelWorker.GetSelectedBlueMagicParamData.Status1CL2 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL2Reflect,1, (KernelWorker.GetSelectedBlueMagicParamData.Status1CL2 & 0x80) >= 1 ? true : false);

            ToolTip(checkBoxBlueMagicCL2Aura,1, (KernelWorker.GetSelectedBlueMagicParamData.Status2CL2 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL2Curse,1, (KernelWorker.GetSelectedBlueMagicParamData.Status2CL2 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL2Doom,1, (KernelWorker.GetSelectedBlueMagicParamData.Status2CL2 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL2Invincible,1, (KernelWorker.GetSelectedBlueMagicParamData.Status2CL2 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL2Petrifying,1, (KernelWorker.GetSelectedBlueMagicParamData.Status2CL2 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL2Float,1, (KernelWorker.GetSelectedBlueMagicParamData.Status2CL2 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL2Confusion,1, (KernelWorker.GetSelectedBlueMagicParamData.Status2CL2 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL2Drain,1, (KernelWorker.GetSelectedBlueMagicParamData.Status2CL2 & 0x80) >= 1 ? true : false);

            ToolTip(checkBoxBlueMagicCL2Eject,1, (KernelWorker.GetSelectedBlueMagicParamData.Status3CL2 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL2Double,1, (KernelWorker.GetSelectedBlueMagicParamData.Status3CL2 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL2Triple,1, (KernelWorker.GetSelectedBlueMagicParamData.Status3CL2 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL2Defend,1, (KernelWorker.GetSelectedBlueMagicParamData.Status3CL2 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL2Unk1,1, (KernelWorker.GetSelectedBlueMagicParamData.Status3CL2 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL2Unk2,1, (KernelWorker.GetSelectedBlueMagicParamData.Status3CL2 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL2Charged,1, (KernelWorker.GetSelectedBlueMagicParamData.Status3CL2 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL2BackAttack,1, (KernelWorker.GetSelectedBlueMagicParamData.Status3CL2 & 0x80) >= 1 ? true : false);

            ToolTip(checkBoxBlueMagicCL2Vit0,1, (KernelWorker.GetSelectedBlueMagicParamData.Status4CL2 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL2AngelWing,1, (KernelWorker.GetSelectedBlueMagicParamData.Status4CL2 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL2Unk3,1, (KernelWorker.GetSelectedBlueMagicParamData.Status4CL2 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL2Unk4,1, (KernelWorker.GetSelectedBlueMagicParamData.Status4CL2 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL2Unk5,1, (KernelWorker.GetSelectedBlueMagicParamData.Status4CL2 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL2Unk6,1, (KernelWorker.GetSelectedBlueMagicParamData.Status4CL2 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL2HasMagic,1, (KernelWorker.GetSelectedBlueMagicParamData.Status4CL2 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL2SummonGF,1, (KernelWorker.GetSelectedBlueMagicParamData.Status4CL2 & 0x80) >= 1 ? true : false);

            ToolTip(checkBoxBlueMagicCL2Death,1, (KernelWorker.GetSelectedBlueMagicParamData.Status5CL2 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL2Poison,1, (KernelWorker.GetSelectedBlueMagicParamData.Status5CL2 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL2Petrify,1, (KernelWorker.GetSelectedBlueMagicParamData.Status5CL2 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL2Darkness,1, (KernelWorker.GetSelectedBlueMagicParamData.Status5CL2 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL2Silence,1, (KernelWorker.GetSelectedBlueMagicParamData.Status5CL2 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL2Berserk,1, (KernelWorker.GetSelectedBlueMagicParamData.Status5CL2 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL2Zombie,1, (KernelWorker.GetSelectedBlueMagicParamData.Status5CL2 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL2Unk7,1, (KernelWorker.GetSelectedBlueMagicParamData.Status5CL2 & 0x80) >= 1 ? true : false);


            ToolTip(checkBoxBlueMagicCL3Sleep,1, (KernelWorker.GetSelectedBlueMagicParamData.Status1CL3 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL3Haste,1, (KernelWorker.GetSelectedBlueMagicParamData.Status1CL3 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL3Slow,1, (KernelWorker.GetSelectedBlueMagicParamData.Status1CL3 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL3Stop,1, (KernelWorker.GetSelectedBlueMagicParamData.Status1CL3 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL3Regen,1, (KernelWorker.GetSelectedBlueMagicParamData.Status1CL3 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL3Protect,1, (KernelWorker.GetSelectedBlueMagicParamData.Status1CL3 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL3Shell,1, (KernelWorker.GetSelectedBlueMagicParamData.Status1CL3 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL3Reflect,1, (KernelWorker.GetSelectedBlueMagicParamData.Status1CL3 & 0x80) >= 1 ? true : false);

            ToolTip(checkBoxBlueMagicCL3Aura,1, (KernelWorker.GetSelectedBlueMagicParamData.Status2CL3 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL3Curse,1, (KernelWorker.GetSelectedBlueMagicParamData.Status2CL3 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL3Doom,1, (KernelWorker.GetSelectedBlueMagicParamData.Status2CL3 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL3Invincible,1, (KernelWorker.GetSelectedBlueMagicParamData.Status2CL3 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL3Petrifying,1, (KernelWorker.GetSelectedBlueMagicParamData.Status2CL3 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL3Float,1, (KernelWorker.GetSelectedBlueMagicParamData.Status2CL3 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL3Confusion,1, (KernelWorker.GetSelectedBlueMagicParamData.Status2CL3 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL3Drain,1, (KernelWorker.GetSelectedBlueMagicParamData.Status2CL3 & 0x80) >= 1 ? true : false);

            ToolTip(checkBoxBlueMagicCL3Eject,1, (KernelWorker.GetSelectedBlueMagicParamData.Status3CL3 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL3Double,1, (KernelWorker.GetSelectedBlueMagicParamData.Status3CL3 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL3Triple,1, (KernelWorker.GetSelectedBlueMagicParamData.Status3CL3 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL3Defend,1, (KernelWorker.GetSelectedBlueMagicParamData.Status3CL3 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL3Unk1,1, (KernelWorker.GetSelectedBlueMagicParamData.Status3CL3 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL3Unk2,1, (KernelWorker.GetSelectedBlueMagicParamData.Status3CL3 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL3Charged,1, (KernelWorker.GetSelectedBlueMagicParamData.Status3CL3 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL3BackAttack,1, (KernelWorker.GetSelectedBlueMagicParamData.Status3CL3 & 0x80) >= 1 ? true : false);

            ToolTip(checkBoxBlueMagicCL3Vit0,1, (KernelWorker.GetSelectedBlueMagicParamData.Status4CL3 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL3AngelWing,1, (KernelWorker.GetSelectedBlueMagicParamData.Status4CL3 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL3Unk3,1, (KernelWorker.GetSelectedBlueMagicParamData.Status4CL3 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL3Unk4,1, (KernelWorker.GetSelectedBlueMagicParamData.Status4CL3 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL3Unk5,1, (KernelWorker.GetSelectedBlueMagicParamData.Status4CL3 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL3Unk6,1, (KernelWorker.GetSelectedBlueMagicParamData.Status4CL3 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL3HasMagic,1, (KernelWorker.GetSelectedBlueMagicParamData.Status4CL3 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL3SummonGF,1, (KernelWorker.GetSelectedBlueMagicParamData.Status4CL3 & 0x80) >= 1 ? true : false);

            ToolTip(checkBoxBlueMagicCL3Death,1, (KernelWorker.GetSelectedBlueMagicParamData.Status5CL3 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL3Poison,1, (KernelWorker.GetSelectedBlueMagicParamData.Status5CL3 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL3Petrify,1, (KernelWorker.GetSelectedBlueMagicParamData.Status5CL3 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL3Darkness,1, (KernelWorker.GetSelectedBlueMagicParamData.Status5CL3 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL3Silence,1, (KernelWorker.GetSelectedBlueMagicParamData.Status5CL3 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL3Berserk,1, (KernelWorker.GetSelectedBlueMagicParamData.Status5CL3 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL3Zombie,1, (KernelWorker.GetSelectedBlueMagicParamData.Status5CL3 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL3Unk7,1, (KernelWorker.GetSelectedBlueMagicParamData.Status5CL3 & 0x80) >= 1 ? true : false);


            ToolTip(checkBoxBlueMagicCL4Sleep,1, (KernelWorker.GetSelectedBlueMagicParamData.Status1CL4 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL4Haste,1, (KernelWorker.GetSelectedBlueMagicParamData.Status1CL4 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL4Slow,1, (KernelWorker.GetSelectedBlueMagicParamData.Status1CL4 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL4Stop,1, (KernelWorker.GetSelectedBlueMagicParamData.Status1CL4 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL4Regen,1, (KernelWorker.GetSelectedBlueMagicParamData.Status1CL4 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL4Protect,1, (KernelWorker.GetSelectedBlueMagicParamData.Status1CL4 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL4Shell,1, (KernelWorker.GetSelectedBlueMagicParamData.Status1CL4 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL4Reflect,1, (KernelWorker.GetSelectedBlueMagicParamData.Status1CL4 & 0x80) >= 1 ? true : false);

            ToolTip(checkBoxBlueMagicCL4Aura,1, (KernelWorker.GetSelectedBlueMagicParamData.Status2CL4 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL4Curse,1, (KernelWorker.GetSelectedBlueMagicParamData.Status2CL4 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL4Doom,1, (KernelWorker.GetSelectedBlueMagicParamData.Status2CL4 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL4Invincible,1, (KernelWorker.GetSelectedBlueMagicParamData.Status2CL4 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL4Petrifying,1, (KernelWorker.GetSelectedBlueMagicParamData.Status2CL4 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL4Float,1, (KernelWorker.GetSelectedBlueMagicParamData.Status2CL4 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL4Confusion,1, (KernelWorker.GetSelectedBlueMagicParamData.Status2CL4 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL4Drain,1, (KernelWorker.GetSelectedBlueMagicParamData.Status2CL4 & 0x80) >= 1 ? true : false);

            ToolTip(checkBoxBlueMagicCL4Eject,1, (KernelWorker.GetSelectedBlueMagicParamData.Status3CL4 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL4Double,1, (KernelWorker.GetSelectedBlueMagicParamData.Status3CL4 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL4Triple,1, (KernelWorker.GetSelectedBlueMagicParamData.Status3CL4 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL4Defend,1, (KernelWorker.GetSelectedBlueMagicParamData.Status3CL4 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL4Unk1,1, (KernelWorker.GetSelectedBlueMagicParamData.Status3CL4 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL4Unk2,1, (KernelWorker.GetSelectedBlueMagicParamData.Status3CL4 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL4Charged,1, (KernelWorker.GetSelectedBlueMagicParamData.Status3CL4 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL4BackAttack,1, (KernelWorker.GetSelectedBlueMagicParamData.Status3CL4 & 0x80) >= 1 ? true : false);

            ToolTip(checkBoxBlueMagicCL4Vit0,1, (KernelWorker.GetSelectedBlueMagicParamData.Status4CL4 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL4AngelWing,1, (KernelWorker.GetSelectedBlueMagicParamData.Status4CL4 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL4Unk3,1, (KernelWorker.GetSelectedBlueMagicParamData.Status4CL4 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL4Unk4,1, (KernelWorker.GetSelectedBlueMagicParamData.Status4CL4 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL4Unk5,1, (KernelWorker.GetSelectedBlueMagicParamData.Status4CL4 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL4Unk6,1, (KernelWorker.GetSelectedBlueMagicParamData.Status4CL4 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL4HasMagic,1, (KernelWorker.GetSelectedBlueMagicParamData.Status4CL4 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL4SummonGF,1, (KernelWorker.GetSelectedBlueMagicParamData.Status4CL4 & 0x80) >= 1 ? true : false);

            ToolTip(checkBoxBlueMagicCL4Death,1, (KernelWorker.GetSelectedBlueMagicParamData.Status5CL4 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL4Poison,1, (KernelWorker.GetSelectedBlueMagicParamData.Status5CL4 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL4Petrify,1, (KernelWorker.GetSelectedBlueMagicParamData.Status5CL4 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL4Darkness,1, (KernelWorker.GetSelectedBlueMagicParamData.Status5CL4 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL4Silence,1, (KernelWorker.GetSelectedBlueMagicParamData.Status5CL4 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL4Berserk,1, (KernelWorker.GetSelectedBlueMagicParamData.Status5CL4 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL4Zombie,1, (KernelWorker.GetSelectedBlueMagicParamData.Status5CL4 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxBlueMagicCL4Unk7,1, (KernelWorker.GetSelectedBlueMagicParamData.Status5CL4 & 0x80) >= 1 ? true : false);
        }

        private void listBoxBlueMagic_SelectedIndexChanged(object sender, EventArgs e)
        {
            _loaded = false;
            if (KernelWorker.Kernel == null || KernelWorker.BackupKernel == null)
                return;

            KernelWorker.ReadBlueMagic(listBoxBlueMagic.SelectedIndex, KernelWorker.BackupKernel);
            KernelWorker.ReadBlueMagicParam(listBoxBlueMagic.SelectedIndex, KernelWorker.BackupKernel);
            try
            {
                ToolTip(textBoxBlueMagicName, 2, KernelWorker.GetSelectedBlueMagicData.OffsetToName);
                ToolTip(textBoxBlueMagicDes, 2, KernelWorker.GetSelectedBlueMagicData.OffsetToDescription);
                ToolTip(comboBoxBlueMagicMagicID,2, comboBoxBlueMagicMagicID.Items[KernelWorker.GetSelectedBlueMagicData.MagicID]);
                ToolTip(comboBoxBlueMagicAttackType,2, comboBoxBlueMagicAttackType.Items[KernelWorker.GetSelectedBlueMagicData.AttackType]);
                ToolTip(checkBoxBlueMagicFlag1,1, (KernelWorker.GetSelectedBlueMagicData.AttackFlags & 0x01) >= 1 ? true : false);
                ToolTip(checkBoxBlueMagicFlag2,1, (KernelWorker.GetSelectedBlueMagicData.AttackFlags & 0x02) >= 1 ? true : false);
                ToolTip(checkBoxBlueMagicFlag3,1, (KernelWorker.GetSelectedBlueMagicData.AttackFlags & 0x04) >= 1 ? true : false);
                ToolTip(checkBoxBlueMagicFlag4,1, (KernelWorker.GetSelectedBlueMagicData.AttackFlags & 0x08) >= 1 ? true : false);
                ToolTip(checkBoxBlueMagicFlag5,1, (KernelWorker.GetSelectedBlueMagicData.AttackFlags & 0x10) >= 1 ? true : false);
                ToolTip(checkBoxBlueMagicFlag6,1, (KernelWorker.GetSelectedBlueMagicData.AttackFlags & 0x20) >= 1 ? true : false);
                ToolTip(checkBoxBlueMagicFlag7,1, (KernelWorker.GetSelectedBlueMagicData.AttackFlags & 0x40) >= 1 ? true : false);
                ToolTip(checkBoxBlueMagicFlag8,1, (KernelWorker.GetSelectedBlueMagicData.AttackFlags & 0x80) >= 1 ? true : false);
                ToolTip(comboBoxBlueMagicElement,2, comboBoxBlueMagicElement.Items[BlueMagic_GetElement()]);
                ToolTip(numericUpDownBlueMagicStatusAttack,0, KernelWorker.GetSelectedBlueMagicData.StatusAttack);
                ToolTip(numericUpDownBlueMagicCrit, 0, KernelWorker.GetSelectedBlueMagicData.CritBonus);

                BlueMagicParamStatusWorker(true);

                ToolTip(numericUpDownBlueMagicCL1AttackPower,0, KernelWorker.GetSelectedBlueMagicParamData.AttackPowerCL1);
                ToolTip(numericUpDownBlueMagicCL1AttackParam,0, KernelWorker.GetSelectedBlueMagicParamData.AttackParamCL1);

                ToolTip(numericUpDownBlueMagicCL2AttackPower,0, KernelWorker.GetSelectedBlueMagicParamData.AttackPowerCL2);
                ToolTip(numericUpDownBlueMagicCL2AttackParam,0, KernelWorker.GetSelectedBlueMagicParamData.AttackParamCL2);

                ToolTip(numericUpDownBlueMagicCL3AttackPower,0, KernelWorker.GetSelectedBlueMagicParamData.AttackPowerCL3);
                ToolTip(numericUpDownBlueMagicCL3AttackParam,0, KernelWorker.GetSelectedBlueMagicParamData.AttackParamCL3);

                ToolTip(numericUpDownBlueMagicCL4AttackPower,0, KernelWorker.GetSelectedBlueMagicParamData.AttackPowerCL4);
                ToolTip(numericUpDownBlueMagicCL4AttackParam,0, KernelWorker.GetSelectedBlueMagicParamData.AttackParamCL4);
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }

            KernelWorker.ReadBlueMagic(listBoxBlueMagic.SelectedIndex, KernelWorker.Kernel);
            KernelWorker.ReadBlueMagicParam(listBoxBlueMagic.SelectedIndex, KernelWorker.Kernel);
            try
            {
                textBoxBlueMagicName.Text = KernelWorker.GetSelectedBlueMagicData.OffsetToName;
                textBoxBlueMagicDes.Text = KernelWorker.GetSelectedBlueMagicData.OffsetToDescription;
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
                numericUpDownBlueMagicCrit.Value = KernelWorker.GetSelectedBlueMagicData.CritBonus;

                BlueMagicParamStatusWorker(false);

                numericUpDownBlueMagicCL1AttackPower.Value = KernelWorker.GetSelectedBlueMagicParamData.AttackPowerCL1;
                numericUpDownBlueMagicCL1AttackParam.Value = KernelWorker.GetSelectedBlueMagicParamData.AttackParamCL1;

                numericUpDownBlueMagicCL2AttackPower.Value = KernelWorker.GetSelectedBlueMagicParamData.AttackPowerCL2;
                numericUpDownBlueMagicCL2AttackParam.Value = KernelWorker.GetSelectedBlueMagicParamData.AttackParamCL2;

                numericUpDownBlueMagicCL3AttackPower.Value = KernelWorker.GetSelectedBlueMagicParamData.AttackPowerCL3;
                numericUpDownBlueMagicCL3AttackParam.Value = KernelWorker.GetSelectedBlueMagicParamData.AttackParamCL3;

                numericUpDownBlueMagicCL4AttackPower.Value = KernelWorker.GetSelectedBlueMagicParamData.AttackPowerCL4;
                numericUpDownBlueMagicCL4AttackParam.Value = KernelWorker.GetSelectedBlueMagicParamData.AttackParamCL4;
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }
            _loaded = true;         
        }

        #endregion

        #region Stat increment abilities

        private void listBoxAbStats_SelectedIndexChanged(object sender, EventArgs e)
        {
            _loaded = false;
            if (KernelWorker.Kernel == null || KernelWorker.BackupKernel == null)
                return;

            KernelWorker.ReadStatPercentageAbilities(listBoxAbStats.SelectedIndex, KernelWorker.BackupKernel);
            try
            {
                ToolTip(numericUpDownAbStatsAP,0x00,KernelWorker.GetSelectedStatPercentageAbilitiesData.AP);
                ToolTip(comboBoxAbStatsStatToIncrease, 0x02,comboBoxAbStatsStatToIncrease.Items[KernelWorker.GetSelectedStatPercentageAbilitiesData.StatToincrease]);
                ToolTip(trackBarAbStatsIncrementValue,0x00,KernelWorker.GetSelectedStatPercentageAbilitiesData.IncreasementValue);
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }

            KernelWorker.ReadStatPercentageAbilities(listBoxAbStats.SelectedIndex, KernelWorker.Kernel);
            try
            {
                numericUpDownAbStatsAP.Value = KernelWorker.GetSelectedStatPercentageAbilitiesData.AP;
                comboBoxAbStatsStatToIncrease.SelectedIndex = KernelWorker.GetSelectedStatPercentageAbilitiesData.StatToincrease;
                trackBarAbStatsIncrementValue.Value = KernelWorker.GetSelectedStatPercentageAbilitiesData.IncreasementValue;
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }
            _loaded = true;
        }

        #endregion

        #region Renzokuken finishers

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

        private void RenzoFinStatusWorker(bool bBackup)
        {
            if (!bBackup)
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
                return;
            }
            ToolTip(checkBoxRenzoFinDeath,1, (KernelWorker.GetSelectedRenzoFinData.Status1 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxRenzoFinPoison, 1, (KernelWorker.GetSelectedRenzoFinData.Status1 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxRenzoFinPetrify, 1, (KernelWorker.GetSelectedRenzoFinData.Status1 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxRenzoFinDarkness, 1, (KernelWorker.GetSelectedRenzoFinData.Status1 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxRenzoFinSilence, 1, (KernelWorker.GetSelectedRenzoFinData.Status1 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxRenzoFinBerserk, 1, (KernelWorker.GetSelectedRenzoFinData.Status1 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxRenzoFinZombie, 1, (KernelWorker.GetSelectedRenzoFinData.Status1 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxRenzoFinUnk7, 1, (KernelWorker.GetSelectedRenzoFinData.Status1 & 0x80) >= 1 ? true : false);

            ToolTip(checkBoxRenzoFinSleep, 1, (KernelWorker.GetSelectedRenzoFinData.Status2 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxRenzoFinHaste, 1, (KernelWorker.GetSelectedRenzoFinData.Status2 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxRenzoFinSlow, 1, (KernelWorker.GetSelectedRenzoFinData.Status2 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxRenzoFinStop, 1, (KernelWorker.GetSelectedRenzoFinData.Status2 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxRenzoFinRegen, 1, (KernelWorker.GetSelectedRenzoFinData.Status2 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxRenzoFinProtect, 1, (KernelWorker.GetSelectedRenzoFinData.Status2 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxRenzoFinShell, 1, (KernelWorker.GetSelectedRenzoFinData.Status2 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxRenzoFinReflect, 1, (KernelWorker.GetSelectedRenzoFinData.Status2 & 0x80) >= 1 ? true : false);

            ToolTip(checkBoxRenzoFinAura, 1, (KernelWorker.GetSelectedRenzoFinData.Status3 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxRenzoFinCurse, 1, (KernelWorker.GetSelectedRenzoFinData.Status3 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxRenzoFinDoom, 1, (KernelWorker.GetSelectedRenzoFinData.Status3 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxRenzoFinInvincible, 1, (KernelWorker.GetSelectedRenzoFinData.Status3 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxRenzoFinPetrifying, 1, (KernelWorker.GetSelectedRenzoFinData.Status3 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxRenzoFinFloat, 1, (KernelWorker.GetSelectedRenzoFinData.Status3 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxRenzoFinConfusion, 1, (KernelWorker.GetSelectedRenzoFinData.Status3 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxRenzoFinDrain, 1, (KernelWorker.GetSelectedRenzoFinData.Status3 & 0x80) >= 1 ? true : false);

            ToolTip(checkBoxRenzoFinEject, 1, (KernelWorker.GetSelectedRenzoFinData.Status4 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxRenzoFinDouble, 1, (KernelWorker.GetSelectedRenzoFinData.Status4 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxRenzoFinTriple, 1, (KernelWorker.GetSelectedRenzoFinData.Status4 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxRenzoFinDefend, 1, (KernelWorker.GetSelectedRenzoFinData.Status4 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxRenzoFinUnk1, 1, (KernelWorker.GetSelectedRenzoFinData.Status4 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxRenzoFinUnk2, 1, (KernelWorker.GetSelectedRenzoFinData.Status4 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxRenzoFinCharged, 1, (KernelWorker.GetSelectedRenzoFinData.Status4 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxRenzoFinBackAttack, 1, (KernelWorker.GetSelectedRenzoFinData.Status4 & 0x80) >= 1 ? true : false);

            ToolTip(checkBoxRenzoFinVit0, 1, (KernelWorker.GetSelectedRenzoFinData.Status5 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxRenzoFinAngelWing, 1, (KernelWorker.GetSelectedRenzoFinData.Status5 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxRenzoFinUnk3, 1, (KernelWorker.GetSelectedRenzoFinData.Status5 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxRenzoFinUnk4, 1, (KernelWorker.GetSelectedRenzoFinData.Status5 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxRenzoFinUnk5, 1, (KernelWorker.GetSelectedRenzoFinData.Status5 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxRenzoFinUnk6, 1, (KernelWorker.GetSelectedRenzoFinData.Status5 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxRenzoFinHasMagic, 1, (KernelWorker.GetSelectedRenzoFinData.Status5 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxRenzoFinSummonGF, 1, (KernelWorker.GetSelectedRenzoFinData.Status5 & 0x80) >= 1 ? true : false);
        }

        private void listBoxRenzoFin_SelectedIndexChanged(object sender, EventArgs e)
        {
            _loaded = false;
            if (KernelWorker.Kernel == null || KernelWorker.BackupKernel == null)
                return;

            KernelWorker.ReadRenzoFin(listBoxRenzoFin.SelectedIndex, KernelWorker.BackupKernel);
            try
            {
                ToolTip(textBoxRenzoFinName, 2, KernelWorker.GetSelectedRenzoFinData.OffsetToName);
                ToolTip(textBoxRenzoFinDes, 2, KernelWorker.GetSelectedRenzoFinData.OffsetToDescription);
                toolTip1.SetToolTip(textBoxCharName, $"Default: {KernelWorker.GetSelectedCharactersData.OffsetToName}");
                ToolTip(comboBoxRenzoFinMagicID, 2, comboBoxRenzoFinMagicID.Items[ KernelWorker.GetSelectedRenzoFinData.MagicID]);
                ToolTip(comboBoxRenzoFinAttackType, 2, comboBoxRenzoFinAttackType.Items[KernelWorker.GetSelectedRenzoFinData.AttackType]);
                ToolTip(numericUpDownRenzoFinAttackPower, 0, KernelWorker.GetSelectedRenzoFinData.AttackPower);
                ToolTip(checkBoxRenzoFinTarget1, 1, (KernelWorker.GetSelectedRenzoFinData.Target & 0x01) >= 1 ? true : false);
                ToolTip(checkBoxRenzoFinTarget2, 1, (KernelWorker.GetSelectedRenzoFinData.Target & 0x02) >= 1 ? true : false);
                ToolTip(checkBoxRenzoFinTarget3, 1, (KernelWorker.GetSelectedRenzoFinData.Target & 0x04) >= 1 ? true : false);
                ToolTip(checkBoxRenzoFinTarget4, 1, (KernelWorker.GetSelectedRenzoFinData.Target & 0x08) >= 1 ? true : false);
                ToolTip(checkBoxRenzoFinTarget5, 1, (KernelWorker.GetSelectedRenzoFinData.Target & 0x10) >= 1 ? true : false);
                ToolTip(checkBoxRenzoFinTarget6, 1, (KernelWorker.GetSelectedRenzoFinData.Target & 0x20) >= 1 ? true : false);
                ToolTip(checkBoxRenzoFinTarget7, 1, (KernelWorker.GetSelectedRenzoFinData.Target & 0x40) >= 1 ? true : false);
                ToolTip(checkBoxRenzoFinTarget8, 1, (KernelWorker.GetSelectedRenzoFinData.Target & 0x80) >= 1 ? true : false);
                ToolTip(checkBoxRenzoFinFlag1, 1, (KernelWorker.GetSelectedRenzoFinData.AttackFlags & 0x01) >= 1 ? true : false);
                ToolTip(checkBoxRenzoFinFlag2, 1, (KernelWorker.GetSelectedRenzoFinData.AttackFlags & 0x02) >= 1 ? true : false);
                ToolTip(checkBoxRenzoFinFlag3, 1, (KernelWorker.GetSelectedRenzoFinData.AttackFlags & 0x04) >= 1 ? true : false);
                ToolTip(checkBoxRenzoFinFlag4, 1, (KernelWorker.GetSelectedRenzoFinData.AttackFlags & 0x08) >= 1 ? true : false);
                ToolTip(checkBoxRenzoFinFlag5, 1, (KernelWorker.GetSelectedRenzoFinData.AttackFlags & 0x10) >= 1 ? true : false);
                ToolTip(checkBoxRenzoFinFlag6, 1, (KernelWorker.GetSelectedRenzoFinData.AttackFlags & 0x20) >= 1 ? true : false);
                ToolTip(checkBoxRenzoFinFlag7, 1, (KernelWorker.GetSelectedRenzoFinData.AttackFlags & 0x40) >= 1 ? true : false);
                ToolTip(checkBoxRenzoFinFlag8, 1, (KernelWorker.GetSelectedRenzoFinData.AttackFlags & 0x80) >= 1 ? true : false);
                ToolTip(numericUpDownRenzoFinHitCount, 0, KernelWorker.GetSelectedRenzoFinData.HitCount);
                ToolTip(comboBoxRenzoFinElement, 2, comboBoxRenzoFinElement.Items[RenzoFin_GetElement()]);
                ToolTip(numericUpDownRenzoFinElementPerc, 0, KernelWorker.GetSelectedRenzoFinData.ElementPerc);
                ToolTip(numericUpDownRenzoFinStatusAttack, 0, KernelWorker.GetSelectedRenzoFinData.StatusAttack);
                RenzoFinStatusWorker(true);
            }

            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }
            KernelWorker.ReadRenzoFin(listBoxRenzoFin.SelectedIndex, KernelWorker.Kernel);
            try
            {
                textBoxRenzoFinName.Text = KernelWorker.GetSelectedRenzoFinData.OffsetToName;
                textBoxRenzoFinDes.Text = KernelWorker.GetSelectedRenzoFinData.OffsetToDescription;
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
                RenzoFinStatusWorker(false);
            }

            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }
            _loaded = true;
        }

        #endregion

        #region Temp characters limit breaks

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

        private void TempCharLBStatusWorker(bool bBackup)
        {
            if (!bBackup)
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
                return;
            }
            else
            {
                ToolTip(checkBoxTempCharLBDeath, 0x01,(KernelWorker.GetSelectedTempCharLBData.Status1 & 0x01) >= 1 ? true : false);
                ToolTip(checkBoxTempCharLBPoison, 0x01, (KernelWorker.GetSelectedTempCharLBData.Status1 & 0x02) >= 1 ? true : false);
                ToolTip(checkBoxTempCharLBPetrify, 0x01,  (KernelWorker.GetSelectedTempCharLBData.Status1 & 0x04) >= 1 ? true : false);
                ToolTip(checkBoxTempCharLBDarkness, 0x01, (KernelWorker.GetSelectedTempCharLBData.Status1 & 0x08) >= 1 ? true : false);
                ToolTip(checkBoxTempCharLBSilence, 0x01, (KernelWorker.GetSelectedTempCharLBData.Status1 & 0x10) >= 1 ? true : false);
                ToolTip(checkBoxTempCharLBBerserk, 0x01, (KernelWorker.GetSelectedTempCharLBData.Status1 & 0x20) >= 1 ? true : false);
                ToolTip(checkBoxTempCharLBZombie, 0x01, (KernelWorker.GetSelectedTempCharLBData.Status1 & 0x40) >= 1 ? true : false);
                ToolTip(checkBoxTempCharLBUnk7, 0x01, (KernelWorker.GetSelectedTempCharLBData.Status1 & 0x80) >= 1 ? true : false);

                ToolTip(checkBoxTempCharLBSleep, 0x01, (KernelWorker.GetSelectedTempCharLBData.Status2 & 0x01) >= 1 ? true : false);
                ToolTip(checkBoxTempCharLBHaste, 0x01, (KernelWorker.GetSelectedTempCharLBData.Status2 & 0x02) >= 1 ? true : false);
                ToolTip(checkBoxTempCharLBSlow, 0x01, (KernelWorker.GetSelectedTempCharLBData.Status2 & 0x04) >= 1 ? true : false);
                ToolTip(checkBoxTempCharLBStop, 0x01, (KernelWorker.GetSelectedTempCharLBData.Status2 & 0x08) >= 1 ? true : false);
                ToolTip(checkBoxTempCharLBRegen, 0x01, (KernelWorker.GetSelectedTempCharLBData.Status2 & 0x10) >= 1 ? true : false);
                ToolTip(checkBoxTempCharLBProtect, 0x01, (KernelWorker.GetSelectedTempCharLBData.Status2 & 0x20) >= 1 ? true : false);
                ToolTip(checkBoxTempCharLBShell, 0x01, (KernelWorker.GetSelectedTempCharLBData.Status2 & 0x40) >= 1 ? true : false);
                ToolTip(checkBoxTempCharLBReflect, 0x01, (KernelWorker.GetSelectedTempCharLBData.Status2 & 0x80) >= 1 ? true : false);

                ToolTip(checkBoxTempCharLBAura, 0x01, (KernelWorker.GetSelectedTempCharLBData.Status3 & 0x01) >= 1 ? true : false);
                ToolTip(checkBoxTempCharLBCurse, 0x01, (KernelWorker.GetSelectedTempCharLBData.Status3 & 0x02) >= 1 ? true : false);
                ToolTip(checkBoxTempCharLBDoom, 0x01, (KernelWorker.GetSelectedTempCharLBData.Status3 & 0x04) >= 1 ? true : false);
                ToolTip(checkBoxTempCharLBInvincible, 0x01, (KernelWorker.GetSelectedTempCharLBData.Status3 & 0x08) >= 1 ? true : false);
                ToolTip(checkBoxTempCharLBPetrifying, 0x01, (KernelWorker.GetSelectedTempCharLBData.Status3 & 0x10) >= 1 ? true : false);
                ToolTip(checkBoxTempCharLBFloat, 0x01, (KernelWorker.GetSelectedTempCharLBData.Status3 & 0x20) >= 1 ? true : false);
                ToolTip(checkBoxTempCharLBConfusion, 0x01, (KernelWorker.GetSelectedTempCharLBData.Status3 & 0x40) >= 1 ? true : false);
                ToolTip(checkBoxTempCharLBDrain, 0x01, (KernelWorker.GetSelectedTempCharLBData.Status3 & 0x80) >= 1 ? true : false);

                ToolTip(checkBoxTempCharLBEject, 0x01, (KernelWorker.GetSelectedTempCharLBData.Status4 & 0x01) >= 1 ? true : false);
                ToolTip(checkBoxTempCharLBDouble, 0x01, (KernelWorker.GetSelectedTempCharLBData.Status4 & 0x02) >= 1 ? true : false);
                ToolTip(checkBoxTempCharLBTriple, 0x01, (KernelWorker.GetSelectedTempCharLBData.Status4 & 0x04) >= 1 ? true : false);
                ToolTip(checkBoxTempCharLBDefend, 0x01, (KernelWorker.GetSelectedTempCharLBData.Status4 & 0x08) >= 1 ? true : false);
                ToolTip(checkBoxTempCharLBUnk1, 0x01, (KernelWorker.GetSelectedTempCharLBData.Status4 & 0x10) >= 1 ? true : false);
                ToolTip(checkBoxTempCharLBUnk2, 0x01, (KernelWorker.GetSelectedTempCharLBData.Status4 & 0x20) >= 1 ? true : false);
                ToolTip(checkBoxTempCharLBCharged, 0x01, (KernelWorker.GetSelectedTempCharLBData.Status4 & 0x40) >= 1 ? true : false);
                ToolTip(checkBoxTempCharLBBackAttack, 0x01, (KernelWorker.GetSelectedTempCharLBData.Status4 & 0x80) >= 1 ? true : false);

                ToolTip(checkBoxTempCharLBVit0, 0x01, (KernelWorker.GetSelectedTempCharLBData.Status5 & 0x01) >= 1 ? true : false);
                ToolTip(checkBoxTempCharLBAngelWing, 0x01, (KernelWorker.GetSelectedTempCharLBData.Status5 & 0x02) >= 1 ? true : false);
                ToolTip(checkBoxTempCharLBUnk3, 0x01, (KernelWorker.GetSelectedTempCharLBData.Status5 & 0x04) >= 1 ? true : false);
                ToolTip(checkBoxTempCharLBUnk4, 0x01, (KernelWorker.GetSelectedTempCharLBData.Status5 & 0x08) >= 1 ? true : false);
                ToolTip(checkBoxTempCharLBUnk5, 0x01, (KernelWorker.GetSelectedTempCharLBData.Status5 & 0x10) >= 1 ? true : false);
                ToolTip(checkBoxTempCharLBUnk6, 0x01, (KernelWorker.GetSelectedTempCharLBData.Status5 & 0x20) >= 1 ? true : false);
                ToolTip(checkBoxTempCharLBHasMagic, 0x01, (KernelWorker.GetSelectedTempCharLBData.Status5 & 0x40) >= 1 ? true : false);
                ToolTip(checkBoxTempCharLBSummonGF, 0x01, (KernelWorker.GetSelectedTempCharLBData.Status5 & 0x80) >= 1 ? true : false);
            }
        }

        private void listBoxTempCharLB_SelectedIndexChanged(object sender, EventArgs e)
        {

            _loaded = false;
            if (KernelWorker.Kernel == null || KernelWorker.BackupKernel == null)
                return;

            KernelWorker.ReadTempCharLB(listBoxTempCharLB.SelectedIndex, KernelWorker.BackupKernel);
            try
            {
                toolTip1.SetToolTip(comboBoxTempCharLBMagicID, $"Default: {comboBoxTempCharLBMagicID.Items[KernelWorker.GetSelectedTempCharLBData.MagicID]}");
                toolTip1.SetToolTip(comboBoxTempCharLBAttackType, $"Default: {comboBoxTempCharLBAttackType.Items[KernelWorker.GetSelectedTempCharLBData.AttackType]}");
                toolTip1.SetToolTip(numericUpDownTempCharLBAttackPower, $"Default: {KernelWorker.GetSelectedTempCharLBData.AttackPower}");
                string check = (KernelWorker.GetSelectedTempCharLBData.Target & 0x01) >= 1 ? "Checked" : "Unchecked";
                toolTip1.SetToolTip(checkBoxTempCharLBTarget1, $"Default: {check}");
                ToolTip(checkBoxTempCharLBTarget2,_bp_checked,(KernelWorker.GetSelectedTempCharLBData.Target & 0x02) >= 1 ? true : false);
                ToolTip(checkBoxTempCharLBTarget3, _bp_checked, (KernelWorker.GetSelectedTempCharLBData.Target & 0x04) >= 1 ? true : false);
                ToolTip(checkBoxTempCharLBTarget4, _bp_checked, (KernelWorker.GetSelectedTempCharLBData.Target & 0x08) >= 1 ? true : false);
                ToolTip(checkBoxTempCharLBTarget5, _bp_checked, (KernelWorker.GetSelectedTempCharLBData.Target & 0x10) >= 1 ? true : false);
                ToolTip(checkBoxTempCharLBTarget6, _bp_checked, (KernelWorker.GetSelectedTempCharLBData.Target & 0x20) >= 1 ? true : false);
                ToolTip(checkBoxTempCharLBTarget7, _bp_checked, (KernelWorker.GetSelectedTempCharLBData.Target & 0x40) >= 1 ? true : false);
                ToolTip(checkBoxTempCharLBTarget8, _bp_checked, (KernelWorker.GetSelectedTempCharLBData.Target & 0x80) >= 1 ? true : false);
                ToolTip(checkBoxTempCharLBFlag1, _bp_checked, (KernelWorker.GetSelectedTempCharLBData.AttackFlags & 0x01) >= 1 ? true : false);
                ToolTip(checkBoxTempCharLBFlag2, _bp_checked, (KernelWorker.GetSelectedTempCharLBData.AttackFlags & 0x02) >= 1 ? true : false);
                ToolTip(checkBoxTempCharLBFlag3, _bp_checked, (KernelWorker.GetSelectedTempCharLBData.AttackFlags & 0x04) >= 1 ? true : false);
                ToolTip(checkBoxTempCharLBFlag4, _bp_checked, (KernelWorker.GetSelectedTempCharLBData.AttackFlags & 0x08) >= 1 ? true : false);
                ToolTip(checkBoxTempCharLBFlag5, _bp_checked, (KernelWorker.GetSelectedTempCharLBData.AttackFlags & 0x10) >= 1 ? true : false);
                ToolTip(checkBoxTempCharLBFlag6, _bp_checked, (KernelWorker.GetSelectedTempCharLBData.AttackFlags & 0x20) >= 1 ? true : false);
                ToolTip(checkBoxTempCharLBFlag7, _bp_checked, (KernelWorker.GetSelectedTempCharLBData.AttackFlags & 0x40) >= 1 ? true : false);
                ToolTip(checkBoxTempCharLBFlag8, _bp_checked, (KernelWorker.GetSelectedTempCharLBData.AttackFlags & 0x80) >= 1 ? true : false);
                ToolTip(numericUpDownTempCharLBHitCount, 0x00, KernelWorker.GetSelectedTempCharLBData.HitCount);
                ToolTip(comboBoxTempCharLBElement, _bp_string, comboBoxTempCharLBElement.Items[TempCharLB_GetElement()]);
                ToolTip(numericUpDownTempCharLBElementPerc, 0x00,KernelWorker.GetSelectedTempCharLBData.ElementPerc);
                ToolTip(numericUpDownTempCharLBStatusAttack, 0x00,KernelWorker.GetSelectedTempCharLBData.StatusAttack);
                TempCharLBStatusWorker(true);
            }

            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }

            KernelWorker.ReadTempCharLB(listBoxTempCharLB.SelectedIndex, KernelWorker.Kernel);
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
                TempCharLBStatusWorker(false);
            }

            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }
            _loaded = true;
        }

        #endregion

        #region Shot

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

        private void ShotStatusWorker(bool bBackup)
        {
            if (!bBackup)
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
                return;
            }
            ToolTip(checkBoxShotDeath, 01, (KernelWorker.GetSelectedShotData.Status1 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxShotPoison, 01, (KernelWorker.GetSelectedShotData.Status1 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxShotPetrify, 01, (KernelWorker.GetSelectedShotData.Status1 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxShotDarkness, 01, (KernelWorker.GetSelectedShotData.Status1 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxShotSilence, 01, (KernelWorker.GetSelectedShotData.Status1 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxShotBerserk, 01, (KernelWorker.GetSelectedShotData.Status1 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxShotZombie, 01, (KernelWorker.GetSelectedShotData.Status1 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxShotUnk7, 01, (KernelWorker.GetSelectedShotData.Status1 & 0x80) >= 1 ? true : false);

            ToolTip(checkBoxShotSleep, 01, (KernelWorker.GetSelectedShotData.Status2 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxShotHaste, 01, (KernelWorker.GetSelectedShotData.Status2 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxShotSlow, 01, (KernelWorker.GetSelectedShotData.Status2 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxShotStop, 01, (KernelWorker.GetSelectedShotData.Status2 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxShotRegen, 01, (KernelWorker.GetSelectedShotData.Status2 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxShotProtect, 01, (KernelWorker.GetSelectedShotData.Status2 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxShotShell, 01, (KernelWorker.GetSelectedShotData.Status2 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxShotReflect, 01, (KernelWorker.GetSelectedShotData.Status2 & 0x80) >= 1 ? true : false);

            ToolTip(checkBoxShotAura, 01, (KernelWorker.GetSelectedShotData.Status3 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxShotCurse, 01, (KernelWorker.GetSelectedShotData.Status3 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxShotDoom, 01, (KernelWorker.GetSelectedShotData.Status3 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxShotInvincible, 01, (KernelWorker.GetSelectedShotData.Status3 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxShotPetrifying, 01, (KernelWorker.GetSelectedShotData.Status3 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxShotFloat, 01, (KernelWorker.GetSelectedShotData.Status3 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxShotConfusion, 01, (KernelWorker.GetSelectedShotData.Status3 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxShotDrain, 01, (KernelWorker.GetSelectedShotData.Status3 & 0x80) >= 1 ? true : false);

            ToolTip(checkBoxShotEject, 01, (KernelWorker.GetSelectedShotData.Status4 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxShotDouble, 01, (KernelWorker.GetSelectedShotData.Status4 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxShotTriple, 01, (KernelWorker.GetSelectedShotData.Status4 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxShotDefend, 01, (KernelWorker.GetSelectedShotData.Status4 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxShotUnk1, 01, (KernelWorker.GetSelectedShotData.Status4 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxShotUnk2, 01, (KernelWorker.GetSelectedShotData.Status4 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxShotCharged, 01, (KernelWorker.GetSelectedShotData.Status4 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxShotBackAttack, 01, (KernelWorker.GetSelectedShotData.Status4 & 0x80) >= 1 ? true : false);

            ToolTip(checkBoxShotVit0, 01, (KernelWorker.GetSelectedShotData.Status5 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxShotAngelWing, 01, (KernelWorker.GetSelectedShotData.Status5 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxShotUnk3, 01, (KernelWorker.GetSelectedShotData.Status5 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxShotUnk4, 01, (KernelWorker.GetSelectedShotData.Status5 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxShotUnk5, 01, (KernelWorker.GetSelectedShotData.Status5 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxShotUnk6, 01, (KernelWorker.GetSelectedShotData.Status5 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxShotHasMagic, 01, (KernelWorker.GetSelectedShotData.Status5 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxShotSummonGF, 01, (KernelWorker.GetSelectedShotData.Status5 & 0x80) >= 1 ? true : false);
        }

        private void listBoxShot_SelectedIndexChanged(object sender, EventArgs e)
        {
            _loaded = false;
            if (KernelWorker.Kernel == null || KernelWorker.BackupKernel == null)
                return;
            KernelWorker.ReadShot(listBoxShot.SelectedIndex, KernelWorker.BackupKernel);
            try
            {
                ToolTip(comboBoxShotMagicID, 0x02,comboBoxShotMagicID.Items[KernelWorker.GetSelectedShotData.MagicID]);
                ToolTip(comboBoxShotAttackType, 0x02, comboBoxShotAttackType.Items[KernelWorker.GetSelectedShotData.AttackType]);
                ToolTip(numericUpDownShotAttackPower,0,KernelWorker.GetSelectedShotData.AttackPower);
                ToolTip(checkBoxShotTarget1,1, (KernelWorker.GetSelectedShotData.Target & 0x01) >= 1 ? true : false);
                ToolTip(checkBoxShotTarget2, 1, (KernelWorker.GetSelectedShotData.Target & 0x02) >= 1 ? true : false);
                ToolTip(checkBoxShotTarget3, 1, (KernelWorker.GetSelectedShotData.Target & 0x04) >= 1 ? true : false);
                ToolTip(checkBoxShotTarget4, 1, (KernelWorker.GetSelectedShotData.Target & 0x08) >= 1 ? true : false);
                ToolTip(checkBoxShotTarget5, 1, (KernelWorker.GetSelectedShotData.Target & 0x10) >= 1 ? true : false);
                ToolTip(checkBoxShotTarget6, 1, (KernelWorker.GetSelectedShotData.Target & 0x20) >= 1 ? true : false);
                ToolTip(checkBoxShotTarget7, 1, (KernelWorker.GetSelectedShotData.Target & 0x40) >= 1 ? true : false);
                ToolTip(checkBoxShotTarget8, 1, (KernelWorker.GetSelectedShotData.Target & 0x80) >= 1 ? true : false);
                ToolTip(checkBoxShotFlag1, 1, (KernelWorker.GetSelectedShotData.AttackFlags & 0x01) >= 1 ? true : false);
                ToolTip(checkBoxShotFlag2, 1, (KernelWorker.GetSelectedShotData.AttackFlags & 0x02) >= 1 ? true : false);
                ToolTip(checkBoxShotFlag3, 1, (KernelWorker.GetSelectedShotData.AttackFlags & 0x04) >= 1 ? true : false);
                ToolTip(checkBoxShotFlag4, 1, (KernelWorker.GetSelectedShotData.AttackFlags & 0x08) >= 1 ? true : false);
                ToolTip(checkBoxShotFlag5, 1, (KernelWorker.GetSelectedShotData.AttackFlags & 0x10) >= 1 ? true : false);
                ToolTip(checkBoxShotFlag6, 1, (KernelWorker.GetSelectedShotData.AttackFlags & 0x20) >= 1 ? true : false);
                ToolTip(checkBoxShotFlag7, 1, (KernelWorker.GetSelectedShotData.AttackFlags & 0x40) >= 1 ? true : false);
                ToolTip(checkBoxShotFlag8, 1, (KernelWorker.GetSelectedShotData.AttackFlags & 0x80) >= 1 ? true : false);
                ToolTip(numericUpDownShotHitCount, 0, KernelWorker.GetSelectedShotData.HitCount);
                ToolTip(comboBoxShotElement, 2,comboBoxShotElement.Items[Shot_GetElement()]);
                ToolTip(numericUpDownShotElementPerc,0, KernelWorker.GetSelectedShotData.ElementPerc);
                ToolTip(numericUpDownShotStatusAttack,0, KernelWorker.GetSelectedShotData.StatusAttack);
                ShotStatusWorker(true);
                ToolTip(comboBoxShotItem,2, comboBoxShotItem.Items[ KernelWorker.GetSelectedShotData.UsedItem]);
                ToolTip(numericUpDownShotCrit, 0, KernelWorker.GetSelectedShotData.CritBonus);
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }

            KernelWorker.ReadShot(listBoxShot.SelectedIndex, KernelWorker.Kernel);
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
                ShotStatusWorker(false);
                comboBoxShotItem.SelectedIndex = KernelWorker.GetSelectedShotData.UsedItem;
                numericUpDownShotCrit.Value = KernelWorker.GetSelectedShotData.CritBonus;
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }
            _loaded = true;
        }

        #endregion

        #region Duel

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

        private int Duel_GetButton1()
        {
            return KernelWorker.GetSelectedDuelData.Button1 == KernelWorker.Buttons.EscapeBattle_D ? 0
                 : KernelWorker.GetSelectedDuelData.Button1 == KernelWorker.Buttons.EscapeBattle_D ? 1
                 : KernelWorker.GetSelectedDuelData.Button1 == KernelWorker.Buttons.RotationLeft_H ? 2
                 : KernelWorker.GetSelectedDuelData.Button1 == KernelWorker.Buttons.RotationRight_G ? 3
                 : KernelWorker.GetSelectedDuelData.Button1 == KernelWorker.Buttons.Cancel_C ? 4
                 : KernelWorker.GetSelectedDuelData.Button1 == KernelWorker.Buttons.Menu_V ? 5
                 : KernelWorker.GetSelectedDuelData.Button1 == KernelWorker.Buttons.Select_X ? 6
                 : KernelWorker.GetSelectedDuelData.Button1 == KernelWorker.Buttons.CardGame_S ? 7
                 : KernelWorker.GetSelectedDuelData.Button1 == KernelWorker.Buttons.Up ? 8
                 : KernelWorker.GetSelectedDuelData.Button1 == KernelWorker.Buttons.Right ? 9
                 : KernelWorker.GetSelectedDuelData.Button1 == KernelWorker.Buttons.Down ? 10
                 : KernelWorker.GetSelectedDuelData.Button1 == KernelWorker.Buttons.Left ? 11
                 : 0;
        }

        private int Duel_GetButton2()
        {
            return KernelWorker.GetSelectedDuelData.Button2 == KernelWorker.Buttons.None ? 0
                 : KernelWorker.GetSelectedDuelData.Button2 == KernelWorker.Buttons.EscapeBattle_D ? 1
                 : KernelWorker.GetSelectedDuelData.Button2 == KernelWorker.Buttons.EscapeBattle_D ? 2
                 : KernelWorker.GetSelectedDuelData.Button2 == KernelWorker.Buttons.RotationLeft_H ? 3
                 : KernelWorker.GetSelectedDuelData.Button2 == KernelWorker.Buttons.RotationRight_G ? 4
                 : KernelWorker.GetSelectedDuelData.Button2 == KernelWorker.Buttons.Cancel_C ? 5
                 : KernelWorker.GetSelectedDuelData.Button2 == KernelWorker.Buttons.Menu_V ? 6
                 : KernelWorker.GetSelectedDuelData.Button2 == KernelWorker.Buttons.Select_X ? 7
                 : KernelWorker.GetSelectedDuelData.Button2 == KernelWorker.Buttons.CardGame_S ? 8
                 : KernelWorker.GetSelectedDuelData.Button2 == KernelWorker.Buttons.Up ? 9
                 : KernelWorker.GetSelectedDuelData.Button2 == KernelWorker.Buttons.Right ? 10
                 : KernelWorker.GetSelectedDuelData.Button2 == KernelWorker.Buttons.Down ? 11
                 : KernelWorker.GetSelectedDuelData.Button2 == KernelWorker.Buttons.Left ? 12 
                 : 0;
        }

        private int Duel_GetButton3()
        {
            return KernelWorker.GetSelectedDuelData.Button3 == KernelWorker.Buttons.None ? 0
                 : KernelWorker.GetSelectedDuelData.Button3 == KernelWorker.Buttons.EscapeBattle_D ? 1
                 : KernelWorker.GetSelectedDuelData.Button3 == KernelWorker.Buttons.EscapeBattle_D ? 2
                 : KernelWorker.GetSelectedDuelData.Button3 == KernelWorker.Buttons.RotationLeft_H ? 3
                 : KernelWorker.GetSelectedDuelData.Button3 == KernelWorker.Buttons.RotationRight_G ? 4
                 : KernelWorker.GetSelectedDuelData.Button3 == KernelWorker.Buttons.Cancel_C ? 5
                 : KernelWorker.GetSelectedDuelData.Button3 == KernelWorker.Buttons.Menu_V ? 6
                 : KernelWorker.GetSelectedDuelData.Button3 == KernelWorker.Buttons.Select_X ? 7
                 : KernelWorker.GetSelectedDuelData.Button3 == KernelWorker.Buttons.CardGame_S ? 8
                 : KernelWorker.GetSelectedDuelData.Button3 == KernelWorker.Buttons.Up ? 9
                 : KernelWorker.GetSelectedDuelData.Button3 == KernelWorker.Buttons.Right ? 10
                 : KernelWorker.GetSelectedDuelData.Button3 == KernelWorker.Buttons.Down ? 11
                 : KernelWorker.GetSelectedDuelData.Button3 == KernelWorker.Buttons.Left ? 12
                 : 0;
        }

        private int Duel_GetButton4()
        {
            return KernelWorker.GetSelectedDuelData.Button4 == KernelWorker.Buttons.None ? 0
                 : KernelWorker.GetSelectedDuelData.Button4 == KernelWorker.Buttons.EscapeBattle_D ? 1
                 : KernelWorker.GetSelectedDuelData.Button4 == KernelWorker.Buttons.EscapeBattle_D ? 2
                 : KernelWorker.GetSelectedDuelData.Button4 == KernelWorker.Buttons.RotationLeft_H ? 3
                 : KernelWorker.GetSelectedDuelData.Button4 == KernelWorker.Buttons.RotationRight_G ? 4
                 : KernelWorker.GetSelectedDuelData.Button4 == KernelWorker.Buttons.Cancel_C ? 5
                 : KernelWorker.GetSelectedDuelData.Button4 == KernelWorker.Buttons.Menu_V ? 6
                 : KernelWorker.GetSelectedDuelData.Button4 == KernelWorker.Buttons.Select_X ? 7
                 : KernelWorker.GetSelectedDuelData.Button4 == KernelWorker.Buttons.CardGame_S ? 8
                 : KernelWorker.GetSelectedDuelData.Button4 == KernelWorker.Buttons.Up ? 9
                 : KernelWorker.GetSelectedDuelData.Button4 == KernelWorker.Buttons.Right ? 10
                 : KernelWorker.GetSelectedDuelData.Button4 == KernelWorker.Buttons.Down ? 11
                 : KernelWorker.GetSelectedDuelData.Button4 == KernelWorker.Buttons.Left ? 12
                 : 0;
        }

        private int Duel_GetButton5()
        {
            return KernelWorker.GetSelectedDuelData.Button5 == KernelWorker.Buttons.None ? 0
                 : KernelWorker.GetSelectedDuelData.Button5 == KernelWorker.Buttons.EscapeBattle_D ? 1
                 : KernelWorker.GetSelectedDuelData.Button5 == KernelWorker.Buttons.EscapeBattle_D ? 2
                 : KernelWorker.GetSelectedDuelData.Button5 == KernelWorker.Buttons.RotationLeft_H ? 3
                 : KernelWorker.GetSelectedDuelData.Button5 == KernelWorker.Buttons.RotationRight_G ? 4
                 : KernelWorker.GetSelectedDuelData.Button5 == KernelWorker.Buttons.Cancel_C ? 5
                 : KernelWorker.GetSelectedDuelData.Button5 == KernelWorker.Buttons.Menu_V ? 6
                 : KernelWorker.GetSelectedDuelData.Button5 == KernelWorker.Buttons.Select_X ? 7
                 : KernelWorker.GetSelectedDuelData.Button5 == KernelWorker.Buttons.CardGame_S ? 8
                 : KernelWorker.GetSelectedDuelData.Button5 == KernelWorker.Buttons.Up ? 9
                 : KernelWorker.GetSelectedDuelData.Button5 == KernelWorker.Buttons.Right ? 10
                 : KernelWorker.GetSelectedDuelData.Button5 == KernelWorker.Buttons.Down ? 11
                 : KernelWorker.GetSelectedDuelData.Button5 == KernelWorker.Buttons.Left ? 12
                 : 0;
        }

        private ushort Duel_GetButton1(int Index)
        {
            ushort button = (ushort)(Index == 0 ? (ushort)KernelWorker.Buttons.EscapeBattle_D :
                Index == 1 ? (ushort)KernelWorker.Buttons.EscapeBattle_F :
                Index == 2 ? (ushort)KernelWorker.Buttons.RotationLeft_H :
                Index == 3 ? (ushort)KernelWorker.Buttons.RotationRight_G :
                Index == 4 ? (ushort)KernelWorker.Buttons.Cancel_C :
                Index == 5 ? (ushort)KernelWorker.Buttons.Menu_V :
                Index == 6 ? (ushort)KernelWorker.Buttons.Select_X :
                Index == 7 ? (ushort)KernelWorker.Buttons.CardGame_S :
                Index == 8 ? (ushort)KernelWorker.Buttons.Up :
                Index == 9 ? (ushort)KernelWorker.Buttons.Right :
                Index == 10 ? (ushort)KernelWorker.Buttons.Down :
                Index == 11 ? (ushort)KernelWorker.Buttons.Left :
                0x0000 /*ErrorHandler*/);
            return button;
        }

        private ushort Duel_GetButtonOthers(int Index)
        {
            ushort button = (ushort)(Index == 0 ? (ushort)KernelWorker.Buttons.None :
                Index == 1 ? (ushort)KernelWorker.Buttons.EscapeBattle_D :
                Index == 2 ? (ushort)KernelWorker.Buttons.EscapeBattle_F :
                Index == 3 ? (ushort)KernelWorker.Buttons.RotationLeft_H :
                Index == 4 ? (ushort)KernelWorker.Buttons.RotationRight_G :
                Index == 5 ? (ushort)KernelWorker.Buttons.Cancel_C :
                Index == 6 ? (ushort)KernelWorker.Buttons.Menu_V :
                Index == 7 ? (ushort)KernelWorker.Buttons.Select_X :
                Index == 8 ? (ushort)KernelWorker.Buttons.CardGame_S :
                Index == 9 ? (ushort)KernelWorker.Buttons.Up :
                Index == 10 ? (ushort)KernelWorker.Buttons.Right :
                Index == 11 ? (ushort)KernelWorker.Buttons.Down :
                Index == 12 ? (ushort)KernelWorker.Buttons.Left :
                0x0000 /*ErrorHandler*/);
            return button;
        }

        private void DuelStatusWorker(bool bBackup)
        {
            if (!bBackup)
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
                return;
            }
            ToolTip(checkBoxDuelDeath, 1, (KernelWorker.GetSelectedDuelData.Status1 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxDuelPoison, 1, (KernelWorker.GetSelectedDuelData.Status1 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxDuelPetrify, 1, (KernelWorker.GetSelectedDuelData.Status1 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxDuelDarkness, 1, (KernelWorker.GetSelectedDuelData.Status1 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxDuelSilence, 1, (KernelWorker.GetSelectedDuelData.Status1 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxDuelBerserk, 1, (KernelWorker.GetSelectedDuelData.Status1 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxDuelZombie, 1, (KernelWorker.GetSelectedDuelData.Status1 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxDuelUnk7, 1, (KernelWorker.GetSelectedDuelData.Status1 & 0x80) >= 1 ? true : false);

            ToolTip(checkBoxDuelSleep, 1, (KernelWorker.GetSelectedDuelData.Status2 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxDuelHaste, 1, (KernelWorker.GetSelectedDuelData.Status2 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxDuelSlow, 1, (KernelWorker.GetSelectedDuelData.Status2 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxDuelStop, 1, (KernelWorker.GetSelectedDuelData.Status2 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxDuelRegen, 1, (KernelWorker.GetSelectedDuelData.Status2 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxDuelProtect, 1, (KernelWorker.GetSelectedDuelData.Status2 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxDuelShell, 1, (KernelWorker.GetSelectedDuelData.Status2 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxDuelReflect, 1, (KernelWorker.GetSelectedDuelData.Status2 & 0x80) >= 1 ? true : false);

            ToolTip(checkBoxDuelAura, 1, (KernelWorker.GetSelectedDuelData.Status3 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxDuelCurse, 1, (KernelWorker.GetSelectedDuelData.Status3 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxDuelDoom, 1, (KernelWorker.GetSelectedDuelData.Status3 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxDuelInvincible, 1, (KernelWorker.GetSelectedDuelData.Status3 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxDuelPetrifying, 1, (KernelWorker.GetSelectedDuelData.Status3 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxDuelFloat, 1, (KernelWorker.GetSelectedDuelData.Status3 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxDuelConfusion, 1, (KernelWorker.GetSelectedDuelData.Status3 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxDuelDrain, 1, (KernelWorker.GetSelectedDuelData.Status3 & 0x80) >= 1 ? true : false);

            ToolTip(checkBoxDuelEject, 1, (KernelWorker.GetSelectedDuelData.Status4 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxDuelDouble, 1, (KernelWorker.GetSelectedDuelData.Status4 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxDuelTriple, 1, (KernelWorker.GetSelectedDuelData.Status4 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxDuelDefend, 1, (KernelWorker.GetSelectedDuelData.Status4 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxDuelUnk1, 1, (KernelWorker.GetSelectedDuelData.Status4 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxDuelUnk2, 1, (KernelWorker.GetSelectedDuelData.Status4 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxDuelCharged, 1, (KernelWorker.GetSelectedDuelData.Status4 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxDuelBackAttack, 1, (KernelWorker.GetSelectedDuelData.Status4 & 0x80) >= 1 ? true : false);

            ToolTip(checkBoxDuelVit0, 1, (KernelWorker.GetSelectedDuelData.Status5 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxDuelAngelWing, 1, (KernelWorker.GetSelectedDuelData.Status5 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxDuelUnk3, 1, (KernelWorker.GetSelectedDuelData.Status5 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxDuelUnk4, 1, (KernelWorker.GetSelectedDuelData.Status5 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxDuelUnk5, 1, (KernelWorker.GetSelectedDuelData.Status5 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxDuelUnk6, 1, (KernelWorker.GetSelectedDuelData.Status5 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxDuelHasMagic, 1, (KernelWorker.GetSelectedDuelData.Status5 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxDuelSummonGF, 1, (KernelWorker.GetSelectedDuelData.Status5 & 0x80) >= 1 ? true : false);
        }


        private void listBoxDuel_SelectedIndexChanged(object sender, EventArgs e)
        {
            _loaded = false;
            if (KernelWorker.Kernel == null || KernelWorker.BackupKernel == null)
                return;

            KernelWorker.ReadDuel(listBoxDuel.SelectedIndex, KernelWorker.BackupKernel);
            try
            {
                ToolTip(comboBoxDuelMagicID,2, comboBoxDuelMagicID.Items[KernelWorker.GetSelectedDuelData.MagicID]);
                ToolTip(comboBoxDuelAttackType,2, comboBoxDuelAttackType.Items[KernelWorker.GetSelectedDuelData.AttackType]);
                ToolTip(numericUpDownDuelAttackPower,0, KernelWorker.GetSelectedDuelData.AttackPower);
                ToolTip(checkBoxDuelFlag1,1, (KernelWorker.GetSelectedDuelData.AttackFlags & 0x01) >= 1 ? true : false);
                ToolTip(checkBoxDuelFlag2,1, (KernelWorker.GetSelectedDuelData.AttackFlags & 0x02) >= 1 ? true : false);
                ToolTip(checkBoxDuelFlag3,1, (KernelWorker.GetSelectedDuelData.AttackFlags & 0x04) >= 1 ? true : false);
                ToolTip(checkBoxDuelFlag4,1, (KernelWorker.GetSelectedDuelData.AttackFlags & 0x08) >= 1 ? true : false);
                ToolTip(checkBoxDuelFlag5,1, (KernelWorker.GetSelectedDuelData.AttackFlags & 0x10) >= 1 ? true : false);
                ToolTip(checkBoxDuelFlag6,1, (KernelWorker.GetSelectedDuelData.AttackFlags & 0x20) >= 1 ? true : false);
                ToolTip(checkBoxDuelFlag7,1, (KernelWorker.GetSelectedDuelData.AttackFlags & 0x40) >= 1 ? true : false);
                ToolTip(checkBoxDuelFlag8,1, (KernelWorker.GetSelectedDuelData.AttackFlags & 0x80) >= 1 ? true : false);
                ToolTip(checkBoxDuelTarget1,1, (KernelWorker.GetSelectedDuelData.Target & 0x01) >= 1 ? true : false);
                ToolTip(checkBoxDuelTarget2,1, (KernelWorker.GetSelectedDuelData.Target & 0x02) >= 1 ? true : false);
                ToolTip(checkBoxDuelTarget3,1, (KernelWorker.GetSelectedDuelData.Target & 0x04) >= 1 ? true : false);
                ToolTip(checkBoxDuelTarget4,1, (KernelWorker.GetSelectedDuelData.Target & 0x08) >= 1 ? true : false);
                ToolTip(checkBoxDuelTarget5,1, (KernelWorker.GetSelectedDuelData.Target & 0x10) >= 1 ? true : false);
                ToolTip(checkBoxDuelTarget6,1, (KernelWorker.GetSelectedDuelData.Target & 0x20) >= 1 ? true : false);
                ToolTip(checkBoxDuelTarget7,1, (KernelWorker.GetSelectedDuelData.Target & 0x40) >= 1 ? true : false);
                ToolTip(checkBoxDuelTarget8,1, (KernelWorker.GetSelectedDuelData.Target & 0x80) >= 1 ? true : false);
                ToolTip(numericUpDownDuelHitCount,0, KernelWorker.GetSelectedDuelData.HitCount);
                ToolTip(comboBoxDuelElement,2, comboBoxDuelElement.Items[Duel_GetElement()]);
                ToolTip(numericUpDownDuelElementPerc,0, KernelWorker.GetSelectedDuelData.ElementPerc);
                ToolTip(numericUpDownDuelStatusAttack,0, KernelWorker.GetSelectedDuelData.StatusAttack);
                ToolTip(comboBoxDuelButton1, 2, comboBoxDuelButton1.Items[Duel_GetButton1()]);
                ToolTip(checkBoxDuelFinisher, 1, ((KernelWorker.GetSelectedDuelData.IsFinisher >> 8) & 1) == 1);
                ToolTip(comboBoxDuelButton2, 2, comboBoxDuelButton2.Items[Duel_GetButton2()]);
                ToolTip(comboBoxDuelButton3, 2, comboBoxDuelButton3.Items[Duel_GetButton3()]);
                ToolTip(comboBoxDuelButton4, 2, comboBoxDuelButton4.Items[Duel_GetButton4()]);
                ToolTip(comboBoxDuelButton5, 2, comboBoxDuelButton5.Items[Duel_GetButton5()]);
                DuelStatusWorker(true);
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }

            KernelWorker.ReadDuel(listBoxDuel.SelectedIndex, KernelWorker.Kernel);
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
                comboBoxDuelButton1.SelectedIndex = Duel_GetButton1();
                checkBoxDuelFinisher.Checked = ((KernelWorker.GetSelectedDuelData.IsFinisher >> 8) & 1) == 1;
                comboBoxDuelButton2.SelectedIndex = Duel_GetButton2();
                comboBoxDuelButton3.SelectedIndex = Duel_GetButton3();
                comboBoxDuelButton4.SelectedIndex = Duel_GetButton4();
                comboBoxDuelButton5.SelectedIndex = Duel_GetButton5();
                DuelStatusWorker(false);
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }
            _loaded = true;
        }

        #endregion

        #region Duel params

        private void DuelParams()
        {
            _loaded = false;
            if (KernelWorker.Kernel == null || KernelWorker.BackupKernel == null)
                return;

            KernelWorker.ReadDuelParams(KernelWorker.BackupKernel);
            try
            {
                ToolTip(comboBoxDuelMove0,2,comboBoxDuelMove0.Items[KernelWorker.GetSelectedDuelParamsData.StartMove0]);
                ToolTip(comboBoxDuelMove1, 2, comboBoxDuelMove1.Items[KernelWorker.GetSelectedDuelParamsData.StartMove1]);
                ToolTip(comboBoxDuelMove2, 2, comboBoxDuelMove2.Items[KernelWorker.GetSelectedDuelParamsData.StartMove2]);
                ToolTip(comboBoxDuelMove3, 2, comboBoxDuelMove3.Items[KernelWorker.GetSelectedDuelParamsData.StartMove3]);
                ToolTip(comboBoxDuelMove4, 2, comboBoxDuelMove4.Items[KernelWorker.GetSelectedDuelParamsData.StartMove4]);
                ToolTip(comboBoxDuelMove5, 2, comboBoxDuelMove5.Items[KernelWorker.GetSelectedDuelParamsData.StartMove5]);
                ToolTip(comboBoxDuelMove6, 2, comboBoxDuelMove6.Items[KernelWorker.GetSelectedDuelParamsData.StartMove6]);
                ToolTip(comboBoxDuelMove7, 2, comboBoxDuelMove7.Items[KernelWorker.GetSelectedDuelParamsData.StartMove7]);
                ToolTip(comboBoxDuelMove8, 2, comboBoxDuelMove8.Items[KernelWorker.GetSelectedDuelParamsData.StartMove8]);
                ToolTip(comboBoxDuelMove9, 2, comboBoxDuelMove9.Items[KernelWorker.GetSelectedDuelParamsData.StartMove9]);
                ToolTip(comboBoxDuelMove10, 2, comboBoxDuelMove10.Items[KernelWorker.GetSelectedDuelParamsData.StartMove10]);
                ToolTip(comboBoxDuelMove11, 2, comboBoxDuelMove11.Items[KernelWorker.GetSelectedDuelParamsData.StartMove11]);
                ToolTip(comboBoxDuelMove12, 2, comboBoxDuelMove12.Items[KernelWorker.GetSelectedDuelParamsData.StartMove12]);
                ToolTip(comboBoxDuelMove13, 2, comboBoxDuelMove13.Items[KernelWorker.GetSelectedDuelParamsData.StartMove13]);
                ToolTip(comboBoxDuelMove14, 2, comboBoxDuelMove14.Items[KernelWorker.GetSelectedDuelParamsData.StartMove14]);
                ToolTip(comboBoxDuelMove15, 2, comboBoxDuelMove15.Items[KernelWorker.GetSelectedDuelParamsData.StartMove15]);
                ToolTip(comboBoxDuelMove16, 2, comboBoxDuelMove16.Items[KernelWorker.GetSelectedDuelParamsData.StartMove16]);
                ToolTip(comboBoxDuelMove17, 2, comboBoxDuelMove17.Items[KernelWorker.GetSelectedDuelParamsData.StartMove17]);
                ToolTip(comboBoxDuelMove18, 2, comboBoxDuelMove18.Items[KernelWorker.GetSelectedDuelParamsData.StartMove18]);
                ToolTip(comboBoxDuelMove19, 2, comboBoxDuelMove19.Items[KernelWorker.GetSelectedDuelParamsData.StartMove19]);
                ToolTip(comboBoxDuelMove20, 2, comboBoxDuelMove20.Items[KernelWorker.GetSelectedDuelParamsData.StartMove20]);
                ToolTip(comboBoxDuelMove21, 2, comboBoxDuelMove21.Items[KernelWorker.GetSelectedDuelParamsData.StartMove21]);
                ToolTip(comboBoxDuelMove22, 2, comboBoxDuelMove22.Items[KernelWorker.GetSelectedDuelParamsData.StartMove22]);
                ToolTip(comboBoxDuelMove23, 2, comboBoxDuelMove23.Items[KernelWorker.GetSelectedDuelParamsData.StartMove23]);
                ToolTip(comboBoxDuelMove24, 2, comboBoxDuelMove24.Items[KernelWorker.GetSelectedDuelParamsData.StartMove24]);
                ToolTip(numericUpDownDuelNextSeq0_1,0, KernelWorker.GetSelectedDuelParamsData.NextSeq0_1);
                ToolTip(numericUpDownDuelNextSeq0_2, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq0_2);
                ToolTip(numericUpDownDuelNextSeq0_3, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq0_3);
                ToolTip(numericUpDownDuelNextSeq0_1, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq0_1);
                ToolTip(numericUpDownDuelNextSeq0_2, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq0_2);
                ToolTip(numericUpDownDuelNextSeq0_3, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq0_3);
                ToolTip(numericUpDownDuelNextSeq1_1, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq1_1);
                ToolTip(numericUpDownDuelNextSeq1_2, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq1_2);
                ToolTip(numericUpDownDuelNextSeq1_3, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq1_3);
                ToolTip(numericUpDownDuelNextSeq2_1, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq2_1);
                ToolTip(numericUpDownDuelNextSeq2_2, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq2_2);
                ToolTip(numericUpDownDuelNextSeq2_3, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq2_3);
                ToolTip(numericUpDownDuelNextSeq3_1, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq3_1);
                ToolTip(numericUpDownDuelNextSeq3_2, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq3_2);
                ToolTip(numericUpDownDuelNextSeq3_3, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq3_3);
                ToolTip(numericUpDownDuelNextSeq4_1, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq4_1);
                ToolTip(numericUpDownDuelNextSeq4_2, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq4_2);
                ToolTip(numericUpDownDuelNextSeq4_3, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq4_3);
                ToolTip(numericUpDownDuelNextSeq5_1, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq5_1);
                ToolTip(numericUpDownDuelNextSeq5_2, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq5_2);
                ToolTip(numericUpDownDuelNextSeq5_3, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq5_3);
                ToolTip(numericUpDownDuelNextSeq6_1, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq6_1);
                ToolTip(numericUpDownDuelNextSeq6_2, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq6_2);
                ToolTip(numericUpDownDuelNextSeq6_3, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq6_3);
                ToolTip(numericUpDownDuelNextSeq7_1, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq7_1);
                ToolTip(numericUpDownDuelNextSeq7_2, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq7_2);
                ToolTip(numericUpDownDuelNextSeq7_3, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq7_3);
                ToolTip(numericUpDownDuelNextSeq8_1, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq8_1);
                ToolTip(numericUpDownDuelNextSeq8_2, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq8_2);
                ToolTip(numericUpDownDuelNextSeq8_3, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq8_3);
                ToolTip(numericUpDownDuelNextSeq9_1, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq9_1);
                ToolTip(numericUpDownDuelNextSeq9_2, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq9_2);
                ToolTip(numericUpDownDuelNextSeq9_3, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq9_3);
                ToolTip(numericUpDownDuelNextSeq10_1, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq10_1);
                ToolTip(numericUpDownDuelNextSeq10_2, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq10_2);
                ToolTip(numericUpDownDuelNextSeq10_3, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq10_3);
                ToolTip(numericUpDownDuelNextSeq11_1, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq11_1);
                ToolTip(numericUpDownDuelNextSeq11_2, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq11_2);
                ToolTip(numericUpDownDuelNextSeq11_3, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq11_3);
                ToolTip(numericUpDownDuelNextSeq12_1, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq12_1);
                ToolTip(numericUpDownDuelNextSeq12_2, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq12_2);
                ToolTip(numericUpDownDuelNextSeq12_3, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq12_3);
                ToolTip(numericUpDownDuelNextSeq13_1, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq13_1);
                ToolTip(numericUpDownDuelNextSeq13_2, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq13_2);
                ToolTip(numericUpDownDuelNextSeq13_3, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq13_3);
                ToolTip(numericUpDownDuelNextSeq14_1, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq14_1);
                ToolTip(numericUpDownDuelNextSeq14_2, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq14_2);
                ToolTip(numericUpDownDuelNextSeq14_3, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq14_3);
                ToolTip(numericUpDownDuelNextSeq15_1, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq15_1);
                ToolTip(numericUpDownDuelNextSeq15_2, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq15_2);
                ToolTip(numericUpDownDuelNextSeq15_3, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq15_3);
                ToolTip(numericUpDownDuelNextSeq16_1, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq16_1);
                ToolTip(numericUpDownDuelNextSeq16_2, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq16_2);
                ToolTip(numericUpDownDuelNextSeq16_3, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq16_3);
                ToolTip(numericUpDownDuelNextSeq17_1, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq17_1);
                ToolTip(numericUpDownDuelNextSeq17_2, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq17_2);
                ToolTip(numericUpDownDuelNextSeq17_3, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq17_3);
                ToolTip(numericUpDownDuelNextSeq18_1, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq18_1);
                ToolTip(numericUpDownDuelNextSeq18_2, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq18_2);
                ToolTip(numericUpDownDuelNextSeq18_3, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq18_3);
                ToolTip(numericUpDownDuelNextSeq19_1, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq19_1);
                ToolTip(numericUpDownDuelNextSeq19_2, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq19_2);
                ToolTip(numericUpDownDuelNextSeq19_3, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq19_3);
                ToolTip(numericUpDownDuelNextSeq20_1, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq20_1);
                ToolTip(numericUpDownDuelNextSeq20_2, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq20_2);
                ToolTip(numericUpDownDuelNextSeq20_3, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq20_3);
                ToolTip(numericUpDownDuelNextSeq21_1, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq21_1);
                ToolTip(numericUpDownDuelNextSeq21_2, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq21_2);
                ToolTip(numericUpDownDuelNextSeq21_3, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq21_3);
                ToolTip(numericUpDownDuelNextSeq22_1, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq22_1);
                ToolTip(numericUpDownDuelNextSeq22_2, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq22_2);
                ToolTip(numericUpDownDuelNextSeq22_3, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq22_3);
                ToolTip(numericUpDownDuelNextSeq23_1, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq23_1);
                ToolTip(numericUpDownDuelNextSeq23_2, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq23_2);
                ToolTip(numericUpDownDuelNextSeq23_3, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq23_3);
                ToolTip(numericUpDownDuelNextSeq24_1, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq24_1);
                ToolTip(numericUpDownDuelNextSeq24_2, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq24_2);
                ToolTip(numericUpDownDuelNextSeq24_3, 0, KernelWorker.GetSelectedDuelParamsData.NextSeq24_3);
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }

            KernelWorker.ReadDuelParams(KernelWorker.Kernel);
            try
            {
                comboBoxDuelMove0.SelectedIndex = KernelWorker.GetSelectedDuelParamsData.StartMove0;
                comboBoxDuelMove1.SelectedIndex = KernelWorker.GetSelectedDuelParamsData.StartMove1;
                comboBoxDuelMove2.SelectedIndex = KernelWorker.GetSelectedDuelParamsData.StartMove2;
                comboBoxDuelMove3.SelectedIndex = KernelWorker.GetSelectedDuelParamsData.StartMove3;
                comboBoxDuelMove4.SelectedIndex = KernelWorker.GetSelectedDuelParamsData.StartMove4;
                comboBoxDuelMove5.SelectedIndex = KernelWorker.GetSelectedDuelParamsData.StartMove5;
                comboBoxDuelMove6.SelectedIndex = KernelWorker.GetSelectedDuelParamsData.StartMove6;
                comboBoxDuelMove7.SelectedIndex = KernelWorker.GetSelectedDuelParamsData.StartMove7;
                comboBoxDuelMove8.SelectedIndex = KernelWorker.GetSelectedDuelParamsData.StartMove8;
                comboBoxDuelMove9.SelectedIndex = KernelWorker.GetSelectedDuelParamsData.StartMove9;
                comboBoxDuelMove10.SelectedIndex = KernelWorker.GetSelectedDuelParamsData.StartMove10;
                comboBoxDuelMove11.SelectedIndex = KernelWorker.GetSelectedDuelParamsData.StartMove11;
                comboBoxDuelMove12.SelectedIndex = KernelWorker.GetSelectedDuelParamsData.StartMove12;
                comboBoxDuelMove13.SelectedIndex = KernelWorker.GetSelectedDuelParamsData.StartMove13;
                comboBoxDuelMove14.SelectedIndex = KernelWorker.GetSelectedDuelParamsData.StartMove14;
                comboBoxDuelMove15.SelectedIndex = KernelWorker.GetSelectedDuelParamsData.StartMove15;
                comboBoxDuelMove16.SelectedIndex = KernelWorker.GetSelectedDuelParamsData.StartMove16;
                comboBoxDuelMove17.SelectedIndex = KernelWorker.GetSelectedDuelParamsData.StartMove17;
                comboBoxDuelMove18.SelectedIndex = KernelWorker.GetSelectedDuelParamsData.StartMove18;
                comboBoxDuelMove19.SelectedIndex = KernelWorker.GetSelectedDuelParamsData.StartMove19;
                comboBoxDuelMove20.SelectedIndex = KernelWorker.GetSelectedDuelParamsData.StartMove20;
                comboBoxDuelMove21.SelectedIndex = KernelWorker.GetSelectedDuelParamsData.StartMove21;
                comboBoxDuelMove22.SelectedIndex = KernelWorker.GetSelectedDuelParamsData.StartMove22;
                comboBoxDuelMove23.SelectedIndex = KernelWorker.GetSelectedDuelParamsData.StartMove23;
                comboBoxDuelMove24.SelectedIndex = KernelWorker.GetSelectedDuelParamsData.StartMove24;
                numericUpDownDuelNextSeq0_1.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq0_1;
                numericUpDownDuelNextSeq0_2.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq0_2;
                numericUpDownDuelNextSeq0_3.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq0_3;
                numericUpDownDuelNextSeq0_1.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq0_1;
                numericUpDownDuelNextSeq0_2.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq0_2;
                numericUpDownDuelNextSeq0_3.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq0_3;
                numericUpDownDuelNextSeq1_1.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq1_1;
                numericUpDownDuelNextSeq1_2.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq1_2;
                numericUpDownDuelNextSeq1_3.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq1_3;
                numericUpDownDuelNextSeq2_1.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq2_1;
                numericUpDownDuelNextSeq2_2.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq2_2;
                numericUpDownDuelNextSeq2_3.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq2_3;
                numericUpDownDuelNextSeq3_1.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq3_1;
                numericUpDownDuelNextSeq3_2.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq3_2;
                numericUpDownDuelNextSeq3_3.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq3_3;
                numericUpDownDuelNextSeq4_1.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq4_1;
                numericUpDownDuelNextSeq4_2.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq4_2;
                numericUpDownDuelNextSeq4_3.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq4_3;
                numericUpDownDuelNextSeq5_1.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq5_1;
                numericUpDownDuelNextSeq5_2.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq5_2;
                numericUpDownDuelNextSeq5_3.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq5_3;
                numericUpDownDuelNextSeq6_1.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq6_1;
                numericUpDownDuelNextSeq6_2.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq6_2;
                numericUpDownDuelNextSeq6_3.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq6_3;
                numericUpDownDuelNextSeq7_1.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq7_1;
                numericUpDownDuelNextSeq7_2.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq7_2;
                numericUpDownDuelNextSeq7_3.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq7_3;
                numericUpDownDuelNextSeq8_1.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq8_1;
                numericUpDownDuelNextSeq8_2.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq8_2;
                numericUpDownDuelNextSeq8_3.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq8_3;
                numericUpDownDuelNextSeq9_1.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq9_1;
                numericUpDownDuelNextSeq9_2.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq9_2;
                numericUpDownDuelNextSeq9_3.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq9_3;
                numericUpDownDuelNextSeq10_1.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq10_1;
                numericUpDownDuelNextSeq10_2.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq10_2;
                numericUpDownDuelNextSeq10_3.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq10_3;
                numericUpDownDuelNextSeq11_1.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq11_1;
                numericUpDownDuelNextSeq11_2.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq11_2;
                numericUpDownDuelNextSeq11_3.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq11_3;
                numericUpDownDuelNextSeq12_1.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq12_1;
                numericUpDownDuelNextSeq12_2.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq12_2;
                numericUpDownDuelNextSeq12_3.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq12_3;
                numericUpDownDuelNextSeq13_1.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq13_1;
                numericUpDownDuelNextSeq13_2.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq13_2;
                numericUpDownDuelNextSeq13_3.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq13_3;
                numericUpDownDuelNextSeq14_1.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq14_1;
                numericUpDownDuelNextSeq14_2.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq14_2;
                numericUpDownDuelNextSeq14_3.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq14_3;
                numericUpDownDuelNextSeq15_1.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq15_1;
                numericUpDownDuelNextSeq15_2.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq15_2;
                numericUpDownDuelNextSeq15_3.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq15_3;
                numericUpDownDuelNextSeq16_1.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq16_1;
                numericUpDownDuelNextSeq16_2.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq16_2;
                numericUpDownDuelNextSeq16_3.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq16_3;
                numericUpDownDuelNextSeq17_1.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq17_1;
                numericUpDownDuelNextSeq17_2.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq17_2;
                numericUpDownDuelNextSeq17_3.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq17_3;
                numericUpDownDuelNextSeq18_1.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq18_1;
                numericUpDownDuelNextSeq18_2.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq18_2;
                numericUpDownDuelNextSeq18_3.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq18_3;
                numericUpDownDuelNextSeq19_1.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq19_1;
                numericUpDownDuelNextSeq19_2.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq19_2;
                numericUpDownDuelNextSeq19_3.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq19_3;
                numericUpDownDuelNextSeq20_1.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq20_1;
                numericUpDownDuelNextSeq20_2.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq20_2;
                numericUpDownDuelNextSeq20_3.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq20_3;
                numericUpDownDuelNextSeq21_1.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq21_1;
                numericUpDownDuelNextSeq21_2.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq21_2;
                numericUpDownDuelNextSeq21_3.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq21_3;
                numericUpDownDuelNextSeq22_1.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq22_1;
                numericUpDownDuelNextSeq22_2.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq22_2;
                numericUpDownDuelNextSeq22_3.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq22_3;
                numericUpDownDuelNextSeq23_1.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq23_1;
                numericUpDownDuelNextSeq23_2.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq23_2;
                numericUpDownDuelNextSeq23_3.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq23_3;
                numericUpDownDuelNextSeq24_1.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq24_1;
                numericUpDownDuelNextSeq24_2.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq24_2;
                numericUpDownDuelNextSeq24_3.Value = KernelWorker.GetSelectedDuelParamsData.NextSeq24_3;
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }
            _loaded = true;
        }

        #endregion

        #region Combine

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

        private void CombineStatusWorker(bool bBackup)
        {
            if (!bBackup)
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
                return;
            }
            ToolTip(checkBoxCombineDeath,1,(KernelWorker.GetSelectedCombineData.Status1 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxCombinePoison,1,(KernelWorker.GetSelectedCombineData.Status1 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxCombinePetrify,1,(KernelWorker.GetSelectedCombineData.Status1 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxCombineDarkness,1,(KernelWorker.GetSelectedCombineData.Status1 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxCombineSilence,1,(KernelWorker.GetSelectedCombineData.Status1 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxCombineBerserk,1,(KernelWorker.GetSelectedCombineData.Status1 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxCombineZombie,1,(KernelWorker.GetSelectedCombineData.Status1 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxCombineUnk7,1,(KernelWorker.GetSelectedCombineData.Status1 & 0x80) >= 1 ? true : false);

            ToolTip(checkBoxCombineSleep,1,(KernelWorker.GetSelectedCombineData.Status2 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxCombineHaste,1,(KernelWorker.GetSelectedCombineData.Status2 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxCombineSlow,1,(KernelWorker.GetSelectedCombineData.Status2 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxCombineStop,1,(KernelWorker.GetSelectedCombineData.Status2 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxCombineRegen,1,(KernelWorker.GetSelectedCombineData.Status2 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxCombineProtect,1,(KernelWorker.GetSelectedCombineData.Status2 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxCombineShell,1,(KernelWorker.GetSelectedCombineData.Status2 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxCombineReflect,1,(KernelWorker.GetSelectedCombineData.Status2 & 0x80) >= 1 ? true : false);

            ToolTip(checkBoxCombineAura,1,(KernelWorker.GetSelectedCombineData.Status3 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxCombineCurse,1,(KernelWorker.GetSelectedCombineData.Status3 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxCombineDoom,1,(KernelWorker.GetSelectedCombineData.Status3 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxCombineInvincible,1,(KernelWorker.GetSelectedCombineData.Status3 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxCombinePetrifying,1,(KernelWorker.GetSelectedCombineData.Status3 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxCombineFloat,1,(KernelWorker.GetSelectedCombineData.Status3 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxCombineConfusion,1,(KernelWorker.GetSelectedCombineData.Status3 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxCombineDrain,1,(KernelWorker.GetSelectedCombineData.Status3 & 0x80) >= 1 ? true : false);

            ToolTip(checkBoxCombineEject,1,(KernelWorker.GetSelectedCombineData.Status4 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxCombineDouble,1,(KernelWorker.GetSelectedCombineData.Status4 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxCombineTriple,1,(KernelWorker.GetSelectedCombineData.Status4 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxCombineDefend,1,(KernelWorker.GetSelectedCombineData.Status4 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxCombineUnk1,1,(KernelWorker.GetSelectedCombineData.Status4 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxCombineUnk2,1,(KernelWorker.GetSelectedCombineData.Status4 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxCombineCharged,1,(KernelWorker.GetSelectedCombineData.Status4 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxCombineBackAttack,1,(KernelWorker.GetSelectedCombineData.Status4 & 0x80) >= 1 ? true : false);

            ToolTip(checkBoxCombineVit0,1,(KernelWorker.GetSelectedCombineData.Status5 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxCombineAngelWing,1,(KernelWorker.GetSelectedCombineData.Status5 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxCombineUnk3,1,(KernelWorker.GetSelectedCombineData.Status5 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxCombineUnk4,1,(KernelWorker.GetSelectedCombineData.Status5 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxCombineUnk5,1,(KernelWorker.GetSelectedCombineData.Status5 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxCombineUnk6,1,(KernelWorker.GetSelectedCombineData.Status5 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxCombineHasMagic,1,(KernelWorker.GetSelectedCombineData.Status5 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxCombineSummonGF,1,(KernelWorker.GetSelectedCombineData.Status5 & 0x80) >= 1 ? true : false);
        }

        private void listBoxCombine_SelectedIndexChanged(object sender, EventArgs e)
        {
            _loaded = false;
            if (KernelWorker.Kernel == null || KernelWorker.BackupKernel == null)
                return;

            KernelWorker.ReadCombine(listBoxCombine.SelectedIndex, KernelWorker.BackupKernel);
            try
            {
                ToolTip(comboBoxCombineMagicID,2, comboBoxCombineMagicID.Items[KernelWorker.GetSelectedCombineData.MagicID]);
                ToolTip(comboBoxCombineAttackType,2, comboBoxCombineAttackType.Items[KernelWorker.GetSelectedCombineData.AttackType]);
                ToolTip(numericUpDownCombineAttackPower,0, KernelWorker.GetSelectedCombineData.AttackPower);
                ToolTip(checkBoxCombineFlag1,1,(KernelWorker.GetSelectedCombineData.AttackFlags & 0x01) >= 1 ? true : false);
                ToolTip(checkBoxCombineFlag2,1,(KernelWorker.GetSelectedCombineData.AttackFlags & 0x02) >= 1 ? true : false);
                ToolTip(checkBoxCombineFlag3,1,(KernelWorker.GetSelectedCombineData.AttackFlags & 0x04) >= 1 ? true : false);
                ToolTip(checkBoxCombineFlag4,1,(KernelWorker.GetSelectedCombineData.AttackFlags & 0x08) >= 1 ? true : false);
                ToolTip(checkBoxCombineFlag5,1,(KernelWorker.GetSelectedCombineData.AttackFlags & 0x10) >= 1 ? true : false);
                ToolTip(checkBoxCombineFlag6,1,(KernelWorker.GetSelectedCombineData.AttackFlags & 0x20) >= 1 ? true : false);
                ToolTip(checkBoxCombineFlag7,1,(KernelWorker.GetSelectedCombineData.AttackFlags & 0x40) >= 1 ? true : false);
                ToolTip(checkBoxCombineFlag8,1,(KernelWorker.GetSelectedCombineData.AttackFlags & 0x80) >= 1 ? true : false);
                ToolTip(checkBoxCombineTarget1,1,(KernelWorker.GetSelectedCombineData.Target & 0x01) >= 1 ? true : false);
                ToolTip(checkBoxCombineTarget2,1,(KernelWorker.GetSelectedCombineData.Target & 0x02) >= 1 ? true : false);
                ToolTip(checkBoxCombineTarget3,1,(KernelWorker.GetSelectedCombineData.Target & 0x04) >= 1 ? true : false);
                ToolTip(checkBoxCombineTarget4,1,(KernelWorker.GetSelectedCombineData.Target & 0x08) >= 1 ? true : false);
                ToolTip(checkBoxCombineTarget5,1,(KernelWorker.GetSelectedCombineData.Target & 0x10) >= 1 ? true : false);
                ToolTip(checkBoxCombineTarget6,1,(KernelWorker.GetSelectedCombineData.Target & 0x20) >= 1 ? true : false);
                ToolTip(checkBoxCombineTarget7,1,(KernelWorker.GetSelectedCombineData.Target & 0x40) >= 1 ? true : false);
                ToolTip(checkBoxCombineTarget8,1,(KernelWorker.GetSelectedCombineData.Target & 0x80) >= 1 ? true : false);
                ToolTip(numericUpDownCombineHitCount,0, KernelWorker.GetSelectedCombineData.HitCount);
                ToolTip(comboBoxCombineElement,2, comboBoxCombineElement.Items[Combine_GetElement()]);
                ToolTip(numericUpDownCombineElementPerc,0, KernelWorker.GetSelectedCombineData.ElementPerc);
                ToolTip(numericUpDownCombineStatusAttack,0, KernelWorker.GetSelectedCombineData.StatusAttack);
                CombineStatusWorker(true);
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }

            KernelWorker.ReadCombine(listBoxCombine.SelectedIndex, KernelWorker.Kernel);
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
                CombineStatusWorker(false);
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }
            _loaded = true;
        }

        #endregion

        #region Battle items

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

        private void BattleItemsStatusWorker(bool bBackup)
        {
            if (!bBackup)
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
                return;
            }
            ToolTip(checkBoxBattleItemsDeath,1, (KernelWorker.GetSelectedBattleItemsData.Status1 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxBattleItemsPoison,1, (KernelWorker.GetSelectedBattleItemsData.Status1 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxBattleItemsPetrify,1, (KernelWorker.GetSelectedBattleItemsData.Status1 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxBattleItemsDarkness,1, (KernelWorker.GetSelectedBattleItemsData.Status1 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxBattleItemsSilence,1, (KernelWorker.GetSelectedBattleItemsData.Status1 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxBattleItemsBerserk,1, (KernelWorker.GetSelectedBattleItemsData.Status1 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxBattleItemsZombie,1, (KernelWorker.GetSelectedBattleItemsData.Status1 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxBattleItemsUnk7,1, (KernelWorker.GetSelectedBattleItemsData.Status1 & 0x80) >= 1 ? true : false);

            ToolTip(checkBoxBattleItemsSleep,1, (KernelWorker.GetSelectedBattleItemsData.Status2 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxBattleItemsHaste,1, (KernelWorker.GetSelectedBattleItemsData.Status2 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxBattleItemsSlow,1, (KernelWorker.GetSelectedBattleItemsData.Status2 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxBattleItemsStop,1, (KernelWorker.GetSelectedBattleItemsData.Status2 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxBattleItemsRegen,1, (KernelWorker.GetSelectedBattleItemsData.Status2 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxBattleItemsProtect,1, (KernelWorker.GetSelectedBattleItemsData.Status2 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxBattleItemsShell,1, (KernelWorker.GetSelectedBattleItemsData.Status2 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxBattleItemsReflect,1, (KernelWorker.GetSelectedBattleItemsData.Status2 & 0x80) >= 1 ? true : false);

            ToolTip(checkBoxBattleItemsAura,1, (KernelWorker.GetSelectedBattleItemsData.Status3 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxBattleItemsCurse,1, (KernelWorker.GetSelectedBattleItemsData.Status3 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxBattleItemsDoom,1, (KernelWorker.GetSelectedBattleItemsData.Status3 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxBattleItemsInvincible,1, (KernelWorker.GetSelectedBattleItemsData.Status3 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxBattleItemsPetrifying,1, (KernelWorker.GetSelectedBattleItemsData.Status3 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxBattleItemsFloat,1, (KernelWorker.GetSelectedBattleItemsData.Status3 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxBattleItemsConfusion,1, (KernelWorker.GetSelectedBattleItemsData.Status3 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxBattleItemsDrain,1, (KernelWorker.GetSelectedBattleItemsData.Status3 & 0x80) >= 1 ? true : false);

            ToolTip(checkBoxBattleItemsEject,1, (KernelWorker.GetSelectedBattleItemsData.Status4 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxBattleItemsDouble,1, (KernelWorker.GetSelectedBattleItemsData.Status4 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxBattleItemsTriple,1, (KernelWorker.GetSelectedBattleItemsData.Status4 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxBattleItemsDefend,1, (KernelWorker.GetSelectedBattleItemsData.Status4 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxBattleItemsUnk1,1, (KernelWorker.GetSelectedBattleItemsData.Status4 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxBattleItemsUnk2,1, (KernelWorker.GetSelectedBattleItemsData.Status4 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxBattleItemsCharged,1, (KernelWorker.GetSelectedBattleItemsData.Status4 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxBattleItemsBackAttack,1, (KernelWorker.GetSelectedBattleItemsData.Status4 & 0x80) >= 1 ? true : false);

            ToolTip(checkBoxBattleItemsVit0,1, (KernelWorker.GetSelectedBattleItemsData.Status5 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxBattleItemsAngelWing,1, (KernelWorker.GetSelectedBattleItemsData.Status5 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxBattleItemsUnk3,1, (KernelWorker.GetSelectedBattleItemsData.Status5 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxBattleItemsUnk4,1, (KernelWorker.GetSelectedBattleItemsData.Status5 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxBattleItemsUnk5,1, (KernelWorker.GetSelectedBattleItemsData.Status5 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxBattleItemsUnk6,1, (KernelWorker.GetSelectedBattleItemsData.Status5 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxBattleItemsHasMagic,1, (KernelWorker.GetSelectedBattleItemsData.Status5 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxBattleItemsSummonGF,1, (KernelWorker.GetSelectedBattleItemsData.Status5 & 0x80) >= 1 ? true : false);
        }

        private void listBoxBattleItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            _loaded = false;
            if (KernelWorker.Kernel == null || KernelWorker.BackupKernel == null)
                return;

            KernelWorker.ReadBattleItems(listBoxBattleItems.SelectedIndex, KernelWorker.BackupKernel);
            try
            {
                ToolTip(comboBoxBattleItemsMagicID,2, comboBoxBattleItemsMagicID.Items[KernelWorker.GetSelectedBattleItemsData.MagicID]);
                ToolTip(comboBoxBattleItemsAttackType,2, comboBoxBattleItemsAttackType.Items[KernelWorker.GetSelectedBattleItemsData.AttackType]);
                ToolTip(numericUpDownBattleItemsAttackPower,0, KernelWorker.GetSelectedBattleItemsData.AttackPower);
                ToolTip(checkBoxBattleItemsTarget1,1, (KernelWorker.GetSelectedBattleItemsData.Target & 0x01) >= 1 ? true : false);
                ToolTip(checkBoxBattleItemsTarget2,1, (KernelWorker.GetSelectedBattleItemsData.Target & 0x02) >= 1 ? true : false);
                ToolTip(checkBoxBattleItemsTarget3,1, (KernelWorker.GetSelectedBattleItemsData.Target & 0x04) >= 1 ? true : false);
                ToolTip(checkBoxBattleItemsTarget4,1, (KernelWorker.GetSelectedBattleItemsData.Target & 0x08) >= 1 ? true : false);
                ToolTip(checkBoxBattleItemsTarget5,1, (KernelWorker.GetSelectedBattleItemsData.Target & 0x10) >= 1 ? true : false);
                ToolTip(checkBoxBattleItemsTarget6,1, (KernelWorker.GetSelectedBattleItemsData.Target & 0x20) >= 1 ? true : false);
                ToolTip(checkBoxBattleItemsTarget7,1, (KernelWorker.GetSelectedBattleItemsData.Target & 0x40) >= 1 ? true : false);
                ToolTip(checkBoxBattleItemsTarget8,1, (KernelWorker.GetSelectedBattleItemsData.Target & 0x80) >= 1 ? true : false);
                ToolTip(checkBoxBattleItemsFlag1,1, (KernelWorker.GetSelectedBattleItemsData.AttackFlags & 0x01) >= 1 ? true : false);
                ToolTip(checkBoxBattleItemsFlag2,1, (KernelWorker.GetSelectedBattleItemsData.AttackFlags & 0x02) >= 1 ? true : false);
                ToolTip(checkBoxBattleItemsFlag3,1, (KernelWorker.GetSelectedBattleItemsData.AttackFlags & 0x04) >= 1 ? true : false);
                ToolTip(checkBoxBattleItemsFlag4,1, (KernelWorker.GetSelectedBattleItemsData.AttackFlags & 0x08) >= 1 ? true : false);
                ToolTip(checkBoxBattleItemsFlag5,1, (KernelWorker.GetSelectedBattleItemsData.AttackFlags & 0x10) >= 1 ? true : false);
                ToolTip(checkBoxBattleItemsFlag6,1, (KernelWorker.GetSelectedBattleItemsData.AttackFlags & 0x20) >= 1 ? true : false);
                ToolTip(checkBoxBattleItemsFlag7,1, (KernelWorker.GetSelectedBattleItemsData.AttackFlags & 0x40) >= 1 ? true : false);
                ToolTip(checkBoxBattleItemsFlag8,1, (KernelWorker.GetSelectedBattleItemsData.AttackFlags & 0x80) >= 1 ? true : false);
                ToolTip(numericUpDownBattleItemsStatusAttack,0, KernelWorker.GetSelectedBattleItemsData.StatusAttack);
                BattleItemsStatusWorker(true);
                ToolTip(numericUpDownBattleItemsAttackParam, 0, KernelWorker.GetSelectedBattleItemsData.AttackParam);
                ToolTip(numericUpDownBattleItemsHitCount,0, KernelWorker.GetSelectedBattleItemsData.HitCount);
                ToolTip(comboBoxBattleItemsElement,2,comboBoxBattleItemsElement.Items[BattleItems_GetElement()]);
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }

            KernelWorker.ReadBattleItems(listBoxBattleItems.SelectedIndex, KernelWorker.Kernel);
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
                BattleItemsStatusWorker(false);
                numericUpDownBattleItemsAttackParam.Value = KernelWorker.GetSelectedBattleItemsData.AttackParam;
                numericUpDownBattleItemsHitCount.Value = KernelWorker.GetSelectedBattleItemsData.HitCount;
                comboBoxBattleItemsElement.SelectedIndex = BattleItems_GetElement();
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }
            _loaded = true;
        }

        #endregion

        #region Slot array

        private void SlotArray()
        {
            _loaded = false;
            if (KernelWorker.Kernel == null || KernelWorker.BackupKernel == null)
                return;

            KernelWorker.ReadSlotArray(KernelWorker.BackupKernel);
            try
            {               
                ToolTip(numericUpDownSlotsArray1, 0, KernelWorker.GetSelectedSlotArrayData.SlotArray1);
                ToolTip(numericUpDownSlotsArray2, 0, KernelWorker.GetSelectedSlotArrayData.SlotArray1);
                ToolTip(numericUpDownSlotsArray3, 0, KernelWorker.GetSelectedSlotArrayData.SlotArray3);
                ToolTip(numericUpDownSlotsArray4, 0, KernelWorker.GetSelectedSlotArrayData.SlotArray4);
                ToolTip(numericUpDownSlotsArray5, 0, KernelWorker.GetSelectedSlotArrayData.SlotArray5);
                ToolTip(numericUpDownSlotsArray6, 0, KernelWorker.GetSelectedSlotArrayData.SlotArray6);
                ToolTip(numericUpDownSlotsArray7, 0, KernelWorker.GetSelectedSlotArrayData.SlotArray7);
                ToolTip(numericUpDownSlotsArray8, 0, KernelWorker.GetSelectedSlotArrayData.SlotArray8);
                ToolTip(numericUpDownSlotsArray9, 0, KernelWorker.GetSelectedSlotArrayData.SlotArray9);
                ToolTip(numericUpDownSlotsArray10, 0, KernelWorker.GetSelectedSlotArrayData.SlotArray10);
                ToolTip(numericUpDownSlotsArray11, 0, KernelWorker.GetSelectedSlotArrayData.SlotArray11);
                ToolTip(numericUpDownSlotsArray12, 0, KernelWorker.GetSelectedSlotArrayData.SlotArray12);
                ToolTip(numericUpDownSlotsArray13, 0, KernelWorker.GetSelectedSlotArrayData.SlotArray13);
                ToolTip(numericUpDownSlotsArray14, 0, KernelWorker.GetSelectedSlotArrayData.SlotArray14);
                ToolTip(numericUpDownSlotsArray15, 0, KernelWorker.GetSelectedSlotArrayData.SlotArray15);
                ToolTip(numericUpDownSlotsArray16, 0, KernelWorker.GetSelectedSlotArrayData.SlotArray16);
                ToolTip(numericUpDownSlotsArray17, 0, KernelWorker.GetSelectedSlotArrayData.SlotArray17);
                ToolTip(numericUpDownSlotsArray18, 0, KernelWorker.GetSelectedSlotArrayData.SlotArray18);
                ToolTip(numericUpDownSlotsArray19, 0, KernelWorker.GetSelectedSlotArrayData.SlotArray19);
                ToolTip(numericUpDownSlotsArray20, 0, KernelWorker.GetSelectedSlotArrayData.SlotArray20);
                ToolTip(numericUpDownSlotsArray21, 0, KernelWorker.GetSelectedSlotArrayData.SlotArray21);
                ToolTip(numericUpDownSlotsArray22, 0, KernelWorker.GetSelectedSlotArrayData.SlotArray22);
                ToolTip(numericUpDownSlotsArray23, 0, KernelWorker.GetSelectedSlotArrayData.SlotArray23);
                ToolTip(numericUpDownSlotsArray24, 0, KernelWorker.GetSelectedSlotArrayData.SlotArray24);
                ToolTip(numericUpDownSlotsArray25, 0, KernelWorker.GetSelectedSlotArrayData.SlotArray25);
                ToolTip(numericUpDownSlotsArray26, 0, KernelWorker.GetSelectedSlotArrayData.SlotArray26);
                ToolTip(numericUpDownSlotsArray27, 0, KernelWorker.GetSelectedSlotArrayData.SlotArray27);
                ToolTip(numericUpDownSlotsArray28, 0, KernelWorker.GetSelectedSlotArrayData.SlotArray28);
                ToolTip(numericUpDownSlotsArray29, 0, KernelWorker.GetSelectedSlotArrayData.SlotArray29);
                ToolTip(numericUpDownSlotsArray30, 0, KernelWorker.GetSelectedSlotArrayData.SlotArray30);
                ToolTip(numericUpDownSlotsArray31, 0, KernelWorker.GetSelectedSlotArrayData.SlotArray31);
                ToolTip(numericUpDownSlotsArray32, 0, KernelWorker.GetSelectedSlotArrayData.SlotArray32);
                ToolTip(numericUpDownSlotsArray33, 0, KernelWorker.GetSelectedSlotArrayData.SlotArray33);
                ToolTip(numericUpDownSlotsArray34, 0, KernelWorker.GetSelectedSlotArrayData.SlotArray34);
                ToolTip(numericUpDownSlotsArray35, 0, KernelWorker.GetSelectedSlotArrayData.SlotArray35);
                ToolTip(numericUpDownSlotsArray36, 0, KernelWorker.GetSelectedSlotArrayData.SlotArray36);
                ToolTip(numericUpDownSlotsArray37, 0, KernelWorker.GetSelectedSlotArrayData.SlotArray37);
                ToolTip(numericUpDownSlotsArray38, 0, KernelWorker.GetSelectedSlotArrayData.SlotArray38);
                ToolTip(numericUpDownSlotsArray39, 0, KernelWorker.GetSelectedSlotArrayData.SlotArray39);
                ToolTip(numericUpDownSlotsArray40, 0, KernelWorker.GetSelectedSlotArrayData.SlotArray40);
                ToolTip(numericUpDownSlotsArray41, 0, KernelWorker.GetSelectedSlotArrayData.SlotArray41);
                ToolTip(numericUpDownSlotsArray42, 0, KernelWorker.GetSelectedSlotArrayData.SlotArray42);
                ToolTip(numericUpDownSlotsArray43, 0, KernelWorker.GetSelectedSlotArrayData.SlotArray43);
                ToolTip(numericUpDownSlotsArray44, 0, KernelWorker.GetSelectedSlotArrayData.SlotArray44);
                ToolTip(numericUpDownSlotsArray45, 0, KernelWorker.GetSelectedSlotArrayData.SlotArray45);
                ToolTip(numericUpDownSlotsArray46, 0, KernelWorker.GetSelectedSlotArrayData.SlotArray46);
                ToolTip(numericUpDownSlotsArray47, 0, KernelWorker.GetSelectedSlotArrayData.SlotArray47);
                ToolTip(numericUpDownSlotsArray48, 0, KernelWorker.GetSelectedSlotArrayData.SlotArray48);
                ToolTip(numericUpDownSlotsArray49, 0, KernelWorker.GetSelectedSlotArrayData.SlotArray49);
                ToolTip(numericUpDownSlotsArray50, 0, KernelWorker.GetSelectedSlotArrayData.SlotArray50);
                ToolTip(numericUpDownSlotsArray51, 0, KernelWorker.GetSelectedSlotArrayData.SlotArray51);
                ToolTip(numericUpDownSlotsArray52, 0, KernelWorker.GetSelectedSlotArrayData.SlotArray52);
                ToolTip(numericUpDownSlotsArray53, 0, KernelWorker.GetSelectedSlotArrayData.SlotArray53);
                ToolTip(numericUpDownSlotsArray54, 0, KernelWorker.GetSelectedSlotArrayData.SlotArray54);
                ToolTip(numericUpDownSlotsArray55, 0, KernelWorker.GetSelectedSlotArrayData.SlotArray55);
                ToolTip(numericUpDownSlotsArray56, 0, KernelWorker.GetSelectedSlotArrayData.SlotArray56);
                ToolTip(numericUpDownSlotsArray57, 0, KernelWorker.GetSelectedSlotArrayData.SlotArray57);
                ToolTip(numericUpDownSlotsArray58, 0, KernelWorker.GetSelectedSlotArrayData.SlotArray58);
                ToolTip(numericUpDownSlotsArray59, 0, KernelWorker.GetSelectedSlotArrayData.SlotArray59);
                ToolTip(numericUpDownSlotsArray60, 0, KernelWorker.GetSelectedSlotArrayData.SlotArray60);
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }

            KernelWorker.ReadSlotArray(KernelWorker.Kernel);
            try
            {
                numericUpDownSlotsArray1.Value = KernelWorker.GetSelectedSlotArrayData.SlotArray1;
                numericUpDownSlotsArray2.Value = KernelWorker.GetSelectedSlotArrayData.SlotArray2;
                numericUpDownSlotsArray3.Value = KernelWorker.GetSelectedSlotArrayData.SlotArray3;
                numericUpDownSlotsArray4.Value = KernelWorker.GetSelectedSlotArrayData.SlotArray4;
                numericUpDownSlotsArray5.Value = KernelWorker.GetSelectedSlotArrayData.SlotArray5;
                numericUpDownSlotsArray6.Value = KernelWorker.GetSelectedSlotArrayData.SlotArray6;
                numericUpDownSlotsArray7.Value = KernelWorker.GetSelectedSlotArrayData.SlotArray7;
                numericUpDownSlotsArray8.Value = KernelWorker.GetSelectedSlotArrayData.SlotArray8;
                numericUpDownSlotsArray9.Value = KernelWorker.GetSelectedSlotArrayData.SlotArray9;
                numericUpDownSlotsArray10.Value = KernelWorker.GetSelectedSlotArrayData.SlotArray10;
                numericUpDownSlotsArray11.Value = KernelWorker.GetSelectedSlotArrayData.SlotArray11;
                numericUpDownSlotsArray12.Value = KernelWorker.GetSelectedSlotArrayData.SlotArray12;
                numericUpDownSlotsArray13.Value = KernelWorker.GetSelectedSlotArrayData.SlotArray13;
                numericUpDownSlotsArray14.Value = KernelWorker.GetSelectedSlotArrayData.SlotArray14;
                numericUpDownSlotsArray15.Value = KernelWorker.GetSelectedSlotArrayData.SlotArray15;
                numericUpDownSlotsArray16.Value = KernelWorker.GetSelectedSlotArrayData.SlotArray16;
                numericUpDownSlotsArray17.Value = KernelWorker.GetSelectedSlotArrayData.SlotArray17;
                numericUpDownSlotsArray18.Value = KernelWorker.GetSelectedSlotArrayData.SlotArray18;
                numericUpDownSlotsArray19.Value = KernelWorker.GetSelectedSlotArrayData.SlotArray19;
                numericUpDownSlotsArray20.Value = KernelWorker.GetSelectedSlotArrayData.SlotArray20;
                numericUpDownSlotsArray21.Value = KernelWorker.GetSelectedSlotArrayData.SlotArray21;
                numericUpDownSlotsArray22.Value = KernelWorker.GetSelectedSlotArrayData.SlotArray22;
                numericUpDownSlotsArray23.Value = KernelWorker.GetSelectedSlotArrayData.SlotArray23;
                numericUpDownSlotsArray24.Value = KernelWorker.GetSelectedSlotArrayData.SlotArray24;
                numericUpDownSlotsArray25.Value = KernelWorker.GetSelectedSlotArrayData.SlotArray25;
                numericUpDownSlotsArray26.Value = KernelWorker.GetSelectedSlotArrayData.SlotArray26;
                numericUpDownSlotsArray27.Value = KernelWorker.GetSelectedSlotArrayData.SlotArray27;
                numericUpDownSlotsArray28.Value = KernelWorker.GetSelectedSlotArrayData.SlotArray28;
                numericUpDownSlotsArray29.Value = KernelWorker.GetSelectedSlotArrayData.SlotArray29;
                numericUpDownSlotsArray30.Value = KernelWorker.GetSelectedSlotArrayData.SlotArray30;
                numericUpDownSlotsArray31.Value = KernelWorker.GetSelectedSlotArrayData.SlotArray31;
                numericUpDownSlotsArray32.Value = KernelWorker.GetSelectedSlotArrayData.SlotArray32;
                numericUpDownSlotsArray33.Value = KernelWorker.GetSelectedSlotArrayData.SlotArray33;
                numericUpDownSlotsArray34.Value = KernelWorker.GetSelectedSlotArrayData.SlotArray34;
                numericUpDownSlotsArray35.Value = KernelWorker.GetSelectedSlotArrayData.SlotArray35;
                numericUpDownSlotsArray36.Value = KernelWorker.GetSelectedSlotArrayData.SlotArray36;
                numericUpDownSlotsArray37.Value = KernelWorker.GetSelectedSlotArrayData.SlotArray37;
                numericUpDownSlotsArray38.Value = KernelWorker.GetSelectedSlotArrayData.SlotArray38;
                numericUpDownSlotsArray39.Value = KernelWorker.GetSelectedSlotArrayData.SlotArray39;
                numericUpDownSlotsArray40.Value = KernelWorker.GetSelectedSlotArrayData.SlotArray40;
                numericUpDownSlotsArray41.Value = KernelWorker.GetSelectedSlotArrayData.SlotArray41;
                numericUpDownSlotsArray42.Value = KernelWorker.GetSelectedSlotArrayData.SlotArray42;
                numericUpDownSlotsArray43.Value = KernelWorker.GetSelectedSlotArrayData.SlotArray43;
                numericUpDownSlotsArray44.Value = KernelWorker.GetSelectedSlotArrayData.SlotArray44;
                numericUpDownSlotsArray45.Value = KernelWorker.GetSelectedSlotArrayData.SlotArray45;
                numericUpDownSlotsArray46.Value = KernelWorker.GetSelectedSlotArrayData.SlotArray46;
                numericUpDownSlotsArray47.Value = KernelWorker.GetSelectedSlotArrayData.SlotArray47;
                numericUpDownSlotsArray48.Value = KernelWorker.GetSelectedSlotArrayData.SlotArray48;
                numericUpDownSlotsArray49.Value = KernelWorker.GetSelectedSlotArrayData.SlotArray49;
                numericUpDownSlotsArray50.Value = KernelWorker.GetSelectedSlotArrayData.SlotArray50;
                numericUpDownSlotsArray51.Value = KernelWorker.GetSelectedSlotArrayData.SlotArray51;
                numericUpDownSlotsArray52.Value = KernelWorker.GetSelectedSlotArrayData.SlotArray52;
                numericUpDownSlotsArray53.Value = KernelWorker.GetSelectedSlotArrayData.SlotArray53;
                numericUpDownSlotsArray54.Value = KernelWorker.GetSelectedSlotArrayData.SlotArray54;
                numericUpDownSlotsArray55.Value = KernelWorker.GetSelectedSlotArrayData.SlotArray55;
                numericUpDownSlotsArray56.Value = KernelWorker.GetSelectedSlotArrayData.SlotArray56;
                numericUpDownSlotsArray57.Value = KernelWorker.GetSelectedSlotArrayData.SlotArray57;
                numericUpDownSlotsArray58.Value = KernelWorker.GetSelectedSlotArrayData.SlotArray58;
                numericUpDownSlotsArray59.Value = KernelWorker.GetSelectedSlotArrayData.SlotArray59;
                numericUpDownSlotsArray60.Value = KernelWorker.GetSelectedSlotArrayData.SlotArray60;
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }
            _loaded = true;
        }

        #endregion

        #region Slot sets

        private void listBoxSlotsSets_SelectedIndexChanged(object sender, EventArgs e)
        {
            _loaded = false;
            if (KernelWorker.Kernel == null || KernelWorker.BackupKernel == null)
                return;

            KernelWorker.ReadSlotsSets(listBoxSlotsSets.SelectedIndex, KernelWorker.BackupKernel);
            try
            {
                ToolTip(comboBoxSlotsMagic1, _bp_string,comboBoxSlotsMagic1.Items[KernelWorker.GetSelectedSlotsSetsData.Magic1]);
                ToolTip(comboBoxSlotsMagic2, _bp_string, comboBoxSlotsMagic1.Items[KernelWorker.GetSelectedSlotsSetsData.Magic2]);
                ToolTip(comboBoxSlotsMagic3, _bp_string, comboBoxSlotsMagic1.Items[KernelWorker.GetSelectedSlotsSetsData.Magic3]);
                ToolTip(comboBoxSlotsMagic4, _bp_string, comboBoxSlotsMagic1.Items[KernelWorker.GetSelectedSlotsSetsData.Magic4]);
                ToolTip(comboBoxSlotsMagic5, _bp_string, comboBoxSlotsMagic1.Items[KernelWorker.GetSelectedSlotsSetsData.Magic5]);
                ToolTip(comboBoxSlotsMagic6, _bp_string, comboBoxSlotsMagic1.Items[KernelWorker.GetSelectedSlotsSetsData.Magic6]);
                ToolTip(comboBoxSlotsMagic7, _bp_string, comboBoxSlotsMagic1.Items[KernelWorker.GetSelectedSlotsSetsData.Magic7]);
                ToolTip(comboBoxSlotsMagic8, _bp_string, comboBoxSlotsMagic1.Items[KernelWorker.GetSelectedSlotsSetsData.Magic8]);
                ToolTip(numericUpDownSlotsCount1,0x00,KernelWorker.GetSelectedSlotsSetsData.Count1);
                ToolTip(numericUpDownSlotsCount2, 0x00, KernelWorker.GetSelectedSlotsSetsData.Count2);
                ToolTip(numericUpDownSlotsCount3, 0x00, KernelWorker.GetSelectedSlotsSetsData.Count3);
                ToolTip(numericUpDownSlotsCount4, 0x00, KernelWorker.GetSelectedSlotsSetsData.Count4);
                ToolTip(numericUpDownSlotsCount5, 0x00, KernelWorker.GetSelectedSlotsSetsData.Count5);
                ToolTip(numericUpDownSlotsCount6, 0x00, KernelWorker.GetSelectedSlotsSetsData.Count6);
                ToolTip(numericUpDownSlotsCount7, 0x00, KernelWorker.GetSelectedSlotsSetsData.Count7);
                ToolTip(numericUpDownSlotsCount8, 0x00, KernelWorker.GetSelectedSlotsSetsData.Count8);
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }

            KernelWorker.ReadSlotsSets(listBoxSlotsSets.SelectedIndex, KernelWorker.Kernel);
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
                MessageBox.Show(Exception.ToString());
            }
            _loaded = true;
        }

        #endregion

        #region Devour     

        private int Devour_GetHealDmg()
        {
           return KernelWorker.GetSelectedDevourData.HealDmg == KernelWorker.HealDmg.Heal ? 0
                : KernelWorker.GetSelectedDevourData.HealDmg == KernelWorker.HealDmg.Damage ? 1
                : 0;
        }

        private byte Devour_GetHealDmg(int Index)
        {
            byte healDmg = (byte)(Index == 0 ? (byte)KernelWorker.HealDmg.Heal :
                Index == 1 ? (byte)KernelWorker.HealDmg.Damage :
                0x00 /*ErrorHandler*/);
            return healDmg;
        }

        private void DevourStatusWorker(bool bBackup)
        {
            if (!bBackup)
            {
                checkBoxDevourDeath.Checked = (KernelWorker.GetSelectedDevourData.Status1 & 0x01) >= 1 ? true : false;
                checkBoxDevourPoison.Checked = (KernelWorker.GetSelectedDevourData.Status1 & 0x02) >= 1 ? true : false;
                checkBoxDevourPetrify.Checked = (KernelWorker.GetSelectedDevourData.Status1 & 0x04) >= 1 ? true : false;
                checkBoxDevourDarkness.Checked = (KernelWorker.GetSelectedDevourData.Status1 & 0x08) >= 1 ? true : false;
                checkBoxDevourSilence.Checked = (KernelWorker.GetSelectedDevourData.Status1 & 0x10) >= 1 ? true : false;
                checkBoxDevourBerserk.Checked = (KernelWorker.GetSelectedDevourData.Status1 & 0x20) >= 1 ? true : false;
                checkBoxDevourZombie.Checked = (KernelWorker.GetSelectedDevourData.Status1 & 0x40) >= 1 ? true : false;
                checkBoxDevourUnk7.Checked = (KernelWorker.GetSelectedDevourData.Status1 & 0x80) >= 1 ? true : false;

                checkBoxDevourSleep.Checked = (KernelWorker.GetSelectedDevourData.Status2 & 0x01) >= 1 ? true : false;
                checkBoxDevourHaste.Checked = (KernelWorker.GetSelectedDevourData.Status2 & 0x02) >= 1 ? true : false;
                checkBoxDevourSlow.Checked = (KernelWorker.GetSelectedDevourData.Status2 & 0x04) >= 1 ? true : false;
                checkBoxDevourStop.Checked = (KernelWorker.GetSelectedDevourData.Status2 & 0x08) >= 1 ? true : false;
                checkBoxDevourRegen.Checked = (KernelWorker.GetSelectedDevourData.Status2 & 0x10) >= 1 ? true : false;
                checkBoxDevourProtect.Checked = (KernelWorker.GetSelectedDevourData.Status2 & 0x20) >= 1 ? true : false;
                checkBoxDevourShell.Checked = (KernelWorker.GetSelectedDevourData.Status2 & 0x40) >= 1 ? true : false;
                checkBoxDevourReflect.Checked = (KernelWorker.GetSelectedDevourData.Status2 & 0x80) >= 1 ? true : false;

                checkBoxDevourAura.Checked = (KernelWorker.GetSelectedDevourData.Status3 & 0x01) >= 1 ? true : false;
                checkBoxDevourCurse.Checked = (KernelWorker.GetSelectedDevourData.Status3 & 0x02) >= 1 ? true : false;
                checkBoxDevourDoom.Checked = (KernelWorker.GetSelectedDevourData.Status3 & 0x04) >= 1 ? true : false;
                checkBoxDevourInvincible.Checked = (KernelWorker.GetSelectedDevourData.Status3 & 0x08) >= 1 ? true : false;
                checkBoxDevourPetrifying.Checked = (KernelWorker.GetSelectedDevourData.Status3 & 0x10) >= 1 ? true : false;
                checkBoxDevourFloat.Checked = (KernelWorker.GetSelectedDevourData.Status3 & 0x20) >= 1 ? true : false;
                checkBoxDevourConfusion.Checked = (KernelWorker.GetSelectedDevourData.Status3 & 0x40) >= 1 ? true : false;
                checkBoxDevourDrain.Checked = (KernelWorker.GetSelectedDevourData.Status3 & 0x80) >= 1 ? true : false;

                checkBoxDevourEject.Checked = (KernelWorker.GetSelectedDevourData.Status4 & 0x01) >= 1 ? true : false;
                checkBoxDevourDouble.Checked = (KernelWorker.GetSelectedDevourData.Status4 & 0x02) >= 1 ? true : false;
                checkBoxDevourTriple.Checked = (KernelWorker.GetSelectedDevourData.Status4 & 0x04) >= 1 ? true : false;
                checkBoxDevourDefend.Checked = (KernelWorker.GetSelectedDevourData.Status4 & 0x08) >= 1 ? true : false;
                checkBoxDevourUnk1.Checked = (KernelWorker.GetSelectedDevourData.Status4 & 0x10) >= 1 ? true : false;
                checkBoxDevourUnk2.Checked = (KernelWorker.GetSelectedDevourData.Status4 & 0x20) >= 1 ? true : false;
                checkBoxDevourCharged.Checked = (KernelWorker.GetSelectedDevourData.Status4 & 0x40) >= 1 ? true : false;
                checkBoxDevourBackAttack.Checked = (KernelWorker.GetSelectedDevourData.Status4 & 0x80) >= 1 ? true : false;

                checkBoxDevourVit0.Checked = (KernelWorker.GetSelectedDevourData.Status5 & 0x01) >= 1 ? true : false;
                checkBoxDevourAngelWing.Checked = (KernelWorker.GetSelectedDevourData.Status5 & 0x02) >= 1 ? true : false;
                checkBoxDevourUnk3.Checked = (KernelWorker.GetSelectedDevourData.Status5 & 0x04) >= 1 ? true : false;
                checkBoxDevourUnk4.Checked = (KernelWorker.GetSelectedDevourData.Status5 & 0x08) >= 1 ? true : false;
                checkBoxDevourUnk5.Checked = (KernelWorker.GetSelectedDevourData.Status5 & 0x10) >= 1 ? true : false;
                checkBoxDevourUnk6.Checked = (KernelWorker.GetSelectedDevourData.Status5 & 0x20) >= 1 ? true : false;
                checkBoxDevourHasMagic.Checked = (KernelWorker.GetSelectedDevourData.Status5 & 0x40) >= 1 ? true : false;
                checkBoxDevourSummonGF.Checked = (KernelWorker.GetSelectedDevourData.Status5 & 0x80) >= 1 ? true : false;
                return;
            }
            ToolTip(checkBoxDevourDeath,1,(KernelWorker.GetSelectedDevourData.Status1 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxDevourPoison,1,(KernelWorker.GetSelectedDevourData.Status1 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxDevourPetrify,1,(KernelWorker.GetSelectedDevourData.Status1 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxDevourDarkness,1,(KernelWorker.GetSelectedDevourData.Status1 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxDevourSilence,1,(KernelWorker.GetSelectedDevourData.Status1 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxDevourBerserk,1,(KernelWorker.GetSelectedDevourData.Status1 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxDevourZombie,1,(KernelWorker.GetSelectedDevourData.Status1 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxDevourUnk7,1,(KernelWorker.GetSelectedDevourData.Status1 & 0x80) >= 1 ? true : false);

            ToolTip(checkBoxDevourSleep,1,(KernelWorker.GetSelectedDevourData.Status2 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxDevourHaste,1,(KernelWorker.GetSelectedDevourData.Status2 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxDevourSlow,1,(KernelWorker.GetSelectedDevourData.Status2 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxDevourStop,1,(KernelWorker.GetSelectedDevourData.Status2 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxDevourRegen,1,(KernelWorker.GetSelectedDevourData.Status2 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxDevourProtect,1,(KernelWorker.GetSelectedDevourData.Status2 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxDevourShell,1,(KernelWorker.GetSelectedDevourData.Status2 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxDevourReflect,1,(KernelWorker.GetSelectedDevourData.Status2 & 0x80) >= 1 ? true : false);

            ToolTip(checkBoxDevourAura,1,(KernelWorker.GetSelectedDevourData.Status3 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxDevourCurse,1,(KernelWorker.GetSelectedDevourData.Status3 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxDevourDoom,1,(KernelWorker.GetSelectedDevourData.Status3 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxDevourInvincible,1,(KernelWorker.GetSelectedDevourData.Status3 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxDevourPetrifying,1,(KernelWorker.GetSelectedDevourData.Status3 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxDevourFloat,1,(KernelWorker.GetSelectedDevourData.Status3 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxDevourConfusion,1,(KernelWorker.GetSelectedDevourData.Status3 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxDevourDrain,1,(KernelWorker.GetSelectedDevourData.Status3 & 0x80) >= 1 ? true : false);

            ToolTip(checkBoxDevourEject,1,(KernelWorker.GetSelectedDevourData.Status4 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxDevourDouble,1,(KernelWorker.GetSelectedDevourData.Status4 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxDevourTriple,1,(KernelWorker.GetSelectedDevourData.Status4 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxDevourDefend,1,(KernelWorker.GetSelectedDevourData.Status4 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxDevourUnk1,1,(KernelWorker.GetSelectedDevourData.Status4 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxDevourUnk2,1,(KernelWorker.GetSelectedDevourData.Status4 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxDevourCharged,1,(KernelWorker.GetSelectedDevourData.Status4 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxDevourBackAttack,1,(KernelWorker.GetSelectedDevourData.Status4 & 0x80) >= 1 ? true : false);

            ToolTip(checkBoxDevourVit0,1,(KernelWorker.GetSelectedDevourData.Status5 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxDevourAngelWing,1,(KernelWorker.GetSelectedDevourData.Status5 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxDevourUnk3,1,(KernelWorker.GetSelectedDevourData.Status5 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxDevourUnk4,1,(KernelWorker.GetSelectedDevourData.Status5 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxDevourUnk5,1,(KernelWorker.GetSelectedDevourData.Status5 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxDevourUnk6,1,(KernelWorker.GetSelectedDevourData.Status5 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxDevourHasMagic,1,(KernelWorker.GetSelectedDevourData.Status5 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxDevourSummonGF,1,(KernelWorker.GetSelectedDevourData.Status5 & 0x80) >= 1 ? true : false);
        }

        private void listBoxDevour_SelectedIndexChanged(object sender, EventArgs e)
        {
            _loaded = false;
            if (KernelWorker.Kernel == null || KernelWorker.BackupKernel == null)
                return;

            KernelWorker.ReadDevour(listBoxDevour.SelectedIndex, KernelWorker.BackupKernel);
            try
            {
                ToolTip(comboBoxDevourHealDmg,2, comboBoxDevourHealDmg.Items[Devour_GetHealDmg()]);
                ToolTip(checkBoxDevourHP1,1, (KernelWorker.GetSelectedDevourData.HpQuantity & 0x01) >= 1 ? true : false);
                ToolTip(checkBoxDevourHP2,1, (KernelWorker.GetSelectedDevourData.HpQuantity & 0x02) >= 1 ? true : false);
                ToolTip(checkBoxDevourHP3,1, (KernelWorker.GetSelectedDevourData.HpQuantity & 0x04) >= 1 ? true : false);
                ToolTip(checkBoxDevourHP4,1, (KernelWorker.GetSelectedDevourData.HpQuantity & 0x08) >= 1 ? true : false);
                ToolTip(checkBoxDevourHP5,1, (KernelWorker.GetSelectedDevourData.HpQuantity & 0x10) >= 1 ? true : false);
                DevourStatusWorker(true);
                ToolTip(checkBoxDevourStat1,1, (KernelWorker.GetSelectedDevourData.RaisedStat & 0x01) >= 1 ? true : false);
                ToolTip(checkBoxDevourStat2,1, (KernelWorker.GetSelectedDevourData.RaisedStat & 0x02) >= 1 ? true : false);
                ToolTip(checkBoxDevourStat3,1, (KernelWorker.GetSelectedDevourData.RaisedStat & 0x04) >= 1 ? true : false);
                ToolTip(checkBoxDevourStat4,1, (KernelWorker.GetSelectedDevourData.RaisedStat & 0x08) >= 1 ? true : false);
                ToolTip(checkBoxDevourStat5,1, (KernelWorker.GetSelectedDevourData.RaisedStat & 0x10) >= 1 ? true : false);
                ToolTip(checkBoxDevourStat6,1, (KernelWorker.GetSelectedDevourData.RaisedStat & 0x20) >= 1 ? true : false);
                ToolTip(numericUpDownDevourHP,0,KernelWorker.GetSelectedDevourData.RaisedHP);
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }

            KernelWorker.ReadDevour(listBoxDevour.SelectedIndex, KernelWorker.Kernel);
            try
            {
                comboBoxDevourHealDmg.SelectedIndex = Devour_GetHealDmg();
                checkBoxDevourHP1.Checked = (KernelWorker.GetSelectedDevourData.HpQuantity & 0x01) >= 1 ? true : false;
                checkBoxDevourHP2.Checked = (KernelWorker.GetSelectedDevourData.HpQuantity & 0x02) >= 1 ? true : false;
                checkBoxDevourHP3.Checked = (KernelWorker.GetSelectedDevourData.HpQuantity & 0x04) >= 1 ? true : false;
                checkBoxDevourHP4.Checked = (KernelWorker.GetSelectedDevourData.HpQuantity & 0x08) >= 1 ? true : false;
                checkBoxDevourHP5.Checked = (KernelWorker.GetSelectedDevourData.HpQuantity & 0x10) >= 1 ? true : false;
                DevourStatusWorker(false);
                checkBoxDevourStat1.Checked = (KernelWorker.GetSelectedDevourData.RaisedStat & 0x01) >= 1 ? true : false;
                checkBoxDevourStat2.Checked = (KernelWorker.GetSelectedDevourData.RaisedStat & 0x02) >= 1 ? true : false;
                checkBoxDevourStat3.Checked = (KernelWorker.GetSelectedDevourData.RaisedStat & 0x04) >= 1 ? true : false;
                checkBoxDevourStat4.Checked = (KernelWorker.GetSelectedDevourData.RaisedStat & 0x08) >= 1 ? true : false;
                checkBoxDevourStat5.Checked = (KernelWorker.GetSelectedDevourData.RaisedStat & 0x10) >= 1 ? true : false;
                checkBoxDevourStat6.Checked = (KernelWorker.GetSelectedDevourData.RaisedStat & 0x20) >= 1 ? true : false;
                numericUpDownDevourHP.Value = KernelWorker.GetSelectedDevourData.RaisedHP;
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }
            _loaded = true;
        }

        #endregion

        #region Misc

        private void Misc()
        {
            _loaded = false;
            if (KernelWorker.Kernel == null || KernelWorker.BackupKernel == null)
                return;

            KernelWorker.ReadMisc(KernelWorker.BackupKernel);
            try
            {
                ToolTip(numericUpDownStatusTimer1,0,KernelWorker.GetSelectedMiscData.StatusTimer1);
                ToolTip(numericUpDownStatusTimer2, 0, KernelWorker.GetSelectedMiscData.StatusTimer2);
                ToolTip(numericUpDownStatusTimer3, 0, KernelWorker.GetSelectedMiscData.StatusTimer3);
                ToolTip(numericUpDownStatusTimer4, 0, KernelWorker.GetSelectedMiscData.StatusTimer4);
                ToolTip(numericUpDownStatusTimer5, 0, KernelWorker.GetSelectedMiscData.StatusTimer5);
                ToolTip(numericUpDownStatusTimer6, 0, KernelWorker.GetSelectedMiscData.StatusTimer6);
                ToolTip(numericUpDownStatusTimer7, 0, KernelWorker.GetSelectedMiscData.StatusTimer7);
                ToolTip(numericUpDownStatusTimer8, 0, KernelWorker.GetSelectedMiscData.StatusTimer8);
                ToolTip(numericUpDownStatusTimer9, 0, KernelWorker.GetSelectedMiscData.StatusTimer9);
                ToolTip(numericUpDownStatusTimer10, 0, KernelWorker.GetSelectedMiscData.StatusTimer10);
                ToolTip(numericUpDownStatusTimer11, 0, KernelWorker.GetSelectedMiscData.StatusTimer11);
                ToolTip(numericUpDownStatusTimer12, 0, KernelWorker.GetSelectedMiscData.StatusTimer12);
                ToolTip(numericUpDownStatusTimer13, 0, KernelWorker.GetSelectedMiscData.StatusTimer13);
                ToolTip(numericUpDownStatusTimer14, 0, KernelWorker.GetSelectedMiscData.StatusTimer14);
                ToolTip(numericUpDownATBMult,0,KernelWorker.GetSelectedMiscData.ATB);
                ToolTip(numericUpDownDeadTimer,0,KernelWorker.GetSelectedMiscData.DeadTimer);
                ToolTip(numericUpDownStatusLimit1,0,KernelWorker.GetSelectedMiscData.StatusLimit1);
                ToolTip(numericUpDownStatusLimit2, 0, KernelWorker.GetSelectedMiscData.StatusLimit2);
                ToolTip(numericUpDownStatusLimit3, 0, KernelWorker.GetSelectedMiscData.StatusLimit3);
                ToolTip(numericUpDownStatusLimit4, 0, KernelWorker.GetSelectedMiscData.StatusLimit4);
                ToolTip(numericUpDownStatusLimit5, 0, KernelWorker.GetSelectedMiscData.StatusLimit5);
                ToolTip(numericUpDownStatusLimit6, 0, KernelWorker.GetSelectedMiscData.StatusLimit6);
                ToolTip(numericUpDownStatusLimit7, 0, KernelWorker.GetSelectedMiscData.StatusLimit7);
                ToolTip(numericUpDownStatusLimit8, 0, KernelWorker.GetSelectedMiscData.StatusLimit8);
                ToolTip(numericUpDownStatusLimit9, 0, KernelWorker.GetSelectedMiscData.StatusLimit9);
                ToolTip(numericUpDownStatusLimit10, 0, KernelWorker.GetSelectedMiscData.StatusLimit10);
                ToolTip(numericUpDownStatusLimit11, 0, KernelWorker.GetSelectedMiscData.StatusLimit11);
                ToolTip(numericUpDownStatusLimit12, 0, KernelWorker.GetSelectedMiscData.StatusLimit12);
                ToolTip(numericUpDownStatusLimit13, 0, KernelWorker.GetSelectedMiscData.StatusLimit13);
                ToolTip(numericUpDownStatusLimit14, 0, KernelWorker.GetSelectedMiscData.StatusLimit14);
                ToolTip(numericUpDownStatusLimit15, 0, KernelWorker.GetSelectedMiscData.StatusLimit15);
                ToolTip(numericUpDownStatusLimit16, 0, KernelWorker.GetSelectedMiscData.StatusLimit16);
                ToolTip(numericUpDownStatusLimit17, 0, KernelWorker.GetSelectedMiscData.StatusLimit17);
                ToolTip(numericUpDownStatusLimit18, 0, KernelWorker.GetSelectedMiscData.StatusLimit18);
                ToolTip(numericUpDownStatusLimit19, 0, KernelWorker.GetSelectedMiscData.StatusLimit19);
                ToolTip(numericUpDownStatusLimit20, 0, KernelWorker.GetSelectedMiscData.StatusLimit20);
                ToolTip(numericUpDownStatusLimit21, 0, KernelWorker.GetSelectedMiscData.StatusLimit21);
                ToolTip(numericUpDownStatusLimit22, 0, KernelWorker.GetSelectedMiscData.StatusLimit22);
                ToolTip(numericUpDownStatusLimit23, 0, KernelWorker.GetSelectedMiscData.StatusLimit23);
                ToolTip(numericUpDownStatusLimit24, 0, KernelWorker.GetSelectedMiscData.StatusLimit24);
                ToolTip(numericUpDownStatusLimit25, 0, KernelWorker.GetSelectedMiscData.StatusLimit25);
                ToolTip(numericUpDownStatusLimit26, 0, KernelWorker.GetSelectedMiscData.StatusLimit26);
                ToolTip(numericUpDownStatusLimit27, 0, KernelWorker.GetSelectedMiscData.StatusLimit27);
                ToolTip(numericUpDownStatusLimit28, 0, KernelWorker.GetSelectedMiscData.StatusLimit28);
                ToolTip(numericUpDownStatusLimit29, 0, KernelWorker.GetSelectedMiscData.StatusLimit29);
                ToolTip(numericUpDownStatusLimit30, 0, KernelWorker.GetSelectedMiscData.StatusLimit30);
                ToolTip(numericUpDownStatusLimit31, 0, KernelWorker.GetSelectedMiscData.StatusLimit31);
                ToolTip(numericUpDownStatusLimit32, 0, KernelWorker.GetSelectedMiscData.StatusLimit32);
                ToolTip(numericUpDownDuelTimer1,0,KernelWorker.GetSelectedMiscData.DuelTimerCL1);
                ToolTip(numericUpDownDuelTimer2, 0, KernelWorker.GetSelectedMiscData.DuelTimerCL2);
                ToolTip(numericUpDownDuelTimer3, 0, KernelWorker.GetSelectedMiscData.DuelTimerCL3);
                ToolTip(numericUpDownDuelTimer4, 0, KernelWorker.GetSelectedMiscData.DuelTimerCL4);
                ToolTip(numericUpDownDuelStart1,0,KernelWorker.GetSelectedMiscData.DuelSeqCL1);
                ToolTip(numericUpDownDuelStart2, 0, KernelWorker.GetSelectedMiscData.DuelSeqCL2);
                ToolTip(numericUpDownDuelStart3, 0, KernelWorker.GetSelectedMiscData.DuelSeqCL3);
                ToolTip(numericUpDownDuelStart4, 0, KernelWorker.GetSelectedMiscData.DuelSeqCL4);
                ToolTip(numericUpDownShotTimer1,0,KernelWorker.GetSelectedMiscData.ShotTimerCL1);
                ToolTip(numericUpDownShotTimer2, 0, KernelWorker.GetSelectedMiscData.ShotTimerCL2);
                ToolTip(numericUpDownShotTimer3, 0, KernelWorker.GetSelectedMiscData.ShotTimerCL3);
                ToolTip(numericUpDownShotTimer4, 0, KernelWorker.GetSelectedMiscData.ShotTimerCL4);
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }

            KernelWorker.ReadMisc(KernelWorker.Kernel);
            try
            {
                numericUpDownStatusTimer1.Value = KernelWorker.GetSelectedMiscData.StatusTimer1;
                numericUpDownStatusTimer2.Value = KernelWorker.GetSelectedMiscData.StatusTimer2;
                numericUpDownStatusTimer3.Value = KernelWorker.GetSelectedMiscData.StatusTimer3;
                numericUpDownStatusTimer4.Value = KernelWorker.GetSelectedMiscData.StatusTimer4;
                numericUpDownStatusTimer5.Value = KernelWorker.GetSelectedMiscData.StatusTimer5;
                numericUpDownStatusTimer6.Value = KernelWorker.GetSelectedMiscData.StatusTimer6;
                numericUpDownStatusTimer7.Value = KernelWorker.GetSelectedMiscData.StatusTimer7;
                numericUpDownStatusTimer8.Value = KernelWorker.GetSelectedMiscData.StatusTimer8;
                numericUpDownStatusTimer9.Value = KernelWorker.GetSelectedMiscData.StatusTimer9;
                numericUpDownStatusTimer10.Value = KernelWorker.GetSelectedMiscData.StatusTimer10;
                numericUpDownStatusTimer11.Value = KernelWorker.GetSelectedMiscData.StatusTimer11;
                numericUpDownStatusTimer12.Value = KernelWorker.GetSelectedMiscData.StatusTimer12;
                numericUpDownStatusTimer13.Value = KernelWorker.GetSelectedMiscData.StatusTimer13;
                numericUpDownStatusTimer14.Value = KernelWorker.GetSelectedMiscData.StatusTimer14;
                numericUpDownATBMult.Value = KernelWorker.GetSelectedMiscData.ATB;
                numericUpDownDeadTimer.Value = KernelWorker.GetSelectedMiscData.DeadTimer;
                numericUpDownStatusLimit1.Value = KernelWorker.GetSelectedMiscData.StatusLimit1;
                numericUpDownStatusLimit2.Value = KernelWorker.GetSelectedMiscData.StatusLimit2;
                numericUpDownStatusLimit3.Value = KernelWorker.GetSelectedMiscData.StatusLimit3;
                numericUpDownStatusLimit4.Value = KernelWorker.GetSelectedMiscData.StatusLimit4;
                numericUpDownStatusLimit5.Value = KernelWorker.GetSelectedMiscData.StatusLimit5;
                numericUpDownStatusLimit6.Value = KernelWorker.GetSelectedMiscData.StatusLimit6;
                numericUpDownStatusLimit7.Value = KernelWorker.GetSelectedMiscData.StatusLimit7;
                numericUpDownStatusLimit8.Value = KernelWorker.GetSelectedMiscData.StatusLimit8;
                numericUpDownStatusLimit9.Value = KernelWorker.GetSelectedMiscData.StatusLimit9;
                numericUpDownStatusLimit10.Value = KernelWorker.GetSelectedMiscData.StatusLimit10;
                numericUpDownStatusLimit11.Value = KernelWorker.GetSelectedMiscData.StatusLimit11;
                numericUpDownStatusLimit12.Value = KernelWorker.GetSelectedMiscData.StatusLimit12;
                numericUpDownStatusLimit13.Value = KernelWorker.GetSelectedMiscData.StatusLimit13;
                numericUpDownStatusLimit14.Value = KernelWorker.GetSelectedMiscData.StatusLimit14;
                numericUpDownStatusLimit15.Value = KernelWorker.GetSelectedMiscData.StatusLimit15;
                numericUpDownStatusLimit16.Value = KernelWorker.GetSelectedMiscData.StatusLimit16;
                numericUpDownStatusLimit17.Value = KernelWorker.GetSelectedMiscData.StatusLimit17;
                numericUpDownStatusLimit18.Value = KernelWorker.GetSelectedMiscData.StatusLimit18;
                numericUpDownStatusLimit19.Value = KernelWorker.GetSelectedMiscData.StatusLimit19;
                numericUpDownStatusLimit20.Value = KernelWorker.GetSelectedMiscData.StatusLimit20;
                numericUpDownStatusLimit21.Value = KernelWorker.GetSelectedMiscData.StatusLimit21;
                numericUpDownStatusLimit22.Value = KernelWorker.GetSelectedMiscData.StatusLimit22;
                numericUpDownStatusLimit23.Value = KernelWorker.GetSelectedMiscData.StatusLimit23;
                numericUpDownStatusLimit24.Value = KernelWorker.GetSelectedMiscData.StatusLimit24;
                numericUpDownStatusLimit25.Value = KernelWorker.GetSelectedMiscData.StatusLimit25;
                numericUpDownStatusLimit26.Value = KernelWorker.GetSelectedMiscData.StatusLimit26;
                numericUpDownStatusLimit27.Value = KernelWorker.GetSelectedMiscData.StatusLimit27;
                numericUpDownStatusLimit28.Value = KernelWorker.GetSelectedMiscData.StatusLimit28;
                numericUpDownStatusLimit29.Value = KernelWorker.GetSelectedMiscData.StatusLimit29;
                numericUpDownStatusLimit30.Value = KernelWorker.GetSelectedMiscData.StatusLimit30;
                numericUpDownStatusLimit31.Value = KernelWorker.GetSelectedMiscData.StatusLimit31;
                numericUpDownStatusLimit32.Value = KernelWorker.GetSelectedMiscData.StatusLimit32;
                numericUpDownDuelTimer1.Value = KernelWorker.GetSelectedMiscData.DuelTimerCL1;
                numericUpDownDuelTimer2.Value = KernelWorker.GetSelectedMiscData.DuelTimerCL2;
                numericUpDownDuelTimer3.Value = KernelWorker.GetSelectedMiscData.DuelTimerCL3;
                numericUpDownDuelTimer4.Value = KernelWorker.GetSelectedMiscData.DuelTimerCL4;
                numericUpDownDuelStart1.Value = KernelWorker.GetSelectedMiscData.DuelSeqCL1;
                numericUpDownDuelStart2.Value = KernelWorker.GetSelectedMiscData.DuelSeqCL2;
                numericUpDownDuelStart3.Value = KernelWorker.GetSelectedMiscData.DuelSeqCL3;
                numericUpDownDuelStart4.Value = KernelWorker.GetSelectedMiscData.DuelSeqCL4;
                numericUpDownShotTimer1.Value = KernelWorker.GetSelectedMiscData.ShotTimerCL1;
                numericUpDownShotTimer2.Value = KernelWorker.GetSelectedMiscData.ShotTimerCL2;
                numericUpDownShotTimer3.Value = KernelWorker.GetSelectedMiscData.ShotTimerCL3;
                numericUpDownShotTimer4.Value = KernelWorker.GetSelectedMiscData.ShotTimerCL4;
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }
            _loaded = true;
        }

        #endregion

        #region Command abilities data

        private int CommandAbilityData_GetElement()
        {
            return KernelWorker.GetSelectedCommandAbilityDataData.Element == KernelWorker.Element.Fire
                        ? 0
                        : KernelWorker.GetSelectedCommandAbilityDataData.Element == KernelWorker.Element.Ice
                            ? 1
                            : KernelWorker.GetSelectedCommandAbilityDataData.Element == KernelWorker.Element.Thunder
                                ? 2
                                : KernelWorker.GetSelectedCommandAbilityDataData.Element == KernelWorker.Element.Earth
                                    ? 3
                                    : KernelWorker.GetSelectedCommandAbilityDataData.Element == KernelWorker.Element.Poison
                                        ? 4
                                        : KernelWorker.GetSelectedCommandAbilityDataData.Element == KernelWorker.Element.Wind
                                            ? 5
                                            : KernelWorker.GetSelectedCommandAbilityDataData.Element ==
                                              KernelWorker.Element.Water
                                                ? 6
                                                : KernelWorker.GetSelectedCommandAbilityDataData.Element ==
                                                  KernelWorker.Element.Holy
                                                    ? 7
                                                    : KernelWorker.GetSelectedCommandAbilityDataData.Element ==
                                                      KernelWorker.Element.NonElemental
                                                        ? comboBoxAbComDataElement.Items.Count - 1
                                                        : 0;
        }

        private byte CommandAbilityData_GetElement(int Index)
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

        private void CommandAbilityDataStatusWorker(bool bBackup)
        {
            if (!bBackup)
            {
                checkBoxAbComDataDeath.Checked = (KernelWorker.GetSelectedCommandAbilityDataData.Status1 & 0x01) >= 1 ? true : false;
                checkBoxAbComDataPoison.Checked = (KernelWorker.GetSelectedCommandAbilityDataData.Status1 & 0x02) >= 1 ? true : false;
                checkBoxAbComDataPetrify.Checked = (KernelWorker.GetSelectedCommandAbilityDataData.Status1 & 0x04) >= 1 ? true : false;
                checkBoxAbComDataDarkness.Checked = (KernelWorker.GetSelectedCommandAbilityDataData.Status1 & 0x08) >= 1 ? true : false;
                checkBoxAbComDataSilence.Checked = (KernelWorker.GetSelectedCommandAbilityDataData.Status1 & 0x10) >= 1 ? true : false;
                checkBoxAbComDataBerserk.Checked = (KernelWorker.GetSelectedCommandAbilityDataData.Status1 & 0x20) >= 1 ? true : false;
                checkBoxAbComDataZombie.Checked = (KernelWorker.GetSelectedCommandAbilityDataData.Status1 & 0x40) >= 1 ? true : false;
                checkBoxAbComDataUnk7.Checked = (KernelWorker.GetSelectedCommandAbilityDataData.Status1 & 0x80) >= 1 ? true : false;

                checkBoxAbComDataSleep.Checked = (KernelWorker.GetSelectedCommandAbilityDataData.Status2 & 0x01) >= 1 ? true : false;
                checkBoxAbComDataHaste.Checked = (KernelWorker.GetSelectedCommandAbilityDataData.Status2 & 0x02) >= 1 ? true : false;
                checkBoxAbComDataSlow.Checked = (KernelWorker.GetSelectedCommandAbilityDataData.Status2 & 0x04) >= 1 ? true : false;
                checkBoxAbComDataStop.Checked = (KernelWorker.GetSelectedCommandAbilityDataData.Status2 & 0x08) >= 1 ? true : false;
                checkBoxAbComDataRegen.Checked = (KernelWorker.GetSelectedCommandAbilityDataData.Status2 & 0x10) >= 1 ? true : false;
                checkBoxAbComDataProtect.Checked = (KernelWorker.GetSelectedCommandAbilityDataData.Status2 & 0x20) >= 1 ? true : false;
                checkBoxAbComDataShell.Checked = (KernelWorker.GetSelectedCommandAbilityDataData.Status2 & 0x40) >= 1 ? true : false;
                checkBoxAbComDataReflect.Checked = (KernelWorker.GetSelectedCommandAbilityDataData.Status2 & 0x80) >= 1 ? true : false;

                checkBoxAbComDataAura.Checked = (KernelWorker.GetSelectedCommandAbilityDataData.Status3 & 0x01) >= 1 ? true : false;
                checkBoxAbComDataCurse.Checked = (KernelWorker.GetSelectedCommandAbilityDataData.Status3 & 0x02) >= 1 ? true : false;
                checkBoxAbComDataDoom.Checked = (KernelWorker.GetSelectedCommandAbilityDataData.Status3 & 0x04) >= 1 ? true : false;
                checkBoxAbComDataInvincible.Checked = (KernelWorker.GetSelectedCommandAbilityDataData.Status3 & 0x08) >= 1 ? true : false;
                checkBoxAbComDataPetrifying.Checked = (KernelWorker.GetSelectedCommandAbilityDataData.Status3 & 0x10) >= 1 ? true : false;
                checkBoxAbComDataFloat.Checked = (KernelWorker.GetSelectedCommandAbilityDataData.Status3 & 0x20) >= 1 ? true : false;
                checkBoxAbComDataConfusion.Checked = (KernelWorker.GetSelectedCommandAbilityDataData.Status3 & 0x40) >= 1 ? true : false;
                checkBoxAbComDataDrain.Checked = (KernelWorker.GetSelectedCommandAbilityDataData.Status3 & 0x80) >= 1 ? true : false;

                checkBoxAbComDataEject.Checked = (KernelWorker.GetSelectedCommandAbilityDataData.Status4 & 0x01) >= 1 ? true : false;
                checkBoxAbComDataDouble.Checked = (KernelWorker.GetSelectedCommandAbilityDataData.Status4 & 0x02) >= 1 ? true : false;
                checkBoxAbComDataTriple.Checked = (KernelWorker.GetSelectedCommandAbilityDataData.Status4 & 0x04) >= 1 ? true : false;
                checkBoxAbComDataDefend.Checked = (KernelWorker.GetSelectedCommandAbilityDataData.Status4 & 0x08) >= 1 ? true : false;
                checkBoxAbComDataUnk1.Checked = (KernelWorker.GetSelectedCommandAbilityDataData.Status4 & 0x10) >= 1 ? true : false;
                checkBoxAbComDataUnk2.Checked = (KernelWorker.GetSelectedCommandAbilityDataData.Status4 & 0x20) >= 1 ? true : false;
                checkBoxAbComDataCharged.Checked = (KernelWorker.GetSelectedCommandAbilityDataData.Status4 & 0x40) >= 1 ? true : false;
                checkBoxAbComDataBackAttack.Checked = (KernelWorker.GetSelectedCommandAbilityDataData.Status4 & 0x80) >= 1 ? true : false;

                checkBoxAbComDataVit0.Checked = (KernelWorker.GetSelectedCommandAbilityDataData.Status5 & 0x01) >= 1 ? true : false;
                checkBoxAbComDataAngelWing.Checked = (KernelWorker.GetSelectedCommandAbilityDataData.Status5 & 0x02) >= 1 ? true : false;
                checkBoxAbComDataUnk3.Checked = (KernelWorker.GetSelectedCommandAbilityDataData.Status5 & 0x04) >= 1 ? true : false;
                checkBoxAbComDataUnk4.Checked = (KernelWorker.GetSelectedCommandAbilityDataData.Status5 & 0x08) >= 1 ? true : false;
                checkBoxAbComDataUnk5.Checked = (KernelWorker.GetSelectedCommandAbilityDataData.Status5 & 0x10) >= 1 ? true : false;
                checkBoxAbComDataUnk6.Checked = (KernelWorker.GetSelectedCommandAbilityDataData.Status5 & 0x20) >= 1 ? true : false;
                checkBoxAbComDataHasMagic.Checked = (KernelWorker.GetSelectedCommandAbilityDataData.Status5 & 0x40) >= 1 ? true : false;
                checkBoxAbComDataSummonGF.Checked = (KernelWorker.GetSelectedCommandAbilityDataData.Status5 & 0x80) >= 1 ? true : false;
                return;
            }
            ToolTip(checkBoxAbComDataDeath,1, (KernelWorker.GetSelectedCommandAbilityDataData.Status1 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxAbComDataPoison,1, (KernelWorker.GetSelectedCommandAbilityDataData.Status1 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxAbComDataPetrify,1, (KernelWorker.GetSelectedCommandAbilityDataData.Status1 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxAbComDataDarkness,1, (KernelWorker.GetSelectedCommandAbilityDataData.Status1 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxAbComDataSilence,1, (KernelWorker.GetSelectedCommandAbilityDataData.Status1 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxAbComDataBerserk,1, (KernelWorker.GetSelectedCommandAbilityDataData.Status1 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxAbComDataZombie,1, (KernelWorker.GetSelectedCommandAbilityDataData.Status1 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxAbComDataUnk7,1, (KernelWorker.GetSelectedCommandAbilityDataData.Status1 & 0x80) >= 1 ? true : false);

            ToolTip(checkBoxAbComDataSleep,1, (KernelWorker.GetSelectedCommandAbilityDataData.Status2 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxAbComDataHaste,1, (KernelWorker.GetSelectedCommandAbilityDataData.Status2 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxAbComDataSlow,1, (KernelWorker.GetSelectedCommandAbilityDataData.Status2 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxAbComDataStop,1, (KernelWorker.GetSelectedCommandAbilityDataData.Status2 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxAbComDataRegen,1, (KernelWorker.GetSelectedCommandAbilityDataData.Status2 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxAbComDataProtect,1, (KernelWorker.GetSelectedCommandAbilityDataData.Status2 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxAbComDataShell,1, (KernelWorker.GetSelectedCommandAbilityDataData.Status2 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxAbComDataReflect,1, (KernelWorker.GetSelectedCommandAbilityDataData.Status2 & 0x80) >= 1 ? true : false);

            ToolTip(checkBoxAbComDataAura,1, (KernelWorker.GetSelectedCommandAbilityDataData.Status3 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxAbComDataCurse,1, (KernelWorker.GetSelectedCommandAbilityDataData.Status3 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxAbComDataDoom,1, (KernelWorker.GetSelectedCommandAbilityDataData.Status3 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxAbComDataInvincible,1, (KernelWorker.GetSelectedCommandAbilityDataData.Status3 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxAbComDataPetrifying,1, (KernelWorker.GetSelectedCommandAbilityDataData.Status3 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxAbComDataFloat,1, (KernelWorker.GetSelectedCommandAbilityDataData.Status3 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxAbComDataConfusion,1, (KernelWorker.GetSelectedCommandAbilityDataData.Status3 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxAbComDataDrain,1, (KernelWorker.GetSelectedCommandAbilityDataData.Status3 & 0x80) >= 1 ? true : false);

            ToolTip(checkBoxAbComDataEject,1, (KernelWorker.GetSelectedCommandAbilityDataData.Status4 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxAbComDataDouble,1, (KernelWorker.GetSelectedCommandAbilityDataData.Status4 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxAbComDataTriple,1, (KernelWorker.GetSelectedCommandAbilityDataData.Status4 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxAbComDataDefend,1, (KernelWorker.GetSelectedCommandAbilityDataData.Status4 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxAbComDataUnk1,1, (KernelWorker.GetSelectedCommandAbilityDataData.Status4 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxAbComDataUnk2,1, (KernelWorker.GetSelectedCommandAbilityDataData.Status4 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxAbComDataCharged,1, (KernelWorker.GetSelectedCommandAbilityDataData.Status4 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxAbComDataBackAttack,1, (KernelWorker.GetSelectedCommandAbilityDataData.Status4 & 0x80) >= 1 ? true : false);

            ToolTip(checkBoxAbComDataVit0,1, (KernelWorker.GetSelectedCommandAbilityDataData.Status5 & 0x01) >= 1 ? true : false);
            ToolTip(checkBoxAbComDataAngelWing,1, (KernelWorker.GetSelectedCommandAbilityDataData.Status5 & 0x02) >= 1 ? true : false);
            ToolTip(checkBoxAbComDataUnk3,1, (KernelWorker.GetSelectedCommandAbilityDataData.Status5 & 0x04) >= 1 ? true : false);
            ToolTip(checkBoxAbComDataUnk4,1, (KernelWorker.GetSelectedCommandAbilityDataData.Status5 & 0x08) >= 1 ? true : false);
            ToolTip(checkBoxAbComDataUnk5,1, (KernelWorker.GetSelectedCommandAbilityDataData.Status5 & 0x10) >= 1 ? true : false);
            ToolTip(checkBoxAbComDataUnk6,1, (KernelWorker.GetSelectedCommandAbilityDataData.Status5 & 0x20) >= 1 ? true : false);
            ToolTip(checkBoxAbComDataHasMagic,1, (KernelWorker.GetSelectedCommandAbilityDataData.Status5 & 0x40) >= 1 ? true : false);
            ToolTip(checkBoxAbComDataSummonGF,1, (KernelWorker.GetSelectedCommandAbilityDataData.Status5 & 0x80) >= 1 ? true : false);
        }

        private void listBoxAbComData_SelectedIndexChanged(object sender, EventArgs e)
        {
            _loaded = false;
            if (KernelWorker.Kernel == null || KernelWorker.BackupKernel == null)
                return;

            KernelWorker.ReadCommandAbilityData(listBoxAbComData.SelectedIndex, KernelWorker.BackupKernel);
            try
            {
                ToolTip(comboBoxAbComDataMagicID,2, comboBoxAbComDataMagicID.Items[KernelWorker.GetSelectedCommandAbilityDataData.MagicID]);
                ToolTip(comboBoxAbComDataAttackType,2, comboBoxAbComDataAttackType.Items[KernelWorker.GetSelectedCommandAbilityDataData.AttackType]);
                ToolTip(numericUpDownAbComDataAttackPower,0, KernelWorker.GetSelectedCommandAbilityDataData.AttackPower);
                ToolTip(checkBoxAbComDataFlag1,1, (KernelWorker.GetSelectedCommandAbilityDataData.AttackFlags & 0x01) >= 1 ? true : false);
                ToolTip(checkBoxAbComDataFlag2,1, (KernelWorker.GetSelectedCommandAbilityDataData.AttackFlags & 0x02) >= 1 ? true : false);
                ToolTip(checkBoxAbComDataFlag3,1, (KernelWorker.GetSelectedCommandAbilityDataData.AttackFlags & 0x04) >= 1 ? true : false);
                ToolTip(checkBoxAbComDataFlag4,1, (KernelWorker.GetSelectedCommandAbilityDataData.AttackFlags & 0x08) >= 1 ? true : false);
                ToolTip(checkBoxAbComDataFlag5,1, (KernelWorker.GetSelectedCommandAbilityDataData.AttackFlags & 0x10) >= 1 ? true : false);
                ToolTip(checkBoxAbComDataFlag6,1, (KernelWorker.GetSelectedCommandAbilityDataData.AttackFlags & 0x20) >= 1 ? true : false);
                ToolTip(checkBoxAbComDataFlag7,1, (KernelWorker.GetSelectedCommandAbilityDataData.AttackFlags & 0x40) >= 1 ? true : false);
                ToolTip(checkBoxAbComDataFlag8,1, (KernelWorker.GetSelectedCommandAbilityDataData.AttackFlags & 0x80) >= 1 ? true : false);
                ToolTip(numericUpDownAbComDataHitCount,0, KernelWorker.GetSelectedCommandAbilityDataData.HitCount);
                ToolTip(comboBoxAbComDataElement,2, comboBoxAbComDataElement.Items[CommandAbilityData_GetElement()]);
                ToolTip(numericUpDownAbComDataStatusAttack,0, KernelWorker.GetSelectedCommandAbilityDataData.StatusAttack);
                CommandAbilityDataStatusWorker(true);
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }

            KernelWorker.ReadCommandAbilityData(listBoxAbComData.SelectedIndex, KernelWorker.Kernel);
            try
            {
                comboBoxAbComDataMagicID.SelectedIndex = KernelWorker.GetSelectedCommandAbilityDataData.MagicID;
                comboBoxAbComDataAttackType.SelectedIndex = KernelWorker.GetSelectedCommandAbilityDataData.AttackType;
                numericUpDownAbComDataAttackPower.Value = KernelWorker.GetSelectedCommandAbilityDataData.AttackPower;
                checkBoxAbComDataFlag1.Checked = (KernelWorker.GetSelectedCommandAbilityDataData.AttackFlags & 0x01) >= 1 ? true : false;
                checkBoxAbComDataFlag2.Checked = (KernelWorker.GetSelectedCommandAbilityDataData.AttackFlags & 0x02) >= 1 ? true : false;
                checkBoxAbComDataFlag3.Checked = (KernelWorker.GetSelectedCommandAbilityDataData.AttackFlags & 0x04) >= 1 ? true : false;
                checkBoxAbComDataFlag4.Checked = (KernelWorker.GetSelectedCommandAbilityDataData.AttackFlags & 0x08) >= 1 ? true : false;
                checkBoxAbComDataFlag5.Checked = (KernelWorker.GetSelectedCommandAbilityDataData.AttackFlags & 0x10) >= 1 ? true : false;
                checkBoxAbComDataFlag6.Checked = (KernelWorker.GetSelectedCommandAbilityDataData.AttackFlags & 0x20) >= 1 ? true : false;
                checkBoxAbComDataFlag7.Checked = (KernelWorker.GetSelectedCommandAbilityDataData.AttackFlags & 0x40) >= 1 ? true : false;
                checkBoxAbComDataFlag8.Checked = (KernelWorker.GetSelectedCommandAbilityDataData.AttackFlags & 0x80) >= 1 ? true : false;
                numericUpDownAbComDataHitCount.Value = KernelWorker.GetSelectedCommandAbilityDataData.HitCount;
                comboBoxAbComDataElement.SelectedIndex = CommandAbilityData_GetElement();
                numericUpDownAbComDataStatusAttack.Value = KernelWorker.GetSelectedCommandAbilityDataData.StatusAttack;
                CommandAbilityDataStatusWorker(false);
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }
            _loaded = true;
        }

        #endregion

        #region Command abilities

        private void listBoxAbCom_SelectedIndexChanged(object sender, EventArgs e)
        {
            _loaded = false;
            if (KernelWorker.Kernel == null || KernelWorker.BackupKernel == null)
                return;

            KernelWorker.ReadCommandAbility(listBoxAbCom.SelectedIndex, KernelWorker.BackupKernel);
            try
            {
                ToolTip(numericUpDownAbComAP,0,KernelWorker.GetSelectedCommandAbilityData.AP);
                ToolTip(comboBoxAbComBattleCommand,2, comboBoxAbComBattleCommand.Items[KernelWorker.GetSelectedCommandAbilityData.BattleCommand]);
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }

            KernelWorker.ReadCommandAbility(listBoxAbCom.SelectedIndex,KernelWorker.Kernel);
            try
            {
                numericUpDownAbComAP.Value = KernelWorker.GetSelectedCommandAbilityData.AP;
                comboBoxAbComBattleCommand.SelectedIndex = KernelWorker.GetSelectedCommandAbilityData.BattleCommand;               
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }
            _loaded = true;
        }

        #endregion

        #region Junction abilities

        private void listBoxAbJun_SelectedIndexChanged(object sender, EventArgs e)
        {
            _loaded = false;
            if (KernelWorker.Kernel == null || KernelWorker.BackupKernel == null)
                return;

            KernelWorker.ReadJunctionAbilities(listBoxAbJun.SelectedIndex, KernelWorker.BackupKernel);
            try
            {
                ToolTip(numericUpDownAbJunAP,0,KernelWorker.GetSelectedJunctionAbilitiesData.AP);
                ToolTip(checkBoxAbJunFlag1,1,(KernelWorker.GetSelectedJunctionAbilitiesData.Flag1 & 0x01) >= 1 ? true : false);
                ToolTip(checkBoxAbJunFlag2, 1, (KernelWorker.GetSelectedJunctionAbilitiesData.Flag1 & 0x02) >= 1 ? true : false);
                ToolTip(checkBoxAbJunFlag3, 1, (KernelWorker.GetSelectedJunctionAbilitiesData.Flag1 & 0x04) >= 1 ? true : false);
                ToolTip(checkBoxAbJunFlag4, 1, (KernelWorker.GetSelectedJunctionAbilitiesData.Flag1 & 0x08) >= 1 ? true : false);
                ToolTip(checkBoxAbJunFlag5, 1, (KernelWorker.GetSelectedJunctionAbilitiesData.Flag1 & 0x10) >= 1 ? true : false);
                ToolTip(checkBoxAbJunFlag6, 1, (KernelWorker.GetSelectedJunctionAbilitiesData.Flag1 & 0x20) >= 1 ? true : false);
                ToolTip(checkBoxAbJunFlag7, 1, (KernelWorker.GetSelectedJunctionAbilitiesData.Flag1 & 0x40) >= 1 ? true : false);
                ToolTip(checkBoxAbJunFlag8, 1, (KernelWorker.GetSelectedJunctionAbilitiesData.Flag1 & 0x80) >= 1 ? true : false);
                ToolTip(checkBoxAbJunFlag9, 1, (KernelWorker.GetSelectedJunctionAbilitiesData.Flag2 & 0x01) >= 1 ? true : false);
                ToolTip(checkBoxAbJunFlag10, 1, (KernelWorker.GetSelectedJunctionAbilitiesData.Flag2 & 0x02) >= 1 ? true : false);
                ToolTip(checkBoxAbJunFlag11, 1, (KernelWorker.GetSelectedJunctionAbilitiesData.Flag2 & 0x04) >= 1 ? true : false);
                ToolTip(checkBoxAbJunFlag12, 1, (KernelWorker.GetSelectedJunctionAbilitiesData.Flag2 & 0x08) >= 1 ? true : false);
                ToolTip(checkBoxAbJunFlag13, 1, (KernelWorker.GetSelectedJunctionAbilitiesData.Flag2 & 0x10) >= 1 ? true : false);
                ToolTip(checkBoxAbJunFlag14, 1, (KernelWorker.GetSelectedJunctionAbilitiesData.Flag2 & 0x20) >= 1 ? true : false);
                ToolTip(checkBoxAbJunFlag15, 1, (KernelWorker.GetSelectedJunctionAbilitiesData.Flag2 & 0x40) >= 1 ? true : false);
                ToolTip(checkBoxAbJunFlag16, 1, (KernelWorker.GetSelectedJunctionAbilitiesData.Flag2 & 0x80) >= 1 ? true : false);
                ToolTip(checkBoxAbJunFlag17, 1, (KernelWorker.GetSelectedJunctionAbilitiesData.Flag3 & 0x01) >= 1 ? true : false);
                ToolTip(checkBoxAbJunFlag18, 1, (KernelWorker.GetSelectedJunctionAbilitiesData.Flag3 & 0x02) >= 1 ? true : false);
                ToolTip(checkBoxAbJunFlag19, 1, (KernelWorker.GetSelectedJunctionAbilitiesData.Flag3 & 0x04) >= 1 ? true : false);
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }

            KernelWorker.ReadJunctionAbilities(listBoxAbJun.SelectedIndex, KernelWorker.Kernel);
            try
            {
                numericUpDownAbJunAP.Value = KernelWorker.GetSelectedJunctionAbilitiesData.AP;
                checkBoxAbJunFlag1.Checked = (KernelWorker.GetSelectedJunctionAbilitiesData.Flag1 & 0x01) >= 1 ? true : false;
                checkBoxAbJunFlag2.Checked = (KernelWorker.GetSelectedJunctionAbilitiesData.Flag1 & 0x02) >= 1 ? true : false;
                checkBoxAbJunFlag3.Checked = (KernelWorker.GetSelectedJunctionAbilitiesData.Flag1 & 0x04) >= 1 ? true : false;
                checkBoxAbJunFlag4.Checked = (KernelWorker.GetSelectedJunctionAbilitiesData.Flag1 & 0x08) >= 1 ? true : false;
                checkBoxAbJunFlag5.Checked = (KernelWorker.GetSelectedJunctionAbilitiesData.Flag1 & 0x10) >= 1 ? true : false;
                checkBoxAbJunFlag6.Checked = (KernelWorker.GetSelectedJunctionAbilitiesData.Flag1 & 0x20) >= 1 ? true : false;
                checkBoxAbJunFlag7.Checked = (KernelWorker.GetSelectedJunctionAbilitiesData.Flag1 & 0x40) >= 1 ? true : false;
                checkBoxAbJunFlag8.Checked = (KernelWorker.GetSelectedJunctionAbilitiesData.Flag1 & 0x80) >= 1 ? true : false;
                checkBoxAbJunFlag9.Checked = (KernelWorker.GetSelectedJunctionAbilitiesData.Flag2 & 0x01) >= 1 ? true : false;
                checkBoxAbJunFlag10.Checked = (KernelWorker.GetSelectedJunctionAbilitiesData.Flag2 & 0x02) >= 1 ? true : false;
                checkBoxAbJunFlag11.Checked = (KernelWorker.GetSelectedJunctionAbilitiesData.Flag2 & 0x04) >= 1 ? true : false;
                checkBoxAbJunFlag12.Checked = (KernelWorker.GetSelectedJunctionAbilitiesData.Flag2 & 0x08) >= 1 ? true : false;
                checkBoxAbJunFlag13.Checked = (KernelWorker.GetSelectedJunctionAbilitiesData.Flag2 & 0x10) >= 1 ? true : false;
                checkBoxAbJunFlag14.Checked = (KernelWorker.GetSelectedJunctionAbilitiesData.Flag2 & 0x20) >= 1 ? true : false;
                checkBoxAbJunFlag15.Checked = (KernelWorker.GetSelectedJunctionAbilitiesData.Flag2 & 0x40) >= 1 ? true : false;
                checkBoxAbJunFlag16.Checked = (KernelWorker.GetSelectedJunctionAbilitiesData.Flag2 & 0x80) >= 1 ? true : false;
                checkBoxAbJunFlag17.Checked = (KernelWorker.GetSelectedJunctionAbilitiesData.Flag3 & 0x01) >= 1 ? true : false;
                checkBoxAbJunFlag18.Checked = (KernelWorker.GetSelectedJunctionAbilitiesData.Flag3 & 0x02) >= 1 ? true : false;
                checkBoxAbJunFlag19.Checked = (KernelWorker.GetSelectedJunctionAbilitiesData.Flag3 & 0x04) >= 1 ? true : false;
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }
            _loaded = true;
        }

        #endregion

        #region Party abilities

        private void listBoxAbParty_SelectedIndexChanged(object sender, EventArgs e)
        {
            _loaded = false;
            if (KernelWorker.Kernel == null || KernelWorker.BackupKernel == null)
                return;

            KernelWorker.ReadPartyAbilities(listBoxAbParty.SelectedIndex, KernelWorker.BackupKernel);
            try
            {
                ToolTip(numericUpDownAbPartyAP,0,KernelWorker.GetSelectedPartyAbilitiesData.AP);
                ToolTip(checkBoxAbPartyFlag1,1,(KernelWorker.GetSelectedPartyAbilitiesData.Flag & 0x01) >= 1 ? true : false);
                ToolTip(checkBoxAbPartyFlag2, 1, (KernelWorker.GetSelectedPartyAbilitiesData.Flag & 0x02) >= 1 ? true : false);
                ToolTip(checkBoxAbPartyFlag3, 1, (KernelWorker.GetSelectedPartyAbilitiesData.Flag & 0x04) >= 1 ? true : false);
                ToolTip(checkBoxAbPartyFlag4, 1, (KernelWorker.GetSelectedPartyAbilitiesData.Flag & 0x08) >= 1 ? true : false);
                ToolTip(checkBoxAbPartyFlag5, 1, (KernelWorker.GetSelectedPartyAbilitiesData.Flag & 0x10) >= 1 ? true : false);
                ToolTip(checkBoxAbPartyFlag6, 1, (KernelWorker.GetSelectedPartyAbilitiesData.Flag & 0x20) >= 1 ? true : false);
                ToolTip(checkBoxAbPartyFlag7, 1, (KernelWorker.GetSelectedPartyAbilitiesData.Flag & 0x40) >= 1 ? true : false);
                ToolTip(checkBoxAbPartyFlag8, 1, (KernelWorker.GetSelectedPartyAbilitiesData.Flag & 0x80) >= 1 ? true : false);
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }

            KernelWorker.ReadPartyAbilities(listBoxAbParty.SelectedIndex, KernelWorker.Kernel);
            try
            {
                numericUpDownAbPartyAP.Value = KernelWorker.GetSelectedPartyAbilitiesData.AP;
                checkBoxAbPartyFlag1.Checked = (KernelWorker.GetSelectedPartyAbilitiesData.Flag & 0x01) >= 1 ? true : false;
                checkBoxAbPartyFlag2.Checked = (KernelWorker.GetSelectedPartyAbilitiesData.Flag & 0x02) >= 1 ? true : false;
                checkBoxAbPartyFlag3.Checked = (KernelWorker.GetSelectedPartyAbilitiesData.Flag & 0x04) >= 1 ? true : false;
                checkBoxAbPartyFlag4.Checked = (KernelWorker.GetSelectedPartyAbilitiesData.Flag & 0x08) >= 1 ? true : false;
                checkBoxAbPartyFlag5.Checked = (KernelWorker.GetSelectedPartyAbilitiesData.Flag & 0x10) >= 1 ? true : false;
                checkBoxAbPartyFlag6.Checked = (KernelWorker.GetSelectedPartyAbilitiesData.Flag & 0x20) >= 1 ? true : false;
                checkBoxAbPartyFlag7.Checked = (KernelWorker.GetSelectedPartyAbilitiesData.Flag & 0x40) >= 1 ? true : false;
                checkBoxAbPartyFlag8.Checked = (KernelWorker.GetSelectedPartyAbilitiesData.Flag & 0x80) >= 1 ? true : false;
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }
            _loaded = true;
        }

        #endregion

        #region GFs abilities

        private int GFAbilities_GetStat()
        {
            return KernelWorker.GetSelectedGFAbilitiesData.StatToIncrease == KernelWorker.StatToIncrease.SumMag
                        ? 0
                        : KernelWorker.GetSelectedGFAbilitiesData.StatToIncrease == KernelWorker.StatToIncrease.HP
                            ? 1
                            : KernelWorker.GetSelectedGFAbilitiesData.StatToIncrease == KernelWorker.StatToIncrease.Boost

                            ? comboBoxAbGFStatToIncrease.Items.Count -1
                            : 0;
        }

        private byte GFAbilities_GetStat(int Index)
        {
            byte stat = (byte)(Index == 2 ? (byte)KernelWorker.StatToIncrease.Boost :
                Index == 0 ? (byte)KernelWorker.StatToIncrease.SumMag :
                Index == 1 ? (byte)KernelWorker.StatToIncrease.HP :
                0x00 /*ErrorHandler*/);
            return stat;
        }


        private void listBoxAbGF_SelectedIndexChanged(object sender, EventArgs e)
        {
            _loaded = false;
            if (KernelWorker.Kernel == null || KernelWorker.BackupKernel == null)
                return;

            KernelWorker.ReadGFAbilities(listBoxAbGF.SelectedIndex,KernelWorker.BackupKernel);
            try
            {
                ToolTip(numericUpDownAbGFAP,0,KernelWorker.GetSelectedGFAbilitiesData.AP);
                ToolTip(checkBoxAbGFBoost,1, (KernelWorker.GetSelectedGFAbilitiesData.EnableBoost & 0x01) >= 1 ? true : false);
                ToolTip(comboBoxAbGFStatToIncrease,2,comboBoxAbGFStatToIncrease.Items[GFAbilities_GetStat()]);
                ToolTip(trackBarAbGFIncrementValue,0, KernelWorker.GetSelectedGFAbilitiesData.IncrementValue);
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }

            KernelWorker.ReadGFAbilities(listBoxAbGF.SelectedIndex, KernelWorker.Kernel);
            try
            {
                numericUpDownAbGFAP.Value = KernelWorker.GetSelectedGFAbilitiesData.AP;
                checkBoxAbGFBoost.Checked = (KernelWorker.GetSelectedGFAbilitiesData.EnableBoost & 0x01) >= 1 ? true : false;
                comboBoxAbGFStatToIncrease.SelectedIndex = GFAbilities_GetStat();
                trackBarAbGFIncrementValue.Value = KernelWorker.GetSelectedGFAbilitiesData.IncrementValue;
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }
            _loaded = true;
        }

        #endregion

        #region Party abilities

        private void listBoxAbChar_SelectedIndexChanged(object sender, EventArgs e)
        {
            _loaded = false;
            if (KernelWorker.Kernel == null || KernelWorker.BackupKernel == null)
                return;

            KernelWorker.ReadCharacterAbilities(listBoxAbChar.SelectedIndex, KernelWorker.BackupKernel);
            try
            {
                ToolTip(numericUpDownAbCharAP,0, KernelWorker.GetSelectedCharacterAbilitiesData.AP);
                ToolTip(checkBoxAbCharFlag1 ,1, (KernelWorker.GetSelectedCharacterAbilitiesData.Flag1 & 0x01) >= 1 ? true : false);
                ToolTip(checkBoxAbCharFlag2, 1, (KernelWorker.GetSelectedCharacterAbilitiesData.Flag1 & 0x02) >= 1 ? true : false);
                ToolTip(checkBoxAbCharFlag3, 1, (KernelWorker.GetSelectedCharacterAbilitiesData.Flag1 & 0x04) >= 1 ? true : false);
                ToolTip(checkBoxAbCharFlag4, 1, (KernelWorker.GetSelectedCharacterAbilitiesData.Flag1 & 0x08) >= 1 ? true : false);
                ToolTip(checkBoxAbCharFlag5, 1, (KernelWorker.GetSelectedCharacterAbilitiesData.Flag1 & 0x10) >= 1 ? true : false);
                ToolTip(checkBoxAbCharFlag6, 1, (KernelWorker.GetSelectedCharacterAbilitiesData.Flag1 & 0x20) >= 1 ? true : false);
                ToolTip(checkBoxAbCharFlag7, 1, (KernelWorker.GetSelectedCharacterAbilitiesData.Flag1 & 0x40) >= 1 ? true : false);
                ToolTip(checkBoxAbCharFlag8, 1, (KernelWorker.GetSelectedCharacterAbilitiesData.Flag1 & 0x80) >= 1 ? true : false);
                ToolTip(checkBoxAbCharFlag9, 1, (KernelWorker.GetSelectedCharacterAbilitiesData.Flag2 & 0x01) >= 1 ? true : false);
                ToolTip(checkBoxAbCharFlag10, 1, (KernelWorker.GetSelectedCharacterAbilitiesData.Flag2 & 0x02) >= 1 ? true : false);
                ToolTip(checkBoxAbCharFlag11, 1, (KernelWorker.GetSelectedCharacterAbilitiesData.Flag2 & 0x04) >= 1 ? true : false);
                ToolTip(checkBoxAbCharFlag12, 1, (KernelWorker.GetSelectedCharacterAbilitiesData.Flag2 & 0x08) >= 1 ? true : false);
                ToolTip(checkBoxAbCharFlag13, 1, (KernelWorker.GetSelectedCharacterAbilitiesData.Flag2 & 0x10) >= 1 ? true : false);
                ToolTip(checkBoxAbCharFlag14, 1, (KernelWorker.GetSelectedCharacterAbilitiesData.Flag2 & 0x20) >= 1 ? true : false);
                ToolTip(checkBoxAbCharFlag15, 1, (KernelWorker.GetSelectedCharacterAbilitiesData.Flag2 & 0x40) >= 1 ? true : false);
                ToolTip(checkBoxAbCharFlag16, 1, (KernelWorker.GetSelectedCharacterAbilitiesData.Flag2 & 0x80) >= 1 ? true : false);
                ToolTip(checkBoxAbCharFlag17, 1, (KernelWorker.GetSelectedCharacterAbilitiesData.Flag3 & 0x01) >= 1 ? true : false);
                ToolTip(checkBoxAbCharFlag18, 1, (KernelWorker.GetSelectedCharacterAbilitiesData.Flag3 & 0x02) >= 1 ? true : false);
                ToolTip(checkBoxAbCharFlag19, 1, (KernelWorker.GetSelectedCharacterAbilitiesData.Flag3 & 0x04) >= 1 ? true : false);
                ToolTip(checkBoxAbCharFlag20, 1, (KernelWorker.GetSelectedCharacterAbilitiesData.Flag3 & 0x08) >= 1 ? true : false);
                ToolTip(checkBoxAbCharFlag21, 1, (KernelWorker.GetSelectedCharacterAbilitiesData.Flag3 & 0x10) >= 1 ? true : false);
                ToolTip(checkBoxAbCharFlag22, 1, (KernelWorker.GetSelectedCharacterAbilitiesData.Flag3 & 0x20) >= 1 ? true : false);
                ToolTip(checkBoxAbCharFlag23, 1, (KernelWorker.GetSelectedCharacterAbilitiesData.Flag3 & 0x40) >= 1 ? true : false);
                ToolTip(checkBoxAbCharFlag24, 1, (KernelWorker.GetSelectedCharacterAbilitiesData.Flag3 & 0x80) >= 1 ? true : false);
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }

            KernelWorker.ReadCharacterAbilities(listBoxAbChar.SelectedIndex, KernelWorker.Kernel);
            try
            {
                numericUpDownAbCharAP.Value = KernelWorker.GetSelectedCharacterAbilitiesData.AP;
                checkBoxAbCharFlag1.Checked = (KernelWorker.GetSelectedCharacterAbilitiesData.Flag1 & 0x01) >= 1 ? true : false;
                checkBoxAbCharFlag2.Checked = (KernelWorker.GetSelectedCharacterAbilitiesData.Flag1 & 0x02) >= 1 ? true : false;
                checkBoxAbCharFlag3.Checked = (KernelWorker.GetSelectedCharacterAbilitiesData.Flag1 & 0x04) >= 1 ? true : false;
                checkBoxAbCharFlag4.Checked = (KernelWorker.GetSelectedCharacterAbilitiesData.Flag1 & 0x08) >= 1 ? true : false;
                checkBoxAbCharFlag5.Checked = (KernelWorker.GetSelectedCharacterAbilitiesData.Flag1 & 0x10) >= 1 ? true : false;
                checkBoxAbCharFlag6.Checked = (KernelWorker.GetSelectedCharacterAbilitiesData.Flag1 & 0x20) >= 1 ? true : false;
                checkBoxAbCharFlag7.Checked = (KernelWorker.GetSelectedCharacterAbilitiesData.Flag1 & 0x40) >= 1 ? true : false;
                checkBoxAbCharFlag8.Checked = (KernelWorker.GetSelectedCharacterAbilitiesData.Flag1 & 0x80) >= 1 ? true : false;
                checkBoxAbCharFlag9.Checked = (KernelWorker.GetSelectedCharacterAbilitiesData.Flag2 & 0x01) >= 1 ? true : false;
                checkBoxAbCharFlag10.Checked = (KernelWorker.GetSelectedCharacterAbilitiesData.Flag2 & 0x02) >= 1 ? true : false;
                checkBoxAbCharFlag11.Checked = (KernelWorker.GetSelectedCharacterAbilitiesData.Flag2 & 0x04) >= 1 ? true : false;
                checkBoxAbCharFlag12.Checked = (KernelWorker.GetSelectedCharacterAbilitiesData.Flag2 & 0x08) >= 1 ? true : false;
                checkBoxAbCharFlag13.Checked = (KernelWorker.GetSelectedCharacterAbilitiesData.Flag2 & 0x10) >= 1 ? true : false;
                checkBoxAbCharFlag14.Checked = (KernelWorker.GetSelectedCharacterAbilitiesData.Flag2 & 0x20) >= 1 ? true : false;
                checkBoxAbCharFlag15.Checked = (KernelWorker.GetSelectedCharacterAbilitiesData.Flag2 & 0x40) >= 1 ? true : false;
                checkBoxAbCharFlag16.Checked = (KernelWorker.GetSelectedCharacterAbilitiesData.Flag2 & 0x80) >= 1 ? true : false;
                checkBoxAbCharFlag17.Checked = (KernelWorker.GetSelectedCharacterAbilitiesData.Flag3 & 0x01) >= 1 ? true : false;
                checkBoxAbCharFlag18.Checked = (KernelWorker.GetSelectedCharacterAbilitiesData.Flag3 & 0x02) >= 1 ? true : false;
                checkBoxAbCharFlag19.Checked = (KernelWorker.GetSelectedCharacterAbilitiesData.Flag3 & 0x04) >= 1 ? true : false;
                checkBoxAbCharFlag20.Checked = (KernelWorker.GetSelectedCharacterAbilitiesData.Flag3 & 0x08) >= 1 ? true : false;
                checkBoxAbCharFlag21.Checked = (KernelWorker.GetSelectedCharacterAbilitiesData.Flag3 & 0x10) >= 1 ? true : false;
                checkBoxAbCharFlag22.Checked = (KernelWorker.GetSelectedCharacterAbilitiesData.Flag3 & 0x20) >= 1 ? true : false;
                checkBoxAbCharFlag23.Checked = (KernelWorker.GetSelectedCharacterAbilitiesData.Flag3 & 0x40) >= 1 ? true : false;
                checkBoxAbCharFlag24.Checked = (KernelWorker.GetSelectedCharacterAbilitiesData.Flag3 & 0x80) >= 1 ? true : false;
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }
            _loaded = true;
        }



        #endregion

        #region Menu abilities

        private void listBoxAbMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            _loaded = false;
            if (KernelWorker.Kernel == null || KernelWorker.BackupKernel == null)
                return;

            KernelWorker.ReadMenuAbilities(listBoxAbMenu.SelectedIndex, KernelWorker.BackupKernel);
            try
            {
                ToolTip(numericUpDownAbMenuAP,0,KernelWorker.GetSelectedMenuAbilitiesData.AP);
                ToolTip(numericUpDownAbMenuIndex,0,KernelWorker.GetSelectedMenuAbilitiesData.Index);
                ToolTip(numericUpDownAbMenuStartEntry,0,KernelWorker.GetSelectedMenuAbilitiesData.StartEntry);
                ToolTip(numericUpDownAbMenuEndEntry,0,KernelWorker.GetSelectedMenuAbilitiesData.EndEntry);
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }

            KernelWorker.ReadMenuAbilities(listBoxAbMenu.SelectedIndex, KernelWorker.Kernel);
            try
            {
                numericUpDownAbMenuAP.Value = KernelWorker.GetSelectedMenuAbilitiesData.AP;
                numericUpDownAbMenuIndex.Value = KernelWorker.GetSelectedMenuAbilitiesData.Index;
                numericUpDownAbMenuStartEntry.Value = KernelWorker.GetSelectedMenuAbilitiesData.StartEntry;
                numericUpDownAbMenuEndEntry.Value = KernelWorker.GetSelectedMenuAbilitiesData.EndEntry;
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }
            _loaded = true;
        }

        #endregion

        #region Battle commands

        private void listBoxBatCom_SelectedIndexChanged(object sender, EventArgs e)
        {
            _loaded = false;
            if (KernelWorker.Kernel == null || KernelWorker.BackupKernel == null)
                return;

            KernelWorker.ReadBattleCommands(listBoxBatCom.SelectedIndex, KernelWorker.BackupKernel);
            try
            {
                ToolTip(comboBoxBatComAbilityID,2,comboBoxBatComAbilityID.Items[KernelWorker.GetSelectedBattleCommandsData.AbilityID]);
                ToolTip(checkBoxBatComFlag1,1,(KernelWorker.GetSelectedBattleCommandsData.Flag & 0x01) >= 1 ? true : false);
                ToolTip(checkBoxBatComFlag2, 1, (KernelWorker.GetSelectedBattleCommandsData.Flag & 0x02) >= 1 ? true : false);
                ToolTip(checkBoxBatComFlag3, 1, (KernelWorker.GetSelectedBattleCommandsData.Flag & 0x04) >= 1 ? true : false);
                ToolTip(checkBoxBatComFlag4, 1, (KernelWorker.GetSelectedBattleCommandsData.Flag & 0x08) >= 1 ? true : false);
                ToolTip(checkBoxBatComFlag5, 1, (KernelWorker.GetSelectedBattleCommandsData.Flag & 0x10) >= 1 ? true : false);
                ToolTip(checkBoxBatComFlag6, 1, (KernelWorker.GetSelectedBattleCommandsData.Flag & 0x20) >= 1 ? true : false);
                ToolTip(checkBoxBatComFlag7, 1, (KernelWorker.GetSelectedBattleCommandsData.Flag & 0x40) >= 1 ? true : false);
                ToolTip(checkBoxBatComFlag8, 1, (KernelWorker.GetSelectedBattleCommandsData.Flag & 0x80) >= 1 ? true : false);
                ToolTip(numericUpDownBatComTarget,0,KernelWorker.GetSelectedBattleCommandsData.Target);
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }

            KernelWorker.ReadBattleCommands(listBoxBatCom.SelectedIndex, KernelWorker.Kernel);
            try
            {
                comboBoxBatComAbilityID.SelectedIndex = KernelWorker.GetSelectedBattleCommandsData.AbilityID;
                checkBoxBatComFlag1.Checked = (KernelWorker.GetSelectedBattleCommandsData.Flag & 0x01) >= 1 ? true : false;
                checkBoxBatComFlag2.Checked = (KernelWorker.GetSelectedBattleCommandsData.Flag & 0x02) >= 1 ? true : false;
                checkBoxBatComFlag3.Checked = (KernelWorker.GetSelectedBattleCommandsData.Flag & 0x04) >= 1 ? true : false;
                checkBoxBatComFlag4.Checked = (KernelWorker.GetSelectedBattleCommandsData.Flag & 0x08) >= 1 ? true : false;
                checkBoxBatComFlag5.Checked = (KernelWorker.GetSelectedBattleCommandsData.Flag & 0x10) >= 1 ? true : false;
                checkBoxBatComFlag6.Checked = (KernelWorker.GetSelectedBattleCommandsData.Flag & 0x20) >= 1 ? true : false;
                checkBoxBatComFlag7.Checked = (KernelWorker.GetSelectedBattleCommandsData.Flag & 0x40) >= 1 ? true : false;
                checkBoxBatComFlag8.Checked = (KernelWorker.GetSelectedBattleCommandsData.Flag & 0x80) >= 1 ? true : false;
                numericUpDownBatComTarget.Value = KernelWorker.GetSelectedBattleCommandsData.Target;
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }
            _loaded = true;
        }

        #endregion

        #region Rinoa commands

        private void listBoxBatComRinoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            _loaded = false;
            if (KernelWorker.Kernel == null || KernelWorker.BackupKernel == null)
                return;

            KernelWorker.ReadRinoaCommands(listBoxBatComRinoa.SelectedIndex, KernelWorker.BackupKernel);
            try
            {
                ToolTip(checkBoxBatComRinoaFlag1, 1, (KernelWorker.GetSelectedRinoaCommandsData.Flag & 0x01) >= 1 ? true : false);
                ToolTip(checkBoxBatComRinoaFlag2, 1, (KernelWorker.GetSelectedRinoaCommandsData.Flag & 0x02) >= 1 ? true : false);
                ToolTip(checkBoxBatComRinoaFlag3, 1, (KernelWorker.GetSelectedRinoaCommandsData.Flag & 0x04) >= 1 ? true : false);
                ToolTip(checkBoxBatComRinoaFlag4, 1, (KernelWorker.GetSelectedRinoaCommandsData.Flag & 0x08) >= 1 ? true : false);
                ToolTip(checkBoxBatComRinoaFlag5, 1, (KernelWorker.GetSelectedRinoaCommandsData.Flag & 0x10) >= 1 ? true : false);
                ToolTip(checkBoxBatComRinoaFlag6, 1, (KernelWorker.GetSelectedRinoaCommandsData.Flag & 0x20) >= 1 ? true : false);
                ToolTip(checkBoxBatComRinoaFlag7, 1, (KernelWorker.GetSelectedRinoaCommandsData.Flag & 0x40) >= 1 ? true : false);
                ToolTip(checkBoxBatComRinoaFlag8, 1, (KernelWorker.GetSelectedRinoaCommandsData.Flag & 0x80) >= 1 ? true : false);
                ToolTip(numericUpDownBatComRinoaTarget, 0, KernelWorker.GetSelectedRinoaCommandsData.Target);
                ToolTip(comboBoxBatComRinoaID, 2, comboBoxBatComRinoaID.Items[KernelWorker.GetSelectedRinoaCommandsData.AbilityID]);
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }

            KernelWorker.ReadRinoaCommands(listBoxBatComRinoa.SelectedIndex, KernelWorker.Kernel);
            try
            {                
                checkBoxBatComRinoaFlag1.Checked = (KernelWorker.GetSelectedRinoaCommandsData.Flag & 0x01) >= 1 ? true : false;
                checkBoxBatComRinoaFlag2.Checked = (KernelWorker.GetSelectedRinoaCommandsData.Flag & 0x02) >= 1 ? true : false;
                checkBoxBatComRinoaFlag3.Checked = (KernelWorker.GetSelectedRinoaCommandsData.Flag & 0x04) >= 1 ? true : false;
                checkBoxBatComRinoaFlag4.Checked = (KernelWorker.GetSelectedRinoaCommandsData.Flag & 0x08) >= 1 ? true : false;
                checkBoxBatComRinoaFlag5.Checked = (KernelWorker.GetSelectedRinoaCommandsData.Flag & 0x10) >= 1 ? true : false;
                checkBoxBatComRinoaFlag6.Checked = (KernelWorker.GetSelectedRinoaCommandsData.Flag & 0x20) >= 1 ? true : false;
                checkBoxBatComRinoaFlag7.Checked = (KernelWorker.GetSelectedRinoaCommandsData.Flag & 0x40) >= 1 ? true : false;
                checkBoxBatComRinoaFlag8.Checked = (KernelWorker.GetSelectedRinoaCommandsData.Flag & 0x80) >= 1 ? true : false;
                numericUpDownBatComRinoaTarget.Value = KernelWorker.GetSelectedRinoaCommandsData.Target;
                comboBoxBatComRinoaID.SelectedIndex = KernelWorker.GetSelectedRinoaCommandsData.AbilityID;
            }

            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }
            _loaded = true;
        }

        #endregion

        #endregion

    }
}