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
            Console.WriteLine("------Inventory Management------");
            Console.WriteLine("\n");
            Console.WriteLine("#################################");
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. Delete Product");
            Console.WriteLine("3. Update Product");
            Console.WriteLine("4. Show Products");
            Console.WriteLine("5. Show Product");
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
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine("Delete Product");
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

        private void ShowProducts()
        {
            var db = new DBConnection();
            var conn = db.CreateConnection();
            var tableNames = db.GetTableNames(conn);
            var columns = db.GetColumnsName(conn, tableNames[0]);
            db.ReadData(conn, tableNames[0], columns);
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
