using AHRestAPI.Models;
using AnimalHouseRestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Converters;

namespace ah4cClientApp.Pages
{
    
    public class UserCabPageModel : PageModel
    {
        public static Client user = AuthPageModel.client;
        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            ah4cClientApp.Pages.IndexModel.check = false;
            return Redirect("/index");
        }

    }
}
