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
            this.program = new Program(programTB.Lines);
        }

        private void registerButton_Click(object sender, EventArgs e)
        {

        }

        
    }
}
