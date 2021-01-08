using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assembler
{
    class RegisterFile
    {
        public int []reg=new int[16];
        public bool []reg_cont=new bool[16];
        public RegisterFile()
        {
            reg[0] = 0;
            reg[1] = 12;
            reg[2] = 2;
            reg[3] = 15;
            reg[4] = 17;
            reg[5] = 98;
            reg[6] = 65;
            reg[7] = 22;
            reg[8] = 33;
            reg[9] = 20;
            reg[10] = 125;
            reg[11] = 985;
            reg[12] = 33;
            reg[13] = 23;
            reg[14] = 1;
            reg[15] = 33;
        }
    }
}
