using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DebtGroup.ViewModels
{
    public class TransactionRestModel
    {        
        public int Purchaser { get; set; }                
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public int[] SplitWith { get; set; }                
    }
}