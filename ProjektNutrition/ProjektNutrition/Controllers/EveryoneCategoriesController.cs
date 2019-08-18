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
    public class EveryoneCategoriesController : Controller
    {
        private Entities db = new Entities();

        // GET: EveryoneCategories
        public ActionResult Index()
        {
            return View(db.Category.ToList());
        }
    }
}
