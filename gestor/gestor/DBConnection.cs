using gestor.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gestor
{
    public class DBConnection
    {
        public SQLiteConnection CreateConnection()
        {
            SQLiteConnection sqlite_conn;
            // Create a new database connection:
            sqlite_conn = new SQLiteConnection("Data Source=database.db; Version = 3; New = True; Compress = True; ");
            // Open the connection:
            try
            {
                sqlite_conn.Open();
            }
            catch (Exception ex)
            {
            }
            return sqlite_conn;
        }

        public void CreateTable(SQLiteConnection conn, string tableName, List<ItemNames> itemNames)
        {
            SQLiteCommand sqlite_cmd;
            string columns = "";
            foreach (var item in itemNames)
            {
                columns += item.Name + " " + item.Type + ", ";
            }
            columns = columns.Remove(columns.Length - 2);
            string Createsql = "CREATE TABLE IF NOT EXISTS " + tableName + " (" + columns + ")";
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = Createsql;
            sqlite_cmd.ExecuteNonQuery();
        }
        /*public void CreateTable(SQLiteConnection conn, string tableName, string[] columns, string[] types)
        {
            SQLiteCommand sqlite_cmd;
            string query = "CREATE TABLE " + tableName + " (";
            for (int i = 0; i < columns.Length; i++)
            {
                query += columns[i] + " " + types[i];
                if (i != columns.Length - 1)
                {
                    query += ", ";
                }
            }
            query += ");";
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = query;
            sqlite_cmd.ExecuteNonQuery();

        }*/
        /*{
            SQLiteCommand sqlite_cmd;
            string Createsql = "CREATE TABLE SampleTable(Col1 VARCHAR(20), Col2 INT)";
            string Createsql1 = "CREATE TABLE SampleTable1(Col1 VARCHAR(20), Col2 INT)";
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = Createsql;
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = Createsql1;
            sqlite_cmd.ExecuteNonQuery();
        }*/

        public void InsertData(SQLiteConnection conn, string tableName, List<Item> items, List<ItemNames> itemNames)
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
                values += "'" +item.Name + "', '" + item.Description + "', '" + item.Category + "', '" + item.Brand + "', '" + item.Model + "', '" + item.SerialNumber + "', '" + item.Location + "', '" + item.Status + "', '" + item.Notes + "', '" + item.AddDate + "', '" + item.Stock + "', '" + item.Price + "'), ";
            }
            values = values.Remove(values.Length - 2);
            string Createsql = "INSERT INTO " + tableName + " (" + columns + ") VALUES (" + values + ")";
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = Createsql;
            sqlite_cmd.ExecuteNonQuery();
        }

        /*public void InsertData(SQLiteConnection conn, string tableName, string[] columns, string[][] values)
        {
            SQLiteCommand sqlite_cmd;
            string query = "INSERT INTO " + tableName + " (";
            for (int i = 0; i < columns.Length; i++)
            {
                query += columns[i];
                if (i != columns.Length - 1)
                {
                    query += ", ";
                }
            }
            query += ") VALUES (";
            for (int i = 0; i < values.Length; i++)
            {
                for (int j = 0; j < values[i].Length; j++)
                {
                    query += "'" + values[i][j] + "'";
                    if (j != values[i].Length - 1)
                    {
                        query += ", ";
                    }
                }
                if (i != values.Length - 1)
                {
                    query += "), (";
                }
            }
            query += ");";
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = query;
            sqlite_cmd.ExecuteNonQuery();
        }*/
        /*{
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "INSERT INTO SampleTable(Col1, Col2) VALUES('Test Text ', 1); ";
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = "INSERT INTO SampleTable(Col1, Col2) VALUES('Test1 Text1 ', 2); ";
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = "INSERT INTO SampleTable(Col1, Col2) VALUES('Test2 Text2 ', 3); ";
            sqlite_cmd.ExecuteNonQuery();


            sqlite_cmd.CommandText = "INSERT INTO SampleTable1(Col1, Col2) VALUES('Test3 Text3 ', 3); ";
            sqlite_cmd.ExecuteNonQuery();

        }*/

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
                string myreader = "";
                for (int i = 0; i < sqlite_datareader.FieldCount; i++)
                {
                    myreader += sqlite_datareader.GetValue(i) + " | ";
                }
                Console.WriteLine(myreader);
            }
            conn.Close();
        }

        /*public void ReadData(SQLiteConnection conn, string tableName, string[] columns)
        {
            SQLiteCommand sqlite_cmd;
            SQLiteDataReader sqlite_datareader;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM " + tableName + ";";
            sqlite_datareader = sqlite_cmd.ExecuteReader();
            for (int j = 0; j < columns.Length; j++)
            {
                Console.Write("{0}\t ", columns[j]);
            }
            Console.WriteLine("\n----------------------");
            while (sqlite_datareader.Read())
            {
                
                for (int i = 0; i < columns.Length; i++)
                {
                    string myreader = sqlite_datareader.GetString(i);
                    
                    Console.Write("{0}\t ", myreader);
                    if (i==1)
                    {
                        Console.Write("\n");
                    }
                }
            }
        }*/
        /*{
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM " + tableName + ";";
            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                string myreader = sqlite_datareader.GetString(0);
                Console.WriteLine(myreader);
            }

        }*/
        /*{
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM SampleTable";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                string myreader = sqlite_datareader.GetString(0);
                Console.WriteLine(myreader);
            }
            conn.Close();
        }*/

        public void DropTable(SQLiteConnection conn, string tableName)
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "DROP TABLE " + tableName + ";";
            sqlite_cmd.ExecuteNonQuery();

        }

        /*public void UpdateData(SQLiteConnection conn, string tableName, List<Item> items, List<ItemNames> itemNames)
        {
            SQLiteCommand sqlite_cmd;
            string query = "UPDATE " + tableName + " SET ";
            for (int i = 0; i < items.Count; i++)
            {
                query += itemNames[i].Name + " = '" + items[i]. + "'";
                if (i != items.Count - 1)
                {
                    query += ", ";
                }
            }
            query += " WHERE " + itemNames[0].Name + " = '" + items[0].Value + "';";
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = query;
            sqlite_cmd.ExecuteNonQuery();
        }*/


        public void TerminateConnection(SQLiteConnection conn)
        {
            conn.Close();
        }
    }
}
