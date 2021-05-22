using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sneha_DL;

namespace Sneha_BL
{
    public class SchoolRepositary : ISchoolRepositary
    {
        DBHelper dbhelper;
        public SchoolRepositary()
        {
            dbhelper = new DBHelper();
        }
        public List<School> GetSchools()
        {
            List<School> schoollist = new List<School>();
            DataSet ds = dbhelper.GetData("dbo.Prc_GetSchools");
            foreach(DataRow dr in ds.Tables[0].Rows)
            {
                School school = new School();
                school.SchoolID = Convert.ToInt32(dr["SchoolID"].ToString());
                school.SchoolName = dr["SchoolName"].ToString();
                school.Duration = dr["Duration"].ToString();
                schoollist.Add(school);
            }
            return schoollist;
        }
        public void EditSchool(School school)
        {
            Dictionary<string, dynamic> schoolparam = new Dictionary<string, dynamic>();
            schoolparam.Add("@SchoolID", school.SchoolID);
            schoolparam.Add("@SchoolName", school.SchoolName);
            schoolparam.Add("@Duration", school.Duration);
            dbhelper.UpdateData("Prc_EditSchool", schoolparam);

        }
        public void AddSchool(School school)
        {
            Dictionary<string, dynamic> schoolparam = new Dictionary<string, dynamic>();
            schoolparam.Add("@SchoolName", school.SchoolName);
            schoolparam.Add("@Duration", school.Duration);
            dbhelper.UpdateData("Prc_AddSchool", schoolparam);

        }

        public void DeleteSchool(int SchoolID)
        {
            Dictionary<string, dynamic> schoolparam = new Dictionary<string, dynamic>();
            schoolparam.Add("@SchoolID", SchoolID);
            dbhelper.UpdateData("Prc_DeleteSchool", schoolparam);
        }

        
    }
}
