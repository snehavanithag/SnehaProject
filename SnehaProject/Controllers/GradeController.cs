using Sneha_BL;
using SnehaProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SnehaProject.Controllers
{
    public class GradeController : Controller
    {
        IGradeRepositary gradeRepositary;
        ISchoolRepositary schoolRepositary;
        public GradeController(IGradeRepositary _graderepositary, ISchoolRepositary _schoolRepositary)
        {
            gradeRepositary = _graderepositary;
            schoolRepositary = _schoolRepositary;
        }
        public ActionResult Index(int? id)
        {
            var mdl = gradeRepositary.GetGradesBySchool().GroupBy(x => x.SchoolID).Select(group => new GradeViewModel { SchoolID = group.Key, GradeList = group.ToList(), SelectedSchoolID= id.HasValue ? id.Value : 1 }).ToList();
            return View(mdl);
        }
        public ActionResult Create(int id)
        {
            Grade grade = new Grade();
            grade.SchoolID = id;
            ViewBag.Schools = schoolRepositary.GetSchools();
            return View(grade);
        }
        [HttpPost]
        public ActionResult Create(Grade grade)
        {
            if(ModelState.IsValid)
            {
                gradeRepositary.Add(grade);
                return RedirectToAction("Index", new { id = grade.SchoolID });
            }
            ViewBag.Schools = schoolRepositary.GetSchools();
            return View(grade);
        }

        public ActionResult Edit(int id)
        {
            var mdl = gradeRepositary.GetGradesBySchool().FirstOrDefault(x => x.GradeID == id);
            ViewBag.Schools = schoolRepositary.GetSchools();
            return View(mdl);
        }

        [HttpPost]
        public ActionResult Edit(Grade grade)
        {
            if(ModelState.IsValid)
            {
                gradeRepositary.Edit(grade);
                return RedirectToAction("Index", new { id = grade.SchoolID });
            }
            ViewBag.Schools = schoolRepositary.GetSchools();
            return View(grade);
        }
        public JsonResult IsAvailableDuration(string GradeYear,int SchoolID)
        {
            bool NotExistDuration = true;
            string[] data = GradeYear.Split('-');
            int GradeStartYear = Convert.ToInt32(data[0]);
            int GradeEndYear = Convert.ToInt32(data[1]);
            var GradeList = gradeRepositary.GetGradesBySchool().Where(x=>x.SchoolID==SchoolID).ToList();

            foreach(var grade in GradeList)
            {
                string[] gradedata = grade.GradeYear.Split('-');
                int StartYear = Convert.ToInt32(gradedata[0]);
                int EndYear = Convert.ToInt32(gradedata[1]);
                if ((GradeStartYear == StartYear) || (GradeStartYear == EndYear) || (GradeEndYear == StartYear) || (GradeEndYear == EndYear))
                    NotExistDuration = false;

                if (!NotExistDuration)
                        break;
               
            }

            if(NotExistDuration)
            {
                var SchoolDuration = gradeRepositary.GetGradesBySchool().Where(x => x.SchoolID == SchoolID).FirstOrDefault().SchoolDuration;
                string[] gradedata = SchoolDuration.Split('-');
                int StartYear = Convert.ToInt32(gradedata[0]);
                int EndYear = Convert.ToInt32(gradedata[1]);

                if (!((GradeStartYear >= StartYear &&  GradeStartYear <= EndYear) && (GradeEndYear >= StartYear && GradeStartYear <= EndYear)))
                    NotExistDuration = false;
                 
            }

            if (NotExistDuration)
            {
                if (GradeStartYear > GradeEndYear)
                    NotExistDuration = false;
            }

            return Json(NotExistDuration, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            gradeRepositary.DeleteGrade(id);
            return Json("Success", JsonRequestBehavior.AllowGet);
        }
    }
}