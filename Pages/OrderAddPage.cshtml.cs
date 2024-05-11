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
        public List<Service> servicesList;
        public List<Animaltype> animalTypes;
        private static JsonSerializerSettings mainsettings = new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
        public static string address = "http://localhost:8081/";
        public IActionResult OnPost(int roomid)
        {
            var showerror = false;
            string strClientName = Request.Form["clientName"];
            string strRoomid = Request.Form["roomId"];
            string strAdmDate = Request.Form["admissionDate"];
            string strAnimalid = Request.Form["animalId"];
            string strIssueDate = Request.Form["issueDate"];
            if (string.IsNullOrEmpty(strClientName) || string.IsNullOrEmpty(strRoomid) || string.IsNullOrEmpty(strAdmDate) || string.IsNullOrEmpty(strIssueDate) && string.IsNullOrEmpty(strAnimalid))
            {
                showerror = true;
                if (showerror)
                {
                    ViewData["showerror"] = "true";
                    ViewData["customerror"] = "��������� ��� ����";
                    return Page();
                }
                return BadRequest();
            }
            else if (!int.TryParse(strClientName, out int clientid) | !int.TryParse(strAnimalid, out int animalid))
            {
                showerror = true;
                if (showerror)
                {
                    ViewData["showerror"] = "true";
                    ViewData["customerror"] = "ID �� ����� ���� �� �������";
                    return Page();
                }
                return BadRequest();
            }
            else
            {
                Order order = new Order();
                order.ClientId = clientid;
                order.RoomId = roomid;
                order.AdmissionDate = DateOnly.Parse(strAdmDate);
                order.IssueDate = DateOnly.Parse(strIssueDate);
                order.AnimalId = animalid;
                var response = new HttpClient().PostAsJsonAsync(address + "orders/addneworder", order).Result;
                if (response.IsSuccessStatusCode)
                {
                    var showsuccess = true;
                    if (showsuccess)
                    {
                        ViewData["showsuccess"] = "true";
                        ViewData["customsuccess"] = "������ ���������";
                        return Page();

                    }
                }
                else
                {
                    showerror = true;
                    if (showerror)
                    {
                        ViewData["showerror"] = "true";
                        ViewData["customerror"] = "���� �������� �� ����� ���� ����� ���� ���������";
                        return Page();
                    }
                    return BadRequest();
                }

                return BadRequest();
            }
        }

        public async Task<IActionResult> OnGet()
        {
            
            var typesResponse = await new HttpClient().GetAsync(address + "getAnimalTypes");

            if (typesResponse.IsSuccessStatusCode)
            {
                var jsonString = await typesResponse.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<Animaltype>>(jsonString);
                ViewData["AnimalTypes"] = data;
            }
            // ������� IActionResult, ��������, RedirectToPage ��� View
            return Page();

        }

        public async Task<IActionResult> GetBreeds(int typeid)
        {
            var catlist = await new HttpClient().GetAsync(address + "getCatBreeds");
            var doglist = await new HttpClient().GetAsync(address + "getDogBreeds");
            var breedlist = await new HttpClient().GetAsync(address + "getAllBreeds");

            if (breedlist.IsSuccessStatusCode)
            {
                if (typeid == 1)
                {
                    var jsonString = await catlist.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<List<Animaltype>>(jsonString);
                    ViewData["AnimalBreeds"] = data;
                }

                if (typeid == 2)
                {
                    var jsonString = await doglist.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<List<Animaltype>>(jsonString);
                    ViewData["AnimalBreeds"] = data;
                }
                else
                {
                    var jsonString = await breedlist.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<List<Animaltype>>(jsonString);
                    ViewData["AnimalBreeds"] = data;
                }

            }

            return Page();
        }
    }
}
