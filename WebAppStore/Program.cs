/*
 * Description: C# Web application 
 *              
 * Author: Victor Robles
 * 
 * Project: Week 10 Assigment Programming Lab - 
 * 
 * Revision: 03/12/20121
 */

using WebAppStore.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.IO;
using System.Reflection;

namespace WebAppStore
{
    public class Program
    {
        //Desktop Computer Specifications
        static public string[,] desktopSpecs = new string[3, 3] { { "Dell", "Gray", "1000.00" }, { "MSI", "Red", "1100.00" }, { "Apple", "White", "1500.00" } };
        //Laptop Specifications
        static public string[,] laptopSpecs = new string[3, 3] { { "Dell", "Black", "1100.00" }, { "MSI", "Black", "1200.00" }, { "Apple", "Gray", "2000.00" } };
        //Notepad Specifications
        static public string[,] notepadSpecs = new string[3, 3] { { "Dell", "White", "1200.00" }, { "Samsung", "Black", "1300.00" }, { "Apple", "White", "1800.00" } };
        //Cellphone Specifications
        static public string[,] cellphoneSpecs = new string[3, 3] { { "Samsung", "Blue", "1400.00" }, { "Google", "White", "1100.00" }, { "Apple", "Silver", "1100.00" } };

        static public List<string> categories = new List<string>();
        static public List<Computers> Inventory = new List<Computers>();
        static public List<Product> Products = new List<Product>();
        static public List<Store> Stores = new List<Store>();
        static public List<Customer> Customers = new List<Customer>();
        static public List<string> quotesPath = new List<string>();
        static public List<string> quotesName = new List<string>();
        static public List<string> Users = new List<string>();
        static public string locationSelected = "";
        static public string typeSelected = "";
        static public string quoteSelected = "";

        Queue<string> orders = new Queue<string>();


        public static string GetContainer(XElement e, string name)
        {
            string value = "";
            try
            {

                if ((value = e.Element(name).Value) == null)
                {
                    throw new NullReferenceException("NullReferenceException.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Exiiting....");
                System.Environment.Exit(0);
            }
            return value;
        }

        public static void Main(string[] args)
        {

            categories.Add("Desktops");
            categories.Add("Laptops");
            categories.Add("Tablets");
            categories.Add("Cell Phones");
                        
            Inventory.Add(new Computers("Desktop", 1000.00));
            Inventory.Add(new Computers("Laptop", 1100.00));
            Inventory.Add(new Computers("Notepad", 1200.00));
            Inventory.Add(new Computers("Cell Phones", 800.00));
            Inventory.Add(new Computers("Desktop", 1000.00, "Linux"));
            Inventory.Add(new Computers("Laptop", 1100.00, "Mac", 2048, 2.0, 16));
            Inventory.Add(new Computers("Notepad", 1200.00, "Windows", 1024, 1.0, 8));
            Inventory.Add(new Computers("Cell Phones", 800.00, "Android"));
            Inventory.Add(new Computers("Desktop", 1000.00, 512));
            Inventory.Add(new Computers("Laptop", 1100.00, 1024, 1.0));
            Inventory.Add(new Computers("Notepad", 1200.00, 1024, 1.0, 8));
            Inventory.Add(new Computers("Cell Phones", 800.00, 2048, 0.5, 8));

            string cs = "Data Source=store_product.db";
            string stm = "SELECT * FROM products";

            string xml = "store_locations.xml";

            using var con = new SqliteConnection(cs);
            con.Open();

            using var cmd = new SqliteCommand(stm, con);
            using SqliteDataReader rdr = cmd.ExecuteReader();

            //find application directory
            var currentPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location.Substring(0, Assembly.GetEntryAssembly().Location.IndexOf("WebAppStore\\")));

            //get mailinglist directory
            string mailingListFolder = currentPath.ToString() + @"\ConsoleAppStore\mailinglist";
            string mailingListFile = mailingListFolder + @"\category.xml";

            //get quotes directory
            string quotesFolder = currentPath.ToString() + @"\ConsoleAppStore\quotes";

            //get users directory
            string usersFolder = currentPath.ToString() + @"\ConsoleAppStore\users";
            string usersFile = usersFolder + @"\users.xml";

            while (rdr.Read())
            {   //Write to the Products Class

                try
                {
                    Products.Add(new Product { Name = rdr.GetString(0), Description = rdr.GetString(1), Brand = rdr.GetString(2), Price = rdr.GetInt16(3), Type = rdr.GetString(4) });
                }
                catch (InvalidCastException ex)
                {

                    Console.WriteLine(ex.Message);
                }
            }

            con.Close();

            XDocument doc = XDocument.Load(xml);


                var locations = from e in doc.Descendants("store")
                                select new
                                {
                                    //Id = e.Element("storeid").Value,
                                    Id = GetContainer(e, "storeid"),
                                    //Location = e.Element("location").Value,
                                    Location = GetContainer(e, "location"),
                                    //Address = e.Element("address").Value,
                                    Address = GetContainer(e, "address"),
                                    //City = e.Element("city").Value,
                                    City = GetContainer(e, "city"),
                                    //State = e.Element("state").Value,
                                    State = GetContainer(e, "state"),
                                    //Zip = e.Element("zip").Value,
                                    Zip = GetContainer(e, "zip"),
                                    //Lat = e.Element("lat").Value,
                                    Lat = GetContainer(e, "lat"),
                                    //Lon = e.Element("lon").Value,
                                    Lon = GetContainer(e, "lon"),
                                };

                foreach (var location in locations)
                    try
                    {
                        Stores.Add(new Store
                        {
                            StoreId = location.Id,
                            Location = location.Location,
                            Address = location.Address,
                            City = location.City,
                            State = location.State,
                            Zip = location.Zip,
                            Lat = float.Parse(location.Lat),
                            Lon = float.Parse(location.Lon)
                        });
                    }
                    catch (System.FormatException ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine("Exiiting....");
                        System.Environment.Exit(0);

                }

            XDocument xDoc = XDocument.Load(mailingListFile);

            var custs = from e in xDoc.Descendants("Category")
                        select new
                        {
                            //Category = e.Element("category").Value,
                            Category = GetContainer(e, "category"),
                            //FirstName = e.Element("firstname").Value,
                            FirstName = GetContainer(e, "firstname"),
                            //LastName = e.Element("lastname").Value,
                            LastName = GetContainer(e, "lastname"),
                            //Address = e.Element("address").Value,
                            Address = GetContainer(e, "address"),
                            //City = e.Element("city").Value,
                            City = GetContainer(e, "city"),
                            //State = e.Element("state").Value,
                            State = GetContainer(e, "state"),
                            //Zip = e.Element("zip").Value,
                            Zip = GetContainer(e, "zip"),
                            //Email = e.Element("email").Value,
                            Email = GetContainer(e, "email"),
                            //Phone = e.Element("phone").Value,
                            Phone = GetContainer(e, "phone"),
                        };


            foreach (var cust in custs)
                try
                {
                    Customers.Add(new Customer
                    {
                        Category = cust.Category,
                        FirstName = cust.FirstName,
                        LastName = cust.LastName,
                        Address = cust.Address,
                        City = cust.City,
                        State = cust.State,
                        Zip = cust.Zip,
                        Email = cust.Email,
                        Phone = cust.Phone,
                    });

                }
                catch (System.FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Exiting....");
                    System.Environment.Exit(0);
                }

            var files = Directory.GetFiles(quotesFolder, "*.*", SearchOption.TopDirectoryOnly);
            foreach (string file in files)
            {
                quotesPath.Add(file);
                quotesName.Add(Path.GetFileName(file));
            }

            XDocument uDoc = XDocument.Load(usersFile);

            var us = from e in uDoc.Descendants("Users")
                     select new
                     {
                         //username = e.Element("username").Value,
                         Username = GetContainer(e, "username"),
                        };

            foreach (var u in us)
                try
                {
                    Users.Add(u.Username);
                }
                catch (System.FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Exiting....");
                    System.Environment.Exit(0);
                }

            CreateHostBuilder(args).Build().Run();
        }


        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
