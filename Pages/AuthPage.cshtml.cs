using ah4cClientApp.DTO;
using ah4cClientApp.Pages.Shared;
using ah4cClientApp.Services;
using AnimalHouseRestAPI.Models;
using AnimalHouseRestAPI.ModelsDTO;
using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using NuGet.Protocol;

namespace ah4cClientApp.Pages
{
    public class AuthPageModel : PageModel
    {
        public static string userforform;
        public static string passwordforform;
        private readonly ILogger<IndexModel> _logger;

        AuthService auth = new AuthService();


        public async Task<ActionResult> OnPost(string username, string password)
        {
            string address = "http://localhost:8081/";
            username = Request.Form["username"];
            password = Request.Form["password"];
            string result = auth.AuthCheck(username, password);
            if (result == "LoginCheckFault")
            {
                var showerror = true;
                if (showerror)
                {
                    ViewData["showerror"] = "true";
                    ViewData["customerror"] = "Логин не может быть пустым!";
                    return Page();
                }
                return RedirectToPage("./AuthPage");
            }
            else if (result == "PasswordCheckFault")
            {
                var showerror2 = true;
                if (showerror2)
                {
                    ViewData["showerror"] = "true";
                    ViewData["customerror"] = "Пароль не может быть пустым!";
                    return Page();
                }
                return RedirectToPage("./AuthPage");
            }
            else if (result == "CheckFault")
            {
                var showerror3 = true;
                if (showerror3)
                {
                    ViewData["showerror"] = "true";
                    ViewData["customerror"] = "Заполните все поля!";
                    return Page();
                }
                return RedirectToPage("./AuthPage");
            }
            else
            {

                ClientDTO clientDTO = new ClientDTO(username, password);
                var response = new HttpClient().PostAsJsonAsync(address + "clients/log", clientDTO).Result;
                var client = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
                
                
                if (response.IsSuccessStatusCode)
                {
                    IndexModel.check = true;
                    return RedirectToPage("UserCabPage");
                }
                else
                {
                    

                    var showerror4 = true;
                    if (showerror4)
                    {
                        ViewData["showerror"] = "true";
                        ViewData["customerror"] = "Неверный логин или пароль!";
                        return Page();
                    }
                    return RedirectToPage("./AuthPage");
                }
            }
        }
    }
}
