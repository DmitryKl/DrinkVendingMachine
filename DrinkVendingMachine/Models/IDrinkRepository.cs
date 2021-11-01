using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkVendingMachine.Models
{
    public interface IDrinkRepository
    {
        IQueryable<Drink> Drinks { get; }
        bool Buy(int id);
        void Save(Drink drink);
        Drink Delete(int id);
    }
}
