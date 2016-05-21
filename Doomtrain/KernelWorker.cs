using System;
using System.Text;

namespace Doomtrain
{
    class KernelWorker
    {
        public static byte[] Kernel;
        public static int MagicDataOffset = -1;
        public static int OffsetToMagicSelected = -1;

        public static int GFDataOffset = -1;
        public static int OffsetToGFSelected = -1;

        public static MagicData GetSelectedMagicData;
        public static GFData GetSelectedGFData;

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
            CharacterLimitBreakes = 19 << 2,
            BlueMagic = 20 << 2,
            UNKNOWN1 = 21 << 2,
            Shot_Irvine = 22 << 2,
            Duel_Zell = 23 << 2,
            UNKNOWN2 = 24 << 2,
            RinoaLimit1 = 25 << 2,
            RinoaLimit2 = 26 << 2,
            UNKNOWN3 = 27 << 2,
            UNKNOWN4 = 28 << 2,
            Devour = 29 << 2,
            UNKNOWN5 = 30 << 2,
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

        public struct MagicData
        {
            public string OffsetName;
            public string SpellDescription;
            public UInt16 MagicID;
            public UInt16 Unknown1;
            public byte SpellPower;
            public byte Unknown2;
            public byte DefaultTarget;
            public byte Unknown3;
            public byte DrawResist;
            public byte HitCount;
            //public Element element;
            public byte Element;
            public byte Unknown4;
            public byte Status1;
            public byte Status2;
            public byte Status3;
            public byte Status4;
            public byte Status5;
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
            public byte[] Unknown6;
        }

        public struct GFData
        {
            public UInt16 GFMagicID;
            public byte GFPower;
            public byte GFHP;
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
        }

        public static void UpdateVariable_Magic(int index, object variable)
        {
            if (!mainForm._loaded || Kernel == null)
                return;
            switch (index)
            {
                case 2:
                {
                    UshortToKernel(Convert.ToUInt16(variable),4, (byte)Mode.Mode_Magic); //MagicID
                    return;
                }
                case 3:
                    {
                        Kernel[OffsetToMagicSelected+8] = Convert.ToByte(variable); //SpellPower
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
                        Kernel[OffsetToMagicSelected + 16] = Convert.ToByte(variable); //Status
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
                        Kernel[OffsetToMagicSelected + 32] = Convert.ToByte(variable); //STATUS ATTACK Modifier
                        return;
                    }

                case 17:
                    {
                        Kernel[OffsetToMagicSelected + 13] = Convert.ToByte(variable); //HIT [It's good, leave it]
                        return;
                    }

                case 18:
                    {
                        Kernel[OffsetToMagicSelected + 33] = Convert.ToByte(variable); //Status Attack Variable
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
                        Array.Copy(temp,0, Kernel, OffsetToMagicSelected+38,2);
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
                        Kernel[OffsetToMagicSelected + 36] = Convert.ToByte(variable); //Status Attack Value
                        return;
                    }

                case 24:
                    {
                        Kernel[OffsetToMagicSelected + 37] = Convert.ToByte(variable); //Status Defense Value
                        return;
                    }

                default:
                    return;
            }

        }

        public static void UpdateVariable_GF(int index, object variable, byte AbilityIndex = 0)
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
                        Kernel[OffsetToGFSelected + 20] = Convert.ToByte(variable); //GFHP
                        return;
                case 3:
                        Kernel[OffsetToGFSelected + 130] = Convert.ToByte(variable); //Power Mod
                        return;
                case 4:
                        Kernel[OffsetToGFSelected + 131] = Convert.ToByte(variable); //Level Mod
                        return;
                case 5:
                    Kernel[OffsetToGFSelected + 30 + (AbilityIndex*4)] = Convert.ToByte(variable);
                    return;

                default:
                    return;
            }
        }

        /// <summary>
        /// This is for MagicID list
        /// </summary>
        /// <param name="a"></param>
        /// <param name="add"></param>
        private static void UshortToKernel(ushort a, int add, byte mode)
        {
            byte[] magicIdBytes = BitConverter.GetBytes(a+1);
            if(mode == (byte)Mode.Mode_Magic)
                Array.Copy(magicIdBytes, 0, Kernel, OffsetToMagicSelected + add, 2);
            else if (mode == (byte) Mode.Mode_GF)
                Array.Copy(magicIdBytes, 0, Kernel, OffsetToGFSelected + add, 2);
            else
                return;
        }

        enum Mode : byte
        {
            Mode_Magic,
            Mode_GF
        }

        public static void ReadKernel(byte[] kernel)
        {
            Kernel = kernel;
            MagicDataOffset = BitConverter.ToInt32(Kernel, (int)KernelSections.MagicData);
            GFDataOffset = BitConverter.ToInt32(Kernel, (int)KernelSections.GFs);

        }

        public static void ReadMagic(int MagicID_List)
        {
            
            GetSelectedMagicData = new MagicData();
            MagicID_List++;
            /*
            int SelectedMagicOffset = MagicID_List == 0 ? 
                MagicDataOffset + 8 + (MagicID_List * 60) 
                : MagicDataOffset + (MagicID_List * 60);*/

            int selectedMagicOffset = MagicDataOffset + (MagicID_List * 60);
            OffsetToMagicSelected = selectedMagicOffset;

            #region UnusedNameRegion functionality. You can use it for future improvements
            GetSelectedMagicData.OffsetName = BuildString( (ushort)(
                    BitConverter.ToInt32(Kernel,(int)KernelSections.Text_Magictext) + (BitConverter.ToUInt16(Kernel, selectedMagicOffset))));
					//BELOW DOESN'T WORK?
            // GetSelectedMagicData.SpellDescription = BuildString((ushort)(
            //BitConverter.ToInt32(kernel, (int)KernelSections.Text_Magictext) + (BitConverter.ToUInt16(kernel, SelectedMagicOffset += 2))));
            //Console.WriteLine("DEBUG: {0}", GetSelectedMagicData.OffsetName);
            #endregion


            GetSelectedMagicData.MagicID = BitConverter.ToUInt16(Kernel, selectedMagicOffset += 4);
            GetSelectedMagicData.Unknown1 = BitConverter.ToUInt16(Kernel, selectedMagicOffset += 2); selectedMagicOffset += 2; //Weird one...
            GetSelectedMagicData.SpellPower = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.Unknown2 = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.DefaultTarget = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.Unknown3 = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.DrawResist = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.HitCount = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.Element = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.Unknown4 = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.Status1 = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.Status2 = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.Status3 = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.Status4 = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.Status5 = Kernel[selectedMagicOffset++];
            GetSelectedMagicData.Unknown5 = BitConverter.ToUInt16(Kernel, selectedMagicOffset += 2);
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
            GetSelectedMagicData.Unknown6 = new byte[18];
            Array.Copy(Kernel, selectedMagicOffset, GetSelectedMagicData.Unknown6, 0, 18);

        }

        public static void ReadGF(int GFID_List)
        {
            GetSelectedGFData = new GFData();
            int selectedGfOffset = GFDataOffset + (GFID_List * 132);
            OffsetToGFSelected = selectedGfOffset;

            GetSelectedGFData.GFMagicID = (ushort)(BitConverter.ToUInt16(Kernel, selectedGfOffset+4) - 1);
            selectedGfOffset += 4 + 2 + 1; //Unknown + MagicID + Unknown
            GetSelectedGFData.GFPower = Kernel[selectedGfOffset];
            selectedGfOffset += 13; //Unknown + GFPower
            GetSelectedGFData.GFHP = Kernel[selectedGfOffset];
            selectedGfOffset += 10; //Unknown+GFHP
            //AbilityRun
            GetSelectedGFData.GFAbility1 = Kernel[selectedGfOffset];
            GetSelectedGFData.GFAbility2 = Kernel[selectedGfOffset + (4*1)];
            GetSelectedGFData.GFAbility3 = Kernel[selectedGfOffset + (4*2)];
            GetSelectedGFData.GFAbility4 = Kernel[selectedGfOffset + (4*3)];
            GetSelectedGFData.GFAbility5 = Kernel[selectedGfOffset + (4 * 4)];
            GetSelectedGFData.GFAbility6 = Kernel[selectedGfOffset + (4 * 5)];
            GetSelectedGFData.GFAbility7 = Kernel[selectedGfOffset + (4 * 6)];
            GetSelectedGFData.GFAbility8 = Kernel[selectedGfOffset + (4 * 7)];
            GetSelectedGFData.GFAbility9 = Kernel[selectedGfOffset + (4 * 8)];
            GetSelectedGFData.GFAbility10 = Kernel[selectedGfOffset + (4 * 9)];
            GetSelectedGFData.GFAbility11 = Kernel[selectedGfOffset + (4 * 10)];
            GetSelectedGFData.GFAbility12 = Kernel[selectedGfOffset + (4 * 11)];
            GetSelectedGFData.GFAbility13 = Kernel[selectedGfOffset + (4 * 12)];
            GetSelectedGFData.GFAbility14 = Kernel[selectedGfOffset + (4 * 13)];
            GetSelectedGFData.GFAbility15 = Kernel[selectedGfOffset + (4 * 14)];
            GetSelectedGFData.GFAbility16 = Kernel[selectedGfOffset + (4 * 15)];
            GetSelectedGFData.GFAbility17 = Kernel[selectedGfOffset + (4 * 16)];
            GetSelectedGFData.GFAbility18 = Kernel[selectedGfOffset + (4 * 17)];
            GetSelectedGFData.GFAbility19 = Kernel[selectedGfOffset + (4 * 18)];
            GetSelectedGFData.GFAbility20 = Kernel[selectedGfOffset + (4 * 19)];
            GetSelectedGFData.GFAbility21 = Kernel[selectedGfOffset + (4 * 20)];
            //EndofAbility
            selectedGfOffset += (4*20) + 19 + 1;
            GetSelectedGFData.GFPowerMod = Kernel[selectedGfOffset];
            GetSelectedGFData.GFLevelMod = Kernel[selectedGfOffset + 1];
        }


        private static string BuildString(int index)
        {
            if (_charstable == null)
                _charstable = Chartable.Split(',');
            StringBuilder sb = new StringBuilder();
            while(true)
            {
                if (Kernel[index] == 0x00)
                    return sb.ToString();
                char c = _charstable[Kernel[index++] - 31].ToCharArray()[0];
                //sb.Append((char)kernel[++index]); //nope
                sb.Append(c);
            }
        }

        
        
    }
}
