using gestor;
using gestor.Models;
using System.Data.SQLite;
using System.Globalization;

namespace Gestor
{
	public class Program
	{
		static void Main(string[] args)
		{
			var menu = new Menu();
			

			SQLiteConnection sqlite_conn;
			var db = new DBConnection();
			sqlite_conn = db.CreateConnection();

			List<ItemNames> itemNames = generateItemNames();
			List<Item> items = generateItems();
	
			db.DropTable(sqlite_conn, "Items");
			db.CreateTable(sqlite_conn, "Items", itemNames);
			db.InsertData(sqlite_conn, "Items", items, itemNames);
			/*var names = db.GetTableNames(sqlite_conn);
			foreach (var name in names)
			{
				Console.WriteLine(name);
			}	*/

				
				/*db.ReadData(sqlite_conn, "Items", itemNames);
				Console.WriteLine("+++++++++++++++++++++++");

				db.DeleteData(sqlite_conn, "Items", 10);
				db.ReadData(sqlite_conn, "Items", itemNames);
				Console.WriteLine("+++++++++++++++++++++++");

				List<Item> items2 = new List<Item>();
				string date2 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
				items2.Add(new Item(10,"hola", "que", "TAL", "Brand 1", "Model 1", "Serial Number 1", "Location 1", "Status 1", "Notes 1", date2, 1, 1.0));

				db.InsertData(sqlite_conn, "Items", items2, itemNames);
				db.ReadData(sqlite_conn, "Items", itemNames);
				Console.WriteLine("+++++++++++++++++++++++");

				var maxID = db.GetMaxId(sqlite_conn, "Items");
				Console.WriteLine("Max ID: " + maxID);*/

				db.TerminateConnection(sqlite_conn);
			
			
			menu.Start();

		}	
		
		
		public static List<Item> generateItems()
		{
			List<Item> items = new List<Item>();
			
			string date2 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

			items.Add(new Item(1,"Item 1", "Description 1", "Category 1", "Brand 1", "Model 1", "Serial Number 1", "Location 1", "Status 1", "Notes 1", date2, 1, 1.0));
			items.Add(new Item(2, "Item 2", "Description 2", "Category 2", "Brand 2", "Model 2", "Serial Number 2", "Location 2", "Status 2", "Notes 2", date2, 2, 2.0));
			items.Add(new Item(3, "Item 3", "Description 3", "Category 3", "Brand 3", "Model 3", "Serial Number 3", "Location 3", "Status 3", "Notes 3", date2, 3, 3.0));
			items.Add(new Item(4, "Item 4", "Description 4", "Category 4", "Brand 4", "Model 4", "Serial Number 4", "Location 4", "Status 4", "Notes 4", date2, 4, 4.0));
			items.Add(new Item(5, "Item 5", "Description 5", "Category 5", "Brand 5", "Model 5", "Serial Number 5", "Location 5", "Status 5", "Notes 5", date2, 5, 5.0));
			items.Add(new Item(6, "Item 6", "Description 6", "Category 6", "Brand 6", "Model 6", "Serial Number 6", "Location 6", "Status 6", "Notes 6", date2, 6, 6.0));
			items.Add(new Item(7, "Item 7", "Description 7", "Category 7", "Brand 7", "Model 7", "Serial Number 7", "Location 7", "Status 7", "Notes 7", date2, 7, 7.0));
			items.Add(new Item(8, "Item 8", "Description 8", "Category 8", "Brand 8", "Model 8", "Serial Number 8", "Location 8", "Status 8", "Notes 8", date2, 8, 8.0));
			items.Add(new Item(9, "Item 9", "Description 9", "Category 9", "Brand 9", "Model 9", "Serial Number 9", "Location 9", "Status 9", "Notes 9", date2, 9, 9.0));
			items.Add(new Item(10, "Item 10", "Description 10", "Category 10", "Brand 10", "Model 10", "Serial Number 10", "Location 10", "Status 10", "Notes 10", date2, 10, 10.0));
			return items;
		}
		
		public static List<ItemNames> generateItemNames()
		{
			List<ItemNames> itemNames = new List<ItemNames>()
			{
				new ItemNames() { Name = "Id", Type = "INTEGER" },
				new ItemNames() { Name = "Name", Type = "VARCHAR(20)" },
				new ItemNames() { Name = "Description", Type = "VARCHAR(20)" },
				new ItemNames() { Name = "Category", Type = "VARCHAR(20)" },
				new ItemNames() { Name = "Brand", Type = "VARCHAR(20)" },
				new ItemNames() { Name = "Model", Type = "VARCHAR(20)" },
				new ItemNames() { Name = "SerialNumber", Type = "VARCHAR(20)" },
				new ItemNames() { Name = "Location", Type = "VARCHAR(20)" },
				new ItemNames() { Name = "Status", Type = "VARCHAR(20)" },
				new ItemNames() { Name = "Notes", Type = "VARCHAR(20)" },
				new ItemNames() { Name = "AddDate", Type = "VARCHAR(20)" },
				new ItemNames() { Name = "Stock", Type = "INTEGER" },
				new ItemNames() { Name = "Price", Type = "DOUBLE" }
			};

			return itemNames;
		}
		
	} 
}