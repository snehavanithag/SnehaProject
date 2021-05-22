using Sneha_BL;
using SnehaProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SnehaProject.Controllers
{
    public class SubjectController : Controller
    {
        private ISubjectRepositary subjectRepositary;
        private IGradeRepositary gradeRepositary;

        public SubjectController(ISubjectRepositary _subjectRepositary, IGradeRepositary _gradeRepositary)
        {
            subjectRepositary = _subjectRepositary;
            gradeRepositary = _gradeRepositary;
        }

        public ActionResult Index()
        {
            List<SubjectList> list = subjectRepositary.GetSubjectList();
            List<Grade> gradelist = gradeRepositary.GetGrades().Where(x=>x.SchoolID != 3).ToList();
           // List<SubjectListByGrade> gradesubjects = subjectRepositary.GetSubjectByGrade().GroupBy(x=>x.GradeName).
            //                                            Select(g => new SubjectListByGrade { GradeNameKey = g.Key, Subjects = g.ToList() }).ToList();
            SubjectViewModel model = new SubjectViewModel();
            model.GradeList = gradelist;
            model.SubjectList = list;
           // model.SubjectsByGrade = gradesubjects;
            return View(model);
        }

        public JsonResult GetSubjects(int id)
        {
            List<Subject> gradesubjects = subjectRepositary.GetSubjectByGrade().Where(x=>x.GradeID == id).ToList();
            return Json(gradesubjects, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddSubjects(List<Subject> SubjectViewModel)
        {
            subjectRepositary.AddSubjectsByGrade(SubjectViewModel);
           
            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RemoveSubjects(List<Subject> SubjectViewModel)
        {
            subjectRepositary.RemoveSubjectsByGrade(SubjectViewModel);
            return Json("Success", JsonRequestBehavior.AllowGet);
        }

        
        public ActionResult GetSubjectByGrade(int GradeID)
        {
           List<SubjectListByGrade> data = subjectRepositary.GetSubjectByGrade().GroupBy(x => x.GradeName).
                                                        Select(g => new SubjectListByGrade { GradeNameKey = g.Key, Subjects = g.ToList(), SelectedGrade = GradeID }).ToList();
            return PartialView("_SubjectsByGrade", data);
         }
    }
}