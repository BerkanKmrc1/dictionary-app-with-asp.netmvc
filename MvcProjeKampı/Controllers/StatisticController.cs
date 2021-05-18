using DataAccessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampı.Controllers
{
    public class StatisticController : Controller
    {
        Context context = new Context();
        // GET: Statistic
        //Ders 40 Görev 2--------5 adet Entity Framework Linq sorgusu
        public ActionResult Index()
        {
            var r1 = context.Categories.Count();
            ViewBag.dgr1 = r1;
            var r2 = context.Headings.Count(x => x.HeadingName == "Yazılım");
            ViewBag.dgr2 = r2;
            var r3 = context.Writers.Count(x => x.WriterName.Contains("a"));
            ViewBag.dgr3 = r3;
            var r4 = context.Headings.Max(x => x.Category.CategoryName);
            ViewBag.dgr4 = r4;
            var r5 = context.Categories.Count(x => x.CategoryStatus == true) - context.Categories.Count(x => x.CategoryStatus == false);
            ViewBag.dgr5 = r5;
            return View();
        }
    }
}