﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Boolean IDEX { get; private set; }
        public String IDEX_A { get; private set; }
        public String IDEX_B { get; private set; }
        public String IDEX_IMM { get; private set; }
        public String IDEX_NPC { get; private set; }
        public String IDEX_IR { get; private set; }
        public String IDEX_instruction { get; private set; }
        public String IDEX_instructionType { get; private set; }

        public Boolean EXMEM { get; private set; }
        public String EXMEM_ALUOutput { get; private set; }
        public String EXMEM_Cond { get; private set; }
        public String EXMEM_B { get; private set; }
        public String EXMEM_IR { get; private set; }
        public String EXMEM_instruction { get; private set; }
        public String EXMEM_instructionType { get; private set; }

        public Boolean MEMWB { get; private set; }
        public String MEMWB_LMD { get; private set; }
        public String MEMWB_Range { get; private set; }
        public String MEMWB_IR { get; private set; }
        public String MEMWB_ALUOutput { get; private set; }
        public String MEMWB_instruction { get; private set; }
        public String MEMWB_instructionType { get; private set; }

        public Boolean WB { get; set; }
        public String WB_Rn { get; private set; }

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

        public void setInstructionFetch(string opcode, string programCtr, string instruction, string instructionType)
        {
            if (instruction != "")  //  if Instruction Fetched is not null -> pipeline map IF is true
            {
                this.IFID = true;
            }
            this.IFID_IR = opcode;
            if (programCtr != "")
            {
                this.IFID_NPC = ((Convert.ToInt32(programCtr) + 1) * 4).ToString("X");
            }
            else
            {
                this.IFID_NPC = "";
            }
            this.IFID_PC = this.IFID_NPC;
            this.IFID_instruction = instruction;
            this.IFID_instructionType = instructionType;
        }

        public void setInstructionDecode(string rs, string rt, string rd, string bse, string IMM, string IFID_IR, string IFID_NPC, string instruction, string instructionType)
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
        }
        
        /*
        public void setInstructionDecode(string A, string B, string IMM, string IFID_IR, string IFID_NPC, string instruction, string instructionType)
        {
            char signextend;
            this.IDEX_A = A;   // [IF/ID.IR 21..25]
            this.IDEX_B = B;   // [IF/ID.IR 16..20]

            if (IFID_IR != "")  //  if Instruction Fetched is not null -> pipeline map IF is true
            {
                this.IDEX = true;
            }

            if (IMM != "")
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

            this.IDEX_IR = IFID_IR;
            this.IDEX_NPC = IFID_NPC;
            this.IDEX_instruction = instruction;
            this.IDEX_instructionType = instructionType;
        }
        */
         
        public void setExecution(string IDEX_A, string IDEX_B, string IDEX_IMM, string IDEX_IR, string instruction, string instructionType )
        {
            if (IDEX_IR != "")  //  if Instruction Fetched is not null -> pipeline map IF is true
            {
                this.EXMEM = true;
            }

            if (instructionType == "Register-Register ALU Instruction")
            {
                Console.WriteLine("Register-Register ALU Instruction");
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
                    this.EXMEM_ALUOutput = (Convert.ToInt64(IDEX_A, 16) / Convert.ToInt64(IDEX_B, 16)).ToString("X");
                    while (this.EXMEM_ALUOutput.Length < 16)
                    {
                        this.EXMEM_ALUOutput = "0" + this.EXMEM_ALUOutput;
                    }
                    this.EXMEM_Cond = "0";
                }
                else if (instruction == "DMODU")
                {
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

            else if (instructionType == "Register-Immediate ALU Instruction" || instructionType == "Load Instruction" || instructionType == "Store Instruction")
            {
                Console.WriteLine("Register-Immediate ALU Instruction || instructionType == Load Instruction || instructionType == Store Instruction");
                //Console.WriteLine((Convert.ToInt64(IDEX_A, 16).ToString("X")) + " + " + (Convert.ToInt64(IDEX_IMM, 16)).ToString("X"));
                    this.EXMEM_ALUOutput = (Convert.ToInt64(IDEX_A, 16) + Convert.ToInt64(IDEX_IMM, 16)).ToString("X");
                    while (this.EXMEM_ALUOutput.Length < 16)
                    {
                        this.EXMEM_ALUOutput = "0" + this.EXMEM_ALUOutput;
                    }
                    this.EXMEM_Cond = "0";
            }
            else if (instructionType == "Branch Instruction")
            {
                Console.WriteLine("Branch Instruction");
                Console.WriteLine((Convert.ToInt64(IDEX_NPC, 16).ToString("X")) + " + " + (Convert.ToInt64(IDEX_IMM, 16) * 8).ToString("X"));
                this.EXMEM_ALUOutput = ((Convert.ToInt64(IDEX_NPC, 16) + Convert.ToInt64(IDEX_IMM, 16) * 8)).ToString("X");
                
                //this.EXMEM_ALUOutput = (Convert.ToInt64(IDEX_A, 16) + Convert.ToInt64(IDEX_IMM, 16)).ToString("X");
                while (this.EXMEM_ALUOutput.Length < 16)
                {
                    this.EXMEM_ALUOutput = "0" + this.EXMEM_ALUOutput;
                }
                this.EXMEM_Cond = "1";
            }
            
            this.EXMEM_B = IDEX_B;
            this.EXMEM_IR = IDEX_IR;
            this.EXMEM_instruction = instruction;
            this.EXMEM_instructionType = instructionType;
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

    }
}
