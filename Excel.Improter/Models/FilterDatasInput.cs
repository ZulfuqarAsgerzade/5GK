using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Excel.Improter.Models
{
    public class FilterDatasInput
    {
        public string Country { get; set; }
        public bool FirstExpensive { get; set; }
        public decimal PriceRangeStart { get; set; }
        public decimal PriceRangeEnd { get; set; }
    }
}
