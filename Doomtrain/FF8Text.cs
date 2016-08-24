using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doomtrain
{
    class FF8Text
    {

        static string[] _charstable;
        private static readonly string Chartable =
        @" , ,1,2,3,4,5,6,7,8,9,%,/,:,!,?,…,+,-,=,*,&,「,」,(,),·,.,,,~,“,”,‘,#,$,',_,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,À,Á,Â,Ä,Ç,È,É,Ê,Ë,Ì,Í,Î,Ï,Ñ,Ò,Ó,Ô,Ö,Ù,Ú,Û,Ü,Œ,ß,à,á,â,ä,ç,è,é,ê,ë,ì,í,î,ï,ñ,ò";
        private static byte[] buffer;

        internal static void InitializeCharTable()
        {

        }

        internal static string BuildString(int index)
        {
            if (_charstable == null)
                _charstable = Chartable.Split(',');
            StringBuilder sb = new StringBuilder();
            while (true)
            {
                if (buffer[index] == 0x00)
                    return sb.ToString();
                char c = _charstable[buffer[index++] - 31].ToCharArray()[0];
                sb.Append(c);
            }
        }

        internal static void SetKernel(byte[] b) // => 
        {
            buffer = b;
        }
    }

    internal class SecretSquirrelClass
    {
        public string Squirrel()
        {
            return "Squirrel";
        }
    }


}
