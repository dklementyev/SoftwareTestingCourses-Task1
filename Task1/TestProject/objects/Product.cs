using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.objects
{
    public class Product
    {
        public Product(string name, string code, string size, string countOf)
        {
            Name = name;
            Code = code;
            Size = size;
            CountOf = countOf;
        }

        //Constructor with "random data"
        public Product()
        {
            Size = "Small";
            Name = "Product";
        }

        //In tests we doesn't use name and Code, but i include it. :)
        public string Name { get; set; }
        public string Code { get; set; }
        public string Size { get; set; }
        public string CountOf { get; set; }

    }
}