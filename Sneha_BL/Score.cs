using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sneha_BL
{
    public class SubjectScore
    {
        public int GradeID { get; set; }
        public int SubjectID { get; set; }
        public  int ScoreID { get; set; }
        public string SubjectName { get; set; }
        public decimal? Score { get; set; }
        [DataType(DataType.Date)]
        public DateTime TestDate { get; set; }
        public string TermName { get; set; }
    }
}
