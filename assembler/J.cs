using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assembler
{
    class J : AssemblyCode
    {
        string offset;
        public override void createParameters(string[] parames, int index, List<string> errorList, int line)
        {
            if (name == "j")
                createJParameters(parames, index,errorList,line);
            else if (name == "halt")
                createHaltParameters(parames, index,errorList,line);
        }
        public void createJParameters(string[] parames, int index, List<string> errorList, int line)
        {
            bool isValid = false;
            for (int i = index; i < parames.Length; i++)
            {
                if (parames[i] == "")
                    continue;
                else if (parames[i][0] == '#')
                    break;
                else
                {
                    offset = parames[i];
                }
            }
        }
        public void createHaltParameters(string[] parames, int index, List<string> errorList, int line)
        {
            bool isValid = false;
            for (int i = index; i < parames.Length; i++)
            {
                if (parames[i] == "")
                    continue;
                else if (parames[i][0] == '#')
                    break;
            }
        }
        public override void convertIntoMachineCode()
        {
            machineCode += "0000";
            machineCode += opcode();
            machineCode += Convert.ToString(0, 2).PadLeft(4, '0');
            machineCode += Convert.ToString(0, 2).PadLeft(4, '0');
            if (name == "halt")
                machineCode += Convert.ToString(0, 2).PadLeft(16, '0');
            else
            {
                try
                {
                    int off = Convert.ToInt32(offset);
                    machineCode += Convert.ToString(off, 2).PadLeft(16, '0');
                }
                catch (Exception ex)
                {
                    isComplete = false;

                }
            }
        }
        public override void completeInCompletes(List<AssemblyCode> lst, List<string> errorList, int line)
        {
            bool isThere = false;
            for (int i = 0; i < lst.Count(); i++)
            {
                if (lst[i].label == offset)
                {
                    //in if ezafe shod.agar ghablan true shode pas yani tekrarie!
                    if (isThere)
                    {
                        errorList.Add("Line " + line + " : Duplicate Label!!");
                    }
                    string tmp = Convert.ToString(i, 2).PadLeft(16, '0');
                    machineCode += tmp;
                    isThere = true;
                }
            }
            // in if ezafe shod
            if (!isThere)
            {
                errorList.Add("Line " + (line+1) + " : Label not found!!");
            }
        }
    }
}
