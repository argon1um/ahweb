using ah4cClientApp.DTO;
using AHRestAPI.Models;
using AnimalHouseRestAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Converters;

namespace ah4cClientApp.Pages
{
    public class UserCabPageModel : PageModel
    {
        public ClientResponseLogin user = AuthPageModel.client;
        public List<OrderGetDTO> Orders;

        public IActionResult OnGet()
        {
            if (user == null)
                return Redirect("/");

            Orders = new HttpClient().GetFromJsonAsync<List<OrderGetDTO>>("http://localhost:8081/orders/orderslist").Result.Where(x => x.ClientPhone == user.ClientPhone).ToList();
            return Page();
        }

        public IActionResult OnPost()
        {
            ah4cClientApp.Pages.IndexModel.check = false;
            return Redirect("/index");
        }
    }
}