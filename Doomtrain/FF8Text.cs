using System;
using System.Text;
using System.Runtime.InteropServices;

namespace Doomtrain
{
    class FF8Text
    {
        public FF8Text()
        {
            InitializeCharTable();
        }

        static string[] _charstable;
        private static readonly string Chartable =
        @" , ,1,2,3,4,5,6,7,8,9,%,/,:,!,?,…,+,-,=,*,&,「,」,(,),·,.,,,~,“,”,‘,#,$,',_,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,À,Á,Â,Ä,Ç,È,É,Ê,Ë,Ì,Í,Î,Ï,Ñ,Ò,Ó,Ô,Ö,Ù,Ú,Û,Ü,Œ,ß,à,á,â,ä,ç,è,é,ê,ë,ì,í,î,ï,ñ,ò";
        private static byte[] buffer;

        private void InitializeCharTable()
        {
            //CHANGEME
            if (_charstable == null)
                _charstable = Chartable.Split(',');

            //REMEMBER TO DELETE THIS:
            SecretSquirrelClass.Squirrel();
        }

        internal static string BuildString(int index)
        {
            //return null; //needs to be deleted later
            StringBuilder sb = new StringBuilder();
            while (true)
            {
                if (buffer[index] == 0x00)
                    break;
                char c = _charstable[buffer[index++] - 31].ToCharArray()[0];
                sb.Append(c);
            }
                /* DELETE ME */ Console.WriteLine(sb.ToString());
            return sb.ToString();
        }

        internal static byte[] Cipher(string _in)
        {
            //SUBJECT TO CHANGE
            if (_in.Length == 0)
                return null;
            buffer = new byte[_in.Length];
            for (int i = 0; i != buffer.Length; i++)
                buffer[i] = (byte)(LocateChar((byte)_in[i]) + 31);
            return buffer;
        }

        internal static uint LocateChar(byte a)
        {
            //AS ABOVE
            uint index = 0;
            while (true)
            {
            again:
                if (index >= _charstable.Length)
                    break;
                if (_charstable[index].Length == 0)
                {
                    index++;
                    goto again;
                }
                if ((char)a == _charstable[index][0])
                    return index;
                index++;
            }
            return 0;
        }

        internal static void SetKernel(byte[] b) => buffer = b; //Okay, that's what Delta look like
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
