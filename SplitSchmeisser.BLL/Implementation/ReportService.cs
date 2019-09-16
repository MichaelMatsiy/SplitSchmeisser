using SplitSchmeisser.BLL.Interfaces;
using SplitSchmeisser.BLL.Models;
using SplitSchmeisser.DAL.Entities;
using SplitSchmeisser.DAL.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace SplitSchmeisser.BLL.Implementation
{
    public class ReportService : IReportService
    {
        private IGenericRepository<Operation> operationRepository;

        public ReportService(IGenericRepository<Operation> operationRepository)
        {
            this.operationRepository = operationRepository;
        }


        public async Task<List<Operation>> GetReport(ReportRequest request)
        {
            var report = await this.operationRepository.GetAll()
                .Where(x => x.GroupId == request.GroupId
                    && x.DateOfLoan > request.StartDate
                    && x.DateOfLoan < request.EndDate)
                .ToListAsync();

            return report;
        }

    }
}
