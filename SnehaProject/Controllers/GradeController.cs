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
            var mdl = gradeRepositary.GetGrades().GroupBy(x => x.SchoolID).Select(group => new GradeViewModel { SchoolID = group.Key, GradeList = group.ToList(), SelectedSchoolID= id.HasValue ? id.Value : 1 }).ToList();
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
            var mdl = gradeRepositary.GetGrades().FirstOrDefault(x => x.GradeID == id);
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
    }
}