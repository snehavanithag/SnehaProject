using Sneha_BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SnehaProject.ViewModel
{
    public class SubjectViewModel
    {
        public List<Grade> GradeList { get; set;}
        public List<SubjectList> SubjectList { get; set; }
      
    }

    public class SubjectListByGrade
    {
        public string GradeNameKey { get; set; }
        public int SelectedGrade { get; set; }
        public int GradeID { get; set; }
        public List<Subject> Subjects { get; set; }

    }
}