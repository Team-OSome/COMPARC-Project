using System;
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
        private List<Register> registers;
        private int tempOffset;
        private Boolean hasBranch = false;
        private Boolean instructionsValid = true;
        private Boolean registersValid = true;

        public Program(String[] program, String[] registers)
        {
            this.instruction = new List<Instruction>();
            this.cycle = new List<Cycle>();
            this.registers = new List<Register>();
            this.initializeInstructionArray(program);
            this.intializeRegisterArray(registers);
            this.isValid();
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

        public int getInstructionLength()
        {
            return this.instruction.Count;
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
            do
            {
                this.cycle.Add(new Cycle());

                
                if (i == 0)     
                {
                    this.cycle[i].setInstructionFetch(this.instruction[i].getOpcode().getOpcodeString(), "4");      //  if first cycle, NPC & PC = 4       
                }
                else
                {
                    this.cycle[i].setInstructionFetch(this.instruction[i].getOpcode().getOpcodeString(), this.cycle[i - 1].IFID_NPC);
                    this.cycle[i].setInstructionDecode(this.cycle[i - 1].IFID_IR, this.cycle[i - 1].IFID_NPC);
                    //this.cycle[i].setExecution("help", this.cycle[i - 1].IDEX_B, "cond", this.cycle[i - 1].IDEX_IR);
                }
                
                i++;
            } while (i < this.instruction.Count);
            MessageBox.Show(i.ToString());
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

    }
}
