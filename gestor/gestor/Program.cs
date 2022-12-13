﻿using gestor;
using gestor.Models;
using System.Data.SQLite;

namespace Gestor
{
	public class Program
	{
		static void Main(string[] args)
		{
			//var menu = new Menu();
			//menu.Start();

			SQLiteConnection sqlite_conn;
			var db = new DBConnection();
			sqlite_conn = db.CreateConnection();

			List<ItemNames> itemNames = generateItemNames();
			List<Item> items = generateItems();


			db.CreateTable(sqlite_conn, "Items", itemNames);
			db.InsertData(sqlite_conn, "Items", items, itemNames);
			db.ReadData(sqlite_conn, "Items", itemNames);
			

		}	
		
		
		public static List<Item> generateItems()
		{
			List<Item> items = new List<Item>();
			items.Add(new Item(1, "Item 1", "Description 1", "Category 1", "Brand 1", "Model 1", "Serial Number 1", "Location 1", "Status 1", "Notes 1", DateTime.Now, 1, 1.0));
			items.Add(new Item(2, "Item 2", "Description 2", "Category 2", "Brand 2", "Model 2", "Serial Number 2", "Location 2", "Status 2", "Notes 2", DateTime.Now, 2, 2.0));
			items.Add(new Item(3, "Item 3", "Description 3", "Category 3", "Brand 3", "Model 3", "Serial Number 3", "Location 3", "Status 3", "Notes 3", DateTime.Now, 3, 3.0));
			items.Add(new Item(4, "Item 4", "Description 4", "Category 4", "Brand 4", "Model 4", "Serial Number 4", "Location 4", "Status 4", "Notes 4", DateTime.Now, 4, 4.0));
			items.Add(new Item(5, "Item 5", "Description 5", "Category 5", "Brand 5", "Model 5", "Serial Number 5", "Location 5", "Status 5", "Notes 5", DateTime.Now, 5, 5.0));
			items.Add(new Item(6, "Item 6", "Description 6", "Category 6", "Brand 6", "Model 6", "Serial Number 6", "Location 6", "Status 6", "Notes 6", DateTime.Now, 6, 6.0));
			items.Add(new Item(7, "Item 7", "Description 7", "Category 7", "Brand 7", "Model 7", "Serial Number 7", "Location 7", "Status 7", "Notes 7", DateTime.Now, 7, 7.0));
			items.Add(new Item(8, "Item 8", "Description 8", "Category 8", "Brand 8", "Model 8", "Serial Number 8", "Location 8", "Status 8", "Notes 8", DateTime.Now, 8, 8.0));
			items.Add(new Item(9, "Item 9", "Description 9", "Category 9", "Brand 9", "Model 9", "Serial Number 9", "Location 9", "Status 9", "Notes 9", DateTime.Now, 9, 9.0));
			items.Add(new Item(10, "Item 10", "Description 10", "Category 10", "Brand 10", "Model 10", "Serial Number 10", "Location 10", "Status 10", "Notes 10", DateTime.Now, 10, 10.0));
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
				new ItemNames() { Name = "AddDate", Type = "DATE" },
				new ItemNames() { Name = "Stock", Type = "INTEGER" },
				new ItemNames() { Name = "Price", Type = "DOUBLE" }
			};

			return itemNames;
		}
		
	} 
}