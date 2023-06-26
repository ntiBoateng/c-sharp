using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
	
	public class ProductRepository : Repository<Product>, IProductRepository
	{
		private readonly ApplicationDbContext _db;

		public ProductRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}

		public void Save() { 
		}

		public void Update(Product product)
		{
			var objectFromDb = _db.Products.FirstOrDefault(u => u.Id == product.Id);
			if (objectFromDb != null)
			{
				objectFromDb.ISBN = product.ISBN;
				objectFromDb.AuthurName = product.AuthurName;
				objectFromDb.Description = product.Description;
				objectFromDb.Price50 = product.Price50;
				objectFromDb.CategoryId = product.CategoryId;
				objectFromDb.Price= product.Price;
				objectFromDb.Price100 = product.Price100;
				objectFromDb.ListPrice = product.ListPrice;
				if(product.ImageUrl != null)
				{
					objectFromDb.ImageUrl = product.ImageUrl;
				}
			}
		}
	}
}
