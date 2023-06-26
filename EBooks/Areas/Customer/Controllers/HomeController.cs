using System.Diagnostics;
using EBooks.DataAccess.Repository.IRepository;
using EBooks.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

[Area("Customer")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        IEnumerable<Product> products = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
        return View(products);
    }
    
    public IActionResult Details(int? id)
    {
        if (id is null or 0)
        {
            return NotFound();
        }

        Product product = _unitOfWork.Product.Get(p => p.Id == id, includeProperties: "Category");
        return View(product);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}