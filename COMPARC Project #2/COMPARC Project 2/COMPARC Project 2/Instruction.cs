using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMPARC_Project_2
{
    public class Instruction                
    {

        #region variables

        private String instructionLine;     //  complete instruction line
        private Boolean valid;              //  check if the instruction entered is valid
        private int instructionType;        //  branch instruction, load/store instruction, Arithmetic Instruction, etc.
        private Opcode opcode;

        private String instruction;         //  instruction mnemonic only

        private String rs;                  //  parameters
        private String rd;
        private String rt;
        private String offset;
        private String branchLocation;
        private String bse;
        private String immediate;

        private int lineNum;

        #endregion

        public Instruction(string instructionLine, int lineNum)
        {
            this.instructionLine = instructionLine;
            this.setInstruction();
            this.setParameters();
            this.lineNum = lineNum;
            Console.WriteLine(this.instruction + "," + this.rd + "," + this.rs + "," + this.rt + "," + this.immediate + "," + this.offset + "," + this.bse);
            this.opcode = new Opcode(this.instruction, this.rd, this.rs, this.rt, this.immediate, this.offset, this.bse, this.lineNum);
            this.opcode.setParameters();
            if(this.opcode.getValid()){
                this.valid = true;
                Console.WriteLine("VALID!!!");
            }
        }

        #region setters
        // sets the instruction
        private void setInstruction()
        {
            String[] splitIns = this.instructionLine.Split();

            if (splitIns[0].Contains(':'))
            {
                Console.WriteLine("This instruction has a branch Location!");
                this.branchLocation = splitIns[0].TrimEnd(':');
                this.instruction = splitIns[1];
            }
            //  first word should contain the instruction
            if (checkExistingInstructions(splitIns[0].ToUpper()))   //if the 1st word in the line is an instruction, this.instruction gets the instruction
            {
                this.instruction = splitIns[0].ToUpper();
            }
            
            else //if instruction does not exist, instruction is invalid
            {
                this.valid = false;
            }
        }

        // sets the parameters depending on the instruction
        private void setParameters()    
        {
            String[]    separators = { ",", " ", "(", ")" };
            
            String[]    words = this.instructionLine.Split(separators, StringSplitOptions.RemoveEmptyEntries);  // splits the instruction line per word to an array
            //if there is a branch location, remove it first... it is already saved as offset.
            if (words[0].Contains(':'))
            {
                Console.WriteLine("Contains ':'");
                words=words.Skip(1).ToArray();
                Console.WriteLine("words now start with: " + words[0]);
                Console.WriteLine("This instruction has offset: " + this.offset);
            }

            switch (this.instruction)
            {
                case "DSUBU" : 
                case "DDIVU" :
                case "DMODU" :
                case "SLT" :
                case "SELNEZ":  this.rd = words[1]; this.rs = words[2]; this.rt = words[3]; this.offset = null;     this.immediate = null;     this.bse = null;     break;
                case "BNEC":    this.rd = null;     this.rs = words[1]; this.rt = words[2]; this.offset = words[3]; this.immediate = null;     this.bse = null;     break;
                case "LD" :
                case "SD":      this.rd = null;     this.rs = null;     this.rt = words[1]; this.offset = words[2]; this.immediate = null;     this.bse = words[3]; break;
                case "DADDIU":  this.rd = null;     this.rs = words[2]; this.rt = words[1]; this.offset = null;     this.immediate = words[3]; this.bse = null;     break;
                case "BC":      this.rd = null;     this.rs = null;     this.rt = null;     this.offset = words[1]; this.immediate = null;     this.bse = null;     break;
            }

            // check if parameters are valid here *** gawin pa ba to?
        }

        public void setOffset(String offset)
        {
            this.offset = offset;
        }

        public void setBranchLocation(String branchLocation)
        {
            this.branchLocation = branchLocation;
        }

        #endregion

        #region getters
        
        public String getInstructionLine()
        {
            return this.instructionLine;
        }

        public Boolean getValid()
        {
            return this.valid;
        }

        public int getInstructionType()
        {
            return this.instructionType;
        }

        public Opcode getOpCode()
        {
            return this.opcode;
        }

        public String getInstruction() 
        {
            return this.instruction;
        }

        public String getRS()
        {
            return this.rs;
        }

        public String getRD()
        {
            return this.rd;
        }

        public String getRT()
        {
            return this.rt;
        }

        public String getOffset()
        {
            return this.offset;
        }

        public String getImmediate()
        {
            return this.immediate;
        }

        public String getBse()
        {
            return this.bse;
        }

        public String getBranchLocation()
        {
            return this.branchLocation;
        }

        #endregion 

        public Boolean checkExistingInstructions(string instruction)
        {
            switch (instruction)
            {
                case "DSUBU":
                case "DDIVU":
                case "DMODU":
                case "SLT":
                case "SELNEZ":
                case "BNEC":
                case "LD":
                case "SD":
                case "DADDIU":
                case "BC": return true; break;
                default: return false;
            }
        }
    
    }

}
