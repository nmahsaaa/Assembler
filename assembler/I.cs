using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assembler
{
    class I : AssemblyCode
    {
        int rt;
        int rs;
        int imm;
        string offset;
        public override void createParameters(string[] parames, int index, List<string> errorList, int line)
        {
            if (name == "lui")
                createLuiParameters(parames, index,errorList,line);
            else if (name == "jalr")
                createJalrParameters(parames, index , errorList,line);
            else
                createOtherParameters(parames, index,errorList,line);
        }
        public void createLuiParameters(string[] parames, int index, List<string> errorList, int line)
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
                            rt = Convert.ToInt32(registers[0]);
                            imm = Convert.ToInt32(registers[1]);
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
        public void createJalrParameters(string[] parames, int index, List<string> errorList, int line)
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
                            rt = Convert.ToInt32(registers[0]);
                            rs = Convert.ToInt32(registers[1]);
                            isValid = true;
                        }
                        catch (Exception ex)
                        {
                            errorList.Add("Line " + line + " : Format is incorrect!!");
                        }
                    }
                }
            }
        }
        public void createOtherParameters(string[] parames, int index, List<string> errorList, int line)
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
                            rt = Convert.ToInt32(registers[0]);
                            rs = Convert.ToInt32(registers[1]);
                            if(name=="addi" || name=="ori" || name=="slti")
                                imm = Convert.ToInt32(registers[2]);
                            else
                                offset = registers[2];
                            isValid = true;
                        }
                        catch (Exception ex)
                        {
                            errorList.Add("Line " + line + " : Format is incorrect!!");
                        }
                    }
                }
            }
        }
        public override void convertIntoMachineCode()
        {
            machineCode += "0000";
           // if (opcode() == "1111")

            machineCode += opcode();
            machineCode += Convert.ToString(rs, 2).PadLeft(4, '0');
            machineCode += Convert.ToString(rt, 2).PadLeft(4, '0');
            if (name == "addi" || name == "ori" || name == "slti" || name == "lui" || name == "jalr")
                machineCode += Convert.ToString(imm, 2).PadLeft(16, '0');
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
        public override void completeInCompletes(List<AssemblyCode> lst,List<string> errorList, int line)
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
                    string tmp2=tmp + machineCode;
                    machineCode = tmp2;
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
