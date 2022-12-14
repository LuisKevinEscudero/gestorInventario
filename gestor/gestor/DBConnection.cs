using gestor.Models;
using System;
using System.Collections.Generic;
using System.Data;
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
                    Console.Write("{0} -> {1}\t",sqlite_datareader.GetName(i), sqlite_datareader.GetValue(i));
                }
                Console.WriteLine("\n--------------------");
            }
        }

        public void UpdateData(SQLiteConnection conn, string tableName, List<ItemNames> itemNames, List<Item> items)
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
                values += "('" + item.Id + "', '  " + item.Name + "', '" + item.Description + "', '" + item.Category + "', '" + item.Brand + "', '" + item.Model + "', '" + item.SerialNumber + "', '" + item.Location + "', '" + item.Status + "', '" + item.Notes + "', '" + item.AddDate + "', '" + item.Stock + "', '" + item.Price + "'), ";
            }
            values = values.Remove(values.Length - 2);
            string Createsql = "UPDATE " + tableName + " SET " + columns + " = " + values;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = Createsql;
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
    }
}
