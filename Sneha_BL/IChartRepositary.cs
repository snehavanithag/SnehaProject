using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sneha_BL
{
    public interface IChartRepositary
    {
        List<Chart> GetChartData(int GradeID, string ChartType);
        List<string> GetChartList();
        List<decimal> GetOverallTermChartData(int GradeID);
    }
}
