using AdsWithLogins.Data;
using AdsWithLogins.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace AdsWithLogins.Web.Controllers
{
    public class HomeController : Controller
    {
        private string connectionString = @"Data Source=.\sqlexpress;Initial Catalog=Giveaway; Integrated Security=true; TrustServerCertificate=true";


        public IActionResult Index()
        {
            var mgr = new GiveawayAdsManager(connectionString);
            var vm = new GiveawaysViewModel();
            vm.GiveawayAds = mgr.GetGiveawayAds();
            vm.UserId = mgr.GetUserIdByEmail(User.Identity.Name);
            return View(vm);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Added(string name, string details, string phoneNumber)
        {
            var mgr = new GiveawayAdsManager(connectionString);
            int userId = (int)mgr.GetUserIdByEmail(User.Identity.Name);
            mgr.AddGiveaway(name, details, phoneNumber, userId);
            return Redirect("/Home/Index");
        }

        [Authorize]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var mgr = new GiveawayAdsManager(connectionString);
            mgr.Delete(id);
            return Redirect("/Home/Index");
        }

        [Authorize]
        public IActionResult Account()
        {
            var mgr = new GiveawayAdsManager(connectionString);
            var vm = new GiveawaysViewModel();
            vm.GiveawayAds = mgr.GetGiveawayAds();
            vm.UserId = mgr.GetUserIdByEmail(User.Identity.Name);
            return View(vm);
        }
    }

}
