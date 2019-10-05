using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SplitSchmeisser.BLL.Interfaces;
using SplitSchmeisser.BLL.Models;

namespace SplitSchmeisser.Web.Controllers
{
    public class ReportController : Controller
    {
        private readonly IReportService reportService;

        public ReportController(IReportService reportService)
        {
            this.reportService = reportService;
        }

        public IActionResult Group(int id)
        {
            ReportRequest request = new ReportRequest { Id = id };
            return View("Report", request);
        }

        [HttpPost]
        public async Task<FileResult> Group(ReportRequest reportRequest)
        {
            byte[] bytes = await this.reportService.GenerateGroupReport(reportRequest);
            return File(bytes, MediaTypeNames.Text.Xml, "Group - Reprot.xml");
        }

        public IActionResult Groups()
        {
            return View("Report");
        }

        [HttpPost]
        public async Task<FileResult> Groups(ReportRequest reportRequest)
        {
            byte[] bytes = await this.reportService.GenerateGroupsReport(reportRequest);
            return File(bytes, MediaTypeNames.Text.Xml, "Groups - Reprot.xml");
        }

        public async Task<FileResult> Operation(int operationId)
        {
            byte[] bytes = await this.reportService.GenerateOperationReport(operationId);
            return File(bytes, MediaTypeNames.Text.Xml, "Operation - Reprot.xml");
        }

        public IActionResult Operations(int groupId)
        {
            var request = new ReportRequest { Id = groupId };
            return View("Report", request);
        }

        [HttpPost]
        public async Task<FileResult> Operations(ReportRequest reportRequest)
        {
            byte[] bytes = await this.reportService.GenerateOperationsReport(reportRequest);
            return File(bytes, MediaTypeNames.Text.Xml, "Operations - Reprot.xml");
        }

        public async Task<IActionResult> Debts(int id)
        {
            byte[] bytes = await this.reportService.GenerateDebtsReport(id);
            return File(bytes, MediaTypeNames.Text.Xml, "Group - UserDebts.xml");
        }
    }
}
    
