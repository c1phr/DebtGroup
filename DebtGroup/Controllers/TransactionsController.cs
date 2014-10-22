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

        public ActionResult Index()
        {
            var viewModel = new TransactionViewModel();
            viewModel.Transactions = db.Transactions.Include(i => i.Purchaser);
            viewModel.Persons = db.Persons;
            return View(viewModel);
        }


        // GET: Transactions
        public JsonResult TransactionList()
        {
            var viewModel = new TransactionViewModel();
            var transactions = db.Transactions.GroupBy(t => t.TransactionID);
            foreach (var transaction in transactions)
            {
                
            }
            //viewModel.Persons = db.Persons;
            return Json(viewModel, JsonRequestBehavior.AllowGet);
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
        public JsonResult Create([FromBody] TransactionRestModel restTrans)
        {
            int newId;
            try
            {
                newId = db.Transactions.Max(t => t.TransactionID) + 1;
            }
            catch (Exception)
            {
                newId = 0;                
            }
            if (restTrans.Amount > 0 && !string.IsNullOrEmpty(restTrans.Description) && restTrans.Purchaser != 0 && restTrans.SplitWith.Length > 0)
            {
                foreach (var splitID in restTrans.SplitWith)
                {
                    //var trans = new Transaction
                    //{
                    //    ID = newId,
                    //    Amount = restTrans.Amount,
                    //    Purchaser = restTrans.Purchaser,
                    //    Description = restTrans.Description,
                    //    SplitWith = splitID
                    //};
                    db.Transactions.Add(
                        new Transaction
                    {
                        TransactionID = newId,
                        Amount = restTrans.Amount,
                        Purchaser = restTrans.Purchaser,
                        Description = restTrans.Description,
                        SplitWith = splitID
                    });

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        return Json(ex);
                    }
                }

                return Json("Transaction added");
            }
            return Json("You forgot a field");
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
        public JsonResult Edit([FromBody]TransactionRestModel restTrans)
        {
            if (restTrans.Amount > 0 && !string.IsNullOrEmpty(restTrans.Description) && restTrans.Purchaser != 0 && restTrans.SplitWith.Length > 0)
            {
                foreach (var splitID in restTrans.SplitWith)
                {
                    var trans = new Transaction
                    {
                        Amount = restTrans.Amount,
                        Purchaser = restTrans.Purchaser,
                        Description = restTrans.Description,
                        SplitWith = splitID
                    };
                    db.Entry(trans).State = EntityState.Modified;
                }

                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return Json(ex);
                }
                return Json("Transaction added");
            }
            return Json("You forgot a field");
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
