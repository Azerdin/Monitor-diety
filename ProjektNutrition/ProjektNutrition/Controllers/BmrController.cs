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

namespace ProjektNutrition.Controllers
{
    public class BmrController : Controller
    {
        // GET: Bmi
        private Entities db = new Entities();

        public ActionResult Index()
        {

            AspNetUsers user = db.AspNetUsers.Find(User.Identity.GetUserId());
            return View(user);
        }
        public ActionResult Create()
        {
            AspNetUsers user = db.AspNetUsers.Find(User.Identity.GetUserId());
            if (user != null)
            {
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(user);
            }
            return View();

        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,height,weight,activity,gender,birthDay")] AspNetUsers user)
        {
            AspNetUsers userLoggin = db.AspNetUsers.Find(User.Identity.GetUserId());
            if (userLoggin != null)
            {


                //if (ModelState.IsValid)
                //{
                    // user.Id = userLoggin.Id;
                    // AspNetUsers user = db.AspNetUsers.Find(User.Identity.GetUserId())
                    double resultKcal = 0.0;
                    int protein, carb, fat;
                    userLoggin.caloricDemand = null;
                    db.SaveChanges();
                    userLoggin.weight = (double)user.weight;
                    userLoggin.height = (double)user.height;
                    userLoggin.birthDay = user.birthDay;
                    userLoggin.activity = user.activity;

                    if(user.gender.Equals("K"))
                        userLoggin.gender = "Kobieta";
                    else if(user.gender.Equals("M"))
                        userLoggin.gender = "Mężczyzna";

                    db.SaveChanges();

                    DateTime dob = Convert.ToDateTime(userLoggin.birthDay);
                    int age = 0;
                    age = DateTime.Now.Year - dob.Year;
                    if (DateTime.Now.DayOfYear < dob.DayOfYear)
                        age -= 1;

                    if (userLoggin.gender.Equals("Kobieta"))
                    {
                        resultKcal = 9.99 * (double)userLoggin.weight + 6.25 * (double)userLoggin.height + 4.92 * age - 161;
                    }
                    else if(userLoggin.gender.Equals("Mężczyzna"))
                    {
                        resultKcal = 9.99 * (double)userLoggin.weight + 6.25 * (double)userLoggin.height + 4.92 * age - 161;
                    }

                    
                     double activ = Double.Parse(userLoggin.activity);
                userLoggin.caloricDemand = (int)(resultKcal * activ);
                protein = (int)(15 / 100 * resultKcal / 4);
                carb = (int)(55 / 100 * resultKcal / 4);
                fat = (int)(30 / 100 * resultKcal / 9);
                userLoggin.protein = protein;
                userLoggin.carb = carb;
                userLoggin.fat = fat;
                    db.SaveChanges();
                    return RedirectToAction("Create");
              //  }
            }


            return RedirectToAction("Create");
        }
    }
}