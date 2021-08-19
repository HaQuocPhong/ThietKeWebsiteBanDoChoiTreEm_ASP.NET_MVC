using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoChoiTreEmWeb.Models;

namespace DoChoiTreEmWeb.Models
{
    public class ReporInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> Sum { get; set; }
        public Nullable<int> Count { get; set; }
        public Nullable<decimal> Avg { get; set; }
        public Nullable<decimal> Max { get; set; }
        public Nullable<decimal> Min { get; set; }
    }
}