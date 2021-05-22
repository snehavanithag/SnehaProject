using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sneha_BL
{
    public class Grade
    {
        public int GradeID { get; set; }
        [Required(ErrorMessage ="*")]
        public string GradeName { get; set; }
        [Required(ErrorMessage ="*")]
        public int SchoolID { get; set; }
        [Required(ErrorMessage ="*")]
        public string GradeYear { get; set; }
        public string SchoolName { get; set; }
    }
}
