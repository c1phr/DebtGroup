using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DebtGroup.Models;

namespace DebtGroup.ViewModels
{
    public class TransactionViewModel
    {
        public IEnumerable<Person> Persons { get; set; }
        public IEnumerable<TransactionRestModel> Transactions { get; set; } 
    }
}