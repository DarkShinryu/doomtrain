using System;
using System.Text;

namespace Doomtrain
{
    class KernelWorker
    {
        #region DECLARATIONS

        public static byte[] Kernel;

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

        public static int CombineDataOffset = -1;
        public static int OffsetToCombineSelected = -1;

        public static int BattleItemsDataOffset = -1;
        public static int OffsetToBattleItemsSelected = -1;

        public static int SlotsSetsDataOffset = -1;
        public static int OffsetToSlotsSetsSelected = -1;

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
        public static CombineData GetSelectedCombineData;
        public static BattleItemsData GetSelectedBattleItemsData;
        public static SlotsSetsData GetSelectedSlotsSetsData;


        static string[] _charstable;
        private static readonly string Chartable =
        @" , ,1,2,3,4,5,6,7,8,9,%,/,:,!,?,…,+,-,=,*,&,「,」,(,),·,.,,,~,“,”,‘,#,$,',_,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,À,Á,Â,Ä,Ç,È,É,Ê,Ë,Ì,Í,Î,Ï,Ñ,Ò,Ó,Ô,Ö,Ù,Ú,Û,Ü,Œ,ß,à,á,â,ä,ç,è,é,ê,ë,ì,í,î,ï,ñ,ò";


        public enum KernelSections : ushort
        { //BitShift to left, to make fast and natural MULTIPLY BY 2 operation
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
            CommandAbility = 11 << 2,
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


        public struct MagicData
        {
            public string OffsetSpellName;
            //public string OffsetSpellDescription;
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
            //public string OffsetGFName;
            //public string OffsetGFDescription;
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
            //public string OffsetGFAttacksName;
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
            //public string WeaponsName;
            public byte RenzokukenFinishers;
            public byte CharacterID;
            public byte AttackType;
            public byte AttackPower;
            public byte HITBonus;
            public byte STRBonus;
            public byte Tier;
        }

        public struct CharactersData
        {
            //public string Name;
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
            //public string OffsetToName
            public UInt16 MagicID;
            public byte AttackType;
            public byte AttackPower;
            public byte AttackFlags;
            public Element Element;
            public byte StatusAttack;
            public byte Status1;
            public byte Status2;
            public byte Status3;
            public byte Status4;
            public byte Status5;
        }

        public struct BlueMagicData
        {
            //public string OffsetToName;
            //public string OffsetToDescription;
            public UInt16 MagicID;
            public byte AttackType;
            public byte AttackFlags;
            public Element Element;
            public byte StatusAttack;
        }

        public struct BlueMagicParamData
        {
            public byte Status1CL1;
            public byte Status2CL1;
            public byte Status3CL1;
            public byte Status4CL1;
            public byte Status5CL1;
            public byte AttackPowerCL1;
            public byte DeathLevelCL1;
            public byte Status1CL2;
            public byte Status2CL2;
            public byte Status3CL2;
            public byte Status4CL2;
            public byte Status5CL2;
            public byte AttackPowerCL2;
            public byte DeathLevelCL2;
            public byte Status1CL3;
            public byte Status2CL3;
            public byte Status3CL3;
            public byte Status4CL3;
            public byte Status5CL3;
            public byte AttackPowerCL3;
            public byte DeathLevelCL3;
            public byte Status1CL4;
            public byte Status2CL4;
            public byte Status3CL4;
            public byte Status4CL4;
            public byte Status5CL4;
            public byte AttackPowerCL4;
            public byte DeathLevelCL4;
        }

        public struct StatPercentageAbilitiesData
        {
            public byte StatToincrease;
            public byte IncreasementValue;
        }

        public struct RenzoFinData
        {
            //public string OffsetToName;
            //public string OffsetToDescription;
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
            //public string OffsetToName;
            //public string OffsetToDescription;
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
            //public string OffsetToName;
            //public string OffsetToDescription;
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
            public byte UsedItem;
        }

        public struct DuelData
        {
            //public string OffsetToName;
            //public string OffsetToDescription;
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
            public UInt16 Button1;
            public UInt16 Button2;
            public UInt16 Button3;
            public UInt16 Button4;
            public UInt16 Button5;
        }

        public struct CombineData
        {
            //public string OffsetToName;
            //public string OffsetToDescription;
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
            //public string OffsetToName;
            //public string OffsetToDescription;
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
            public byte HitCount;
            public Element Element;
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

        #endregion

        #region WRITE KERNEL VARIABLES

        public static void UpdateVariable_Magic(int index, object variable, byte arg0 = 127)
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
                        Array.Copy(temp, 0, Kernel, OffsetToMagicSelected + 36, 2);
                        return;
                    }

                case 22:
                    {
                        ushort a = BitConverter.ToUInt16(Kernel, OffsetToMagicSelected + 40);
                        byte[] temp = BitConverter.GetBytes(a ^= Convert.ToUInt16(variable));
                        Array.Copy(temp, 0, Kernel, OffsetToMagicSelected + 39, 2);
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

        public static void UpdateVariable_GF(int index, object variable, byte AbilityIndex = 0, byte arg0 = 127)
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

        public static void UpdateVariable_GFAttacks(int index, object variable, byte arg0 = 127)
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

        public static void UpdateVariable_Weapons(int index, object variable, byte arg0 = 127)
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
                    Kernel[OffsetToWeaponsSelected + 6] = Convert.ToByte(variable); //attack power
                    return;
                case 3:
                    Kernel[OffsetToWeaponsSelected + 7] = Convert.ToByte(variable); //hit bonus
                    return;
                case 4:
                    Kernel[OffsetToWeaponsSelected + 8] = Convert.ToByte(variable); //str bonus
                    return;
                case 5:
                    Kernel[OffsetToWeaponsSelected + 9] = Convert.ToByte(variable); //weapon tier
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

        public static void UpdateVariable_Characters(int index, object variable)
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

                default:
                    return;
            }
        }

        public static void UpdateVariable_EnemyAttacks(int index, object variable, byte arg0 = 127)
        {
            if (!mainForm._loaded || Kernel == null)
                return;
            switch (index)
            {
                case 0:
                    UshortToKernel(Convert.ToUInt16(variable), 2, (byte)Mode.Mode_EnemyAttacks); //MagicID
                    return;
                case 1:
                    Kernel[OffsetToEnemyAttacksSelected + 6] = Convert.ToByte(variable); //attack type
                    return;
                case 2:
                    Kernel[OffsetToEnemyAttacksSelected + 7] = Convert.ToByte(variable); //attack power
                    return;
                case 3:
                    Kernel[OffsetToEnemyAttacksSelected + 8] ^= Convert.ToByte(variable); //attack flags
                    return;
                case 4:
                    Kernel[OffsetToEnemyAttacksSelected + 10] = Convert.ToByte(variable); //element
                    return;
                case 5:
                    Kernel[OffsetToEnemyAttacksSelected + 12] = Convert.ToByte(variable); //status attack
                    return;
                case 6:
                    EnemyAttacksStatusUpdator(arg0, variable); //Status
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

        public static void UpdateVariable_BlueMagic(int index, object variable)
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
                    Kernel[OffsetToBlueMagicParamSelected + 7] = Convert.ToByte(variable); //death level CL1
                    return;
                case 3:
                    Kernel[OffsetToBlueMagicParamSelected + 14] = Convert.ToByte(variable); //attack power CL2
                    return;
                case 4:
                    Kernel[OffsetToBlueMagicParamSelected + 15] = Convert.ToByte(variable); //death level CL2
                    return;
                case 5:
                    Kernel[OffsetToBlueMagicParamSelected + 22] = Convert.ToByte(variable); //attack power CL3
                    return;
                case 6:
                    Kernel[OffsetToBlueMagicParamSelected + 23] = Convert.ToByte(variable); //death level CL3
                    return;
                case 7:
                    Kernel[OffsetToBlueMagicParamSelected + 30] = Convert.ToByte(variable); //attack power CL4
                    return;
                case 8:
                    Kernel[OffsetToBlueMagicParamSelected + 31] = Convert.ToByte(variable); //death level CL4
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

        public static void UpdateVariable_StatPercentageAbilities(int index, object variable)
        {
            if (!mainForm._loaded || Kernel == null)
                return;
            switch (index)
            {
                case 0:
                    Kernel[OffsetToStatPercentageAbilitiesSelected + 5] = Convert.ToByte(variable); //stat to increase
                    return;
                case 1:
                    Kernel[OffsetToStatPercentageAbilitiesSelected + 6] = Convert.ToByte(variable); //increasement value
                    return;

                default:
                    return;
            }
        }

        public static void UpdateVariable_RenzoFin(int index, object variable, byte arg0 = 127)
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

        public static void UpdateVariable_TempCharLB(int index, object variable, byte arg0 = 127)
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

        public static void UpdateVariable_Shot(int index, object variable, byte arg0 = 127)
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
                    ShotStatusUpdator(arg0, variable); //Status
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

        public static void UpdateVariable_Duel(int index, object variable, byte arg0 = 127)
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
                    Kernel[OffsetToDuelSelected + 16] ^= Convert.ToByte(variable); //button 1, not sure if correct
                    return;
                case 10:
                    Kernel[OffsetToDuelSelected + 18] ^= Convert.ToByte(variable); //button 2
                    return;
                case 11:
                    Kernel[OffsetToDuelSelected + 20] ^= Convert.ToByte(variable); //button 3
                    return;
                case 12:
                    Kernel[OffsetToDuelSelected + 22] ^= Convert.ToByte(variable); //button 4
                    return;
                case 13:
                    Kernel[OffsetToDuelSelected + 24] ^= Convert.ToByte(variable); //button 5
                    return;
                case 14:
                    DuelStatusUpdator(arg0, variable); //Status
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

        public static void UpdateVariable_Combine(int index, object variable, byte arg0 = 127)
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

        public static void UpdateVariable_BattleItems(int index, object variable, byte arg0 = 127)
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
                    Kernel[OffsetToBattleItemsSelected + 22] = Convert.ToByte(variable); //Hit Count
                    return;
                case 8:
                    Kernel[OffsetToBattleItemsSelected + 23] = Convert.ToByte(variable); //Element
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

        #region MAGIC ID

        /// <summary>
        /// This is for MagicID list
        /// </summary>
        /// <param name="a"></param>
        /// <param name="add"></param>
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
            Mode_BattleItems
        }

        #endregion

        #region READ KERNEL VARIABLES

        public static void ReadKernel(byte[] kernel)
        {
            Kernel = kernel;
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
            CombineDataOffset = BitConverter.ToInt32(Kernel, (int)KernelSections.RinoaLimit2);
            BattleItemsDataOffset = BitConverter.ToInt32(Kernel, (int)KernelSections.BattleItems);
            SlotsSetsDataOffset = BitConverter.ToInt32(Kernel, (int)KernelSections.SelphieSlotsSets);
        }


        public static void ReadMagic(int MagicID_List)
        {

            GetSelectedMagicData = new MagicData();
            MagicID_List++; //skip dummy entry
            /*
            int SelectedMagicOffset = MagicID_List == 0 ? 
                MagicDataOffset + 8 + (MagicID_List * 60) 
                : MagicDataOffset + (MagicID_List * 60);*/

            int selectedMagicOffset = MagicDataOffset + (MagicID_List * 60);
            OffsetToMagicSelected = selectedMagicOffset;

            #region UnusedNameRegion functionality. You can use it for future improvements
            GetSelectedMagicData.OffsetSpellName = BuildString((ushort)(
                    BitConverter.ToInt32(Kernel, (int)KernelSections.Text_Magictext) + (BitConverter.ToUInt16(Kernel, selectedMagicOffset))));

            //BELOW DOESN'T WORK?
            // GetSelectedMagicData.OffsetSpellDescription = BuildString((ushort)(
            //BitConverter.ToInt32(kernel, (int)KernelSections.Text_Magictext) + (BitConverter.ToUInt16(kernel, SelectedMagicOffset += 2))));
            //Console.WriteLine("DEBUG: {0}", GetSelectedMagicData.OffsetSpellName);
            #endregion


            GetSelectedMagicData.MagicID = BitConverter.ToUInt16(Kernel, selectedMagicOffset += 4);
            selectedMagicOffset += 2;
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

        public static void ReadGF(int GFID_List)
        {
            GetSelectedGFData = new GFData();
            int selectedGfOffset = GFDataOffset + (GFID_List * 132);
            OffsetToGFSelected = selectedGfOffset;

            GetSelectedGFData.GFMagicID = (ushort)(BitConverter.ToUInt16(Kernel, selectedGfOffset + 4));
            selectedGfOffset += 4 + 2; //Name Offset + Description Offset + MagicID
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
            GetSelectedGFData.GFHP = Kernel[selectedGfOffset];
            selectedGfOffset += 7; //Unknown+GFHP
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

            selectedGfOffset += 2;
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

        public static void ReadGFAttacks(int GFAttacksID_List)
        {
            GetSelectedGFAttacksData = new GFAttacksData();
            int selectedGfAttacksOffset = GFAttacksDataOffset + (GFAttacksID_List * 20);
            OffsetToGFAttacksSelected = selectedGfAttacksOffset;

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

        public static void ReadWeapons(int WeaponsID_List)
        {
            GetSelectedWeaponsData = new WeaponsData();
            int selectedWeaponsOffset = WeaponsDataOffset + (WeaponsID_List * 12);
            OffsetToWeaponsSelected = selectedWeaponsOffset;

            GetSelectedWeaponsData.RenzokukenFinishers = Kernel[selectedWeaponsOffset + 2];
            selectedWeaponsOffset += 4;
            GetSelectedWeaponsData.CharacterID = Kernel[selectedWeaponsOffset++];
            GetSelectedWeaponsData.AttackType = Kernel[selectedWeaponsOffset++];
            GetSelectedWeaponsData.AttackPower = Kernel[selectedWeaponsOffset++];
            GetSelectedWeaponsData.HITBonus = Kernel[selectedWeaponsOffset++];
            GetSelectedWeaponsData.STRBonus = Kernel[selectedWeaponsOffset++];
            GetSelectedWeaponsData.Tier = Kernel[selectedWeaponsOffset++];
        }

        public static void ReadCharacters(int CharactersID_List)
        {
            GetSelectedCharactersData = new CharactersData();
            int selectedCharactersOffset = CharactersDataOffset + (CharactersID_List * 36);
            OffsetToCharactersSelected = selectedCharactersOffset;

            GetSelectedCharactersData.CrisisLevel = Kernel[selectedCharactersOffset + 2];
            selectedCharactersOffset += 3;
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

        public static void ReadEnemyAttacks(int EnemyAttacksID_List)
        {
            GetSelectedEnemyAttacksData = new EnemyAttacksData();
            int selectedEnemyAttacksOffset = EnemyAttacksDataOffset + (EnemyAttacksID_List * 20);
            OffsetToEnemyAttacksSelected = selectedEnemyAttacksOffset;

            GetSelectedEnemyAttacksData.MagicID = (ushort)(BitConverter.ToUInt16(Kernel, selectedEnemyAttacksOffset + 2));
            selectedEnemyAttacksOffset += 6;
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
            selectedEnemyAttacksOffset += 1;
            GetSelectedEnemyAttacksData.Status1 = Kernel[selectedEnemyAttacksOffset++];
            selectedEnemyAttacksOffset += 1;
            GetSelectedEnemyAttacksData.Status2 = Kernel[selectedEnemyAttacksOffset++];
            GetSelectedEnemyAttacksData.Status3 = Kernel[selectedEnemyAttacksOffset++];
            GetSelectedEnemyAttacksData.Status4 = Kernel[selectedEnemyAttacksOffset++];
            GetSelectedEnemyAttacksData.Status5 = Kernel[selectedEnemyAttacksOffset++];
        }

        public static void ReadBlueMagic(int BlueMagicID_List)
        {
            GetSelectedBlueMagicData = new BlueMagicData();
            int selectedBlueMagicOffset = BlueMagicDataOffset + (BlueMagicID_List * 16);
            OffsetToBlueMagicSelected = selectedBlueMagicOffset;

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
        }

        public static void ReadBlueMagicParam(int BlueMagicParamID_List)
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
            GetSelectedBlueMagicParamData.DeathLevelCL1 = Kernel[selectedBlueMagicParamOffset++];

            GetSelectedBlueMagicParamData.Status1CL2 = Kernel[selectedBlueMagicParamOffset++];
            GetSelectedBlueMagicParamData.Status2CL2 = Kernel[selectedBlueMagicParamOffset++];
            GetSelectedBlueMagicParamData.Status3CL2 = Kernel[selectedBlueMagicParamOffset++];
            GetSelectedBlueMagicParamData.Status4CL2 = Kernel[selectedBlueMagicParamOffset++];
            GetSelectedBlueMagicParamData.Status5CL2 = Kernel[selectedBlueMagicParamOffset++];
            selectedBlueMagicParamOffset += 1;
            GetSelectedBlueMagicParamData.AttackPowerCL2 = Kernel[selectedBlueMagicParamOffset++];
            GetSelectedBlueMagicParamData.DeathLevelCL2 = Kernel[selectedBlueMagicParamOffset++];

            GetSelectedBlueMagicParamData.Status1CL3 = Kernel[selectedBlueMagicParamOffset++];
            GetSelectedBlueMagicParamData.Status2CL3 = Kernel[selectedBlueMagicParamOffset++];
            GetSelectedBlueMagicParamData.Status3CL3 = Kernel[selectedBlueMagicParamOffset++];
            GetSelectedBlueMagicParamData.Status4CL3 = Kernel[selectedBlueMagicParamOffset++];
            GetSelectedBlueMagicParamData.Status5CL3 = Kernel[selectedBlueMagicParamOffset++];
            selectedBlueMagicParamOffset += 1;
            GetSelectedBlueMagicParamData.AttackPowerCL3 = Kernel[selectedBlueMagicParamOffset++];
            GetSelectedBlueMagicParamData.DeathLevelCL3 = Kernel[selectedBlueMagicParamOffset++];

            GetSelectedBlueMagicParamData.Status1CL4 = Kernel[selectedBlueMagicParamOffset++];
            GetSelectedBlueMagicParamData.Status2CL4 = Kernel[selectedBlueMagicParamOffset++];
            GetSelectedBlueMagicParamData.Status3CL4 = Kernel[selectedBlueMagicParamOffset++];
            GetSelectedBlueMagicParamData.Status4CL4 = Kernel[selectedBlueMagicParamOffset++];
            GetSelectedBlueMagicParamData.Status5CL4 = Kernel[selectedBlueMagicParamOffset++];
            selectedBlueMagicParamOffset += 1;
            GetSelectedBlueMagicParamData.AttackPowerCL4 = Kernel[selectedBlueMagicParamOffset++];
            GetSelectedBlueMagicParamData.DeathLevelCL4 = Kernel[selectedBlueMagicParamOffset++];
        }

        public static void ReadStatPercentageAbilities(int StatPercentageAbilitiesID_List)
        {
            GetSelectedStatPercentageAbilitiesData = new StatPercentageAbilitiesData();
            int selectedStatPercentageAbilitiesOffset = StatPercentageAbilitiesDataOffset + (StatPercentageAbilitiesID_List * 8);
            OffsetToStatPercentageAbilitiesSelected = selectedStatPercentageAbilitiesOffset;

            selectedStatPercentageAbilitiesOffset += 5;
            GetSelectedStatPercentageAbilitiesData.StatToincrease = Kernel[selectedStatPercentageAbilitiesOffset++];
            GetSelectedStatPercentageAbilitiesData.IncreasementValue = Kernel[selectedStatPercentageAbilitiesOffset++];
        }

        public static void ReadRenzoFin(int RenzoFinID_List)
        {
            GetSelectedRenzoFinData = new RenzoFinData();
            int selectedRenzoFinOffset = RenzoFinDataOffset + (RenzoFinID_List * 24);
            OffsetToRenzoFinSelected = selectedRenzoFinOffset;

            GetSelectedRenzoFinData.MagicID = (ushort)(BitConverter.ToUInt16(Kernel, selectedRenzoFinOffset + 4));
            selectedRenzoFinOffset += 4 + 2;
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
       
        public static void ReadTempCharLB(int TempCharLBID_List)
        {
            GetSelectedTempCharLBData = new TempCharLBData();
            int selectedTempCharLBOffset = TempCharLBDataOffset + (TempCharLBID_List * 24);
            OffsetToTempCharLBSelected = selectedTempCharLBOffset;

            GetSelectedTempCharLBData.MagicID = (ushort)(BitConverter.ToUInt16(Kernel, selectedTempCharLBOffset + 4));
            selectedTempCharLBOffset += 4 + 2;
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

        public static void ReadShot(int ShotID_List)
        {
            GetSelectedShotData = new ShotData();
            int selectedShotOffset = ShotDataOffset + (ShotID_List * 24);
            OffsetToShotSelected = selectedShotOffset;

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
            selectedShotOffset += 1;
            GetSelectedShotData.Status2 = Kernel[selectedShotOffset++];
            GetSelectedShotData.Status3 = Kernel[selectedShotOffset++];
            GetSelectedShotData.Status4 = Kernel[selectedShotOffset++];
            GetSelectedShotData.Status5 = Kernel[selectedShotOffset++];
        }

        public static void ReadDuel(int DuelID_List)
        {
            GetSelectedDuelData = new DuelData();
            int selectedDuelOffset = DuelDataOffset + (DuelID_List * 32);
            OffsetToDuelSelected = selectedDuelOffset;

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
            GetSelectedDuelData.Button1 = Kernel[selectedDuelOffset];
            selectedDuelOffset += 2;
            GetSelectedDuelData.Button2 = Kernel[selectedDuelOffset];
            selectedDuelOffset += 2;
            GetSelectedDuelData.Button3 = Kernel[selectedDuelOffset];
            selectedDuelOffset += 2;
            GetSelectedDuelData.Button4 = Kernel[selectedDuelOffset];
            selectedDuelOffset += 2;
            GetSelectedDuelData.Button5 = Kernel[selectedDuelOffset];
            selectedDuelOffset += 2;
            GetSelectedDuelData.Status1 = Kernel[selectedDuelOffset++];
            selectedDuelOffset += 1;
            GetSelectedDuelData.Status2 = Kernel[selectedDuelOffset++];
            GetSelectedDuelData.Status3 = Kernel[selectedDuelOffset++];
            GetSelectedDuelData.Status4 = Kernel[selectedDuelOffset++];
            GetSelectedDuelData.Status5 = Kernel[selectedDuelOffset++];
        }

        public static void ReadCombine(int CombineID_List)
        {
            GetSelectedCombineData = new CombineData();
            int selectedCombineOffset = CombineDataOffset + (CombineID_List * 20);
            OffsetToCombineSelected = selectedCombineOffset;

            GetSelectedCombineData.MagicID = (ushort)(BitConverter.ToUInt16(Kernel, selectedCombineOffset + 2));
            selectedCombineOffset += 2 + 2;
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

        public static void ReadBattleItems(int BattleItemsID_List)
        {
            GetSelectedBattleItemsData = new BattleItemsData();
            BattleItemsID_List++; //skip dummy entry
            int selectedBattleItemsOffset = BattleItemsDataOffset + (BattleItemsID_List * 24);
            OffsetToBattleItemsSelected = selectedBattleItemsOffset;

            GetSelectedBattleItemsData.MagicID = (ushort)(BitConverter.ToUInt16(Kernel, selectedBattleItemsOffset + 4));
            selectedBattleItemsOffset += 4 + 2;
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
            selectedBattleItemsOffset += 2;
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

        public static void ReadSlotsSets(int SlotsSetsID_List)
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


        private static string BuildString(int index)
        {
            if (_charstable == null)
                _charstable = Chartable.Split(',');
            StringBuilder sb = new StringBuilder();
            while (true)
            {
                if (Kernel[index] == 0x00)
                    return sb.ToString();
                char c = _charstable[Kernel[index++] - 31].ToCharArray()[0];
                sb.Append(c);
            }
        }

    }
}

