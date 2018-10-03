using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLWebApp.Models
{
    public class FinalTestReport
    {
        public SelectListItem Site = new SelectListItem { Value = "8", Text = "Moko" };

        int _siteid = 2;
        public int Siteid { get => _siteid; set => _siteid = value; }

        int _numberOfDays = 10;
        public int NumberOfDays { get => _numberOfDays; set => _numberOfDays = value; }

        int _hrOffset = 0;
        public int HrOffset { get => _hrOffset; set => _hrOffset = value; }

        public FinalTestCount[] FinalTestCounts { get; set; }

        public FinalTestReport(int siteId=2, int numberOfDays=10, int hroffset=0)
        {
            Siteid = siteId;
            NumberOfDays = numberOfDays;
            HrOffset = hroffset;
        }
    }

    public class FinalTestCount
    {
        public FinalTestCountHeader Header { get; set; }
        public FinalTestCountItem[] Items { get; set; }
    }

    public class FinalTestCountHeader
    {
        public int TotalCount { get; set; }
        public string Sku { get; set; }
        public string ProductName { get; set; }
    }

    public class FinalTestCountItem
    {
        public int TotalCount { get; set; }
        public DateTime CreatedDate { get; set; }
    }

}
