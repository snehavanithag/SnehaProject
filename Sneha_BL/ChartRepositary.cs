using Sneha_DL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sneha_BL
{
    public class ChartRepositary : IChartRepositary
    {

        private DBHelper dbHelper;
        public ChartRepositary()
        {
            dbHelper = new DBHelper();
        }

        public List<Chart> GetChartData(int GradeID,string ChartType)
        {
            DataSet ds = new DataSet();
            string SPName = string.Empty;
            List<Chart> charts = new List<Chart>();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();
            param.Add("@GradeID", GradeID);
            switch(ChartType)
            {
                case "LineChart":
                    SPName = "Prc_Chart";
                    break;
                case "BarChart":
                    SPName = "Prc_GetBarChart";
                    break;
                case "OverallTermPercentageBarChart":
                    break;
            }
            ds= dbHelper.GetData(SPName, param);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Chart chart = new Chart();
                chart.SubjectName = dr["SubjectName"].ToString();
                chart.Score = Convert.ToDecimal(dr["Score"]);
                chart.TermDate = SPName == "Prc_Chart" ?  dr["TermDate"].ToString() : dr["TermName"].ToString();
                charts.Add(chart);
            }
            return charts;
        }

        public List<decimal> GetOverallTermChartData(int GradeID)
        {
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();
            param.Add("@GradeID", GradeID);
            DataSet ds = dbHelper.GetData("Prc_GetOverAllTermChart", param);
            List<decimal> TermsScore = new List<decimal>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                TermsScore.Add(Convert.ToDecimal(dr["TermScore"]));
            }
            return TermsScore;
        }

        public List<string> GetChartList()
        {
            DataSet ds = dbHelper.GetData("Prc_GetTermList");
            List<string> TermList = new List<string>();
            foreach(DataRow dr in ds.Tables[0].Rows)
            {
                TermList.Add(dr["TermName"].ToString());
            }
            return TermList;
        }
    }
}
