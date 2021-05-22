using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sneha_BL
{
    public interface ISchoolRepositary
    {
        List<School> GetSchools();
        void EditSchool(School school);
        void AddSchool(School school);
        void DeleteSchool(int SchoolID);
    }
}
