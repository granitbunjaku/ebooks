using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace EBooks.Models;

public class Cart
{
    public int Id { get; set; }
    public string UserId { get; set; }
    [ValidateNever]
    public List<CartProduct> CartProducts { get; set; }
}