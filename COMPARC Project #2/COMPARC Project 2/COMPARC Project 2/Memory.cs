﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMPARC_Project_2
{
    class Memory
    {
        private String memoryLocation;
        private String Value;

        public Memory(String memoryLocation, String Value)
        {
            this.memoryLocation = memoryLocation;
            this.Value = Value;
        }

        public String getValue()
        {
            return this.Value;
        }

        public String getLocation()
        {
            return this.memoryLocation;
        }

        public void setValue(string value)
        {
            this.Value = value;
        }

    }
}
