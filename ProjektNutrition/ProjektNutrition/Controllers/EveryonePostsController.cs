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
    public class EveryonePostsController : Controller
    {
        private Entities db = new Entities();

        // GET: EveryonePosts
        public ActionResult Index()
        {
            var post = db.Post.Include(p => p.AspNetUsers);
            return View(post.ToList());
        }
    }
}
