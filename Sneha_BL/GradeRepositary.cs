using Sneha_DL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sneha_BL
{
    public class GradeRepositary:IGradeRepositary
    {
        private DBHelper dbHelper;

        public GradeRepositary()
        {
            dbHelper = new DBHelper();
        }
        public List<Grade> GetGrades()
        {
            DataSet ds = dbHelper.GetData("Prc_GetGrades");
            List<Grade> GradeList = new List<Grade>();
            foreach(DataRow dr in ds.Tables[0].Rows)
            {
                Grade grade = new Grade();
                grade.GradeID = Convert.ToInt32(dr["GradeID"]);
                grade.GradeName = dr["GradeName"].ToString();
                grade.GradeYear = dr["GradeYear"].ToString();
                grade.SchoolName = dr["SchoolName"].ToString();
                grade.SchoolID = Convert.ToInt32(dr["SchoolID"]);
                GradeList.Add(grade);
            }
            return GradeList;
        }

        public void Add(Grade grade)
        {
            Dictionary<string, dynamic> gradeparams = new Dictionary<string, dynamic>();
            gradeparams.Add("@GradeName", grade.GradeName);
            gradeparams.Add("@GradeYear", grade.GradeYear);
            gradeparams.Add("@SchoolID", grade.SchoolID);
            dbHelper.UpdateData("Prc_AddGrade", gradeparams);
        }

        public void Edit(Grade grade)
        {
            Dictionary<string, dynamic> gradeparams = new Dictionary<string, dynamic>();
            gradeparams.Add("@GradeID", grade.GradeID);
            gradeparams.Add("@GradeName", grade.GradeName);
            gradeparams.Add("@GradeYear", grade.GradeYear);
            gradeparams.Add("@SchoolID", grade.SchoolID);
            dbHelper.UpdateData("Prc_EditGrade", gradeparams);
        }

       
    }
}
