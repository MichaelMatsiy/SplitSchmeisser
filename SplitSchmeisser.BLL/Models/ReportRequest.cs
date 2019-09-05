using System;
using System.Collections.Generic;
using System.Text;

namespace SplitSchmeisser.BLL.Models
{
    public class ReportRequest
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int GroupId { get; set; }
    }
}
