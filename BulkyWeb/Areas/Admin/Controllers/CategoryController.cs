using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class CategoryController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

		public CategoryController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public IActionResult Index()
		{
			List<Category> objCategoryList = _unitOfWork.Category.GetAllItems().ToList();
			return View(objCategoryList);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Category obj)
		{
			if (obj.Name == obj.DisplayOrder.ToString())
			{
				ModelState.AddModelError("name", "Category name cannot be the same as Display order!");
			}
			if (ModelState.IsValid)
			{
				_unitOfWork.Category.AddItem(obj);
				_unitOfWork.Save();
				TempData["success"] = $"Category {obj.Name} has been created successfully!";
				return RedirectToAction("Index");
			}
			return View();
		}

		public IActionResult Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			Category? idFromDb = _unitOfWork.Category.GetSingleItem(u => u.Id == id);
			if (idFromDb == null)
			{
				return NotFound();
			}
			return View(idFromDb);
		}

		[HttpPost]
		public IActionResult Edit(Category obj)
		{

			if (ModelState.IsValid)
			{
				_unitOfWork.Category.Update(obj);
				_unitOfWork.Save();
				TempData["success"] = $"Category {obj.Name} has been updated successfully!";
				return RedirectToAction("Index");
			}
			return View();
		}


		public IActionResult Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			Category? obj = _unitOfWork.Category.GetSingleItem(u => u.Id == id);
			if (obj == null)
			{
				return NotFound();
			}

			return View(obj);
		}


		[HttpPost, ActionName("Delete")]
		public IActionResult DeletePost(int? id)
		{
			Category? itemFound = _unitOfWork.Category.GetSingleItem(u => u.Id == id);

			if (itemFound == null)
			{
				return NotFound();
			}
			_unitOfWork.Category.DeleteItem(itemFound);
			_unitOfWork.Save();
			TempData["success"] = $"Category  {itemFound.Name} has been deleted successfully!";
			return RedirectToAction("Index");

		}
	}
}
