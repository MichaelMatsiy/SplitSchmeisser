using SplitSchmeisser.BLL.Models;
using SplitSchmeisser.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SplitSchmeisser.BLL.Interfaces
{
    public interface IReportService
    {
        Task<List<Operation>> GetReport(ReportRequest request);

        void TryGet();
    }
}
