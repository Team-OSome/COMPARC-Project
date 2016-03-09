using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMPARC_Project_2
{
    public class Instruction                
    {
        private String instructionLine;
        private Boolean valid;              // check if the instruction entered is valid
        private int instructionType;        //  branch instruction, load/store instruction, Arithmetic Instruction, etc.
        private Opcode opcode;

        private String instruction;
        private String rs;
        private String rd;
        private String rt;
        private String offset;
        private String immediate;
        
        public Instruction(string instructionLine)
        {
            //opcode = new Opcode();
            this.instructionLine = instructionLine;
            this.setInstruction();
            this.setInstructionIndex();
        }

        
        public void setInstruction()
        {
            String[] splitIns = this.instructionLine.Split();

            //  first word should contain the instruction
            if (checkExistingInstructions(splitIns[0].ToUpper()))
            {
                this.instruction = splitIns[0].ToUpper();
            }
            else
            {
                this.valid = false;
            }
        }

        public void setInstructionIndex()
        {
            String[] splitIns = this.instructionLine.Split();
            String insIndex = "";

            //second word/s should contain the registers
            //first all the words after the intruction should be concatenated before splitting
            for (int i = 0; i < splitIns.Length; i++)
            {
                insIndex += insIndex + splitIns[i];
            }

            Console.WriteLine(insIndex);

            //depending on the instruction split the instruction index to the indexes that are used
        }

        public Boolean checkExistingInstructions(string instruction)
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

        public Boolean getValid()
        {
            return this.valid;
        }
    
    }

}
