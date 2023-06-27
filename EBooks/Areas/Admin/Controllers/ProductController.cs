using System.Security.Claims;
using EBooks.DataAccess.Repository.IRepository;
using EBooks.Models;
using EBooks.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Differencing;

namespace WebApplication1.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
    {
        this._unitOfWork = unitOfWork;
        this._webHostEnvironment = webHostEnvironment;
    }
    
    // GET
    public IActionResult Index()
    {
        List<Product> products = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
        return View(products);
    }

    [HttpGet]
    public IActionResult Upsert(int? id)
    {
        IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(c => new SelectListItem
        {
            Text = c.Name,
            Value = c.Id.ToString()
        });

        // ViewBag.CategoryList = CategoryList;
        // ViewData["CategoryList"] = CategoryList;
    
        ProductVM productVM = new()
        {
            CategoryList = CategoryList,
            Product = new Product()
        };

        if (id is null or 0)
        {
            return View(productVM);
        }
        else
        {
            //update
            productVM.Product = _unitOfWork.Product.Get(p => p.Id == id);
            return View(productVM);
        }

    }

    [HttpPost]
    public IActionResult Upsert(ProductVM productVM, IFormFile? file)
    {
        if (ModelState.IsValid)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;

            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string filePath = Path.Combine(wwwRootPath, @"images\product");

                if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                {
                    var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                
                using (var fileStream = new FileStream(Path.Combine(filePath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                productVM.Product.ImageUrl = @"\images\product\" + fileName;
            }

            productVM.Product.PublisherId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (productVM.Product.Id == 0)
            {
                _unitOfWork.Product.Add(productVM.Product);
            }
            else
            {
                _unitOfWork.Product.Update(productVM.Product);
            }
            
            _unitOfWork.Save();
            TempData["success"] = "Product Created Successfully";
            return RedirectToAction("Index");
        }
        else
        {
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });

            productVM.CategoryList = CategoryList;
            return View(productVM);
        }
    }

    // [HttpGet]
    // public IActionResult Delete(int? id)
    // {
    //     if (id == null)
    //     {
    //         return NotFound();
    //     }
    //
    //     Product? product = _unitOfWork.Product.Get(p => p.Id == id);
    //
    //     if (product == null)
    //     {
    //         return NotFound();
    //     }
    //
    //     return View(product);
    // }
    //
    // [HttpPost]
    // public IActionResult Delete(Product product)
    // {
    //     _unitOfWork.Product.Remove(product);
    //     _unitOfWork.Save();
    //     TempData["success"] = "Product Deleted Successfully";
    //     return RedirectToAction("Index");
    // }

    #region API CALLS

    [HttpGet]
    public IActionResult GetAll()
    {
        List<Product> products = _unitOfWork.Product.GetAll(includeProperties: "Category").Where(p => p.PublisherId == User.FindFirstValue(ClaimTypes.NameIdentifier)).ToList();
        return Json(new { data = products });
    }
    
    [HttpDelete]
    public IActionResult Delete(int? id)
    {
        var wwwRootPath = _webHostEnvironment.WebRootPath;
        
        var productToDelete = _unitOfWork.Product.Get(p => p.Id == id);
        if (productToDelete == null)
        {
            return Json(new { success = false, message = "Error while deleting" });
        }

        var oldImagePath = Path.Combine(wwwRootPath, productToDelete.ImageUrl.TrimStart('\\'));

        if (System.IO.File.Exists(oldImagePath))
        {
            System.IO.File.Delete(oldImagePath);
        }
        
        _unitOfWork.Product.Remove(productToDelete);
        _unitOfWork.Save();
        return Json(new { success = false, message = "Delete Successful" });
    }

    #endregion
}