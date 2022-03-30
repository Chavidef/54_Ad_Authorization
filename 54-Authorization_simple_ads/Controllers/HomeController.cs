using _54_Authorization_simple_ads.Data;
using _54_Authorization_simple_ads.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace _54_Authorization_simple_ads.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _connectionString = @"Data Source=.\sqlexpress;Initial Catalog=SimpleAdsAuthorization;Integrated Security=true;";

        [Authorize]
        public IActionResult NewAd()
        {
            return View();
        }
        [HttpPost]
        public IActionResult NewAd(Ad ad)
        {
            if(!User.Identity.IsAuthenticated)
            {
                return Redirect("/account/login");
            }
            var adRepo = new AdRepository(_connectionString);
            var userRepo = new UserRepository(_connectionString);
            var user = userRepo.GetByEmail(User.Identity.Name);
            ad.UserId = user.Id;
            adRepo.AddAd(ad);
            return Redirect("/");
        }
        public IActionResult Index()
        {
            var adRepo = new AdRepository(_connectionString);
            var userRepo = new UserRepository(_connectionString);
            var ads = adRepo.GetAllAds();
            if (User.Identity.IsAuthenticated)
            {
                var user = userRepo.GetByEmail(User.Identity.Name);
                foreach (Ad ad in ads)
                {
                    if (ad.UserId == user.Id)
                    {
                        ad.IsAuthorized = true;
                    }
                    else
                    {
                        ad.IsAuthorized = false;
                    }
                }

            }
            var vm = new AdViewModel();
            vm.Ads = ads;
            return View(vm);
        }
        [Authorize]
        [HttpPost]
        public IActionResult DeleteAd(int id)
        {
            var adRepo = new AdRepository(_connectionString);
            adRepo.DeleteAd(id);
            return Redirect("/");
        }
        [Authorize]
        public IActionResult MyAccount()
        {
            var userRepo = new UserRepository(_connectionString);
            var adRepo = new AdRepository(_connectionString);
            var user = userRepo.GetByEmail(User.Identity.Name);
            var ads = adRepo.GetAdsById(user.Id);
            var vm = new AdViewModel();
            vm.Ads = ads;
            return View(vm);
        }




    }
}
