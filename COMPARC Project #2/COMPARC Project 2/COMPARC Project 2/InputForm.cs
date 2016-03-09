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
        private String[] program;

        public InputForm()
        {
            InitializeComponent();
        }

        private void simulateBtn_Click(object sender, EventArgs e)
        {
            this.program = programTB.Lines;
        }

        public String[] getProgram()
        {
            return this.program;
        }
    }
}
