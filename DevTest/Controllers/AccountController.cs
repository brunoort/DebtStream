using DevTest.Service.Models;
using DevTest.Service.Services;
using DevTest.Services.Models;
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

        public async Task<IActionResult> Dashboard()
        {
            if(ViewData["AccountInfo"] == null) { 

                var accountId = HttpContext.Session.GetString("accountId");

                var userResponse = await _debtApiService.GetAccount(accountId);

                var accountData = new
                {
                    accountId = accountId, 
                    loginTime = DateTime.Now,
                    userData = userResponse
                };

                ViewData["AccountInfo"] = JsonConvert.SerializeObject(accountData);

                var accountInfoJson = ViewData["AccountInfo"] as string;
                AccountInfo accountInfo = JsonConvert.DeserializeObject<AccountInfo>(accountInfoJson);

                var accountResponse = await _debtApiService.GetAccount(accountInfo.accountId);

                ViewBag.AccountResponse = accountResponse;

                return View("Dashboard");
            } else
            {
                return View("Dashboard");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            var registerResponse = await _debtApiService.Register(model);

            if(registerResponse != true)
            {
                ViewBag.Error = "Registration Failed";
                return View();
            }

            HttpContext.Session.SetString("accountId", model.AccountId);

            var userResponse = await _debtApiService.GetAccount(model.AccountId);

            var accountInfo = new
            {
                accountId = model.AccountId,
                userEmail = model.Email,
                loginTime = DateTime.Now,
                userData = userResponse
            };

            ViewData["AccountInfo"] = JsonConvert.SerializeObject(accountInfo);

            return Json(new { redirectUrl = Url.Action("Dashboard", "Account") });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateEmail([FromBody] UpdateEmailViewModel model)
        {
            string accountId = HttpContext.Session.GetString("accountId");

            if (string.IsNullOrEmpty(accountId))
                return RedirectToAction("Login", "Home");

            var registerResponse = await _debtApiService.UpdateEmail(accountId, model);

            if (!registerResponse)
            {
                return Json(new
                {
                    success = false,
                    error = "Update failed. Please try again.",
                    redirectUrl = Url.Action("UpdateEmail", "Account")
                });
            }

            return Json(new
            {
                success = true,
                redirectUrl = Url.Action("Dashboard", "Account")
            });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAddress([FromBody] UpdateAddressViewModel model)
        {
            string accountId = HttpContext.Session.GetString("accountId");

            if (string.IsNullOrEmpty(accountId))
                return RedirectToAction("Login", "Home");

            var registerResponse = await _debtApiService.UpdateAddress(accountId, model);

            if (!registerResponse)
            {
                return Json(new
                {
                    success = false,
                    error = "Update failed. Please try again.",
                    redirectUrl = Url.Action("UpdateAddress", "Account")
                });
            }

            return Json(new
            {
                success = true,
                redirectUrl = Url.Action("Dashboard", "Account")
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var response = _debtApiService.ValidateLogin(model.Email, model.Password).Result;

            if (response.isSuccessful == true)
            {
                //adding this because the validateLogin api response don't match
                //with the correcty accountId
                if(response.AccountId == "123")
                {
                    response.AccountId = "ACC001";
                }

                HttpContext.Session.SetString("accountId", response.AccountId);

                var userResponse = await _debtApiService.GetAccount(response.AccountId);

                var accountInfo = new
                {
                    accountId = response.AccountId,
                    userEmail = response.Email,
                    loginTime = DateTime.Now,
                    userData = userResponse
                };

                ViewData["AccountInfo"] = JsonConvert.SerializeObject(accountInfo);
                return Json(new { redirectUrl = Url.Action("Dashboard", "Account") });
            }

            return Json(new
            {
                success = false,
                error = "Login failed. Please try again.",
                redirectUrl = Url.Action("Index", "Home")
            });
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
