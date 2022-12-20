using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gestor.Models
{
    [Table(Name ="Item")]
    public class Item
    {
        [System.ComponentModel.DataAnnotations.Schema.DatabaseGenerat‌ed(System.ComponentM‌​odel.DataAnnotations‌​.Schema.DatabaseGeneratedOp‌​tion.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string SerialNumber { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public string AddDate { get; set; }
        public int Stock { get; set; }

        public double Price { get; set; }

        public Item()
        {
            
        }
        
        public Item(int id, string name, string description, string category, string brand, string model, string serialNumber, string location, string status, string notes, string addDate, int stock, double price)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.Category = category;
            this.Brand = brand;
            this.Model = model;
            this.SerialNumber = serialNumber;
            this.Location = location;
            this.Status = status;
            this.Notes = notes;
            this.AddDate = addDate;
            this.Stock = stock;
            this.Price = price;
        }

        public Item(string name, string description, string category, string brand, string model, string serialNumber, string location, string status, string notes, string addDate, int stock, double price)
        {
            this.Name = name;
            this.Description = description;
            this.Category = category;
            this.Brand = brand;
            this.Model = model;
            this.SerialNumber = serialNumber;
            this.Location = location;
            this.Status = status;
            this.Notes = notes;
            this.AddDate = addDate;
            this.Stock = stock;
            this.Price = price;
        }



    }
}
