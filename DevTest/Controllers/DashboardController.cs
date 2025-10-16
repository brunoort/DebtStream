using DevTest.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace DevTest.Controllers
{
    public class DashboardController : Controller
    {
        public DebtApiService _debtApiService;

        public DashboardController(DebtApiService debtApiService)
        {
            _debtApiService = debtApiService;
        }

        public async Task<IActionResult> Index(string accountId, string userEmail, DateTime loginTime)
        {
            ViewBag.AccountId = accountId;
            ViewBag.UserEmail = userEmail;
            ViewBag.LoginTime = loginTime;

            var accountInfo = await _debtApiService.GetAccount(accountId);

            return View(accountInfo);
        }
    }
}
