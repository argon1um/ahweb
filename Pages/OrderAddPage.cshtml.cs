using ah4cClientApp.DTO;
using AHRestAPI.Models;
using AnimalHouseRestAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ah4cClientApp.Pages
{
    public class OrderAddPageModel : PageModel
    {
        public string roomId;
        public string animalType;
        public List<Service> servicesList;
        public List<Animaltype> animalTypes;
        private static JsonSerializerSettings mainsettings = new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
        public static string address = "http://localhost:8081/";
         
        public IActionResult OnPost(int roomid)

        {
            var showerror = false;
            string strClientName = Request.Form["clientName"];
            string strClientPhone = Request.Form["clientPhone"];
            string strRoomid = Request.Form["roomId"];
            string strIssueDate = Request.Form["issueDate"];
            string strAdmDate = Request.Form["admissionDate"];
            string animalType = Request.Form["animaltypes"];
            string animalBreed = Request.Form["animalBreed"];
            string animalName = Request.Form["animalName"];
            string animalAge = Request.Form["animalAge"];
            string animalWeight = Request.Form["animalWeight"];
            string animalHeight = Request.Form["animalHeight"];
            string acceptrules = Request.Form["acceptrules"];
            string gen = Request.Form["gens"];
            if (string.IsNullOrEmpty(strClientName) || string.IsNullOrEmpty(strClientPhone) || string.IsNullOrEmpty(strRoomid) || string.IsNullOrEmpty(strAdmDate) || string.IsNullOrEmpty(strIssueDate) && string.IsNullOrEmpty(animalType) || string.IsNullOrEmpty(animalBreed)
                || string.IsNullOrEmpty(animalName) || string.IsNullOrEmpty(animalAge) || string.IsNullOrEmpty(animalWeight) || string.IsNullOrEmpty(animalHeight) || string.IsNullOrEmpty(gen))
            {
                showerror = true;
                if (showerror)
                {
                    ViewData["showerror"] = "true";
                    ViewData["customerror"] = "Заполните все поля";
                    return Page();
                }
                return BadRequest();
            }
            else if (!decimal.TryParse(strClientPhone, out decimal clientphone)){
                if (acceptrules == null)
                {
                    var showsuccess = true;
                    if (showsuccess)
                    {
                        ViewData["showsuccess"] = "true";
                        ViewData["customsuccess"] = "Номер телефона введён некорректно";
                        return BadRequest();
                    }
                    return BadRequest();
                }
                return BadRequest();
            }
            else
            {
                if (acceptrules == null)
                {
                    var showsuccess = true;
                    if (showsuccess)
                    {
                        ViewData["showsuccess"] = "true";
                        ViewData["customsuccess"] = "Вы не согласились с обработкой персональных данных";
                        return BadRequest();
                    }
                    return BadRequest();
                }
                else
                {
                    if (DateOnly.Parse(strAdmDate) > DateOnly.Parse(strIssueDate))
                    {
                        showerror = true;
                        if (showerror)
                        {
                            ViewData["showerror"] = "true";
                            ViewData["customerror"] = "Дата принятия не может быть позже даты выселения";
                            return Page();
                        }
                        return BadRequest();
                    }

                    OrderAddDTO order = new OrderAddDTO();
                    order.orderNoteId = 0;
                    order.orderId = 0;
                    order.admDate = DateOnly.Parse(strAdmDate);
                    order.issueDate = DateOnly.Parse(strIssueDate);
                    order.clientPhone = decimal.Parse(strClientPhone);
                    order.clientName = strClientName;
                    order.roomId = roomid;
                    order.animalType = animalType;
                    order.animalName = animalName;
                    order.animalAge = int.Parse(animalAge);
                    order.animalWeight = int.Parse(animalWeight)/1000;
                    order.animalHeight = int.Parse(animalHeight)/100;
                    order.animalBreed = animalBreed;
                    order.animalGen = gen;
                    var response = new HttpClient().PostAsJsonAsync(address + "orders/addneworder", order).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var showsuccess = true;
                        if (showsuccess)
                        {
                            ViewData["showsuccess"] = "true";
                            ViewData["customsuccess"] = "Заявка добавлена";
                            return Page();

                        }
                    }
                    else
                    {
                        return BadRequest("Uncatched ex");
                    }


                    return BadRequest();
                }
            }
        }

        public async Task<IActionResult> OnGet()
        {
            
            var typesResponse = await new HttpClient().GetAsync(address + "getAnimalTypes");
            roomId = Request.Query["roomId"];
            if (typesResponse.IsSuccessStatusCode)
            {
                var jsonString = await typesResponse.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<Animaltype>>(jsonString, mainsettings);
                ViewData["AnimalTypes"] = data;
            }
            return Page();
        }
    }
}
