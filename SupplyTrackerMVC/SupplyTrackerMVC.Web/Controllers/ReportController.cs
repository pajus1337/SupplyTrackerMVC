using Microsoft.AspNetCore.Mvc;
using SupplyTrackerMVC.Application.Enums;
using SupplyTrackerMVC.Application.Interfaces;
using SupplyTrackerMVC.Application.ViewModels.ReportVm;

namespace SupplyTrackerMVC.Web.Controllers
{
    public class ReportController : BaseController
    {
        private readonly IReportService _reportService;
        private readonly IReportGenerator _reportGenerator;

        public ReportController(IReportService reportService, IReportGenerator reportGenerator)
        {
            _reportService = reportService;
            _reportGenerator = reportGenerator;
        }

        [HttpGet]
        public async Task<IActionResult> GeneratePdfReport(ReportType reportType, CancellationToken cancellationToken)
        {
            var serviceResponse = await _reportService.PrepareReportFilterVm(reportType, cancellationToken);
            return View(serviceResponse.Data);
        }

        [HttpPost]
        public async Task<IActionResult> GeneratePdfReport(ReportFilterVm model, CancellationToken cancellationToken)
        {
            var serviceResponse = await _reportService.GenerateReportAsync(model, cancellationToken);

            if (!serviceResponse.IsSuccessful)
            {
                return HandleErrors(serviceResponse);
            }

            return File(serviceResponse.Data.GeneratedPdf, "application/pdf", "report.pdf");
        }
    }
}
