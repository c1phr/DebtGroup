using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DebtGroup.ViewModels
{
    public class TransactionIndexModel : TransactionRestModel
    {
        public new string Purchaser { get; set; }
        public new string[] SplitWith { get; set; }                
    }
}