using EBooks.DataAccess.Data;
using EBooks.DataAccess.Repository;
using EBooks.DataAccess.Repository.IRepository;
using EBooks.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

[Area("Admin")]
public class CategoryController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    
    public CategoryController(IUnitOfWork unitOfWork)
    {
        this._unitOfWork = unitOfWork;
    }
    
    // GET
    public IActionResult Index()
    {
        var objCategoryList = _unitOfWork.Category.GetAll().ToList();
        return View(objCategoryList);
    }

    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Create(Category obj)
    {
        // if (obj.Name == obj.DisplayOrder.ToString())
        // {
        //     ModelState.AddModelError("Name", "The Display Order cannot exactly match the Name");
        // }
        
        if (ModelState.IsValid)
        {
            _unitOfWork.Category.Add(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category created successfully";
            return RedirectToAction("Index");
        }

        return View();
    }

    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        Category? categoryFromDb = _unitOfWork.Category.Get(c => c.Id == id);
        // Category? categoryFromDb1 = _db.Categories.FirstOrDefault(c => c.Id == id);
        // Category? categoryFromDb2 = _db.Categories.Where(c => c.Id == id).FirstOrDefault();

        if (categoryFromDb == null)
        {
            return NotFound();
        }
        
        return View(categoryFromDb);
    }
    
    [HttpPost]
    public IActionResult Edit(Category obj)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.Category.Update(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category updated successfully";
            return RedirectToAction("Index");
        }

        return View();
    }
    
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        Category? categoryFromDb = _unitOfWork.Category.Get(c => c.Id == id);
        // Category? categoryFromDb1 = _db.Categories.FirstOrDefault(c => c.Id == id);
        // Category? categoryFromDb2 = _db.Categories.Where(c => c.Id == id).FirstOrDefault();

        if (categoryFromDb == null)
        {
            return NotFound();
        }
        
        return View(categoryFromDb);
    }
    
    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePost(int? id)
    {
        Category? category = _unitOfWork.Category.Get(c => c.Id == id);

        if (category == null)
        {
            return NotFound();
        }

        _unitOfWork.Category.Remove(category);
        _unitOfWork.Save();
        TempData["success"] = "Category deleted successfully";
        return RedirectToAction("Index");
    }

    
}