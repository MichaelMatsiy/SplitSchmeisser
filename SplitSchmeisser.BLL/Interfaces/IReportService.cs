using SplitSchmeisser.BLL.Models;
using System.Threading.Tasks;

namespace SplitSchmeisser.BLL.Interfaces
{
    public interface IReportService
    {
        Task<byte[]> GenerateGroupReportAsync(ReportRequest request);
        Task<byte[]> GenerateGroupsReportAsync(ReportRequest request);
        Task<byte[]> GenerateOperationReportAsync(int id);
        Task<byte[]> GenerateOperationsReportAsync(ReportRequest request);
        Task<byte[]> GenerateDebtsReportAsync(int id);

    }
}
