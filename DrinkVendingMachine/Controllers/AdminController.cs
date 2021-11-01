using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrinkVendingMachine.Models;
using DrinkVendingMachine.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using DrinkVendingMachine.Infrastructure;

namespace DrinkVendingMachine.Controllers
{
    [ServiceFilter(typeof(CustomAuthorizationAttribute))]
    public class AdminController : Controller
    {
        IDrinkRepository drinkRepository;
        ICoinRepository coinRepository;
        IWebHostEnvironment appEnvironment;

        public AdminController(IDrinkRepository drinkRepository, ICoinRepository coinRepository, IWebHostEnvironment appEnvironment)
        {
            this.drinkRepository = drinkRepository;
            this.coinRepository = coinRepository;
            this.appEnvironment = appEnvironment;
        }

        public IActionResult Index()
        {
            ViewBag.Key = HttpContext.Request.Query["key"].ToString();
            return View(new DrinkListViewModel()
            {
                Coins = coinRepository.Coins,
                Drinks = drinkRepository.Drinks
            });
        }

        public IActionResult DrinkList()
        {
            return PartialView(drinkRepository.Drinks);
        }

        public IActionResult DrinkItem(int id)
        {
            var drink = drinkRepository.Drinks.FirstOrDefault(d => d.Id == id);
            if(drink == null)
            {
                drink = new Drink();
            }
            return PartialView(drink);
        }

        [HttpPost]
        public async Task<IActionResult> SaveDrink(Drink drink, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string path = "/Files/" + file.FileName;
                    using (var fileStream = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    drink.ImagePath = path;
                }

                drinkRepository.Save(drink);

                TempData["Message"] =  "Сохранено";
            }
            else
            {
                TempData["Message"] = "Неверные данные";
            }
            return PartialView("DrinkItem", drink);
        }

        [HttpPost]
        public IActionResult DeleteDrink(int id)
        {
            var deletedDrink = drinkRepository.Delete(id);
            if(deletedDrink != null)
            {
                TempData["Message"] = $"{deletedDrink.Title} был удален";
            }
            else
            {
                TempData["Message"] = "Напиток не найден.";
            }
            return PartialView("DrinkItem", deletedDrink);
        }

        public IActionResult CoinList()
        {
            return PartialView("CoinList", coinRepository.Coins);
        }

        [HttpPost]
        public IActionResult ChangeBlockCoin(int id)
        {
            coinRepository.ChangeBlocked(id);
            return Ok();
        }
    }
}
