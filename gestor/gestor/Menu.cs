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
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. Delete Product");
            Console.WriteLine("3. Update Product");
            Console.WriteLine("4. Show Products");
            Console.WriteLine("5. Show Product by ID");
            Console.WriteLine("0. Exit");
            Console.WriteLine("#################################");
            Console.WriteLine("\n");
            Console.WriteLine("Seleccione una opcion: ");
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
                    Console.WriteLine("Opcion no valida");
                    break;
            }
        }

        private void AddProduct()
        {
            var db = new DBConnection();
            var conn = db.CreateConnection();
            var tableNames = db.GetTableNames(conn);

            var tableName = "";
            foreach (var i in tableNames)
            {
                if (i == "Items")
                {
                    tableName = i;
                }
            }

            var itemNames = db.GetColumnsName(conn, tableName);
            var items = new List<Item>();
            var item = new Item();
            Console.WriteLine("Enter the request data and press enter for each field");

            item.Id = db.GetMaxId(conn, tableName)+1;

            Console.WriteLine("Enter the name of the product: ");
            item.Name = Console.ReadLine();

            Console.WriteLine("Enter the description of the product: ");
            item.Description = Console.ReadLine();

            Console.WriteLine("Enter the category of the product: ");
            item.Category = Console.ReadLine();

            Console.WriteLine("Enter the brand of the product: ");
            item.Brand = Console.ReadLine();

            Console.WriteLine("Enter the model of the product: ");
            item.Model = Console.ReadLine();

            Console.WriteLine("Enter the serial number of the product: ");
            item.SerialNumber = Console.ReadLine();

            Console.WriteLine("Enter the location of the product: ");
            item.Location = Console.ReadLine();

            Console.WriteLine("Enter the status of the product: ");
            item.Status = Console.ReadLine();

            Console.WriteLine("Enter the notes of the product: ");
            item.Notes = Console.ReadLine();
            
            item.AddDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            Console.WriteLine("Enter the stock of the product: ");
            item.Stock = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter the price of the product: ");
            item.Price = Convert.ToDouble(Console.ReadLine());

            items.Add(item);

            db.InsertData(conn, tableName, items, itemNames);

        }
        
        private void DeleteProduct()
        {
            var db = new DBConnection();
            var conn = db.CreateConnection();
            var tableNames = db.GetTableNames(conn);

            var tableName = "";
            foreach (var i in tableNames)
            {
                if (i == "Items")
                {
                    tableName = i;
                }
            }

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

            var tableName = "";
            foreach (var i in tableNames)
            {
                if (i=="Items")
                {
                    tableName = i;
                }
            }

            var columns = db.GetColumnsName(conn, tableName);
            db.ReadData(conn, tableName, columns);
            db.TerminateConnection(conn);
        }

        private void ShowProductById()
        {
            Console.WriteLine("Enter the ID of the product: ");
            var id =Convert.ToInt32(Console.ReadLine());
            var db = new DBConnection();
            var conn = db.CreateConnection();
            var tableNames = db.GetTableNames(conn);

            var tableName = "";
            foreach (var i in tableNames)
            {
                if (i == "Items")
                {
                    tableName = i;
                }
            }

            var columns = db.GetColumnsName(conn, tableName);
            db.ReadById(conn, tableName, columns, id);
            db.TerminateConnection(conn);
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
