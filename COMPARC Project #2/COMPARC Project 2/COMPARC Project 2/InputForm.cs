﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COMPARC_Project_2
{
    public partial class InputForm : Form
    {
        private Program program;
        private int viewCycle;

        public InputForm()
        {
            InitializeComponent();
        }

        private void InputForm_Load(object sender, EventArgs e)
        {
            opCodeGridView.Columns.Add("Line Number", "Line Number");
            opCodeGridView.Columns.Add("Instruction", "Instruction");
            opCodeGridView.Columns.Add("Op Code", "Op Code");

            memoryGridView.Columns.Add("Memory Location", "Memory Location");
            memoryGridView.Columns.Add("Value", "Value");
            initializeMemory();

            this.viewCycle = 0;

        }

        #region Button Controls

        private void simulateBtn_Click(object sender, EventArgs e)
        {
           
            this.program = new Program(programTB.Lines, getRegisterTextBox(), getMemoryTextBox());

            if(this.program.getInstructionsValid())
                setOpCodeGridView();

            this.displayInternalPipelineRegistersInOneLabel();
            this.displayPipelineMap();
            this.refreshRegisters();
            this.displayInternalPipelineRegisters(this.viewCycle);

            gotoCycleTB.Text = (viewCycle + 1).ToString();
            gotoCycleBtn.Visible = true;
            gotoCycleTB.Visible = true;
            nextCycleBtn.Visible = true;
            lastCycleBtn.Visible = true;
            prevCycleBtn.Visible = false;
            firstCycleBtn.Visible = false;
        }

        private void nextCycleBtn_Click(object sender, EventArgs e)
        {
            this.viewCycle++;
            gotoCycleTB.Text = (viewCycle + 1).ToString();
            this.displayInternalPipelineRegisters(this.viewCycle);

            if (this.viewCycle < (this.program.getNumCycles() - 1))
            {
                nextCycleBtn.Visible = true;
                lastCycleBtn.Visible = true;
            }
            else
            {
                nextCycleBtn.Visible = false;
                lastCycleBtn.Visible = false;
            }
            if (this.viewCycle > 0)
            {
                prevCycleBtn.Visible = true;
                firstCycleBtn.Visible = true;
            }
        }

        private void prevCycleBtn_Click(object sender, EventArgs e)
        {
            this.viewCycle--;
            gotoCycleTB.Text = (viewCycle + 1).ToString();
            this.displayInternalPipelineRegisters(this.viewCycle);

            if (this.viewCycle > 0)
            {
                prevCycleBtn.Visible = true;
                firstCycleBtn.Visible = true;
            }
            else
            {
                prevCycleBtn.Visible = false;
                firstCycleBtn.Visible = false;
            }
            if (this.viewCycle < (this.program.getNumCycles() - 1))
            {
                nextCycleBtn.Visible = true;
                lastCycleBtn.Visible = true;
            }
        }

        private void lastCycleBtn_Click(object sender, EventArgs e)
        {
            this.viewCycle = this.program.getNumCycles() - 1;
            gotoCycleTB.Text = (viewCycle + 1).ToString();
            this.displayInternalPipelineRegisters(this.viewCycle);
            nextCycleBtn.Visible = false;
            lastCycleBtn.Visible = false;
            prevCycleBtn.Visible = true;
            firstCycleBtn.Visible = true;
        }

        private void firstCycleBtn_Click(object sender, EventArgs e)
        {
            this.viewCycle = 0;
            gotoCycleTB.Text = (viewCycle + 1).ToString();
            this.displayInternalPipelineRegisters(this.viewCycle);
            nextCycleBtn.Visible = true;
            lastCycleBtn.Visible = true;
            prevCycleBtn.Visible = false;
            firstCycleBtn.Visible = false;
        }

        private void gotoCycleBtn_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(gotoCycleTB.Text) > 0 && Convert.ToInt32(gotoCycleTB.Text) <= this.program.getNumCycles())
            {
                this.viewCycle = Convert.ToInt32(gotoCycleTB.Text) - 1;
                this.displayInternalPipelineRegisters(this.viewCycle);
                if (this.viewCycle  == 0)
                {
                    prevCycleBtn.Visible = false;
                    firstCycleBtn.Visible = false;
                    lastCycleBtn.Visible = true;
                    nextCycleBtn.Visible = true;
                }
                else if (this.viewCycle == (this.program.getNumCycles() - 1))
                {
                    prevCycleBtn.Visible = true;
                    firstCycleBtn.Visible = true;
                    lastCycleBtn.Visible = false;
                    nextCycleBtn.Visible = false;
                }
                else
                {
                    prevCycleBtn.Visible = true;
                    firstCycleBtn.Visible = true;
                    lastCycleBtn.Visible = true;
                    nextCycleBtn.Visible = true;
                }
            }
            else
            {
                MessageBox.Show("Cycle Out of Bounds");
            }
            
        }

        #endregion

        #region text box getters

        private String[] getRegisterTextBox()
        {
            String[] registers = new String[32] {r0TextBox.Text, r1TextBox.Text, r2TextBox.Text, r3TextBox.Text, r4TextBox.Text, 
                                                r5TextBox.Text, r6TextBox.Text, r7TextBox.Text, r8TextBox.Text, r9TextBox.Text, 
                                                r10TextBox.Text, r11TextBox.Text, r12TextBox.Text, r13TextBox.Text, r14TextBox.Text,
                                                r15TextBox.Text, r16TextBox.Text, r17TextBox.Text, r18TextBox.Text, r19TextBox.Text,
                                                r20TextBox.Text, r21TextBox.Text, r22TextBox.Text, r23TextBox.Text, r24TextBox.Text,
                                                r25TextBox.Text, r26TextBox.Text, r27TextBox.Text, r28TextBox.Text, r29TextBox.Text,
                                                r30TextBox.Text, r31TextBox.Text};
            return registers;

        }

        private String[] getMemoryTextBox()
        {
            String[] memory = new String[8192];

            for (int i = 0; i < 8192; i++)
            {
                memory[i] = memoryGridView.Rows[i].Cells["Value"].Value.ToString().ToUpper().Replace(" ", String.Empty);
            }

            return memory;
        }

        #endregion

        #region set/display/view functions

        private String addSpaces(string text)
        {
            return text.Substring(0, 4) + " " + text.Substring(4, 4) + " " + text.Substring(8, 4) + " " + text.Substring(12, 4);
        }

        private void refreshRegisters()
        {
            r1TextBox.Text = this.addSpaces(this.program.getRegisterData(1));
            r2TextBox.Text = this.addSpaces(this.program.getRegisterData(2));
            r3TextBox.Text = this.addSpaces(this.program.getRegisterData(3));
            r4TextBox.Text = this.addSpaces(this.program.getRegisterData(4));
            r5TextBox.Text = this.addSpaces(this.program.getRegisterData(5));
            r6TextBox.Text = this.addSpaces(this.program.getRegisterData(6));
            r7TextBox.Text = this.addSpaces(this.program.getRegisterData(7));
            r8TextBox.Text = this.addSpaces(this.program.getRegisterData(8));
            r9TextBox.Text = this.addSpaces(this.program.getRegisterData(9));
            r10TextBox.Text = this.addSpaces(this.program.getRegisterData(10));
            r11TextBox.Text = this.addSpaces(this.program.getRegisterData(11));
            r12TextBox.Text = this.addSpaces(this.program.getRegisterData(12));
            r13TextBox.Text = this.addSpaces(this.program.getRegisterData(13));
            r14TextBox.Text = this.addSpaces(this.program.getRegisterData(14));
            r15TextBox.Text = this.addSpaces(this.program.getRegisterData(15));
            r16TextBox.Text = this.addSpaces(this.program.getRegisterData(16));
            r17TextBox.Text = this.addSpaces(this.program.getRegisterData(17));
            r18TextBox.Text = this.addSpaces(this.program.getRegisterData(18));
            r19TextBox.Text = this.addSpaces(this.program.getRegisterData(19));
            r20TextBox.Text = this.addSpaces(this.program.getRegisterData(20));
            r21TextBox.Text = this.addSpaces(this.program.getRegisterData(21));
            r22TextBox.Text = this.addSpaces(this.program.getRegisterData(22));
            r23TextBox.Text = this.addSpaces(this.program.getRegisterData(23));
            r24TextBox.Text = this.addSpaces(this.program.getRegisterData(24));
            r25TextBox.Text = this.addSpaces(this.program.getRegisterData(25));
            r26TextBox.Text = this.addSpaces(this.program.getRegisterData(26));
            r27TextBox.Text = this.addSpaces(this.program.getRegisterData(27));
            r28TextBox.Text = this.addSpaces(this.program.getRegisterData(28));
            r29TextBox.Text = this.addSpaces(this.program.getRegisterData(29));
            r30TextBox.Text = this.addSpaces(this.program.getRegisterData(30));
            r31TextBox.Text = this.addSpaces(this.program.getRegisterData(31));
        }

        private void setOpCodeGridView()
        {
            int hex = 0x0000;
            opCodeGridView.Rows.Clear();

            for (int i = 0; i < this.program.getInstructionLength(); i++)
            {
                opCodeGridView.Rows.Add(hex.ToString("x").ToUpper(), this.program.getInstructionLine(i), this.program.getInstructionOpCode(i));
                hex += 4;
            }
        }

        private void initializeMemory()
        {
            int hex = 0x3FFF;

            for (int i = 0; i < 8192; i++)
            {
                memoryGridView.Rows.Add(hex.ToString("x").ToUpper(), "00");
                hex--;
            }
        }

        private void displayInternalPipelineRegistersInOneLabel()
        {
            internalPipeRegLbl.Text = "";
            for (int i = 0; i < program.getNumCycles(); i++)
            {
                internalPipeRegLbl.Text += "Cycle: " + (i + 1).ToString() + "\n";
                internalPipeRegLbl.Text += "Instruction Fetch \n";
                internalPipeRegLbl.Text += "    IF/ID.IR  = " + program.getIFID_IR(i) + "\n";
                internalPipeRegLbl.Text += "    IF/ID.NPC = " + program.getIFID_NPC(i) + "\n";
                internalPipeRegLbl.Text += "    IF/ID.PC  = " + program.getIFID_PC(i) + "\n";
                internalPipeRegLbl.Text += "Instruction Decode \n";
                internalPipeRegLbl.Text += "    ID/EX.A   = " + program.getIDEX_A(i) + "\n";
                internalPipeRegLbl.Text += "    ID/EX.B   = " + program.getIDEX_B(i) + "\n";
                internalPipeRegLbl.Text += "    ID/EX.IMM = " + program.getIDEX_IMM(i) + "\n";
                internalPipeRegLbl.Text += "    ID/EX.IR  = " + program.getIDEX_IR(i) + "\n";
                internalPipeRegLbl.Text += "    ID/EX.NPC = " + program.getIDEX_NPC(i) + "\n";
                internalPipeRegLbl.Text += "Execution \n";
                internalPipeRegLbl.Text += "    EX/MEM.ALUOutput   = " + program.getEXMEM_ALUOutput(i) + "\n";
                internalPipeRegLbl.Text += "    EX/MEM.COND   = " + program.getEXMEM_Cond(i) + "\n";
                internalPipeRegLbl.Text += "    EX/MEM.IR   = " + program.getEXMEM_IR(i) + "\n";
                internalPipeRegLbl.Text += "    EX/MEM.B   = " + program.getEXMEM_B(i) + "\n";
                internalPipeRegLbl.Text += "Memory Access \n";
                internalPipeRegLbl.Text += "    MEM/WB.LMD   = " + program.getMEMWB_LMD(i) + "\n";
                internalPipeRegLbl.Text += "    MEM/WB.Range   = " + program.getMEMWB_Range(i) + "\n";
                internalPipeRegLbl.Text += "    MEM/WB.IR  = " + program.getMEMWB_IR(i) + "\n";
                internalPipeRegLbl.Text += "    MEM/WB.ALUOutput   = " + program.getMEMWB_ALUOutput(i) + "\n";
                internalPipeRegLbl.Text += "Write Back \n";
                internalPipeRegLbl.Text += "    Rn = " + program.getMEMWB_ALUOutput(i); //   program.getWriteBackRegister(i) + " = " + program.getMEMWB_ALUOutput(i);
                internalPipeRegLbl.Text += "\n";
            }
        }

        private void displayInternalPipelineRegisters(int i)
        {
            cycleLbl.Text = "Cycle " + (i + 1).ToString();
            IFID_IR_Lbl.Text = program.getIFID_IR(i);
            IFID_NPC_Lbl.Text = program.getIFID_NPC(i);
            IFID_PC_Lbl.Text = program.getIFID_PC(i);
        }

        private void displayPipelineMap()
        {
            pipelineMapGridView.Columns.Clear();
            pipelineMapGridView.Columns.Add("Instruction", "Instruction");
            for (int i = 0; i < program.getNumCycles(); i++)
            {
                pipelineMapGridView.Columns.Add("Cycle " + (i + 1), "Cycle " + (i + 1));
            }
        }

        #endregion

        




    }
}
