using Sneha_BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SnehaProject.ViewModel;

namespace SnehaProject.Controllers
{
    public class ChartController : Controller
    {

        private IChartRepositary chartRepositary;
        private IGradeRepositary gradeRepositary;
        private ISubjectRepositary subjectRepositary;

        public ChartController(IChartRepositary _chartRepositary, IGradeRepositary _gradeRepositary, ISubjectRepositary _subjectRepositary)
        {
            chartRepositary =  _chartRepositary;
            gradeRepositary = _gradeRepositary;
            subjectRepositary = _subjectRepositary;
        }
        // GET: Chart

        public ActionResult Index()
        {
            ViewBag.GradeList = gradeRepositary.GetGrades().Select(x => new { GradeValue = (x.GradeID + "," + x.GradeYear), GradeText = x.GradeName }).ToList();
            ViewBag.ChartList = new List<string>()
            {
                "Line Chart By Subject" ,
                "Bar Chart By Term",
                "Overall Percentage By Term(Bar Chart)"
            };

            return View();
        }
        [HttpPost]
        public ActionResult GetLineChart(int id,string year)
        {
            ChartViewModel chartViewModel = new ChartViewModel();
            var ChartResult = chartRepositary.GetChartData(id,"LineChart");
            List<ChartList> ChartList =ChartResult.GroupBy(x => x.SubjectName).Select(g => new ChartList { SubjectName = g.Key,  ChartValue= g.ToList() }).ToList();

            chartViewModel.xAxis = new XAxisData();
            chartViewModel.xAxis.categories= AppendYear(year);
            chartViewModel.yAxis = new List<ChartData>();
            foreach (var item in ChartList)
            {
                ChartData ChartData = new ChartData();
                ChartData.name = item.SubjectName;
                ChartData.data = new decimal[11];
                //foreach (var data in item.ChartValue)
                //{
                    int i = 0;
                    foreach (var month in chartViewModel.xAxis.categories)
                    {
                        var ScoreMonth = item.ChartValue.Where(x => x.TermDate.StartsWith(month)).FirstOrDefault();
                        if (ScoreMonth != null)
                        {
                            ChartData.data[i] = ScoreMonth.Score;
                            i++;
                        }
                    }
                // }
                chartViewModel.yAxis.Add(ChartData);
            }

            return Json(chartViewModel,JsonRequestBehavior.AllowGet);

        }

        private string[] AppendYear(string year)
        {
           string[] MonthArray = new string[] { "August", "September", "October", "November", "December", "January", "February", "March", "April", "May", "June" };
           string[] YearArray = year.Split('-');
           string[] MonthYearArray = new string[11];
            int i = 0;
           foreach(var month in MonthArray)
           {
                switch(month)
                {
                    case "August":
                    case "September":
                    case "October":
                    case "November":
                    case "December":
                        {
                            MonthYearArray[i] = month + "-" + YearArray[0];
                            break;
                        }
                    case "January":
                    case "February":
                    case "March":
                    case "April":
                    case "May":
                    case "June":
                        {
                            MonthYearArray[i] = month + "-" + YearArray[1];
                            break;
                        }


                }
                i = i + 1;

           }

            return MonthYearArray;
        }

        public ActionResult GetBarChart(int id)
        {
            ChartViewModel chartViewModel = new ChartViewModel();
            var ChartResult = chartRepositary.GetChartData(id, "BarChart");
            chartViewModel.xAxis = new XAxisData();

            var SubjectList = ChartResult.OrderBy(x => x.SubjectName).GroupBy(x => x.SubjectName).Select(g => new { SubjectName = g.Key }).ToList();
            string[] Categories = new string[subjectRepositary.GetSubjectByGrade().Where(x=>x.GradeID == id).ToList().Count];
            int i = 0;
            foreach (var str in SubjectList)
            {
                Categories[i] =  str.SubjectName;
                i = i + 1;

            }
            chartViewModel.xAxis.categories =Categories ;



            chartViewModel.yAxis = new List<ChartData>();

            chartViewModel.yAxis = ChartResult.OrderBy(x=>x.SubjectName).GroupBy(x => x.TermDate).Select(g => new ChartData { name = g.Key, data = g.Select(x => x.Score).ToArray() }).ToList();

            return Json(chartViewModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetOverallTermChart(int id)
        {
            ChartViewModel chartViewModel = new ChartViewModel();
            var ChartResult = chartRepositary.GetOverallTermChartData(id);
            chartViewModel.xAxis = new XAxisData();
            var TermList = chartRepositary.GetChartList();
          
            chartViewModel.xAxis.categories = TermList.ToArray();

            ChartData ChartData = new ChartData();
            ChartData.name = gradeRepositary.GetGrades().Where(x => x.GradeID == id).FirstOrDefault().GradeName;
            ChartData.data = ChartResult.ToArray();

            List<ChartData> listData = new List<ChartData>();
            listData.Add(ChartData);
            chartViewModel.yAxis = listData;

            return Json(chartViewModel, JsonRequestBehavior.AllowGet);
        }

    }
}