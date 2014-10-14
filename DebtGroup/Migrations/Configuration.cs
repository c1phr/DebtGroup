using System.Collections.Generic;
using DebtGroup.Models;

namespace DebtGroup.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DebtGroup.DAL.DebtGroupContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DebtGroup.DAL.DebtGroupContext context)
        {
            var people = new List<Person>
            {
                new Person {FirstName = "Ryan", LastName = "Batchelder"},
                new Person {FirstName = "Cassie", LastName = "Badalamenti"},
                new Person {FirstName = "Dustin", LastName = "Story"}
            };

            people.ForEach(s => context.Persons.AddOrUpdate(s));
            context.SaveChanges();
        }
    }
}
