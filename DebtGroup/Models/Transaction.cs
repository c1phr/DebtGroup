using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

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
        public string SplitWith { get; set; }
        
        [JsonIgnore]
        public virtual Person Person { get; set; }        
    }
}