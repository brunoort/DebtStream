using DevTest.Service.Models;
using DevTest.Service.Services;
using DevTest.Services.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DevTest.Controllers
{
    public class AccountController : Controller
    {
        public DebtApiService _debtApiService;

        public AccountController(DebtApiService debtApiService)
        {
            _debtApiService = debtApiService;
        }

        public IActionResult RegisterPage() => View();

        public IActionResult UpdateAddress() => View();
        public IActionResult UpdateEmail() => View();


        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            var registerResponse = await _debtApiService.Register(model);

            if(registerResponse != true)
            {
                ViewBag.Error = "Registration Failed";
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateEmail([FromBody] UpdateEmailViewModel model)
        {
            string accountId = HttpContext.Session.GetString("accountId");

            if (string.IsNullOrEmpty(accountId))
                return RedirectToAction("Login", "Home");

            var registerResponse = await _debtApiService.UpdateEmail(accountId, model);

            if (registerResponse != true)
            {
                ViewBag.Error = "Updated Failed";
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAddress([FromBody] UpdateAddressViewModel model)
        {
            string accountId = HttpContext.Session.GetString("accountId");

            if (string.IsNullOrEmpty(accountId))
                return RedirectToAction("Login", "Home");

            var registerResponse = await _debtApiService.UpdateAddress(accountId, model);

            if (registerResponse != true)
            {
                ViewBag.Error = "Updated Failed";
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var response = _debtApiService.ValidateLogin(model.Email, model.Password).Result;

            if (response.isSuccessful == true)
            {
                var accountInfo = new { 
                    accountId = "ACC-2024-0012345",
                    userEmail = model.Email, 
                    loginTime = DateTime.Now 
                };

                HttpContext.Session.SetString("accountId", accountInfo.accountId);

                TempData["AccountInfo"] = JsonConvert.SerializeObject(accountInfo);
                return Json(new { redirectUrl = Url.Action("Index", "Dashboard") });
            }

            ModelState.AddModelError("", "Login inválido.");
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
