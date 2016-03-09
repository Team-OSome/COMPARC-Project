using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COMPARC_Project_2
{
    static class Driver
    {
        /// <summary>
        /// The main entry point for the application. This is where the program flow is.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var inputForm = new InputForm();                //  start by getting the inputs from the Input Form
            Application.Run(inputForm);
        }
    }
}
