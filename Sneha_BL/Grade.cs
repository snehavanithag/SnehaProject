using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Sneha_BL
{
    public class Grade
    {
        public int? GradeID { get; set; }
        [Required(ErrorMessage ="Grade Name Required")]
        public string GradeName { get; set; }
        [Required(ErrorMessage ="*")]
        public int SchoolID { get; set; }
        [Required(ErrorMessage ="Grade Year Required")]
        [Remote("IsAvailableDuration","Grade", AdditionalFields = "SchoolID", ErrorMessage="Grade Year Already Exist/Should Fall With In School Year/Start Year Should be Less Than End Year")]
        public string GradeYear { get; set; }
        public string SchoolName { get; set; }
        public string SchoolDuration { get; set; }
    }
}
