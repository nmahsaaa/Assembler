using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace assembler
{
    public partial class Assembler : Form
    {
        public Assembler()
        {
            InitializeComponent();
        }
        string assemblyCode = "";
        RegisterFile regs= new RegisterFile();
        ControlUnit control=new ControlUnit();
        Memory data= new Memory();
        Assemblerr assm;
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open assembly File";
            theDialog.Filter = "TXT files|*.txt";
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = theDialog.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            StreamReader reader = new StreamReader(myStream);
                            assemblyCode = reader.ReadToEnd();
                            richtxtAssemblyCode.Text = assemblyCode;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void richtxtAssemblyCode_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InstructionMemory inst = new InstructionMemory();
            inst.inst = assm.assemblyCodes;
            while(control.pc <inst.inst.Count && control.ins_c == 1)
            {

                AssemblyCode line = inst.inst[control.pc];
                Ins r=new Ins();
                inst.register_racodnize(line , r , regs ,control);

               Execute alu = new Execute(r , control);

               int alu_result = alu.exe(control   , data ,regs);
               if(control.write_b==1)
               {
                    if(control.r_type == 1)
                        regs.reg[alu.rd] = alu_result;
                    if (control.im_c==1)
                        regs.reg[alu.rt] = alu_result;
                }

        if (control.jr_pc==0)
            control.pc_counter();
     }
     for(int i=0 ; i<regs.reg.Length ; i++)
     {
         richTextBox1.Text+="Register #"+i+" : "+regs.reg[i]+"\n";
     }


    float cout = 0;
   // float b = 16;
    for(int i =0 ; i<16 ; i++){
        if(regs.reg_cont[i])
            cout++;
    }

    richTextBox1.Text += "_____________________________________________" + "\n";
    richTextBox1.Text += "Register Haye Estefade Shode: " + ((cout / 16) * 100) + "%" + "\n";
    richTextBox1.Text += "Memory Estefade Shode: " + (data.mem_percent) + "%" + "\n";
    richTextBox1.Text += "Tedade Dastoor Amal ha: " + (inst.inst.Count) + "\n";


   // ui->label_2->setText( QString :: number((cout/b)*100));

   // ui->mem->setText( QString :: number(data.mem_percent * 4) +(" bytes") );
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richtxtAssemblyCode.Text = "";
            richtxtMachineCode.Text = "";
        }

        private void Assembler_Load(object sender, EventArgs e)
        {

        }

        private void customizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Assemblerr assm = new Assemblerr();
            this.assm = assm;
            assm.assemblyCodee = assemblyCode;
            assm.createAssemblyCodes();
            richtxtMachineCode.Text = "";
            if (assm.errorList.Count == 0)
            {
                foreach (AssemblyCode ac in assm.assemblyCodes)
                {
                    richtxtMachineCode.Text += ac.machineCode;
                    richtxtMachineCode.Text += "\n";
                }
            }
            label1.Text = "";
            foreach (string error in assm.errorList)
                label1.Text += error + "\n";
        }
    }
}
