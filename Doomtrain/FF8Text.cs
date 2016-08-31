using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Doomtrain
{
    class FF8Text
    {
#region CharTable
        private static Dictionary<byte, string> chartable = new Dictionary<byte, string>
        {
            {0x00, "t"},
            {0x02, "\n"},
            {0x0E, "SPECIAL CHARACTER TODO"},
            {0x20, " "},
            {0x21, "0"},
            {0x22, "1"},
            {0x23, "2"},
            {0x24, "3"},
            {0x25, "4"},
            {0x26, "5"},
            {0x27, "6"},
            {0x28, "7"},
            {0x29, "8"},
            {0x2A, "9"},
            {0x2B, "%"},
            {0x2C, "/"},
            {0x2D, ":"},
            {0x2E, "!"},
            {0x2F, "?"},
            {0x30, "…"},
            {0x31, "+"},
            {0x32, "-"},
            {0x33, "SPECIAL CHARACTER TODO"},
            {0x34, "*"},
            {0x35, "&"},
            {0x36, "SPECIAL CHARACTER TODO" },
            {0x37, "SPECIAL CHARACTER TODO" },
            {0x38, "("},
            {0x39, ")"},
            {0x3A, "SPECIAL CHARACTER TODO"},
            {0x3B, "."},
            {0x3C, ","},
            {0x3D, "~"},
            {0x3E, "SPECIAL CHARACTER TODO"},
            {0x3F, "SPECIAL CHARACTER TODO"},
            {0x40, "'"},
            {0x41, "#"},
            {0x42, "$"},
            {0x43, "`"},
            {0x44, "_"},
            {0x45, "A"},
            {0x46, "B"},
            {0x47, "C"},
            {0x48, "D"},
            {0x49, "E"},
            {0x4A, "F"},
            {0x4B, "G"},
            {0x4C, "H"},
            {0x4D, "I"},
            {0x4E, "J"},
            {0x4F, "K"},
            {0x50, "L"},
            {0x51, "M"},
            {0x52, "N"},
            {0x53, "O"},
            {0x54, "P"},
            {0x55, "Q"},
            {0x56, "R"},
            {0x57, "S"},
            {0x58, "T"},
            {0x59, "U"},
            {0x5A, "V"},
            {0x5B, "W"},
            {0x5C, "X"},
            {0x5D, "Y"},
            {0x5E, "Z"},
            {0x5F, "a"},
            {0x60, "b"},
            {0x61, "c"},
            {0x62, "d"},
            {0x63, "e"},
            {0x64, "f"},
            {0x65, "g"},
            {0x66, "h"},
            {0x67, "i"},
            {0x68, "j"},
            {0x69, "k"},
            {0x6A, "l"},
            {0x6B, "m"},
            {0x6C, "n"},
            {0x6D, "o"},
            {0x6E, "p"},
            {0x6F, "q"},
            {0x70, "r"},
            {0x71, "s"},
            {0x72, "t"},
            {0x73, "u"},
            {0x74, "v"},
            {0x75, "w"},
            {0x76, "x"},
            {0x77, "y"},
            {0x78, "z"},
            {0x79, "Ł"}, //wow, polish character?
            {0x7C, "Ä"},
            {0x88, "Ó"},
            {0x8A, "Ö"},
            {0x8E, "Ü"},
            {0x90, "ß"},
            {0x94, "ä"},
            {0xA0, "ó"},
            {0xA2, "ö"},
            {0xA6, "ü"},
            {0xA8, "Ⅷ"},
            {0xA9, "["},
            {0xAA, "]"},
            {0xAB, "[SQUARE]"},
            {0xAC, "@"},
            {0xAD, "[SSQUARE]"},
            {0xAE, "{"},
            {0xAF, "}"},
            {0xC6, "Ⅵ"},
            {0xC7, "Ⅱ"},
            {0xC9, "™"},
            {0xCA, "<"},
            {0xCB, ">"},
            {0xE8, "in"},
            {0xE9, "e "},
            {0xEA, "ne"},
            {0xEB, "to"},
            {0xEC, "re"},
            {0xED, "HP"},
            {0xEE, "l "},
            {0xEF, "ll"},
            {0xF0, "GF"},
            {0xF1, "nt"},
            {0xF2, "il"},
            {0xF3, "o "},
            {0xF4, "ef"},
            {0xF5, "on"},
            {0xF6, " w"},
            {0xF7, " r"},
            {0xF8, "wi"},
            {0xF9, "fi"},
            {0xFB, "s "},
            {0xFC, "ar"},
            {0xFE, " S"},
            {0xFF, "ag"}
        };
#endregion

        private static byte[] buffer;

        internal static string BuildString(int index)
        {
            //DEBUG:
            SecretSquirrelClass.Squirrel();
            //ENDDEBUG
            StringBuilder sb = new StringBuilder();
            while (true)
            {
                if (buffer[index] == 0x00)
                    break;
                sb.Append(chartable[buffer[index++]]);
            }
                /* DELETE ME */ Console.WriteLine(sb.ToString());
            return sb.ToString();
        }

        internal static byte[] Cipher(string _in)
        {
            //Reverse dictionary TODO
            return null;
        }

        internal static void SetKernel(byte[] b) => buffer = b; //Okay, that's what Lambda** look like
    }

    internal class SecretSquirrelClass
    {
        [DllImport("kernel32")]
        static extern bool AllocConsole();

        public static string Squirrel()
        {
            AllocConsole();
            return "Squirrel";
        }
    }
}
