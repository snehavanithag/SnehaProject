using Sneha_BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SnehaProject.ViewModel
{
    public class UpdateScoreViewModel
    {
        public SubjectScore SubjectScore { get; set; }
        public string GradeStartDate{ get; set; }
        public string GradeEndDate { get; set; }
    }
}