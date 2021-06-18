using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace clothesWebSite.Controllers
{
    public class ClothesController : Controller
    {
        // GET: Clothes
        public ActionResult Index()
        {
            return View();
        }
    }
}