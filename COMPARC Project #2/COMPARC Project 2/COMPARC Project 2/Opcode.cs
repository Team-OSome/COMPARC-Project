using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace COMPARC_Project_2
{  
    
    public class Opcode
    {
        private char opcodeType;     //  J-type, I-type, etc.
        private String opcodeString="";

        private String instruction;
        private String rs;
        private String rd;
        private String rt;
        private String offset;
        private int offsetDec;
        private String immediate;
        private String bse;

        //these are the Opcode versions
        private String instructionO;
        private String rsO;
        private String rdO;
        private String rtO;
        private String offsetO;
        private int offsetDecO;
        private String immediateO;
        private String bseO;

        public Opcode(String instruction, String rd, String rs, String rt, String immediate, String offset, String bse)
        {
            this.instruction = instruction;
            this.rd = rd;
            this.rs = rs;
            this.rt = rt;
            this.immediate = immediate;
            this.offset = offset;
            this.bse = bse;

            this.opcodeType= getInstructionType(this.instruction);
            this.instructionO = getInstructionOpcode(this.instruction);

            this.opcodeString+= instructionO + " ";

            if(opcodeType.Equals('R'))
            {

            }
            if(opcodeType.Equals('I'))
            {
                if (this.instruction == "BNEC" || this.instruction == "DADDIU") 
                {

                }
            }
            if(opcodeType.Equals('J'))
            {
                this.offsetDec = Int32.Parse(offset, System.Globalization.NumberStyles.HexNumber);
                this.offsetO = Convert.ToString(offsetDec, 2);                
                this.opcodeString += offsetO.PadLeft(26, '0');
            }
        }

        public char getInstructionType(String instruction)
        {
            switch (instruction)
            {
                case "DSUBU": 
                case "DDIV":
                case "DMODU":
                case "SLT":
                case "SELENEZ": return 'R';
                case "BNEC":
                case "LD":
                case "SD":
                case "DADDIU": return 'I';
                case "BC": return 'J';
                default: return '0';
            }
        }

        public String getInstructionOpcode(String instruction)
        {
            switch (instruction)
            {
                case "DSUBU":
                case "DDIV":
                case "DMODU":
                case "SLT":
                case "SELENEZ": return "000000";
                case "BNEC": return "011000";
                case "LD": return "110111";
                case "SD": return "111111";
                case "DADDIU": return "011001";
                case "BC": return "110010";
                default: return "";
            }
        }

    }
}
