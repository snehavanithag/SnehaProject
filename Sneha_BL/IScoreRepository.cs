using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sneha_BL
{
    public interface IScoreRepository
    {
        List<SubjectScore> GetScoresByGradeID();
        void AddScore(SubjectScore Score);
    }
}
