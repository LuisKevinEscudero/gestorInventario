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

//define a constant 


namespace gestor
{
    public class Menu
    {
        string optionMenu = "";
        public const bool VACIO = false;
        public const bool NO_VACIO = true;

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
            Console.WriteLine("#### 7. Delete Product by Name ##");
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
                    ShowProductById();
                    break;
                case "6":
                    Console.Clear();
                    Console.WriteLine("Show Product by Name");
                    ShowProductByName();
                    break;
                case "7":
                    Console.Clear();
                    Console.WriteLine("Delete Product by Name");
                    DeleteProductByName();
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

            if (CheckItem(item) == VACIO)
            {
                Console.WriteLine("You need to enter at least one field to update");
            }
            else
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
            bool empty = NO_VACIO;
            
            if (item.Name == string.Empty && item.Description == string.Empty && item.Category == string.Empty &&
                item.Brand == string.Empty && item.Model == string.Empty && item.SerialNumber == string.Empty &&
                item.Location == string.Empty && item.Status == string.Empty && item.Notes == string.Empty &&
                item.AddDate == string.Empty && item.Stock == null && item.Price == null)
            {
                empty = VACIO;
            }
            return empty;
        }

        private void ShowProducts()
        {
            var db = new DBConnection();
            var listItems = db.ReadAll();
            db.ShowItems(listItems);
        }

        private void ShowProductById()
        {
            var db = new DBConnection();
            
            Console.WriteLine("Enter the ID of the product: ");
            string input = Console.ReadLine();
            int id;
            var maxID = db.GetMaxId();
            if (Int32.TryParse(input, out id) && Convert.ToInt32(input) <= maxID && Convert.ToInt32(input) >=0)
            {
                id = Convert.ToInt32(input);
                var item = db.Read(id);
                db.ShowItem(item);
            }
            else
            {
                throw new IdNotFoundException("The ID entered is not valid: " + input);
            }
        }


        private void ShowProductByName()
        {
            var db = new DBConnection();

            Console.WriteLine("Enter the name of the product: ");
            var name = Console.ReadLine();

            var item = db.ReadByName(name);
            if (item != null)
            {
                db.ShowItem(item);
            }
            else
            {
                throw new NameNotFoundException("The name entered is not valid: " + name);
            }

        }

        private void DeleteProductByName()
        {
            var db = new DBConnection();

            Console.WriteLine("Enter the name of the product: ");
            var name = Console.ReadLine();

            var item = db.ReadByName(name);
            if (item != null)
            {
                if (db.Delete(item.Id))
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
                throw new NameNotFoundException("The name entered is not valid: " + name);
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
