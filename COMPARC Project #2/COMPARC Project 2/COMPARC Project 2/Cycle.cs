using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COMPARC_Project_2
{
    public class Cycle
    {
        #region variables

        
        public Boolean IFID { get; private set; }
        public String IFID_IR { get; private set; }
        public String IFID_NPC { get; private set; }
        public String IFID_PC { get; private set; }
        public String IFID_instruction { get; private set; }
        public String IFID_instructionType { get; private set; }
        public String IFID_instructionLine { get; private set; }
        public string IFID_rs { get; private set; }
        public string IFID_rd { get; private set; }
        public string IFID_rt { get; private set; }
        public string IFID_bse { get; private set; }
        public string IFID_imm { get; private set; }

        public Boolean IDEX { get; private set; }
        public String IDEX_A { get; private set; }
        public String IDEX_B { get; private set; }
        public String IDEX_IMM { get; private set; }
        public String IDEX_NPC { get; private set; }
        public String IDEX_IR { get; private set; }
        public String IDEX_instruction { get; private set; }
        public String IDEX_instructionType { get; private set; }
        public String IDEX_instructionLine { get; private set; }

        public Boolean EXMEM { get; private set; }
        public String EXMEM_ALUOutput { get; private set; }
        public String EXMEM_Cond { get; private set; }
        public String EXMEM_B { get; private set; }
        public String EXMEM_IR { get; private set; }
        public String EXMEM_instruction { get; private set; }
        public String EXMEM_instructionType { get; private set; }
        public String EXMEM_instructionLine { get; private set; }

        public Boolean MEMWB { get; private set; }
        public String MEMWB_LMD { get; private set; }
        public String MEMWB_Range { get; private set; }
        public String MEMWB_IR { get; private set; }
        public String MEMWB_ALUOutput { get; private set; }
        public String MEMWB_instruction { get; private set; }
        public String MEMWB_instructionType { get; private set; }

        public Boolean WB { get; set; }
        public String WB_Rn { get; private set; }

        public Boolean dataHazard { get; set; }
        public String hazardLine { get; set; }

        #endregion

        public Cycle()
        {
            this.IFID = false;
            this.IFID_IR = "";
            this.IFID_NPC = null;
            this.IFID_PC = null;

            this.IDEX = false;
            this.IDEX_A = null;
            this.IDEX_B = null;
            this.IDEX_IMM = "";
            this.IDEX_NPC = null;
            this.IDEX_IR = "";

            this.EXMEM = false;
            this.EXMEM_ALUOutput = null;
            this.EXMEM_Cond = null;
            this.EXMEM_B = null;
            this.EXMEM_IR = "";

            this.MEMWB = false;
            this.MEMWB_LMD = null;
            this.MEMWB_Range = null;
            this.MEMWB_IR = "";
            this.MEMWB_ALUOutput = null;

            this.WB = false;
            this.WB_Rn = null;        
        }

        public void setInstructionFetch(string opcodeString, string programCtr, string EXMEM_instructionType, string EXMEM_ALUOutput, string instruction, string instructionType, string instructionLine, string rs, string rt, string rd, string bse, string imm)
        {
            if (instruction != "")  //  if Instruction Fetched is not null -> pipeline map IF is true
            {
                this.IFID = true;
            }
            this.IFID_IR = opcodeString;
            if (programCtr != "")
            {
                this.IFID_NPC = ((Convert.ToInt32(programCtr) + 1) * 4).ToString("X");
                if (EXMEM_instructionType == "Branch Instruction")
                {
                    this.IFID_NPC = EXMEM_ALUOutput;
                }
            }
            else
            {
                this.IFID_NPC = "";
            }
            if (EXMEM_instructionType == "Branch Instruction")
            {
                this.IFID_NPC = EXMEM_ALUOutput;
            }
                

            this.IFID_PC = this.IFID_NPC;
            this.IFID_instruction = instruction;
            this.IFID_instructionType = instructionType;
            this.IFID_instructionLine = instructionLine;
            this.IFID_rs = rs;
            this.IFID_rt = rt;
            this.IFID_rd = rd;
            this.IFID_bse = bse;
            this.IFID_imm = imm;
        }

        public void setInstructionDecode(string rs, string rt, string rd, string bse, string IMM, string IFID_IR, string IFID_NPC, string instruction, string instructionType, string instructionLine)
        {
            char signextend;

            if (IFID_IR != "")  //  if Instruction Fetched is not null -> pipeline map IF is true
            {
                this.IDEX = true;
            }

            if (instructionType == "Register-Register ALU Instruction" || instructionType == "Register-Immediate ALU Instruction" || instructionType == "Branch Instruction")
            {
                this.IDEX_A = rs;
                this.IDEX_B = rt;
                if (IMM != "")
                {
                    if (IMM != null)
                    {
                        this.IDEX_IMM = IMM.Replace(" ", "");       //  remove white spaces
                        signextend = this.IDEX_IMM[0];              //  get sign extend value
                        String text = this.IDEX_IMM;                // convert to hex
                        String val = Convert.ToInt64(text, 2).ToString("X").ToUpper();
                        text = "";
                        for (int i = val.Length; i < 4; i++)
                        {
                            text += "0";
                        }
                        text += val;
                        this.IDEX_IMM = text;
                        if (signextend == '1')
                        {
                            this.IDEX_IMM = "FFFFFFFFFFFF" + this.IDEX_IMM;
                        }
                        else
                        {
                            this.IDEX_IMM = "000000000000" + this.IDEX_IMM;
                        }
                    } 
                }
            }
            else if (instructionType == "Load Instruction" || instructionType == "Store Instruction")
            {
                this.IDEX_A = bse;
                this.IDEX_B = rt;
                if (IMM != "")
                {
                    if (IMM != null)
                    {
                        this.IDEX_IMM = IMM.Replace(" ", "");       //  remove white spaces
                        signextend = this.IDEX_IMM[0];              //  get sign extend value
                        String text = this.IDEX_IMM;                // convert to hex
                        String val = Convert.ToInt64(text, 2).ToString("X").ToUpper();
                        text = "";
                        for (int i = val.Length; i < 4; i++)
                        {
                            text += "0";
                        }
                        text += val;
                        this.IDEX_IMM = text;
                        if (signextend == '1')
                        {
                            this.IDEX_IMM = "FFFFFFFFFFFF" + this.IDEX_IMM;
                        }
                        else
                        {
                            this.IDEX_IMM = "000000000000" + this.IDEX_IMM;
                        }
                    }
                }
            }
            else
            {

            }
            this.IDEX_IR = IFID_IR;
            this.IDEX_NPC = IFID_NPC;
            this.IDEX_instruction = instruction;
            this.IDEX_instructionType = instructionType;
            this.IDEX_instructionLine = instructionLine;
        }

        public Boolean setExecution(string IDEX_A, string IDEX_B, string IDEX_IMM, string IDEX_IR, string IDEX_NPC, string instruction, string instructionType, string instructionLine)
        {
            string temp;

            if (IDEX_IR != "")  //  if Instruction Fetched is not null -> pipeline map IF is true
            {
                this.EXMEM = true;
            }

            #region Register-Register ALU Instruction
            if (instructionType == "Register-Register ALU Instruction")
            {
                //Console.WriteLine("Register-Register ALU Instruction");
                if (instruction == "DSUBU")
                {
                    this.EXMEM_ALUOutput = (Convert.ToInt64(IDEX_A, 16) - Convert.ToInt64(IDEX_B, 16)).ToString("X");
                    while (this.EXMEM_ALUOutput.Length < 16)
                    {
                        this.EXMEM_ALUOutput = "0" + this.EXMEM_ALUOutput;
                    }
                    this.EXMEM_Cond = "0";
                }
                else if (instruction == "DDIVU")
                {
                    if (Convert.ToInt64(IDEX_B, 16) == 0)
                    {
                        MessageBox.Show("Divide by zero error.");
                        return true;
                    }
                    this.EXMEM_ALUOutput = (Convert.ToInt64(IDEX_A, 16) / Convert.ToInt64(IDEX_B, 16)).ToString("X");
                    while (this.EXMEM_ALUOutput.Length < 16)
                    {
                        this.EXMEM_ALUOutput = "0" + this.EXMEM_ALUOutput;
                    }
                    this.EXMEM_Cond = "0";
                }
                else if (instruction == "DMODU")
                {
                    if (Convert.ToInt64(IDEX_B, 16) == 0)
                    {
                        MessageBox.Show("Divide by zero error.");
                        return true;
                    }
                    this.EXMEM_ALUOutput = (Convert.ToInt64(IDEX_A, 16) % Convert.ToInt64(IDEX_B, 16)).ToString("X");
                    while (this.EXMEM_ALUOutput.Length < 16)
                    {
                        this.EXMEM_ALUOutput = "0" + this.EXMEM_ALUOutput;
                    }
                    this.EXMEM_Cond = "0";
                }
                else if (instruction == "SLT")
                {
                    if (Convert.ToInt64(IDEX_A, 16) < Convert.ToInt64(IDEX_B, 16))
                    {
                        this.EXMEM_ALUOutput = "0000000000000001";
                    }
                    else
	                {
                        this.EXMEM_ALUOutput = "0000000000000000";
	                }
                    this.EXMEM_Cond = "0";
                }
                else if (instruction == "SELNEZ")
	            {
		            if (Convert.ToInt64(IDEX_B, 16) != 0)
                    {
                        this.EXMEM_ALUOutput = Convert.ToInt64(IDEX_A, 16).ToString("X");
                        while (this.EXMEM_ALUOutput.Length < 16)
                        {
                            this.EXMEM_ALUOutput = "0" + this.EXMEM_ALUOutput;
                        }
                    }
                    else
                    {
                        this.EXMEM_ALUOutput = "0000000000000000";
                    }
                    this.EXMEM_Cond = "0";
	            }
                
            }
            #endregion
            #region Register-Immediate ALU Instruction
            else if (instructionType == "Register-Immediate ALU Instruction" || instructionType == "Load Instruction" || instructionType == "Store Instruction")
            {
                //Console.WriteLine("Register-Immediate ALU Instruction || instructionType == Load Instruction || instructionType == Store Instruction");
                //Console.WriteLine((Convert.ToInt64(IDEX_A, 16).ToString("X")) + " + " + (Convert.ToInt64(IDEX_IMM, 16)).ToString("X"));
                    this.EXMEM_ALUOutput = (Convert.ToInt64(IDEX_A, 16) + Convert.ToInt64(IDEX_IMM, 16)).ToString("X");
                    while (this.EXMEM_ALUOutput.Length < 16)
                    {
                        this.EXMEM_ALUOutput = "0" + this.EXMEM_ALUOutput;
                    }
                    this.EXMEM_Cond = "0";
            }
            #endregion
            #region Branch Instruction
            else if (instructionType == "Branch Instruction")
            {
                if (instruction == "BC")
                {
                    //Console.WriteLine("Branch Instruction");
                    //Console.WriteLine(IDEX_NPC + " + " + (Convert.ToInt64(IDEX_IMM, 16) * 4).ToString("X"));
                    this.EXMEM_ALUOutput = ((Convert.ToInt64(IDEX_NPC, 16) + Convert.ToInt64(IDEX_IMM, 16) * 4)).ToString("X");

                    while (this.EXMEM_ALUOutput.Length < 16)
                    {
                        this.EXMEM_ALUOutput = "0" + this.EXMEM_ALUOutput;
                    }
                    this.EXMEM_Cond = "1";
                }
                else if (instruction == "BNEC")
                {
                    //Console.WriteLine("BNEC Instruction");
                    
                    if (IDEX_A != IDEX_B)
                    {
                        //temp = Convert.ToInt64(IDEX_IMM, 2).ToString();
                        temp = IDEX_IMM;

                        if (temp[0] == 'F')
                        {
                            //Console.WriteLine("BNEC Instruction negative offset");
                            //Console.WriteLine(IDEX_NPC + " - " + (Convert.ToInt64(hextToDecSigned(IDEX_IMM), 2) * 4).ToString("X"));
                            this.EXMEM_ALUOutput = ((Convert.ToInt64(IDEX_NPC, 16) - Convert.ToInt64(hextToDecSigned(IDEX_IMM), 2) * 4)).ToString("X");
                            //Console.WriteLine("OUTPUT= " + this.EXMEM_ALUOutput);
                            //Console.WriteLine(IDEX_A + "!=" + IDEX_B);
                        }

                        else
                        {
                            //Console.WriteLine(IDEX_NPC + " + " + (Convert.ToInt64(IDEX_IMM, 16) * 4).ToString("X"));
                            this.EXMEM_ALUOutput = ((Convert.ToInt64(IDEX_NPC, 16) + Convert.ToInt64(IDEX_IMM, 16) * 4)).ToString("X");
                        }
                        
                        while (this.EXMEM_ALUOutput.Length < 16)
                        {
                            this.EXMEM_ALUOutput = "0" + this.EXMEM_ALUOutput;
                        }
                        this.EXMEM_Cond = "1";
                    }
                    else
                    {
                        this.EXMEM_ALUOutput = Convert.ToInt64(IDEX_NPC, 16).ToString("X");
                        while (this.EXMEM_ALUOutput.Length < 16)
                        {
                            this.EXMEM_ALUOutput = "0" + this.EXMEM_ALUOutput;
                        }
                        this.EXMEM_Cond = "0";
                    }
                }
            }
            #endregion

            this.EXMEM_B = IDEX_B;
            this.EXMEM_IR = IDEX_IR;
            this.EXMEM_instruction = instruction;
            this.EXMEM_instructionType = instructionType;
            return false;
        }

        public void setMemoryAccess(string EXMEM_IR, string EXMEM_ALUOutput, string LMD, string instruction, string instructionType)
        {
            if (EXMEM_IR != "")  //  if Instruction Fetched is not null -> pipeline map IF is true
            {
                this.MEMWB = true;
            }

            if (instructionType == "Register-Register ALU Instruction" || instructionType == "Register-Immediate ALU Instruction")
            {
                this.MEMWB_LMD = "";
            }
            else if (instructionType == "Load Instruction")
            {
                this.MEMWB_LMD = LMD;
            }
            else
            {
                this.MEMWB_LMD = "";
            }
            this.MEMWB_IR =EXMEM_IR;
            this.MEMWB_ALUOutput = EXMEM_ALUOutput;
            this.MEMWB_instruction = instruction;
            this.MEMWB_instructionType = instructionType;
        }

        private String hextToDecSigned(string inputStr)
        {
            int i;
             inputStr = String.Join(String.Empty,inputStr.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
            //inputStr = Convert.ToString(Convert.ToInt64(inputStr, 16), 2);
            System.Text.StringBuilder str = new System.Text.StringBuilder(inputStr);

            for ( i = str.Length-1; i >= 0; i--)
            {
                if (str[i] == '1')
                {
                    break;
                }
            }
            i--;
            for (int k = i; k >= 0; k--)
			{
			    if (str[k] == '0')
	            {
		            str[k] = '1';
	            }
                else
	            {
                    str[k] = '0';
	            }
			}
            Console.WriteLine("HEX: " + inputStr + "   TO:   " + str.ToString());
            return str.ToString();
        }

    }
}

