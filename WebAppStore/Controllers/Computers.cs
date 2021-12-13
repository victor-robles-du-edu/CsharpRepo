using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppStore.Controllers
{
    public class Computers
    {
        public string computerType;
        public double price;
        public string OS;
        public int RAM;
        public double diskSize;
        public double processorCores;
        private static double totalPrice;


        //Overload Method with only computerType and Price
        public Computers(string computerType, double price)
        {
            this.computerType = computerType;
            this.price = price;
        }

        //Overload Method with computerType, price and RAM wiht default 512, diskSize with default 0.5 TB and processorCores with default 4
        public Computers(string computerType, double price, int RAM = 512, double diskSize = 0.5, double processorCores = 4)
        {
            this.computerType = computerType;
            this.price = price;
            this.RAM = RAM;
            this.diskSize = diskSize;
            this.processorCores = processorCores;
        }

        //Overload Method with computerType, price and Operating System plus the previous RAM wiht default 512, diskSize with default 0.5 TB and processorCores with default 4
        public Computers(string computerType, double price, string OS, int RAM = 512, double diskSize = 0.5, double processorCores = 4)
        {
            this.computerType = computerType;
            this.price = price;
            this.OS = OS;
            this.RAM = RAM;
            this.diskSize = diskSize;
            this.processorCores = processorCores;
        }

        //Display order values
        public void DisplayOrders()
        {
            Console.WriteLine($"{computerType} price: ${price} RAM: {RAM} Disk: {diskSize} Cores: {processorCores} OS: {OS}");
        }

        //calculate total price of orders
        public double getTotalPrice()
        {
            try
            {   if (price < 0) 
                {
                    throw new ArithmeticException("Negative value");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            return totalPrice;
        }
    }
}
