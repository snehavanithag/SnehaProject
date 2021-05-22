using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sneha_DL;

namespace Sneha_BL
{
    public class ScoreRepository : IScoreRepository
    {
        private DBHelper dbHelper;
        public ScoreRepository()
        {
            dbHelper = new DBHelper();
        }

        public void AddScore(SubjectScore Score)
        {
            Dictionary<string, dynamic> ScoreParam = new Dictionary<string, dynamic>();
            ScoreParam.Add("@SubjectID", Score.SubjectID);
            ScoreParam.Add("@GradeID", Score.GradeID);
            ScoreParam.Add("@Score", Score.Score);
            ScoreParam.Add("@TestDate", Score.TestDate);
            dbHelper.UpdateData("Prc_AddScore", ScoreParam);
        }

        public List<SubjectScore> GetScoresByGradeID()
        {
            DataSet ds = dbHelper.GetData("Prc_GetScores");
            List<SubjectScore> list = new List<SubjectScore>();
            if(ds != null)
            {
                foreach(DataRow dr in ds.Tables[0].Rows)
                {
                    SubjectScore Score = new SubjectScore();
                    Score.GradeID = Convert.ToInt32(dr["GradeID"]);
                    Score.SubjectID = Convert.ToInt32(dr["SubjectID"]);
                    Score.SubjectName = dr["SubjectName"].ToString();
                    Score.TestDate = Convert.ToDateTime(dr["TestDate"]);
                    Score.TermName = dr["TermName"].ToString();
                    Score.Score = Convert.ToDecimal(dr["Score"]);
                    list.Add(Score);
                }
            }
            return list;
        }
    }
}
