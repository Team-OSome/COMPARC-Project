using System;
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

        }

        private void simulateBtn_Click(object sender, EventArgs e)
        {
            String[] registers = new String[32] {r0TextBox.Text, r1TextBox.Text, r2TextBox.Text, r3TextBox.Text, r4TextBox.Text, 
                                                r5TextBox.Text, r6TextBox.Text, r7TextBox.Text, r8TextBox.Text, r9TextBox.Text, 
                                                r10TextBox.Text, r11TextBox.Text, r12TextBox.Text, r13TextBox.Text, r14TextBox.Text,
                                                r15TextBox.Text, r16TextBox.Text, r17TextBox.Text, r18TextBox.Text, r19TextBox.Text,
                                                r20TextBox.Text, r21TextBox.Text, r22TextBox.Text, r23TextBox.Text, r24TextBox.Text,
                                                r25TextBox.Text, r26TextBox.Text, r27TextBox.Text, r28TextBox.Text, r29TextBox.Text,
                                                r30TextBox.Text, r31TextBox.Text};

            this.program = new Program(programTB.Lines, registers);

            if(this.program.getInstructionsValid())
                setOpCodeGridView();

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
            }
        }

        private void registerButton_Click(object sender, EventArgs e)
        {

        }

        private void setOpCodeGridView()
        {
            int hex = 0x00;
            opCodeGridView.Rows.Clear();

            for (int i = 0; i < this.program.getInstructionLength(); i++)
            {
                opCodeGridView.Rows.Add(hex.ToString("x").ToUpper(), this.program.getInstructionLine(i), this.program.getInstructionOpCode(i));
                hex += 4;
            }
        }

        private void initializeMemory()
        {
            int hex = 0x2000;

            for (int i = 0; i < 8192; i++)
            {
                memoryGridView.Rows.Add(hex.ToString("x").ToUpper(), "00");
                hex++;
            }
        }

        
    }
}
