using System;
using System.Windows.Forms;

namespace Doomtrain
{
    class KernelWorker
    {
        #region DECLARATIONS

        public static byte[] Kernel;
        public static byte[] BackupKernel;
        private static uint[,] TextOffsets; //2D uint

        struct Sections
        {
            public const int _1_BattleCommands = 39;
            public const int _2_MagicData = 57;
            public const int _3_JGF = 16;
            public const int _4_EnAttack = 384;
            public const int _5_Weap = 33;
            public const int _6_renzokukenF = 4;
            public const int _7_char = 11;
            public const int _8_BatItems = 33;
            public const int _9_Items = 166;
            public const int _10_nonJGF = 16;
            public const int _11_CommAbilities = 12;
            public const int _12_JnctAblt = 20;
            public const int _13_CommAbilities = 19;
            public const int _14_offPercent = 19;
            public const int _15_CharAbilt = 20;
            public const int _16_PartyAbil = 5;
            public const int _17_GFAbil = 9;
            public const int _18_MenuAbil = 24;
            public const int _19_tempCharAb = 5;
            public const int _20_BlueMag = 20;
            public const int _21_BlueMagParams = 64;
            public const int _22_IrvineLB = 8;
            public const int _23_ZellDuel = 10;
            public const int _24_ZellParam = 1;
            public const int _25_RinoaLB = 2;
            public const int _26_RinoaLB2 = 33;
            //public const int _27_SlotArray = 0;
            //public const int _28_SelphieLB = 0;
            public const int _29_Devour = 16;
            //public const int _30_Misc = 0;
            //public const int _31_MiscText = 0;


        }

        public static int MagicDataOffset = -1;
        public static int OffsetToMagicSelected = -1;

        public static int GFDataOffset = -1;
        public static int OffsetToGFSelected = -1;

        public static int GFAttacksDataOffset = -1;
        public static int OffsetToGFAttacksSelected = -1;

        public static int WeaponsDataOffset = -1;
        public static int OffsetToWeaponsSelected = -1;

        public static int CharactersDataOffset = -1;
        public static int OffsetToCharactersSelected = -1;

        public static int EnemyAttacksDataOffset = -1;
        public static int OffsetToEnemyAttacksSelected = -1;

        public static int BlueMagicDataOffset = -1;
        public static int OffsetToBlueMagicSelected = -1;
        public static int BlueMagicParamDataOffset = -1;
        public static int OffsetToBlueMagicParamSelected = -1;

        public static int StatPercentageAbilitiesDataOffset = -1;
        public static int OffsetToStatPercentageAbilitiesSelected = -1;

        public static int RenzoFinDataOffset = -1;
        public static int OffsetToRenzoFinSelected = -1;

        public static int TempCharLBDataOffset = -1;
        public static int OffsetToTempCharLBSelected = -1;

        public static int ShotDataOffset = -1;
        public static int OffsetToShotSelected = -1;

        public static int DuelDataOffset = -1;
        public static int OffsetToDuelSelected = -1;
        public static int DuelParamsDataOffset = -1;
        public static int OffsetToDuelParamsSelected = -1;

        public static int CombineDataOffset = -1;
        public static int OffsetToCombineSelected = -1;

        public static int ItemsDataOffset = -1;
        public static int OffsetToItemsSelected = -1;

        public static int BattleItemsDataOffset = -1;
        public static int OffsetToBattleItemsSelected = -1;

        public static int SlotArrayDataOffset = -1;
        public static int OffsetToSlotArraySelected = -1;
        public static int SlotsSetsDataOffset = -1;
        public static int OffsetToSlotsSetsSelected = -1;

        public static int DevourDataOffset = -1;
        public static int OffsetToDevourSelected = -1;

        public static int MiscDataOffset = -1;
        public static int OffsetToMiscSelected = -1;

        public static int CommandAbilityDataDataOffset = -1;
        public static int OffsetToCommandAbilityDataSelected = -1;

        public static int CommandAbilityDataOffset = -1;
        public static int OffsetToCommandAbilitySelected = -1;

        public static int JunctionAbilitiesDataOffset = -1;
        public static int OffsetToJunctionAbilitiesSelected = -1;

        public static int PartyAbilitiesDataOffset = -1;
        public static int OffsetToPartyAbilitiesSelected = -1;

        public static int GFAbilitiesDataOffset = -1;
        public static int OffsetToGFAbilitiesSelected = -1;

        public static int CharacterAbilitiesDataOffset = -1;
        public static int OffsetToCharacterAbilitiesSelected = -1;

        public static int MenuAbilitiesDataOffset = -1;
        public static int OffsetToMenuAbilitiesSelected = -1;

        public static int BattleCommandsDataOffset = -1;
        public static int OffsetToBattleCommandsSelected = -1;

        public static int RinoaCommandsDataOffset = -1;
        public static int OffsetToRinoaCommandsSelected = -1;

        public static MagicData GetSelectedMagicData;
        public static GFData GetSelectedGFData;
        public static GFAttacksData GetSelectedGFAttacksData;
        public static WeaponsData GetSelectedWeaponsData;
        public static CharactersData GetSelectedCharactersData;
        public static EnemyAttacksData GetSelectedEnemyAttacksData;
        public static BlueMagicData GetSelectedBlueMagicData;
        public static BlueMagicParamData GetSelectedBlueMagicParamData;
        public static StatPercentageAbilitiesData GetSelectedStatPercentageAbilitiesData;
        public static RenzoFinData GetSelectedRenzoFinData;
        public static TempCharLBData GetSelectedTempCharLBData;
        public static ShotData GetSelectedShotData;
        public static DuelData GetSelectedDuelData;
        public static DuelParamsData GetSelectedDuelParamsData;
        public static CombineData GetSelectedCombineData;
        public static ItemsData GetSelectedItemsData;
        public static BattleItemsData GetSelectedBattleItemsData;
        public static SlotArrayData GetSelectedSlotArrayData;
        public static SlotsSetsData GetSelectedSlotsSetsData;
        public static DevourData GetSelectedDevourData;
        public static MiscData GetSelectedMiscData;
        public static CommandAbilityDataData GetSelectedCommandAbilityDataData;
        public static CommandAbilityData GetSelectedCommandAbilityData;
        public static JunctionAbilitiesData GetSelectedJunctionAbilitiesData;
        public static PartyAbilitiesData GetSelectedPartyAbilitiesData;
        public static GFAbilitiesData GetSelectedGFAbilitiesData;
        public static CharacterAbilitiesData GetSelectedCharacterAbilitiesData;
        public static MenuAbilitiesData GetSelectedMenuAbilitiesData;
        public static BattleCommandsData GetSelectedBattleCommandsData;
        public static RinoaCommandsData GetSelectedRinoaCommandsData;

        public enum KernelSections : ushort
        {
            BattleCommands = 1 << 2,
            MagicData = 2 << 2,
            GFs = 3 << 2,
            EnemyAttacks = 4 << 2,
            Weapons = 5 << 2,
            RenzokukenFinisher = 6 << 2,
            Characters = 7 << 2,
            BattleItems = 8 << 2,
            NonBattleItems = 9 << 2,
            GFAttacks = 10 << 2,
            CommandAbilityData = 11 << 2,
            JunctionAbilities = 12 << 2,
            CommandAbilities = 13 << 2,
            StatPercentageIncreasingAbilities = 14 << 2,
            CharacterAbilities = 15 << 2,
            PartyAbilities = 16 << 2,
            GFAbilities = 17 << 2,
            MenuAbilities = 18 << 2,
            TempCharacterLimitBreakes = 19 << 2,
            BlueMagic = 20 << 2,
            BlueMagicParam = 21 << 2,
            Shot_Irvine = 22 << 2,
            Duel_Zell = 23 << 2,
            Duel_ZellParam = 24 << 2,
            RinoaLimit1 = 25 << 2,
            RinoaLimit2 = 26 << 2,
            SelphieSlotArray = 27 << 2,
            SelphieSlotsSets = 28 << 2,
            Devour = 29 << 2,
            Misc = 30 << 2,
            MiscTextPointers = 31 << 2,
            Text_BattleCommand = 32 << 2,
            Text_Magictext = 33 << 2,
            Text_JunctionableGF = 34 << 2,
            Text_Enemyattacktext = 35 << 2,
            Text_Weapontext = 36 << 2,
            Text_Renzokukenfinisherstext = 37 << 2,
            Text_Characternames = 38 << 2,
            Text_Battleitemnames = 39 << 2,
            Text_Nonbattleitemnames = 40 << 2,
            Text_NonjunctionableGFattackname = 41 << 2,
            Text_Junctionabilitiestext = 42 << 2,
            Text_Commandabilitiestext = 43 << 2,
            Text_Statpercentageincreasingabilitiestext = 44 << 2,
            Text_Characterabilitytext = 45 << 2,
            Text_Partyabilitytext = 46 << 2,
            Text_GFabilitytext = 47 << 2,
            Text_Menuabilitytext = 48 << 2,
            Text_Temporarycharacterlimitbreaktext = 49 << 2,
            Text_Bluemagictext = 50 << 2,
            Text_Shottext = 51 << 2,
            Text_Dueltext = 52 << 2,
            Text_Rinoalimitbreaktext = 53 << 2,
            Text_Rinoalimitbreaktext2 = 54 << 2,
            Text_Devourtext = 55 << 2,
            Text_Misctext = 56 << 2,
        }


        internal enum Element : byte
        {
            Fire = 0x01,
            Ice = 0x02,
            Thunder = 0x04,
            Earth = 0x08,
            Poison = 0x10,
            Wind = 0x20,
            Water = 0x40,
            Holy = 0x80,
            NonElemental = 0x00
        }

        internal enum HealDmg : byte //used in devour
        {
            Heal = 0x1E,
            Damage = 0x1F
        }

        internal enum Buttons : ushort //used in duel
        {
            None = 0xFFFF,
            EscapeBattle_D = 0x0001,
            EscapeBattle_F = 0x0002,
            RotationLeft_H = 0x0004,
            RotationRight_G= 0x0008,
            Cancel_C = 0x0010,
            Menu_V = 0x0020,
            Select_X = 0x0040,
            CardGame_S = 0x0080,
            Up = 0x1000,
            Right = 0x2000,
            Down = 0x4000,
            Left = 0x8000
        }

        internal enum StatToIncrease : byte //used in GF abilities
        {
            SumMag = 0x00,
            HP = 0x01,
            Boost = 0xFF
        }


        public struct MagicData
        {
            public string OffsetSpellName;
            public string OffsetSpellDescription;
            public UInt16 MagicID;
            public byte Unknown1;
            public byte AttackType;
            public byte SpellPower;
            public byte Unknown2;
            public byte DefaultTarget;
            public byte Flags;
            public byte DrawResist;
            public byte HitCount;
            public Element Element;
            public byte Unknown4;
            public byte StatusMagic1;
            public byte StatusMagic2;
            public byte StatusMagic3;
            public byte StatusMagic4;
            public byte StatusMagic5;
            public UInt16 Unknown5;
            public byte HP;
            public byte STR;
            public byte VIT;
            public byte MAG;
            public byte SPR;
            public byte SPD;
            public byte EVA;
            public byte HIT;
            public byte LUCK;
            public byte ElemAttackEN;
            public byte ElemAttackVAL;
            public byte ElemDefenseEN;
            public byte ElemDefenseVAL;
            public byte StatusATKval;
            public byte StatusDEFval;
            public UInt16 StatusATKEN;
            public UInt16 StatusDefEN;
            public byte StatusAttack;
            public byte QuezacoltCompatibility;
            public byte ShivaCompatibility;
            public byte IfritCompatibility;
            public byte SirenCompatibility;
            public byte BrothersCompatibility;
            public byte DiablosCompatibility;
            public byte CarbuncleCompatibility;
            public byte LeviathanCompatibility;
            public byte PandemonaCompatibility;
            public byte CerberusCompatibility;
            public byte AlexanderCompatibility;
            public byte DoomtrainCompatibility;
            public byte BahamutCompatibility;
            public byte CactuarCompatibility;
            public byte TonberryCompatibility;
            public byte EdenCompatibility;
            public byte[] Unknown6;
        }

        public struct GFData
        {
            public string OffsetGFAttackName;
            public string OffsetGFAttackDescription;
            public UInt16 GFMagicID;
            public byte GFAttackType;
            public byte GFPower;
            public byte GFFlags;
            public Element ElementGF;
            public byte StatusGF1;
            public byte StatusGF2;
            public byte StatusGF3;
            public byte StatusGF4;
            public byte StatusGF5;
            public byte GFHP;
            public byte GFEXP;
            public byte GFStatusAttack;
            public byte GFPowerMod;
            public byte GFLevelMod;
            public UInt16 GFAbility1;
            public UInt16 GFAbility2;
            public UInt16 GFAbility3;
            public UInt16 GFAbility4;
            public UInt16 GFAbility5;
            public UInt16 GFAbility6;
            public UInt16 GFAbility7;
            public UInt16 GFAbility8;
            public UInt16 GFAbility9;
            public UInt16 GFAbility10;
            public UInt16 GFAbility11;
            public UInt16 GFAbility12;
            public UInt16 GFAbility13;
            public UInt16 GFAbility14;
            public UInt16 GFAbility15;
            public UInt16 GFAbility16;
            public UInt16 GFAbility17;
            public UInt16 GFAbility18;
            public UInt16 GFAbility19;
            public UInt16 GFAbility20;
            public UInt16 GFAbility21;
            public UInt16 GFAbilityUnlock1;
            public UInt16 GFAbilityUnlock2;
            public UInt16 GFAbilityUnlock3;
            public UInt16 GFAbilityUnlock4;
            public UInt16 GFAbilityUnlock5;
            public UInt16 GFAbilityUnlock6;
            public UInt16 GFAbilityUnlock7;
            public UInt16 GFAbilityUnlock8;
            public UInt16 GFAbilityUnlock9;
            public UInt16 GFAbilityUnlock10;
            public UInt16 GFAbilityUnlock11;
            public UInt16 GFAbilityUnlock12;
            public UInt16 GFAbilityUnlock13;
            public UInt16 GFAbilityUnlock14;
            public UInt16 GFAbilityUnlock15;
            public UInt16 GFAbilityUnlock16;
            public UInt16 GFAbilityUnlock17;
            public UInt16 GFAbilityUnlock18;
            public UInt16 GFAbilityUnlock19;
            public UInt16 GFAbilityUnlock20;
            public UInt16 GFAbilityUnlock21;
            public byte GFQuezacoltCompatibility;
            public byte GFShivaCompatibility;
            public byte GFIfritCompatibility;
            public byte GFSirenCompatibility;
            public byte GFBrothersCompatibility;
            public byte GFDiablosCompatibility;
            public byte GFCarbuncleCompatibility;
            public byte GFLeviathanCompatibility;
            public byte GFPandemonaCompatibility;
            public byte GFCerberusCompatibility;
            public byte GFAlexanderCompatibility;
            public byte GFDoomtrainCompatibility;
            public byte GFBahamutCompatibility;
            public byte GFCactuarCompatibility;
            public byte GFTonberryCompatibility;
            public byte GFEdenCompatibility;
        }

        public struct GFAttacksData
        {
            public string OffsetGFAttacksName;
            public UInt16 GFAttacksMagicID;
            public byte GFAttacksAttackType;
            public byte GFAttacksPower;
            public byte GFAttacksStatus;
            public byte GFAttacksFlags;
            public Element ElementGFAttacks;
            public byte StatusGFAttacks1;
            public byte StatusGFAttacks2;
            public byte StatusGFAttacks3;
            public byte StatusGFAttacks4;
            public byte StatusGFAttacks5;
            public byte GFAttacksPowerMod;
            public byte GFAttacksLevelMod;
        }

        public struct WeaponsData
        {
            public string OffsetToName;
            public byte RenzokukenFinishers;
            public byte CharacterID;
            public byte AttackType;
            public byte AttackPower;
            public byte AttackParam;
            public byte STRBonus;
            public byte Tier;
            public byte CritBonus;
            public byte Melee;
        }

        public struct CharactersData
        {
            public string OffsetToName;
            public byte CrisisLevel;
            public byte Gender;
            public byte LimitID;
            public byte LimitParam;
            public byte EXP1;
            public byte EXP2;
            public byte HP1;
            public byte HP2;
            public byte HP3;
            public byte HP4;
            public byte STR1;
            public byte STR2;
            public byte STR3;
            public byte STR4;
            public byte VIT1;
            public byte VIT2;
            public byte VIT3;
            public byte VIT4;
            public byte MAG1;
            public byte MAG2;
            public byte MAG3;
            public byte MAG4;
            public byte SPR1;
            public byte SPR2;
            public byte SPR3;
            public byte SPR4;
            public byte SPD1;
            public byte SPD2;
            public byte SPD3;
            public byte SPD4;
            public byte LUCK1;
            public byte LUCK2;
            public byte LUCK3;
            public byte LUCK4;
        }

        public struct EnemyAttacksData
        {
            public string OffsetToName;
            public UInt16 MagicID;
            public byte CameraChange;
            public byte AttackType;
            public byte AttackPower;
            public byte AttackFlags;
            public Element Element;
            public byte StatusAttack;
            public byte AttackParam;
            public byte Status1;
            public byte Status2;
            public byte Status3;
            public byte Status4;
            public byte Status5;
        }

        public struct BlueMagicData
        {
            public string OffsetToName;
            public string OffsetToDescription;
            public UInt16 MagicID;
            public byte AttackType;
            public byte AttackFlags;
            public Element Element;
            public byte StatusAttack;
            public byte CritBonus;
        }

        public struct BlueMagicParamData
        {
            public byte Status1CL1;
            public byte Status2CL1;
            public byte Status3CL1;
            public byte Status4CL1;
            public byte Status5CL1;
            public byte AttackPowerCL1;
            public byte AttackParamCL1;
            public byte Status1CL2;
            public byte Status2CL2;
            public byte Status3CL2;
            public byte Status4CL2;
            public byte Status5CL2;
            public byte AttackPowerCL2;
            public byte AttackParamCL2;
            public byte Status1CL3;
            public byte Status2CL3;
            public byte Status3CL3;
            public byte Status4CL3;
            public byte Status5CL3;
            public byte AttackPowerCL3;
            public byte AttackParamCL3;
            public byte Status1CL4;
            public byte Status2CL4;
            public byte Status3CL4;
            public byte Status4CL4;
            public byte Status5CL4;
            public byte AttackPowerCL4;
            public byte AttackParamCL4;
        }

        public struct StatPercentageAbilitiesData
        {
            public string OffsetToName;
            public string OffsetToDescription;
            public byte StatToincrease;
            public byte IncreasementValue;
            public byte AP;
        }

        public struct RenzoFinData
        {
            public string OffsetToName;
            public string OffsetToDescription;
            public UInt16 MagicID;
            public byte AttackType;
            public byte AttackPower;
            public byte Target;
            public byte AttackFlags;
            public byte HitCount;
            public Element Element;
            public byte ElementPerc;
            public byte StatusAttack;
            public byte Status1;
            public byte Status2;
            public byte Status3;
            public byte Status4;
            public byte Status5;
        }
        
        public struct TempCharLBData
        {
            public string OffsetToName;
            public string OffsetToDescription;
            public UInt16 MagicID;
            public byte AttackType;
            public byte AttackPower;
            public byte Target;
            public byte AttackFlags;
            public byte HitCount;
            public Element Element;
            public byte ElementPerc;
            public byte StatusAttack;
            public byte Status1;
            public byte Status2;
            public byte Status3;
            public byte Status4;
            public byte Status5;
        }

        public struct ShotData
        {
            public string OffsetToName;
            public string OffsetToDescription;
            public UInt16 MagicID;
            public byte AttackType;
            public byte AttackPower;
            public byte Target;
            public byte AttackFlags;
            public byte HitCount;
            public Element Element;
            public byte ElementPerc;
            public byte StatusAttack;
            public byte CritBonus;
            public byte Status1;
            public byte Status2;
            public byte Status3;
            public byte Status4;
            public byte Status5;
            public byte UsedItem;
        }

        public struct DuelData
        {
            public string OffsetToName;
            public string OffsetToDescription;
            public UInt16 MagicID;
            public byte AttackType;
            public byte AttackPower;
            public byte AttackFlags;
            public byte Target;
            public byte HitCount;
            public Element Element;
            public byte ElementPerc;
            public byte StatusAttack;
            public byte Status1;
            public byte Status2;
            public byte Status3;
            public byte Status4;
            public byte Status5;
            public Buttons Button1;
            public UInt16 IsFinisher;
            public Buttons Button2;
            public Buttons Button3;
            public Buttons Button4;
            public Buttons Button5;
        }

        public struct DuelParamsData
        {
            public byte StartMove0;
            public byte StartMove1;
            public byte StartMove2;
            public byte StartMove3;
            public byte StartMove4;
            public byte StartMove5;
            public byte StartMove6;
            public byte StartMove7;
            public byte StartMove8;
            public byte StartMove9;
            public byte StartMove10;
            public byte StartMove11;
            public byte StartMove12;
            public byte StartMove13;
            public byte StartMove14;
            public byte StartMove15;
            public byte StartMove16;
            public byte StartMove17;
            public byte StartMove18;
            public byte StartMove19;
            public byte StartMove20;
            public byte StartMove21;
            public byte StartMove22;
            public byte StartMove23;
            public byte StartMove24;
            public byte NextSeq0_1;
            public byte NextSeq0_2;
            public byte NextSeq0_3;
            public byte NextSeq1_1;
            public byte NextSeq1_2;
            public byte NextSeq1_3;
            public byte NextSeq2_1;
            public byte NextSeq2_2;
            public byte NextSeq2_3;
            public byte NextSeq3_1;
            public byte NextSeq3_2;
            public byte NextSeq3_3;
            public byte NextSeq4_1;
            public byte NextSeq4_2;
            public byte NextSeq4_3;
            public byte NextSeq5_1;
            public byte NextSeq5_2;
            public byte NextSeq5_3;
            public byte NextSeq6_1;
            public byte NextSeq6_2;
            public byte NextSeq6_3;
            public byte NextSeq7_1;
            public byte NextSeq7_2;
            public byte NextSeq7_3;
            public byte NextSeq8_1;
            public byte NextSeq8_2;
            public byte NextSeq8_3;
            public byte NextSeq9_1;
            public byte NextSeq9_2;
            public byte NextSeq9_3;
            public byte NextSeq10_1;
            public byte NextSeq10_2;
            public byte NextSeq10_3;
            public byte NextSeq11_1;
            public byte NextSeq11_2;
            public byte NextSeq11_3;
            public byte NextSeq12_1;
            public byte NextSeq12_2;
            public byte NextSeq12_3;
            public byte NextSeq13_1;
            public byte NextSeq13_2;
            public byte NextSeq13_3;
            public byte NextSeq14_1;
            public byte NextSeq14_2;
            public byte NextSeq14_3;
            public byte NextSeq15_1;
            public byte NextSeq15_2;
            public byte NextSeq15_3;
            public byte NextSeq16_1;
            public byte NextSeq16_2;
            public byte NextSeq16_3;
            public byte NextSeq17_1;
            public byte NextSeq17_2;
            public byte NextSeq17_3;
            public byte NextSeq18_1;
            public byte NextSeq18_2;
            public byte NextSeq18_3;
            public byte NextSeq19_1;
            public byte NextSeq19_2;
            public byte NextSeq19_3;
            public byte NextSeq20_1;
            public byte NextSeq20_2;
            public byte NextSeq20_3;
            public byte NextSeq21_1;
            public byte NextSeq21_2;
            public byte NextSeq21_3;
            public byte NextSeq22_1;
            public byte NextSeq22_2;
            public byte NextSeq22_3;
            public byte NextSeq23_1;
            public byte NextSeq23_2;
            public byte NextSeq23_3;
            public byte NextSeq24_1;
            public byte NextSeq24_2;
            public byte NextSeq24_3;
        }

        public struct CombineData
        {
            public string OffsetToName;
            public UInt16 MagicID;
            public byte AttackType;
            public byte AttackPower;
            public byte Target;
            public byte AttackFlags;
            public byte HitCount;
            public Element Element;
            public byte ElementPerc;
            public byte StatusAttack;
            public byte Status1;
            public byte Status2;
            public byte Status3;
            public byte Status4;
            public byte Status5;
        }

        public struct BattleItemsData
        {
            public string OffsetToName;
            public string OffsetToDescription;
            public UInt16 MagicID;
            public byte AttackType;
            public byte AttackPower;
            public byte Target;
            public byte AttackFlags;
            public byte StatusAttack;
            public byte Status1;
            public byte Status2;
            public byte Status3;
            public byte Status4;
            public byte Status5;
            public byte AttackParam;
            public byte HitCount;
            public Element Element;
        }
    
        public struct ItemsData
        {
            public string OffsetToName;
            public string OffsetToDescription;
        }

        public struct SlotArrayData
        {
            public byte SlotArray1;
            public byte SlotArray2;
            public byte SlotArray3;
            public byte SlotArray4;
            public byte SlotArray5;
            public byte SlotArray6;
            public byte SlotArray7;
            public byte SlotArray8;
            public byte SlotArray9;
            public byte SlotArray10;
            public byte SlotArray11;
            public byte SlotArray12;
            public byte SlotArray13;
            public byte SlotArray14;
            public byte SlotArray15;
            public byte SlotArray16;
            public byte SlotArray17;
            public byte SlotArray18;
            public byte SlotArray19;
            public byte SlotArray20;
            public byte SlotArray21;
            public byte SlotArray22;
            public byte SlotArray23;
            public byte SlotArray24;
            public byte SlotArray25;
            public byte SlotArray26;
            public byte SlotArray27;
            public byte SlotArray28;
            public byte SlotArray29;
            public byte SlotArray30;
            public byte SlotArray31;
            public byte SlotArray32;
            public byte SlotArray33;
            public byte SlotArray34;
            public byte SlotArray35;
            public byte SlotArray36;
            public byte SlotArray37;
            public byte SlotArray38;
            public byte SlotArray39;
            public byte SlotArray40;
            public byte SlotArray41;
            public byte SlotArray42;
            public byte SlotArray43;
            public byte SlotArray44;
            public byte SlotArray45;
            public byte SlotArray46;
            public byte SlotArray47;
            public byte SlotArray48;
            public byte SlotArray49;
            public byte SlotArray50;
            public byte SlotArray51;
            public byte SlotArray52;
            public byte SlotArray53;
            public byte SlotArray54;
            public byte SlotArray55;
            public byte SlotArray56;
            public byte SlotArray57;
            public byte SlotArray58;
            public byte SlotArray59;
            public byte SlotArray60;
        }

        public struct SlotsSetsData
        {
            public byte Magic1;
            public byte Count1;
            public byte Magic2;
            public byte Count2;
            public byte Magic3;
            public byte Count3;
            public byte Magic4;
            public byte Count4;
            public byte Magic5;
            public byte Count5;
            public byte Magic6;
            public byte Count6;
            public byte Magic7;
            public byte Count7;
            public byte Magic8;
            public byte Count8;
        }

        public struct DevourData
        {
            public string OffsetToDescription;
            public HealDmg HealDmg;
            public byte HpQuantity;
            public byte Status2;
            public byte Status3;
            public byte Status4;
            public byte Status5;
            public byte Status1;
            public byte RaisedStat;
            public byte RaisedHP;
        }

        public struct MiscData
        {
            public byte StatusTimer1;
            public byte StatusTimer2;
            public byte StatusTimer3;
            public byte StatusTimer4;
            public byte StatusTimer5;
            public byte StatusTimer6;
            public byte StatusTimer7;
            public byte StatusTimer8;
            public byte StatusTimer9;
            public byte StatusTimer10;
            public byte StatusTimer11;
            public byte StatusTimer12;
            public byte StatusTimer13;
            public byte StatusTimer14;
            public byte ATB;
            public byte DeadTimer;
            public byte StatusLimit1;
            public byte StatusLimit2;
            public byte StatusLimit3;
            public byte StatusLimit4;
            public byte StatusLimit5;
            public byte StatusLimit6;
            public byte StatusLimit7;
            public byte StatusLimit8;
            public byte StatusLimit9;
            public byte StatusLimit10;
            public byte StatusLimit11;
            public byte StatusLimit12;
            public byte StatusLimit13;
            public byte StatusLimit14;
            public byte StatusLimit15;
            public byte StatusLimit16;
            public byte StatusLimit17;
            public byte StatusLimit18;
            public byte StatusLimit19;
            public byte StatusLimit20;
            public byte StatusLimit21;
            public byte StatusLimit22;
            public byte StatusLimit23;
            public byte StatusLimit24;
            public byte StatusLimit25;
            public byte StatusLimit26;
            public byte StatusLimit27;
            public byte StatusLimit28;
            public byte StatusLimit29;
            public byte StatusLimit30;
            public byte StatusLimit31;
            public byte StatusLimit32;
            public byte DuelTimerCL1;
            public byte DuelSeqCL1;
            public byte DuelTimerCL2;
            public byte DuelSeqCL2;
            public byte DuelTimerCL3;
            public byte DuelSeqCL3;
            public byte DuelTimerCL4;
            public byte DuelSeqCL4;
            public byte ShotTimerCL1;
            public byte ShotTimerCL2;
            public byte ShotTimerCL3;
            public byte ShotTimerCL4;
        }

        public struct CommandAbilityDataData
        {
            public UInt16 MagicID;
            public byte AttackType;
            public byte AttackPower;
            public byte AttackFlags;
            public byte HitCount;
            public Element Element;
            public byte StatusAttack;
            public byte Status1;
            public byte Status2;
            public byte Status3;
            public byte Status4;
            public byte Status5;
        }

        public struct CommandAbilityData
        {
            public string OffsetToName;
            public string OffsetToDescription;
            public byte AP;
            public byte BattleCommand;
        }

        public struct JunctionAbilitiesData
        {
            public string OffsetToName;
            public string OffsetToDescription;
            public byte AP;
            public byte Flag1;
            public byte Flag2;
            public byte Flag3;
        }

        public struct PartyAbilitiesData
        {
            public string OffsetToName;
            public string OffsetToDescription;
            public byte AP;
            public byte Flag;
        }

        public struct GFAbilitiesData
        {
            public string OffsetToName;
            public string OffsetToDescription;
            public byte AP;
            public byte EnableBoost;
            public StatToIncrease StatToIncrease;
            public byte IncrementValue;

        }

        public struct CharacterAbilitiesData
        {
            public string OffsetToName;
            public string OffsetToDescription;
            public byte AP;
            public byte Flag1;
            public byte Flag2;
            public byte Flag3;
        }

        public struct MenuAbilitiesData
        {
            public string OffsetToName;
            public string OffsetToDescription;
            public byte AP;
            public byte Index;
            public byte StartEntry;
            public byte EndEntry;
        }

        public struct BattleCommandsData
        {
            public string OffsetToName;
            public string OffsetToDescription;
            public byte AbilityID;
            public byte Flag;
            public byte Target;
        }

        public struct RinoaCommandsData
        {
            public string OffsetToName;
            public string OffsetToDescription;
            public byte Flag;
            public byte Target;
            public byte AbilityID;
        }

        #endregion


        #region WRITE KERNEL VARIABLES

        #region MAGIC

        public static void UpdateVariable_Magic(int index, object variable, byte arg0 = 127, object sender = null)
        {
            if (!mainForm._loaded || Kernel == null)
                return;
            switch (index)
            {
                case 2:
                    {
                        UshortToKernel(Convert.ToUInt16(variable), 4, (byte)Mode.Mode_Magic); //MagicID
                        return;
                    }
                case 3:
                    {
                        Kernel[OffsetToMagicSelected + 8] = Convert.ToByte(variable); //SpellPower
                        return;
                    }
                case 4:
                    {
                        Kernel[OffsetToMagicSelected + 12] = Convert.ToByte(variable); //drawResist
                        return;
                    }
                case 5:
                    {
                        Kernel[OffsetToMagicSelected + 14] = Convert.ToByte(variable); //Element
                        return;
                    }
                case 6:
                    {
                        MagicStatusUpdator(arg0, variable); //Status
                        return;
                    }

                case 7:
                    {
                        Kernel[OffsetToMagicSelected + 23] = Convert.ToByte(variable); //HP
                        return;
                    }

                case 8:
                    {
                        Kernel[OffsetToMagicSelected + 24] = Convert.ToByte(variable); //STR
                        return;
                    }

                case 9:
                    {
                        Kernel[OffsetToMagicSelected + 25] = Convert.ToByte(variable); //VIT
                        return;
                    }

                case 10:
                    {
                        Kernel[OffsetToMagicSelected + 26] = Convert.ToByte(variable); //MAG
                        return;
                    }

                case 11:
                    {
                        Kernel[OffsetToMagicSelected + 27] = Convert.ToByte(variable); //SPR
                        return;
                    }

                case 12:
                    {
                        Kernel[OffsetToMagicSelected + 28] = Convert.ToByte(variable); //SPD
                        return;
                    }

                case 13:
                    {
                        Kernel[OffsetToMagicSelected + 29] = Convert.ToByte(variable); //EVA
                        return;
                    }

                case 14:
                    {
                        Kernel[OffsetToMagicSelected + 30] = Convert.ToByte(variable); //HITJ
                        return;
                    }

                case 15:
                    {
                        Kernel[OffsetToMagicSelected + 31] = Convert.ToByte(variable); //LUCK
                        return;
                    }

                case 16:
                    {
                        Kernel[OffsetToMagicSelected + 32] = Convert.ToByte(variable); // J-Elem attack enabler
                        return;
                    }

                case 17:
                    {
                        Kernel[OffsetToMagicSelected + 13] = Convert.ToByte(variable); //HIT [It's good, leave it]
                        return;
                    }

                case 18:
                    {
                        Kernel[OffsetToMagicSelected + 33] = Convert.ToByte(variable); //Characters J-Elem attack value
                        return;
                    }

                case 19:
                    {
                        Kernel[OffsetToMagicSelected + 34] ^= Convert.ToByte(variable); //Elemental Defense Modifier XOR!!!!!!!!!!
                        return;
                    }

                case 20:
                    {
                        Kernel[OffsetToMagicSelected + 35] = Convert.ToByte(variable); //Elemental Defense Value
                        return;
                    }

                case 21:
                    {
                        ushort a = BitConverter.ToUInt16(Kernel, OffsetToMagicSelected + 38);
                        byte[] temp = BitConverter.GetBytes(a ^= Convert.ToUInt16(variable));
                        Array.Copy(temp, 0, Kernel, OffsetToMagicSelected + 38, 2);
                        return;
                    }

                case 22:
                    {
                        ushort a = BitConverter.ToUInt16(Kernel, OffsetToMagicSelected + 40);
                        byte[] temp = BitConverter.GetBytes(a ^= Convert.ToUInt16(variable));
                        Array.Copy(temp, 0, Kernel, OffsetToMagicSelected + 40, 2);
                        return;
                    }

                case 23:
                    {
                        Kernel[OffsetToMagicSelected + 36] = Convert.ToByte(variable); //J-Status Attack Value
                        return;
                    }

                case 24:
                    {
                        Kernel[OffsetToMagicSelected + 37] = Convert.ToByte(variable); //J-Status Defense Value
                        return;
                    }
                case 25:
                    {
                        Kernel[OffsetToMagicSelected + 22] = Convert.ToByte(variable); //Status Attack 
                        return;
                    }
                case 26:
                    {
                        Kernel[OffsetToMagicSelected + 42] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Quezacolt Compatibility
                        return;
                    }
                case 27:
                    {
                        Kernel[OffsetToMagicSelected + 43] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Shiva Compatibility
                        return;
                    }
                case 28:
                    {
                        Kernel[OffsetToMagicSelected + 44] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Ifrit Compatibility
                        return;
                    }
                case 29:
                    {
                        Kernel[OffsetToMagicSelected + 45] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Siren Compatibility
                        return;
                    }
                case 30:
                    {
                        Kernel[OffsetToMagicSelected + 46] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Brothers Compatibility
                        return;
                    }
                case 31:
                    {
                        Kernel[OffsetToMagicSelected + 47] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Diablos Compatibility
                        return;
                    }
                case 32:
                    {
                        Kernel[OffsetToMagicSelected + 48] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Carbuncle Compatibility
                        return;
                    }
                case 33:
                    {
                        Kernel[OffsetToMagicSelected + 49] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Leviathan Compatibility
                        return;
                    }
                case 34:
                    {
                        Kernel[OffsetToMagicSelected + 50] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Pandemona Compatibility
                        return;
                    }
                case 35:
                    {
                        Kernel[OffsetToMagicSelected + 51] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Cerberus Compatibility
                        return;
                    }
                case 36:
                    {
                        Kernel[OffsetToMagicSelected + 52] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Alexander Compatibility
                        return;
                    }
                case 37:
                    {
                        Kernel[OffsetToMagicSelected + 53] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Doomtrain Compatibility
                        return;
                    }
                case 38:
                    {
                        Kernel[OffsetToMagicSelected + 54] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Bahamut Compatibility
                        return;
                    }
                case 39:
                    {
                        Kernel[OffsetToMagicSelected + 55] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Cactuar Compatibility
                        return;
                    }
                case 40:
                    {
                        Kernel[OffsetToMagicSelected + 56] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Tonberry Compatibility
                        return;
                    }
                case 41:
                    {
                        Kernel[OffsetToMagicSelected + 57] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Eden Compatibility
                        return;
                    }
                case 42:
                    {
                        Kernel[OffsetToMagicSelected + 10] = (byte)(Kernel[OffsetToMagicSelected + 10] ^ Convert.ToByte(variable)); //default target
                        return;
                    }
                case 43:
                    {
                        Kernel[OffsetToMagicSelected + 7] = Convert.ToByte(variable); //attack type
                        return;
                    }
                case 44:
                    {
                        Kernel[OffsetToMagicSelected + 11] ^= Convert.ToByte(variable); //flags
                        return;
                    }
                case 45:
                    {//name
                        if (sender == null) //NullReferenceException handling
                            return;
                        if (GetSelectedMagicData.OffsetSpellName.Length == (sender as TextBox).Text.Length)
                            //if no moving pointers
                        {
                            int offset = BitConverter.ToInt32(Kernel, (int) KernelSections.Text_Magictext) +
                                         BitConverter.ToUInt16(Kernel, OffsetToMagicSelected);
                            byte[] buffer = FF8Text.Cipher((sender as TextBox).Text);
                            for (int i = 0; i != buffer.Length; i++)
                                Kernel[i + offset] = buffer[i];
                        }
                        else
                        {
                            ; //TODO 
                        }
                        return;
                    }
                case 46:
                    {
                        //description
                        if (sender == null) //NullReferenceException handling
                            return;
                        if (GetSelectedMagicData.OffsetSpellDescription.Length == (sender as TextBox).Text.Length)
                        {
                            int offset = BitConverter.ToInt32(Kernel, (int)KernelSections.Text_Magictext) +
                                         BitConverter.ToUInt16(Kernel, OffsetToMagicSelected+2);
                            byte[] buffer = FF8Text.Cipher((sender as TextBox).Text);
                            for (int i = 0; i != buffer.Length; i++)
                                Kernel[i + offset] = buffer[i];
                        }
                        else
                        {
                            ; //TODO 
                        }
                        return;
                    }

                default:
                    return;
            }

        }
        public static void MagicStatusUpdator(byte StatusByteIndex, object variable)
        {
            switch (StatusByteIndex)
            {
                case 0:
                    Kernel[OffsetToMagicSelected + 16] = (byte)(Kernel[OffsetToMagicSelected + 16] ^ Convert.ToByte(variable)); //Perform XOR logic for this 
                    return;
                case 1:
                    Kernel[OffsetToMagicSelected + 17] = (byte)(Kernel[OffsetToMagicSelected + 17] ^ Convert.ToByte(variable));
                    return;
                case 2:
                    Kernel[OffsetToMagicSelected + 18] = (byte)(Kernel[OffsetToMagicSelected + 18] ^ Convert.ToByte(variable));
                    return;
                case 3:
                    Kernel[OffsetToMagicSelected + 19] = (byte)(Kernel[OffsetToMagicSelected + 19] ^ Convert.ToByte(variable));
                    return;
                case 4:
                    Kernel[OffsetToMagicSelected + 20] = (byte)(Kernel[OffsetToMagicSelected + 20] ^ Convert.ToByte(variable));
                    return;
            }
        }

        #endregion

        #region J-GF

        public static void UpdateVariable_GF(int index, object variable, byte AbilityIndex = 0, byte arg0 = 127, object sender = null)
        {
            if (!mainForm._loaded || Kernel == null)
                return;
            switch (index)
            {
                case 0:
                    UshortToKernel(Convert.ToUInt16(variable), 4, (byte)Mode.Mode_GF); //MagicID
                    return;
                case 1:
                    Kernel[OffsetToGFSelected + 7] = Convert.ToByte(variable); //GFPower
                    return;
                case 2:
                    Kernel[OffsetToGFSelected + 13] = Convert.ToByte(variable); //Element
                    return;
                case 3:
                    Kernel[OffsetToGFSelected + 20] = Convert.ToByte(variable); //GFHP
                    return;
                case 4:
                    Kernel[OffsetToGFSelected + 130] = Convert.ToByte(variable); //Power Mod
                    return;
                case 5:
                    Kernel[OffsetToGFSelected + 131] = Convert.ToByte(variable); //Level Mod
                    return;
                case 6:
                    Kernel[OffsetToGFSelected + 30 + (AbilityIndex * 4)] = Convert.ToByte(variable); //GF abilities
                    return;
                case 7:
                    GFStatusUpdator(arg0, variable); //Status
                    return;
                case 8:
                    Kernel[OffsetToGFSelected + 27] = Convert.ToByte(variable); //enable modifier
                    return;
                case 9: //Reset Status
                    Kernel[OffsetToGFSelected + 14] = 0x00;
                    Kernel[OffsetToGFSelected + 16] = 0x00;
                    Kernel[OffsetToGFSelected + 17] = 0x00;
                    Kernel[OffsetToGFSelected + 18] = 0x00;
                    Kernel[OffsetToGFSelected + 19] = 0x00;
                    return;
                case 10:
                        Kernel[OffsetToGFSelected + 112] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Quezacolt Compatibility
                        return;
                case 11:
                        Kernel[OffsetToGFSelected + 113] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Shiva Compatibility
                        return;
                case 12:
                        Kernel[OffsetToGFSelected + 114] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Ifrit Compatibility
                        return;
                case 13:
                        Kernel[OffsetToGFSelected + 115] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Siren Compatibility
                        return;
                case 14:
                        Kernel[OffsetToGFSelected + 116] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Brothers Compatibility
                        return;
                case 15:
                        Kernel[OffsetToGFSelected + 117] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Diablos Compatibility
                        return;
                case 16:
                        Kernel[OffsetToGFSelected + 118] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Carbuncle Compatibility
                        return;
                case 17:
                        Kernel[OffsetToGFSelected + 119] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Leviathan Compatibility
                        return;
                case 18:
                        Kernel[OffsetToGFSelected + 120] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Pandemona Compatibility
                        return;
                case 19:
                        Kernel[OffsetToGFSelected + 121] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Cerberus Compatibility
                        return;
                case 20:
                        Kernel[OffsetToGFSelected + 122] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Alexander Compatibility
                        return;
                case 21:
                        Kernel[OffsetToGFSelected + 123] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Doomtrain Compatibility
                        return;
                case 22:
                        Kernel[OffsetToGFSelected + 124] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Bahamut Compatibility
                        return;
                case 23:
                        Kernel[OffsetToGFSelected + 125] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Cactuar Compatibility
                        return;
                case 24:
                        Kernel[OffsetToGFSelected + 126] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Tonberry Compatibility
                        return;
                case 25:
                        Kernel[OffsetToGFSelected + 127] = Convert.ToByte(100 - 5 * Convert.ToDecimal(variable)); //Eden Compatibility
                        return;
                case 26:
                        Kernel[OffsetToGFSelected + 6] = Convert.ToByte(variable); //attack type
                        return;
                case 27:
                        Kernel[OffsetToGFSelected + 10] ^= Convert.ToByte(variable); //flags
                        return;
                case 28:
                        Kernel[OffsetToGFSelected + 28 + (AbilityIndex * 4)] = Convert.ToByte(variable); //GF abilities
                        return;
                case 29:
                        Kernel[OffsetToGFSelected + 24] = (byte)(Convert.ToInt16(variable) / 10); //GFEXP
                        return;
                case 30:
                    //Name
                    return;
                case 31:
                    //Description
                    return;

                default:
                    return;
            }
        }
        private static void GFStatusUpdator(byte StatusByteIndex, object variable)
        {
            switch (StatusByteIndex)
            {
                case 0:
                    Kernel[OffsetToGFSelected + 14] = (byte)(Kernel[OffsetToGFSelected + 14] ^ Convert.ToByte(variable)); //Perform XOR logic for this 
                    return;      
                case 1:          
                    Kernel[OffsetToGFSelected + 16] = (byte)(Kernel[OffsetToGFSelected + 16] ^ Convert.ToByte(variable));
                    return;     
                case 2:         
                    Kernel[OffsetToGFSelected + 17] = (byte)(Kernel[OffsetToGFSelected + 17] ^ Convert.ToByte(variable));
                    return;     
                case 3:         
                    Kernel[OffsetToGFSelected + 18] = (byte)(Kernel[OffsetToGFSelected + 18] ^ Convert.ToByte(variable));
                    return;       
                case 4:           
                    Kernel[OffsetToGFSelected + 19] = (byte)(Kernel[OffsetToGFSelected + 19] ^ Convert.ToByte(variable));
                    return;
            }
        }

        #endregion

        #region NJ-GF

        public static void UpdateVariable_GFAttacks(int index, object variable, byte arg0 = 127, object sender = null)
        {
            if (!mainForm._loaded || Kernel == null)
                return;
            switch (index)
            {
                case 0:
                    UshortToKernel(Convert.ToUInt16(variable), 2, (byte)Mode.Mode_GFAttacks); //MagicID
                    return;
                case 1:
                    Kernel[OffsetToGFAttacksSelected + 5] = Convert.ToByte(variable); //GFPower
                    return;
                case 2:
                    Kernel[OffsetToGFAttacksSelected + 6] = Convert.ToByte(variable); //enable modifier
                    return;
                case 3:
                    Kernel[OffsetToGFAttacksSelected + 11] = Convert.ToByte(variable); //Element
                    return;
                case 4:
                    GFAttacksStatusUpdator(arg0, variable); //Status
                    return;
                case 5:
                    Kernel[OffsetToGFAttacksSelected + 18] = Convert.ToByte(variable); //Power Mod
                    return;
                case 6:
                    Kernel[OffsetToGFAttacksSelected + 19] = Convert.ToByte(variable); //Level Mod
                    return;
                case 7: //Reset Status
                    Kernel[OffsetToGFAttacksSelected + 12] = 0x00;
                    Kernel[OffsetToGFAttacksSelected + 13] = 0x00;
                    Kernel[OffsetToGFAttacksSelected + 14] = 0x00;
                    Kernel[OffsetToGFAttacksSelected + 15] = 0x00;
                    Kernel[OffsetToGFAttacksSelected + 16] = 0x00;
                    return;
                case 8:
                    Kernel[OffsetToGFAttacksSelected + 4] = Convert.ToByte(variable); //attack type
                    return;
                case 9:
                    Kernel[OffsetToGFAttacksSelected + 8] ^= Convert.ToByte(variable); //flags
                    return;
                case 10:
                    //Name
                    return;

                default:
                    return;
            }
        }
        private static void GFAttacksStatusUpdator(byte StatusByteIndex, object variable)
        {
            switch (StatusByteIndex)
            {
                case 0:
                    Kernel[OffsetToGFAttacksSelected + 12] = (byte)(Kernel[OffsetToGFAttacksSelected + 12] ^ Convert.ToByte(variable)); //Perform XOR logic for this 
                    return;
                case 1:
                    Kernel[OffsetToGFAttacksSelected + 13] = (byte)(Kernel[OffsetToGFAttacksSelected + 13] ^ Convert.ToByte(variable));
                    return;
                case 2:
                    Kernel[OffsetToGFAttacksSelected + 14] = (byte)(Kernel[OffsetToGFAttacksSelected + 14] ^ Convert.ToByte(variable));
                    return;
                case 3:
                    Kernel[OffsetToGFAttacksSelected + 15] = (byte)(Kernel[OffsetToGFAttacksSelected + 15] ^ Convert.ToByte(variable));
                    return;
                case 4:
                    Kernel[OffsetToGFAttacksSelected + 16] = (byte)(Kernel[OffsetToGFAttacksSelected + 16] ^ Convert.ToByte(variable));
                    return;
            }
        }

        #endregion

        #region WEAPONS

        public static void UpdateVariable_Weapons(int index, object variable, byte arg0 = 127, object sender = null)
        {
            if (!mainForm._loaded || Kernel == null)
                return;
            switch (index)
            {
                case 0:
                    RenzokukenFinishersUpdator(arg0, variable); //renzokuken finishers
                    return;
                case 1:
                    Kernel[OffsetToWeaponsSelected + 4] = Convert.ToByte(variable); //character id
                    return;
                case 2:
                    Kernel[OffsetToWeaponsSelected + 5] = Convert.ToByte(variable); //attack type
                    return;
                case 3:
                    Kernel[OffsetToWeaponsSelected + 6] = Convert.ToByte(variable); //attack power
                    return;
                case 4:
                    Kernel[OffsetToWeaponsSelected + 7] = Convert.ToByte(variable); //hit bonus
                    return;
                case 5:
                    Kernel[OffsetToWeaponsSelected + 8] = Convert.ToByte(variable); //str bonus
                    return;
                case 6:
                    Kernel[OffsetToWeaponsSelected + 9] = Convert.ToByte(variable); //weapon tier
                    return;
                case 7:
                    Kernel[OffsetToWeaponsSelected + 10] = Convert.ToByte(variable); //crit bonus
                    return;
                case 8:
                    Kernel[OffsetToWeaponsSelected + 11] = Convert.ToByte(variable); //melee weapon?
                    return;
                case 9:
                    //Name
                    return;

                default:
                    return;
            }
        }
        private static void RenzokukenFinishersUpdator(byte FinisherByteIndex, object variable)
        {
            switch (FinisherByteIndex)
            {
                case 0:
                    Kernel[OffsetToWeaponsSelected + 2] = (byte)(Kernel[OffsetToWeaponsSelected + 2] ^ Convert.ToByte(variable)); //Perform XOR logic for this 
                    return;
            }
        }

        #endregion

        #region CHARACTERS

        public static void UpdateVariable_Characters(int index, object variable, object sender = null)
        {
            if (!mainForm._loaded || Kernel == null)
                return;
            switch (index)
            {
                case 0:
                    Kernel[OffsetToCharactersSelected + 2] = Convert.ToByte(variable); //crisis level mult
                    return;
                case 1:
                    Kernel[OffsetToCharactersSelected + 3] = Convert.ToByte(variable); //gender
                    return;
                case 2:
                    Kernel[OffsetToCharactersSelected + 4] = Convert.ToByte(variable); //limit id
                    return;
                case 3:
                    Kernel[OffsetToCharactersSelected + 5] = Convert.ToByte(variable); //limit param
                    return;
                case 4:
                    Kernel[OffsetToCharactersSelected + 6] = Convert.ToByte(variable); //exp 1
                    return;
                case 5:
                    Kernel[OffsetToCharactersSelected + 7] = Convert.ToByte(variable); //exp 2
                    return;
                case 6:
                    Kernel[OffsetToCharactersSelected + 8] = Convert.ToByte(variable); //hp 1
                    return;
                case 7:
                    Kernel[OffsetToCharactersSelected + 9] = Convert.ToByte(variable); //hp 2
                    return;
                case 8:
                    Kernel[OffsetToCharactersSelected + 10] = Convert.ToByte(variable); //hp 3
                    return;
                case 9:
                    Kernel[OffsetToCharactersSelected + 11] = Convert.ToByte(variable); //hp 4
                    return;
                case 10:
                    Kernel[OffsetToCharactersSelected + 12] = Convert.ToByte(variable); //str 1
                    return;
                case 11:
                    Kernel[OffsetToCharactersSelected + 13] = Convert.ToByte(variable); //str 2
                    return;
                case 12:
                    Kernel[OffsetToCharactersSelected + 14] = Convert.ToByte(variable); //str 3
                    return;
                case 13:
                    Kernel[OffsetToCharactersSelected + 15] = Convert.ToByte(variable); //str 4
                    return;
                case 14:
                    Kernel[OffsetToCharactersSelected + 16] = Convert.ToByte(variable); //vit 1
                    return;
                case 15:
                    Kernel[OffsetToCharactersSelected + 17] = Convert.ToByte(variable); //vit 2
                    return;
                case 16:
                    Kernel[OffsetToCharactersSelected + 18] = Convert.ToByte(variable); //vit 3
                    return;
                case 17:
                    Kernel[OffsetToCharactersSelected + 19] = Convert.ToByte(variable); //vit 4
                    return;
                case 18:
                    Kernel[OffsetToCharactersSelected + 20] = Convert.ToByte(variable); //mag 1
                    return;
                case 19:
                    Kernel[OffsetToCharactersSelected + 21] = Convert.ToByte(variable); //mag 2
                    return;
                case 20:
                    Kernel[OffsetToCharactersSelected + 22] = Convert.ToByte(variable); //mag 3
                    return;
                case 21:
                    Kernel[OffsetToCharactersSelected + 23] = Convert.ToByte(variable); //mag 4
                    return;
                case 22:
                    Kernel[OffsetToCharactersSelected + 24] = Convert.ToByte(variable); //spr 1
                    return;
                case 23:
                    Kernel[OffsetToCharactersSelected + 25] = Convert.ToByte(variable); //spr 2
                    return;
                case 24:
                    Kernel[OffsetToCharactersSelected + 26] = Convert.ToByte(variable); //spr 3
                    return;
                case 25:
                    Kernel[OffsetToCharactersSelected + 27] = Convert.ToByte(variable); //spr 4
                    return;
                case 26:
                    Kernel[OffsetToCharactersSelected + 28] = Convert.ToByte(variable); //spd 1
                    return;
                case 27:
                    Kernel[OffsetToCharactersSelected + 29] = Convert.ToByte(variable); //spd 2
                    return;
                case 28:
                    Kernel[OffsetToCharactersSelected + 30] = Convert.ToByte(variable); //spd 3
                    return;
                case 29:
                    Kernel[OffsetToCharactersSelected + 31] = Convert.ToByte(variable); //spd 4
                    return;
                case 30:
                    Kernel[OffsetToCharactersSelected + 32] = Convert.ToByte(variable); //luck 1
                    return;
                case 31:
                    Kernel[OffsetToCharactersSelected + 33] = Convert.ToByte(variable); //luck 2
                    return;
                case 32:
                    Kernel[OffsetToCharactersSelected + 34] = Convert.ToByte(variable); //luck 3
                    return;
                case 33:
                    Kernel[OffsetToCharactersSelected + 35] = Convert.ToByte(variable); //luck 4
                    return;
                case 34:
                    //name
                    return;

                default:
                    return;
            }
        }

        #endregion

        #region ENEMY ATTACK

        public static void UpdateVariable_EnemyAttacks(int index, object variable, byte arg0 = 127, object sender = null)
        {
            if (!mainForm._loaded || Kernel == null)
                return;
            switch (index)
            {
                case 0:
                    UshortToKernel(Convert.ToUInt16(variable), 2, (byte)Mode.Mode_EnemyAttacks); //MagicID
                    return;
                case 1:
                    Kernel[OffsetToEnemyAttacksSelected + 4] = (byte)((Kernel[OffsetToEnemyAttacksSelected + 4] & 0x80) | Convert.ToByte(variable)); //camera change spinner
                    return;
                case 2:
                    Kernel[OffsetToEnemyAttacksSelected + 4] ^= 0x80; //camera change tickbox
                    return;
                case 3:
                    Kernel[OffsetToEnemyAttacksSelected + 6] = Convert.ToByte(variable); //attack type
                    return;
                case 4:
                    Kernel[OffsetToEnemyAttacksSelected + 7] = Convert.ToByte(variable); //attack power
                    return;
                case 5:
                    Kernel[OffsetToEnemyAttacksSelected + 8] ^= Convert.ToByte(variable); //attack flags
                    return;
                case 6:
                    Kernel[OffsetToEnemyAttacksSelected + 10] = Convert.ToByte(variable); //element
                    return;
                case 7:
                    Kernel[OffsetToEnemyAttacksSelected + 12] = Convert.ToByte(variable); //status attack
                    return;
                case 8:
                    Kernel[OffsetToEnemyAttacksSelected + 13] = Convert.ToByte(variable); //attack param
                    return;
                case 9:
                    EnemyAttacksStatusUpdator(arg0, variable); //Status
                    return;
                case 10:
                    //Name
                    return;

                default:
                    return;
            }
        }
        private static void EnemyAttacksStatusUpdator(byte StatusByteIndex, object variable)
        {
            switch (StatusByteIndex)
            {
                case 0:
                    Kernel[OffsetToEnemyAttacksSelected + 14] = (byte)(Kernel[OffsetToEnemyAttacksSelected + 14] ^ Convert.ToByte(variable));
                    return;
                case 1:
                    Kernel[OffsetToEnemyAttacksSelected + 16] = (byte)(Kernel[OffsetToEnemyAttacksSelected + 16] ^ Convert.ToByte(variable));
                    return;
                case 2:
                    Kernel[OffsetToEnemyAttacksSelected + 17] = (byte)(Kernel[OffsetToEnemyAttacksSelected + 17] ^ Convert.ToByte(variable));
                    return;
                case 3:
                    Kernel[OffsetToEnemyAttacksSelected + 18] = (byte)(Kernel[OffsetToEnemyAttacksSelected + 18] ^ Convert.ToByte(variable));
                    return;
                case 4:
                    Kernel[OffsetToEnemyAttacksSelected + 19] = (byte)(Kernel[OffsetToEnemyAttacksSelected + 19] ^ Convert.ToByte(variable));
                    return;
            }
        }

        #endregion

        #region BLUE MAGIC

        public static void UpdateVariable_BlueMagic(int index, object variable, object sender = null)
        {
            if (!mainForm._loaded || Kernel == null)
                return;
            switch (index)
            {
                case 0:
                    UshortToKernel(Convert.ToUInt16(variable), 4, (byte)Mode.Mode_BlueMagic); //MagicID
                    return;
                case 1:
                    Kernel[OffsetToBlueMagicSelected + 7] = Convert.ToByte(variable); //attack type
                    return;
                case 2:
                    Kernel[OffsetToBlueMagicSelected + 10] ^= Convert.ToByte(variable); //flags
                    return;
                case 3:
                    Kernel[OffsetToBlueMagicSelected + 12] = Convert.ToByte(variable); //element
                    return;
                case 4:
                    Kernel[OffsetToBlueMagicSelected + 13] = Convert.ToByte(variable); //status attack
                    return;
                case 5:
                    Kernel[OffsetToBlueMagicSelected + 14] = Convert.ToByte(variable); //crit bonus
                    return;
                case 6:
                    //name
                    return;
                case 7:
                    //description
                    return;

                default:
                    return;
            }
        }
        public static void UpdateVariable_BlueMagicParam(int index, object variable, byte arg0 = 127)
        {
            if (!mainForm._loaded || Kernel == null)
                return;
            switch (index)
            {
                case 0:
                    BlueMagicParamStatusUpdator(arg0, variable); //status all CL
                    return;
                case 1:
                    Kernel[OffsetToBlueMagicParamSelected + 6] = Convert.ToByte(variable); //attack power CL1
                    return;
                case 2:
                    Kernel[OffsetToBlueMagicParamSelected + 7] = Convert.ToByte(variable); //attack param CL1
                    return;
                case 3:
                    Kernel[OffsetToBlueMagicParamSelected + 14] = Convert.ToByte(variable); //attack power CL2
                    return;
                case 4:
                    Kernel[OffsetToBlueMagicParamSelected + 15] = Convert.ToByte(variable); //attack param CL2
                    return;
                case 5:
                    Kernel[OffsetToBlueMagicParamSelected + 22] = Convert.ToByte(variable); //attack power CL3
                    return;
                case 6:
                    Kernel[OffsetToBlueMagicParamSelected + 23] = Convert.ToByte(variable); //attack param CL3
                    return;
                case 7:
                    Kernel[OffsetToBlueMagicParamSelected + 30] = Convert.ToByte(variable); //attack power CL4
                    return;
                case 8:
                    Kernel[OffsetToBlueMagicParamSelected + 31] = Convert.ToByte(variable); //attack param CL4
                    return;

                default:
                    return;
            }
        }
        private static void BlueMagicParamStatusUpdator(byte StatusByteIndex, object variable)
        {
            switch (StatusByteIndex)
            {   //5 cases per CL
                case 0:
                    Kernel[OffsetToBlueMagicParamSelected + 0] = (byte)(Kernel[OffsetToBlueMagicParamSelected + 0] ^ Convert.ToByte(variable));
                    return;
                case 1:
                    Kernel[OffsetToBlueMagicParamSelected + 1] = (byte)(Kernel[OffsetToBlueMagicParamSelected + 1] ^ Convert.ToByte(variable));
                    return;
                case 2:
                    Kernel[OffsetToBlueMagicParamSelected + 2] = (byte)(Kernel[OffsetToBlueMagicParamSelected + 2] ^ Convert.ToByte(variable));
                    return;
                case 3:
                    Kernel[OffsetToBlueMagicParamSelected + 3] = (byte)(Kernel[OffsetToBlueMagicParamSelected + 3] ^ Convert.ToByte(variable));
                    return;
                case 4:
                    Kernel[OffsetToBlueMagicParamSelected + 4] = (byte)(Kernel[OffsetToBlueMagicParamSelected + 4] ^ Convert.ToByte(variable));
                    return;
                case 5:
                    Kernel[OffsetToBlueMagicParamSelected + 8] = (byte)(Kernel[OffsetToBlueMagicParamSelected + 8] ^ Convert.ToByte(variable));
                    return;
                case 6:
                    Kernel[OffsetToBlueMagicParamSelected + 9] = (byte)(Kernel[OffsetToBlueMagicParamSelected + 9] ^ Convert.ToByte(variable));
                    return;
                case 7:
                    Kernel[OffsetToBlueMagicParamSelected + 10] = (byte)(Kernel[OffsetToBlueMagicParamSelected + 10] ^ Convert.ToByte(variable));
                    return;
                case 8:
                    Kernel[OffsetToBlueMagicParamSelected + 11] = (byte)(Kernel[OffsetToBlueMagicParamSelected + 11] ^ Convert.ToByte(variable));
                    return;
                case 9:
                    Kernel[OffsetToBlueMagicParamSelected + 12] = (byte)(Kernel[OffsetToBlueMagicParamSelected + 12] ^ Convert.ToByte(variable));
                    return;
                case 10:
                    Kernel[OffsetToBlueMagicParamSelected + 16] = (byte)(Kernel[OffsetToBlueMagicParamSelected + 16] ^ Convert.ToByte(variable));
                    return;
                case 11:
                    Kernel[OffsetToBlueMagicParamSelected + 17] = (byte)(Kernel[OffsetToBlueMagicParamSelected + 17] ^ Convert.ToByte(variable));
                    return;
                case 12:
                    Kernel[OffsetToBlueMagicParamSelected + 18] = (byte)(Kernel[OffsetToBlueMagicParamSelected + 18] ^ Convert.ToByte(variable));
                    return;
                case 13:
                    Kernel[OffsetToBlueMagicParamSelected + 19] = (byte)(Kernel[OffsetToBlueMagicParamSelected + 19] ^ Convert.ToByte(variable));
                    return;
                case 14:
                    Kernel[OffsetToBlueMagicParamSelected + 20] = (byte)(Kernel[OffsetToBlueMagicParamSelected + 20] ^ Convert.ToByte(variable));
                    return;
                case 15:
                    Kernel[OffsetToBlueMagicParamSelected + 24] = (byte)(Kernel[OffsetToBlueMagicParamSelected + 24] ^ Convert.ToByte(variable));
                    return;
                case 16:
                    Kernel[OffsetToBlueMagicParamSelected + 25] = (byte)(Kernel[OffsetToBlueMagicParamSelected + 25] ^ Convert.ToByte(variable));
                    return;
                case 17:
                    Kernel[OffsetToBlueMagicParamSelected + 26] = (byte)(Kernel[OffsetToBlueMagicParamSelected + 26] ^ Convert.ToByte(variable));
                    return;
                case 18:
                    Kernel[OffsetToBlueMagicParamSelected + 27] = (byte)(Kernel[OffsetToBlueMagicParamSelected + 27] ^ Convert.ToByte(variable));
                    return;
                case 19:
                    Kernel[OffsetToBlueMagicParamSelected + 28] = (byte)(Kernel[OffsetToBlueMagicParamSelected + 28] ^ Convert.ToByte(variable));
                    return;
            }
        }

        #endregion

        #region STAT PERCENTAGE ABILITIES

        public static void UpdateVariable_StatPercentageAbilities(int index, object variable, object sender = null)
        {
            if (!mainForm._loaded || Kernel == null)
                return;
            switch (index)
            {
                case 0:
                    Kernel[OffsetToStatPercentageAbilitiesSelected + 4] = Convert.ToByte(variable); //AP
                    return;
                case 1:
                    Kernel[OffsetToStatPercentageAbilitiesSelected + 5] = Convert.ToByte(variable); //stat to increase
                    return;
                case 2:
                    Kernel[OffsetToStatPercentageAbilitiesSelected + 6] = Convert.ToByte(variable); //increasement value
                    return;
                case 3:
                    //Name
                    return;
                case 4:
                    //Description
                    return;

                default:
                    return;
            }
        }

        #endregion

        #region RENZOKUKEN FINISHERS

        public static void UpdateVariable_RenzoFin(int index, object variable, byte arg0 = 127, object sender = null)
        {
            if (!mainForm._loaded || Kernel == null)
                return;
            switch (index)
            {
                case 0:
                    UshortToKernel(Convert.ToUInt16(variable), 4, (byte)Mode.Mode_RenzoFin); //MagicID
                    return;
                case 1:
                    Kernel[OffsetToRenzoFinSelected + 6] = Convert.ToByte(variable); //Attack type
                    return;
                case 2:
                    Kernel[OffsetToRenzoFinSelected + 8] = Convert.ToByte(variable); //Attack power
                    return;
                case 3:
                    Kernel[OffsetToRenzoFinSelected + 10] = (byte)(Kernel[OffsetToRenzoFinSelected + 10] ^ Convert.ToByte(variable)); //target
                    return;
                case 4:
                    Kernel[OffsetToRenzoFinSelected + 11] = (byte)(Kernel[OffsetToRenzoFinSelected + 11] ^ Convert.ToByte(variable)); //attack flags
                    return;
                case 5:
                    Kernel[OffsetToRenzoFinSelected + 12] = Convert.ToByte(variable); //Hit Count
                    return;
                case 6:
                    Kernel[OffsetToRenzoFinSelected + 13] = Convert.ToByte(variable); //Element
                    return;
                case 7:
                    Kernel[OffsetToRenzoFinSelected + 14] = Convert.ToByte(variable); //Element %
                    return;
                case 8:
                    Kernel[OffsetToRenzoFinSelected + 15] = Convert.ToByte(variable); //Attack Status
                    return;
                case 9:
                    RenzoFinStatusUpdator(arg0, variable); //Status
                    return;
                case 10:
                    //name
                    return;
                case 11:
                    //description
                    return;

                default:
                    return;
            }
        }
        private static void RenzoFinStatusUpdator(byte StatusByteIndex, object variable)
        {
            switch (StatusByteIndex)
            {
                case 0:
                    Kernel[OffsetToRenzoFinSelected + 18] = (byte)(Kernel[OffsetToRenzoFinSelected + 18] ^ Convert.ToByte(variable));
                    return;
                case 1:
                    Kernel[OffsetToRenzoFinSelected + 20] = (byte)(Kernel[OffsetToRenzoFinSelected + 20] ^ Convert.ToByte(variable));
                    return;
                case 2:
                    Kernel[OffsetToRenzoFinSelected + 21] = (byte)(Kernel[OffsetToRenzoFinSelected + 21] ^ Convert.ToByte(variable));
                    return;
                case 3:
                    Kernel[OffsetToRenzoFinSelected + 22] = (byte)(Kernel[OffsetToRenzoFinSelected + 22] ^ Convert.ToByte(variable));
                    return;
                case 4:
                    Kernel[OffsetToRenzoFinSelected + 23] = (byte)(Kernel[OffsetToRenzoFinSelected + 23] ^ Convert.ToByte(variable));
                    return;
            }
        }

        #endregion

        #region TEMP CHARACTERS LIMIT BREAKS

        public static void UpdateVariable_TempCharLB(int index, object variable, byte arg0 = 127, object sender = null)
        {
            if (!mainForm._loaded || Kernel == null)
                return;
            switch (index)
            {
                case 0:
                    UshortToKernel(Convert.ToUInt16(variable), 4, (byte)Mode.Mode_TempCharLB); //MagicID
                    return;
                case 1:
                    Kernel[OffsetToTempCharLBSelected + 6] = Convert.ToByte(variable); //Attack type
                    return;
                case 2:
                    Kernel[OffsetToTempCharLBSelected + 7] = Convert.ToByte(variable); //Attack power
                    return;
                case 3:
                    Kernel[OffsetToTempCharLBSelected + 10] = (byte)(Kernel[OffsetToTempCharLBSelected + 10] ^ Convert.ToByte(variable)); //default target
                    return;
                case 4:
                    Kernel[OffsetToTempCharLBSelected + 11] = (byte)(Kernel[OffsetToTempCharLBSelected + 11] ^ Convert.ToByte(variable)); //attack flags
                    return;
                case 5:
                    Kernel[OffsetToTempCharLBSelected + 12] = Convert.ToByte(variable); //Hit Count
                    return;
                case 6:
                    Kernel[OffsetToTempCharLBSelected + 13] = Convert.ToByte(variable); //Element
                    return;
                case 7:
                    Kernel[OffsetToTempCharLBSelected + 14] = Convert.ToByte(variable); //Element %
                    return;
                case 8:
                    Kernel[OffsetToTempCharLBSelected + 15] = Convert.ToByte(variable); //Status Attack
                    return;
                case 9:
                    TempCharLBStatusUpdator(arg0, variable); //Status
                    return;
                case 10:
                    //Name
                    return;
                case 11:
                    //Description
                    return;

                default:
                    return;
            }
        }
        private static void TempCharLBStatusUpdator(byte StatusByteIndex, object variable)
        {
            switch (StatusByteIndex)
            {
                case 0:
                    Kernel[OffsetToTempCharLBSelected + 16] = (byte)(Kernel[OffsetToTempCharLBSelected + 16] ^ Convert.ToByte(variable));
                    return;
                case 1:
                    Kernel[OffsetToTempCharLBSelected + 20] = (byte)(Kernel[OffsetToTempCharLBSelected + 20] ^ Convert.ToByte(variable));
                    return;
                case 2:
                    Kernel[OffsetToTempCharLBSelected + 21] = (byte)(Kernel[OffsetToTempCharLBSelected + 21] ^ Convert.ToByte(variable));
                    return;
                case 3:
                    Kernel[OffsetToTempCharLBSelected + 22] = (byte)(Kernel[OffsetToTempCharLBSelected + 22] ^ Convert.ToByte(variable));
                    return;
                case 4:
                    Kernel[OffsetToTempCharLBSelected + 23] = (byte)(Kernel[OffsetToTempCharLBSelected + 23] ^ Convert.ToByte(variable));
                    return;
            }
        }

        #endregion

        #region SHOT

        public static void UpdateVariable_Shot(int index, object variable, byte arg0 = 127, object sender = null)
        {
            if (!mainForm._loaded || Kernel == null)
                return;
            switch (index)
            {
                case 0:
                    UshortToKernel(Convert.ToUInt16(variable), 4, (byte)Mode.Mode_Shot); //MagicID
                    return;
                case 1:
                    Kernel[OffsetToShotSelected + 6] = Convert.ToByte(variable); //Attack type
                    return;
                case 2:
                    Kernel[OffsetToShotSelected + 7] = Convert.ToByte(variable); //Attack power
                    return;
                case 3:
                    Kernel[OffsetToShotSelected + 10] = (byte)(Kernel[OffsetToShotSelected + 10] ^ Convert.ToByte(variable)); //default target
                    return;
                case 4:
                    Kernel[OffsetToShotSelected + 11] = (byte)(Kernel[OffsetToShotSelected + 11] ^ Convert.ToByte(variable)); //attack flags
                    return;
                case 5:
                    Kernel[OffsetToShotSelected + 12] = Convert.ToByte(variable); //Hit Count
                    return;
                case 6:
                    Kernel[OffsetToShotSelected + 13] = Convert.ToByte(variable); //Element
                    return;
                case 7:
                    Kernel[OffsetToShotSelected + 14] = Convert.ToByte(variable); //Element %
                    return;
                case 8:
                    Kernel[OffsetToShotSelected + 15] = Convert.ToByte(variable); //Status Attack
                    return;
                case 9:
                    Kernel[OffsetToShotSelected + 18] = Convert.ToByte(variable); //Used Item
                    return;
                case 10:
                    Kernel[OffsetToShotSelected + 19] = Convert.ToByte(variable); //Crit Bonus
                    return;
                case 11:
                    ShotStatusUpdator(arg0, variable); //Status
                    return;
                case 12:
                    //Name
                    return;
                case 13:
                    //Description
                    return;

                default:
                    return;
            }
        }
        private static void ShotStatusUpdator(byte StatusByteIndex, object variable)
        {
            switch (StatusByteIndex)
            {
                case 0:
                    Kernel[OffsetToShotSelected + 16] = (byte)(Kernel[OffsetToShotSelected + 16] ^ Convert.ToByte(variable));
                    return;
                case 1:
                    Kernel[OffsetToShotSelected + 20] = (byte)(Kernel[OffsetToShotSelected + 20] ^ Convert.ToByte(variable));
                    return;
                case 2:
                    Kernel[OffsetToShotSelected + 21] = (byte)(Kernel[OffsetToShotSelected + 21] ^ Convert.ToByte(variable));
                    return;
                case 3:
                    Kernel[OffsetToShotSelected + 22] = (byte)(Kernel[OffsetToShotSelected + 22] ^ Convert.ToByte(variable));
                    return;
                case 4:
                    Kernel[OffsetToShotSelected + 23] = (byte)(Kernel[OffsetToShotSelected + 23] ^ Convert.ToByte(variable));
                    return;
            }
        }

        #endregion

        #region DUEL

        public static void UpdateVariable_Duel(int index, object variable, byte arg0 = 127, object sender = null)
        {
            if (!mainForm._loaded || Kernel == null)
                return;
            switch (index)
            {
                case 0:
                    UshortToKernel(Convert.ToUInt16(variable), 4, (byte)Mode.Mode_Duel); //MagicID
                    return;
                case 1:
                    Kernel[OffsetToDuelSelected + 6] = Convert.ToByte(variable); //Attack type
                    return;
                case 2:
                    Kernel[OffsetToDuelSelected + 7] = Convert.ToByte(variable); //Attack power
                    return;
                case 3:
                    Kernel[OffsetToDuelSelected + 8] = (byte)(Kernel[OffsetToDuelSelected + 8] ^ Convert.ToByte(variable)); //attack flags
                    return;
                case 4:
                    Kernel[OffsetToDuelSelected + 10] = (byte)(Kernel[OffsetToDuelSelected + 10] ^ Convert.ToByte(variable)); //default target
                    return;
                case 5:
                    Kernel[OffsetToDuelSelected + 12] = Convert.ToByte(variable); //Hit Count
                    return;
                case 6:
                    Kernel[OffsetToDuelSelected + 13] = Convert.ToByte(variable); //Element
                    return;
                case 7:
                    Kernel[OffsetToDuelSelected + 14] = Convert.ToByte(variable); //Element %
                    return;
                case 8:
                    Kernel[OffsetToDuelSelected + 15] = Convert.ToByte(variable); //Status Attack
                    return;
                case 9:
                    ushort newValue = (ushort)((BitConverter.ToUInt16(Kernel, OffsetToDuelSelected + 16) & 0x0100) | Convert.ToUInt16(variable));
                    ButtonsToKernel(newValue, 16); //button 1 combobox   
                    return;
                case 10:
                    ushort newValue2 = (ushort)((BitConverter.ToUInt16(Kernel, OffsetToDuelSelected + 16)) ^ 0x0100);
                    ButtonsToKernel(newValue2, 16); //button 1 isfinisher
                    return;
                case 11:
                    ButtonsToKernel(Convert.ToUInt16(variable), 18); //button 2
                    return;
                case 12:
                    ButtonsToKernel(Convert.ToUInt16(variable), 20); //button 3
                    return;
                case 13:
                    ButtonsToKernel(Convert.ToUInt16(variable), 22); //button 4
                    return;
                case 14:
                    ButtonsToKernel(Convert.ToUInt16(variable), 24); //button 5
                    return;
                case 15:
                    DuelStatusUpdator(arg0, variable); //Status
                    return;
                case 16:
                    //Name
                    return;
                case 17:
                    //Description
                    return;

                default:
                    return;
            }
        }
        private static void DuelStatusUpdator(byte StatusByteIndex, object variable)
        {
            switch (StatusByteIndex)
            {
                case 0:
                    Kernel[OffsetToDuelSelected + 26] = (byte)(Kernel[OffsetToDuelSelected + 26] ^ Convert.ToByte(variable));
                    return;
                case 1:
                    Kernel[OffsetToDuelSelected + 28] = (byte)(Kernel[OffsetToDuelSelected + 28] ^ Convert.ToByte(variable));
                    return;
                case 2:
                    Kernel[OffsetToDuelSelected + 29] = (byte)(Kernel[OffsetToDuelSelected + 29] ^ Convert.ToByte(variable));
                    return;
                case 3:
                    Kernel[OffsetToDuelSelected + 30] = (byte)(Kernel[OffsetToDuelSelected + 30] ^ Convert.ToByte(variable));
                    return;
                case 4:
                    Kernel[OffsetToDuelSelected + 31] = (byte)(Kernel[OffsetToDuelSelected + 31] ^ Convert.ToByte(variable));
                    return;
            }
        }
        public static void UpdateVariable_DuelParams(int index, object variable)
        {
            if (!mainForm._loaded || Kernel == null)
                return;
            switch (index)
            {
                case 0:
                    Kernel[OffsetToDuelParamsSelected] = Convert.ToByte(variable); //Start Move Seq 0
                    return;
                case 1:
                    Kernel[OffsetToDuelParamsSelected + 4] = Convert.ToByte(variable); //Start Move Seq 1
                    return;
                case 2:
                    Kernel[OffsetToDuelParamsSelected + 8] = Convert.ToByte(variable); //Start Move Seq 2
                    return;
                case 3:
                    Kernel[OffsetToDuelParamsSelected + 12] = Convert.ToByte(variable); //Start Move Seq 3
                    return;
                case 4:
                    Kernel[OffsetToDuelParamsSelected + 16] = Convert.ToByte(variable); //Start Move Seq 4
                    return;
                case 5:
                    Kernel[OffsetToDuelParamsSelected + 20] = Convert.ToByte(variable); //Start Move Seq 5
                    return;
                case 6:
                    Kernel[OffsetToDuelParamsSelected + 24] = Convert.ToByte(variable); //Start Move Seq 6
                    return;
                case 7:
                    Kernel[OffsetToDuelParamsSelected + 28] = Convert.ToByte(variable); //Start Move Seq 7
                    return;
                case 8:
                    Kernel[OffsetToDuelParamsSelected + 32] = Convert.ToByte(variable); //Start Move Seq 8
                    return;
                case 9:
                    Kernel[OffsetToDuelParamsSelected + 36] = Convert.ToByte(variable); //Start Move Seq 9
                    return;
                case 10:
                    Kernel[OffsetToDuelParamsSelected + 40] = Convert.ToByte(variable); //Start Move Seq 10
                    return;
                case 11:
                    Kernel[OffsetToDuelParamsSelected + 44] = Convert.ToByte(variable); //Start Move Seq 11
                    return;
                case 12:
                    Kernel[OffsetToDuelParamsSelected + 48] = Convert.ToByte(variable); //Start Move Seq 12
                    return;
                case 13:
                    Kernel[OffsetToDuelParamsSelected + 52] = Convert.ToByte(variable); //Start Move Seq 13
                    return;
                case 14:
                    Kernel[OffsetToDuelParamsSelected + 56] = Convert.ToByte(variable); //Start Move Seq 14
                    return;
                case 15:
                    Kernel[OffsetToDuelParamsSelected + 60] = Convert.ToByte(variable); //Start Move Seq 15
                    return;
                case 16:
                    Kernel[OffsetToDuelParamsSelected + 64] = Convert.ToByte(variable); //Start Move Seq 16
                    return;
                case 17:
                    Kernel[OffsetToDuelParamsSelected + 68] = Convert.ToByte(variable); //Start Move Seq 17
                    return;
                case 18:
                    Kernel[OffsetToDuelParamsSelected + 72] = Convert.ToByte(variable); //Start Move Seq 18
                    return;
                case 19:
                    Kernel[OffsetToDuelParamsSelected + 76] = Convert.ToByte(variable); //Start Move Seq 19
                    return;
                case 20:
                    Kernel[OffsetToDuelParamsSelected + 80] = Convert.ToByte(variable); //Start Move Seq 20
                    return;
                case 21:
                    Kernel[OffsetToDuelParamsSelected + 84] = Convert.ToByte(variable); //Start Move Seq 21
                    return;
                case 22:
                    Kernel[OffsetToDuelParamsSelected + 88] = Convert.ToByte(variable); //Start Move Seq 22
                    return;
                case 23:
                    Kernel[OffsetToDuelParamsSelected + 92] = Convert.ToByte(variable); //Start Move Seq 23
                    return;
                case 24:
                    Kernel[OffsetToDuelParamsSelected + 96] = Convert.ToByte(variable); //Start Move Seq 24
                    return;
                case 25:
                    Kernel[OffsetToDuelParamsSelected + 1] = Convert.ToByte(variable); //Next Seq 0_1
                    return;
                case 26:
                    Kernel[OffsetToDuelParamsSelected + 2] = Convert.ToByte(variable); //Next Seq 0_2
                    return;
                case 27:
                    Kernel[OffsetToDuelParamsSelected + 3] = Convert.ToByte(variable); //Next Seq 0_3
                    return;
                case 28:
                    Kernel[OffsetToDuelParamsSelected + 5] = Convert.ToByte(variable); //Next Seq 1_1
                    return;
                case 29:
                    Kernel[OffsetToDuelParamsSelected + 6] = Convert.ToByte(variable); //Next Seq 1_2
                    return;
                case 30:
                    Kernel[OffsetToDuelParamsSelected + 7] = Convert.ToByte(variable); //Next Seq 1_3
                    return;
                case 31:
                    Kernel[OffsetToDuelParamsSelected + 9] = Convert.ToByte(variable); //Next Seq 2_1
                    return;
                case 32:
                    Kernel[OffsetToDuelParamsSelected + 10] = Convert.ToByte(variable); //Next Seq 2_2
                    return;
                case 33:
                    Kernel[OffsetToDuelParamsSelected + 11] = Convert.ToByte(variable); //Next Seq 2_3
                    return;
                case 34:
                    Kernel[OffsetToDuelParamsSelected + 13] = Convert.ToByte(variable); //Next Seq 3_1
                    return;
                case 35:
                    Kernel[OffsetToDuelParamsSelected + 14] = Convert.ToByte(variable); //Next Seq 3_2
                    return;
                case 36:
                    Kernel[OffsetToDuelParamsSelected + 15] = Convert.ToByte(variable); //Next Seq 3_3
                    return;
                case 37:
                    Kernel[OffsetToDuelParamsSelected + 17] = Convert.ToByte(variable); //Next Seq 4_1
                    return;
                case 38:
                    Kernel[OffsetToDuelParamsSelected + 18] = Convert.ToByte(variable); //Next Seq 4_2
                    return;
                case 39:
                    Kernel[OffsetToDuelParamsSelected + 19] = Convert.ToByte(variable); //Next Seq 4_3
                    return;
                case 40:
                    Kernel[OffsetToDuelParamsSelected + 21] = Convert.ToByte(variable); //Next Seq 5_1
                    return;
                case 41:
                    Kernel[OffsetToDuelParamsSelected + 22] = Convert.ToByte(variable); //Next Seq 5_2
                    return;
                case 42:
                    Kernel[OffsetToDuelParamsSelected + 23] = Convert.ToByte(variable); //Next Seq 5_3
                    return;
                case 43:
                    Kernel[OffsetToDuelParamsSelected + 25] = Convert.ToByte(variable); //Next Seq 6_1
                    return;
                case 44:
                    Kernel[OffsetToDuelParamsSelected + 26] = Convert.ToByte(variable); //Next Seq 6_2
                    return;
                case 45:
                    Kernel[OffsetToDuelParamsSelected + 27] = Convert.ToByte(variable); //Next Seq 6_3
                    return;
                case 46:
                    Kernel[OffsetToDuelParamsSelected + 29] = Convert.ToByte(variable); //Next Seq 7_1
                    return;
                case 47:
                    Kernel[OffsetToDuelParamsSelected + 30] = Convert.ToByte(variable); //Next Seq 7_2
                    return;
                case 48:
                    Kernel[OffsetToDuelParamsSelected + 31] = Convert.ToByte(variable); //Next Seq 7_3
                    return;
                case 49:
                    Kernel[OffsetToDuelParamsSelected + 33] = Convert.ToByte(variable); //Next Seq 8_1
                    return;
                case 50:
                    Kernel[OffsetToDuelParamsSelected + 34] = Convert.ToByte(variable); //Next Seq 8_2
                    return;
                case 51:
                    Kernel[OffsetToDuelParamsSelected + 35] = Convert.ToByte(variable); //Next Seq 8_3
                    return;
                case 52:
                    Kernel[OffsetToDuelParamsSelected + 37] = Convert.ToByte(variable); //Next Seq 9_1
                    return;
                case 53:
                    Kernel[OffsetToDuelParamsSelected + 38] = Convert.ToByte(variable); //Next Seq 9_2
                    return;
                case 54:
                    Kernel[OffsetToDuelParamsSelected + 39] = Convert.ToByte(variable); //Next Seq 9_3
                    return;
                case 55:
                    Kernel[OffsetToDuelParamsSelected + 41] = Convert.ToByte(variable); //Next Seq 10_1
                    return;
                case 56:
                    Kernel[OffsetToDuelParamsSelected + 42] = Convert.ToByte(variable); //Next Seq 10_2
                    return;
                case 57:
                    Kernel[OffsetToDuelParamsSelected + 43] = Convert.ToByte(variable); //Next Seq 10_3
                    return;
                case 58:
                    Kernel[OffsetToDuelParamsSelected + 45] = Convert.ToByte(variable); //Next Seq 11_1
                    return;
                case 59:
                    Kernel[OffsetToDuelParamsSelected + 46] = Convert.ToByte(variable); //Next Seq 11_2
                    return;
                case 60:
                    Kernel[OffsetToDuelParamsSelected + 47] = Convert.ToByte(variable); //Next Seq 11_3
                    return;
                case 61:
                    Kernel[OffsetToDuelParamsSelected + 49] = Convert.ToByte(variable); //Next Seq 12_1
                    return;
                case 62:
                    Kernel[OffsetToDuelParamsSelected + 50] = Convert.ToByte(variable); //Next Seq 12_2
                    return;
                case 63:
                    Kernel[OffsetToDuelParamsSelected + 51] = Convert.ToByte(variable); //Next Seq 12_3
                    return;
                case 64:
                    Kernel[OffsetToDuelParamsSelected + 53] = Convert.ToByte(variable); //Next Seq 13_1
                    return;
                case 65:
                    Kernel[OffsetToDuelParamsSelected + 54] = Convert.ToByte(variable); //Next Seq 13_2
                    return;
                case 66:
                    Kernel[OffsetToDuelParamsSelected + 55] = Convert.ToByte(variable); //Next Seq 13_3
                    return;
                case 67:
                    Kernel[OffsetToDuelParamsSelected + 57] = Convert.ToByte(variable); //Next Seq 14_1
                    return;
                case 68:
                    Kernel[OffsetToDuelParamsSelected + 58] = Convert.ToByte(variable); //Next Seq 14_2
                    return;
                case 69:
                    Kernel[OffsetToDuelParamsSelected + 59] = Convert.ToByte(variable); //Next Seq 14_3
                    return;
                case 70:
                    Kernel[OffsetToDuelParamsSelected + 61] = Convert.ToByte(variable); //Next Seq 15_1
                    return;
                case 71:
                    Kernel[OffsetToDuelParamsSelected + 62] = Convert.ToByte(variable); //Next Seq 15_2
                    return;
                case 72:
                    Kernel[OffsetToDuelParamsSelected + 63] = Convert.ToByte(variable); //Next Seq 15_3
                    return;
                case 73:
                    Kernel[OffsetToDuelParamsSelected + 65] = Convert.ToByte(variable); //Next Seq 16_1
                    return;
                case 74:
                    Kernel[OffsetToDuelParamsSelected + 66] = Convert.ToByte(variable); //Next Seq 16_2
                    return;
                case 75:
                    Kernel[OffsetToDuelParamsSelected + 67] = Convert.ToByte(variable); //Next Seq 16_3
                    return;
                case 76:
                    Kernel[OffsetToDuelParamsSelected + 69] = Convert.ToByte(variable); //Next Seq 17_1
                    return;
                case 77:
                    Kernel[OffsetToDuelParamsSelected + 70] = Convert.ToByte(variable); //Next Seq 17_2
                    return;
                case 78:
                    Kernel[OffsetToDuelParamsSelected + 71] = Convert.ToByte(variable); //Next Seq 17_3
                    return;
                case 79:
                    Kernel[OffsetToDuelParamsSelected + 73] = Convert.ToByte(variable); //Next Seq 18_1
                    return;
                case 80:
                    Kernel[OffsetToDuelParamsSelected + 74] = Convert.ToByte(variable); //Next Seq 18_2
                    return;
                case 81:
                    Kernel[OffsetToDuelParamsSelected + 75] = Convert.ToByte(variable); //Next Seq 18_3
                    return;
                case 82:
                    Kernel[OffsetToDuelParamsSelected + 77] = Convert.ToByte(variable); //Next Seq 19_1
                    return;
                case 83:
                    Kernel[OffsetToDuelParamsSelected + 78] = Convert.ToByte(variable); //Next Seq 19_2
                    return;
                case 84:
                    Kernel[OffsetToDuelParamsSelected + 79] = Convert.ToByte(variable); //Next Seq 19_3
                    return;
                case 85:
                    Kernel[OffsetToDuelParamsSelected + 81] = Convert.ToByte(variable); //Next Seq 20_1
                    return;
                case 86:
                    Kernel[OffsetToDuelParamsSelected + 82] = Convert.ToByte(variable); //Next Seq 20_2
                    return;
                case 87:
                    Kernel[OffsetToDuelParamsSelected + 83] = Convert.ToByte(variable); //Next Seq 20_3
                    return;
                case 88:
                    Kernel[OffsetToDuelParamsSelected + 85] = Convert.ToByte(variable); //Next Seq 21_1
                    return;
                case 89:
                    Kernel[OffsetToDuelParamsSelected + 86] = Convert.ToByte(variable); //Next Seq 21_2
                    return;
                case 90:
                    Kernel[OffsetToDuelParamsSelected + 87] = Convert.ToByte(variable); //Next Seq 21_3
                    return;
                case 91:
                    Kernel[OffsetToDuelParamsSelected + 89] = Convert.ToByte(variable); //Next Seq 22_1
                    return;
                case 92:
                    Kernel[OffsetToDuelParamsSelected + 90] = Convert.ToByte(variable); //Next Seq 22_2
                    return;
                case 93:
                    Kernel[OffsetToDuelParamsSelected + 91] = Convert.ToByte(variable); //Next Seq 22_3
                    return;
                case 94:
                    Kernel[OffsetToDuelParamsSelected + 93] = Convert.ToByte(variable); //Next Seq 23_1
                    return;
                case 95:
                    Kernel[OffsetToDuelParamsSelected + 94] = Convert.ToByte(variable); //Next Seq 23_2
                    return;
                case 96:
                    Kernel[OffsetToDuelParamsSelected + 95] = Convert.ToByte(variable); //Next Seq 23_3
                    return;
                case 97:
                    Kernel[OffsetToDuelParamsSelected + 97] = Convert.ToByte(variable); //Next Seq 24_1
                    return;
                case 98:
                    Kernel[OffsetToDuelParamsSelected + 98] = Convert.ToByte(variable); //Next Seq 24_2
                    return;
                case 99:
                    Kernel[OffsetToDuelParamsSelected + 99] = Convert.ToByte(variable); //Next Seq 24_3
                    return;


                default:
                    return;
            }
        }

        #endregion

        #region COMBINE

        public static void UpdateVariable_Combine(int index, object variable, byte arg0 = 127, object sender = null)
        {
            if (!mainForm._loaded || Kernel == null)
                return;
            switch (index)
            {
                case 0:
                    UshortToKernel(Convert.ToUInt16(variable), 2, (byte)Mode.Mode_Combine); //MagicID
                    return;
                case 1:
                    Kernel[OffsetToCombineSelected + 4] = Convert.ToByte(variable); //Attack type
                    return;
                case 2:
                    Kernel[OffsetToCombineSelected + 5] = Convert.ToByte(variable); //Attack power
                    return;
                case 3:
                    Kernel[OffsetToCombineSelected + 6] = (byte)(Kernel[OffsetToCombineSelected + 6] ^ Convert.ToByte(variable)); //attack flags
                    return;
                case 4:
                    Kernel[OffsetToCombineSelected + 8] = (byte)(Kernel[OffsetToCombineSelected + 8] ^ Convert.ToByte(variable)); //default target
                    return;
                case 5:
                    Kernel[OffsetToCombineSelected + 10] = Convert.ToByte(variable); //Hit Count
                    return;
                case 6:
                    Kernel[OffsetToCombineSelected + 11] = Convert.ToByte(variable); //Element
                    return;
                case 7:
                    Kernel[OffsetToCombineSelected + 12] = Convert.ToByte(variable); //Element %
                    return;
                case 8:
                    Kernel[OffsetToCombineSelected + 13] = Convert.ToByte(variable); //Status Attack
                    return;
                case 9:
                    CombineStatusUpdator(arg0, variable); //Status
                    return;
                case 10:
                    //Name
                    return;

                default:
                    return;
            }
        }
        private static void CombineStatusUpdator(byte StatusByteIndex, object variable)
        {
            switch (StatusByteIndex)
            {
                case 0:
                    Kernel[OffsetToCombineSelected + 14] = (byte)(Kernel[OffsetToCombineSelected + 14] ^ Convert.ToByte(variable));
                    return;
                case 1:
                    Kernel[OffsetToCombineSelected + 16] = (byte)(Kernel[OffsetToCombineSelected + 16] ^ Convert.ToByte(variable));
                    return;
                case 2:
                    Kernel[OffsetToCombineSelected + 17] = (byte)(Kernel[OffsetToCombineSelected + 17] ^ Convert.ToByte(variable));
                    return;
                case 3:
                    Kernel[OffsetToCombineSelected + 18] = (byte)(Kernel[OffsetToCombineSelected + 18] ^ Convert.ToByte(variable));
                    return;
                case 4:
                    Kernel[OffsetToCombineSelected + 19] = (byte)(Kernel[OffsetToCombineSelected + 19] ^ Convert.ToByte(variable));
                    return;
            }
        }

        #endregion

        #region BATTLE ITEMS

        public static void UpdateVariable_BattleItems(int index, object variable, byte arg0 = 127, object sender = null)
        {
            if (!mainForm._loaded || Kernel == null)
                return;
            switch (index)
            {
                case 0:
                    UshortToKernel(Convert.ToUInt16(variable), 4, (byte)Mode.Mode_BattleItems); //MagicID
                    return;
                case 1:
                    Kernel[OffsetToBattleItemsSelected + 6] = Convert.ToByte(variable); //Attack type
                    return;
                case 2:
                    Kernel[OffsetToBattleItemsSelected + 7] = Convert.ToByte(variable); //Attack power
                    return;
                case 3:
                    Kernel[OffsetToBattleItemsSelected + 9] = (byte)(Kernel[OffsetToBattleItemsSelected + 9] ^ Convert.ToByte(variable)); //target
                    return;
                case 4:
                    Kernel[OffsetToBattleItemsSelected + 11] = (byte)(Kernel[OffsetToBattleItemsSelected + 11] ^ Convert.ToByte(variable)); //attack flags
                    return;
                case 5:
                    Kernel[OffsetToBattleItemsSelected + 13] = Convert.ToByte(variable); //Attack Status
                    return;
                case 6:
                    BattleItemsStatusUpdator(arg0, variable); //Status
                    return;
                case 7:
                    Kernel[OffsetToBattleItemsSelected + 20] = Convert.ToByte(variable); //Attack Param
                    return;
                case 8:
                    Kernel[OffsetToBattleItemsSelected + 22] = Convert.ToByte(variable); //Hit Count
                    return;
                case 9:
                    Kernel[OffsetToBattleItemsSelected + 23] = Convert.ToByte(variable); //Element
                    return;
                case 10:
                    //Name
                    return;
                case 11:
                    //Description
                    return;

                default:
                    return;
            }
        }
        private static void BattleItemsStatusUpdator(byte StatusByteIndex, object variable)
        {
            switch (StatusByteIndex)
            {
                case 0:
                    Kernel[OffsetToBattleItemsSelected + 14] = (byte)(Kernel[OffsetToBattleItemsSelected + 14] ^ Convert.ToByte(variable));
                    return;
                case 1:
                    Kernel[OffsetToBattleItemsSelected + 16] = (byte)(Kernel[OffsetToBattleItemsSelected + 16] ^ Convert.ToByte(variable));
                    return;
                case 2:
                    Kernel[OffsetToBattleItemsSelected + 17] = (byte)(Kernel[OffsetToBattleItemsSelected + 17] ^ Convert.ToByte(variable));
                    return;
                case 3:
                    Kernel[OffsetToBattleItemsSelected + 18] = (byte)(Kernel[OffsetToBattleItemsSelected + 18] ^ Convert.ToByte(variable));
                    return;
                case 4:
                    Kernel[OffsetToBattleItemsSelected + 19] = (byte)(Kernel[OffsetToBattleItemsSelected + 19] ^ Convert.ToByte(variable));
                    return;
            }
        }

        #endregion

        #region ITEMS

        public static void UpdateVariable_Items(int index, object variable, object sender = null)
        {
            if (!mainForm._loaded || Kernel == null)
                return;
            switch (index)
            {
                case 0:
                    //Name
                    return;
                case 1:
                    //Description
                    return;

                default:
                    return;
            }
        }

        #endregion

        #region SLOT

        public static void UpdateVariable_SlotArray(int index, object variable, object sender = null)
        {
            if (!mainForm._loaded || Kernel == null)
                return;
            switch (index)
            {
                case 0:
                    Kernel[OffsetToSlotArraySelected] = Convert.ToByte(variable); //Magic 1
                    return;
                case 1:
                    Kernel[OffsetToSlotArraySelected + 1] = Convert.ToByte(variable); //Count 1
                    return;
                case 2:
                    Kernel[OffsetToSlotArraySelected + 2] = Convert.ToByte(variable); //Magic 2
                    return;
                case 3:
                    Kernel[OffsetToSlotArraySelected + 3] = Convert.ToByte(variable); //Count 2
                    return;
                case 4:
                    Kernel[OffsetToSlotArraySelected + 4] = Convert.ToByte(variable); //Magic 3
                    return;
                case 5:
                    Kernel[OffsetToSlotArraySelected + 5] = Convert.ToByte(variable); //Count 3
                    return;
                case 6:
                    Kernel[OffsetToSlotArraySelected + 6] = Convert.ToByte(variable); //Magic 4
                    return;
                case 7:
                    Kernel[OffsetToSlotArraySelected + 7] = Convert.ToByte(variable); //Count 4
                    return;
                case 8:
                    Kernel[OffsetToSlotArraySelected + 8] = Convert.ToByte(variable); //Magic 5
                    return;
                case 9:
                    Kernel[OffsetToSlotArraySelected + 9] = Convert.ToByte(variable); //Count 5
                    return;
                case 10:
                    Kernel[OffsetToSlotArraySelected + 10] = Convert.ToByte(variable); //Magic 6
                    return;
                case 11:
                    Kernel[OffsetToSlotArraySelected + 11] = Convert.ToByte(variable); //Count 6
                    return;
                case 12:
                    Kernel[OffsetToSlotArraySelected + 12] = Convert.ToByte(variable); //Magic 7
                    return;
                case 13:
                    Kernel[OffsetToSlotArraySelected + 13] = Convert.ToByte(variable); //Count 7
                    return;
                case 14:
                    Kernel[OffsetToSlotArraySelected + 14] = Convert.ToByte(variable); //Magic 8
                    return;
                case 15:
                    Kernel[OffsetToSlotArraySelected + 15] = Convert.ToByte(variable); //Count 8
                    return;
                case 16:
                    Kernel[OffsetToSlotArraySelected + 16] = Convert.ToByte(variable); //Count 8
                    return;
                case 17:
                    Kernel[OffsetToSlotArraySelected + 17] = Convert.ToByte(variable); //Count 8
                    return;
                case 18:
                    Kernel[OffsetToSlotArraySelected + 18] = Convert.ToByte(variable); //Count 8
                    return;
                case 19:
                    Kernel[OffsetToSlotArraySelected + 19] = Convert.ToByte(variable); //Count 8
                    return;
                case 20:
                    Kernel[OffsetToSlotArraySelected + 20] = Convert.ToByte(variable); //Count 8
                    return;
                case 21:
                    Kernel[OffsetToSlotArraySelected + 21] = Convert.ToByte(variable); //Count 8
                    return;
                case 22:
                    Kernel[OffsetToSlotArraySelected + 22] = Convert.ToByte(variable); //Count 8
                    return;
                case 23:
                    Kernel[OffsetToSlotArraySelected + 23] = Convert.ToByte(variable); //Count 8
                    return;
                case 24:
                    Kernel[OffsetToSlotArraySelected + 24] = Convert.ToByte(variable); //Count 8
                    return;
                case 25:
                    Kernel[OffsetToSlotArraySelected + 25] = Convert.ToByte(variable); //Count 8
                    return;
                case 26:
                    Kernel[OffsetToSlotArraySelected + 26] = Convert.ToByte(variable); //Count 8
                    return;
                case 27:
                    Kernel[OffsetToSlotArraySelected + 27] = Convert.ToByte(variable); //Count 8
                    return;
                case 28:
                    Kernel[OffsetToSlotArraySelected + 28] = Convert.ToByte(variable); //Count 8
                    return;
                case 29:
                    Kernel[OffsetToSlotArraySelected + 29] = Convert.ToByte(variable); //Count 8
                    return;
                case 30:
                    Kernel[OffsetToSlotArraySelected + 30] = Convert.ToByte(variable); //Count 8
                    return;
                case 31:
                    Kernel[OffsetToSlotArraySelected + 31] = Convert.ToByte(variable); //Count 8
                    return;
                case 32:
                    Kernel[OffsetToSlotArraySelected + 32] = Convert.ToByte(variable); //Count 8
                    return;
                case 33:
                    Kernel[OffsetToSlotArraySelected + 33] = Convert.ToByte(variable); //Count 8
                    return;
                case 34:
                    Kernel[OffsetToSlotArraySelected + 34] = Convert.ToByte(variable); //Count 8
                    return;
                case 35:
                    Kernel[OffsetToSlotArraySelected + 35] = Convert.ToByte(variable); //Count 8
                    return;
                case 36:
                    Kernel[OffsetToSlotArraySelected + 36] = Convert.ToByte(variable); //Count 8
                    return;
                case 37:
                    Kernel[OffsetToSlotArraySelected + 37] = Convert.ToByte(variable); //Count 8
                    return;
                case 38:
                    Kernel[OffsetToSlotArraySelected + 38] = Convert.ToByte(variable); //Count 8
                    return;
                case 39:
                    Kernel[OffsetToSlotArraySelected + 39] = Convert.ToByte(variable); //Count 8
                    return;
                case 40:
                    Kernel[OffsetToSlotArraySelected + 40] = Convert.ToByte(variable); //Count 8
                    return;
                case 41:
                    Kernel[OffsetToSlotArraySelected + 41] = Convert.ToByte(variable); //Count 8
                    return;
                case 42:
                    Kernel[OffsetToSlotArraySelected + 42] = Convert.ToByte(variable); //Count 8
                    return;
                case 43:
                    Kernel[OffsetToSlotArraySelected + 43] = Convert.ToByte(variable); //Count 8
                    return;
                case 44:
                    Kernel[OffsetToSlotArraySelected + 44] = Convert.ToByte(variable); //Count 8
                    return;
                case 45:
                    Kernel[OffsetToSlotArraySelected + 45] = Convert.ToByte(variable); //Count 8
                    return;
                case 46:
                    Kernel[OffsetToSlotArraySelected + 46] = Convert.ToByte(variable); //Count 8
                    return;
                case 47:
                    Kernel[OffsetToSlotArraySelected + 47] = Convert.ToByte(variable); //Count 8
                    return;
                case 48:
                    Kernel[OffsetToSlotArraySelected + 48] = Convert.ToByte(variable); //Count 8
                    return;
                case 49:
                    Kernel[OffsetToSlotArraySelected + 49] = Convert.ToByte(variable); //Count 8
                    return;
                case 50:
                    Kernel[OffsetToSlotArraySelected + 50] = Convert.ToByte(variable); //Count 8
                    return;
                case 51:
                    Kernel[OffsetToSlotArraySelected + 51] = Convert.ToByte(variable); //Count 8
                    return;
                case 52:
                    Kernel[OffsetToSlotArraySelected + 52] = Convert.ToByte(variable); //Count 8
                    return;
                case 53:
                    Kernel[OffsetToSlotArraySelected + 53] = Convert.ToByte(variable); //Count 8
                    return;
                case 54:
                    Kernel[OffsetToSlotArraySelected + 54] = Convert.ToByte(variable); //Count 8
                    return;
                case 55:
                    Kernel[OffsetToSlotArraySelected + 55] = Convert.ToByte(variable); //Count 8
                    return;
                case 56:
                    Kernel[OffsetToSlotArraySelected + 56] = Convert.ToByte(variable); //Count 8
                    return;
                case 57:
                    Kernel[OffsetToSlotArraySelected + 57] = Convert.ToByte(variable); //Count 8
                    return;
                case 58:
                    Kernel[OffsetToSlotArraySelected + 58] = Convert.ToByte(variable); //Count 8
                    return;
                case 59:
                    Kernel[OffsetToSlotArraySelected + 59] = Convert.ToByte(variable); //Count 8
                    return;

                default:
                    return;
            }
        }
        public static void UpdateVariable_SlotsSets(int index, object variable)
        {
            if (!mainForm._loaded || Kernel == null)
                return;
            switch (index)
            {
                case 0:
                    Kernel[OffsetToSlotsSetsSelected] = Convert.ToByte(variable); //Magic 1
                    return;
                case 1:
                    Kernel[OffsetToSlotsSetsSelected + 1] = Convert.ToByte(variable); //Count 1
                    return;
                case 2:
                    Kernel[OffsetToSlotsSetsSelected + 2] = Convert.ToByte(variable); //Magic 2
                    return;
                case 3:
                    Kernel[OffsetToSlotsSetsSelected + 3] = Convert.ToByte(variable); //Count 2
                    return;
                case 4:
                    Kernel[OffsetToSlotsSetsSelected + 4] = Convert.ToByte(variable); //Magic 3
                    return;
                case 5:
                    Kernel[OffsetToSlotsSetsSelected + 5] = Convert.ToByte(variable); //Count 3
                    return;
                case 6:
                    Kernel[OffsetToSlotsSetsSelected + 6] = Convert.ToByte(variable); //Magic 4
                    return;
                case 7:
                    Kernel[OffsetToSlotsSetsSelected + 7] = Convert.ToByte(variable); //Count 4
                    return;
                case 8:
                    Kernel[OffsetToSlotsSetsSelected + 8] = Convert.ToByte(variable); //Magic 5
                    return;
                case 9:
                    Kernel[OffsetToSlotsSetsSelected + 9] = Convert.ToByte(variable); //Count 5
                    return;
                case 10:
                    Kernel[OffsetToSlotsSetsSelected + 10] = Convert.ToByte(variable); //Magic 6
                    return;
                case 11:
                    Kernel[OffsetToSlotsSetsSelected + 11] = Convert.ToByte(variable); //Count 6
                    return;
                case 12:
                    Kernel[OffsetToSlotsSetsSelected + 12] = Convert.ToByte(variable); //Magic 7
                    return;
                case 13:
                    Kernel[OffsetToSlotsSetsSelected + 13] = Convert.ToByte(variable); //Count 7
                    return;
                case 14:
                    Kernel[OffsetToSlotsSetsSelected + 14] = Convert.ToByte(variable); //Magic 8
                    return;
                case 15:
                    Kernel[OffsetToSlotsSetsSelected + 15] = Convert.ToByte(variable); //Count 8
                    return;


                default:
                    return;
            }
        }

        #endregion

        #region DEVOUR

        public static void UpdateVariable_Devour(int index, object variable, byte arg0 = 127, object sender = null)
        {
            if (!mainForm._loaded || Kernel == null)
                return;
            switch (index)
            {
                case 0:
                    Kernel[OffsetToDevourSelected + 2] = Convert.ToByte(variable); //HP Heal/Damage
                    return;
                case 1:
                    Kernel[OffsetToDevourSelected + 3] = (byte)(Kernel[OffsetToDevourSelected + 3] ^ Convert.ToByte(variable)); //HP Quantity Flag
                    return;
                case 2:
                    DevourStatusUpdator(arg0, variable); //Status
                    return;
                case 3:
                    Kernel[OffsetToDevourSelected + 10] = (byte)(Kernel[OffsetToDevourSelected + 10] ^ Convert.ToByte(variable)); //Raised Stats Flag
                    return;
                case 4:
                    Kernel[OffsetToDevourSelected + 11] = Convert.ToByte(variable); //Raised HP Quantity
                    return;
                case 5:
                    //Description
                    return;

                default:
                    return;
            }
        }
        private static void DevourStatusUpdator(byte StatusByteIndex, object variable)
        {
            switch (StatusByteIndex)
            {
                case 0:
                    Kernel[OffsetToDevourSelected + 8] = (byte)(Kernel[OffsetToDevourSelected + 8] ^ Convert.ToByte(variable));
                    return;
                case 1:
                    Kernel[OffsetToDevourSelected + 4] = (byte)(Kernel[OffsetToDevourSelected + 4] ^ Convert.ToByte(variable));
                    return;
                case 2:
                    Kernel[OffsetToDevourSelected + 5] = (byte)(Kernel[OffsetToDevourSelected + 5] ^ Convert.ToByte(variable));
                    return;
                case 3:
                    Kernel[OffsetToDevourSelected + 6] = (byte)(Kernel[OffsetToDevourSelected + 6] ^ Convert.ToByte(variable));
                    return;
                case 4:
                    Kernel[OffsetToDevourSelected + 7] = (byte)(Kernel[OffsetToDevourSelected + 7] ^ Convert.ToByte(variable));
                    return;
            }
        }

        #endregion

        #region MISC

        public static void UpdateVariable_Misc(int index, object variable, object sender = null)
        {
            if (!mainForm._loaded || Kernel == null)
                return;
            switch (index)
            {
                case 0:
                    Kernel[OffsetToMiscSelected] = Convert.ToByte(variable); //Status Timer 1
                    return;
                case 1:
                    Kernel[OffsetToMiscSelected + 1] = Convert.ToByte(variable); //Status Timer 2
                    return;
                case 2:
                    Kernel[OffsetToMiscSelected + 2] = Convert.ToByte(variable); //Status Timer 3
                    return;
                case 3:
                    Kernel[OffsetToMiscSelected + 3] = Convert.ToByte(variable); //Status Timer 4
                    return;
                case 4:
                    Kernel[OffsetToMiscSelected + 4] = Convert.ToByte(variable); //Status Timer 5
                    return;
                case 5:
                    Kernel[OffsetToMiscSelected + 5] = Convert.ToByte(variable); //Status Timer 6
                    return;
                case 6:
                    Kernel[OffsetToMiscSelected + 6] = Convert.ToByte(variable); //Status Timer 7
                    return;
                case 7:
                    Kernel[OffsetToMiscSelected + 7] = Convert.ToByte(variable); //Status Timer 8
                    return;
                case 8:
                    Kernel[OffsetToMiscSelected + 8] = Convert.ToByte(variable); //Status Timer 9
                    return;
                case 9:
                    Kernel[OffsetToMiscSelected + 9] = Convert.ToByte(variable); //Status Timer 10
                    return;
                case 10:
                    Kernel[OffsetToMiscSelected + 10] = Convert.ToByte(variable); //Status Timer 11
                    return;
                case 11:
                    Kernel[OffsetToMiscSelected + 11] = Convert.ToByte(variable); //Status Timer 12
                    return;
                case 12:
                    Kernel[OffsetToMiscSelected + 12] = Convert.ToByte(variable); //Status Timer 13
                    return;
                case 13:
                    Kernel[OffsetToMiscSelected + 13] = Convert.ToByte(variable); //Status Timer 14
                    return;
                case 14:
                    Kernel[OffsetToMiscSelected + 14] = Convert.ToByte(variable); //ATB Multiplier
                    return;
                case 15:
                    Kernel[OffsetToMiscSelected + 15] = Convert.ToByte(variable); //Dead Timer
                    return;
                case 16:
                    Kernel[OffsetToMiscSelected + 16] = Convert.ToByte(variable); //Status Limit 1
                    return;
                case 17:
                    Kernel[OffsetToMiscSelected + 17] = Convert.ToByte(variable); //Status Limit 2
                    return;
                case 18:
                    Kernel[OffsetToMiscSelected + 18] = Convert.ToByte(variable); //Status Limit 3
                    return;
                case 19:
                    Kernel[OffsetToMiscSelected + 19] = Convert.ToByte(variable); //Status Limit 4
                    return;
                case 20:
                    Kernel[OffsetToMiscSelected + 20] = Convert.ToByte(variable); //Status Limit 5
                    return;
                case 21:
                    Kernel[OffsetToMiscSelected + 21] = Convert.ToByte(variable); //Status Limit 6
                    return;
                case 22:
                    Kernel[OffsetToMiscSelected + 22] = Convert.ToByte(variable); //Status Limit 7
                    return;
                case 23:
                    Kernel[OffsetToMiscSelected + 23] = Convert.ToByte(variable); //Status Limit 8
                    return;
                case 24:
                    Kernel[OffsetToMiscSelected + 24] = Convert.ToByte(variable); //Status Limit 9
                    return;
                case 25:
                    Kernel[OffsetToMiscSelected + 25] = Convert.ToByte(variable); //Status Limit 10
                    return;
                case 26:
                    Kernel[OffsetToMiscSelected + 26] = Convert.ToByte(variable); //Status Limit 11
                    return;
                case 27:
                    Kernel[OffsetToMiscSelected + 27] = Convert.ToByte(variable); //Status Limit 12
                    return;
                case 28:
                    Kernel[OffsetToMiscSelected + 28] = Convert.ToByte(variable); //Status Limit 13
                    return;
                case 29:
                    Kernel[OffsetToMiscSelected + 29] = Convert.ToByte(variable); //Status Limit 14
                    return;
                case 30:
                    Kernel[OffsetToMiscSelected + 30] = Convert.ToByte(variable); //Status Limit 15
                    return;
                case 31:
                    Kernel[OffsetToMiscSelected + 31] = Convert.ToByte(variable); //Status Limit 16
                    return;
                case 32:
                    Kernel[OffsetToMiscSelected + 32] = Convert.ToByte(variable); //Status Limit 17
                    return;
                case 33:
                    Kernel[OffsetToMiscSelected + 33] = Convert.ToByte(variable); //Status Limit 18
                    return;
                case 34:
                    Kernel[OffsetToMiscSelected + 34] = Convert.ToByte(variable); //Status Limit 19
                    return;
                case 35:
                    Kernel[OffsetToMiscSelected + 35] = Convert.ToByte(variable); //Status Limit 20
                    return;
                case 36:
                    Kernel[OffsetToMiscSelected + 36] = Convert.ToByte(variable); //Status Limit 21
                    return;
                case 37:
                    Kernel[OffsetToMiscSelected + 37] = Convert.ToByte(variable); //Status Limit 22
                    return;
                case 38:
                    Kernel[OffsetToMiscSelected + 38] = Convert.ToByte(variable); //Status Limit 23
                    return;
                case 39:
                    Kernel[OffsetToMiscSelected + 39] = Convert.ToByte(variable); //Status Limit 24
                    return;
                case 40:
                    Kernel[OffsetToMiscSelected + 40] = Convert.ToByte(variable); //Status Limit 25
                    return;
                case 41:
                    Kernel[OffsetToMiscSelected + 41] = Convert.ToByte(variable); //Status Limit 26
                    return;
                case 42:
                    Kernel[OffsetToMiscSelected + 42] = Convert.ToByte(variable); //Status Limit 27
                    return;
                case 43:
                    Kernel[OffsetToMiscSelected + 43] = Convert.ToByte(variable); //Status Limit 28
                    return;
                case 44:
                    Kernel[OffsetToMiscSelected + 44] = Convert.ToByte(variable); //Status Limit 29
                    return;
                case 45:
                    Kernel[OffsetToMiscSelected + 45] = Convert.ToByte(variable); //Status Limit 30
                    return;
                case 46:
                    Kernel[OffsetToMiscSelected + 46] = Convert.ToByte(variable); //Status Limit 31
                    return;
                case 47:
                    Kernel[OffsetToMiscSelected + 47] = Convert.ToByte(variable); //Status Limit 32
                    return;
                case 48:
                    Kernel[OffsetToMiscSelected + 48] = Convert.ToByte(variable); //Duel Timer CL1
                    return;
                case 49:
                    Kernel[OffsetToMiscSelected + 49] = Convert.ToByte(variable); //Duel Start Seq CL1
                    return;
                case 50:
                    Kernel[OffsetToMiscSelected + 50] = Convert.ToByte(variable); //Duel Timer CL2
                    return;
                case 51:
                    Kernel[OffsetToMiscSelected + 51] = Convert.ToByte(variable); //Duel Start Seq CL2
                    return;
                case 52:
                    Kernel[OffsetToMiscSelected + 52] = Convert.ToByte(variable); //Duel Timer CL3
                    return;
                case 53:
                    Kernel[OffsetToMiscSelected + 53] = Convert.ToByte(variable); //Duel Start Seq CL3
                    return;
                case 54:
                    Kernel[OffsetToMiscSelected + 54] = Convert.ToByte(variable); //Duel Timer CL4
                    return;
                case 55:
                    Kernel[OffsetToMiscSelected + 55] = Convert.ToByte(variable); //Duel Start Seq CL4
                    return;
                case 56:
                    Kernel[OffsetToMiscSelected + 56] = Convert.ToByte(variable); //Shot Timer CL1
                    return;
                case 57:
                    Kernel[OffsetToMiscSelected + 57] = Convert.ToByte(variable); //Shot Timer CL2
                    return;
                case 58:
                    Kernel[OffsetToMiscSelected + 58] = Convert.ToByte(variable); //Shot Timer CL3
                    return;
                case 59:
                    Kernel[OffsetToMiscSelected + 59] = Convert.ToByte(variable); //Shot Timer CL4
                    return;

                default:
                    return;
            }
        }

        #endregion

        #region COMMAND ABILITY DATA

        public static void UpdateVariable_CommandAbilityData(int index, object variable, byte arg0 = 127, object sender = null)
        {
            if (!mainForm._loaded || Kernel == null)
                return;
            switch (index)
            {
                case 0:
                    UshortToKernel(Convert.ToUInt16(variable), 0, (byte)Mode.Mode_CommandAbilityData); //MagicID
                    return;
                case 1:
                    Kernel[OffsetToCommandAbilityDataSelected + 4] = Convert.ToByte(variable); //Attack type
                    return;
                case 2:
                    Kernel[OffsetToCommandAbilityDataSelected + 5] = Convert.ToByte(variable); //Attack power
                    return;
                case 3:
                    Kernel[OffsetToCommandAbilityDataSelected + 6] = (byte)(Kernel[OffsetToCommandAbilityDataSelected + 6] ^ Convert.ToByte(variable)); //attack flags
                    return;
                case 4:
                    Kernel[OffsetToCommandAbilityDataSelected + 7] = Convert.ToByte(variable); //Hit Count
                    return;
                case 5:
                    Kernel[OffsetToCommandAbilityDataSelected + 8] = Convert.ToByte(variable); //Element
                    return;
                case 6:
                    Kernel[OffsetToCommandAbilityDataSelected + 9] = Convert.ToByte(variable); //Status Attack
                    return;
                case 7:
                    CommandAbilityDataStatusUpdator(arg0, variable); //Status
                    return;

                default:
                    return;
            }
        }
        private static void CommandAbilityDataStatusUpdator(byte StatusByteIndex, object variable)
        {
            switch (StatusByteIndex)
            {
                case 0:
                    Kernel[OffsetToCommandAbilityDataSelected + 10] = (byte)(Kernel[OffsetToCommandAbilityDataSelected + 10] ^ Convert.ToByte(variable));
                    return;
                case 1:
                    Kernel[OffsetToCommandAbilityDataSelected + 12] = (byte)(Kernel[OffsetToCommandAbilityDataSelected + 12] ^ Convert.ToByte(variable));
                    return;
                case 2:
                    Kernel[OffsetToCommandAbilityDataSelected + 13] = (byte)(Kernel[OffsetToCommandAbilityDataSelected + 13] ^ Convert.ToByte(variable));
                    return;
                case 3:
                    Kernel[OffsetToCommandAbilityDataSelected + 14] = (byte)(Kernel[OffsetToCommandAbilityDataSelected + 14] ^ Convert.ToByte(variable));
                    return;
                case 4:
                    Kernel[OffsetToCommandAbilityDataSelected + 15] = (byte)(Kernel[OffsetToCommandAbilityDataSelected + 15] ^ Convert.ToByte(variable));
                    return;
            }
        }

        #endregion

        #region COMMAND ABILITY

        public static void UpdateVariable_CommandAbility(int index, object variable, object sender = null)
        {
            if (!mainForm._loaded || Kernel == null)
                return;
            switch (index)
            {
                case 0:
                    Kernel[OffsetToCommandAbilitySelected + 4] = Convert.ToByte(variable); //AP
                    return;
                case 1:
                    Kernel[OffsetToCommandAbilitySelected + 5] = Convert.ToByte(variable); //Battle Command
                    return;
                case 2:
                    //Name
                    return;
                case 3:
                    //Description
                    return;


                default:
                    return;
            }
        }

        #endregion

        #region JUNCTION ABILITIES

        public static void UpdateVariable_JunctionAbilities(int index, object variable, object sender = null)
        {
            if (!mainForm._loaded || Kernel == null)
                return;
            switch (index)
            {
                case 0:
                    Kernel[OffsetToJunctionAbilitiesSelected + 4] = Convert.ToByte(variable); //AP
                    return;
                case 1:
                    Kernel[OffsetToJunctionAbilitiesSelected + 5] = (byte)(Kernel[OffsetToJunctionAbilitiesSelected + 5] ^ Convert.ToByte(variable)); // flag 1
                    return;
                case 2:
                    Kernel[OffsetToJunctionAbilitiesSelected + 6] = (byte)(Kernel[OffsetToJunctionAbilitiesSelected + 6] ^ Convert.ToByte(variable)); // flag 2
                    return;
                case 3:
                    Kernel[OffsetToJunctionAbilitiesSelected + 7] = (byte)(Kernel[OffsetToJunctionAbilitiesSelected + 7] ^ Convert.ToByte(variable)); // flag 3
                    return;
                case 4:
                    //Name
                    return;
                case 5:
                    //Description
                    return;

                default:
                    return;
            }
        }

        #endregion

        #region PARTY ABILITIES

        public static void UpdateVariable_PartyAbilities(int index, object variable, object sender = null)
        {
            if (!mainForm._loaded || Kernel == null)
                return;
            switch (index)
            {
                case 0:
                    Kernel[OffsetToPartyAbilitiesSelected + 4] = Convert.ToByte(variable); //AP
                    return;
                case 1:
                    Kernel[OffsetToPartyAbilitiesSelected + 5] = (byte)(Kernel[OffsetToPartyAbilitiesSelected + 5] ^ Convert.ToByte(variable)); // flag
                    return;
                case 2:
                    //Name
                    return;
                case 3:
                    //Description
                    return;

                default:
                    return;
            }
        }

        #endregion

        #region GF ABILITIES

        public static void UpdateVariable_GFAbilities(int index, object variable, object sender = null)
        {
            if (!mainForm._loaded || Kernel == null)
                return;
            switch (index)
            {
                case 0:
                    Kernel[OffsetToGFAbilitiesSelected + 4] = Convert.ToByte(variable); //AP
                    return;
                case 1:
                    Kernel[OffsetToGFAbilitiesSelected + 5] = (byte)(Kernel[OffsetToGFAbilitiesSelected + 5] ^ Convert.ToByte(variable)); // enable boost
                    return;
                case 2:
                    Kernel[OffsetToGFAbilitiesSelected + 6] = Convert.ToByte(variable); //stat to increase
                    return;
                case 3:
                    Kernel[OffsetToGFAbilitiesSelected + 7] = Convert.ToByte(variable); //increasement value
                    return;
                case 4:
                    //Name
                    return;
                case 5:
                    //Description
                    return;

                default:
                    return;
            }
        }

        #endregion

        #region CHARACTER ABILITIES

        public static void UpdateVariable_CharacterAbilities(int index, object variable, object sender = null)
        {
            if (!mainForm._loaded || Kernel == null)
                return;
            switch (index)
            {
                case 0:
                    Kernel[OffsetToCharacterAbilitiesSelected + 4] = Convert.ToByte(variable); //AP
                    return;
                case 1:
                    Kernel[OffsetToCharacterAbilitiesSelected + 5] = (byte)(Kernel[OffsetToCharacterAbilitiesSelected + 5] ^ Convert.ToByte(variable)); // flag 1
                    return;
                case 2:
                    Kernel[OffsetToCharacterAbilitiesSelected + 6] = (byte)(Kernel[OffsetToCharacterAbilitiesSelected + 6] ^ Convert.ToByte(variable)); // flag 2
                    return;
                case 3:
                    Kernel[OffsetToCharacterAbilitiesSelected + 7] = (byte)(Kernel[OffsetToCharacterAbilitiesSelected + 7] ^ Convert.ToByte(variable)); // flag 3
                    return;
                case 4:
                    //Name
                    return;
                case 5:
                    //Description
                    return;

                default:
                    return;
            }
        }

        #endregion

        #region MENU ABILITIES

        public static void UpdateVariable_MenuAbilities(int index, object variable, object sender = null)
        {
            if (!mainForm._loaded || Kernel == null)
                return;
            switch (index)
            {
                case 0:
                    Kernel[OffsetToMenuAbilitiesSelected + 4] = Convert.ToByte(variable); //AP
                    return;
                case 1:
                    Kernel[OffsetToMenuAbilitiesSelected + 5] = Convert.ToByte(variable); //Index to m00X files
                    return;
                case 2:
                    Kernel[OffsetToMenuAbilitiesSelected + 6] = Convert.ToByte(variable); //Start entry
                    return;
                case 3:
                    Kernel[OffsetToMenuAbilitiesSelected + 7] = Convert.ToByte(variable); //End entry
                    return;
                case 4:
                    //Name
                    return;
                case 5:
                    //Description
                    return;

                default:
                    return;
            }
        }

        #endregion

        #region BATTLE COMMANDS

        public static void UpdateVariable_BattleCommands(int index, object variable, object sender = null, int Entry = 0)
        {
            if (!mainForm._loaded || Kernel == null)
                return;
            switch (index)
            {
                case 0:
                    Kernel[OffsetToBattleCommandsSelected + 4] = Convert.ToByte(variable); //Ability ID
                    return;
                case 1:
                    Kernel[OffsetToBattleCommandsSelected + 5] = (byte)(Kernel[OffsetToBattleCommandsSelected + 5] ^ Convert.ToByte(variable)); //Flag
                    return;
                case 2:
                    Kernel[OffsetToBattleCommandsSelected + 6] = Convert.ToByte(variable); //Target
                    return;
                case 3: //name
                    int newLength = (sender as TextBox).Text.Length;
                    if(BitConverter.ToUInt16(Kernel,(int)TextOffsets[Entry,0]) == 0xFFFF)
                        return; //NULL name
                    int textLEA = BitConverter.ToUInt16(Kernel, (int) TextOffsets[Entry, 0]);
                    string s = FF8Text.BuildString(textLEA + (int)KernelSections.Text_BattleCommand);
                    if (s.Length == newLength)
                    {
                        byte[] buffer = FF8Text.Cipher((sender as TextBox).Text);
                        for (int i = 0; i != buffer.Length; i++)
                            Kernel[i + TextOffsets[Entry, 0]] = buffer[i];
                    }
                    else
                        Text_MovePointers(Entry, (sender as TextBox).Text, (sender as TextBox).Text.Length - s.Length);
                    return;
                case 4:
                    //Description
                    return;

                default:
                    return;
            }
        }

        #endregion

        #region RINOA COMMANDS

        public static void UpdateVariable_RinoaCommands(int index, object variable, object sender = null)
        {
            if (!mainForm._loaded || Kernel == null)
                return;
            switch (index)
            {
                case 0:
                    Kernel[OffsetToRinoaCommandsSelected + 4] = (byte)(Kernel[OffsetToRinoaCommandsSelected + 4] ^ Convert.ToByte(variable)); //Flag
                    return;
                case 1:
                    Kernel[OffsetToRinoaCommandsSelected + 5] = Convert.ToByte(variable); //Target
                    return;
                case 2:
                    Kernel[OffsetToRinoaCommandsSelected + 6] = Convert.ToByte(variable); //Ability ID
                    return;
                case 3:
                    //Name
                    return;
                case 4:
                    //Description
                    return;

                default:
                    return;
            }
        }

        #endregion

        #endregion


        #region DUEL BUTTONS TO KERNEL

        /// <summary>
        /// This is for Duel buttons
        /// </summary>
        /// <param name="a"></param>
        /// <param name="offset_to_button"></param>
        private static void ButtonsToKernel(ushort a, int offset_to_button)
        {
            Kernel[OffsetToDuelSelected + offset_to_button] = (byte)a;
            Kernel[OffsetToDuelSelected + offset_to_button + 1] = (byte)(a >> 8);
        }

        #endregion

        #region MAGIC ID TO KERNEL
        private static void UshortToKernel(ushort a, int add, byte mode)
        {
            byte[] magicIdBytes = BitConverter.GetBytes(a);
            switch (mode)
            {
                case (byte) Mode.Mode_Magic:
                    Array.Copy(magicIdBytes, 0, Kernel, OffsetToMagicSelected + add, 2);
                    break;
                case (byte) Mode.Mode_GF:
                    Array.Copy(magicIdBytes, 0, Kernel, OffsetToGFSelected + add, 2);
                    break;
                case (byte)Mode.Mode_GFAttacks:
                    Array.Copy(magicIdBytes, 0, Kernel, OffsetToGFAttacksSelected + add, 2);
                    break;
                case (byte)Mode.Mode_EnemyAttacks:
                    Array.Copy(magicIdBytes, 0, Kernel, OffsetToEnemyAttacksSelected + add, 2);
                    break;
                case (byte)Mode.Mode_BlueMagic:
                    Array.Copy(magicIdBytes, 0, Kernel, OffsetToBlueMagicSelected + add, 2);
                    break;
                case (byte)Mode.Mode_RenzoFin:
                    Array.Copy(magicIdBytes, 0, Kernel, OffsetToRenzoFinSelected + add, 2);
                    break;
                case (byte)Mode.Mode_TempCharLB:
                    Array.Copy(magicIdBytes, 0, Kernel, OffsetToTempCharLBSelected + add, 2);
                    break;
                case (byte)Mode.Mode_Shot:
                    Array.Copy(magicIdBytes, 0, Kernel, OffsetToShotSelected + add, 2);
                    break;
                case (byte)Mode.Mode_Duel:
                    Array.Copy(magicIdBytes, 0, Kernel, OffsetToDuelSelected + add, 2);
                    break;
                case (byte)Mode.Mode_Combine:
                    Array.Copy(magicIdBytes, 0, Kernel, OffsetToCombineSelected + add, 2);
                    break;
                case (byte)Mode.Mode_BattleItems:
                    Array.Copy(magicIdBytes, 0, Kernel, OffsetToBattleItemsSelected + add, 2);
                    break;
                case (byte)Mode.Mode_CommandAbilityData:
                    Array.Copy(magicIdBytes, 0, Kernel, OffsetToCommandAbilityDataSelected + add, 2);
                    break;

                default:
                    return;
            }
        }

        enum Mode : byte
        {
            Mode_Magic,
            Mode_GF,
            Mode_GFAttacks,
            Mode_EnemyAttacks,
            Mode_BlueMagic,
            Mode_RenzoFin,
            Mode_TempCharLB,
            Mode_Shot,
            Mode_Duel,
            Mode_Combine,
            Mode_BattleItems,
            Mode_CommandAbilityData
        }

        #endregion


        #region READ KERNEL VARIABLES

        #region KERNEL OFFSETS

        public static void ReadKernel(byte[] kernel)
        {
            Kernel = kernel;
            FF8Text.SetKernel(Kernel);

            BattleCommandsDataOffset = BitConverter.ToInt32(Kernel, (int)KernelSections.BattleCommands);
            MagicDataOffset = BitConverter.ToInt32(Kernel, (int)KernelSections.MagicData);
            GFDataOffset = BitConverter.ToInt32(Kernel, (int)KernelSections.GFs);
            GFAttacksDataOffset = BitConverter.ToInt32(Kernel, (int)KernelSections.GFAttacks);
            WeaponsDataOffset = BitConverter.ToInt32(Kernel, (int)KernelSections.Weapons);
            CharactersDataOffset = BitConverter.ToInt32(Kernel, (int)KernelSections.Characters);
            EnemyAttacksDataOffset = BitConverter.ToInt32(Kernel, (int)KernelSections.EnemyAttacks);
            BlueMagicDataOffset = BitConverter.ToInt32(Kernel, (int)KernelSections.BlueMagic);
            BlueMagicParamDataOffset = BitConverter.ToInt32(Kernel, (int)KernelSections.BlueMagicParam);
            StatPercentageAbilitiesDataOffset = BitConverter.ToInt32(Kernel, (int)KernelSections.StatPercentageIncreasingAbilities);
            RenzoFinDataOffset = BitConverter.ToInt32(Kernel, (int)KernelSections.RenzokukenFinisher);
            TempCharLBDataOffset = BitConverter.ToInt32(Kernel, (int)KernelSections.TempCharacterLimitBreakes);
            ShotDataOffset = BitConverter.ToInt32(Kernel, (int)KernelSections.Shot_Irvine);
            DuelDataOffset = BitConverter.ToInt32(Kernel, (int)KernelSections.Duel_Zell);
            DuelParamsDataOffset = BitConverter.ToInt32(Kernel, (int)KernelSections.Duel_ZellParam);
            CombineDataOffset = BitConverter.ToInt32(Kernel, (int)KernelSections.RinoaLimit2);
            BattleItemsDataOffset = BitConverter.ToInt32(Kernel, (int)KernelSections.BattleItems);
            ItemsDataOffset = BitConverter.ToInt32(Kernel, (int)KernelSections.NonBattleItems);
            SlotArrayDataOffset = BitConverter.ToInt32(Kernel, (int)KernelSections.SelphieSlotArray);
            SlotsSetsDataOffset = BitConverter.ToInt32(Kernel, (int)KernelSections.SelphieSlotsSets);
            DevourDataOffset = BitConverter.ToInt32(Kernel, (int)KernelSections.Devour);
            MiscDataOffset = BitConverter.ToInt32(Kernel, (int)KernelSections.Misc);
            CommandAbilityDataDataOffset = BitConverter.ToInt32(Kernel, (int)KernelSections.CommandAbilityData);
            CommandAbilityDataOffset = BitConverter.ToInt32(Kernel, (int)KernelSections.CommandAbilities);
            JunctionAbilitiesDataOffset = BitConverter.ToInt32(Kernel, (int)KernelSections.JunctionAbilities);
            PartyAbilitiesDataOffset = BitConverter.ToInt32(Kernel, (int)KernelSections.PartyAbilities);
            GFAbilitiesDataOffset = BitConverter.ToInt32(Kernel, (int)KernelSections.GFAbilities);
            CharacterAbilitiesDataOffset = BitConverter.ToInt32(Kernel, (int)KernelSections.CharacterAbilities);
            MenuAbilitiesDataOffset = BitConverter.ToInt32(Kernel, (int)KernelSections.MenuAbilities);
            RinoaCommandsDataOffset = BitConverter.ToInt32(Kernel, (int)KernelSections.RinoaLimit1);

            InitializeTextPointers();

        }

        

        #endregion


        #region MAGIC

        public static void ReadMagic(int MagicID_List, byte[] Kernel)
        {
            GetSelectedMagicData = new MagicData();
            MagicID_List++;//skip dummy entry

            int selectedMagicOffset = MagicDataOffset + (MagicID_List * 60);
            OffsetToMagicSelected = selectedMagicOffset;

            GetSelectedMagicData.OffsetSpellName = FF8Text.BuildString((ushort)(
                    BitConverter.ToInt32(Kernel, (int)KernelSections.Text_Magictext) + BitConverter.ToUInt16(Kernel, selectedMagicOffset)));
            GetSelectedMagicData.OffsetSpellDescription = FF8Text.BuildString((ushort)(
                BitConverter.ToInt32(Kernel, (int)KernelSections.Text_Magictext) + (BitConverter.ToUInt16(Kernel, selectedMagicOffset + 2))));
            GetSelectedMagicData.MagicID = BitConverter.ToUInt16(Kernel, selectedMagicOffset + 4);
            selectedMagicOffset += 6;
            GetSelectedMagicData.Unknown1 = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.AttackType = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.SpellPower = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.Unknown2 = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.DefaultTarget = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.Flags = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.DrawResist = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.HitCount = Kernel[selectedMagicOffset++];
            byte b = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.Element =
                b == (byte)Element.Fire
                    ? Element.Fire
                    : b == (byte)Element.Holy
                        ? Element.Holy
                        : b == (byte)Element.Ice
                            ? Element.Ice
                            : b == (byte)Element.NonElemental
                                ? Element.NonElemental
                                : b == (byte)Element.Poison
                                    ? Element.Poison
                                    : b == (byte)Element.Thunder
                                        ? Element.Thunder
                                        : b == (byte)Element.Water
                                            ? Element.Water
                                            : b == (byte)Element.Wind
                                                ? Element.Wind
                                                : b == (byte)Element.Earth
                                                    ? Element.Earth
                                                    : 0; //Error handler
            GetSelectedMagicData.Unknown4 = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.StatusMagic1 = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.StatusMagic2 = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.StatusMagic3 = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.StatusMagic4 = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.StatusMagic5 = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.Unknown5 = BitConverter.ToUInt16(Kernel, selectedMagicOffset ++);
            GetSelectedMagicData.StatusAttack = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.HP = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.STR = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.VIT = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.MAG = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.SPR = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.SPD = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.EVA = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.HIT = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.LUCK = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.ElemAttackEN = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.ElemAttackVAL = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.ElemDefenseEN = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.ElemDefenseVAL = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.StatusATKval = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.StatusDEFval = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.StatusATKEN = BitConverter.ToUInt16(Kernel, selectedMagicOffset);
            selectedMagicOffset += 2;
            GetSelectedMagicData.StatusDefEN = BitConverter.ToUInt16(Kernel, selectedMagicOffset);
            selectedMagicOffset += 2;
            GetSelectedMagicData.QuezacoltCompatibility = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.ShivaCompatibility = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.IfritCompatibility = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.SirenCompatibility = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.BrothersCompatibility = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.DiablosCompatibility = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.CarbuncleCompatibility = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.LeviathanCompatibility = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.PandemonaCompatibility = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.CerberusCompatibility = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.AlexanderCompatibility = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.DoomtrainCompatibility = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.BahamutCompatibility = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.CactuarCompatibility = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.TonberryCompatibility = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.EdenCompatibility = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.Unknown6 = new byte[2];
            Array.Copy(Kernel, selectedMagicOffset, GetSelectedMagicData.Unknown6, 0, 2);
        }

        #endregion

        #region J-GF

        public static void ReadGF(int GFID_List, byte[] Kernel)
        {
            GetSelectedGFData = new GFData();
            int selectedGfOffset = GFDataOffset + (GFID_List * 132);
            OffsetToGFSelected = selectedGfOffset;

            GetSelectedGFData.OffsetGFAttackName = FF8Text.BuildString((ushort)(
                BitConverter.ToInt32(Kernel, (int)KernelSections.Text_JunctionableGF) + (BitConverter.ToUInt16(Kernel, selectedGfOffset))));
            GetSelectedGFData.OffsetGFAttackDescription = FF8Text.BuildString((ushort)(
                BitConverter.ToInt32(Kernel, (int)KernelSections.Text_JunctionableGF) + (BitConverter.ToUInt16(Kernel, selectedGfOffset + 2))));
            GetSelectedGFData.GFMagicID = (ushort)(BitConverter.ToUInt16(Kernel, selectedGfOffset + 4));
            selectedGfOffset += 6; //Name Offset + Description Offset + MagicID
            GetSelectedGFData.GFAttackType = Kernel[selectedGfOffset++];
            GetSelectedGFData.GFPower = Kernel[selectedGfOffset];
            selectedGfOffset += 3; //Unknown + GFPower
            GetSelectedGFData.GFFlags = Kernel[selectedGfOffset];
            selectedGfOffset += 3;
            byte b = Kernel[selectedGfOffset++];
            GetSelectedGFData.ElementGF =
                b == (byte)Element.Fire
                    ? Element.Fire
                    : b == (byte)Element.Holy
                        ? Element.Holy
                        : b == (byte)Element.Ice
                            ? Element.Ice
                            : b == (byte)Element.NonElemental
                                ? Element.NonElemental
                                : b == (byte)Element.Poison
                                    ? Element.Poison
                                    : b == (byte)Element.Thunder
                                        ? Element.Thunder
                                        : b == (byte)Element.Water
                                            ? Element.Water
                                            : b == (byte)Element.Wind
                                                ? Element.Wind
                                                : b == (byte)Element.Earth
                                                    ? Element.Earth
                                                    : 0; //Error handler
            GetSelectedGFData.StatusGF1 = Kernel[selectedGfOffset++];
            selectedGfOffset += 1;
            GetSelectedGFData.StatusGF2 = Kernel[selectedGfOffset++];
            GetSelectedGFData.StatusGF3 = Kernel[selectedGfOffset++];
            GetSelectedGFData.StatusGF4 = Kernel[selectedGfOffset++];
            GetSelectedGFData.StatusGF5 = Kernel[selectedGfOffset++];
            GetSelectedGFData.GFHP = Kernel[selectedGfOffset++];
            selectedGfOffset += 3;
            GetSelectedGFData.GFEXP = Kernel[selectedGfOffset++];
            selectedGfOffset += 2;
            GetSelectedGFData.GFStatusAttack = Kernel[selectedGfOffset++];        
                
            //AbilityRun
            GetSelectedGFData.GFAbilityUnlock1 = Kernel[selectedGfOffset];
            selectedGfOffset += 2;
            GetSelectedGFData.GFAbility1 = Kernel[selectedGfOffset];
            selectedGfOffset += 2;
            GetSelectedGFData.GFAbilityUnlock2 = Kernel[selectedGfOffset];
            selectedGfOffset += 2;
            GetSelectedGFData.GFAbility2 = Kernel[selectedGfOffset];
            selectedGfOffset += 2;
            GetSelectedGFData.GFAbilityUnlock3 = Kernel[selectedGfOffset];
            selectedGfOffset += 2;
            GetSelectedGFData.GFAbility3 = Kernel[selectedGfOffset];
            selectedGfOffset += 2;
            GetSelectedGFData.GFAbilityUnlock4 = Kernel[selectedGfOffset];
            selectedGfOffset += 2;
            GetSelectedGFData.GFAbility4 = Kernel[selectedGfOffset];
            selectedGfOffset += 2;
            GetSelectedGFData.GFAbilityUnlock5 = Kernel[selectedGfOffset];
            selectedGfOffset += 2;
            GetSelectedGFData.GFAbility5 = Kernel[selectedGfOffset];
            selectedGfOffset += 2;
            GetSelectedGFData.GFAbilityUnlock6 = Kernel[selectedGfOffset];
            selectedGfOffset += 2;
            GetSelectedGFData.GFAbility6 = Kernel[selectedGfOffset];
            selectedGfOffset += 2;
            GetSelectedGFData.GFAbilityUnlock7 = Kernel[selectedGfOffset];
            selectedGfOffset += 2;
            GetSelectedGFData.GFAbility7 = Kernel[selectedGfOffset];
            selectedGfOffset += 2;
            GetSelectedGFData.GFAbilityUnlock8 = Kernel[selectedGfOffset];
            selectedGfOffset += 2;
            GetSelectedGFData.GFAbility8 = Kernel[selectedGfOffset];
            selectedGfOffset += 2;
            GetSelectedGFData.GFAbilityUnlock9 = Kernel[selectedGfOffset];
            selectedGfOffset += 2;
            GetSelectedGFData.GFAbility9 = Kernel[selectedGfOffset];
            selectedGfOffset += 2;
            GetSelectedGFData.GFAbilityUnlock10 = Kernel[selectedGfOffset];
            selectedGfOffset += 2;
            GetSelectedGFData.GFAbility10 = Kernel[selectedGfOffset];
            selectedGfOffset += 2;
            GetSelectedGFData.GFAbilityUnlock11 = Kernel[selectedGfOffset];
            selectedGfOffset += 2;
            GetSelectedGFData.GFAbility11 = Kernel[selectedGfOffset];
            selectedGfOffset += 2;
            GetSelectedGFData.GFAbilityUnlock12 = Kernel[selectedGfOffset];
            selectedGfOffset += 2;
            GetSelectedGFData.GFAbility12 = Kernel[selectedGfOffset];
            selectedGfOffset += 2;
            GetSelectedGFData.GFAbilityUnlock13 = Kernel[selectedGfOffset];
            selectedGfOffset += 2;
            GetSelectedGFData.GFAbility13 = Kernel[selectedGfOffset];
            selectedGfOffset += 2;
            GetSelectedGFData.GFAbilityUnlock14 = Kernel[selectedGfOffset];
            selectedGfOffset += 2;
            GetSelectedGFData.GFAbility14 = Kernel[selectedGfOffset];
            selectedGfOffset += 2;
            GetSelectedGFData.GFAbilityUnlock15 = Kernel[selectedGfOffset];
            selectedGfOffset += 2;
            GetSelectedGFData.GFAbility15 = Kernel[selectedGfOffset];
            selectedGfOffset += 2;
            GetSelectedGFData.GFAbilityUnlock16 = Kernel[selectedGfOffset];
            selectedGfOffset += 2;
            GetSelectedGFData.GFAbility16 = Kernel[selectedGfOffset];
            selectedGfOffset += 2;
            GetSelectedGFData.GFAbilityUnlock17 = Kernel[selectedGfOffset];
            selectedGfOffset += 2;
            GetSelectedGFData.GFAbility17 = Kernel[selectedGfOffset];
            selectedGfOffset += 2;
            GetSelectedGFData.GFAbilityUnlock18 = Kernel[selectedGfOffset];
            selectedGfOffset += 2;
            GetSelectedGFData.GFAbility18 = Kernel[selectedGfOffset];
            selectedGfOffset += 2;
            GetSelectedGFData.GFAbilityUnlock19 = Kernel[selectedGfOffset];
            selectedGfOffset += 2;
            GetSelectedGFData.GFAbility19 = Kernel[selectedGfOffset];
            selectedGfOffset += 2;
            GetSelectedGFData.GFAbilityUnlock20 = Kernel[selectedGfOffset];
            selectedGfOffset += 2;
            GetSelectedGFData.GFAbility20 = Kernel[selectedGfOffset];
            selectedGfOffset += 2;
            GetSelectedGFData.GFAbilityUnlock21 = Kernel[selectedGfOffset];
            selectedGfOffset += 2;
            GetSelectedGFData.GFAbility21 = Kernel[selectedGfOffset++];
            //EndofAbility

            selectedGfOffset += 1;
            GetSelectedGFData.GFQuezacoltCompatibility = Kernel[selectedGfOffset++];
            GetSelectedGFData.GFShivaCompatibility = Kernel[selectedGfOffset++];
            GetSelectedGFData.GFIfritCompatibility = Kernel[selectedGfOffset++];
            GetSelectedGFData.GFSirenCompatibility = Kernel[selectedGfOffset++];
            GetSelectedGFData.GFBrothersCompatibility = Kernel[selectedGfOffset++];
            GetSelectedGFData.GFDiablosCompatibility = Kernel[selectedGfOffset++];
            GetSelectedGFData.GFCarbuncleCompatibility = Kernel[selectedGfOffset++];
            GetSelectedGFData.GFLeviathanCompatibility = Kernel[selectedGfOffset++];
            GetSelectedGFData.GFPandemonaCompatibility = Kernel[selectedGfOffset++];
            GetSelectedGFData.GFCerberusCompatibility = Kernel[selectedGfOffset++];
            GetSelectedGFData.GFAlexanderCompatibility = Kernel[selectedGfOffset++];
            GetSelectedGFData.GFDoomtrainCompatibility = Kernel[selectedGfOffset++];
            GetSelectedGFData.GFBahamutCompatibility = Kernel[selectedGfOffset++];
            GetSelectedGFData.GFCactuarCompatibility = Kernel[selectedGfOffset++];
            GetSelectedGFData.GFTonberryCompatibility = Kernel[selectedGfOffset++];
            GetSelectedGFData.GFEdenCompatibility = Kernel[selectedGfOffset++];
            selectedGfOffset += 2;
            GetSelectedGFData.GFPowerMod = Kernel[selectedGfOffset++];
            GetSelectedGFData.GFLevelMod = Kernel[selectedGfOffset++];
        }

        #endregion

        #region NJ-GF

        public static void ReadGFAttacks(int GFAttacksID_List, byte[] Kernel)
        {
            GetSelectedGFAttacksData = new GFAttacksData();
            int selectedGfAttacksOffset = GFAttacksDataOffset + (GFAttacksID_List * 20);
            OffsetToGFAttacksSelected = selectedGfAttacksOffset;

            GetSelectedGFAttacksData.OffsetGFAttacksName = FF8Text.BuildString((ushort)(
                BitConverter.ToInt32(Kernel, (int)KernelSections.Text_NonjunctionableGFattackname) + (BitConverter.ToUInt16(Kernel, selectedGfAttacksOffset))));
            GetSelectedGFAttacksData.GFAttacksMagicID = (ushort)(BitConverter.ToUInt16(Kernel, selectedGfAttacksOffset + 2));
            selectedGfAttacksOffset += 4;
            GetSelectedGFAttacksData.GFAttacksAttackType = Kernel[selectedGfAttacksOffset++];
            GetSelectedGFAttacksData.GFAttacksPower = Kernel[selectedGfAttacksOffset++];            
            GetSelectedGFAttacksData.GFAttacksStatus = Kernel[selectedGfAttacksOffset++];
            selectedGfAttacksOffset += 1;
            GetSelectedGFAttacksData.GFAttacksFlags = Kernel[selectedGfAttacksOffset++];
            selectedGfAttacksOffset += 2;
            byte b = Kernel[selectedGfAttacksOffset++];
            GetSelectedGFAttacksData.ElementGFAttacks =
                b == (byte)Element.Fire
                    ? Element.Fire
                    : b == (byte)Element.Holy
                        ? Element.Holy
                        : b == (byte)Element.Ice
                            ? Element.Ice
                            : b == (byte)Element.NonElemental
                                ? Element.NonElemental
                                : b == (byte)Element.Poison
                                    ? Element.Poison
                                    : b == (byte)Element.Thunder
                                        ? Element.Thunder
                                        : b == (byte)Element.Water
                                            ? Element.Water
                                            : b == (byte)Element.Wind
                                                ? Element.Wind
                                                : b == (byte)Element.Earth
                                                    ? Element.Earth
                                                    : 0; //Error handler
            GetSelectedGFAttacksData.StatusGFAttacks1 = Kernel[selectedGfAttacksOffset++];
            GetSelectedGFAttacksData.StatusGFAttacks2 = Kernel[selectedGfAttacksOffset++];
            GetSelectedGFAttacksData.StatusGFAttacks3 = Kernel[selectedGfAttacksOffset++];
            GetSelectedGFAttacksData.StatusGFAttacks4 = Kernel[selectedGfAttacksOffset++];
            GetSelectedGFAttacksData.StatusGFAttacks5 = Kernel[selectedGfAttacksOffset++];
            selectedGfAttacksOffset += 1;
            GetSelectedGFAttacksData.GFAttacksPowerMod = Kernel[selectedGfAttacksOffset];
            GetSelectedGFAttacksData.GFAttacksLevelMod = Kernel[selectedGfAttacksOffset + 1];
        }

        #endregion

        #region WEAPONS

        public static void ReadWeapons(int WeaponsID_List, byte[] Kernel)
        {
            GetSelectedWeaponsData = new WeaponsData();
            int selectedWeaponsOffset = WeaponsDataOffset + (WeaponsID_List * 12);
            OffsetToWeaponsSelected = selectedWeaponsOffset;

            GetSelectedWeaponsData.OffsetToName = FF8Text.BuildString((ushort)(
                BitConverter.ToInt32(Kernel, (int)KernelSections.Text_Weapontext) + (BitConverter.ToUInt16(Kernel, selectedWeaponsOffset))));
            GetSelectedWeaponsData.RenzokukenFinishers = Kernel[selectedWeaponsOffset + 2];
            selectedWeaponsOffset += 4;
            GetSelectedWeaponsData.CharacterID = Kernel[selectedWeaponsOffset++];
            GetSelectedWeaponsData.AttackType = Kernel[selectedWeaponsOffset++];
            GetSelectedWeaponsData.AttackPower = Kernel[selectedWeaponsOffset++];
            GetSelectedWeaponsData.AttackParam = Kernel[selectedWeaponsOffset++];
            GetSelectedWeaponsData.STRBonus = Kernel[selectedWeaponsOffset++];
            GetSelectedWeaponsData.Tier = Kernel[selectedWeaponsOffset++];
            GetSelectedWeaponsData.CritBonus = Kernel[selectedWeaponsOffset++];
            GetSelectedWeaponsData.Melee = Kernel[selectedWeaponsOffset++];
        }

        #endregion

        #region CHARACTERS

        public static void ReadCharacters(int CharactersID_List, byte[] Kernel)
        {
            GetSelectedCharactersData = new CharactersData();
            int selectedCharactersOffset = CharactersDataOffset + (CharactersID_List * 36);
            OffsetToCharactersSelected = selectedCharactersOffset;

            GetSelectedCharactersData.OffsetToName = FF8Text.BuildString((ushort)(
                BitConverter.ToInt32(Kernel, (int)KernelSections.Text_Characternames) + (BitConverter.ToUInt16(Kernel, selectedCharactersOffset))));
            selectedCharactersOffset += 2;
            GetSelectedCharactersData.CrisisLevel = Kernel[selectedCharactersOffset++];
            GetSelectedCharactersData.Gender = Kernel[selectedCharactersOffset++];
            GetSelectedCharactersData.LimitID = Kernel[selectedCharactersOffset++];
            GetSelectedCharactersData.LimitParam = Kernel[selectedCharactersOffset++];
            GetSelectedCharactersData.EXP1 = Kernel[selectedCharactersOffset++];
            GetSelectedCharactersData.EXP2 = Kernel[selectedCharactersOffset++];
            GetSelectedCharactersData.HP1 = Kernel[selectedCharactersOffset++];
            GetSelectedCharactersData.HP2 = Kernel[selectedCharactersOffset++];
            GetSelectedCharactersData.HP3 = Kernel[selectedCharactersOffset++];
            GetSelectedCharactersData.HP4 = Kernel[selectedCharactersOffset++];
            GetSelectedCharactersData.STR1 = Kernel[selectedCharactersOffset++];
            GetSelectedCharactersData.STR2 = Kernel[selectedCharactersOffset++];
            GetSelectedCharactersData.STR3 = Kernel[selectedCharactersOffset++];
            GetSelectedCharactersData.STR4 = Kernel[selectedCharactersOffset++];
            GetSelectedCharactersData.VIT1 = Kernel[selectedCharactersOffset++];
            GetSelectedCharactersData.VIT2 = Kernel[selectedCharactersOffset++];
            GetSelectedCharactersData.VIT3 = Kernel[selectedCharactersOffset++];
            GetSelectedCharactersData.VIT4 = Kernel[selectedCharactersOffset++];
            GetSelectedCharactersData.MAG1 = Kernel[selectedCharactersOffset++];
            GetSelectedCharactersData.MAG2 = Kernel[selectedCharactersOffset++];
            GetSelectedCharactersData.MAG3 = Kernel[selectedCharactersOffset++];
            GetSelectedCharactersData.MAG4 = Kernel[selectedCharactersOffset++];
            GetSelectedCharactersData.SPR1 = Kernel[selectedCharactersOffset++];
            GetSelectedCharactersData.SPR2 = Kernel[selectedCharactersOffset++];
            GetSelectedCharactersData.SPR3 = Kernel[selectedCharactersOffset++];
            GetSelectedCharactersData.SPR4 = Kernel[selectedCharactersOffset++];
            GetSelectedCharactersData.SPD1 = Kernel[selectedCharactersOffset++];
            GetSelectedCharactersData.SPD2 = Kernel[selectedCharactersOffset++];
            GetSelectedCharactersData.SPD3 = Kernel[selectedCharactersOffset++];
            GetSelectedCharactersData.SPD4 = Kernel[selectedCharactersOffset++];
            GetSelectedCharactersData.LUCK1 = Kernel[selectedCharactersOffset++];
            GetSelectedCharactersData.LUCK2 = Kernel[selectedCharactersOffset++];
            GetSelectedCharactersData.LUCK3 = Kernel[selectedCharactersOffset++];
            GetSelectedCharactersData.LUCK4 = Kernel[selectedCharactersOffset++];
        }

        #endregion

        #region ENEMY ATTACKS

        public static void ReadEnemyAttacks(int EnemyAttacksID_List, byte[] Kernel)
        {
            GetSelectedEnemyAttacksData = new EnemyAttacksData();
            EnemyAttacksID_List++; //skip dummy entry
            int selectedEnemyAttacksOffset = EnemyAttacksDataOffset + (EnemyAttacksID_List * 20);
            OffsetToEnemyAttacksSelected = selectedEnemyAttacksOffset;

            GetSelectedEnemyAttacksData.OffsetToName = FF8Text.BuildString((ushort)(
                BitConverter.ToInt32(Kernel, (int)KernelSections.Text_Enemyattacktext) + (BitConverter.ToUInt16(Kernel, selectedEnemyAttacksOffset))));
            GetSelectedEnemyAttacksData.MagicID = (ushort)(BitConverter.ToUInt16(Kernel, selectedEnemyAttacksOffset + 2));
            selectedEnemyAttacksOffset += 4;
            GetSelectedEnemyAttacksData.CameraChange = Kernel[selectedEnemyAttacksOffset++];
            selectedEnemyAttacksOffset += 1;
            GetSelectedEnemyAttacksData.AttackType = Kernel[selectedEnemyAttacksOffset++];
            GetSelectedEnemyAttacksData.AttackPower = Kernel[selectedEnemyAttacksOffset++];
            GetSelectedEnemyAttacksData.AttackFlags = Kernel[selectedEnemyAttacksOffset++];
            selectedEnemyAttacksOffset += 1;
            byte b = Kernel[selectedEnemyAttacksOffset++];
            GetSelectedEnemyAttacksData.Element =
                b == (byte)Element.Fire
                    ? Element.Fire
                    : b == (byte)Element.Holy
                        ? Element.Holy
                        : b == (byte)Element.Ice
                            ? Element.Ice
                            : b == (byte)Element.NonElemental
                                ? Element.NonElemental
                                : b == (byte)Element.Poison
                                    ? Element.Poison
                                    : b == (byte)Element.Thunder
                                        ? Element.Thunder
                                        : b == (byte)Element.Water
                                            ? Element.Water
                                            : b == (byte)Element.Wind
                                                ? Element.Wind
                                                : b == (byte)Element.Earth
                                                    ? Element.Earth
                                                    : 0; //Error handler
            selectedEnemyAttacksOffset += 1;
            GetSelectedEnemyAttacksData.StatusAttack = Kernel[selectedEnemyAttacksOffset++];
            GetSelectedEnemyAttacksData.AttackParam = Kernel[selectedEnemyAttacksOffset++];
            GetSelectedEnemyAttacksData.Status1 = Kernel[selectedEnemyAttacksOffset++];
            selectedEnemyAttacksOffset += 1;
            GetSelectedEnemyAttacksData.Status2 = Kernel[selectedEnemyAttacksOffset++];
            GetSelectedEnemyAttacksData.Status3 = Kernel[selectedEnemyAttacksOffset++];
            GetSelectedEnemyAttacksData.Status4 = Kernel[selectedEnemyAttacksOffset++];
            GetSelectedEnemyAttacksData.Status5 = Kernel[selectedEnemyAttacksOffset++];
        }

        #endregion

        #region BLUE MAGIC

        public static void ReadBlueMagic(int BlueMagicID_List, byte[] Kernel)
        {
            GetSelectedBlueMagicData = new BlueMagicData();
            int selectedBlueMagicOffset = BlueMagicDataOffset + (BlueMagicID_List * 16);
            OffsetToBlueMagicSelected = selectedBlueMagicOffset;

            GetSelectedBlueMagicData.OffsetToName = FF8Text.BuildString((ushort)(
                BitConverter.ToInt32(Kernel, (int)KernelSections.Text_Bluemagictext) + (BitConverter.ToUInt16(Kernel, selectedBlueMagicOffset))));
            GetSelectedBlueMagicData.OffsetToDescription = FF8Text.BuildString((ushort)(
                BitConverter.ToInt32(Kernel, (int)KernelSections.Text_Bluemagictext) + (BitConverter.ToUInt16(Kernel, selectedBlueMagicOffset + 2))));
            GetSelectedBlueMagicData.MagicID = (ushort)(BitConverter.ToUInt16(Kernel, selectedBlueMagicOffset + 4));
            selectedBlueMagicOffset += 7;
            GetSelectedBlueMagicData.AttackType = Kernel[selectedBlueMagicOffset++];
            selectedBlueMagicOffset += 2;
            GetSelectedBlueMagicData.AttackFlags = Kernel[selectedBlueMagicOffset++];
            selectedBlueMagicOffset += 1;
            byte b = Kernel[selectedBlueMagicOffset++];
            GetSelectedBlueMagicData.Element =
                b == (byte)Element.Fire
                    ? Element.Fire
                    : b == (byte)Element.Holy
                        ? Element.Holy
                        : b == (byte)Element.Ice
                            ? Element.Ice
                            : b == (byte)Element.NonElemental
                                ? Element.NonElemental
                                : b == (byte)Element.Poison
                                    ? Element.Poison
                                    : b == (byte)Element.Thunder
                                        ? Element.Thunder
                                        : b == (byte)Element.Water
                                            ? Element.Water
                                            : b == (byte)Element.Wind
                                                ? Element.Wind
                                                : b == (byte)Element.Earth
                                                    ? Element.Earth
                                                    : 0; //Error handler
            GetSelectedBlueMagicData.StatusAttack = Kernel[selectedBlueMagicOffset++];
            GetSelectedBlueMagicData.CritBonus = Kernel[selectedBlueMagicOffset++];
        }

        public static void ReadBlueMagicParam(int BlueMagicParamID_List, byte[] Kernel)
        {
            GetSelectedBlueMagicParamData = new BlueMagicParamData();
            int selectedBlueMagicParamOffset = BlueMagicParamDataOffset + (BlueMagicParamID_List * 32); //pretending the section is 32 bytes instead of 8
            OffsetToBlueMagicParamSelected = selectedBlueMagicParamOffset;

            GetSelectedBlueMagicParamData.Status1CL1 = Kernel[selectedBlueMagicParamOffset++];
            GetSelectedBlueMagicParamData.Status2CL1 = Kernel[selectedBlueMagicParamOffset++];
            GetSelectedBlueMagicParamData.Status3CL1 = Kernel[selectedBlueMagicParamOffset++];
            GetSelectedBlueMagicParamData.Status4CL1 = Kernel[selectedBlueMagicParamOffset++];
            GetSelectedBlueMagicParamData.Status5CL1 = Kernel[selectedBlueMagicParamOffset++];
            selectedBlueMagicParamOffset += 1;
            GetSelectedBlueMagicParamData.AttackPowerCL1 = Kernel[selectedBlueMagicParamOffset++];
            GetSelectedBlueMagicParamData.AttackParamCL1 = Kernel[selectedBlueMagicParamOffset++];

            GetSelectedBlueMagicParamData.Status1CL2 = Kernel[selectedBlueMagicParamOffset++];
            GetSelectedBlueMagicParamData.Status2CL2 = Kernel[selectedBlueMagicParamOffset++];
            GetSelectedBlueMagicParamData.Status3CL2 = Kernel[selectedBlueMagicParamOffset++];
            GetSelectedBlueMagicParamData.Status4CL2 = Kernel[selectedBlueMagicParamOffset++];
            GetSelectedBlueMagicParamData.Status5CL2 = Kernel[selectedBlueMagicParamOffset++];
            selectedBlueMagicParamOffset += 1;
            GetSelectedBlueMagicParamData.AttackPowerCL2 = Kernel[selectedBlueMagicParamOffset++];
            GetSelectedBlueMagicParamData.AttackParamCL2 = Kernel[selectedBlueMagicParamOffset++];

            GetSelectedBlueMagicParamData.Status1CL3 = Kernel[selectedBlueMagicParamOffset++];
            GetSelectedBlueMagicParamData.Status2CL3 = Kernel[selectedBlueMagicParamOffset++];
            GetSelectedBlueMagicParamData.Status3CL3 = Kernel[selectedBlueMagicParamOffset++];
            GetSelectedBlueMagicParamData.Status4CL3 = Kernel[selectedBlueMagicParamOffset++];
            GetSelectedBlueMagicParamData.Status5CL3 = Kernel[selectedBlueMagicParamOffset++];
            selectedBlueMagicParamOffset += 1;
            GetSelectedBlueMagicParamData.AttackPowerCL3 = Kernel[selectedBlueMagicParamOffset++];
            GetSelectedBlueMagicParamData.AttackParamCL3 = Kernel[selectedBlueMagicParamOffset++];

            GetSelectedBlueMagicParamData.Status1CL4 = Kernel[selectedBlueMagicParamOffset++];
            GetSelectedBlueMagicParamData.Status2CL4 = Kernel[selectedBlueMagicParamOffset++];
            GetSelectedBlueMagicParamData.Status3CL4 = Kernel[selectedBlueMagicParamOffset++];
            GetSelectedBlueMagicParamData.Status4CL4 = Kernel[selectedBlueMagicParamOffset++];
            GetSelectedBlueMagicParamData.Status5CL4 = Kernel[selectedBlueMagicParamOffset++];
            selectedBlueMagicParamOffset += 1;
            GetSelectedBlueMagicParamData.AttackPowerCL4 = Kernel[selectedBlueMagicParamOffset++];
            GetSelectedBlueMagicParamData.AttackParamCL4 = Kernel[selectedBlueMagicParamOffset++];
        }

        #endregion

        #region STAT PERCENTAGE ABILITIES

        public static void ReadStatPercentageAbilities(int StatPercentageAbilitiesID_List, byte[] Kernel)
        {
            GetSelectedStatPercentageAbilitiesData = new StatPercentageAbilitiesData();
            int selectedStatPercentageAbilitiesOffset = StatPercentageAbilitiesDataOffset + (StatPercentageAbilitiesID_List * 8);
            OffsetToStatPercentageAbilitiesSelected = selectedStatPercentageAbilitiesOffset;

            GetSelectedStatPercentageAbilitiesData.OffsetToName = FF8Text.BuildString((ushort)(
                BitConverter.ToInt32(Kernel, (int)KernelSections.Text_Statpercentageincreasingabilitiestext) + 
                (BitConverter.ToUInt16(Kernel, selectedStatPercentageAbilitiesOffset))));
            GetSelectedStatPercentageAbilitiesData.OffsetToDescription = FF8Text.BuildString((ushort)(
                BitConverter.ToInt32(Kernel, (int)KernelSections.Text_Statpercentageincreasingabilitiestext) + 
                (BitConverter.ToUInt16(Kernel, selectedStatPercentageAbilitiesOffset + 2))));
            selectedStatPercentageAbilitiesOffset += 4;
            GetSelectedStatPercentageAbilitiesData.AP = Kernel[selectedStatPercentageAbilitiesOffset++];
            GetSelectedStatPercentageAbilitiesData.StatToincrease = Kernel[selectedStatPercentageAbilitiesOffset++];
            GetSelectedStatPercentageAbilitiesData.IncreasementValue = Kernel[selectedStatPercentageAbilitiesOffset++];
        }

        #endregion

        #region RENZOKUKEN FINISHERS

        public static void ReadRenzoFin(int RenzoFinID_List, byte[] Kernel)
        {
            GetSelectedRenzoFinData = new RenzoFinData();
            int selectedRenzoFinOffset = RenzoFinDataOffset + (RenzoFinID_List * 24);
            OffsetToRenzoFinSelected = selectedRenzoFinOffset;

            GetSelectedRenzoFinData.OffsetToName = FF8Text.BuildString((ushort)(
                BitConverter.ToInt32(Kernel, (int)KernelSections.Text_Renzokukenfinisherstext) + (BitConverter.ToUInt16(Kernel, selectedRenzoFinOffset))));
            GetSelectedRenzoFinData.OffsetToDescription = FF8Text.BuildString((ushort)(
                BitConverter.ToInt32(Kernel, (int)KernelSections.Text_Renzokukenfinisherstext) + (BitConverter.ToUInt16(Kernel, selectedRenzoFinOffset + 2))));
            GetSelectedRenzoFinData.MagicID = (ushort)(BitConverter.ToUInt16(Kernel, selectedRenzoFinOffset + 4));
            selectedRenzoFinOffset += 6;
            GetSelectedRenzoFinData.AttackType = Kernel[selectedRenzoFinOffset++];
            selectedRenzoFinOffset += 1;
            GetSelectedRenzoFinData.AttackPower = Kernel[selectedRenzoFinOffset++];
            selectedRenzoFinOffset += 1;
            GetSelectedRenzoFinData.Target = Kernel[selectedRenzoFinOffset++];
            GetSelectedRenzoFinData.AttackFlags = Kernel[selectedRenzoFinOffset++];
            GetSelectedRenzoFinData.HitCount = Kernel[selectedRenzoFinOffset++];
            byte b = Kernel[selectedRenzoFinOffset++];
            GetSelectedRenzoFinData.Element =
                b == (byte)Element.Fire
                    ? Element.Fire
                    : b == (byte)Element.Holy
                        ? Element.Holy
                        : b == (byte)Element.Ice
                            ? Element.Ice
                            : b == (byte)Element.NonElemental
                                ? Element.NonElemental
                                : b == (byte)Element.Poison
                                    ? Element.Poison
                                    : b == (byte)Element.Thunder
                                        ? Element.Thunder
                                        : b == (byte)Element.Water
                                            ? Element.Water
                                            : b == (byte)Element.Wind
                                                ? Element.Wind
                                                : b == (byte)Element.Earth
                                                    ? Element.Earth
                                                    : 0; //Error handler
            GetSelectedRenzoFinData.ElementPerc = Kernel[selectedRenzoFinOffset++];
            GetSelectedRenzoFinData.StatusAttack = Kernel[selectedRenzoFinOffset++];
            selectedRenzoFinOffset += 2;
            GetSelectedRenzoFinData.Status1 = Kernel[selectedRenzoFinOffset++];
            selectedRenzoFinOffset += 1;
            GetSelectedRenzoFinData.Status2 = Kernel[selectedRenzoFinOffset++];
            GetSelectedRenzoFinData.Status3 = Kernel[selectedRenzoFinOffset++];
            GetSelectedRenzoFinData.Status4 = Kernel[selectedRenzoFinOffset++];
            GetSelectedRenzoFinData.Status5 = Kernel[selectedRenzoFinOffset++];
        }

        #endregion

        #region TEMP CHARACTERS LIMIT BREAKS

        public static void ReadTempCharLB(int TempCharLBID_List, byte[] Kernel)
        {
            GetSelectedTempCharLBData = new TempCharLBData();
            int selectedTempCharLBOffset = TempCharLBDataOffset + (TempCharLBID_List * 24);
            OffsetToTempCharLBSelected = selectedTempCharLBOffset;

            GetSelectedTempCharLBData.OffsetToName = FF8Text.BuildString((ushort)(
                BitConverter.ToInt32(Kernel, (int)KernelSections.Text_Temporarycharacterlimitbreaktext) + (BitConverter.ToUInt16(Kernel, selectedTempCharLBOffset))));
            GetSelectedTempCharLBData.OffsetToDescription = FF8Text.BuildString((ushort)(
                BitConverter.ToInt32(Kernel, (int)KernelSections.Text_Temporarycharacterlimitbreaktext) + (BitConverter.ToUInt16(Kernel, selectedTempCharLBOffset + 2))));
            GetSelectedTempCharLBData.MagicID = (ushort)(BitConverter.ToUInt16(Kernel, selectedTempCharLBOffset + 4));
            selectedTempCharLBOffset += 6;
            GetSelectedTempCharLBData.AttackType = Kernel[selectedTempCharLBOffset++];
            GetSelectedTempCharLBData.AttackPower = Kernel[selectedTempCharLBOffset++];
            selectedTempCharLBOffset += 2;
            GetSelectedTempCharLBData.Target = Kernel[selectedTempCharLBOffset++];
            GetSelectedTempCharLBData.AttackFlags = Kernel[selectedTempCharLBOffset++];
            GetSelectedTempCharLBData.HitCount = Kernel[selectedTempCharLBOffset++];
            byte b = Kernel[selectedTempCharLBOffset++];
            GetSelectedTempCharLBData.Element =
                b == (byte)Element.Fire
                    ? Element.Fire
                    : b == (byte)Element.Holy
                        ? Element.Holy
                        : b == (byte)Element.Ice
                            ? Element.Ice
                            : b == (byte)Element.NonElemental
                                ? Element.NonElemental
                                : b == (byte)Element.Poison
                                    ? Element.Poison
                                    : b == (byte)Element.Thunder
                                        ? Element.Thunder
                                        : b == (byte)Element.Water
                                            ? Element.Water
                                            : b == (byte)Element.Wind
                                                ? Element.Wind
                                                : b == (byte)Element.Earth
                                                    ? Element.Earth
                                                    : 0; //Error handler
            GetSelectedTempCharLBData.ElementPerc = Kernel[selectedTempCharLBOffset++];
            GetSelectedTempCharLBData.StatusAttack = Kernel[selectedTempCharLBOffset++];
            GetSelectedTempCharLBData.Status1 = Kernel[selectedTempCharLBOffset++];
            selectedTempCharLBOffset += 3;
            GetSelectedTempCharLBData.Status2 = Kernel[selectedTempCharLBOffset++];
            GetSelectedTempCharLBData.Status3 = Kernel[selectedTempCharLBOffset++];
            GetSelectedTempCharLBData.Status4 = Kernel[selectedTempCharLBOffset++];
            GetSelectedTempCharLBData.Status5 = Kernel[selectedTempCharLBOffset++];
        }

        #endregion

        #region SHOT

        public static void ReadShot(int ShotID_List, byte[] Kernel)
        {
            GetSelectedShotData = new ShotData();
            int selectedShotOffset = ShotDataOffset + (ShotID_List * 24);
            OffsetToShotSelected = selectedShotOffset;

            GetSelectedShotData.OffsetToName = FF8Text.BuildString((ushort)(
                BitConverter.ToInt32(Kernel, (int)KernelSections.Text_Shottext) + (BitConverter.ToUInt16(Kernel, selectedShotOffset))));
            GetSelectedShotData.OffsetToDescription = FF8Text.BuildString((ushort)(
                BitConverter.ToInt32(Kernel, (int)KernelSections.Text_Shottext) + (BitConverter.ToUInt16(Kernel, selectedShotOffset + 2))));
            GetSelectedShotData.MagicID = (ushort)(BitConverter.ToUInt16(Kernel, selectedShotOffset + 4));
            selectedShotOffset += 4 + 2;
            GetSelectedShotData.AttackType = Kernel[selectedShotOffset++];
            GetSelectedShotData.AttackPower = Kernel[selectedShotOffset++];
            selectedShotOffset += 2;
            GetSelectedShotData.Target = Kernel[selectedShotOffset++];
            GetSelectedShotData.AttackFlags = Kernel[selectedShotOffset++];
            GetSelectedShotData.HitCount = Kernel[selectedShotOffset++];
            byte b = Kernel[selectedShotOffset++];
            GetSelectedShotData.Element =
                b == (byte)Element.Fire
                    ? Element.Fire
                    : b == (byte)Element.Holy
                        ? Element.Holy
                        : b == (byte)Element.Ice
                            ? Element.Ice
                            : b == (byte)Element.NonElemental
                                ? Element.NonElemental
                                : b == (byte)Element.Poison
                                    ? Element.Poison
                                    : b == (byte)Element.Thunder
                                        ? Element.Thunder
                                        : b == (byte)Element.Water
                                            ? Element.Water
                                            : b == (byte)Element.Wind
                                                ? Element.Wind
                                                : b == (byte)Element.Earth
                                                    ? Element.Earth
                                                    : 0; //Error handler
            GetSelectedShotData.ElementPerc = Kernel[selectedShotOffset++];
            GetSelectedShotData.StatusAttack = Kernel[selectedShotOffset++];
            GetSelectedShotData.Status1 = Kernel[selectedShotOffset++];
            selectedShotOffset += 1;
            GetSelectedShotData.UsedItem = Kernel[selectedShotOffset++];
            GetSelectedShotData.CritBonus = Kernel[selectedShotOffset++];
            GetSelectedShotData.Status2 = Kernel[selectedShotOffset++];
            GetSelectedShotData.Status3 = Kernel[selectedShotOffset++];
            GetSelectedShotData.Status4 = Kernel[selectedShotOffset++];
            GetSelectedShotData.Status5 = Kernel[selectedShotOffset++];
        }

        #endregion

        #region DUEL

        public static void ReadDuel(int DuelID_List, byte[] Kernel)
        {
            GetSelectedDuelData = new DuelData();
            int selectedDuelOffset = DuelDataOffset + (DuelID_List * 32);
            OffsetToDuelSelected = selectedDuelOffset;

            GetSelectedDuelData.OffsetToName = FF8Text.BuildString((ushort)(
                BitConverter.ToInt32(Kernel, (int)KernelSections.Text_Dueltext) + (BitConverter.ToUInt16(Kernel, selectedDuelOffset))));
            GetSelectedDuelData.OffsetToDescription = FF8Text.BuildString((ushort)(
                BitConverter.ToInt32(Kernel, (int)KernelSections.Text_Dueltext) + (BitConverter.ToUInt16(Kernel, selectedDuelOffset + 2))));
            GetSelectedDuelData.MagicID = (ushort)(BitConverter.ToUInt16(Kernel, selectedDuelOffset + 4));
            selectedDuelOffset += 4 + 2;
            GetSelectedDuelData.AttackType = Kernel[selectedDuelOffset++];
            GetSelectedDuelData.AttackPower = Kernel[selectedDuelOffset++];
            GetSelectedDuelData.AttackFlags = Kernel[selectedDuelOffset++];
            selectedDuelOffset += 1;
            GetSelectedDuelData.Target = Kernel[selectedDuelOffset++];
            selectedDuelOffset += 1;
            GetSelectedDuelData.HitCount = Kernel[selectedDuelOffset++];

            byte b = Kernel[selectedDuelOffset++];
            GetSelectedDuelData.Element =
                b == (byte)Element.Fire
                    ? Element.Fire
                    : b == (byte)Element.Holy
                        ? Element.Holy
                        : b == (byte)Element.Ice
                            ? Element.Ice
                            : b == (byte)Element.NonElemental
                                ? Element.NonElemental
                                : b == (byte)Element.Poison
                                    ? Element.Poison
                                    : b == (byte)Element.Thunder
                                        ? Element.Thunder
                                        : b == (byte)Element.Water
                                            ? Element.Water
                                            : b == (byte)Element.Wind
                                                ? Element.Wind
                                                : b == (byte)Element.Earth
                                                    ? Element.Earth
                                                    : 0; //Error handler
            GetSelectedDuelData.ElementPerc = Kernel[selectedDuelOffset++];
            GetSelectedDuelData.StatusAttack = Kernel[selectedDuelOffset++];
            GetSelectedDuelData.Button1 = (Buttons)(BitConverter.ToUInt16(Kernel, selectedDuelOffset) & 0xFEFF);
            GetSelectedDuelData.IsFinisher = (ushort)(BitConverter.ToUInt16(Kernel, selectedDuelOffset));
            selectedDuelOffset += 2;
            GetSelectedDuelData.Button2 = (Buttons)(BitConverter.ToUInt16(Kernel, selectedDuelOffset));
            selectedDuelOffset += 2;
            GetSelectedDuelData.Button3 = (Buttons)(BitConverter.ToUInt16(Kernel, selectedDuelOffset));
            selectedDuelOffset += 2;
            GetSelectedDuelData.Button4 = (Buttons)(BitConverter.ToUInt16(Kernel, selectedDuelOffset));
            selectedDuelOffset += 2;
            GetSelectedDuelData.Button5 = (Buttons)(BitConverter.ToUInt16(Kernel, selectedDuelOffset));
            selectedDuelOffset += 2;
            GetSelectedDuelData.Status1 = Kernel[selectedDuelOffset++];
            selectedDuelOffset += 1;
            GetSelectedDuelData.Status2 = Kernel[selectedDuelOffset++];
            GetSelectedDuelData.Status3 = Kernel[selectedDuelOffset++];
            GetSelectedDuelData.Status4 = Kernel[selectedDuelOffset++];
            GetSelectedDuelData.Status5 = Kernel[selectedDuelOffset++];
        }

        public static void ReadDuelParams(byte[] Kernel)
        {
            GetSelectedDuelParamsData = new DuelParamsData();
            int selectedDuelParamsOffset = DuelParamsDataOffset;
            OffsetToDuelParamsSelected = selectedDuelParamsOffset;

            GetSelectedDuelParamsData.StartMove0 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq0_1 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq0_2 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq0_3 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.StartMove1 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq1_1 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq1_2 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq1_3 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.StartMove2 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq2_1 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq2_2 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq2_3 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.StartMove3 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq3_1 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq3_2 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq3_3 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.StartMove4 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq4_1 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq4_2 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq4_3 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.StartMove5 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq5_1 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq5_2 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq5_3 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.StartMove6 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq6_1 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq6_2 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq6_3 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.StartMove7 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq7_1 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq7_2 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq7_3 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.StartMove8 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq8_1 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq8_2 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq8_3 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.StartMove9 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq9_1 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq9_2 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq9_3 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.StartMove10 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq10_1 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq10_2 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq10_3 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.StartMove11 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq11_1 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq11_2 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq11_3 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.StartMove12 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq12_1 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq12_2 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq12_3 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.StartMove13 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq13_1 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq13_2 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq13_3 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.StartMove14 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq14_1 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq14_2 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq14_3 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.StartMove15 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq15_1 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq15_2 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq15_3 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.StartMove16 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq16_1 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq16_2 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq16_3 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.StartMove17 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq17_1 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq17_2 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq17_3 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.StartMove18 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq18_1 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq18_2 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq18_3 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.StartMove19 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq19_1 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq19_2 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq19_3 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.StartMove20 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq20_1 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq20_2 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq20_3 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.StartMove21 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq21_1 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq21_2 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq21_3 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.StartMove22 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq22_1 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq22_2 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq22_3 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.StartMove23 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq23_1 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq23_2 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq23_3 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.StartMove24 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq24_1 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq24_2 = Kernel[selectedDuelParamsOffset++];
            GetSelectedDuelParamsData.NextSeq24_3 = Kernel[selectedDuelParamsOffset++];
        }

        #endregion

        #region COMBINE

        public static void ReadCombine(int CombineID_List, byte[] Kernel)
        {
            GetSelectedCombineData = new CombineData();
            int selectedCombineOffset = CombineDataOffset + (CombineID_List * 20);
            OffsetToCombineSelected = selectedCombineOffset;

            GetSelectedCombineData.OffsetToName = FF8Text.BuildString((ushort)(
                BitConverter.ToInt32(Kernel, (int)KernelSections.Text_Rinoalimitbreaktext2) + (BitConverter.ToUInt16(Kernel, selectedCombineOffset))));
            GetSelectedCombineData.MagicID = (ushort)(BitConverter.ToUInt16(Kernel, selectedCombineOffset + 2));
            selectedCombineOffset += 4;
            GetSelectedCombineData.AttackType = Kernel[selectedCombineOffset++];
            GetSelectedCombineData.AttackPower = Kernel[selectedCombineOffset++];
            GetSelectedCombineData.AttackFlags = Kernel[selectedCombineOffset++];
            selectedCombineOffset += 1;
            GetSelectedCombineData.Target = Kernel[selectedCombineOffset++];
            selectedCombineOffset += 1;
            GetSelectedCombineData.HitCount = Kernel[selectedCombineOffset++];
            byte b = Kernel[selectedCombineOffset++];
            GetSelectedCombineData.Element =
                b == (byte)Element.Fire
                    ? Element.Fire
                    : b == (byte)Element.Holy
                        ? Element.Holy
                        : b == (byte)Element.Ice
                            ? Element.Ice
                            : b == (byte)Element.NonElemental
                                ? Element.NonElemental
                                : b == (byte)Element.Poison
                                    ? Element.Poison
                                    : b == (byte)Element.Thunder
                                        ? Element.Thunder
                                        : b == (byte)Element.Water
                                            ? Element.Water
                                            : b == (byte)Element.Wind
                                                ? Element.Wind
                                                : b == (byte)Element.Earth
                                                    ? Element.Earth
                                                    : 0; //Error handler
            GetSelectedCombineData.ElementPerc = Kernel[selectedCombineOffset++];
            GetSelectedCombineData.StatusAttack = Kernel[selectedCombineOffset++];
            GetSelectedCombineData.Status1 = Kernel[selectedCombineOffset++];
            selectedCombineOffset += 1;
            GetSelectedCombineData.Status2 = Kernel[selectedCombineOffset++];
            GetSelectedCombineData.Status3 = Kernel[selectedCombineOffset++];
            GetSelectedCombineData.Status4 = Kernel[selectedCombineOffset++];
            GetSelectedCombineData.Status5 = Kernel[selectedCombineOffset++];
        }

        #endregion

        #region BATTLE ITEMS

        public static void ReadBattleItems(int BattleItemsID_List, byte[] Kernel)
        {
            GetSelectedBattleItemsData = new BattleItemsData();
            BattleItemsID_List++; //skip dummy entry
            int selectedBattleItemsOffset = BattleItemsDataOffset + (BattleItemsID_List * 24);
            OffsetToBattleItemsSelected = selectedBattleItemsOffset;

            GetSelectedBattleItemsData.OffsetToName = FF8Text.BuildString((ushort)(
                BitConverter.ToInt32(Kernel, (int)KernelSections.Text_Battleitemnames) + (BitConverter.ToUInt16(Kernel, selectedBattleItemsOffset))));
            GetSelectedBattleItemsData.OffsetToDescription = FF8Text.BuildString((ushort)(
                BitConverter.ToInt32(Kernel, (int)KernelSections.Text_Battleitemnames) + (BitConverter.ToUInt16(Kernel, selectedBattleItemsOffset + 2))));
            GetSelectedBattleItemsData.MagicID = (ushort)(BitConverter.ToUInt16(Kernel, selectedBattleItemsOffset + 4));
            selectedBattleItemsOffset += 6;
            GetSelectedBattleItemsData.AttackType = Kernel[selectedBattleItemsOffset++];
            GetSelectedBattleItemsData.AttackPower = Kernel[selectedBattleItemsOffset++];
            selectedBattleItemsOffset += 1;
            GetSelectedBattleItemsData.Target = Kernel[selectedBattleItemsOffset++];
            selectedBattleItemsOffset += 1;
            GetSelectedBattleItemsData.AttackFlags = Kernel[selectedBattleItemsOffset++];
            selectedBattleItemsOffset += 1;
            GetSelectedBattleItemsData.StatusAttack = Kernel[selectedBattleItemsOffset++];
            GetSelectedBattleItemsData.Status1 = Kernel[selectedBattleItemsOffset++];
            selectedBattleItemsOffset += 1;
            GetSelectedBattleItemsData.Status2 = Kernel[selectedBattleItemsOffset++];
            GetSelectedBattleItemsData.Status3 = Kernel[selectedBattleItemsOffset++];
            GetSelectedBattleItemsData.Status4 = Kernel[selectedBattleItemsOffset++];
            GetSelectedBattleItemsData.Status5 = Kernel[selectedBattleItemsOffset++];
            GetSelectedBattleItemsData.AttackParam = Kernel[selectedBattleItemsOffset++];
            selectedBattleItemsOffset += 1;
            GetSelectedBattleItemsData.HitCount = Kernel[selectedBattleItemsOffset++];
            byte b = Kernel[selectedBattleItemsOffset++];
            GetSelectedBattleItemsData.Element =
                b == (byte)Element.Fire
                    ? Element.Fire
                    : b == (byte)Element.Holy
                        ? Element.Holy
                        : b == (byte)Element.Ice
                            ? Element.Ice
                            : b == (byte)Element.NonElemental
                                ? Element.NonElemental
                                : b == (byte)Element.Poison
                                    ? Element.Poison
                                    : b == (byte)Element.Thunder
                                        ? Element.Thunder
                                        : b == (byte)Element.Water
                                            ? Element.Water
                                            : b == (byte)Element.Wind
                                                ? Element.Wind
                                                : b == (byte)Element.Earth
                                                    ? Element.Earth
                                                    : 0; //Error handler
        }

        #endregion

        #region ITEMS

        public static void ReadItems(int ItemsID_List, byte[] Kernel)
        {
            GetSelectedItemsData = new ItemsData();
            int selectedItemsOffset = ItemsDataOffset + (ItemsID_List * 4);
            OffsetToItemsSelected = selectedItemsOffset;

            GetSelectedItemsData.OffsetToName = FF8Text.BuildString((ushort)(
                BitConverter.ToInt32(Kernel, (int)KernelSections.Text_Nonbattleitemnames) + (BitConverter.ToUInt16(Kernel, selectedItemsOffset))));
            GetSelectedItemsData.OffsetToDescription = FF8Text.BuildString((ushort)(
                BitConverter.ToInt32(Kernel, (int)KernelSections.Text_Nonbattleitemnames) + (BitConverter.ToUInt16(Kernel, selectedItemsOffset + 2))));
        }

        #endregion

        #region SLOT

        public static void ReadSlotArray(byte[] Kernel)
        {
            GetSelectedSlotArrayData = new SlotArrayData();
            int selectedSlotArrayOffset = SlotArrayDataOffset;
            OffsetToSlotArraySelected = selectedSlotArrayOffset;

            GetSelectedSlotArrayData.SlotArray1 = Kernel[selectedSlotArrayOffset++];
            GetSelectedSlotArrayData.SlotArray2 = Kernel[selectedSlotArrayOffset++];
            GetSelectedSlotArrayData.SlotArray3 = Kernel[selectedSlotArrayOffset++];
            GetSelectedSlotArrayData.SlotArray4 = Kernel[selectedSlotArrayOffset++];
            GetSelectedSlotArrayData.SlotArray5 = Kernel[selectedSlotArrayOffset++];
            GetSelectedSlotArrayData.SlotArray6 = Kernel[selectedSlotArrayOffset++];
            GetSelectedSlotArrayData.SlotArray7 = Kernel[selectedSlotArrayOffset++];
            GetSelectedSlotArrayData.SlotArray8 = Kernel[selectedSlotArrayOffset++];
            GetSelectedSlotArrayData.SlotArray9 = Kernel[selectedSlotArrayOffset++];
            GetSelectedSlotArrayData.SlotArray10 = Kernel[selectedSlotArrayOffset++];
            GetSelectedSlotArrayData.SlotArray11 = Kernel[selectedSlotArrayOffset++];
            GetSelectedSlotArrayData.SlotArray12 = Kernel[selectedSlotArrayOffset++];
            GetSelectedSlotArrayData.SlotArray13 = Kernel[selectedSlotArrayOffset++];
            GetSelectedSlotArrayData.SlotArray14 = Kernel[selectedSlotArrayOffset++];
            GetSelectedSlotArrayData.SlotArray15 = Kernel[selectedSlotArrayOffset++];
            GetSelectedSlotArrayData.SlotArray16 = Kernel[selectedSlotArrayOffset++];
            GetSelectedSlotArrayData.SlotArray17 = Kernel[selectedSlotArrayOffset++];
            GetSelectedSlotArrayData.SlotArray18 = Kernel[selectedSlotArrayOffset++];
            GetSelectedSlotArrayData.SlotArray19 = Kernel[selectedSlotArrayOffset++];
            GetSelectedSlotArrayData.SlotArray20 = Kernel[selectedSlotArrayOffset++];
            GetSelectedSlotArrayData.SlotArray21 = Kernel[selectedSlotArrayOffset++];
            GetSelectedSlotArrayData.SlotArray22 = Kernel[selectedSlotArrayOffset++];
            GetSelectedSlotArrayData.SlotArray23 = Kernel[selectedSlotArrayOffset++];
            GetSelectedSlotArrayData.SlotArray24 = Kernel[selectedSlotArrayOffset++];
            GetSelectedSlotArrayData.SlotArray25 = Kernel[selectedSlotArrayOffset++];
            GetSelectedSlotArrayData.SlotArray26 = Kernel[selectedSlotArrayOffset++];
            GetSelectedSlotArrayData.SlotArray27 = Kernel[selectedSlotArrayOffset++];
            GetSelectedSlotArrayData.SlotArray28 = Kernel[selectedSlotArrayOffset++];
            GetSelectedSlotArrayData.SlotArray29 = Kernel[selectedSlotArrayOffset++];
            GetSelectedSlotArrayData.SlotArray30 = Kernel[selectedSlotArrayOffset++];
            GetSelectedSlotArrayData.SlotArray31 = Kernel[selectedSlotArrayOffset++];
            GetSelectedSlotArrayData.SlotArray32 = Kernel[selectedSlotArrayOffset++];
            GetSelectedSlotArrayData.SlotArray33 = Kernel[selectedSlotArrayOffset++];
            GetSelectedSlotArrayData.SlotArray34 = Kernel[selectedSlotArrayOffset++];
            GetSelectedSlotArrayData.SlotArray35 = Kernel[selectedSlotArrayOffset++];
            GetSelectedSlotArrayData.SlotArray36 = Kernel[selectedSlotArrayOffset++];
            GetSelectedSlotArrayData.SlotArray37 = Kernel[selectedSlotArrayOffset++];
            GetSelectedSlotArrayData.SlotArray38 = Kernel[selectedSlotArrayOffset++];
            GetSelectedSlotArrayData.SlotArray39 = Kernel[selectedSlotArrayOffset++];
            GetSelectedSlotArrayData.SlotArray40 = Kernel[selectedSlotArrayOffset++];
            GetSelectedSlotArrayData.SlotArray41 = Kernel[selectedSlotArrayOffset++];
            GetSelectedSlotArrayData.SlotArray42 = Kernel[selectedSlotArrayOffset++];
            GetSelectedSlotArrayData.SlotArray43 = Kernel[selectedSlotArrayOffset++];
            GetSelectedSlotArrayData.SlotArray44 = Kernel[selectedSlotArrayOffset++];
            GetSelectedSlotArrayData.SlotArray45 = Kernel[selectedSlotArrayOffset++];
            GetSelectedSlotArrayData.SlotArray46 = Kernel[selectedSlotArrayOffset++];
            GetSelectedSlotArrayData.SlotArray47 = Kernel[selectedSlotArrayOffset++];
            GetSelectedSlotArrayData.SlotArray48 = Kernel[selectedSlotArrayOffset++];
            GetSelectedSlotArrayData.SlotArray49 = Kernel[selectedSlotArrayOffset++];
            GetSelectedSlotArrayData.SlotArray50 = Kernel[selectedSlotArrayOffset++];
            GetSelectedSlotArrayData.SlotArray51 = Kernel[selectedSlotArrayOffset++];
            GetSelectedSlotArrayData.SlotArray52 = Kernel[selectedSlotArrayOffset++];
            GetSelectedSlotArrayData.SlotArray53 = Kernel[selectedSlotArrayOffset++];
            GetSelectedSlotArrayData.SlotArray54 = Kernel[selectedSlotArrayOffset++];
            GetSelectedSlotArrayData.SlotArray55 = Kernel[selectedSlotArrayOffset++];
            GetSelectedSlotArrayData.SlotArray56 = Kernel[selectedSlotArrayOffset++];
            GetSelectedSlotArrayData.SlotArray57 = Kernel[selectedSlotArrayOffset++];
            GetSelectedSlotArrayData.SlotArray58 = Kernel[selectedSlotArrayOffset++];
            GetSelectedSlotArrayData.SlotArray59 = Kernel[selectedSlotArrayOffset++];
            GetSelectedSlotArrayData.SlotArray60 = Kernel[selectedSlotArrayOffset++];
        }

        public static void ReadSlotsSets(int SlotsSetsID_List, byte[] Kernel)
        {
            GetSelectedSlotsSetsData = new SlotsSetsData();
            int selectedSlotsSetsOffset = SlotsSetsDataOffset + (SlotsSetsID_List * 16);
            OffsetToSlotsSetsSelected = selectedSlotsSetsOffset;

            GetSelectedSlotsSetsData.Magic1 = Kernel[selectedSlotsSetsOffset++];
            GetSelectedSlotsSetsData.Count1 = Kernel[selectedSlotsSetsOffset++];
            GetSelectedSlotsSetsData.Magic2 = Kernel[selectedSlotsSetsOffset++];
            GetSelectedSlotsSetsData.Count2 = Kernel[selectedSlotsSetsOffset++];
            GetSelectedSlotsSetsData.Magic3 = Kernel[selectedSlotsSetsOffset++];
            GetSelectedSlotsSetsData.Count3 = Kernel[selectedSlotsSetsOffset++];
            GetSelectedSlotsSetsData.Magic4 = Kernel[selectedSlotsSetsOffset++];
            GetSelectedSlotsSetsData.Count4 = Kernel[selectedSlotsSetsOffset++];
            GetSelectedSlotsSetsData.Magic5 = Kernel[selectedSlotsSetsOffset++];
            GetSelectedSlotsSetsData.Count5 = Kernel[selectedSlotsSetsOffset++];
            GetSelectedSlotsSetsData.Magic6 = Kernel[selectedSlotsSetsOffset++];
            GetSelectedSlotsSetsData.Count6 = Kernel[selectedSlotsSetsOffset++];
            GetSelectedSlotsSetsData.Magic7 = Kernel[selectedSlotsSetsOffset++];
            GetSelectedSlotsSetsData.Count7 = Kernel[selectedSlotsSetsOffset++];
            GetSelectedSlotsSetsData.Magic8 = Kernel[selectedSlotsSetsOffset++];
            GetSelectedSlotsSetsData.Count8 = Kernel[selectedSlotsSetsOffset++];
        }

        #endregion

        #region DEVOUR

        public static void ReadDevour(int DevourID_List, byte[] Kernel)
        {
            GetSelectedDevourData = new DevourData();
            int selectedDevourOffset = DevourDataOffset + (DevourID_List * 12);
            OffsetToDevourSelected = selectedDevourOffset;

            GetSelectedDevourData.OffsetToDescription = FF8Text.BuildString((ushort)(
                BitConverter.ToInt32(Kernel, (int)KernelSections.Text_Devourtext) + (BitConverter.ToUInt16(Kernel, selectedDevourOffset))));
            selectedDevourOffset += 2;
            byte b = Kernel[selectedDevourOffset++];
            GetSelectedDevourData.HealDmg =
                b == (byte)HealDmg.Heal
                    ? HealDmg.Heal
                    : b == (byte)HealDmg.Damage
                        ? HealDmg.Damage
                        : 0; //Error handler
            GetSelectedDevourData.HpQuantity = Kernel[selectedDevourOffset++];
            GetSelectedDevourData.Status2 = Kernel[selectedDevourOffset++];
            GetSelectedDevourData.Status3 = Kernel[selectedDevourOffset++];
            GetSelectedDevourData.Status4 = Kernel[selectedDevourOffset++];
            GetSelectedDevourData.Status5 = Kernel[selectedDevourOffset++];
            GetSelectedDevourData.Status1 = Kernel[selectedDevourOffset++];
            selectedDevourOffset += 1;
            GetSelectedDevourData.RaisedStat = Kernel[selectedDevourOffset++];
            GetSelectedDevourData.RaisedHP = Kernel[selectedDevourOffset++];
        }

        #endregion

        #region MISC

        public static void ReadMisc(byte[] Kernel)
        {
            GetSelectedMiscData = new MiscData();
            int selectedMiscOffset = MiscDataOffset;
            OffsetToMiscSelected = selectedMiscOffset;

            GetSelectedMiscData.StatusTimer1 = Kernel[selectedMiscOffset++];
            GetSelectedMiscData.StatusTimer2 = Kernel[selectedMiscOffset++];
            GetSelectedMiscData.StatusTimer3 = Kernel[selectedMiscOffset++];
            GetSelectedMiscData.StatusTimer4 = Kernel[selectedMiscOffset++];
            GetSelectedMiscData.StatusTimer5 = Kernel[selectedMiscOffset++];
            GetSelectedMiscData.StatusTimer6 = Kernel[selectedMiscOffset++];
            GetSelectedMiscData.StatusTimer7 = Kernel[selectedMiscOffset++];
            GetSelectedMiscData.StatusTimer8 = Kernel[selectedMiscOffset++];
            GetSelectedMiscData.StatusTimer9 = Kernel[selectedMiscOffset++];
            GetSelectedMiscData.StatusTimer10 = Kernel[selectedMiscOffset++];
            GetSelectedMiscData.StatusTimer11 = Kernel[selectedMiscOffset++];
            GetSelectedMiscData.StatusTimer12 = Kernel[selectedMiscOffset++];
            GetSelectedMiscData.StatusTimer13 = Kernel[selectedMiscOffset++];
            GetSelectedMiscData.StatusTimer14 = Kernel[selectedMiscOffset++];
            GetSelectedMiscData.ATB = Kernel[selectedMiscOffset++];
            GetSelectedMiscData.DeadTimer = Kernel[selectedMiscOffset++];
            GetSelectedMiscData.StatusLimit1 = Kernel[selectedMiscOffset++];
            GetSelectedMiscData.StatusLimit2 = Kernel[selectedMiscOffset++];
            GetSelectedMiscData.StatusLimit3 = Kernel[selectedMiscOffset++];
            GetSelectedMiscData.StatusLimit4 = Kernel[selectedMiscOffset++];
            GetSelectedMiscData.StatusLimit5 = Kernel[selectedMiscOffset++];
            GetSelectedMiscData.StatusLimit6 = Kernel[selectedMiscOffset++];
            GetSelectedMiscData.StatusLimit7 = Kernel[selectedMiscOffset++];
            GetSelectedMiscData.StatusLimit8 = Kernel[selectedMiscOffset++];
            GetSelectedMiscData.StatusLimit9 = Kernel[selectedMiscOffset++];
            GetSelectedMiscData.StatusLimit10 = Kernel[selectedMiscOffset++];
            GetSelectedMiscData.StatusLimit11 = Kernel[selectedMiscOffset++];
            GetSelectedMiscData.StatusLimit12 = Kernel[selectedMiscOffset++];
            GetSelectedMiscData.StatusLimit13 = Kernel[selectedMiscOffset++];
            GetSelectedMiscData.StatusLimit14 = Kernel[selectedMiscOffset++];
            GetSelectedMiscData.StatusLimit15 = Kernel[selectedMiscOffset++];
            GetSelectedMiscData.StatusLimit16 = Kernel[selectedMiscOffset++];
            GetSelectedMiscData.StatusLimit17 = Kernel[selectedMiscOffset++];
            GetSelectedMiscData.StatusLimit18 = Kernel[selectedMiscOffset++];
            GetSelectedMiscData.StatusLimit19 = Kernel[selectedMiscOffset++];
            GetSelectedMiscData.StatusLimit20 = Kernel[selectedMiscOffset++];
            GetSelectedMiscData.StatusLimit21 = Kernel[selectedMiscOffset++];
            GetSelectedMiscData.StatusLimit22 = Kernel[selectedMiscOffset++];
            GetSelectedMiscData.StatusLimit23 = Kernel[selectedMiscOffset++];
            GetSelectedMiscData.StatusLimit24 = Kernel[selectedMiscOffset++];
            GetSelectedMiscData.StatusLimit25 = Kernel[selectedMiscOffset++];
            GetSelectedMiscData.StatusLimit26 = Kernel[selectedMiscOffset++];
            GetSelectedMiscData.StatusLimit27 = Kernel[selectedMiscOffset++];
            GetSelectedMiscData.StatusLimit28 = Kernel[selectedMiscOffset++];
            GetSelectedMiscData.StatusLimit29 = Kernel[selectedMiscOffset++];
            GetSelectedMiscData.StatusLimit30 = Kernel[selectedMiscOffset++];
            GetSelectedMiscData.StatusLimit31 = Kernel[selectedMiscOffset++];
            GetSelectedMiscData.StatusLimit32 = Kernel[selectedMiscOffset++];
            GetSelectedMiscData.DuelTimerCL1 = Kernel[selectedMiscOffset++];
            GetSelectedMiscData.DuelSeqCL1 = Kernel[selectedMiscOffset++];
            GetSelectedMiscData.DuelTimerCL2 = Kernel[selectedMiscOffset++];
            GetSelectedMiscData.DuelSeqCL2 = Kernel[selectedMiscOffset++];
            GetSelectedMiscData.DuelTimerCL3 = Kernel[selectedMiscOffset++];
            GetSelectedMiscData.DuelSeqCL3 = Kernel[selectedMiscOffset++];
            GetSelectedMiscData.DuelTimerCL4 = Kernel[selectedMiscOffset++];
            GetSelectedMiscData.DuelSeqCL4 = Kernel[selectedMiscOffset++];
            GetSelectedMiscData.ShotTimerCL1 = Kernel[selectedMiscOffset++];
            GetSelectedMiscData.ShotTimerCL2 = Kernel[selectedMiscOffset++];
            GetSelectedMiscData.ShotTimerCL3 = Kernel[selectedMiscOffset++];
            GetSelectedMiscData.ShotTimerCL4 = Kernel[selectedMiscOffset++];
        }

        #endregion

        #region COMMAND ABILITY DATA

        public static void ReadCommandAbilityData(int CommandAbilityDataID_List, byte[] Kernel)
        {
            GetSelectedCommandAbilityDataData = new CommandAbilityDataData();
            int selectedCommandAbilityDataDataOffset = CommandAbilityDataDataOffset + (CommandAbilityDataID_List * 16);
            OffsetToCommandAbilityDataSelected = selectedCommandAbilityDataDataOffset;

            GetSelectedCommandAbilityDataData.MagicID = (ushort)(BitConverter.ToUInt16(Kernel, selectedCommandAbilityDataDataOffset));
            selectedCommandAbilityDataDataOffset += 4;
            GetSelectedCommandAbilityDataData.AttackType = Kernel[selectedCommandAbilityDataDataOffset++];
            GetSelectedCommandAbilityDataData.AttackPower = Kernel[selectedCommandAbilityDataDataOffset++];
            GetSelectedCommandAbilityDataData.AttackFlags = Kernel[selectedCommandAbilityDataDataOffset++];
            GetSelectedCommandAbilityDataData.HitCount = Kernel[selectedCommandAbilityDataDataOffset++];
            byte b = Kernel[selectedCommandAbilityDataDataOffset++];
            GetSelectedCommandAbilityDataData.Element =
                b == (byte)Element.Fire
                    ? Element.Fire
                    : b == (byte)Element.Holy
                        ? Element.Holy
                        : b == (byte)Element.Ice
                            ? Element.Ice
                            : b == (byte)Element.NonElemental
                                ? Element.NonElemental
                                : b == (byte)Element.Poison
                                    ? Element.Poison
                                    : b == (byte)Element.Thunder
                                        ? Element.Thunder
                                        : b == (byte)Element.Water
                                            ? Element.Water
                                            : b == (byte)Element.Wind
                                                ? Element.Wind
                                                : b == (byte)Element.Earth
                                                    ? Element.Earth
                                                    : 0; //Error handler
            GetSelectedCommandAbilityDataData.StatusAttack = Kernel[selectedCommandAbilityDataDataOffset++];
            GetSelectedCommandAbilityDataData.Status1 = Kernel[selectedCommandAbilityDataDataOffset++];
            selectedCommandAbilityDataDataOffset += 1;
            GetSelectedCommandAbilityDataData.Status2 = Kernel[selectedCommandAbilityDataDataOffset++];
            GetSelectedCommandAbilityDataData.Status3 = Kernel[selectedCommandAbilityDataDataOffset++];
            GetSelectedCommandAbilityDataData.Status4 = Kernel[selectedCommandAbilityDataDataOffset++];
            GetSelectedCommandAbilityDataData.Status5 = Kernel[selectedCommandAbilityDataDataOffset++];
        }

        #endregion

        #region COMMAND ABILITY

        public static void ReadCommandAbility(int CommandAbilityID_List, byte[] Kernel)
        {
            GetSelectedCommandAbilityData = new CommandAbilityData();
            int selectedCommandAbilityDataOffset = CommandAbilityDataOffset + (CommandAbilityID_List * 8);
            OffsetToCommandAbilitySelected = selectedCommandAbilityDataOffset;

            GetSelectedCommandAbilityData.OffsetToName = FF8Text.BuildString((ushort)(
                BitConverter.ToInt32(Kernel, (int)KernelSections.Text_Commandabilitiestext) + (BitConverter.ToUInt16(Kernel, selectedCommandAbilityDataOffset))));
            GetSelectedCommandAbilityData.OffsetToDescription = FF8Text.BuildString((ushort)(
                BitConverter.ToInt32(Kernel, (int)KernelSections.Text_Commandabilitiestext) + (BitConverter.ToUInt16(Kernel, selectedCommandAbilityDataOffset + 2))));
            selectedCommandAbilityDataOffset += 4;
            GetSelectedCommandAbilityData.AP = Kernel[selectedCommandAbilityDataOffset++];
            GetSelectedCommandAbilityData.BattleCommand = Kernel[selectedCommandAbilityDataOffset++];
        }

        #endregion

        #region JUNCTION ABILITIES

        public static void ReadJunctionAbilities(int JunctionAbilitiesID_List, byte[] Kernel)
        {
            GetSelectedJunctionAbilitiesData = new JunctionAbilitiesData();
            JunctionAbilitiesID_List++; //skip dummy entry
            int selectedJunctionAbilitiesOffset = JunctionAbilitiesDataOffset + (JunctionAbilitiesID_List * 8);
            OffsetToJunctionAbilitiesSelected = selectedJunctionAbilitiesOffset;

            GetSelectedJunctionAbilitiesData.OffsetToName = FF8Text.BuildString((ushort)(
                BitConverter.ToInt32(Kernel, (int)KernelSections.Text_Junctionabilitiestext) + (BitConverter.ToUInt16(Kernel, selectedJunctionAbilitiesOffset))));
            GetSelectedJunctionAbilitiesData.OffsetToDescription = FF8Text.BuildString((ushort)(
                BitConverter.ToInt32(Kernel, (int)KernelSections.Text_Junctionabilitiestext) + (BitConverter.ToUInt16(Kernel, selectedJunctionAbilitiesOffset + 2))));
            selectedJunctionAbilitiesOffset += 4;
            GetSelectedJunctionAbilitiesData.AP = Kernel[selectedJunctionAbilitiesOffset++];
            GetSelectedJunctionAbilitiesData.Flag1 = Kernel[selectedJunctionAbilitiesOffset++];
            GetSelectedJunctionAbilitiesData.Flag2 = Kernel[selectedJunctionAbilitiesOffset++];
            GetSelectedJunctionAbilitiesData.Flag3 = Kernel[selectedJunctionAbilitiesOffset++];
        }

        #endregion

        #region PARTY ABILITIES

        public static void ReadPartyAbilities(int PartyAbilitiesID_List, byte[] Kernel)
        {
            GetSelectedPartyAbilitiesData = new PartyAbilitiesData();
            int selectedPartyAbilitiesOffset = PartyAbilitiesDataOffset + (PartyAbilitiesID_List * 8);
            OffsetToPartyAbilitiesSelected = selectedPartyAbilitiesOffset;

            GetSelectedPartyAbilitiesData.OffsetToName = FF8Text.BuildString((ushort)(
                BitConverter.ToInt32(Kernel, (int)KernelSections.Text_Partyabilitytext) + (BitConverter.ToUInt16(Kernel, selectedPartyAbilitiesOffset))));
            GetSelectedPartyAbilitiesData.OffsetToDescription = FF8Text.BuildString((ushort)(
                BitConverter.ToInt32(Kernel, (int)KernelSections.Text_Partyabilitytext) + (BitConverter.ToUInt16(Kernel, selectedPartyAbilitiesOffset + 2))));
            selectedPartyAbilitiesOffset += 4;
            GetSelectedPartyAbilitiesData.AP = Kernel[selectedPartyAbilitiesOffset++];
            GetSelectedPartyAbilitiesData.Flag = Kernel[selectedPartyAbilitiesOffset++];
        }

        #endregion

        #region GF ABILITIES

        public static void ReadGFAbilities(int GFAbilitiesID_List, byte[] Kernel)
        {
            GetSelectedGFAbilitiesData = new GFAbilitiesData();
            int selectedGFAbilitiesOffset = GFAbilitiesDataOffset + (GFAbilitiesID_List * 8);
            OffsetToGFAbilitiesSelected = selectedGFAbilitiesOffset;

            GetSelectedGFAbilitiesData.OffsetToName = FF8Text.BuildString((ushort)(
                BitConverter.ToInt32(Kernel, (int)KernelSections.Text_GFabilitytext) + (BitConverter.ToUInt16(Kernel, selectedGFAbilitiesOffset))));
            GetSelectedGFAbilitiesData.OffsetToDescription = FF8Text.BuildString((ushort)(
                BitConverter.ToInt32(Kernel, (int)KernelSections.Text_GFabilitytext) + (BitConverter.ToUInt16(Kernel, selectedGFAbilitiesOffset + 2))));
            selectedGFAbilitiesOffset += 4;
            GetSelectedGFAbilitiesData.AP = Kernel[selectedGFAbilitiesOffset++];
            GetSelectedGFAbilitiesData.EnableBoost = Kernel[selectedGFAbilitiesOffset++];
            byte b = Kernel[selectedGFAbilitiesOffset++];
            GetSelectedGFAbilitiesData.StatToIncrease =
                b == (byte)StatToIncrease.SumMag
                    ? StatToIncrease.SumMag
                    : b == (byte)StatToIncrease.HP
                        ? StatToIncrease.HP
                        : b == (byte)StatToIncrease.Boost
                            ? StatToIncrease.Boost
                            : 0; //Error handler
            GetSelectedGFAbilitiesData.IncrementValue = Kernel[selectedGFAbilitiesOffset++];
        }

        #endregion

        #region CHARACTER ABILITIES

        public static void ReadCharacterAbilities(int CharacterAbilitiesID_List, byte[] Kernel)
        {
            GetSelectedCharacterAbilitiesData = new CharacterAbilitiesData();
            int selectedCharacterAbilitiesOffset = CharacterAbilitiesDataOffset + (CharacterAbilitiesID_List * 8);
            OffsetToCharacterAbilitiesSelected = selectedCharacterAbilitiesOffset;

            GetSelectedCharacterAbilitiesData.OffsetToName = FF8Text.BuildString((ushort)(
                BitConverter.ToInt32(Kernel, (int)KernelSections.Text_Characterabilitytext) + (BitConverter.ToUInt16(Kernel, selectedCharacterAbilitiesOffset))));
            GetSelectedCharacterAbilitiesData.OffsetToDescription = FF8Text.BuildString((ushort)(
                BitConverter.ToInt32(Kernel, (int)KernelSections.Text_Characterabilitytext) + (BitConverter.ToUInt16(Kernel, selectedCharacterAbilitiesOffset + 2))));
            selectedCharacterAbilitiesOffset += 4;
            GetSelectedCharacterAbilitiesData.AP = Kernel[selectedCharacterAbilitiesOffset++];
            GetSelectedCharacterAbilitiesData.Flag1 = Kernel[selectedCharacterAbilitiesOffset++];
            GetSelectedCharacterAbilitiesData.Flag2 = Kernel[selectedCharacterAbilitiesOffset++];
            GetSelectedCharacterAbilitiesData.Flag3 = Kernel[selectedCharacterAbilitiesOffset++];
        }

        #endregion

        #region MENU ABILITIES

        public static void ReadMenuAbilities(int MenuAbilitiesID_List, byte[] Kernel)
        {
            GetSelectedMenuAbilitiesData = new MenuAbilitiesData();
            int selectedMenuAbilitiesOffset = MenuAbilitiesDataOffset + (MenuAbilitiesID_List * 8);
            OffsetToMenuAbilitiesSelected = selectedMenuAbilitiesOffset;

            GetSelectedMenuAbilitiesData.OffsetToName = FF8Text.BuildString((ushort)(
                BitConverter.ToInt32(Kernel, (int)KernelSections.Text_Menuabilitytext) + (BitConverter.ToUInt16(Kernel, selectedMenuAbilitiesOffset))));
            GetSelectedMenuAbilitiesData.OffsetToDescription = FF8Text.BuildString((ushort)(
                BitConverter.ToInt32(Kernel, (int)KernelSections.Text_Menuabilitytext) + (BitConverter.ToUInt16(Kernel, selectedMenuAbilitiesOffset + 2))));
            selectedMenuAbilitiesOffset += 4;
            GetSelectedMenuAbilitiesData.AP = Kernel[selectedMenuAbilitiesOffset++];
            GetSelectedMenuAbilitiesData.Index = Kernel[selectedMenuAbilitiesOffset++];
            GetSelectedMenuAbilitiesData.StartEntry = Kernel[selectedMenuAbilitiesOffset++];
            GetSelectedMenuAbilitiesData.EndEntry = Kernel[selectedMenuAbilitiesOffset++];
        }

        #endregion

        #region BATTLE COMMANDS

        public static void ReadBattleCommands(int BattleCommandsID_List, byte[] Kernel)
        {
            GetSelectedBattleCommandsData = new BattleCommandsData();
            BattleCommandsID_List++; //skip dummy entry
            int selectedBattleCommandsOffset = BattleCommandsDataOffset + (BattleCommandsID_List * 8);
            OffsetToBattleCommandsSelected = selectedBattleCommandsOffset;

            GetSelectedBattleCommandsData.OffsetToName = FF8Text.BuildString((ushort)(
                BitConverter.ToInt32(Kernel, (int)KernelSections.Text_BattleCommand) + (BitConverter.ToUInt16(Kernel, selectedBattleCommandsOffset))));
            GetSelectedBattleCommandsData.OffsetToDescription = FF8Text.BuildString((ushort)(
                BitConverter.ToInt32(Kernel, (int)KernelSections.Text_BattleCommand) + (BitConverter.ToUInt16(Kernel, selectedBattleCommandsOffset + 2))));
            selectedBattleCommandsOffset += 4;
            GetSelectedBattleCommandsData.AbilityID = Kernel[selectedBattleCommandsOffset++];
            GetSelectedBattleCommandsData.Flag = Kernel[selectedBattleCommandsOffset++];
            GetSelectedBattleCommandsData.Target = Kernel[selectedBattleCommandsOffset++];
        }

        #endregion

        #region RINOA COMMANDS

        public static void ReadRinoaCommands(int RinoaCommandsID_List, byte[] Kernel)
        {
            GetSelectedRinoaCommandsData = new RinoaCommandsData();
            int selectedRinoaCommandsOffset = RinoaCommandsDataOffset + (RinoaCommandsID_List * 8);
            OffsetToRinoaCommandsSelected = selectedRinoaCommandsOffset;

            GetSelectedRinoaCommandsData.OffsetToName = FF8Text.BuildString((ushort)(
                BitConverter.ToInt32(Kernel, (int)KernelSections.Text_Rinoalimitbreaktext) + (BitConverter.ToUInt16(Kernel, selectedRinoaCommandsOffset))));
            GetSelectedRinoaCommandsData.OffsetToDescription = FF8Text.BuildString((ushort)(
                BitConverter.ToInt32(Kernel, (int)KernelSections.Text_Rinoalimitbreaktext) + (BitConverter.ToUInt16(Kernel, selectedRinoaCommandsOffset + 2))));
            selectedRinoaCommandsOffset += 4;
            GetSelectedRinoaCommandsData.Flag = Kernel[selectedRinoaCommandsOffset++];
            GetSelectedRinoaCommandsData.Target = Kernel[selectedRinoaCommandsOffset++];
            GetSelectedRinoaCommandsData.AbilityID = Kernel[selectedRinoaCommandsOffset++];
        }

        #endregion

        #endregion

        private static void InitializeTextPointers()
        {
            int size = Sections._1_BattleCommands * 2;
            size += Sections._2_MagicData * 2;
            size += Sections._3_JGF * 2;
            size += Sections._4_EnAttack;
            size += Sections._5_Weap;
            size += Sections._6_renzokukenF * 2;
            size += Sections._7_char;
            size += Sections._8_BatItems * 2;
            size += Sections._9_Items * 2; //<--
            size += Sections._10_nonJGF;
            size += Sections._12_JnctAblt * 2;
            size += Sections._13_CommAbilities * 2;
            size += Sections._14_offPercent * 2;
            size += Sections._15_CharAbilt * 2;
            size += Sections._16_PartyAbil * 2;
            size += Sections._17_GFAbil * 2;
            size += Sections._18_MenuAbil * 2;
            size += Sections._19_tempCharAb * 2;
            size += Sections._20_BlueMag * 2;
            size += Sections._22_IrvineLB * 2;
            size += Sections._23_ZellDuel * 2;
            size += Sections._25_RinoaLB * 2;
            size += Sections._26_RinoaLB2;
            size += Sections._29_Devour;
            TextOffsets = new uint[size, 2];
            int index = 0;
            for (int i = 0; i != Sections._1_BattleCommands * 2 - 2; i += 2) //BattleCommands
            {
                TextOffsets[i, 0] = (uint)(BattleCommandsDataOffset + index * 8 + 0);
                /*if (TextOffsets[i, 0] != 0xFFFF)
                    TextOffsets[i, 1] =
                        (uint)
                            FF8Text.BuildString(BitConverter.ToUInt16(Kernel, (int)KernelSections.Text_BattleCommand) +
                                                (int)TextOffsets[i, 0]).Length;
                                                */
                TextOffsets[i + 1, 0] = (uint)(BattleCommandsDataOffset + index * 8 + 2);
                /*if (TextOffsets[i + 1, 0] != 0xFFFF)
                    TextOffsets[i + 1, 1] =
                        (uint)
                            FF8Text.BuildString(BitConverter.ToUInt16(Kernel, (int)KernelSections.Text_BattleCommand) +
                                                (int)TextOffsets[i + 1, 0]).Length;*/
                index++;
            }
            index = 0;
        }

        /// <summary>
        /// entry should be the TextOffset[ENTRY] position!
        /// </summary>
        /// <param name="entry">should be all sections global to TextOffset[entry]</param>
        /// <param name="sender">is string of sender.Text</param>
        /// <param name="change">change is natural difference between two strings</param>
        private static void Text_MovePointers(int entry, string sender, int change)
        {
            return; //It's huge function/data processing, so return to make the software working before this is finished

            //Update every possible pointer in all possible sections that have text
            for (int i = entry; i != TextOffsets.GetLength(0); i++)
            {
                if (entry < Sections._1_BattleCommands*2)
                {
                    //adjust pointers for Text_BattleCommands
                }
                /*if (TextOffsets[i, 0] != 0)
                {
                    ushort buffer = (ushort) ((int) TextOffsets[i, 1] + change);
                    byte[] bufBytes = BitConverter.GetBytes(buffer);
                    Array.Copy(bufBytes, 0, Kernel, (int) TextOffsets[i, 0], 2);
                }*/
            }

        }
    }
}

