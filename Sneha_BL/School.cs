using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Sneha_BL
{
    public partial class School
    {
        public int SchoolID { get; set; }
        [Required]
        public string SchoolName { get; set; }
        [Required]
        [Remote("IsAvailableDuration", "School", AdditionalFields ="SchoolID", ErrorMessage = "Duration Already Exists")]
        [RegularExpression(@"^[0-9]{4}-[0-9]{4}", ErrorMessage = "Range Should Be dddd-dddd")]
        public string Duration { get; set; }
    }
}
