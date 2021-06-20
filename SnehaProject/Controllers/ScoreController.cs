using Sneha_BL;
using SnehaProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SnehaProject.Controllers
{
    public class ScoreController : Controller
    {
        private IScoreRepository scoreRepository;
        private ISubjectRepositary subjectRepositary;
        private IGradeRepositary gradeRepositary;
        public ScoreController(IScoreRepository _scoreRepository,ISubjectRepositary _subjectRepositary,IGradeRepositary _gradeRepositary)
        {
            scoreRepository = _scoreRepository;
            subjectRepositary = _subjectRepositary;
            gradeRepositary = _gradeRepositary;
        }
        public ActionResult Index(int? id)
        {
            ViewBag.GradeList = gradeRepositary.GetGrades().Select(x => new { GradeValue =(x.GradeID +"," +x.GradeYear) , GradeText = x.GradeName }).ToList();
            ViewData["GradeName"] = "Select Grade";
            if (id.HasValue)
            {
                ViewBag.GradeID = id.Value;
                var GradeName = gradeRepositary.GetGrades().Where(x => x.GradeID == id.Value).FirstOrDefault().GradeName;
                ViewData["GradeName"] = GradeName;
                ViewBag.GradeYear = gradeRepositary.GetGrades().Where(x => x.GradeID == id.Value).FirstOrDefault().GradeYear;

            }
                return View();
         
        }

        public ActionResult GetScores(int GradeID,string GradeYear)
        {
            ScoreViewModel model = new ScoreViewModel();
            model.ScoreList = scoreRepository.GetScoresByGradeID().Where(x => x.GradeID == GradeID).ToList();
            model.AddScore = new SubjectScore();
            
            model.SubjectList= subjectRepositary.GetSubjectByGrade().Where(x => x.GradeID == GradeID).ToList();
            ViewBag.SchoolStartMonth = ConfigurationManager.AppSettings["SchoolStartMonth"];
            ViewBag.SchoolEndMonth = ConfigurationManager.AppSettings["SchoolEndMonth"];
            ViewBag.GradeYear = GradeYear;
            ViewBag.GradeID = GradeID;
            var StartDate = Convert.ToDateTime(ViewBag.SchoolStartMonth + "/01/" + GradeYear.Split('-')[0]);
            model.AddScore.TestDate = Convert.ToDateTime(ViewBag.SchoolStartMonth + "/01/" + GradeYear.Split('-')[0]);
            return PartialView("_ScoresByGrade", model);
        }

        [HttpPost]
        //public ActionResult Create(ScoreViewModel Score)
        public ActionResult Create(SubjectScore SubjectScore)
        {
            if (ModelState.IsValid)
            {
                SubjectScore Score = scoreRepository.AddScore(SubjectScore);
                // return RedirectToAction("Index", new { id = Score.AddScore.GradeID });
                return Json(new { errors = false , Score = Score }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    errors = true,
                    errorList = GetModelStateErrors(ModelState)
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public IEnumerable<ValidationError> GetModelStateErrors(ModelStateDictionary modelState)
        {
             var errors = (from m in modelState
                          where m.Value.Errors.Count() > 0
                          select
                             new ValidationError
                             {
                                 PropertyName = m.Key,
                                 ErrorList = (from msg in m.Value.Errors
                                              select msg.ErrorMessage).ToArray()
                             }).AsEnumerable();
            return errors;
        }

        public ActionResult UpdateScore(UpdateScoreViewModel UpdateScoreViewModel)
        {
            return PartialView("_UpdateScoreView", UpdateScoreViewModel);
        }

        [HttpPost]
        public ActionResult Update(UpdateScoreViewModel UpdateScoreViewModel)
        {
            if (ModelState.IsValid)
            {
                scoreRepository.UpdateScore(UpdateScoreViewModel.SubjectScore);
                List<SubjectScore> ScoreData = scoreRepository.GetScoresByGradeID().Where(x => x.GradeID == UpdateScoreViewModel.SubjectScore.GradeID).ToList();
                //  return RedirectToAction("Index", new { id = UpdateScoreViewModel.SubjectScore.GradeID });
                return Json(new { errors = false, ListData = ScoreData }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    errors = true,
                    errorList = GetModelStateErrors(ModelState)
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult DeleteScore(int ScoreID)
        {
            scoreRepository.DeleteScore(ScoreID);
            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}