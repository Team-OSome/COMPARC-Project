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
                    //Console.WriteLine("Removed line i=" + i);
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
                //showAllOpcodes();
                showAllHexOpcodes();
            if(this.instructionsValid)
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

            for (int i = (this.memory.Capacity - 1); i >= 0; i--)
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
                    //Console.WriteLine("Found: " + this.instruction[i].getInstruction() + ", i=" + i);
                    //Console.WriteLine(this.instruction[i].getInstruction() + " branches to" + this.instruction[i].getOffset());
                    this.hasBranch = true;

                    for (int j = 0; j < this.instruction.Count(); j++)
                    {
                        //Console.WriteLine("branch location of line " + j + ": " + this.instruction[j].getBranchLocation());
                        if (this.instruction[i].getOffset().Equals(this.instruction[j].getBranchLocation()))
                        {
                           // Console.WriteLine("i= " + i);
                           // Console.WriteLine("j= " + j);
                            tempOffset = j - i - 1;
                            //Console.WriteLine("tempOffset= " + tempOffset);
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

        public Boolean getRegisterValid()
        {
            return this.registersValid;
        }

        public String getMemoryData(int i)
        {
            return this.memory[i].getValue();
        }

        public Boolean getMemoryValid()
        {
            return this.memoryValid;
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

        public Boolean getIFID(int i)
        {
            return this.cycle[i].IFID;
        }

        public Boolean getIDEX(int i)
        {
            return this.cycle[i].IDEX;
        }

        public Boolean getEXMEM(int i)
        {
            return this.cycle[i].EXMEM;
        }

        public Boolean getMEMWB(int i)
        {
            return this.cycle[i].MEMWB;
        }

        public Boolean getWB(int i)
        {
            return this.cycle[i].WB;
        }

        public String getWriteBackRegister(int i)
        {
            if (i > 0)
            {
                if (this.cycle[i - 1].MEMWB_instructionType == "Register-Register ALU Instruction" || this.cycle[i - 1].MEMWB_instructionType == "Register-Immediate ALU Instruction")
                {
                    return "R" + Convert.ToInt32(this.cycle[i - 1].MEMWB_IR.Substring(19, 5), 2).ToString() + " = "
                        + this.cycle[i - 1].MEMWB_ALUOutput.Substring(0, 4) + " "
                        + this.cycle[i - 1].MEMWB_ALUOutput.Substring(4, 4) + " "
                        + this.cycle[i - 1].MEMWB_ALUOutput.Substring(8, 4) + " "
                        + this.cycle[i - 1].MEMWB_ALUOutput.Substring(12, 4);
                }
                else if (this.cycle[i - 1].MEMWB_instructionType == "Load Instruction")
                {
                        return "R" + Convert.ToInt32(this.cycle[i - 1].MEMWB_IR.Substring(13, 5), 2).ToString() + " = "
                        + this.cycle[i - 1].MEMWB_LMD.Substring(0, 4) + " "
                        + this.cycle[i - 1].MEMWB_LMD.Substring(4, 4) + " "
                        + this.cycle[i - 1].MEMWB_LMD.Substring(8, 4) + " "
                        + this.cycle[i - 1].MEMWB_LMD.Substring(12, 4);
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
            if (prevInstruction.getInstruction() == "BNEC" || prevInstruction.getInstruction() == "BC")
            {
                return false;
            }
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

        #region showOpcodes

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
                Console.WriteLine(i.ToString() + ": " + this.instruction[i].getOpcode().getHexOpcodeString());
            }

        }

        #endregion

        #region Load and Store Methods

        private Boolean checkLoadAddress(string startAddress, string instructionType)
        {
            if (startAddress != "" && startAddress != null)
            {
                if (instructionType == "Load Instruction")
                {
                    int x = Convert.ToInt32(startAddress, 16);
                    if (x >= 8192 && x <= 16376)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }

            }
            else
            {
                return true;
            }
        }

        private String loadDouble(string startAddress, int i)
        {
            string loadedValue = "";
            if (this.cycle[i - 1].EXMEM_instructionType == "Load Instruction")
            {
                if (startAddress != "")
                {
                    
                    int address = Convert.ToInt32(startAddress, 16) - 8192;
                    if (address >= 0 && address <= 8192)
                    {
                        for (int k = address + 7; k >=  address; k--)
                        {
                            loadedValue += this.memory[k].getValue();
                        }
                        return loadedValue;
                    }
                    else
                    {
                        return "error";
                    }
                }
            }
            return "";
        }

        private void storeDouble(string startAddress, int i)
        {
            int j = 0;
           
            if (this.cycle[i - 1].EXMEM_instructionType == "Store Instruction")
            {
                if (startAddress != "")
                {
                    int address = Convert.ToInt32(startAddress, 16) - 8192;
                    if (address >= 0 && address <= 8192)
                    {
                        for (int k = address + 7; k >= address; k--)
                        {
                            this.memory[k].setValue(this.cycle[i - 1].EXMEM_B.Substring(j, 2));
                            j += 2;
                        }
                    }
                }
            }
        }

        #endregion

        private void pipeline()
        {
            int i = 0;
            int c = 0;
            int temp = 0;
            Boolean addressRange = true ;
            int totalCycles = this.instruction.Count + 4;
            do
            {
                this.cycle.Add(new Cycle());
                this.numCycles++;

                if (i < this.instruction.Count)
                {
                    if (this.instruction[i].getInstructionType() == "Branch Instruction")
                    {
                        temp = i;

                        i = this.InstructionFetch(i, c);
                        this.InstructionDecode(i, c);
                        this.Execution(i, c);
                        addressRange = this.MemoryAccess(i, c); if (addressRange == false) break;
                        this.WriteBack(c);
          
                        i++;
                        c++;
                        this.cycle.Add(new Cycle());
                        this.numCycles++;
                        i = this.InstructionFetch(i, c);
                        this.InstructionDecode(i, c);
                        this.Execution(i, c);
                        addressRange = this.MemoryAccess(i, c); if (addressRange == false) break;
                        this.WriteBack(c);

                        i++;
                        c++;
                        this.cycle.Add(new Cycle());
                        this.numCycles++;
                        i = this.InstructionFetch(i, c);
                        this.Execution(i, c);
                        addressRange = this.MemoryAccess(i, c); if (addressRange == false) break;
                        this.WriteBack(c);
                        
                        i++;
                        c++;
                        this.cycle.Add(new Cycle());
                        this.numCycles++;
                        i = this.InstructionFetch(i, c);
                        addressRange = this.MemoryAccess(i, c); if (addressRange == false) break;
                        this.WriteBack(c);
                        
                        i++;
                        c++;
                        this.cycle.Add(new Cycle());
                        this.numCycles++;
                        i = this.InstructionFetch(i, c);
                        this.WriteBack(c);

                        i = temp;
                    }
                    else
                    {
                        i = this.InstructionFetch(i, c);                                 //i gets the instrucion array of the next instruction
                        this.InstructionDecode(i, c);
                        this.Execution(i, c);
                        addressRange = this.MemoryAccess(i, c); if (addressRange == false) break;                //break if out of address range
                        this.WriteBack(c);
                    }
                }
                else
                {
                    i = this.InstructionFetch(i, c);                                 //i gets the instrucion array of the next instruction
                    this.InstructionDecode(i, c);
                    this.Execution(i, c);
                    addressRange = this.MemoryAccess(i, c); if (addressRange == false) break;                //break if out of address range
                    this.WriteBack(c);
                }
                i++;
                c++;
            } while (i < totalCycles);

            if (addressRange == false)                  //break if out of address range
            {
                this.cycle = null;
                this.cycle = new List<Cycle>();
                this.cycle.Add(new Cycle());
                this.numCycles = 0;
            }    

        }

        private int InstructionFetch(int i, int c)      // Returns the instruction array number of the next instruction
        {
            if (i == 0)     //If first instruction there is no previous cycle to get EXMEM_instructionType and EXMEM_ALUOutput
            {
                this.cycle[c].setInstructionFetch(
                    this.instruction[i].getOpcode().getOpcodeString(),
                    this.instruction[i].getLineNumber().ToString(),
                    "",
                    "",
                    this.instruction[i].getInstruction(),
                    this.instruction[i].getInstructionType(),
                    this.instruction[i].getInstructionLine(),
                    this.registers[Convert.ToInt32(this.instruction[i].getOpcode().rsO, 2)].getValue(),
                    this.registers[Convert.ToInt32(this.instruction[i].getOpcode().rtO, 2)].getValue(),
                    this.registers[Convert.ToInt32(this.instruction[i].getOpcode().rdO, 2)].getValue(),
                    this.registers[Convert.ToInt32(this.instruction[i].getOpcode().bseO, 2)].getValue(),
                    this.instruction[i].getOpcode().getOpcodeString().Substring(18)
                    );
            }
            else if (i >= this.instruction.Count)       //If last instruction there is no more instructions to fetch
            {
                this.cycle[c].setInstructionFetch("","","","","","","","","","","","");
            }
            else
            {
                for (int k = 0; k < this.instruction.Count; k++)
                {
                    if (instruction[k].getLineNumber() * 4 == Convert.ToInt32(this.cycle[c - 1].IFID_NPC, 16))
                    {
                        i = k;
                        k = this.instruction.Count;
                    }
                    else if (this.instruction[this.instruction.Count - 1].getLineNumber() * 4 < Convert.ToInt32(this.cycle[c - 1].IFID_NPC, 16))
                    {
                        k = this.instruction.Count;
                        i = this.instruction.Count;
                    }
                }
                    if (i >= this.instruction.Count)       //If last instruction there is no more instructions to fetch
                    {
                        this.cycle[c].setInstructionFetch("", "", "", "", "", "", "", "", "", "", "", "");
                    }
                    else
                    {
                        this.cycle[c].setInstructionFetch(
                            this.instruction[i].getOpcode().getOpcodeString(),
                            this.instruction[i].getLineNumber().ToString(),
                            this.cycle[c - 1].EXMEM_instructionType,
                            this.cycle[c - 1].EXMEM_ALUOutput,
                            this.instruction[i].getInstruction(),
                            this.instruction[i].getInstructionType(),
                            this.instruction[i].getInstructionLine(),
                            this.registers[Convert.ToInt32(this.instruction[i].getOpcode().rsO, 2)].getValue(),
                            this.registers[Convert.ToInt32(this.instruction[i].getOpcode().rtO, 2)].getValue(),
                            this.registers[Convert.ToInt32(this.instruction[i].getOpcode().rdO, 2)].getValue(),
                            this.registers[Convert.ToInt32(this.instruction[i].getOpcode().bseO, 2)].getValue(),
                            this.instruction[i].getOpcode().getOpcodeString().Substring(18)
                            );
                    }
                
            }
            return i;
        }

        private void InstructionDecode(int i, int c)
        {
            if (c == 0)
            {
                this.cycle[c].setInstructionDecode("", "", "", "", "", "", "", "", "", "");
            }
            else
            {
                this.cycle[c].setInstructionDecode(
                    this.cycle[c - 1].IFID_rs,
                    this.cycle[c - 1].IFID_rt,
                    this.cycle[c - 1].IFID_rd,
                    this.cycle[c - 1].IFID_bse,
                    this.cycle[c - 1].IFID_imm,
                    this.cycle[c - 1].IFID_IR,
                    this.cycle[c - 1].IFID_NPC,
                    this.cycle[c - 1].IFID_instruction,
                    this.cycle[c - 1].IFID_instructionType,
                    this.cycle[c - 1].IFID_instructionLine
                    );
            }
            
        }

        private void Execution(int i, int c)
        {
            if (c == 0)
            {
                this.cycle[c].setExecution("", "", "", "", "", "", "", "");
            }
            else
            {
                this.cycle[c].setExecution(
                    this.cycle[c - 1].IDEX_A,
                    this.cycle[c - 1].IDEX_B,
                    this.cycle[c - 1].IDEX_IMM,
                    this.cycle[c - 1].IDEX_IR,
                    this.cycle[c - 1].IDEX_NPC,
                    this.cycle[c - 1].IDEX_instruction,
                    this.cycle[c - 1].IDEX_instructionType,
                    this.cycle[c - 1].IDEX_instructionLine
                    );
            }
        }

        private Boolean MemoryAccess(int i, int c)
        {
            if (c == 0)
            {
                this.cycle[c].setMemoryAccess("", "", "", "", "");
            }
            else
            {
                if (!(checkLoadAddress(this.cycle[c - 1].EXMEM_ALUOutput, this.cycle[c - 1].EXMEM_instructionType)))
                    {
                    return false;
                    }
                this.storeDouble(this.cycle[c - 1].EXMEM_ALUOutput, c);
                this.cycle[c].setMemoryAccess(
                    this.cycle[c - 1].EXMEM_IR,
                    this.cycle[c - 1].EXMEM_ALUOutput,
                    this.loadDouble(this.cycle[c - 1].EXMEM_ALUOutput, c),
                    this.cycle[c - 1].EXMEM_instruction,
                    this.cycle[c - 1].EXMEM_instructionType
                    );
            }
            return true;           
        }

        private void WriteBack(int i)
        {
            if (i != 0)
            {
                if (this.cycle[i - 1].MEMWB_IR != "")  //  if Instruction Fetched is not null -> pipeline map IF is true
                {
                    this.cycle[i].WB = true;
                }

                if (this.cycle[i - 1].MEMWB_instructionType == "Register-Register ALU Instruction")
                {
                    this.registers[Convert.ToInt32(this.cycle[i - 1].MEMWB_IR.Substring(19, 5), 2)].setRegisterValue(this.cycle[i - 1].MEMWB_ALUOutput);
                }
                else if (this.cycle[i - 1].MEMWB_instructionType == "Register-Immediate ALU Instruction")
                {
                    if (this.cycle[i - 1].MEMWB_instruction == "DADDIU")
                    {
                        this.registers[Convert.ToInt32(this.cycle[i - 1].MEMWB_IR.Substring(13, 5), 2)].setRegisterValue(this.cycle[i - 1].MEMWB_ALUOutput);
                    }
                }
                else if (this.cycle[i - 1].MEMWB_instructionType == "Load Instruction")
                {
                    this.registers[Convert.ToInt32(this.cycle[i - 1].MEMWB_IR.Substring(13, 5), 2)].setRegisterValue(this.cycle[i - 1].MEMWB_LMD);
                }
                else
                {

                }
            }
        }
    }
}

