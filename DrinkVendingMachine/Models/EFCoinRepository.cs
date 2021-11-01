using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkVendingMachine.Models
{
    public class EFCoinRepository : ICoinRepository
    {
        DrinkContext context;

        public EFCoinRepository(DrinkContext context)
        {
            this.context = context;
        }

        public IQueryable<Coin> Coins => context.Coins;

        public void ChangeBlocked(int id)
        {
            var coin = context.Coins.FirstOrDefault(c => c.Id == id);
            if(coin != null)
            {
                coin.Blocked = !coin.Blocked;
                context.SaveChanges();
            }
        }

        public bool AddCoin(int value)
        {
            var coin = context.Coins.FirstOrDefault(c => c.Value == value && !c.Blocked);

            if(coin != null)
            {
                coin.Count++;
                context.SaveChanges();
                return true;
            }

            return false;
        }

        public void TakeCoins(Dictionary<int, int> coins)
        {
            foreach (var (coinValue, coinCount) in coins)
            {
                var coin = context.Coins.FirstOrDefault(c => c.Value == coinValue);

                if (coin == null)
                {
                    throw new Exception($"Монеты с номиналом {coinValue} не существует.");
                }

                if (coin.Count < coinCount)
                {
                    throw new Exception($"Недостаточное количество монет номиналом {coinCount}.");
                }

                coin.Count -= coinCount;
            }

            context.SaveChanges();
        }
    }
}
