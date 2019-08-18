using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjektNutrition.Models;

namespace ProjektNutrition.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class FoodDaysController : Controller
    {
        private Entities db = new Entities();

        // GET: FoodDays
        public ActionResult Index()
        {
            var foodDay = db.FoodDay.Include(f => f.AspNetUsers);
            return View(foodDay.ToList());
        }

        // GET: FoodDays/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodDay foodDay = db.FoodDay.Find(id);
            if (foodDay == null)
            {
                return HttpNotFound();
            }
            return View(foodDay);
        }

        // GET: FoodDays/Create
        public ActionResult Create()
        {
            ViewBag.Users_id = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: FoodDays/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Users_id,date,sumCaloric,sumProtein,sumCarb,sumFat,description")] FoodDay foodDay)
        {
            if (ModelState.IsValid)
            {
                db.FoodDay.Add(foodDay);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Users_id = new SelectList(db.AspNetUsers, "Id", "Email", foodDay.Users_id);
            return View(foodDay);
        }

        // GET: FoodDays/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodDay foodDay = db.FoodDay.Find(id);
            if (foodDay == null)
            {
                return HttpNotFound();
            }
            ViewBag.Users_id = new SelectList(db.AspNetUsers, "Id", "Email", foodDay.Users_id);
            return View(foodDay);
        }

        // POST: FoodDays/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Users_id,date,sumCaloric,sumProtein,sumCarb,sumFat,description")] FoodDay foodDay)
        {
            if (ModelState.IsValid)
            {
                db.Entry(foodDay).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Users_id = new SelectList(db.AspNetUsers, "Id", "Email", foodDay.Users_id);
            return View(foodDay);
        }

        // GET: FoodDays/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodDay foodDay = db.FoodDay.Find(id);
            if (foodDay == null)
            {
                return HttpNotFound();
            }
            return View(foodDay);
        }

        // POST: FoodDays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FoodDay foodDay = db.FoodDay.Find(id);
            if(foodDay.Product_FoodDay.Count == 0)
            {
                db.FoodDay.Remove(foodDay);
                db.SaveChanges();
            }
            
            return RedirectToAction("Index");
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
