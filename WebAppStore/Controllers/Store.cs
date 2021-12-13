using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppStore.Controllers
{
    public class Store
    {
        public string StoreId { get; set; }
        public string Location { get; set; }
        public string Address{ get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public float Lat { get; set; }
        public float Lon { get; set; }

    }
}
