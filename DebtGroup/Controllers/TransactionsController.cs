using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using DebtGroup.DAL;
using DebtGroup.Models;
using System.Data.Entity.Infrastructure;
using DebtGroup.ViewModels;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;

namespace DebtGroup.Controllers
{
    public class TransactionsController : Controller
    {
        private DebtGroupContext db = new DebtGroupContext();

        // GET: Transactions
        public ActionResult Index()
        {
            var viewModel = new TransactionViewModel();
            viewModel.Transactions = db.Transactions.Include(i => i.Person);
            viewModel.Persons = db.Persons;
            return View(viewModel);
        }

        // GET: Transactions/Details/5
        public string Details(int? id)
        {
            if (id == null)
            {
                return "Bad Request";
            }           
            var transaction = (from tran in db.Transactions
                               where tran.ID == id
                               join per in db.Persons on tran.Purchaser equals per.ID
                               select new {tran.ID, tran.Amount, tran.Description, tran.SplitWith, per.FirstName, per.LastName}).SingleOrDefault();      
            
            if (transaction == null)
            {                
                return "Transaction not found";
            }            
           
            return JsonConvert.SerializeObject(transaction);
        }

        // GET: Transactions/Create
        public ActionResult Create()
        {
            PopulatePurchaserDropdown();
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[System.Web.Mvc.HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ID,Purchaser,Amount,Description")] Transaction transaction)
        //{
        //    if (ModelState.IsValid)
        //    {                
        //        db.Transactions.Add(transaction);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(transaction);
        //}

        [System.Web.Mvc.HttpPost]
        public JsonResult Create([FromBody] Transaction trans)
        {
            if (trans.Amount > 0 && !string.IsNullOrEmpty(trans.Description) && trans.Purchaser != 0 && !string.IsNullOrEmpty(trans.SplitWith))
            {
                db.Transactions.Add(trans);
                db.SaveChanges();
                return Json("Transaction added");
            }
            return Json("Error adding transaction");
        }

        // GET: Transactions/Edit/5
        //public string Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return null;
        //    }
        //    Transaction transaction = db.Transactions.Find(id);
        //    if (transaction == null)
        //    {
        //        return "Not found";
        //    }                                         
        //    return JsonConvert.SerializeObject(transaction);
        //}

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [System.Web.Mvc.HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ID,Purchaser,Amount,Description,Persons")] Transaction transaction)
        public JsonResult Edit([FromBody]Transaction trans)
        {
            db.Entry(trans).State = EntityState.Modified;
            db.SaveChanges();
            //if (ModelState.IsValid)
            //{
            //    ModelState.SetModelValue("SplitWith", new ValueProviderResult(String.Join(",", ModelState["Persons"].ToString()), string.Empty, new CultureInfo("en-US")));
            //    transaction.Persons = db.Persons.ToList().Select(x => new SelectListItem
            //    {
            //        Text = x.FullName,
            //        Value = x.ID.ToString()
            //    });
            //    ModelState.Remove("Persons");
            //    db.Entry(transaction).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //return View(transaction);
            return Json("Updated");
        }

        // GET: Transactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [System.Web.Mvc.HttpPost, System.Web.Mvc.ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transaction transaction = db.Transactions.Find(id);
            db.Transactions.Remove(transaction);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private void PopulatePurchaserDropdown(object selectedPerson = null)
        {
            var personQuery = from p in db.Persons  
                orderby p.LastName
                select p;
            ViewBag.Purchaser = new SelectList(personQuery, "ID", "FullName", selectedPerson);
        }

        private void PopulateSplitDropdown(object selectedPerson = null)
        {
            var personQuery = from p in db.Persons
                              orderby p.LastName
                              select p;
            ViewBag.Persons = new MultiSelectList(personQuery, "ID", "FullName");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
