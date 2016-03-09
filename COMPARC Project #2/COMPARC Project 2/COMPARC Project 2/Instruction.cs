using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMPARC_Project_2
{
    public class Instruction                
    {
        private String instruction;
        private Boolean valid;              // check if the instruction entered is valid
        private int instructionType;        //  branch instruction, load/store instruction, Arithmetic Instruction, etc.
        private Opcode opcode;
        private String mnemonic;
        
        public Instruction(string instruction)
        {
            opcode = new Opcode();
            this.instruction = instruction;
            this.setMnemonic();
            //this.checkValid();
        }

        private void checkValid()
        {
            if(this.existingInstructions(this.instruction))
            {
                this.valid = true;
            }
            else
            {
                this.valid = false;
            }
        }

        public Boolean getValid()
        {
            return this.valid;
        }

        public String getInstruction()
        {
            return this.instruction;
        }

        public void setMnemonic()
        {
            String[] mnemonic = instruction.Split();
            Console.WriteLine(mnemonic[0]);
            if (this.existingInstructions(mnemonic[0]))
            {
                Console.WriteLine("correct instruction");
                this.mnemonic = mnemonic[0];
            }
            else
            {
                this.valid = false;
            }

        }

        public Boolean existingInstructions(string instruction)
        {
            switch (instruction)
            {
                case "DSUBU":
                case "DDIV":
                case "DMODU":
                case "SLT":
                case "SELENEZ":
                case "BNEC":
                case "LD":
                case "SD":
                case "DADDIU":
                case "BC": return true;
                default: return false;
            }
        }
    
    }

}
