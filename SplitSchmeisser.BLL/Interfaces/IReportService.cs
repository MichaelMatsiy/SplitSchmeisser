using SplitSchmeisser.BLL.Models;
using SplitSchmeisser.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SplitSchmeisser.BLL.Interfaces
{
    public interface IReportService
    {
        Task<byte[]> GenerateGroupReport(ReportRequest request);
        Task<byte[]> GenerateGroupsReport(ReportRequest request);
        Task<byte[]> GenerateOperationReport(int id);
        Task<byte[]> GenerateOperationsReport(ReportRequest request);
        Task<byte[]> GenerateDebtsReport(int id);

    }
}
