using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace DebtGroup.Models
{
    public class Transaction
    {
        [Key, Column(Order = 0)]
        public int ID { get; set; }
        
        [Key, Column(Order = 1)]
        public int Purchaser { get; set; }

        [ForeignKey("Purchaser")]
        public virtual Person PurchaserID { get; set; }

        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        public string Description { get; set; }
        
        [Key, Column(Order = 2)]
        public int SplitWith { get; set; }

        [ForeignKey("SplitWith")]
        public virtual Person SplitID { get; set; }

        //public IEnumerable<SelectListItem> Persons { get; set; }
        
        //[JsonIgnore]                
        //public virtual Person Person { get; set; }    
    }
}