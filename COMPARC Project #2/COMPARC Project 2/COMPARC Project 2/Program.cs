﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COMPARC_Project_2
{
    public class Program
    {
        private List<Instruction> instruction;
        private List<Cycle> cycle;
        private int numCycles;
        private List<Register> registers;
        private int tempOffset;
        private Boolean hasBranch = false;
        private Boolean instructionsValid = true;
        private Boolean registersValid = true;
        private List<Memory> memory;

        public Program(String[] program, String[] registers)
        {
            this.instruction = new List<Instruction>();
            this.cycle = new List<Cycle>();
            this.registers = new List<Register>();
            this.memory = new List<Memory>();
            this.initializeInstructionArray(program);
            this.intializeRegisterArray(registers);
            instructionsValid = this.isValid();
            this.registersValid = isRegistersValid();
            
            if (this.instructionsValid && hasBranch)
            {
                this.setBranchOffsets();
                showAllOpcodes();
            }

            this.pipeline();
        }

        #region setters 

        private void initializeInstructionArray(String[] program) //sets all the instructions
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

        private void intializeRegisterArray(String[] registers)   //sets all registers
        {
            this.registers.Capacity = 32;
            int num = 0;

            for (int i = 0; i < registers.Length; i++)
                registers[i] = registers[i].Replace(" ", string.Empty);

            for (int i = 0; i < this.registers.Capacity; i++)
            {
                this.registers.Add(new Register("R" + num, registers[i]));
                num++;
            }
        }

        private void setBranchOffsets()                           //sets branch offset if an instruction is a jump
        {
            for (int i = 0; i < this.instruction.Count(); i++)
            {
                if (this.instruction[i].getInstruction().Equals("BNEC") || this.instruction[i].getInstruction().Equals("BC"))
                {
                    Console.WriteLine("Found: " + this.instruction[i].getInstruction() + ", i=" + i);
                    Console.WriteLine(this.instruction[i].getInstruction() + " branches to" + this.instruction[i].getOffset());
                    this.hasBranch = true;

                    for (int j = 0; j < this.instruction.Count(); j++)
                    {
                        Console.WriteLine("branch location of line " + j + ": " + this.instruction[j].getBranchLocation());
                        if (this.instruction[i].getOffset().Equals(this.instruction[j].getBranchLocation()))
                        {
                            Console.WriteLine("i= " + i);
                            Console.WriteLine("j= " + j);
                            tempOffset = j - i - 1;
                            Console.WriteLine("tempOffset= " + tempOffset);
                            this.instruction[i].setOffset(tempOffset.ToString());
                            this.instruction[i].getOpcode().setOffset(tempOffset.ToString());
                            this.instruction[i].getOpcode().addOpcodeString(this.instruction[i].getOpcode().getOffsetO());
                        }

                    }

                }
            }
        }

        #endregion

        #region getters

        public String getInstructionOpCode(int lineNum)
        {
            return this.instruction[lineNum].getOpcode().getOpcodeString();
        }

        public String getInstructionLine(int lineNum)
        {
            return this.instruction[lineNum].getInstructionLine();
        }

        public Boolean getInstructionsValid()
        {
            return this.instructionsValid;
        }

        public int getInstructionLength()
        {
            return this.instruction.Count;
        }


        public String getIFID_IR(int i)
        {
            return this.cycle[i].IFID_IR;
        }

        public String getIFID_NPC(int i)
        {
            return this.cycle[i].IFID_NPC;
        }

        public String getIFID_PC(int i)
        {
            return this.cycle[i].IFID_PC;
        }

        public String getIDEX_A(int i)
        {
            return this.cycle[i].IDEX_A;
        }
        public String getIDEX_B(int i)
        {
            return this.cycle[i].IDEX_B;
        }

        public String getIDEX_IMM(int i)
        {
            return this.cycle[i].IDEX_IMM;
        }

        public String getIDEX_IR(int i)
        {
            return this.cycle[i].IDEX_IR;
        }

        public String getIDEX_NPC(int i)
        {
            return this.cycle[i].IDEX_NPC;
        }

        public String getEXMEM_ALUOutput(int i)
        {
            return this.cycle[i].EXMEM_ALUOutput;
        }

        public String getEXMEM_Cond(int i)
        {
            return this.cycle[i].EXMEM_Cond;
        }

        public String getEXMEM_IR(int i)
        {
            return this.cycle[i].EXMEM_IR;
        }

        public String getEXMEM_B(int i)
        {
            return this.cycle[i].EXMEM_B;
        }

        public int getNumCycles()
        {
            return this.numCycles;
        }

        #endregion

        #region checking functions

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

        private Boolean isRegistersValid() //checks if all registers are valid
        {
            for (int i = 0; i < this.registers.Capacity; i++)
                if (!isHexValid(registers[i].getValue()) || registers[i].getValue().Length != 16)
                {
                    System.Windows.Forms.MessageBox.Show("ERROR AT : " + registers[i].getName());
                    return false;
                }
            return true;
        }

        private Boolean isHexValid(String value)
        {
            if (value.All(c => "0123456789ABCDEF".Contains(c)))
                return true;
            else
                return false;
        }

        private Boolean checkDataHazard(Instruction currInstruction, Instruction prevInstruction)
        {

            // I to I
            if (prevInstruction.getOpcode().getOpcodeType() == 'I' && currInstruction.getOpcode().getOpcodeType() == 'I')
            {
                // I to load/store
                if (prevInstruction.getOpcode().rtO == currInstruction.getOpcode().bseO)
                {
                    return true;
                }

                //I to not load/store
                if (prevInstruction.getOpcode().rtO == currInstruction.getOpcode().rsO)
                {
                    return true;
                }
            }

            // R to R
            if (prevInstruction.getOpcode().getOpcodeType() == 'R' && currInstruction.getOpcode().getOpcodeType() == 'R')
            {
                //  rd = rs || rd = rt
                if (prevInstruction.getOpcode().rdO == currInstruction.getOpcode().rsO || prevInstruction.getOpcode().rdO == currInstruction.getOpcode().rtO)
                {
                    return true;
                }
            }

            // I to R
            if (prevInstruction.getOpcode().getOpcodeType() == 'I' && currInstruction.getOpcode().getOpcodeType() == 'R')
            {
                if (prevInstruction.getOpcode().rtO == currInstruction.getOpcode().rsO || prevInstruction.getOpcode().rtO == currInstruction.getOpcode().rtO)
                {
                    return true;
                }
            }

            // R to I
            if (prevInstruction.getOpcode().getOpcodeType() == 'I' && currInstruction.getOpcode().getOpcodeType() == 'R')
            {
                // R to load/store
                if (prevInstruction.getOpcode().rdO == currInstruction.getOpcode().bseO)
                {
                    return true;
                }

                // R to not load/store
                if (prevInstruction.getOpcode().rdO == currInstruction.getOpcode().rsO)
                {
                    return true;
                }
            }

            return false;
        }


        #endregion

        private void showAllOpcodes() //checks if all lines are valid
        {
            
            for (int i = 0; i < instruction.Count; i++)
            {
                Console.WriteLine(this.instruction[i].getOpcode().getOpcodeString());
            }
             
        }

        private void pipeline()
        {
            int i = 0;

            this.numCycles = 0;
            do
            {
                this.cycle.Add(new Cycle());
                this.numCycles++;
                
                if (i == 0)     
                {
                    this.cycle[i].setInstructionFetch(this.instruction[i].getOpcode().getOpcodeString(), "0");      //  if first cycle, NPC & PC = 4       
                }
                else
                {
                    this.cycle[i].setInstructionFetch(this.instruction[i].getOpcode().getOpcodeString(), this.cycle[i - 1].IFID_NPC);
                    this.cycle[i].setInstructionDecode(
                        this.registers[Convert.ToInt32(this.instruction[i - 1].getOpcode().getOpcodeString().Substring(7, 5), 2)].getValue(),   //get data in the A ([IF/ID.IR 21..25])
                        this.registers[Convert.ToInt32(this.instruction[i - 1].getOpcode().getOpcodeString().Substring(13, 5), 2)].getValue(),  //get data in the B ([IF/ID.IR 16..20])
                        this.instruction[i - 1].getOpcode().getOpcodeString().Substring(18), 
                        this.cycle[i - 1].IFID_IR, this.cycle[i - 1].IFID_NPC);                   
                    this.cycle[i].setExecution(
                        this.instruction[i - 1].getInstruction(),
                        this.instruction[i - 1].getInstructionType(), 
                        this.cycle[i - 1].IDEX_A, 
                        this.cycle[i - 1].IDEX_B, 
                        this.cycle[i - 1].IDEX_IMM, 
                        this.cycle[i - 1].IDEX_IR);
                    if (this.checkDataHazard(this.instruction[i], this.instruction[i - 1]))
                    {
                        for (int k = i; k < 3+i; k++)
                        {
                            this.cycle.Add(new Cycle());
                            this.numCycles++;
                            this.cycle[k].stall = true;

                            this.cycle[k].setExecution(
                                this.instruction[k - 1].getInstruction(),
                                this.instruction[k - 1].getInstructionType(),
                                this.cycle[k - 1].IDEX_A,
                                this.cycle[k - 1].IDEX_B,
                                this.cycle[k - 1].IDEX_IMM,
                                this.cycle[k - 1].IDEX_IR);
                        }
                        i += 3;
                    }

                }
                i++;
            } while (i < this.instruction.Count);
            
        }

    }
}
