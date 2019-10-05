using System;
using System.Collections.Generic;
using System.Text;

namespace SplitSchmeisser.BLL.Models
{
    public class ReportRequest
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

    }
}
