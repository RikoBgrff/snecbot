using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace learnkeyboard
{
    public class Product
       
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }

        public string Name { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
        public int Count { get; set; }
        //public string SubCategory { get; set; }

    }
}
