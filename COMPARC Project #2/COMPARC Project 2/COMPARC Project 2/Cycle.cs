using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMPARC_Project_2
{
    public class Cycle
    {
        private String IFID_IR { get; set; }
        private String IFID_NPC { get; set; }
        private String IFID_PC { get; set; }

        private String IDEX_A { get; set; }
        private String IDEX_B { get; set; }
        private String IDEX_IMM { get; set; }
        private String IDEX_NPC { get; set; }
        private String IDEX_IR { get; set; }

        private String EXMEM_ALUOutput { get; set; }
        private String EXMEM_Cond { get; set; }
        private String EXMEM_B { get; set; }
        private String EXMEM_IR { get; set; }

        private String MEMWB_LMD { get; set; }
        private String MEMWB_Range { get; set; }
        private String MEMWB_IR { get; set; }
        private String MEMWB_ALUOutput { get; set; }

        private String WB_Rn { get; set; }
        
        public Cycle()
        {
            IFID_IR = null;
            IFID_NPC = null;
            IFID_PC = null;

            IDEX_A = null;
            IDEX_B = null;
            IDEX_IMM = null;
            IDEX_NPC = null;
            IDEX_IR = null;

            EXMEM_ALUOutput = null;
            EXMEM_Cond = null;
            EXMEM_B = null;
            EXMEM_IR = null;

            MEMWB_LMD = null;
            MEMWB_Range = null;
            MEMWB_IR = null;
            MEMWB_ALUOutput = null;

            WB_Rn = null;        
        }

        public void setInstructionFetch(string opcode, string programCtr)
        {
            this.IFID_IR = opcode;
            this.IFID_NPC = programCtr;
            this.IFID_PC = this.IFID_NPC;
        }

        public void setInstructionDecode(string IFID_IR, string IFID_NPC)
        {
            this.IDEX_A = "";   // [IF/ID.IR 21..25]
            this.IDEX_B = "";   // [IF/ID.IR 16..20]
            this.IDEX_IMM = ""; // [Sign Extend + IF/ID.IR 0..15]

            this.IDEX_IR = IFID_IR;
            this.IDEX_NPC = IFID_NPC;
        }

        public void setExecution(string ALUOutput, string IDEX_B, string cond, string IDEX_IR)
        {
            this.EXMEM_ALUOutput = ALUOutput;
            this.EXMEM_B = IDEX_B;
            this.EXMEM_Cond = cond;
            this.EXMEM_IR = IDEX_IR;
        }

        public void setMemoryAccess(string LMD, string EXMEM_IR, string EXMEM_ALUOutput)
        {
            this.MEMWB_LMD = LMD ;
            //this.MEMWB_Range = ;
            this.MEMWB_IR =EXMEM_IR;
            this.MEMWB_ALUOutput = EXMEM_ALUOutput;
        }

    }
}
