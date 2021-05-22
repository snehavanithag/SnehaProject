using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sneha_BL
{
    public interface IGradeRepositary
    {
        List<Grade> GetGrades();
        void Add(Grade grade);
        void Edit(Grade grade);
    }
}
