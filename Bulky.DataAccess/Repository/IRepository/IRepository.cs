using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository.IRepository
{
	public interface IRepository<T> where T : class
	{
		//T - Category or Product
		IEnumerable<T> GetAllItems(string? includeProperties = null);

		T GetSingleItem(Expression<Func<T,bool>> filter, string? includeProperties = null);

		void AddItem(T category);

		void DeleteItem(T category);

		void DeleteManyItems(IEnumerable<T> entities);
	}
}
