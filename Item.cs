using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamJRPG
{
    public class Item
    {
        public int value;
        public string name;
        public Item(string name) 
        {
            this.name = name;
            value = 1;
        }

        
    }
}
