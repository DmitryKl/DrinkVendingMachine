using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DrinkVendingMachine.Models;
using DrinkVendingMachine.Models.ViewModels;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;

namespace DrinkVendingMachine.Controllers
{
    public class HomeController : Controller
    {
        IDrinkRepository drinkRepository;
        ICoinRepository coinRepository;

        public HomeController(IDrinkRepository drinkRepository, ICoinRepository coinRepository)
        {
            this.drinkRepository = drinkRepository;
            this.coinRepository = coinRepository;
        }

        public IActionResult Index()
        {
            return View(new DrinkListViewModel()
            {
                Coins = coinRepository.Coins,
                Drinks = drinkRepository.Drinks
            });
        }

        public IActionResult Buy(int id)
        {
            bool drinkBought = drinkRepository.Buy(id);

            if (drinkBought)
            {
                return Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status406NotAcceptable);
            }
        }

        public IActionResult AddCoin(int value)
        {
            if (coinRepository.AddCoin(value))
            {
                return Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status406NotAcceptable);
            }
        }

        public IActionResult TakeChange(int value)
        {
            Dictionary<int, int> coins = coinRepository.Coins.ToDictionary(c => c.Value, c => c.Count);

            var changeCalculateResult = GetChange(coins, value);

            if (changeCalculateResult.Success)
            {
                try
                {
                    coinRepository.TakeCoins(changeCalculateResult.Coins);
                }
                catch
                {
                    return StatusCode(StatusCodes.Status406NotAcceptable);
                }
            }
            else
            {
                return StatusCode(StatusCodes.Status406NotAcceptable);
            }

            return PartialView("ChangeResult", changeCalculateResult.Coins);
        }

        class ChangeCalculateResult
        {
            public bool Success { get; set; }
            public Dictionary<int, int> Coins { get; set; }
        }

        private ChangeCalculateResult GetChange(Dictionary<int, int> coins, int value)
        {
            var result = new ChangeCalculateResult();

            result.Coins = GreedCalculateChange(coins, value);
            result.Success = result.Coins.Sum(c => c.Key * c.Value) == value;

            return result;
        }

        private Dictionary<int, int> GreedCalculateChange(Dictionary<int, int> coins, int value)
        {
            Dictionary<int, int> result = new Dictionary<int, int>();

            int rest = value;

            foreach (var (coinValue, coinCount) in new SortedDictionary<int, int>(coins).Reverse())
            {
                var count = Math.Min(coinCount, rest / coinValue);
                rest -= count * coinValue;

                if(count > 0)
                {
                    result[coinValue] = count;
                }
                

                if (rest == 0)
                {
                    break;
                }
            }

            return result;
        }
    }
}
