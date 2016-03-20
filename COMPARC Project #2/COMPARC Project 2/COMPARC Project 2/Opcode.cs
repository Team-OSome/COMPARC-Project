using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace COMPARC_Project_2
{  
    
    public class Opcode
    {
        #region Variables
        private char opcodeType;     //  J-type, I-type, etc.
        private String opcodeString="";
        private Boolean valid = false;

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
        public String rsO { get; private set; } 
        public String rdO { get; private set; }
        public String rtO { get; private set; }
        private int rsDec;
        private int rdDec;
        private int rtDec;
        public String offsetO { get; private set; }
        private int offsetDecO;
        public String immediateO { get; private set; }
        private int immediateDec;
        public String bseO { get; private set; }
        private int bseDec;

        private int lineNum;

        private String hexOpcodeString;

        #endregion

        public Opcode(String instruction, String rd, String rs, String rt, String immediate, String offset, String bse, int lineNum)
        {
            this.instruction = instruction;
            this.rd = rd;
            this.rs = rs;
            this.rt = rt;
            this.immediate = immediate;
            this.offset = offset;
            this.bse = bse;
            this.lineNum = lineNum;

            this.opcodeType= getInstructionType(this.instruction);
            //Console.WriteLine("Type:"+this.opcodeType);
            this.instructionO = getInstructionOpcode(this.instruction);

        }

        public char getInstructionType(String instruction)
        {
            switch (instruction)
            {
                case "DSUBU": 
                case "DDIVU":
                case "DMODU":
                case "SLT":
                case "SELNEZ": return 'R';
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
                case "DDIVU":
                case "DMODU":
                case "SLT":
                case "SELNEZ": return "000000";
                case "BNEC": return "011000";
                case "LD": return "110111";
                case "SD": return "111111";
                case "DADDIU": return "011001";
                case "BC": return "110010";
                default: return "";
            }
        }

        public Boolean getValid()
        {
            return this.valid;
        }

        public String getOpcodeString()
        {
            return opcodeString;
        }

        public String getOffsetO()
        {
            return this.offsetO;
        }

        public void setParameters()
        {
            this.opcodeString += instructionO + " ";
            #region type R algo
            if (opcodeType.Equals('R'))
            {
                try
                {
                    //Console.WriteLine("0");
                    this.rsO = this.rs.TrimStart('R');
                    this.rtO = this.rt.TrimStart('R');
                    this.rdO = this.rd.TrimStart('R');
                    //Console.WriteLine("1");
                    this.rsDec = Int32.Parse(rsO);
                    this.rtDec = Int32.Parse(rtO);
                    this.rdDec = Int32.Parse(rdO);

                    this.rsO = Convert.ToString(rsDec, 2);
                    this.rtO = Convert.ToString(rtDec, 2);
                    this.rdO = Convert.ToString(rdDec, 2);

                    this.rsO = rsO.PadLeft(5, '0');
                    this.rtO = rtO.PadLeft(5, '0');
                    this.rdO = rdO.PadLeft(5, '0');

                    if (rsDec > 31 || rtDec > 31 || rdDec > 31)
                        this.valid = false;
                    else
                        this.valid = true;
                }
                catch (Exception e) 
                {
                    System.Windows.Forms.MessageBox.Show(e.Message);
                }
            }
            #endregion

            #region type I algo //BNEC nalang kulang... does not have offset
            if (opcodeType.Equals('I'))
            {
                #region BNEC and DADDIU
                if (this.instruction == "BNEC" || this.instruction == "DADDIU")
                {
                    try
                    {
                        this.rsO = this.rs.TrimStart('R');
                        this.rtO = this.rt.TrimStart('R');

                        this.rsDec = Int32.Parse(rsO);
                        this.rtDec = Int32.Parse(rtO);

                        this.rsO = Convert.ToString(rsDec, 2);
                        this.rtO = Convert.ToString(rtDec, 2);

                        this.rsO = rsO.PadLeft(5, '0');
                        this.rtO = rtO.PadLeft(5, '0');

                        if (this.instruction == "BNEC") //this would need offset
                        {
                            //offset would be set through the program.cs
                        }

                        else if (this.instruction == "DADDIU") //this would need immediate
                        {
                            try
                            {
                                this.immediateO = this.immediate.TrimStart('#');
                                this.immediateDec = Int32.Parse(immediateO, System.Globalization.NumberStyles.HexNumber);
                                this.immediateO = Convert.ToString(immediateDec, 2);
                                if (immediateDec > 32768) 
                                {
                                    this.immediateO = immediateO.PadLeft(16, '1');
                                }
                                else
                                {
                                    this.immediateO = immediateO.PadLeft(16, '0');
                                }
                            }
                            catch (Exception e)
                            {
                                System.Windows.Forms.MessageBox.Show(e.Message);
                            }
                        }


                        if (rsDec >= 0 || rtDec <= 31) //immediateDec should be added to the value inside Rt...
                        {
                            this.valid = true;
                        }

                        else
                            this.valid = false;



                    }
                    catch (Exception e)
                    {
                        System.Windows.Forms.MessageBox.Show(e.Message);
                    }

                }
                #endregion
                #region LD and SD
                if (this.instruction == "LD" || this.instruction == "SD")
                {
                    try
                    {
                        this.bseO = this.bse.TrimStart('R');
                        this.rtO = this.rt.TrimStart('R');

                        this.bseDec = Int32.Parse(bseO);
                        this.rtDec = Int32.Parse(rtO);

                        this.bseO = Convert.ToString(bseDec, 2);
                        this.rtO = Convert.ToString(rtDec, 2);

                        this.bseO = bseO.PadLeft(5, '0');
                        this.rtO = rtO.PadLeft(5, '0');

                        this.offsetDec = Int32.Parse(offset, System.Globalization.NumberStyles.HexNumber);

                        if (offsetDec >= 8192 && offsetDec <= 16376) //The value= base + immediate should not surpass memory locations: 2000h-3FFF
                        {                                            //16376 because max offset should be 3FF8-3FFF. if offset is >3FF8, this.valid=false

                            this.offsetO = Convert.ToString(offsetDec, 2);
                            this.offsetO = offsetO.PadLeft(16, '0');

                            this.valid = true;
                        }
                        /*
                        if (offsetDec >= 8192 && offsetDec <= 16383) //The value= base + immediate should not surpass memory locations: 2000h-3FFF
                        {

                            this.offsetO = Convert.ToString(offsetDec, 2);
                            this.offsetO = offsetO.PadLeft(16, '0');

                            this.valid = true;
                        }*/

                        else
                        {
                            this.valid = false;
                        }
                            
                    }
                    catch (Exception e)
                    {
                        System.Windows.Forms.MessageBox.Show(e.Message);
                    }
                }
                #endregion
            }
            #endregion

            #region type J algo
            if (opcodeType.Equals('J')) //wait mali to... we have to solve for offset pa...
            {
                try
                {   /*
                    this.offsetDec = Int32.Parse(offset, System.Globalization.NumberStyles.HexNumber);
                    this.offsetO = Convert.ToString(offsetDec, 2);
                    this.offsetO = offsetO.PadLeft(26, '0');
                    this.opcodeString += offsetO;
                     */

                    this.valid = true;
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message);
                }
            }
            #endregion


            setOpcodeString(); //this is just a test, this should be called in the program.cs
            printOpcodeBinary();
        }

        public void setOpcodeString()
        {
            switch (instruction)
            {
                case "DSUBU":  opcodeString = instructionO + " " + rsO + " " + rtO + " " + rdO + " 00000 101111"; break;
                case "DDIVU":   opcodeString = instructionO + " " + rsO + " " + rtO + " " + rdO + " 00010 011111"; break;
                case "DMODU":  opcodeString = instructionO + " " + rsO + " " + rtO + " " + rdO + " 00011 011111"; break;
                case "SLT":    opcodeString = instructionO + " " + rsO + " " + rtO + " " + rdO + " 00000 101010"; break;
                case "SELNEZ": opcodeString = instructionO + " " + rsO + " " + rtO + " " + rdO + " 00000 110111"; break;
                case "BNEC":   opcodeString = instructionO + " " + rsO + " " + rtO; break; // no offset yet
                case "LD":     opcodeString = instructionO + " " + bseO + " " + rtO + " " + offsetO; break;
                case "SD":     opcodeString = instructionO + " " + bseO + " " + rtO + " " + offsetO; break;
                case "DADDIU": opcodeString= instructionO + " " + rsO + " " + rtO + " " + immediateO ; break;
                case "BC": break;
                default: break;
            }
        }

        public void setOffset(String offset) 
        {   
            this.offset = offset;
            //Console.WriteLine("offset=" + offset);

            this.offsetDec = Int32.Parse(this.offset);
            //Console.WriteLine("offsetDec=" + offsetDec);

           
            this.offsetO = Convert.ToString(offsetDec, 2);
            //Console.WriteLine("offsetO=" + offsetO);

            //Console.WriteLine("OffsetO is: " + offsetO);
            
            //SIGN EXTEND DAPAT TO!!!
            
            if (this.opcodeType.Equals('J'))                //BC
                this.offsetO = offsetO.PadLeft(26, '0'); 
            if (this.opcodeType.Equals('I'))                //BNEC
                this.offsetO = offsetO.PadLeft(16, '0');

            //Console.WriteLine("offsetO="+this.offsetO);
        }

        public void addOpcodeString(String addition)
        {
            this.opcodeString += addition;
        }

        public void printOpcodeBinary()
        {
            //Console.WriteLine(opcodeString);
        }

        public char getOpcodeType()
        {
            return opcodeType;
        }

        public void setHexOpcodeString()
        {
            this.hexOpcodeString = BinaryStringToHexString(this.opcodeString.Replace(" ", ""));
        }

        public String getHexOpcodeString()
        {
            return this.hexOpcodeString;
        }

        public static String BinaryStringToHexString(String binary)
        {
            StringBuilder result = new StringBuilder(binary.Length / 8 + 1);

            // TODO: check all 1's or 0's... Will throw otherwise

            int mod4Len = binary.Length % 8;
            if (mod4Len != 0)
            {
                // pad to length multiple of 8
                binary = binary.PadLeft(((binary.Length / 8) + 1) * 8, '0');
            }
            try
            {
                for (int i = 0; i < binary.Length; i += 8)
                {
                    string eightBits = binary.Substring(i, 8);
                    result.AppendFormat("{0:X2}", Convert.ToByte(eightBits, 2));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Caught error at Binary to Hex String.");
            }

            return result.ToString();
        }

    }
}
