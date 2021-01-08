using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assembler
{
    class R : AssemblyCode
    {
        int rd;
        int rs;
        int rt;
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
                    string[] registers = parames[i].Split(',');
                    if (registers.Length == 3)
                    {
                        try
                        {
                            rd = Convert.ToInt32(registers[0]);
                            rs = Convert.ToInt32(registers[1]);
                            rt = Convert.ToInt32(registers[2]);
                            isValid = true;
                        }
                        catch (Exception ex)
                        {
                            errorList.Add("Line " + (line+1) + " : Format is incorrect!!");
                        }
                    }
                }
            }
        }
        public override void convertIntoMachineCode()
        {   
            machineCode += "0000";
            machineCode += opcode();
            machineCode += Convert.ToString(rs, 2).PadLeft(4, '0');
            machineCode += Convert.ToString(rt, 2).PadLeft(4, '0');
            machineCode += Convert.ToString(rd, 2).PadLeft(4, '0');
            machineCode += "000000000000";
        }

    }
}
