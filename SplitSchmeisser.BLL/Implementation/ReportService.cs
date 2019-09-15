using SplitSchmeisser.BLL.Interfaces;
using SplitSchmeisser.BLL.Models;
using SplitSchmeisser.BLL.ReportWorker;
using SplitSchmeisser.DAL.Entities;
using SplitSchmeisser.DAL.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SplitSchmeisser.BLL.Implementation
{
    public class ReportService : IReportService
    {
        private IGenericRepository<Operation> operationRepository;
        IReportGenerateResolver generateResolver;

        public ReportService(IGenericRepository<Operation> operationRepository, IReportGenerateResolver generateResolver)
        {
            this.operationRepository = operationRepository;
            this.generateResolver = generateResolver;
        }

        private readonly object _syncRoot = new object();
        int i = 0;

        public async Task<List<Operation>> GetReport(ReportRequest request)
        {
            //var resolver = new ReportGenerateResolver();

            var report = await this.operationRepository.GetAll(true)
                .Where(x => x.GroupId == request.GroupId
                    && x.DateOfLoan > request.StartDate
                    && x.DateOfLoan < request.EndDate)
                .ToListAsync();


            return report;// await generateResolver.Enqueue(() => { return report; });
        }

        public async void TryGet()
        {
            var resolver = new ReportGenerateResolver();

            var test1Task = await resolver.Enqueue(() => { return Test(); });
            var test2Task = await resolver.Enqueue(() => { return Test(); });
            var test3Task = await resolver.Enqueue(() => { return Test(); });
            var test4Task = await resolver.Enqueue(() => { return Test(); });

            //var list = new List<Task>
            //{
            //    test1Task, test2Task, test3Task, test4Task
            //};

            //await Task.WhenAll(list);

            //var test1 = await test1Task;
            //var test2 = await test2Task;
            //var test3 = await test3Task;
            //var test4 = await test4Task;

            var i = 0;
        }

        private Task<int> Test()
        {
            Thread.Sleep(1200);

            return Task.Factory.StartNew(() => { return i++; });
        }
    }
}
