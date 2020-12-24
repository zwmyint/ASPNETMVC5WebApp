using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPNETMVC5WebApp.Controllers
{
    public class DropDownListController : Controller
    {
        public enum MySkills
        {
            ASPNETMVC,
            ASPNETWEPAPI,
            CSHARP,
            DOCUSIGN,
            JQUERY
        }
        public struct ConvertEnum
        {
            public int Value { get; set; }
            public String Text { get; set; }
        }

        // GET: DropDownList
        public ActionResult Index()
        {

            #region ViewBag  
            List<SelectListItem> mySkills = new List<SelectListItem>()
            {
            new SelectListItem{ Text="ASP.NET MVC", Value = "1" },
            new SelectListItem{ Text="ASP.NET WEB API", Value = "2" },
            new SelectListItem{ Text="ENTITY FRAMEWORK", Value = "3" },
            new SelectListItem{ Text="DOCUSIGN", Value = "4" },
            new SelectListItem{ Text="ORCHARD CMS", Value = "5" },
            new SelectListItem{ Text="JQUERY", Value = "6" },
            new SelectListItem{ Text="ZENDESK", Value = "7" },
            new SelectListItem{ Text="LINQ", Value = "8" },
            new SelectListItem{ Text="C#", Value = "9" },
            new SelectListItem{ Text="GOOGLE ANALYTICS", Value = "10" },
            };
            ViewBag.MySkills = mySkills;
            #endregion
            #region ViewData  
            ViewData["MySkills"] = mySkills;
            #endregion
            #region TempData  
            TempData["MySkills"] = mySkills;
            #endregion

            #region Enum  
            var myskill = new List<ConvertEnum>();
            foreach (MySkills lang in Enum.GetValues(typeof(MySkills)))
                myskill.Add(new ConvertEnum { Value = (int)lang, Text = lang.ToString() });
            ViewBag.MySkillEnum = myskill;
            #endregion

            #region Database with EF  
            //using (CSharpEntities cshparpEntity = new CSharpEntities())
            //{
            //    var fromDatabaseEF = new SelectList(cshparpEntity.MySkills.ToList(), "ID", "Name");
            //    ViewData["DBMySkills"] = fromDatabaseEF;
            //}
            #endregion

            #region Model  
            //var model = new ASPNETMVC5WebApp.Models.MySkills();
            //using (CSharpEntities cshparpEntity = new CSharpEntities())
            //{
            //    var dbData = cshparpEntity.MySkills.ToList();
            //    model.Skills = GetSelectListItems(dbData);
            //}
            #endregion

            //
            return View();
        }

        //private IEnumerable<SelectListItem> GetSelectListItems(IEnumerable<MySkill> elements)
        //{
        //    var selectList = new List<SelectListItem>();
        //    foreach (var element in elements)
        //    {
        //        selectList.Add(new SelectListItem
        //        {
        //            Value = element.ID.ToString(),
        //            Text = element.Name
        //        });
        //    }
        //    return selectList;
        //}

        //public JsonResult ReturnJSONDataToAJax() //It will be fired from Jquery ajax call  
        //{
        //    CSharpEntities cshparpEntity = new CSharpEntities();
        //    var jsonData = cshparpEntity.MySkills.ToList();
        //    return Json(jsonData, JsonRequestBehavior.AllowGet);
        //}

    }
}