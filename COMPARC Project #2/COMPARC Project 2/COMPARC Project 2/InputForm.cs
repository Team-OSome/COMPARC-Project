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

            //MessageBox.Show(this.program.getInstructionOpCode(0));
            //MessageBox.Show(this.program.getInstructionLength().ToString());

            opCodeTextBox.Text = "Line 1" + Environment.NewLine;
            opCodeTextBox.Text += "Line 2" + Environment.NewLine;
            opCodeTextBox.Text += "Line 3" + Environment.NewLine;

            //this.program.pipelineMap(); 
        }

        private void registerButton_Click(object sender, EventArgs e)
        {

        }

        private void setOpCodeTextBox()
        {
            for (int i = 0; i < this.program.getInstructionLength(); i++)
            {
                opCodeTextBox.Text = 
            }
        }

        
    }
}
