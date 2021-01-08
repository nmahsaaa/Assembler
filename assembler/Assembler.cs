using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace assembler
{
    class Assemblerr
    {

        public string assemblyCodee;
        string machineCode;
        public List<AssemblyCode> assemblyCodes = new List<AssemblyCode>();
        public List<string> errorList = new List<string>();
        public void createAssemblyCodes()
        {
            using (StringReader reader = new StringReader(assemblyCodee))
            {
                string line;
                int i = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    createAssemblyCode(line,i);
                    i++;
                }
            }
            int j=0;
            foreach (AssemblyCode ac in assemblyCodes)
            {
                if (!ac.isComplete)
                    ac.completeInCompletes(assemblyCodes,errorList,j);
                j++;
            }
        }
        public void createAssemblyCode(string str,int line)
        {
            string[] parames=str.Split(' ');
            AssemblyCode ac =null;
            string label="";
            bool error = false;
            for (int i = 0; i < parames.Length; i++)
            {
                if (parames[i] == "")
                    continue;
                if (parames[i] == "add" || parames[i] == "sub" 
                    || parames[i] == "slt" || parames[i] == "or" 
                    || parames[i] == "nand")
                {
                    ac = new R();
                    ac.label = label;
                    ac.name = parames[i];
                    ac.type = "R";
                    ac.createParameters(parames, i+1,errorList,line);
                    break;
                }
                else if (parames[i] == "addi" || parames[i] == "lui" 
                    || parames[i] == "slti" || parames[i] == "ori" 
                    || parames[i] == "lw" || parames[i] == "sw" 
                    || parames[i] == "beq" || parames[i] == "jalr")
                {
                    ac = new I();
                    ac.label = label;
                    ac.name = parames[i];
                    ac.type = "I";
                    ac.createParameters(parames, i + 1,errorList,line);
                    break;
                }
                else if (parames[i] == "j" || parames[i] == "halt")
                {
                    ac = new J();
                    ac.label = label;
                    ac.name = parames[i];
                    ac.type = "J";
                    ac.createParameters(parames, i + 1,errorList,line);
                    break;
                }
                else if (parames[i] == ".fill" || parames[i] == ".space")
                {
                    ac = new Directives();
                    ac.label = label;
                    ac.name = parames[i];
                    ac.type = "d";
                    ac.createParameters(parames, i + 1,errorList,line);
                    break;
                }
                else
                {
                    if (label == "")
                        label = parames[i];
                    else
                    {
                        errorList.Add("Line " + line + " : The assembly name is incorrect!!");
                        error = true;
                        break ;
                    }
                }
            }
            if (!error)
            {
                assemblyCodes.Add(ac);
                ac.convertIntoMachineCode();
            }
        }
    }
}
