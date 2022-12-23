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
                    //Console.WriteLine("Record inserted successfully with rowid {0}.", rowid);
                    return true;
                }
                else
                {
                    //Console.WriteLine("Error inserting record.");
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
                /*int recordsBefore = conn.ExecuteScalar<int>("SELECT COUNT(*) FROM Item WHERE Id = " + item.Id);
                conn.Update(item);
                int recordsAfter = conn.ExecuteScalar<int>("SELECT COUNT(*) FROM Item WHERE Id = " + item.Id);
                if (recordsBefore < recordsAfter)
                {
                    Console.WriteLine("Record updated successfully.");
                    return true;
                }
                else
                {
                    Console.WriteLine("Error updating record.");
                    return false;
                }*/
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
                    //Console.WriteLine("Record deleted successfully.");
                    return true;
                }
                else
                {
                    //Console.WriteLine("Error deleting record.");
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

        public void DeleteByName(string name)
        {
            using (var conn = new SQLiteConnection(dbName))
            {
                string query = "DELETE from Item WHERE Name = '" + name + "';";
                conn.Query<Item>(query);
            }
        }

        public int GetMaxId()
        {
            using (var conn = new SQLiteConnection(dbName))
            {
                return conn.ExecuteScalar<int>("SELECT MAX(Id) FROM Item");
            }
        }



        /*public SQLiteConnection CreateConnection()
        {
            SQLiteConnection sqlite_conn;
            // Create a new database connection:
            sqlite_conn = new SQLiteConnection("Data Source=database.db; Version = 3; New = True; Compress = True; ");
            // Open the connection:
            try
            {
                sqlite_conn.
            }
            catch (Exception ex)
            {
            }
            return sqlite_conn;
        }*/


        /*public void CreateTable(SQLiteConnection conn, string tableName, List<ItemNames> itemNames)
        {
            SQLiteCommand sqlite_cmd;
            string columns = "";
            string idFormat = "id INTEGER PRIMARY KEY AUTOINCREMENT, ";
            foreach (var item in itemNames)
            {
                if (item.Name == "Id")
                {
                    columns += idFormat;
                }
                else
                {
                    columns += item.Name + " " + item.Type + ", ";
                }
            }
            columns = columns.Remove(columns.Length - 2);
            string Createsql = "CREATE TABLE IF NOT EXISTS " + tableName + " (" + columns + ")";
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = Createsql;
            sqlite_cmd.ExecuteNonQuery();
        }*/



        /*public void InsertData(SQLiteConnection conn, string tableName, List<Item> items, List<ItemNames> itemNames)
        {
            SQLiteCommand sqlite_cmd;
            string columns = "";
            string values = "";
            foreach (var item in itemNames)
            {
                columns += item.Name + ", ";
            }
            columns = columns.Remove(columns.Length - 2);
            foreach (var item in items)
            {
                values += "('" + item.Id + "', '" + item.Name + "', '" + item.Description + "', '" + item.Category + "', '" + item.Brand + "', '" + item.Model + "', '" + item.SerialNumber + "', '" + item.Location + "', '" + item.Status + "', '" + item.Notes + "', '" + item.AddDate + "', '" + item.Stock + "', '" + item.Price + "'), ";
            }
            values = values.Remove(values.Length - 2);
            string Createsql = "INSERT INTO " + tableName + " (" + columns + ") VALUES " + values;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = Createsql;
            sqlite_cmd.ExecuteNonQuery();
        }

        public void ReadData(SQLiteConnection conn, string tableName, List<ItemNames> itemNames)
        {
            SQLiteCommand sqlite_cmd;
            SQLiteDataReader sqlite_datareader;
            string columns = "";
            foreach (var item in itemNames)
            {
                columns += item.Name + ", ";
            }
            columns = columns.Remove(columns.Length - 2);
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT " + columns + " FROM " + tableName + ";";
            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {

                for (int i = 0; i < sqlite_datareader.FieldCount; i++)
                {
                    Console.Write("{0} -> {1}\t", sqlite_datareader.GetName(i), sqlite_datareader.GetValue(i));
                }
                Console.WriteLine("\n--------------------");
            }
        }


        public void ReadById(SQLiteConnection conn, string tableName, List<ItemNames> itemNames, int id)
        {
            SQLiteCommand sqlite_cmd;
            SQLiteDataReader sqlite_datareader;
            string columns = "";
            foreach (var item in itemNames)
            {
                columns += item.Name + ", ";
            }
            columns = columns.Remove(columns.Length - 2);
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT " + columns + " FROM " + tableName + " WHERE Id = " + id + ";";
            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {

                for (int i = 0; i < sqlite_datareader.FieldCount; i++)
                {
                    Console.Write("{0} -> {1}\t", sqlite_datareader.GetName(i), sqlite_datareader.GetValue(i));
                }
                Console.WriteLine("\n--------------------");
            }
        }

        public void ReadByName(SQLiteConnection conn, string tableName, List<ItemNames> itemNames, string name)
        {
            SQLiteCommand sqlite_cmd;
            SQLiteDataReader sqlite_datareader;
            string columns = "";
            foreach (var item in itemNames)
            {
                columns += item.Name + ", ";
            }
            columns = columns.Remove(columns.Length - 2);
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT " + columns + " FROM " + tableName + " WHERE Name = '" + name + "';";
            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                for (int i = 0; i < sqlite_datareader.FieldCount; i++)
                {
                    Console.Write("{0} -> {1}\t", sqlite_datareader.GetName(i), sqlite_datareader.GetValue(i));
                }
                Console.WriteLine("\n--------------------");
            }
        }

        public void UpdateData(SQLiteConnection conn, string tableName, List<ItemNames> itemNames, List<Item> items, int idItem)
        {
            SQLiteCommand sqlite_cmd;
            string columns = "";

            //select the fields that are not empty in items
            foreach (var item in items)
            {
                if (item.Id != null)
                {
                    columns += "Id = '" + item.Id + "', ";
                }
                if (item.Name != null)
                {
                    columns += "Name = '" + item.Name + "', ";
                }
                if (item.Description != null)
                {
                    columns += "Description = '" + item.Description + "', ";
                }
                if (item.Category != null)
                {
                    columns += "Category = '" + item.Category + "', ";
                }
                if (item.Brand != null)
                {
                    columns += "Brand = '" + item.Brand + "', ";
                }
                if (item.Model != null)
                {
                    columns += "Model = '" + item.Model + "', ";
                }
                if (item.SerialNumber != null)
                {
                    columns += "SerialNumber = '" + item.SerialNumber + "', ";
                }
                if (item.Location != null)
                {
                    columns += "Location = '" + item.Location + "', ";
                }
                if (item.Status != null)
                {
                    columns += "Status = '" + item.Status + "', ";
                }
                if (item.Notes != null)
                {
                    columns += "Notes = '" + item.Notes + "', ";
                }
                if (item.AddDate != null)
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
            }
            

            columns = columns.Remove(columns.Length - 2);
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "UPDATE " + tableName + " SET " + columns + " WHERE Id = " + idItem + ";";

            Console.WriteLine(sqlite_cmd.CommandText);
            sqlite_cmd.ExecuteNonQuery();
        }

        public void DropTable(SQLiteConnection conn, string tableName)
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "DROP TABLE " + tableName + ";";
            sqlite_cmd.ExecuteNonQuery();
        }

        public void TerminateConnection(SQLiteConnection conn)
        {
            conn.Close();
        }

        public void DeleteData(SQLiteConnection conn, string tableName, int idItem)
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "DELETE FROM " + tableName + " WHERE id = " + idItem;
            sqlite_cmd.ExecuteNonQuery();
        }

        public void DeleteAllData(SQLiteConnection conn, string tableName)
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "DELETE FROM " + tableName;
            sqlite_cmd.ExecuteNonQuery();
        }

        public List<string> GetTableNames(SQLiteConnection conn)
        {
            SQLiteCommand sqlite_cmd;
            SQLiteDataReader sqlite_datareader;

            List<string> tables = new List<string>();

            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT name FROM sqlite_master WHERE type='table';";
            sqlite_datareader = sqlite_cmd.ExecuteReader();

            while (sqlite_datareader.Read())
            {
                tables.Add(sqlite_datareader.GetValue(0).ToString());
            }
            return tables;
        }

        public List<ItemNames> GetColumnsName(SQLiteConnection conn, string tableName)
        {
            SQLiteCommand sqlite_cmd;
            SQLiteDataReader sqlite_datareader;

            List<ItemNames> columns = new List<ItemNames>();

            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "PRAGMA table_info(" + tableName + ");";
            sqlite_datareader = sqlite_cmd.ExecuteReader();

            while (sqlite_datareader.Read())
            {
                columns.Add(
                    new ItemNames
                    {
                        Name = sqlite_datareader["name"].ToString(),
                        Type = sqlite_datareader["type"].ToString()
                    }
                    );
            }
            return columns;
        }

        public int GetMaxId(SQLiteConnection conn, string tableName)
        {
            SQLiteCommand sqlite_cmd;
            SQLiteDataReader sqlite_datareader;

            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT MAX(id) FROM " + tableName + ";";
            sqlite_datareader = sqlite_cmd.ExecuteReader();
            int maxId = 0;
            while (sqlite_datareader.Read())
            {
                maxId = Convert.ToInt32(sqlite_datareader.GetValue(0));
            }
            return maxId;
        }*/
    }
}
