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
    [Authorize]
    public class EveryoneProduct_FoodDayController : Controller
    {
        private Entities db = new Entities();

        // GET: EveryoneProduct_FoodDay
        public ActionResult Index()
        {
            var product_FoodDay = db.Product_FoodDay.Include(p => p.FoodDay).Include(p => p.Product);
            return View(product_FoodDay.ToList());
        }

        // GET: EveryoneProduct_FoodDay/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_FoodDay product_FoodDay = db.Product_FoodDay.Find(id);
            if (product_FoodDay == null)
            {
                return HttpNotFound();
            }
            return View(product_FoodDay);
        }

        // GET: EveryoneProduct_FoodDay/Create
        public ActionResult Create()
        {
            ViewBag.FoodDay_id = new SelectList(db.FoodDay, "id", "Users_id");
            return View();
        }

        // POST: EveryoneProduct_FoodDay/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Product_id,FoodDay_id,weight,count,caloric,protein,carb,fat")] Product_FoodDay product_FoodDay)
        {
            if (ModelState.IsValid)
            {
                db.Product_FoodDay.Add(product_FoodDay);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FoodDay_id = new SelectList(db.FoodDay, "id", "Users_id", product_FoodDay.FoodDay_id);
            ViewBag.Product_id = new SelectList(db.Product, "id", "Users_id", product_FoodDay.Product_id);
            return View(product_FoodDay);
        }

        // GET: EveryoneProduct_FoodDay/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_FoodDay product_FoodDay = db.Product_FoodDay.Find(id);
            if (product_FoodDay == null)
            {
                return HttpNotFound();
            }
            ViewBag.FoodDay_id = new SelectList(db.FoodDay, "id", "Users_id", product_FoodDay.FoodDay_id);
            ViewBag.Product_id = new SelectList(db.Product, "id", "Users_id", product_FoodDay.Product_id);
            return View(product_FoodDay);
        }

        // POST: EveryoneProduct_FoodDay/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Product_id,FoodDay_id,weight,count,caloric,protein,carb,fat")] Product_FoodDay product_FoodDay)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product_FoodDay).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FoodDay_id = new SelectList(db.FoodDay, "id", "Users_id", product_FoodDay.FoodDay_id);
            ViewBag.Product_id = new SelectList(db.Product, "id", "Users_id", product_FoodDay.Product_id);
            return View(product_FoodDay);
        }

        // GET: EveryoneProduct_FoodDay/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_FoodDay product_FoodDay = db.Product_FoodDay.Find(id);
            if (product_FoodDay == null)
            {
                return HttpNotFound();
            }
            return View(product_FoodDay);
        }

        // POST: EveryoneProduct_FoodDay/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product_FoodDay product_FoodDay = db.Product_FoodDay.Find(id);
            db.Product_FoodDay.Remove(product_FoodDay);
            db.SaveChanges();
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
