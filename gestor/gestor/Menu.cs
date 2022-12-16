using gestor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gestor
{
    public class Menu
    {
        string optionMenu = "";

        public void Start()
        {
            do
            {
                TextMenu();
            }
            while (optionMenu != "0");
        }

        private void TextMenu()
        {
            Console.WriteLine("------INVENTORY MANAGEMENT------");
            Console.WriteLine("#################################");
            Console.WriteLine("#### 1. Add Product #############");
            Console.WriteLine("#### 2. Delete Product ##########");
            Console.WriteLine("#### 3. Update Product ##########");
            Console.WriteLine("#### 4. Show Products ###########");
            Console.WriteLine("#### 5. Show Product by ID ######");
            Console.WriteLine("#### 6. Show Product by Name ####");
            Console.WriteLine("#### 7. Add Table ###############");
            Console.WriteLine("#### 8. Show Tables ############");
            Console.WriteLine("#### 0. Exit ####################");
            Console.WriteLine("#################################");
            Console.WriteLine("\n");
            Console.WriteLine("Enter the number of the option you want: ");
            optionMenu = Console.ReadLine();
            selectionMenu(optionMenu);
        }

        private void selectionMenu(string optionMenu)
        {
            var db = new DBConnection();
            
            switch (optionMenu)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("Add Product");
                    AddProduct();
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine("Delete Product");
                    DeleteProduct();
                    break;
                case "3":
                    Console.Clear();
                    Console.WriteLine("Update Product");
                    break;
                case "4":
                    Console.Clear();
                    Console.WriteLine("Show Products");
                    ShowProducts();
                    break;
                case "5":
                    Console.Clear();
                    Console.WriteLine("Show Product by ID");
                    ShowProductById();
                    break;
                case "6":
                    Console.Clear();
                    Console.WriteLine("Show Product by Name");
                    //ShowProductByName();
                    break;
                case "7":
                    Console.Clear();
                    Console.WriteLine("Add Table");
                    AddTable();
                    break;
                case "8":
                    Console.Clear();
                    Console.WriteLine("Show Tables");
                    ShowTables();
                    break;
                case "0":
                    Console.Clear();
                    Console.WriteLine("Exit");
                    break;
                case "r":
                    Console.Clear();
                    TextMenu();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Invalid option");
                    break;
            }
        }

        private void AddProduct()
        {
            var db = new DBConnection();
            var conn = db.CreateConnection();
            var tableNames = db.GetTableNames(conn);

            string tableName = tableSelector(tableNames);

            var itemNames = db.GetColumnsName(conn, tableName);
            var items = new List<Item>();
            var item = new Item();
            Console.WriteLine("Enter the request data and press enter for each field");

            item.Id = db.GetMaxId(conn, tableName)+1;

            /*Console.WriteLine("Enter the name of the product(string): ");
            item.Name = Console.ReadLine();

            Console.WriteLine("Enter the description of the product(string): ");
            item.Description = Console.ReadLine();

            Console.WriteLine("Enter the category of the product(string): ");
            item.Category = Console.ReadLine();

            Console.WriteLine("Enter the brand of the product(string): ");
            item.Brand = Console.ReadLine();

            Console.WriteLine("Enter the model of the product(string): ");
            item.Model = Console.ReadLine();

            Console.WriteLine("Enter the serial number of the product(string): ");
            item.SerialNumber = Console.ReadLine();

            Console.WriteLine("Enter the location of the product(string): ");
            item.Location = Console.ReadLine();

            Console.WriteLine("Enter the status of the product(string): ");
            item.Status = Console.ReadLine();

            Console.WriteLine("Enter the notes of the product(string): ");
            item.Notes = Console.ReadLine();
            
            item.AddDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            Console.WriteLine("Enter the stock of the product(int): ");
            item.Stock = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter the price of the product(double): ");
            item.Price = Convert.ToDouble(Console.ReadLine());*/

            for (int i = 1; i < itemNames.Count; i++)
            {
                Console.WriteLine("Enter the " + itemNames[i] + " of the product: ");
                item.Items.Add(itemNames[i], Console.ReadLine());
            }

            items.Add(item);

            db.InsertData(conn, tableName, items, itemNames);

            Console.WriteLine("\nProduct added successfully");
            Console.WriteLine("\nThe data entered is: ");
            Console.WriteLine("Id: " + item.Id);
            Console.WriteLine("Name: " + item.Name);
            Console.WriteLine("Description: " + item.Description);
            Console.WriteLine("Category: " + item.Category);
            Console.WriteLine("Brand: " + item.Brand);
            Console.WriteLine("Model: " + item.Model);
            Console.WriteLine("Serial Number: " + item.SerialNumber);
            Console.WriteLine("Location: " + item.Location);
            Console.WriteLine("Status: " + item.Status);
            Console.WriteLine("Notes: " + item.Notes);
            Console.WriteLine("Add Date: " + item.AddDate);
            Console.WriteLine("Stock: " + item.Stock);
            Console.WriteLine("Price: " + item.Price);
            

        }
        
        private void DeleteProduct()
        {
            var db = new DBConnection();
            var conn = db.CreateConnection();
            var tableNames = db.GetTableNames(conn);

            string tableName = tableSelector(tableNames);

            Console.WriteLine("Enter the ID of the product to delete: ");
            var id = Convert.ToInt32( Console.ReadLine());
            

            db.DeleteData(conn, tableName, id);
            db.TerminateConnection(conn);
            
            Console.WriteLine("Product deleted");

        }
        
        private void ShowProducts()
        {
            var db = new DBConnection();
            var conn = db.CreateConnection();
            var tableNames = db.GetTableNames(conn);

            string tableName = tableSelector(tableNames);

            var columns = db.GetColumnsName(conn, tableName);
            db.ReadData(conn, tableName, columns);
            db.TerminateConnection(conn);
        }

        private void ShowProductById()
        {
            
            var db = new DBConnection();
            var conn = db.CreateConnection();
            var tableNames = db.GetTableNames(conn);

            string tableName = tableSelector(tableNames);

            Console.WriteLine("Enter the ID of the product: ");
            var id =Convert.ToInt32(Console.ReadLine());
            
            var columns = db.GetColumnsName(conn, tableName);
            db.ReadById(conn, tableName, columns, id);
            db.TerminateConnection(conn);
        }

        private void AddTable()
        {
            var db = new DBConnection();
            var conn = db.CreateConnection();
            var tableNames = db.GetTableNames(conn);

            Console.WriteLine("Enter the name of the table: ");
            var tableName = Console.ReadLine();

            List<ItemNames> itemNames = new List<ItemNames>();
            ItemNames itemName = new ItemNames();
            
            itemName.Name = "Id";
            itemName.Type = "int";
            itemNames.Add(itemName);
            

            Console.WriteLine("Enter the number of columns: ");
            var columnsNumber = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("The first column is always Id(int) and is not necessary to enter it");

            for (int i = 0; i < columnsNumber; i++)
            {
                itemName = new ItemNames();
                Console.WriteLine("Enter the name of the column: ");
                itemName.Name = Console.ReadLine();
                Console.WriteLine("Enter the type of the column: ");
                itemName.Type = Console.ReadLine();
                itemNames.Add(itemName);
            }

            db.CreateTable(conn, tableName, itemNames);
            db.TerminateConnection(conn);
        }

        private string tableSelector(List<string> tableNames)
        {
            var tableName = "";
            Console.WriteLine("This is the list of tables in the database: ");
            foreach (var i in tableNames)
            {
                Console.WriteLine("- " + i);
            }

            Console.WriteLine("Enter the name of the table: ");
            var tableNameInput = Console.ReadLine();

            foreach (var i in tableNames)
            {
                if (i == tableNameInput)
                {
                    tableName = i;
                }
            }

            return tableName;
        }
        
        private void ShowTables()
        {
            var db = new DBConnection();
            var conn = db.CreateConnection();
            var tableNames = db.GetTableNames(conn);

            Console.WriteLine("This is the list of tables in the database: ");
            foreach (var i in tableNames)
            {
                Console.WriteLine("- " + i);
            }
        }


        private void returMenu()
        {
            string optionReturn;
            Console.WriteLine("Press R to return to the menu");
            optionReturn = Console.ReadLine();
            selectionMenu(optionReturn);

        }
    }
}
