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
        
        public Instruction(string instruction)
        {
            opcode = new Opcode();
            this.instruction = instruction;
            this.checkValid();
        }

        private void checkValid()
        {
            
        }

        public Boolean getValid()
        {
            return this.valid;
        }

        public String getInstruction()
        {
            return this.instruction;
        }
    
    }

}
