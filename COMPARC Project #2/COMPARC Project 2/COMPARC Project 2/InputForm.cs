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
        private List<Instruction> instruction;

        public InputForm()
        {
            InitializeComponent();
        }

        private void InputForm_Load(object sender, EventArgs e)
        {
            this.instruction = new List<Instruction>();
        }

        private void simulateBtn_Click(object sender, EventArgs e)
        {
            this.initializeInstructionArray(programTB.Lines);
            this.printConsoleProgram();
        }

        private void initializeInstructionArray(String[] instructions)
        {
            for (int i = 0; i < instructions.Length; i++)
            {
                this.instruction.Add(new Instruction(instructions[i]));
            }
        }

        private void printConsoleProgram()
        {
            for (int i = 0; i < instruction.Count; i++)
                Console.WriteLine(this.instruction[i].getInstruction());
        }
    }
}
