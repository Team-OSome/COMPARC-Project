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
        private int numCycles;
        private List<Register> registers;
        private int tempOffset;
        private Boolean hasBranch = false;
        private Boolean instructionsValid = true;
        private Boolean registersValid = true;
        private Boolean memoryValid = true;
        private List<Memory> memory;
        private String[] program;

        public Program(String[] program, String[] registers, String[] memory)
        {
            this.instruction = new List<Instruction>();
            this.cycle = new List<Cycle>();
            this.registers = new List<Register>();
            this.memory = new List<Memory>();

            List<String> programList = program.ToList<String>();

            for (int i = 0; i < programList.Count(); i++)
            {
                Console.WriteLine(i + ":" + programList.ElementAt(i)+".");
            }

            for (int i = 0; i < programList.Count(); i++) 
            {
                //Console.WriteLine(i + ":" + programList.ElementAt(i));
                if (programList.ElementAt(i).Equals("") || programList.ElementAt(i).Equals(" ") || programList.ElementAt(i).Equals(13) || programList.ElementAt(i).Equals(0))
                {
                    Console.WriteLine("Removed line i=" + i);
                    programList.RemoveAt(i);
                    i--;
                }
            }

            this.program = programList.ToArray();



            this.initializeInstructionArray(this.program);
            this.intializeRegisterArray(registers);
            this.intializeMemoryArray(memory);
            
            this.instructionsValid = this.isValid();
            this.registersValid = isRegistersValid();
            this.memoryValid = isMemoryValid();
            
            if (this.instructionsValid && hasBranch)
            {
                this.setBranchOffsets();
            }

            for (int i = 0; i < instruction.Count(); i++)
            {
                this.instruction[i].getOpcode().setHexOpcodeString();
            }
                showAllOpcodes();
                showAllHexOpcodes();

            this.pipeline();
        }

        #region setters/intializers 

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

        private void intializeMemoryArray(String[] memory)
        {
            this.memory.Capacity = 8192;
            int num = 0x3FFF;

            for (int i = 0; i < this.memory.Capacity; i++)
            {
                this.memory.Add(new Memory(num.ToString("x").ToUpper(), memory[i]));
                num--;
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

        public String getRegisterData(int i)
        {
            return this.registers[i].getValue();
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

        public String getMEMWB_LMD(int i)
        {
            return this.cycle[i].MEMWB_LMD;
        }

        public String getMEMWB_Range(int i)
        {
            return this.cycle[i].MEMWB_Range;
        }

        public String getMEMWB_ALUOutput(int i)
        {
            return this.cycle[i].MEMWB_ALUOutput;
        }

        public String getMEMWB_IR(int i)
        {
            return this.cycle[i].MEMWB_IR;
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
                if (!isHexValid(this.registers[i].getValue()) || this.registers[i].getValue().Length != 16)
                {
                    System.Windows.Forms.MessageBox.Show("ERROR AT : " + this.registers[i].getName());
                    return false;
                }
            return true;
        }

        private Boolean isMemoryValid() //checks if all memory location values are valid
        {
            for (int i = 0; i < this.memory.Capacity; i++)
                if (!isHexValid(this.memory[i].getValue()) || this.memory[i].getValue().Length != 2)
                {
                    System.Windows.Forms.MessageBox.Show("ERROR AT MEMORY LOCATION : " + this.memory[i].getLocation());
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

        private void showAllOpcodes() 
        {
            
            for (int i = 0; i < instruction.Count; i++)
            {
                Console.WriteLine(this.instruction[i].getOpcode().getOpcodeString());
            }
             
        }

        private void showAllHexOpcodes() //checks if all lines are valid
        {

            for (int i = 0; i < instruction.Count; i++)
            {
                Console.WriteLine(this.instruction[i].getOpcode().getHexOpcodeString());
            }

        }

        private void pipeline()
        {
            int i = 0;
            int totalCycles = this.instruction.Count + 4;
            //int completedCycles = 0;
            this.numCycles = 0;
            do
            {
                this.cycle.Add(new Cycle());
                this.numCycles++;

                if (i == 0)     //  if first cycle, NPC & PC = 4
                {
                    this.cycle[i].setInstructionFetch(
                        this.instruction[i].getOpcode().getOpcodeString(), 
                        "0",
                        this.instruction[i].getInstruction(),
                        this.instruction[i].getInstructionType()
                        );             
                }
                else if (i == this.instruction.Count)
                {
                    this.cycle[i].setInstructionFetch(
                        "", 
                        "",
                        "",
                        ""
                        );
                    this.cycle[i].setInstructionDecode(
                        this.registers[Convert.ToInt32(this.cycle[i - 1].IFID_IR.Substring(7, 5), 2)].getValue(),   //get data in the A ([IF/ID.IR 21..25])
                        this.registers[Convert.ToInt32(this.cycle[i - 1].IFID_IR.Substring(13, 5), 2)].getValue(),  //get data in the B ([IF/ID.IR 16..20])
                        this.instruction[i - 1].getOpcode().getOpcodeString().Substring(18),
                        this.cycle[i - 1].IFID_IR,
                        this.cycle[i - 1].IFID_NPC,
                        this.cycle[i - 1].IFID_instruction,
                        this.cycle[i - 1].IFID_instructionType
                        );
                }
                else if (i >= this.instruction.Count + 1)
                {
                    this.cycle[i].setInstructionFetch(
                        "",
                        "",
                        "",
                        ""
                        );
                    this.cycle[i].setInstructionDecode(
                         "",   //get data in the A ([IF/ID.IR 21..25])
                         "",  //get data in the B ([IF/ID.IR 16..20])
                         "",
                         "",
                         "",
                         "",
                         ""
                         );
                }
                else
                {
                   this.cycle[i].setInstructionFetch(
                        this.instruction[i].getOpcode().getOpcodeString(),
                        this.cycle[i - 1].IFID_NPC,
                        this.instruction[i].getInstruction(),
                        this.instruction[i].getInstructionType()
                        );     
		           this.cycle[i].setInstructionDecode(
                        this.registers[Convert.ToInt32(this.cycle[i - 1].IFID_IR.Substring(7, 5), 2)].getValue(),   //get data in the A ([IF/ID.IR 21..25])
                        this.registers[Convert.ToInt32(this.cycle[i - 1].IFID_IR.Substring(13, 5), 2)].getValue(),  //get data in the B ([IF/ID.IR 16..20])
                        this.instruction[i - 1].getOpcode().getOpcodeString().Substring(18),
                        this.cycle[i - 1].IFID_IR,
                        this.cycle[i - 1].IFID_NPC,
                        this.cycle[i - 1].IFID_instruction,
                        this.cycle[i - 1].IFID_instructionType
                        );

                }
                if (i != 0)
                {
                    this.cycle[i].setExecution(
                        this.cycle[i - 1].IDEX_A,
                        this.cycle[i - 1].IDEX_B,
                        this.cycle[i - 1].IDEX_IMM,
                        this.cycle[i - 1].IDEX_IR,
                        this.cycle[i - 1].IDEX_instruction,
                        this.cycle[i - 1].IDEX_instructionType
                        );
                    this.cycle[i].setMemoryAccess(
                        this.cycle[i - 1].EXMEM_IR,
                        this.cycle[i - 1].EXMEM_ALUOutput,
                        this.cycle[i - 1].EXMEM_instruction,
                        this.cycle[i - 1].EXMEM_instructionType
                        );
                    this.pipelineWriteBack(i);
                }
                
                i++;
            } while (i < totalCycles);
            
        }

        private void pipelineWriteBack(int i)
        {
            if (this.cycle[i - 1].MEMWB_instructionType == "Register-Register ALU Instruction")
            {
                this.registers[Convert.ToInt32(this.cycle[i - 1].MEMWB_IR.Substring(19, 5), 2)].setRegisterValue(this.cycle[i - 1].MEMWB_ALUOutput);
            }
            else if (this.cycle[i - 1].MEMWB_instructionType == "Register-Immediate ALU Instruction")
            {
                if (this.cycle[i-1].MEMWB_instruction == "DADDIU")
                {
                    this.registers[Convert.ToInt32(this.cycle[i - 1].MEMWB_IR.Substring(13, 5), 2)].setRegisterValue(this.cycle[i - 1].MEMWB_ALUOutput);
                }
            }
            else if (this.cycle[i - 1].MEMWB_instructionType == "Load Instruction")
            {

            }
            else
            {

            }
        }
        
        public String getWriteBackRegister(int i)
        {
            if (i > 0)
            {
                if (this.cycle[i - 1].MEMWB_instructionType == "Register-Register ALU Instruction")
                {
                    return "R" + Convert.ToInt32(this.cycle[i - 1].MEMWB_IR.Substring(19, 5), 2).ToString() + " = " + this.cycle[i - 1].MEMWB_ALUOutput;
                }
                else
                {
                    return "";
                }
            }
            
            else
            {
                return "";
            }
                
        }
         

    }
}

/*
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
 */