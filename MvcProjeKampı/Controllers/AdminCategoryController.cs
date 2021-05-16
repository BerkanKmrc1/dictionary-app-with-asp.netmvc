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
    public class AdminCategoryController : Controller
    {
        CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
        public ActionResult Index()
        {
            var categoryValues = categoryManager.GetListBL();
            return View(categoryValues);
            
        }
        [HttpGet]
        public ActionResult AddCategory()
        {

            return View();
        }
        [HttpPost]
        public ActionResult AddCategory(Category category)
        {
            CategoryValidator categoryValidator = new CategoryValidator();
            ValidationResult validationResult = categoryValidator.Validate(category);
            if(validationResult.IsValid)
            {
                categoryManager.CategoryAddBL(category);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }
        public ActionResult DeleteCategory(int id)
        {
            var c = categoryManager.GetByIDBL(id);
            categoryManager.CategoryDeleteBL(c);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult UpdateCategory(int id)
        {
            var u = categoryManager.GetByIDBL(id);
            return View(u);//burada u ile döndürmemizin sebebi dönecek olan view de güncellenecek değerleri ekrana getirebilmek html helperları ile
        }
        [HttpPost]
        public ActionResult UpdateCategory(Category p)
        {
            categoryManager.CategoryUpdateBL(p);
            return RedirectToAction("Index");
        }
    }
}