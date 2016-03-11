using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMPARC_Project_2
{
    public class Program
    {
        private List<Instruction> instruction;
        private List<Register> registers;

        public Program(String[] program)
        {
            this.instruction = new List<Instruction>();
            //this.registers = new List<Register>();
            this.initializeInstructionArray(program);
            this.setBranchOffsets();
            this.isValid();
        }

        private void setBranchOffsets()
        {
            for (int i = 0; i < this.instruction.Count(); i++)
            {
                if (this.instruction[i].getInstruction().Equals("BNEC") || this.instruction[i].getInstruction().Equals("BC")) 
                {
                    Console.WriteLine("Found: " + this.instruction[i].getInstruction());
                    /*
                    for (int j = 0; j < this.instruction.Count(); j++)
                    {
                        
                    }
                     */
                }
            }
        }

        private void initializeInstructionArray(String[] program)
        {
            for (int i = 0; i < program.Length; i++)
            {
                this.instruction.Add(new Instruction(program[i]));
            }
        }

        private Boolean isValid()
        {
            for (int i = 0; i < instruction.Count; i++)
                if (this.instruction[i].getValid() == false)
                {
                    System.Windows.Forms.MessageBox.Show("Error at line #" + (i + 1));
                    return false;
                }
            return true;
        }
    }
}
