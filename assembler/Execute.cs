using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assembler
{
    class Execute
    {
        public int rd;
        public int rs;
        public int rt;
        public int imm;
        public Execute(Ins ins, ControlUnit control)
        {
            this.rs = ins.rs;
            this.rt = ins.rt;
            if (control.r_type==1)
                this.rd = ins.rd;
            if (control.im_c==1 || control.j==1)
                this.imm = ins.imm;
        }
        public int exe(ControlUnit control, Memory data, RegisterFile reg)
        {
            if (control.alu_c == "halt")
                return 0;
            if (control.r_type == 1)
            {
                if (control.alu_c == "add")
                    return rs + rt;
                if (control.alu_c == "sub")
                    return rs - rt;
                if (control.alu_c == "slt")
                {
                    if (rs - rt >= 0)
                        return 0;
                    return 1;
                }
                if (control.alu_c == "or")
                    return rs | rt;
                if (control.alu_c == "nand")
                {
                    return ~(rs & rt);
                }


            }
            if (control.im_c == 1)
            {
                if (control.alu_c == "add")
                {
                    return rs + imm;
                }
                if (control.alu_c == "or")
                {
                    return rs | imm;
                }
                if (control.alu_c == "slt")
                {
                    if (rs - imm >= 0)
                        return 0;
                    return 1;
                }
                if (control.alu_c == "lui")
                {
                    return imm << 16;
                }
                if (control.alu_c == "lw")
                {
                    data.mem_c();
                    return data.mem[rs + imm];
                }
                if (control.alu_c == "sw")
                {
                    data.mem_c();
                    data.mem[rs + imm] = reg.reg[rt];
                    return 0;
                }
                if (control.alu_c == "beq")
                {
                    if (rs == reg.reg[rt])
                    {
                        control.jr_pc = 1;
                        control.pc = control.pc + 1 + imm;
                    }
                    return 0;
                }
                if (control.alu_c == "jalr")
                {
                    reg.reg[rt] = control.pc + 1;
                    control.pc = rs;
                    return 0;
                }
            }
            if (control.j==1)
            {
                if (control.alu_c == "j")
                {
                    control.pc = imm;
                    return 0;
                }
            }
            return 0;
        }
    }
}
