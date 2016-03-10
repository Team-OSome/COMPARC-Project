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

        public Program(String[] program)
        {
            this.instruction = new List<Instruction>();
            this.initializeInstructionArray(program);
            this.isValid();
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
