using AHRestAPI.Models;
using AnimalHouseRestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace ah4cClientApp.Pages
{
    
    public class BookingPageModel : PageModel
    {
        private static JsonSerializerSettings mainsettings = new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
        public static string address = "http://localhost:8081";
        public static List<Room> rooms = new List<Room>();
        public static int selectedRoomid;
        public void OnGet()
        {
            var response = new HttpClient().GetStringAsync(address + "/rooms/allfreerooms").Result;
            rooms = JsonConvert.DeserializeObject<List<Room>>(response);

           

        }

        public void OnPost()
        {
           
        }
    }
}
