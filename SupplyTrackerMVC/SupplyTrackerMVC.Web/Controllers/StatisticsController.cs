using Microsoft.AspNetCore.Mvc;
using SupplyTrackerMVC.Application.Interfaces;

namespace SupplyTrackerMVC.Web.Controllers
{
    public class StatisticsController : BaseController
    {
        private readonly IStatisticsService _statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var serviceResponse = await _statisticsService.GetStatisticsAsync(cancellationToken);
            return View(serviceResponse.Data);
        }
    }
}
