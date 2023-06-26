using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyWeb.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ProductController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IWebHostEnvironment _webHostEnvironment;
		public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment) { 
			_unitOfWork = unitOfWork;
			_webHostEnvironment = webHostEnvironment;
		}

		public IActionResult Index()
		{
			// from this index page, we will try to display all list of products in the system
			List<Product> allProducts = _unitOfWork.Product.GetAllItems(includeProperties: "Category").ToList();

			return View(allProducts);
		}

		// this controller will create or update a product
		public IActionResult Upsert(int? id)
		{
			
			ProductViewModel productVm = new() 
			{
				CategoryList = _unitOfWork.Category.GetAllItems().Select(u => new SelectListItem
				{
					Text = u.Name,
					Value = u.Id.ToString()
				}),
				Product	= new Product() 
			  };

			if(id == null || id == 0){
			
				return View(productVm);
			}
			else{
				productVm.Product = _unitOfWork.Product.GetSingleItem(u => u.Id == id);
				return View(productVm);
			}
		}

		//this controller has the logic for you to create a product
		[HttpPost]
		public IActionResult Upsert(ProductViewModel productVm, IFormFile? file)
		{
			if(ModelState.IsValid)
			{
				string webRootPath = _webHostEnvironment.WebRootPath;
				if(file != null)
				{
					string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
					string productPath = Path.Combine(webRootPath, @"images\products");

					// this is when updating a product, if the user chooses a new image, we will delete the old image and replace it with the nnew image!
					if (!string.IsNullOrEmpty(productVm.Product.ImageUrl))
					{
						// delete the old img
						var oldImagePath = Path.Combine(webRootPath, productVm.Product.ImageUrl.TrimStart('\\'));
						if (System.IO.File.Exists(oldImagePath))
						{
							System.IO.File.Delete(oldImagePath);
						}
					}

					using (var fileStream = new FileStream(Path.Combine(productPath, filename), FileMode.Create))
					{
						file.CopyTo(fileStream);
					}
					productVm.Product.ImageUrl = @"\images\products\" + filename;
				}
				if(productVm.Product.Id == 0)
				{
					_unitOfWork.Product.AddItem(productVm.Product);
				}
				else
				{
					_unitOfWork.Product.Update(productVm.Product);
				}
				_unitOfWork.Save();
				if(productVm.Product.Id == 0)
				{
					TempData["success"] = $"Product {productVm.Product.Title} has been created successfully!";
				}
				else
				{
					TempData["success"] = $"Product {productVm.Product.Title} has been updated successfully!";
				}
				return RedirectToAction("Index");
			}
			else
			{
				productVm.CategoryList = _unitOfWork.Category.GetAllItems().Select(u => new SelectListItem
				{
					Text=u.Name,
					Value = u.Id.ToString()
				});
			}

			return View(productVm);
		}

	

		// this controller has the logic for you update a product
		public IActionResult Edit(int? id)
		{
			if(id == null)
			{
				return NotFound();
			}
			
			Product? productFromDb = _unitOfWork.Product.GetSingleItem(u => u.Id == id);
			if(productFromDb == null)
			{
				return NotFound();
			}
			
			return View(productFromDb);
		}

		[HttpPost]
		public IActionResult Edit(Product product)
		{
			if (ModelState.IsValid)
			{
				_unitOfWork.Product.Update(product);
				_unitOfWork.Save();
				TempData["success"] = $"Product {product.Title} has been updated successfully!";
				return RedirectToAction("Index");
			}
			return View();
		}

		public IActionResult Delete(int? id)
		{
			if(id == null)
			{
				return NotFound();
			}

			Product productFromDb = _unitOfWork.Product.GetSingleItem(product => product.Id == id);
			if( productFromDb == null)
			{
				return NotFound();
			}
			return View(productFromDb);
		}

		[HttpPost, ActionName("Delete")]
		public IActionResult DeletePost(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			Product? item = _unitOfWork.Product.GetSingleItem(product => product.Id == id);
			if(item == null)
			{
				return NotFound();
			}

			_unitOfWork.Product.DeleteItem(item);
			_unitOfWork.Save();
			TempData["success"] = $"Product {item.Title} has been deleted successfully!";
			return RedirectToAction("Index");
		}


	
		#region API CALLS
		[HttpGet]
		public IActionResult GetAllItems()
		{
			List<Product> allProducts = _unitOfWork.Product.GetAllItems(includeProperties: "Category").ToList();
			return Json(new { data = allProducts });
		}

		[HttpDelete]
		public IActionResult Deletes(int? id)
		{
			var product = _unitOfWork.Product.GetSingleItem(u=>u.Id == id);
			if(product == null) {
				return Json(new
				{
					success = false,
					message = "Error while deleting product"
				});
			}
			var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, product.ImageUrl.TrimStart('\\'));
			if(System.IO.File.Exists(oldImagePath))
			{
				System.IO.File.Delete(oldImagePath);
			}
			_unitOfWork.Product.DeleteItem(product);
			_unitOfWork.Save();
			return Json(new
			{
				success = true,
				message = "successfully deleted product"
			});

		}
		#endregion
	}
}
