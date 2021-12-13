using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppStore.Controllers
{
    public class Product
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public double Price { get; set; }
        public string Type { get; set; }

        public string FullDescription
        {
            get
            {
                return $"{ Name } { Description } { Brand } { Price } { Type }";
            }
        }

        public string ShortDescription
        {
            get
            {
                return $"{ Brand },{ Price },{ Type }";
            }
        }
    }
}
