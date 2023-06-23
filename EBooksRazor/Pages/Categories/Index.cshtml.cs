using EBooksRazor.Data;
using EBooksRazor.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EBooksRazor.Pages.Categories;

public class IndexModel : PageModel
{
    private ApplicationDbContext _db;
    public List<Category> CategoryList { get; set; }
    
    public IndexModel(ApplicationDbContext db)
    {
        this._db = db;
    }

    public void OnGet()
    {
        CategoryList = _db.Categories.ToList();
    }
}