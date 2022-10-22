using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace learnkeyboard
{
    public class Customer
    {
        public string Name { get; set; }
        public Address Address { get; set; } = new Address();
        public string Number { get; set; }

    }
}
