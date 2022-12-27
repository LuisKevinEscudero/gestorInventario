using gestor;
using gestor.Models;

using System.Globalization;

namespace Gestor
{
	public class Program
	{
		static void Main(string[] args)
		{
			var menu = new Menu();

			/*DBConnection db = new DBConnection();
			db.DropTable();
			db.CreateTable();

			var itemList = generateItems();
			foreach (var item in itemList)
			{
				db.Insert(item);
			}*/

			/*var x = db.ReadAll();
			db.ShowItems(x);
			Console.WriteLine("****************************");
			//create item to update
			var itemToUpdate = new Item(1, "update", "hola", "", "", "", "", "location1", "status1", "notes1", "addDate1", 1, 1.1);
			//var itemToUpdate = new Item(1, "update", "hola", "category1", "brand1", "model1", "serialNumber1", "location1", "status1", "notes1", "addDate1", 1, 1.1);

			db.Update(itemToUpdate);
			db.DeleteByName("update");
			var y= db.ReadAll();
			db.ShowItems(y);*/
			//Item x = db.ReadByName("Item 4");
			//Item y = db.Read(4);
			//db.ShowItem(x);




			menu.Start();

		}	
		
		
		
	} 
}