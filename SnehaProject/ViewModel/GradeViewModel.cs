using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sneha_BL;

namespace SnehaProject.ViewModel
{
    public class GradeViewModel
    {
        public int? SchoolID { get; set; }
        public List<Grade> GradeList { get; set; }
        public int? SelectedSchoolID { get; set; }
    }
}