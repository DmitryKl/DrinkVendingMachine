using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DrinkVendingMachine.Models
{
    public class Drink
    {
        public int Id { get; set; }
        [Display(Name ="Наименование")]
        public string Title { get; set; }
        [Display(Name = "Цена")]
        public int Cost { get; set; }
        [Display(Name = "Количество")]
        public int Count { get; set; } = 0;
        [Display(Name = "Изображение")]
        public string ImagePath { get; set; }
    }
}
