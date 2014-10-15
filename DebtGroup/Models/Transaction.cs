using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace DebtGroup.Models
{
    public class Transaction
    {
        public int ID { get; set; }
        [ForeignKey("Person")]
        public int Purchaser { get; set; }

        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        public string Description { get; set; }
        public int[] SplitWith { get; set; }
        
        public virtual Person Person { get; set; }
    }
}