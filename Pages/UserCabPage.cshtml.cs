using ah4cClientApp.DTO;
using AHRestAPI.Models;
using AHRestAPI.ModelsDTO;
using AnimalHouseRestAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json.Converters;
using System.Security.Policy;

namespace ah4cClientApp.Pages
{
    public class UserCabPageModel : PageModel
    {
        public ClientResponseLogin user = AuthPageModel.client;
        public List<OrderGetDTO> Orders;

        public IActionResult OnGet()
        {

            if (user != null)
            {
                Orders = new HttpClient().GetFromJsonAsync<List<OrderGetDTO>>("http://localhost:8081/orders/orderslist").Result.Where(x => x.ClientPhone == user.ClientPhone).ToList();
                return Page();
            }
            else
            {
                return BadRequest();
            }


        }

        public IActionResult OnPost()
        {
            ah4cClientApp.Pages.IndexModel.check = false;
            return Redirect("/index");
        }
    }
}