using SplitSchmeisser.BLL.Models;
using SplitSchmeisser.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SplitSchmeisser.BLL.Interfaces
{
    public interface IReportService
    {
        Task<List<Operation>> GetReport(ReportRequest request);
    }
}
