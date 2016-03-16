using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMPARC_Project_2
{
    class Register
    {
        private String registerName;
        private String value;

        public Register(String registerName, String value)
        {
            this.registerName = registerName;
            this.value = value;
        }

        public void setRegisterValue(String value)
        {
            if(this.registerName != "R0")
                this.value = value;
        }

        public String getValue()
        {
            return this.value;
        }

        public String getName()
        {
            return this.registerName;
        }

    }
}
