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

        public SubjectScore AddScore(SubjectScore Score)
        {
            SubjectScore SubjectScore = new SubjectScore();
            Dictionary<string, dynamic> ScoreParam = new Dictionary<string, dynamic>();
            ScoreParam.Add("@SubjectID", Score.SubjectID);
            ScoreParam.Add("@GradeID", Score.GradeID);
            ScoreParam.Add("@Score", Score.Score);
            ScoreParam.Add("@TestDate", Score.TestDate);
            ScoreParam.Add("@ScoreID", "Output");
            ScoreParam.Add("@TermName", "Output");
            Dictionary<string, dynamic> output =  dbHelper.AddData("Prc_AddScore", ScoreParam);
            foreach(KeyValuePair<string, dynamic> keyValuePair in output)
            {
                switch(keyValuePair.Key)
                {
                    case "@ScoreID":
                        SubjectScore.ScoreID = Convert.ToInt32(keyValuePair.Value);
                        break;
                    case "@TermName":
                        SubjectScore.TermName = Convert.ToString(keyValuePair.Value);
                        break;
                }
            }
            return SubjectScore;
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
                    Score.ScoreID = Convert.ToInt32(dr["ScoreID"]);
                    Score.SubjectName = dr["SubjectName"].ToString();
                    Score.TestDate = Convert.ToDateTime(dr["TestDate"]);
                    Score.TermName = dr["TermName"].ToString();
                    Score.Score = Convert.ToDecimal(dr["Score"]);
                    list.Add(Score);
                }
            }
            return list;
        }
        public void UpdateScore(SubjectScore Score)
        {
            Dictionary<string, dynamic> scoreparam = new Dictionary<string, dynamic>();
            scoreparam.Add("@ScoreID", Score.ScoreID);
            scoreparam.Add("@TestDate", Score.TestDate);
            scoreparam.Add("@Score", Score.Score);
            dbHelper.UpdateData("Prc_EditScore", scoreparam);

        }

        public void DeleteScore(int ScoreID)
        {
            Dictionary<string, dynamic> scoreparam = new Dictionary<string, dynamic>();
            scoreparam.Add("@ScoreID", ScoreID);
            dbHelper.UpdateData("Prc_DeleteScore", scoreparam);
        }
    }
}
