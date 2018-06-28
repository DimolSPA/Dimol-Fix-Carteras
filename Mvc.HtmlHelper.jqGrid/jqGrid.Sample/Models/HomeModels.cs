using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jqGrid.Sample.Models
{
    public class HomeModel
    {
        public string GridData { get; set; }
        public string GridSelect { get; set; }
    }

    public class Organization
    {
        public string Id { get; set; }
        public string Parent { get; set; }
        public string Name { get; set; }
        public int AssetAllocation { get; set; }
        public int ClientReview { get; set; }
        public int IneligibleActivity { get; set; }
        public int InvestmentGuideline { get; set; }
        public decimal BillableAssets { get; set; }
        public decimal NonEarnedPcs { get; set; }
    }

}