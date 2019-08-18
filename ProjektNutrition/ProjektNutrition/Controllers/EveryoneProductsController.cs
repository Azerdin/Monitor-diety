using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjektNutrition.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

namespace ProjektNutrition.Controllers
{
    public class EveryoneProductsController : Controller
    {
        private Entities db = new Entities();

        // GET: EveryoneProducts
        public ActionResult Index(int? id)
        {
            var idUser = User.Identity.GetUserId();
            var id1 = id;
            var product = db.Product.Include(p => p.AspNetUsers).Include(p => p.Category).Where(
            model => /*(model.AspNetUsers.Id == idUser ||
            model.AspNetUsers.AspNetUserRoles.FirstOrDefault().AspNetRoles.Name.Equals("Administrator") ||
            model.AspNetUsers.AspNetUserRoles.FirstOrDefault().AspNetRoles.Name.Equals("Dietetyk")) &&(*/
           ( model.Category.id == id)



            );
            return View(product.ToList());
        }

        // GET: EveryoneProducts/Details/5
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }
            int sum = (int) (product.carb + product.fat + product.protein);
            int carb = (int) (product.carb);
            int fat = (int) (product.fat);
            int protein = (int) (product.protein);
            List<DataPoint> dataPoints = new List<DataPoint>();
            dataPoints.Add(new DataPoint("Węglowodane", 30));
            dataPoints.Add(new DataPoint("Tłuszcz", 40));
            dataPoints.Add(new DataPoint("Białko", 30));


            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);
            ViewBag.Protein = protein;
            ViewBag.Carb = carb;
            ViewBag.Fat = fat;
            return View(product);
        }

        // GET: EveryoneProducts/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details([Bind(Include = "id,weight")] Product chooseProduct)
        {
            var idUser = User.Identity.GetUserId();
            AspNetUsers user = db.AspNetUsers.Find(idUser);
            Product_FoodDay product_FoodDay = new Product_FoodDay();
            var foodDay1 = db.FoodDay.Where(model => model.AspNetUsers.Id == idUser).ToList();
            var id1 = foodDay1.Last().id;
            FoodDay LastfoodDay = db.FoodDay.Find(id1);
            Product p1 = db.Product.Find(chooseProduct.id);
            product_FoodDay.weight = chooseProduct.weight;
            product_FoodDay.Product_id = chooseProduct.id;
            product_FoodDay.FoodDay_id = LastfoodDay.id;
            chooseProduct.weight = p1.weight;
            db.Product_FoodDay.Add(product_FoodDay);
            db.SaveChanges();

            FoodDay foodDay = db.FoodDay.Find(product_FoodDay.FoodDay_id);
            Product product = db.Product.Find(product_FoodDay.Product_id);
            /*if(product_FoodDay.weight == null)
            {
                foodDay.sumCaloric = product.caloric * (double)product_FoodDay.count;
                foodDay.sumCarb += product.carb * (double)product_FoodDay.count;
                foodDay.sumProtein += product.protein * (double)product_FoodDay.count;
                foodDay.sumFat += product.fat * (double)product_FoodDay.count;
                db.Entry(foodDay).State = EntityState.Modified;
                db.SaveChanges();
            }*/
            //else if(product_FoodDay.count == null)
            //{
            product_FoodDay.caloric = product.caloric * (double)product_FoodDay.weight / (double)product.weight;
            product_FoodDay.carb = product.carb * (double)product_FoodDay.weight / (double)product.weight;
            product_FoodDay.protein = product.protein * (double)product_FoodDay.weight / (double)product.weight;
            product_FoodDay.fat = product.fat * (double)product_FoodDay.weight / (double)product.weight;
            product_FoodDay.caloric = product.caloric * (double)product_FoodDay.weight / (double)product.weight;

            foodDay.sumCaloric += product.caloric * (double)product_FoodDay.weight / (double)product.weight;
            foodDay.sumCarb += product.carb * (double)product_FoodDay.weight / (double)product.weight;
            foodDay.sumProtein += product.protein * (double)product_FoodDay.weight / (double)product.weight;
            foodDay.sumFat += product.fat * (double)product_FoodDay.weight / (double)product.weight;
            if(user.caloricDemand != null)
            {
                if (foodDay.sumCaloric < (int)user.caloricDemand)
                {
                    foodDay.description = "Za mało spożyłeś kalorii";
                }
                else if (foodDay.sumCaloric > (int)user.caloricDemand)
                {
                    foodDay.description = "Spożyłeś za dużo kalorii";
                }
                else
                {
                    foodDay.description = "Spożyłeś odpowiednią ilość kalorii";
                }
            }
            
            db.Entry(foodDay).State = EntityState.Modified;
            db.SaveChanges();
            // }

            return RedirectToAction("Index");
        }
        public ActionResult Create()
        {
            ViewBag.Users_id = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.Category_id = new SelectList(db.Category, "id", "name");
            return View();
        }

        // POST: EveryoneProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Users_id,name,caloric,protein,carb,fat,weight,count,Category_id")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Product.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Users_id = new SelectList(db.AspNetUsers, "Id", "Email", product.Users_id);
            ViewBag.Category_id = new SelectList(db.Category, "id", "name", product.Category_id);
            return View(product);
        }

        // GET: EveryoneProducts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.Users_id = new SelectList(db.AspNetUsers, "Id", "Email", product.Users_id);
            ViewBag.Category_id = new SelectList(db.Category, "id", "name", product.Category_id);
            return View(product);
        }

        // POST: EveryoneProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Users_id,name,caloric,protein,carb,fat,weight,count,Category_id")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Users_id = new SelectList(db.AspNetUsers, "Id", "Email", product.Users_id);
            ViewBag.Category_id = new SelectList(db.Category, "id", "name", product.Category_id);
            return View(product);
        }

        // GET: EveryoneProducts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: EveryoneProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Product.Find(id);
            db.Product.Remove(product);
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
