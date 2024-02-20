using ah4cClientApp.Services;
using AnimalHouseRestAPI.ModelsDTO;
using Azure.Messaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Client;
using System.Net;
using System.Text;
using System.Web;

namespace ah4cClientApp.Pages
{
    public class IndexModel : PageModel
    {
        public static bool check = false;
        AuthService auth = new AuthService();
        private readonly ILogger<IndexModel> _logger;
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }


    }
}