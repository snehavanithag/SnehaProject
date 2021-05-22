using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sneha_BL;

namespace SnehaProject.ViewModel
{
    public class ScoreViewModel
    {
        public SubjectScore AddScore { get; set; }
        public List<SubjectScore> ScoreList { get; set; }
        public List<Subject> SubjectList { get; set; }
    }
}