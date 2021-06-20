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
        [Required(ErrorMessage = "Subject Is Required")]
        public int SubjectID { get; set; }
        public  int ScoreID { get; set; }
        public string SubjectName { get; set; }
        [RegularExpression(@"^[1-9]\d*(\.\d+)?$", ErrorMessage = "Accepts Only Numaric")]
        [Required(ErrorMessage ="Score Is Required")]
        public decimal? Score { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage ="TestDate Is Required")]
        public DateTime TestDate { get; set; }
        public string TermName { get; set; }
    }
}
