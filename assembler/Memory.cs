using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assembler
{
    class Memory
    {
        private int j=0 ;
        public int []mem= new int[16000];
        public int mem_count;

        public float mem_percent;
        public Memory()
        {
            mem[0] = -1;
            mem[98] = 100;
            mem_percent = 0;
        }

        public void mem_c()
        {
            mem_count++;
            mem_percent += (mem_count / 16000) * 100; 

           /* bool flage = false;
            for (int i =0; i<16000 ; i++)
            {
                if (mem_count[i]==-1)
                    break;
                if (mem_count[i] == a)
                    flage = true;
                if (mem_count[i] != a)
                    mem_percent++;
            }
            if (!flage)
            {
                mem_count[j++]=a;
                mem_count[j] = -1;
                mem_percent++;
            }
            */

        }
    }
}
