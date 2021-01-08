using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assembler
{
    class ControlUnit
    {
        public int r_type;
        public int jr_pc ;
        public int ins_c;
        public int pc ;
        public int mem_r;
        public int im_c;
        public int write_b;
        public int j;
        public string alu_c;
        public ControlUnit()
        {
            ins_c = 1;
            pc = 0;
            jr_pc = 0;
        }
        public void pc_counter()
        {
            if (ins_c == 1)
                pc++;
        }
    }
}
