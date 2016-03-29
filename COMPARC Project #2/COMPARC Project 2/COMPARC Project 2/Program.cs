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

            if (isProgramValid())
            {
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
                if (this.instructionsValid)
                    this.pipeline();
            }              
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

        public bool isInstructionBranch(int lineNum)
        {
            if (this.instruction[lineNum].getInstructionType() == "Branch Instruction")
            {
                return true;
            }
            else
            {
                return false;
            }            
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

        private Boolean isProgramValid()
        {
            if (this.registersValid && this.memoryValid && this.instructionsValid)
                return true;
            else
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

        #region Pipeline

        private void pipeline()
        {
            int i = 0;
            int c = 0;
            int stall = 0;
            Boolean addressRange = true ;
            Boolean executionError = false;
            int totalCycles = this.instruction.Count + 3;
            do
            {
                this.cycle.Add(new Cycle());
                this.numCycles++;

                stall = i;
                i = getInstruction(i, c);

                if (i < this.instruction.Count)
                {
                        this.InstructionFetch(i, c);
                        if (this.InstructionDecode(i, c))
                        {
                            Console.WriteLine("Data Hazard 1 Stall @ cycle" + c);
                            i = stall;
                            do
                            {
                                
                                this.InstructionFetch(i, c);
                                this.InstructionDecode(i, c);
                                executionError = this.Execution(i, c); if (executionError == true) break;
                                addressRange = this.MemoryAccess(i, c); if (addressRange == false) break;
                                this.WriteBack(c);
                                c++;

                                this.cycle.Add(new Cycle());
                                this.numCycles++;

                            } while (this.InstructionDecode(i, c));

                            this.cycle.RemoveAt(this.cycle.Count - 1);
                            this.numCycles--;
                        }
                        else
                        {
                            #region FLUSH
                            if (this.instruction[i].getInstructionType() == "Branch Instruction")
                            {
                                Console.WriteLine("branch at cycle: " + c);
                                this.InstructionFetch(i, c);
                                this.InstructionDecode(i, c);
                                executionError = this.Execution(i, c); if (executionError == true) break;
                                addressRange = this.MemoryAccess(i, c); if (addressRange == false) break;
                                this.WriteBack(c);

                                c++;

                                stall = i;
                                i = getInstruction(i, c);
                                this.cycle.Add(new Cycle());
                                this.numCycles++;
                                this.InstructionFetch(i, c);
                                if (this.InstructionDecode(i, c))
                                {
                                    i = stall;
                                    do
                                    {
                                        Console.WriteLine("STALLLLL");
                                        this.InstructionFetch(i, c);
                                        this.InstructionDecode(i, c);
                                        executionError = this.Execution(i, c); if (executionError == true) break;
                                        addressRange = this.MemoryAccess(i, c); if (addressRange == false) break;
                                        this.WriteBack(c);
                                        c++;

                                        this.cycle.Add(new Cycle());
                                        this.numCycles++;

                                    } while (this.InstructionDecode(i, c));

                                    this.cycle.RemoveAt(this.cycle.Count - 1);
                                    this.numCycles--;

                                    i = getInstruction(i, c);
                                    this.cycle.Add(new Cycle());
                                    this.numCycles++;
                                    this.InstructionFetch(i, c);
                                    this.InstructionDecode(i, c);
                                    executionError = this.Execution(i, c); if (executionError == true) break;
                                    addressRange = this.MemoryAccess(i, c); if (addressRange == false) break;
                                    this.WriteBack(c);

                                    c++;

                                    i = getInstruction(i, c);
                                    this.cycle.Add(new Cycle());
                                    this.numCycles++;
                                    this.InstructionFetch(i, c);
                                    executionError = this.Execution(i, c); if (executionError == true) break;
                                    addressRange = this.MemoryAccess(i, c); if (addressRange == false) break;
                                    this.WriteBack(c);

                                    c++;

                                    i = getInstruction(i, c);
                                    this.cycle.Add(new Cycle());
                                    this.numCycles++;
                                    this.InstructionFetch(i, c);
                                    addressRange = this.MemoryAccess(i, c); if (addressRange == false) break;
                                    this.WriteBack(c);

                                    c++;

                                    i = getInstruction(i, c);
                                    this.cycle.Add(new Cycle());
                                    this.numCycles++;
                                    this.InstructionFetch(i, c);
                                    this.WriteBack(c);

                                    c++;
                                }
                                else
                                {
                                    executionError = this.Execution(i, c); if (executionError == true) break;
                                    addressRange = this.MemoryAccess(i, c); if (addressRange == false) break;
                                    this.WriteBack(c);
                                    c++;

                                    i = getInstruction(i, c);
                                    this.cycle.Add(new Cycle());
                                    this.numCycles++;
                                    this.InstructionFetch(i, c);
                                    executionError = this.Execution(i, c); if (executionError == true) break;
                                    addressRange = this.MemoryAccess(i, c); if (addressRange == false) break;
                                    this.WriteBack(c);

                                    c++;

                                    i = getInstruction(i, c);
                                    this.cycle.Add(new Cycle());
                                    this.numCycles++;
                                    this.InstructionFetch(i, c);
                                    addressRange = this.MemoryAccess(i, c); if (addressRange == false) break;
                                    this.WriteBack(c);

                                    c++;

                                    i = getInstruction(i, c);
                                    this.cycle.Add(new Cycle());
                                    this.numCycles++;
                                    this.InstructionFetch(i, c);
                                    this.WriteBack(c);

                                    c++;
                                }

                                
                            }
                            #endregion
                            else
                            {
                                executionError = this.Execution(i, c); if (executionError == true) break;
                                addressRange = this.MemoryAccess(i, c); if (addressRange == false) break;
                                this.WriteBack(c);
                                c++;
                            }
                        }   
                }
                else
                {
                    this.InstructionFetch(i, c);
                    if(this.InstructionDecode(i, c))
                    {
                        i = stall; 
                        do
                        {
                            Console.WriteLine("STALLLLL");
                            this.InstructionFetch(i, c);
                            this.InstructionDecode(i, c);
                            executionError = this.Execution(i, c); if (executionError == true) break;
                            addressRange = this.MemoryAccess(i, c); if (addressRange == false) break;
                            this.WriteBack(c);
                            c++;

                            this.cycle.Add(new Cycle());
                            this.numCycles++;

                        } while (this.InstructionDecode(i, c));

                        this.cycle.RemoveAt(this.cycle.Count - 1);
                        this.numCycles--;
                    }
                    else
                    {
                        executionError = this.Execution(i, c); if (executionError == true) break;
                        addressRange = this.MemoryAccess(i, c); if (addressRange == false) break;
                        this.WriteBack(c);
                        c++;
                    }
                }
                
            } while (i < totalCycles);

            if (addressRange == false || executionError == true)                  //break if out of address range
            {
                this.cycle = null;
                this.cycle = new List<Cycle>();
                this.cycle.Add(new Cycle());
                this.numCycles = 0;
            }    
        }

        private int getInstruction(int i, int c)
        {
            int temp = i;

            if (c == 0)
            {
                return 0;
            }

            for (i = 0; i < this.instruction.Count; i++)
            {
                if (this.cycle[c - 1].IFID_NPC != "")
                {
                    if (instruction[i].getLineNumber() * 4 == Convert.ToInt32(this.cycle[c - 1].IFID_NPC, 16))
                    {
                        return i;
                    }
                }
            }

            return temp+1;
        }

        private void InstructionFetch(int i, int c)      // Returns the instruction array number of the next instruction
        {
            if (i == 0)     //If first instruction there is no previous cycle to get EXMEM_instructionType and EXMEM_ALUOutput
            {
                this.cycle[c].setInstructionFetch(
                    this.instruction[i].getOpcode().getOpcodeString(),
                    this.instruction[i].getLineNumber().ToString(),
                    "",
                    "4",
                    this.instruction[i].getInstruction(),
                    this.instruction[i].getInstructionType(),
                    this.instruction[i].getInstructionLine(),
                    Convert.ToInt32(this.instruction[i].getOpcode().rsO, 2).ToString(),
                    Convert.ToInt32(this.instruction[i].getOpcode().rtO, 2).ToString(),
                    Convert.ToInt32(this.instruction[i].getOpcode().rdO, 2).ToString(),
                    Convert.ToInt32(this.instruction[i].getOpcode().bseO, 2).ToString(),
                    this.instruction[i].getOpcode().getOpcodeString().Substring(18)
                    );
            }
            else if (i >= this.instruction.Count)
            {
                this.cycle[c].setInstructionFetch("", "", this.cycle[c - 1].EXMEM_instructionType, this.cycle[c - 1].EXMEM_ALUOutput, "", "", "", "", "", "", "", "");
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
                    Convert.ToInt32(this.instruction[i].getOpcode().rsO, 2).ToString(),
                    Convert.ToInt32(this.instruction[i].getOpcode().rtO, 2).ToString(),
                    Convert.ToInt32(this.instruction[i].getOpcode().rdO, 2).ToString(),
                    Convert.ToInt32(this.instruction[i].getOpcode().bseO, 2).ToString(),
                    this.instruction[i].getOpcode().getOpcodeString().Substring(18)
                    );
            }
        }

        private Boolean InstructionDecode(int i, int c)
        {
            if (c == 0)
            {
                this.cycle[c].setInstructionDecode("", "", "", "", "", "", "", "", "", "");
            }
            else
            {

                if (i >= this.instruction.Count + 1)
                {
                    this.cycle[c].setInstructionDecode("", "", "", "", "", "", "", "", "", "");
                }
                else
                {
                    if(this.checkDataHazard(
                        c,
                        Convert.ToInt32(this.cycle[c - 1].IFID_rs),
                        Convert.ToInt32(this.cycle[c - 1].IFID_rt),
                        Convert.ToInt32(this.cycle[c - 1].IFID_rd),
                        Convert.ToInt32(this.cycle[c - 1].IFID_bse)
                        ))
                    {
                        Console.WriteLine("id returns true");
                        this.cycle[c].setInstructionDecode("", "", "", "", "", "", "", "", "", "");
                        return true;
                    }

                    this.cycle[c].setInstructionDecode(
                    this.registers[Convert.ToInt32(this.cycle[c - 1].IFID_rs)].getValue(),
                    this.registers[Convert.ToInt32(this.cycle[c - 1].IFID_rt)].getValue(),
                    this.registers[Convert.ToInt32(this.cycle[c - 1].IFID_rd)].getValue(),
                    this.registers[Convert.ToInt32(this.cycle[c - 1].IFID_bse)].getValue(),
                    this.cycle[c - 1].IFID_imm,
                    this.cycle[c - 1].IFID_IR,
                    this.cycle[c - 1].IFID_NPC,
                    this.cycle[c - 1].IFID_instruction,
                    this.cycle[c - 1].IFID_instructionType,
                    this.cycle[c - 1].IFID_instructionLine
                    );

                    this.setDataHazard(
                        c,
                        Convert.ToInt32(this.cycle[c - 1].IFID_rs),
                        Convert.ToInt32(this.cycle[c - 1].IFID_rt),
                        Convert.ToInt32(this.cycle[c - 1].IFID_rd),
                        Convert.ToInt32(this.cycle[c - 1].IFID_bse)
                        );

                }
                
            }
            return false;
        }

        private Boolean Execution(int i, int c)
        {
            if (c == 0)
            {
                this.cycle[c].setExecution("", "", "", "", "", "", "", "");
            }
            else
            {
                return this.cycle[c].setExecution(
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
            return false;
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
                    this.clearDataHazard(Convert.ToInt32(this.cycle[i - 1].MEMWB_IR.Substring(19, 5), 2));
                }
                else if (this.cycle[i - 1].MEMWB_instructionType == "Register-Immediate ALU Instruction")
                {
                    if (this.cycle[i - 1].MEMWB_instruction == "DADDIU")
                    {
                        this.registers[Convert.ToInt32(this.cycle[i - 1].MEMWB_IR.Substring(13, 5), 2)].setRegisterValue(this.cycle[i - 1].MEMWB_ALUOutput);
                        this.clearDataHazard(Convert.ToInt32(this.cycle[i - 1].MEMWB_IR.Substring(13, 5), 2));
                    }
                }
                else if (this.cycle[i - 1].MEMWB_instructionType == "Load Instruction")
                {
                    this.registers[Convert.ToInt32(this.cycle[i - 1].MEMWB_IR.Substring(13, 5), 2)].setRegisterValue(this.cycle[i - 1].MEMWB_LMD);
                    this.clearDataHazard(Convert.ToInt32(this.cycle[i - 1].MEMWB_IR.Substring(13, 5), 2));
                }
            }
        }

        #endregion

        #region Data Hazards

        private void setDataHazard(int c, int rs, int rt, int rd, int bse)
        {
            if(this.cycle[c - 1].IFID_instructionType == "Register-Register ALU Instruction")
            {
                if (rd != 0)
                {
                    this.registers[rd].busy = true;
                } 
            }
            else if (this.cycle[c - 1].IFID_instructionType == "Register-Immediate ALU Instruction" || this.cycle[c - 1].IFID_instructionType == "Load Instruction")
            {
                if (rt != 0)
                {
                    this.registers[rt].busy = true;
                }
            }
        }

        private void clearDataHazard(int i)
        {
            this.registers[i].busy = false;
        }

        private Boolean checkDataHazard(int c, int rs, int rt, int rd, int bse)
        {
            if (this.cycle[c - 1].IFID_instructionType == "Store Instruction")
            {
                if (this.registers[rt].busy)
                {
                    Console.WriteLine("Stall because register " + rt.ToString() + " is busy.");
                    return true;   
                }
            }
            else if (this.cycle[c - 1].IFID_instructionType == "Register-Register ALU Instruction")
            {
                if (this.registers[rs].busy || this.registers[rt].busy )
                {
                    Console.WriteLine("Stall because register " + rs.ToString() + " or register " + rt.ToString() + " is busy.");
                    return true;   
                }
            }
            else if (this.cycle[c - 1].IFID_instructionType == "Register-Immediate ALU Instruction")
            {
                if (this.registers[rs].busy)
                {
                    Console.WriteLine("Stall because register " + rs.ToString() + " is busy.");
                    return true;   
                }
            }
            else if (this.cycle[c - 1].IFID_instructionType == "Branch Instruction")
            {
                if (this.cycle[c - 1].IFID_instruction == "BNEC")
                {
                    if (this.registers[rs].busy || this.registers[rt].busy)
                    {
                        Console.WriteLine("Branch stall because register " + rs.ToString() + " or register " + rt.ToString() + " is busy.");
                        return true;
                    }
                }

            }

            return false;
        }

        #endregion
    }
}
