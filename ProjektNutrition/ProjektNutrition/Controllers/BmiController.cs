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
    public class BmiController : Controller
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
            if(user !=null)
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
        public ActionResult Create([Bind(Include = "id,height,weight")] AspNetUsers user)
        {
            AspNetUsers userLoggin = db.AspNetUsers.Find(User.Identity.GetUserId());
            if(userLoggin != null)
            {

            
            if (ModelState.IsValid)
            {
                // user.Id = userLoggin.Id;
                // AspNetUsers user = db.AspNetUsers.Find(User.Identity.GetUserId());
                userLoggin.bmi = null;
                db.SaveChanges();
                userLoggin.weight = (double)user.weight;
                userLoggin.height = (double)user.height;
                db.SaveChanges();
                userLoggin.bmi = (int)((double)user.weight / (((double)user.height/100) * ((double)user.height/100)));
                db.SaveChanges();
                return RedirectToAction("Create");
            }
            }
                ViewBag.Message = ((double)user.weight / ((double)user.height * (double)user.height)).ToString();


            return RedirectToAction("Create");
        }
    }
}