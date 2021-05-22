using Sneha_BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SnehaProject.ViewModel
{
    public class ChartViewModel
    {
        public XAxisData xAxis { get; set; }
        public List<ChartData> yAxis { get; set; }
    }

    public class ChartList
    {
        public string SubjectName { get; set; }
        public List<Chart> ChartValue { get; set; }
    }

    public class ChartData
    {
        public string name { get; set; }
        public decimal[] data { get; set; }
    }

    public class XAxisData
    {
        public string[] categories { get; set; }
    }
}