/*
 * Description: C# Console application 
 *  
 * Author: Victor Robles
 * 
 * Project: Week 8 Assigment Programming Lab - 
 * 
 * Revision: 03/07/2021
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.IO;
using System.Reflection;
using System.Globalization;
using Excel = Microsoft.Office.Interop.Excel;
using WebAppStore.Controllers;
using Microsoft.Office.Interop.Excel;
using System.Security.Cryptography;
using System.Text;

namespace ConsoleAppStore
{
    class Program
    {
        private static double total;

        public static string GetContainer(XElement e, string name)
        {
            string value = "";
            try
            {
                value = e.Element(name).Value;
                if (value == null)
                {
                    throw new NullReferenceException("NullReferenceException.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Exiting....");
                System.Environment.Exit(0);
            }

            return value;
        }

        //Online reference
        private static string getHash(string text)
        {
            // SHA512 is disposable by inheritance.  
            using (var sha256 = SHA256.Create())
            {
                // Send a sample text to hash.  
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));
                // Get the hashed string.  
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        private static string getPassword(string option)
        {

            var exp = new Expressions();
            string password = "";

            while (true)
            {

                if (option == "1")
                {
                    Console.Write("please enter a new password(8 - 15 character, must contain one upper & lower case letter & one number):\n");
                    password = Console.ReadLine();
                    bool passwordValid = exp.IsPasswordValid(password);
                    if (passwordValid)
                    {
                        password = getHash(password);
                        Console.WriteLine("Password Saved");
                        break;
                    }
                    Console.WriteLine("\nIncorrect Password Type (8-15 character, must contain one upper & lower case letter & one number) required, To continue press Y To exit press N:\n");
                    string choice = Console.ReadLine().ToLower();
                    if (choice == "n")
                    {
                        Console.WriteLine("Exiting....");
                        System.Environment.Exit(0);
                    }
                }
                else if (option == "2")
                {
                    Console.Write("please enter password:\n");
                    password = Console.ReadLine();
                    password = getHash(password);
                    break;
                }
            }

            return password;
          
        }

        static void Main(string[] args)
        {

            List<Computers> Inventory = new List<Computers>();
            List<Customer> Customers = new List<Customer>();

            //find application directory
            var currentPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location.Substring(0, Assembly.GetEntryAssembly().Location.IndexOf("bin\\")));

            //add mailinglist directory
            string destinationFolder = currentPath.ToString() + @"\mailinglist";            
            //add quotes directory
            string quotesFolder = currentPath.ToString() + @"\quotes";
            //add user directory
            string userFolder = currentPath.ToString() + @"\users";

            //create directory folder if it does not exist
            Directory.CreateDirectory(destinationFolder);
            Directory.CreateDirectory(quotesFolder);
            Directory.CreateDirectory(userFolder);

            //file destination
            string destinationFile = destinationFolder + @"\category.xml";
            string userFile = userFolder + @"\users.xml";

            Console.WriteLine("*******************************************************************************************************");
            Console.WriteLine("\tVictory E-Store");
            Console.WriteLine("\tOwner: Victor Robles");
            Console.WriteLine("\tMy Store Scenario is E-Commerce Store that sells computer and networking equipment.");
            Console.WriteLine("\tIt specializes in pre-built and customized-to-order computers, servers and netwok equipment.");
            Console.WriteLine("\tHit ESC to exit anytime");
            Console.WriteLine("*******************************************************************************************************");
            Console.Write("Enter your name: ");
            string name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Console.ReadLine().ToLower()); //Capitalizes first letter of each word


            var exp = new Expressions();
            bool nameValid = exp.IsNameValid(name);

            if (nameValid)
            {
                //check if file already exists in users directory
                bool fileExists = File.Exists(userFile);
                //if file does not exist create a new xml file
                if (!fileExists)
                {

                    Console.Write($"{name},");
                    string password = getPassword("1");
                    XDocument xDoc = new XDocument(
                            new XElement("Users",
                            new XElement("username", name),
                            new XElement("password", password)
                            ));
                    xDoc.Save(userFile);
                    Console.WriteLine("users directory created");
                }
                else if (fileExists)
                {

                    string userName = null;
                    string password = null;
                    bool userValid = false;
                    XDocument xDoc = XDocument.Load(userFile);

                    var users = from e in xDoc.Descendants("Users")
                                select new
                                {
                                    //userName = e.Element("user").Value,
                                    UserName = GetContainer(e, "username"),
                                    //password = e.Element("password").Value,
                                    Password = GetContainer(e, "password"),                                   
                                };

                    foreach (var user in users)
                        if (name == user.UserName)
                        {
                            userName = user.UserName;
                            password = user.Password;
                            userValid = true;
                            break;
                        }
                    
                    if (userValid)
                    {                       
                        string entry = null;
                        while (entry != password) 
                        {
                            Console.Write($"{name}, ");
                            entry = getPassword("2");
                            if (entry != password)
                            {
                                Console.WriteLine("Incorrect password, press Y to Continue");
                                var k = Console.ReadKey(true);
                                if (k.Key == ConsoleKey.E)
                                {
                                    Console.WriteLine("Exiting....");
                                    System.Environment.Exit(0);
                                }
                            }
                        }

                    }
                    else
                    {
                        Console.Write($"{name}, ");
                        password = getPassword("1");
                        // update the file and add new elements
                        var newElement =
                            new XElement("Users",
                            new XElement("username", name),
                            new XElement("password", password)
                            );
                        xDoc.Root.Add(newElement);
                        xDoc.Save(userFile);
                        Console.WriteLine("users directory updated");
                    }

                }
            }
            else 
            {
                Console.WriteLine("Name Type invalid Exiting system....");
                System.Environment.Exit(0);
            }

            Console.WriteLine("\t");
            Console.WriteLine($"Welcome { name }. I hope you are having a good day on { DateTime.Now }");

            Console.WriteLine("Press 1 Enter to view customized orders");
            Console.WriteLine("Press 2 Enter to add customer to mailing list");
            Console.WriteLine("Press 3 Enter to create a customized computer quote");
            Console.WriteLine("press any other key to exit application");
            string key = Console.ReadLine();

            //exit environment if option 1 or 2 not prompt
            if (key == "1")
            {
                Inventory.Add(new Computers("Desktop", -1000.00));
                Inventory.Add(new Computers("Laptop", 1100.00));
                Inventory.Add(new Computers("Notepad", 1200.00));
                Inventory.Add(new Computers("Desktop", 1000.00, "Linux"));
                Inventory.Add(new Computers("Laptop", 1100.00, "Mac", 2048, 2.0, 16));
                Inventory.Add(new Computers("Notepad", 1200.00, "Windows", 1024, 1.0, 8));
                Inventory.Add(new Computers("Desktop", 1000.00, 512));
                Inventory.Add(new Computers("Laptop", 1100.00, 1024, 1.0));
                Inventory.Add(new Computers("Notepad", 1200.00, 1024, 1.0, 8));

                Console.WriteLine("Recent customized computer orders include:");
                foreach (Computers PC in Inventory)
                    PC.DisplayOrders();

                foreach (Computers PC in Inventory)
                    total = PC.getTotalPrice();
                Console.WriteLine($"The total price of all the customized orders is: {total}");
            }
            else if (key == "2")
            {
                string firstName = "";
                while (true)
                {
                    Console.WriteLine("Enter Customer First Name");
                    firstName = Console.ReadLine();
                    if (exp.IsNameValid(firstName))
                    {
                        Console.WriteLine("First Name Added\n");
                        break;
                    }
                    Console.WriteLine("\nIncorrect Name Type, To continue press Y To exit press N:\n");
                    string choice = Console.ReadLine().ToLower();
                    if (choice == "n")
                    {
                        Console.WriteLine("Exiting....");
                        System.Environment.Exit(0);
                    }
                }

                string lastName = "";
                while (true)
                {
                    Console.WriteLine("Enter Customer Last Name");
                    lastName = Console.ReadLine();
                    if (exp.IsNameValid(lastName))
                    {
                        Console.WriteLine("Last Name Added\n");
                        break;
                    }
                    Console.WriteLine("\nIncorrect Name Type, To continue press Y To exit press N:\n");
                    string choice = Console.ReadLine().ToLower();
                    if (choice == "n")
                    {
                        Console.WriteLine("Exiting....");
                        System.Environment.Exit(0);
                    }
                }

                string address = "";
                while (true)
                {
                    Console.WriteLine("Enter Customer Address");
                    address = Console.ReadLine();
                    if (exp.IsAddressValid(address))
                    {
                        Console.WriteLine("Address Name Added\n");
                        break;
                    }
                    Console.WriteLine("\nIncorrect Address Type, To continue press Y To exit press N:\n");
                    string choice = Console.ReadLine().ToLower();
                    if (choice == "n")
                    {
                        Console.WriteLine("Exiting....");
                        System.Environment.Exit(0);
                    }
                }

                string city = "";
                while (true)
                {
                    Console.WriteLine("Enter Customer City");
                    city = Console.ReadLine();
                    if (exp.IsNameValid(city))
                    {
                        Console.WriteLine("City Name Added\n");
                        break;
                    }
                    Console.WriteLine("\nIncorrect City Name Type, To continue press Y To exit press N:\n");
                    string choice = Console.ReadLine().ToLower();
                    if (choice == "n")
                    {
                        Console.WriteLine("Exiting....");
                        System.Environment.Exit(0);
                    }
                }

                string state = "";
                while (true)
                {
                    Console.WriteLine("Enter Customer State");
                    state = Console.ReadLine();
                    if (exp.IsStateValid(state))
                    {
                        Console.WriteLine("State Name Added\n");
                        break;
                    }
                    Console.WriteLine("\nIncorrect State Name Type, To continue press Y To exit press N:\n");
                    string choice = Console.ReadLine().ToLower();
                    if (choice == "n")
                    {
                        Console.WriteLine("Exiting....");
                        System.Environment.Exit(0);
                    }
                }

                string zip = "";
                while (true)
                {
                    Console.WriteLine("Enter Customer ZipCode");
                    zip = Console.ReadLine();
                    if (exp.IsZipValid(zip))
                    {
                        Console.WriteLine("Zipcode Name Added\n");
                        break;
                    }
                    Console.WriteLine("\nIncorrect ZipCode Name Type, To continue press Y To exit press N:\n");
                    string choice = Console.ReadLine().ToLower();
                    if (choice == "n")
                    {
                        Console.WriteLine("Exiting....");
                        System.Environment.Exit(0);
                    }
                }

                string email = "";
                while (true)
                {
                    Console.WriteLine("Enter Customer Email");
                    email = Console.ReadLine();
                    if (exp.IsEmailValid(email))
                    {
                        Console.WriteLine("Email Added\n");
                        break;
                    }
                    Console.WriteLine("\nIncorrect Email Type, To continue press Y To exit press N:\n");
                    string choice = Console.ReadLine().ToLower();
                    if (choice == "n")
                    {
                        Console.WriteLine("Exiting....");
                        System.Environment.Exit(0);
                    }
                }

                string phone = "";
                while (true)
                {
                    Console.WriteLine("Enter Customer Phone Number");
                    phone = Console.ReadLine();
                    if (exp.IsPhoneValid(phone))
                    {
                        Console.WriteLine("Phone Number Added\n");
                        break;
                    }
                    Console.WriteLine("\nIncorrect Phone Number Type, To continue press Y To exit press N:\n");
                    string choice = Console.ReadLine().ToLower();
                    if (choice == "n")
                    {
                        Console.WriteLine("Exiting....");
                        System.Environment.Exit(0);
                    }
                }

                string category = "";
                while (true)
                {
                    Console.WriteLine("Enter Customer Category of interest");
                    category = Console.ReadLine();
                    if (exp.IsCategoryValid(category))
                    {
                        Console.WriteLine("Category Added\n");
                        break;
                    }
                    Console.WriteLine("\nIncorrect Category Name, To continue press Y To exit press N:\n");
                    string choice = Console.ReadLine().ToLower();
                    if (choice == "n")
                    {
                        Console.WriteLine("Exiting....");
                        System.Environment.Exit(0);
                    }
                }

                Customers.Add(new Customer { FirstName = firstName, LastName = lastName, Address = address, City = city, State = state, Zip = zip, Email = email, Phone = phone, Category = category });

                //check if file already exists in mailinglist directory
                bool fileExists = File.Exists(destinationFile);
                //if file does not exist create a new xml file
                if (!fileExists)
                {
                    XDocument xDoc = new XDocument(
                            new XElement("Category",
                            new XElement("category", Customers[0].Category),
                            new XElement("firstname", Customers[0].FirstName),
                            new XElement("lastname", Customers[0].LastName),
                            new XElement("address", Customers[0].Address),
                            new XElement("city", Customers[0].City),
                            new XElement("state", Customers[0].State),
                            new XElement("zip", Customers[0].Zip),
                            new XElement("email", Customers[0].Email),
                            new XElement("phone", Customers[0].Phone)
                            ));
                    xDoc.Save(destinationFile);
                    Console.WriteLine("category.xml in Directory mailinglist created");
                }
                else if (fileExists)
                {

                    XDocument xDoc = XDocument.Load(destinationFile);

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

                    // update the file and add new elements
                    var newElement =
                        new XElement("Category",
                        new XElement("category", Customers[0].Category),
                        new XElement("firstname", Customers[0].FirstName),
                        new XElement("lastname", Customers[0].LastName),
                        new XElement("address", Customers[0].Address),
                        new XElement("city", Customers[0].City),
                        new XElement("state", Customers[0].State),
                        new XElement("zip", Customers[0].Zip),
                        new XElement("email", Customers[0].Email),
                        new XElement("phone", Customers[0].Phone)
                        );
                    xDoc.Root.Add(newElement);
                    xDoc.Save(destinationFile);
                    Console.WriteLine("category.xml in Directory mailinglist updated");
                }
            }
            else if (key == "3")
            {
                var customerInfo = new Customer();
                var productInfo = new Product();
                var computer = new Computers("", 0);
                List<Specification> Specs = new List<Specification>();

                string firstName = "";
                while (true)
                {
                    Console.WriteLine("Enter Customer First Name");
                    firstName = Console.ReadLine();
                    if (exp.IsNameValid(firstName))
                    {
                        Console.WriteLine("First Name Added\n");
                        break;
                    }
                    Console.WriteLine("\nIncorrect Name Type, To continue press Y To exit press N:\n");
                    string choice = Console.ReadLine().ToLower();
                    if (choice == "n")
                    {
                        Console.WriteLine("Exiting....");
                        System.Environment.Exit(0);
                    }
                }

                string lastName = "";
                while (true)
                {
                    Console.WriteLine("Enter Customer Last Name");
                    lastName = Console.ReadLine();
                    if (exp.IsNameValid(lastName))
                    {
                        Console.WriteLine("Last Name Added\n");
                        break;
                    }
                    Console.WriteLine("\nIncorrect Name Type, To continue press Y To exit press N:\n");
                    string choice = Console.ReadLine().ToLower();
                    if (choice == "n")
                    {
                        Console.WriteLine("Exiting....");
                        System.Environment.Exit(0);
                    }
                }

                string address = "";
                while (true)
                {
                    Console.WriteLine("Enter Customer Address");
                    address = Console.ReadLine();
                    if (exp.IsAddressValid(address))
                    {
                        Console.WriteLine("Address Name Added\n");
                        break;
                    }
                    Console.WriteLine("\nIncorrect Address Type, To continue press Y To exit press N:\n");
                    string choice = Console.ReadLine().ToLower();
                    if (choice == "n")
                    {
                        Console.WriteLine("Exiting....");
                        System.Environment.Exit(0);
                    }
                }

                string city = "";
                while (true)
                {
                    Console.WriteLine("Enter Customer City");
                    city = Console.ReadLine();
                    if (exp.IsNameValid(city))
                    {
                        Console.WriteLine("City Name Added\n");
                        break;
                    }
                    Console.WriteLine("\nIncorrect City Name Type, To continue press Y To exit press N:\n");
                    string choice = Console.ReadLine().ToLower();
                    if (choice == "n")
                    {
                        Console.WriteLine("Exiting....");
                        System.Environment.Exit(0);
                    }
                }

                string state = "";
                while (true)
                {
                    Console.WriteLine("Enter Customer State");
                    state = Console.ReadLine();
                    if (exp.IsStateValid(state))
                    {
                        Console.WriteLine("State Name Added\n");
                        break;
                    }
                    Console.WriteLine("\nIncorrect State Name Type, To continue press Y To exit press N:\n");
                    string choice = Console.ReadLine().ToLower();
                    if (choice == "n")
                    {
                        Console.WriteLine("Exiting....");
                        System.Environment.Exit(0);
                    }
                }

                string zip = "";
                while (true)
                {
                    Console.WriteLine("Enter Customer ZipCode");
                    zip = Console.ReadLine();
                    if (exp.IsZipValid(zip))
                    {
                        Console.WriteLine("Zipcode Name Added\n");
                        break;
                    }
                    Console.WriteLine("\nIncorrect ZipCode Name Type, To continue press Y To exit press N:\n");
                    string choice = Console.ReadLine().ToLower();
                    if (choice == "n")
                    {
                        Console.WriteLine("Exiting....");
                        System.Environment.Exit(0);
                    }
                }

                string email = "";
                while (true)
                {
                    Console.WriteLine("Enter Customer Email");
                    email = Console.ReadLine();
                    if (exp.IsEmailValid(email))
                    {
                        Console.WriteLine("Email Added\n");
                        break;
                    }
                    Console.WriteLine("\nIncorrect Email Type, To continue press Y To exit press N:\n");
                    string choice = Console.ReadLine().ToLower();
                    if (choice == "n")
                    {
                        Console.WriteLine("Exiting....");
                        System.Environment.Exit(0);
                    }
                }

                string phone = "";
                while (true)
                {
                    Console.WriteLine("Enter Customer Phone Number");
                    phone = Console.ReadLine();
                    if (exp.IsPhoneValid(phone))
                    {
                        Console.WriteLine("Phone Number Added\n");
                        break;
                    }
                    Console.WriteLine("\nIncorrect Phone Number Type, To continue press Y To exit press N:\n");
                    string choice = Console.ReadLine().ToLower();
                    if (choice == "n")
                    {
                        Console.WriteLine("Exiting....");
                        System.Environment.Exit(0);
                    }
                }

                string category = "";
                while (true)
                {
                    Console.WriteLine("Enter Customer Category of interest");
                    category = Console.ReadLine();
                    if (exp.IsCategoryValid(category))
                    {
                        Console.WriteLine("Category Added\n");
                        break;
                    }
                    Console.WriteLine("\nIncorrect Category Name, To continue press Y To exit press N:\n");
                    string choice = Console.ReadLine().ToLower();
                    if (choice == "n")
                    {
                        Console.WriteLine("Exiting....");
                        System.Environment.Exit(0);
                    }
                }

                string[] parts = new string[3] { "brand", "RAM", "Hard Drive" };
                double totalPrice = 0;


                for (int i = 0; i < 3; i++)
                {
                    try
                    {
                        Console.WriteLine($"Enter {parts[i]} ");
                        string part = Console.ReadLine();

                        Console.WriteLine($"Enter {parts[i]} price as a number");
                        double price = float.Parse(Console.ReadLine());

                        if (i == 0)
                        {
                            productInfo.Brand = part;
                            productInfo.Price = price;
                            Specs.Add(new Specification { Part = part, Price = price });
                        }
                        else
                        {
                            Specs.Add(new Specification { Part = part, Price = price });
                        }

                        if (parts[i] == "RAM")
                        {
                            computer.RAM = int.Parse(part);
                        }

                        if (parts[i] == "Hard Drive")
                        {
                            computer.diskSize = int.Parse(part);
                        }

                        totalPrice += price;
                    }
                    catch (System.FormatException ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine("Exiting....");
                        System.Environment.Exit(0);
                    }
                }
                productInfo.Price = totalPrice;

                computer.computerType = productInfo.Brand;
                computer.price = productInfo.Price;

                string quoteName = customerInfo.FirstName + "_" + customerInfo.LastName + "_quote.xlsx";
                string excelFile = quotesFolder + @"\" + quoteName;
                var missing = System.Reflection.Missing.Value;
                var excel = new Excel.Application();
                Excel.Workbook workBook = excel.Workbooks.Add(missing);
                var range = (Excel.Range)excel.Cells[1, 1];

                string info = customerInfo.CustomerLabels;
                string data = customerInfo.CustomerDescription;
                string[] lineInfo = info.Split(',');
                string[] lineData = data.Split(',');


                for (int i = 0; i < lineData.Length; i++)
                {
                    range = (Excel.Range)excel.Cells[1, i + 1];
                    range.Font.FontStyle = "Bold";
                    range.Value2 = lineInfo[i];
                    range = (Excel.Range)excel.Cells[2, i + 1];
                    range.Value2 = lineData[i];
                }

                for (int i = 0; i < Specs.Count; i++)
                {
                    if (i == 0)
                    {
                        range = (Excel.Range)excel.Cells[4, 1];
                        range.Font.FontStyle = "Bold";
                        range.Value2 = "Type";
                        range = (Excel.Range)excel.Cells[4, 2];
                        range.Font.FontStyle = "Bold";
                        range.Value2 = "Price";
                    }
                    range = (Excel.Range)excel.Cells[5 + i, 1];

                    range.Value2 = Specs[i].Part;
                    range = (Excel.Range)excel.Cells[5 + i, 2];
                    range.Value2 = Specs[i].Price;

                    if (i == (Specs.Count - 1))
                    {
                        range = (Excel.Range)excel.Cells[6 + i, 1];
                        range.Font.FontStyle = "Bold";
                        range.Value2 = "Total";
                        range = (Excel.Range)excel.Cells[6 + i, 2];
                        range.Value2 = computer.price;
                    }
                }

                workBook.SaveAs(excelFile);
                workBook.Close();

                Console.WriteLine($"Quote {quoteName} created in directory path {excelFile}");
                Console.WriteLine("");

                Console.WriteLine("Display Quote yes Or no:");
                string display = Console.ReadLine().ToUpper();

                if (display == "YES")
                {
                    Application excelApp = new Application();

                    Workbook excelBook = excelApp.Workbooks.Open(excelFile);
                    _Worksheet excelSheet = excelBook.Sheets[1];
                    Excel.Range excelRange = excelSheet.UsedRange;

                    int rows = excelRange.Rows.Count;
                    int cols = excelRange.Columns.Count;

                    for (int i = 1; i <= rows; i++)
                    {
                        Console.Write("\r\n");
                        for (int j = 1; j <= cols; j++)
                        {
                            range = (Excel.Range)excelApp.Cells[i, j];
                            if (range != null && range.Value2 != null)
                                Console.Write(range.Value2.ToString() + "\t");
                        }
                    }
                    Console.WriteLine("\n");
                    excelApp.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
                }
                else {
                    Console.WriteLine("Exiting....");
                    System.Environment.Exit(0);
                }
            }

            else
            {
                Console.WriteLine("Exiting....");
                System.Environment.Exit(0);
            }
            Console.ReadLine();
        }
            
        
    }
}

