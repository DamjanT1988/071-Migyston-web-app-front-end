using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Migyston_web_app.Pages
{
    public class EditModel : PageModel
    {
        private readonly ILogger<EditModel> _logger;

        public EditModel(ILogger<EditModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}