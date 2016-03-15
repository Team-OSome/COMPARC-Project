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
        private List<Cycle> cycle;
        //private List<Register> registers;
        private int tempOffset;
        private Boolean hasBranch = false;
        private Boolean instructionsValid = true;

        public Program(String[] program)
        {
            this.instruction = new List<Instruction>();
            //this.registers = new List<Register>();
            this.initializeInstructionArray(program);
            this.isValid();

            if (this.instructionsValid && hasBranch)
            {
                this.setBranchOffsets();
                showAllOpcodes();
            }
        }

        private void setBranchOffsets()
        {
            for (int i = 0; i < this.instruction.Count(); i++)
            {
                if (this.instruction[i].getInstruction().Equals("BNEC") || this.instruction[i].getInstruction().Equals("BC")) 
                {
                    Console.WriteLine("Found: " + this.instruction[i].getInstruction()+ ", i="+i);
                    Console.WriteLine(this.instruction[i].getInstruction() + " branches to" + this.instruction[i].getOffset());
                    this.hasBranch = true;

                    for (int j = 0; j < this.instruction.Count(); j++)
                    {
                        Console.WriteLine("branch location of line "+j+ ": "+this.instruction[j].getBranchLocation());
                        if (this.instruction[i].getOffset().Equals(this.instruction[j].getBranchLocation()))
                        {
                            Console.WriteLine("i= " + i);
                            Console.WriteLine("j= " + j);
                            tempOffset = j - i -1;
                            Console.WriteLine("tempOffset= " + tempOffset);
                            this.instruction[i].setOffset(tempOffset.ToString());
                            this.instruction[i].getOpcode().setOffset(tempOffset.ToString());
                            this.instruction[i].getOpcode().addOpcodeString(this.instruction[i].getOpcode().getOffsetO());
                        }
                            
                    }
                     
                }
            }
        }

        private void initializeInstructionArray(String[] program)
        {
            for (int i = 0; i < program.Length; i++)
            {
                this.instruction.Add(new Instruction(program[i],i));
                if (!(this.instruction[i].getValid()))
                    this.instructionsValid = false;
                else if (this.instruction[i].getInstruction().Equals("BNEC") || this.instruction[i].getInstruction().Equals("BC"))
                    this.hasBranch = true;
                
            }
        }

        private Boolean isValid() //checks if all lines are valid
        {
            for (int i = 0; i < instruction.Count; i++)
                if (this.instruction[i].getValid() == false)
                {
                    System.Windows.Forms.MessageBox.Show("Error at line #" + (i + 1));
                    return false;
                }
            return true;
        }

        private void showAllOpcodes() //checks if all lines are valid
        {
            
            for (int i = 0; i < instruction.Count; i++)
            {
                Console.WriteLine(this.instruction[i].getOpcode().getOpcodeString());
            }
             
        }

        public String[][] getPipelineMap()
        {

        }
    }
}
