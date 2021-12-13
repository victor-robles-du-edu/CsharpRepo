using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppStore
{
    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Category { get; set; }

        public string CustomerDescription
        {
            get
            {
                return $"{ FirstName },{ LastName },{ Address },{ City },{ State },{ Zip },{ Email },{ Phone },{ Category }";
            }

        }

        public string CustomerLabels
        {
            get
            {
                return $"First Name,Last Name,Address,City,State,Zip,Email,Phone,Category";

            }
        }
    }
}
