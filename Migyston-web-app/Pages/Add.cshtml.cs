using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Migyston_web_app.Models;
using Migyston_web_app.Services;

namespace Migyston_web_app.Pages
{
    public class ServiceModel : PageModel
    {
        public List<Product> products = new();
        [BindProperty]
        public Product NewProduct { get; set; } = new();

        public void OnGet()
        {
            //ProductService.GetProducts();
            products = ProductService.GetAll();
        }

        
        public string IsSwedish(Product product)
        {
            return product.isSwedish ? "Swedish" : "International";
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            ProductService.Add(NewProduct);
            return RedirectToAction("Get");
        }

        public IActionResult OnPostDelete(int id)
        {
            ProductService.Delete(id);
            return RedirectToAction("Get");
        }
    }
}