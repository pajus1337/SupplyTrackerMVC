using Microsoft.AspNetCore.Mvc;
using SupplyTrackerMVC.Application.Interfaces;
using SupplyTrackerMVC.Application.ViewModels.ReportVm;

namespace SupplyTrackerMVC.Web.Controllers
{
    public class ReportController : BaseController
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        public async Task<IActionResult> GeneratePdfReport(CancellationToken cancellationToken)
        {
            var model = await _reportService.PrepareReportFilterVm(cancellationToken) ;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> GeneratePdfReport(ReportFilterVm model)
        {
            throw new NotImplementedException();
        }



    }
}
