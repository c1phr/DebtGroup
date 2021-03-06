﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DebtGroup.Models
{
    public class Person
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        //public virtual ICollection<Transaction> Transactions { get; set; } 
    }
}