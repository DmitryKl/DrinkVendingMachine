using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkVendingMachine.Models
{
    public interface ICoinRepository
    {
        IQueryable<Coin> Coins { get; }

        void ChangeBlocked(int id);
        bool AddCoin(int value);
        void TakeCoins(Dictionary<int, int> coins);
    }
}
