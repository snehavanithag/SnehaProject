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
        SubjectScore AddScore(SubjectScore Score);
        void UpdateScore(SubjectScore Score);
        void DeleteScore(int ScoreID);
    }
}
