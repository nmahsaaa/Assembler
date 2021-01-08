using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assembler
{
    class InstructionMemory
    {
        public List<AssemblyCode> inst= new List<AssemblyCode>();
        public InstructionMemory()
        {

        }
        public void register_racodnize(AssemblyCode ac, Ins ins, RegisterFile regs, ControlUnit control)
        {
            string opcode = ac.machineCode.Substring(4, 4);
            opcode_r(opcode, control);
            string Rs = ac.machineCode.Substring(8,4);
            int s = bd(Rs);
            ins.rs = regs.reg[s];
            regs.reg_cont[s] = true;
            if (control.r_type == 1)
            {
                string Rt = ac.machineCode.Substring(12, 4);
                long t = bd(Rt);
                ins.rt = regs.reg[t];
                regs.reg_cont[t] = true;

                string Rd=ac.machineCode.Substring(16, 4);
                ins.rd = bd(Rd);
                regs.reg_cont[ins.rd] = true;
            }
            else if (control.im_c == 1) // I_Type
            {
                string Rt = ac.machineCode.Substring(12, 4);
                ins.rt = bd(Rt);
                regs.reg_cont[ins.rt] = true;

                string imm = ac.machineCode.Substring(16, 16);
                ins.imm = bd(imm);
            }
            else if (control.j==1) //JType
            {
                string imm = ac.machineCode.Substring(16, 16);
                ins.imm = bd(imm);
            }
        }
        void opcode_r(string op, ControlUnit control)
        {
            if (op == "0000")
            {
                control.r_type = 1;
                control.alu_c = "add";
                control.mem_r = 0;
                control.im_c = 0;
                control.jr_pc = 0; // ~(pc+1)
                control.write_b = 1;
                control.j = 0;
                control.ins_c = 1; //baraye halt
            }
            else if (op == "0001")
            {
                control.r_type = 1;
                control.alu_c = "sub";
                control.mem_r = 0;
                control.im_c = 0;
                control.jr_pc = 0;
                control.write_b = 1;
                control.j = 0;
                control.ins_c = 1;
            }
            else if (op == "0010")
            {
                control.r_type = 1;
                control.alu_c = "slt";
                control.mem_r = 0;
                control.im_c = 0;
                control.jr_pc = 0;
                control.write_b = 1;
                control.j = 0;
                control.ins_c = 1;
            }
            else if (op == "0011")
            {
                control.r_type = 1;
                control.alu_c = "or";
                control.mem_r = 0;
                control.im_c = 0;
                control.jr_pc = 0;
                control.j = 0;
                control.ins_c = 1;
            }
            else if (op == "0100")
            {
                control.r_type = 1;
                control.alu_c = "nand";
                control.mem_r = 0;
                control.im_c = 0;
                control.jr_pc = 0;
                control.write_b = 1;
                control.j = 0;
                control.ins_c = 1;
            }
            else if (op == "0101") // azinja I
            {
                control.im_c = 1;
                control.alu_c = "add";
                control.mem_r = 0;
                control.r_type = 0;
                control.jr_pc = 0;
                control.write_b = 1;
                control.j = 0;
                control.ins_c = 1;
            }
            else if (op == "0111")
            {
                control.im_c = 1;
                control.alu_c = "or";
                control.mem_r = 0;
                control.r_type = 0;
                control.jr_pc = 0;
                control.write_b = 1;
                control.j = 0;
                control.ins_c = 1;
            }
            else if (op == "0110")
            {
                control.im_c = 1;
                control.alu_c = "slt";
                control.mem_r = 0;
                control.r_type = 0;
                control.jr_pc = 0;
                control.write_b = 1;
                control.j = 0;
                control.ins_c = 1;
            }
            else if (op == "1000")
            {
                control.im_c = 1;
                control.alu_c = "lui";
                control.mem_r = 0;
                control.r_type = 0;
                control.jr_pc = 0;
                control.write_b = 1;
                control.j = 0;
                control.ins_c = 1;
            }
            else if (op == "1001")
            {
                control.im_c = 1;
                control.alu_c = "lw";
                control.mem_r = 1;
                control.r_type = 0;
                control.jr_pc = 0;
                control.write_b = 1;
                control.j = 0;
                control.ins_c = 1;
            }
            else if (op == "1010")
            {
                control.im_c = 1;
                control.alu_c = "sw";
                control.mem_r = 1;
                control.r_type = 0;
                control.jr_pc = 0;
                control.write_b = 0;
                control.j = 0;
                control.ins_c = 1;
            }
            else if (op == "1011")
            {
                control.im_c = 1;
                control.alu_c = "beq";
                control.mem_r = 0;
                control.r_type = 0;
                control.jr_pc = 0;
                control.write_b = 0;
                control.j = 0;
                control.ins_c = 1;
            }
            else if (op == "1100")
            {
                control.im_c = 1;
                control.alu_c = "jalr";
                control.mem_r = 0;
                control.r_type = 0;
                control.jr_pc = 1;
                control.write_b = 0;
                control.j = 0;
                control.ins_c = 1;
            }
            else if (op == "1101")
            {
                control.im_c = 0;
                control.alu_c = "j";
                control.mem_r = 0;
                control.r_type = 0;
                control.jr_pc = 1;
                control.write_b = 0;
                control.j = 1;
                control.ins_c = 1;
            }
            else if (op == "1110")
            {
                control.im_c = 0;
                control.alu_c = "halt";
                control.mem_r = 0;
                control.r_type = 0;
                control.jr_pc = 1;
                control.write_b = 0;
                control.j = 1;
                control.ins_c = 0;
            }
        }
        int bd(string q)
        {
            int re = 0;
            for (int i = 0; i < q.Length; i++)
            {
                if (q[i] == '1')
                {
                    re = Convert.ToInt32(re + Math.Pow(2, (q.Length-1) - i));
                }
            }
            return re;
        }
    /*    string binary(int a)
        {
            string result = "";
            int p = 0;
            while (a >= 2)
            {
                p = a % 2;
                result = p + result;
                a /= 2;
            }
            result = a + result;
            while (result.Length < 32)
                result = "0" + result;
            return result;
        }*/
    }
}
