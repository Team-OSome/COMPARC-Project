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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void simulateBtn_Click(object sender, EventArgs e)
        {
            if (programTB.Lines.Length > 0)
                label1.Text = "line 1 : " + programTB.Lines[0]; 
        }
    }
}
