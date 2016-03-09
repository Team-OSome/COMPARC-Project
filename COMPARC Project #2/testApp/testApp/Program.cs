using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testApp
{
    class Program
    {
        static void Main(string[] args)
        {

            String line = "DSUBU R1, R2, R3";

            String rs = "";
            String rd = "";
            String rt = "";
            String offset = "";
            String bse = "";
            String immediate = "";

            String[] separators = { ",", " ", "(", ")" };
            String[] words = line.Split(separators, StringSplitOptions.RemoveEmptyEntries); 

            switch (words[0])
            {
                case "DSUBU":
                case "DDIV":
                case "DMODU":
                case "SLT":
                case "SELENEZ": rd = words[1]; rs = words[2]; rt = words[3]; offset = null; immediate = null; bse = null; break;
                case "BNEC": rd = null; rs = words[1]; rt = words[2]; offset = words[3]; immediate = null; bse = null; break;
                case "LD":
                case "SD": rd = null; rs = null; rt = words[1]; offset = words[2]; immediate = null; bse = words[3]; break;
                case "DADDIU": rd = null; rs = words[2]; rt = words[1]; offset = null; immediate = words[3]; bse = null; break;
                case "BC": rd = null; rs = null; rt = null; offset = words[1]; immediate = null; bse = null; break;
            }

            Console.WriteLine(rs);
            Console.WriteLine(rd);
            Console.WriteLine(rt);
            Console.WriteLine(offset);
            Console.WriteLine(bse);
            Console.WriteLine(immediate);


            Console.ReadLine();
        }
    }
}
