using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assembler
{
    class AssemblyCode
    {
        public string type;
        public string label;
        public string name;
        public string machineCode;
        public bool isComplete = true;
        public virtual void createParameters(string[] parames , int index , List<string> errorList,int line)
        {

        }
        public virtual void convertIntoMachineCode()
        {

        }
        public virtual void completeInCompletes(List<AssemblyCode> lst, List<string> errorList, int line)
        {

        }
        public string opcode()
        {
            if (name == "add")
                return "0000";
            else if (name == "sub")
                return "0001";
            else if (name == "slt")
                return "0010";
            else if (name == "or")
                return "0011";
            else if (name == "nand")
                return "0100";
            else if (name == "addi")
                return "0101";
            else if (name == "slti")
                return "0110";
            else if (name == "ori")
                return "0111";
            else if (name == "lui")
                return "1000";
            else if (name == "lw")
                return "1001";
            else if (name == "sw")
                return "1010";
            else if (name == "beq")
                return "1011";
            else if (name == "jalr")
                return "1100";
            else if (name == "j")
                return "1101";
            else if (name == "halt")
                return "1110";
            else
                return "1111";

        }
    }
}
