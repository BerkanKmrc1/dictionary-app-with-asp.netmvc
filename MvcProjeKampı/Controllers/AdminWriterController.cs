using BusinessLayer.Concrete;
using BusinessLayer.FluentValidation;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampı.Controllers
{
    public class AdminWriterController : Controller
    {
        // GET: Writer
        WriterManager writerManager = new WriterManager(new EfWriterDal());
        public ActionResult Index()
        {
            var writerValues = writerManager.GetListBL();
            return View(writerValues);
        }
        [HttpGet]
        public ActionResult AddWriter()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddWriter(Writer writer)
        {
            WriterValidator writerValidator = new WriterValidator();
            ValidationResult validationResult = writerValidator.Validate(writer);
            if(validationResult.IsValid)
            {
                writerManager.WriterAddBL(writer);
                return RedirectToAction("Index");
            }
            else
            {
                foreach(var item in validationResult.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }
        public ActionResult DeleteWriter(int id)
        {
            var d = writerManager.GetByIDBL(id);
            writerManager.WriterDeleteBL(d);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult UpdateWriter(int id)
        {
           var u= writerManager.GetByIDBL(id);
           return View(u);
        }
        [HttpPost]
        public ActionResult UpdateWriter(Writer p)
        {
            writerManager.WriterUpdateBL(p);
            return RedirectToAction("Index");

        }
    }
}