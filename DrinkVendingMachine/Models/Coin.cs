using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DrinkVendingMachine.Models
{
    public class Coin
    {
        public int Id { get; set; }
        [Range(0,10000)]
        public int Value { get; set; }
        public bool Blocked { get; set; } = false;
        [Range(0, int.MaxValue)]
        public int Count { get; set; } = 0;
    }
}
