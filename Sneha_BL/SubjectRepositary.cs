using Sneha_DL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sneha_BL
{
    public class SubjectRepositary : ISubjectRepositary
    {

        private DBHelper dbHelper;

        public SubjectRepositary()
        {
            dbHelper = new DBHelper();
        }  

        public void AddSubjectsByGrade(List<Subject> subject)
        {
            var SubjectIDList = string.Join(",", subject.Select(x => x.SubjectID.ToString()));
            var GradeID = subject.Select(x => x.GradeID).FirstOrDefault();
            Dictionary<string, dynamic> subjectparam = new Dictionary<string, dynamic>();
            subjectparam.Add("@SubjectIDList", SubjectIDList);
            subjectparam.Add("@GradeID", GradeID);
            dbHelper.UpdateData("Prc_AddSubject", subjectparam);
        }

        public void RemoveSubjectsByGrade(List<Subject> subject)
        {
            var SubjectIDList = string.Join(",", subject.Select(x => x.SubjectID.ToString()));
            var GradeID = subject.Select(x => x.GradeID).FirstOrDefault();
            Dictionary<string, dynamic> subjectparam = new Dictionary<string, dynamic>();
            subjectparam.Add("@SubjectIDList", SubjectIDList);
            subjectparam.Add("@GradeID", GradeID);
            dbHelper.UpdateData("Prc_DeleteSubjectByGrade", subjectparam);
        }

        public List<Subject> GetSubjectByGrade()
        {
            DataSet ds = dbHelper.GetData("Prc_GetSubjectsByGrade");
            List<Subject> GradeSubjects = new List<Subject>();
            if (ds != null)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Subject subjects = new Subject();
                    subjects.SubjectID = dr["SubjectID"] != DBNull.Value ? Convert.ToInt32(dr["SubjectID"]) : (int?)null;
                    subjects.GradeID = dr["GradeID"] != DBNull.Value ? Convert.ToInt32(dr["GradeID"]) : (int?)null;
                    subjects.SubjectName = dr["SubjectName"].ToString();
                    subjects.GradeName = dr["GradeName"].ToString();
                    GradeSubjects.Add(subjects);
                }
            }
            return GradeSubjects;
        }

        public List<SubjectList> GetSubjectList()
        {
            DataSet ds = dbHelper.GetData("Prc_GetSubjectList");
            List<SubjectList> Subjects = new List<SubjectList>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                SubjectList subjectlist = new SubjectList();
                subjectlist.SubjectID = Convert.ToInt32(dr["SubjectID"]);
                subjectlist.SubjectName = dr["SubjectName"].ToString();
                Subjects.Add(subjectlist);
            }
            return Subjects;
        }
    }
}
