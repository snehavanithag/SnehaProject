using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sneha_BL
{
    public interface ISubjectRepositary
    {
        List<SubjectList> GetSubjectList();
        void AddSubjectsByGrade(List<Subject> subject);
        void RemoveSubjectsByGrade(List<Subject> subject);
        List<Subject> GetSubjectByGrade();
    }
}
