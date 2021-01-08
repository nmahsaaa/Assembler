using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assembler
{
    class Directives : AssemblyCode
    {
        string offset;
        public override void createParameters(string[] parames, int index, List<string> errorList, int line)
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
        public override void convertIntoMachineCode()
        {
            if (name == ".fill")
            {
                try
                {
                    int off = Convert.ToInt32(offset);
                    machineCode += Convert.ToString(off, 2).PadLeft(32, '0');
                }
                catch (Exception ex)
                {
                    isComplete = false;
                }
            }
            else if (name==".space")
            {
                try
                {
                    int off = Convert.ToInt32(offset);
                    for (int i = 0; i < off; i++)
                        machineCode += "0";
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
                    string tmp = Convert.ToString(i, 2).PadLeft(32, '0');
                    tmp += machineCode;
                    machineCode = tmp;
                    isThere = true;
                }
            }
            // in if ezafe shod
            if (!isThere)
            {
                errorList.Add("Line " + line + " : Label not found!!");
            }
        }
    }
}
