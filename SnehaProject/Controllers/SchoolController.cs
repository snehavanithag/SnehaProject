using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sneha_BL;

namespace SnehaProject.Controllers
{
    public class SchoolController : Controller
    {
        // GET: School

        ISchoolRepositary schoolRepositary;
        public SchoolController(ISchoolRepositary _schoolRep)
        {
            schoolRepositary = _schoolRep;
        }

        public ActionResult Index()
        {
            List<School> mdl = schoolRepositary.GetSchools();
            return View(mdl);
        }

        public ActionResult Edit(int id)
        {
            var mdl = schoolRepositary.GetSchools().Find(x=>x.SchoolID == id);
            return View(mdl);
        }

        [HttpPost]
        public ActionResult Edit(School school)
        {
            if(ModelState.IsValid)
            {
                schoolRepositary.EditSchool(school);
                return RedirectToAction("Index");
            }
            return View(school);
        }

        public ActionResult Create()
        {
            School school = new School();
            school.SchoolID = 0;
            return View(school);
        }

        [HttpPost]
        public ActionResult Create(School school)
        {
            if(ModelState.IsValid)
            {
                schoolRepositary.AddSchool(school);
                return RedirectToAction("Index");
            }
            return View(school);
        }
        [HttpGet]
        public JsonResult IsAvailableDuration(string Duration, int SchoolID)
        {
            bool NotExistDuration = true;
            if (SchoolID == 0)
                   NotExistDuration = RemoteValidation(Duration);
            else
            {
                var School = schoolRepositary.GetSchools().Where(x => x.SchoolID == SchoolID).FirstOrDefault();
                if(School.Duration != Duration)
                     NotExistDuration = RemoteValidation(Duration);
            
            }
            return Json(NotExistDuration, JsonRequestBehavior.AllowGet);
        }

        private bool RemoteValidation(string Duration)
        {
            string[] DurationPeriod = Duration.Split('-');
            int DurationStart = Convert.ToInt32(DurationPeriod[0]);
            int DurationEnd = Convert.ToInt32(DurationPeriod[1]);
            bool NotExistDuration = true;
            foreach (var Period in schoolRepositary.GetSchools().Select(x => x.Duration).ToList())
            {
                string[] ExistPeriod = Period.Split('-');

                for (int existYear = Convert.ToInt32(ExistPeriod[0]); existYear <= Convert.ToInt32(ExistPeriod[1]); existYear++)
                {
                    if ((existYear >= DurationStart) && (existYear <= DurationEnd))
                    {
                        NotExistDuration = false;
                    }
                    if (!NotExistDuration)
                        break;
                }
            }
            return NotExistDuration;
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            schoolRepositary.DeleteSchool(id);
            return Json("Success", JsonRequestBehavior.AllowGet);
        }
    }
}