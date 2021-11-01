using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkVendingMachine.Models
{
    public class EFDrinkRepository : IDrinkRepository
    {
        DrinkContext context;

        public EFDrinkRepository(DrinkContext context)
        {
            this.context = context;
        }

        public IQueryable<Drink> Drinks => context.Drinks;

        public bool Buy(int id)
        {
            var drinkEntity = context.Drinks.FirstOrDefault(d => d.Id == id);
            if(drinkEntity != null)
            {
                if(drinkEntity.Count > 0)
                {
                    drinkEntity.Count--;
                    context.SaveChanges();
                    return true;
                }                
            }

            return false;
        }

        public void Save(Drink drink)
        {
            if(drink.Id == 0)
            {
                context.Drinks.Add(drink);
            }
            else
            {
                var drinkEntity = context.Drinks.FirstOrDefault(d => d.Id == drink.Id);

                if(drinkEntity != null)
                {
                    drinkEntity.Title = drink.Title;
                    drinkEntity.Cost = drink.Cost;
                    drinkEntity.Count = drink.Count;
                    drinkEntity.ImagePath = drink.ImagePath;
                }
            }
            context.SaveChanges();
        }

        public Drink Delete(int id)
        {
            var drink = context.Drinks.FirstOrDefault(d => d.Id == id);

            if(drink != null)
            {
                context.Drinks.Remove(drink);
                context.SaveChanges();
            }
            return drink;
        }
    }
}
