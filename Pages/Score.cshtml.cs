using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Quiz.Pages
{
    public class ScoreModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Score { get; set; }
        public void OnGet()
        {
        }
    }
}
