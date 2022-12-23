using gestor.Exceptions;
using gestor.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static SQLite.TableMapping;

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
            Console.WriteLine("#### r. Reestart the menu #######");
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
                    UpdateProduct();
                    break;
                case "4":
                    Console.Clear();
                    Console.WriteLine("Show Products");
                    ShowProducts();
                    break;
                case "5":
                    Console.Clear();
                    Console.WriteLine("Show Product by ID");
                    //ShowProductById();
                    break;
                case "6":
                    Console.Clear();
                    Console.WriteLine("Show Product by Name");
                    //ShowProductByName();
                    break;
                case "7":
                    Console.Clear();
                    Console.WriteLine("Add Table");
                    //AddTable();
                    break;
                case "8":
                    Console.Clear();
                    Console.WriteLine("Show Tables");
                    //ShowTables();
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

            var item = new Item();
            Console.WriteLine("Enter the request data and press enter for each field");

            Console.WriteLine("Enter the name of the product(string): ");
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
            var input = Console.ReadLine();
            int stock;
            if (Int32.TryParse(input, out stock) && Convert.ToInt32(input) >=0)
            {
                item.Stock = Convert.ToInt32(input);
            }
            else if (input == string.Empty)
            {
                item.Stock = 0;
            }
            else
            {
                throw new StockException("The stock must be a  positive number: " + input);
            }

            Console.WriteLine("Enter the price of the product(double): ");
            input = Console.ReadLine();
            double price;
            if (Double.TryParse(input, out price) && Convert.ToDouble(input) >= 0)
            {
                item.Price = Convert.ToDouble(input);
            }
            else if (input == string.Empty)
            {
                item.Price = 0;
            }
            else
            {
                throw new PriceException("The price must be a positive number: " + input);
            }


            if (db.Insert(item))
            {
                Console.WriteLine("The product was added successfully");
            }
            else
            {
                throw new InsertException("Error inserting the product");
            }
            
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

            Console.WriteLine("Enter the ID of the product to delete: ");
            string input = Console.ReadLine();
            int id;

            int maxID = db.GetMaxId();

            if (maxID == null)
            {
                Console.WriteLine("There are no products to delete");
            }
            
            if (Int32.TryParse(input, out id) && Convert.ToInt32(input) <= maxID && Convert.ToInt32(input) >= 0)
            {
                id = Convert.ToInt32(input);
                if (db.Delete(id))
                {
                    Console.WriteLine("The product was deleted successfully");
                }
                else
                {
                    throw new DeleteException("Error deleting the product");
                }
            }
            else
            {
                throw new IdNotFoundException("The ID entered is not valid: " + input);
            }
            
        }

        private void UpdateProduct()
        {
            var db = new DBConnection();

            var item = new Item();
            Console.WriteLine("Enter the request data and press enter for each field, " +
                "if you don't want to change a field, just press enter");

            Console.WriteLine("Enter the ID of the product to update: ");
            string input = Console.ReadLine();
            int id;
            var maxID = db.GetMaxId();

            if (Int32.TryParse(input, out id) && Convert.ToInt32(input) <= maxID && Convert.ToInt32(input) >= 0)
            {
                id = Convert.ToInt32(input);
                item.Id = id;

                Console.WriteLine("Enter the name of the product(string): ");
                item.Name = CheckInput(Console.ReadLine());
                

                Console.WriteLine("Enter the description of the product(string): ");
                item.Description = CheckInput(Console.ReadLine());

                Console.WriteLine("Enter the category of the product(string): ");
                item.Category = CheckInput(Console.ReadLine());

                Console.WriteLine("Enter the brand of the product(string): ");
                item.Brand = CheckInput(Console.ReadLine());

                Console.WriteLine("Enter the model of the product(string): ");
                item.Model = CheckInput(Console.ReadLine());

                Console.WriteLine("Enter the serial number of the product(string): ");
                item.SerialNumber = CheckInput(Console.ReadLine());

                Console.WriteLine("Enter the location of the product(string): ");
                item.Location = CheckInput(Console.ReadLine());

                Console.WriteLine("Enter the status of the product(string): ");
                item.Status = CheckInput(Console.ReadLine());

                Console.WriteLine("Enter the notes of the product(string): ");
                item.Notes = CheckInput(Console.ReadLine());

                item.AddDate = string.Empty;

                Console.WriteLine("Enter the stock of the product(int): ");
                input = Console.ReadLine();
                int stock;
                if (Int32.TryParse(input, out stock) && Convert.ToInt32(input) >= 0)
                {
                    item.Stock = Convert.ToInt32(input);
                }
                else if (input == string.Empty)
                {

                    item.Stock = null;
                }
                else
                {
                    throw new StockException("The stock must be a positive number: " + input);
                }

                Console.WriteLine("Enter the price of the product(double): ");
                input = Console.ReadLine();
                double price;
                if (Double.TryParse(input, out price) && Convert.ToDouble(input) >= 0)
                {
                    item.Price = Convert.ToDouble(input);
                }
                else if (input == string.Empty)
                {
                    item.Price = null;
                }
                else
                {
                    throw new PriceException("The price must be a positive number: " + input);
                }
            }
            else
            {
                throw new IdNotFoundException("The ID entered is not valid: " + input);
            }

            if (!CheckItem(item))
            {
                Console.WriteLine("hola");
                if (db.Update(item))
                {
                    Console.WriteLine("The product was updated successfully");
                }
                else
                {
                    throw new UpdateException("Error updating the product");
                }
            }
            else
            {
                Console.WriteLine("You need to enter at least one field to update");
            }
        }
        
        private string CheckInput(string input)
        {
            if (input == string.Empty)
            {
                return "";
            }
            else
            {
                return input;
            }
        }

        private bool CheckItem(Item item)
        {
            bool empty = false;

            if (item.Name == string.Empty)
            {
                Console.WriteLine("name: "+item.Name);
                empty= true;
            }
            if (item.Description == string.Empty)
            {
                Console.WriteLine("descripcion: "+item.Description);
                empty = true;
            }
            if (item.Category == string.Empty)
            {
                Console.WriteLine("category: "+item.Category);
                empty = true;
            }
            if (item.Brand == string.Empty)
            {
                Console.WriteLine("brand: "+item.Brand);
                empty = true;
            }
            if (item.Model == string.Empty)
            {
                Console.WriteLine("model: "+item.Model);
                empty = true;
            }
            if (item.SerialNumber == string.Empty)
            {
                Console.WriteLine("serial number: "+item.SerialNumber);
                empty = true;
            }
            if (item.Location == string.Empty)
            {
                Console.WriteLine("location: "+item.Location);
                empty = true;
            }
            if (item.Status == string.Empty)
            {
                Console.WriteLine("status: "+item.Status);
                empty = true;
            }
            if (item.Notes == string.Empty)
            {
                Console.WriteLine("notes: "+item.Notes);
                empty = true;
            }
            if (item.AddDate == string.Empty)
            {
                Console.WriteLine("addDate: "+item.AddDate);
                empty = true;
            }
            if (item.Stock == null)
            {
                Console.WriteLine("stock: "+item.Stock);
                empty = true;
            }
            if (item.Price == null)
            {
                Console.WriteLine("price: "+item.Price);
                empty = true;
            }
            return empty;
        }

        private void ShowProducts()
        {
            var db = new DBConnection();
            var listItems = db.ReadAll();
            db.ShowItems(listItems);
        }

        /*private void ShowProductById()
        {

            var db = new DBConnection();
            var conn = db.CreateConnection();
            //var tableNames = db.GetTableNames(conn);

            //string tableName = tableSelector(tableNames);

            string tableName = "Items";

            Console.WriteLine("Enter the ID of the product: ");
            string input = Console.ReadLine();
            int id;
            var maxID = db.GetMaxId(conn, tableName);
            if (Int32.TryParse(input, out id) && Convert.ToInt32(input) <= maxID && Convert.ToInt32(input) >=0)
            {
                id = Convert.ToInt32(input);
                var columns = db.GetColumnsName(conn, tableName);
                db.ReadById(conn, tableName, columns, id);
            }
            else
            {
                throw new IdNotFoundException("The ID entered is not valid: " + input);
            }

            
            db.TerminateConnection(conn);
        }


        private void ShowProductByName()
        {
            var db = new DBConnection();
            var conn = db.CreateConnection();
            //var tableNames = db.GetTableNames(conn);

            //string tableName = tableSelector(tableNames);

            string tableName = "Items";

            Console.WriteLine("Enter the name of the product: ");
            var name = Console.ReadLine();

            var columns = db.GetColumnsName(conn, tableName);
            db.ReadByName(conn, tableName, columns, name);
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
        }*/


        private void returMenu()
        {
            string optionReturn;
            Console.WriteLine("Press R to return to the menu");
            optionReturn = Console.ReadLine();
            selectionMenu(optionReturn);

        }
    }
}
