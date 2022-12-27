using gestor.Models;
using System.Linq;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace gestor
{
    public class DBConnection
    {
        private string dbName = "database.db";

        public void CreateTable()
        {
            using (var conn = new SQLiteConnection(dbName))
            {
                conn.CreateTable<Item>();
            }
        }

        public void DropTable()
        {
            using (var conn = new SQLiteConnection(dbName))
            {
                conn.DropTable<Item>();
            }
        }
        
        public bool Insert(Item item)
        {
            using (var conn = new SQLiteConnection(dbName))
            {
                conn.Insert(item);

                long rowid = conn.ExecuteScalar<long>("SELECT last_insert_rowid()");
                if (rowid > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        
        public Item Read(int id)
        {
            using (var conn = new SQLiteConnection(dbName))
            {
                return conn.Table<Item>().Where(x => x.Id == id).FirstOrDefault();
            }
        }

        public Item ReadByName(string name)
        {
            using (var conn = new SQLiteConnection(dbName))
            {
                return conn.Table<Item>().Where(x => x.Name == name).FirstOrDefault();
            }
        }

        public List<Item> ReadAll()
        {
            using (var conn = new SQLiteConnection(dbName))
            {
                return conn.Table<Item>().ToList();
            }
        }

        public bool Update(Item item)
        {
            using (var conn = new SQLiteConnection(dbName))
            {
                string columns = "";

                if (item.Name != string.Empty)
                {
                    columns += "Name = '" + item.Name + "', ";
                }
                if (item.Description != string.Empty)
                {
                    columns += "Description = '" + item.Description + "', ";
                }
                if (item.Category != string.Empty)
                {
                    columns += "Category = '" + item.Category + "', ";
                }
                if (item.Brand != string.Empty)
                {
                    columns += "Brand = '" + item.Brand + "', ";
                }
                if (item.Model != string.Empty)
                {
                    columns += "Model = '" + item.Model + "', ";
                }
                if (item.SerialNumber != string.Empty)
                {
                    columns += "SerialNumber = '" + item.SerialNumber + "', ";
                }
                if (item.Location != string.Empty)
                {
                    columns += "Location = '" + item.Location + "', ";
                }
                if (item.Status != string.Empty)
                {
                    columns += "Status = '" + item.Status + "', ";
                }
                if (item.Notes != string.Empty)
                {
                    columns += "Notes = '" + item.Notes + "', ";
                }
                if (item.AddDate != string.Empty)
                {
                    columns += "AddDate = '" + item.AddDate + "', ";
                }
                if (item.Stock != null)
                {
                    columns += "Stock = '" + item.Stock + "', ";
                }
                if (item.Price != null)
                {
                    columns += "Price = '" + item.Price + "', ";
                }
                columns = columns.Remove(columns.Length - 2);


                string query = "UPDATE Item SET " + columns + " WHERE Id = " + item.Id + ";";
                Console.WriteLine(query);
                conn.Query<Item>(query);
                return true;
            }
        }

        public void ShowItem(Item i)
        {
                Console.WriteLine("Id: " + i.Id);
                Console.WriteLine("Name: " + i.Name);
                Console.WriteLine("Description: " + i.Description);
                Console.WriteLine("Category: " + i.Category);
                Console.WriteLine("Brand: " + i.Brand);
                Console.WriteLine("Model: " + i.Model);
                Console.WriteLine("Serial Number: " + i.SerialNumber);
                Console.WriteLine("Location: " + i.Location);
                Console.WriteLine("Status: " + i.Status);
                Console.WriteLine("Notes: " + i.Notes);
                Console.WriteLine("Add Date: " + i.AddDate);
                Console.WriteLine("Stock: " + i.Stock);
                Console.WriteLine("Price: " + i.Price);
        }

        public void ShowItems(List<Item> itemList)
        {
            foreach (Item i in itemList)
            {
                ShowItem(i);
                Console.WriteLine("--------------------------------------------------");
            }
        }

        public bool Delete(int id)
        {
            using (var conn = new SQLiteConnection(dbName))
            {
                int recordsBefore = conn.ExecuteScalar<int>("SELECT COUNT(*) FROM Item WHERE Id = ?", id);
                conn.Delete<Item>(id);
                int recordsAfter = conn.ExecuteScalar<int>("SELECT COUNT(*) FROM Item WHERE Id = ?", id);
                if (recordsBefore > recordsAfter)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        
        public void DeleteAll()
        {
            using (var conn = new SQLiteConnection(dbName))
            {
                conn.DeleteAll<Item>();
            }
        }


        public int GetMaxId()
        {
            using (var conn = new SQLiteConnection(dbName))
            {
                return conn.ExecuteScalar<int>("SELECT MAX(Id) FROM Item");
            }
        }

        public List<Item> generateItems()
        {
            List<Item> items = new List<Item>();

            string date2 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            items.Add(new Item(1, "Item 1", "Description 1", "Category 1", "Brand 1", "Model 1", "Serial Number 1", "Location 1", "Status 1", "Notes 1", date2, 1, 1.0));
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
    }
}
