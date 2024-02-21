using AnimalHouseRestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Globalization;

namespace ah4cClientApp.Pages
{
    public class ServicePageModel : PageModel
    {
        public static List<Service> services = new List<Service>();

        
        public void OnGet()
        {
            var response = new HttpClient().GetStringAsync("http://localhost:8081/services/alllist").Result;
            services = JsonConvert.DeserializeObject<List<Service>>(response);
        }
    }
}
