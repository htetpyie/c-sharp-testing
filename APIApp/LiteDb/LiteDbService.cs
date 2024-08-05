using LiteDB;
//https://blog.georgekosmidis.net/using-litedb-in-an-asp-net-core-api.html
namespace APIApp.LiteDb
{
	public class LiteDbService
	{
		private LiteDatabase _db;
		public LiteDbService()
		{
			_db = new LiteDatabase(@"MyLiteDb.db");
		}

		public void CreateCustomer()
		{
			var customer = new Customer
			{
				Name = "John Doe"+DateTime.Now.ToString(),
				Phones = new string[] { "8000-0000", "9000-0000" },
				Age = 39,
				IsActive = true
			};

			var col = _db.GetCollection<Customer>("customers");
			col.EnsureIndex(x => x.Name, true);
			col.Insert(customer);
		}

		public IEnumerable<Customer> GetAllCustomer()
		{
			return _db.GetCollection<Customer>("customers").FindAll();
		}
	}
}
