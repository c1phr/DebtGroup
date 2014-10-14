using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DebtGroup.Models;

namespace DebtGroup.DAL
{
    public class DebtGroupContext : DbContext
    {
        public DebtGroupContext() : base("DebtGroupContext")
        {
            
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

    }
}