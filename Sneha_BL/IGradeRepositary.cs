using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sneha_BL
{
    public interface IGradeRepositary
    {
        List<Grade> GetGradesBySchool();
        void Add(Grade grade);
        void Edit(Grade grade);
        List<Grade> GetGrades();
        void DeleteGrade(int GradeID);
    }
}
