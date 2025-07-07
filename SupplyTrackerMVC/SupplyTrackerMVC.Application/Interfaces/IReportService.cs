using SupplyTrackerMVC.Application.Enums;
using SupplyTrackerMVC.Application.Responses;
using SupplyTrackerMVC.Application.ViewModels.ReportVm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.Interfaces
{
    public interface IReportService
    {
        Task<ActionResponse<ReportFilterVm>> PrepareReportFilterVm(ReportType reportType, CancellationToken cancellationToken);
        Task<ActionResponse<ReportGenerationResult>> GenerateReportAsync(ReportFilterVm filterModel, CancellationToken cancellationToken);
    }
}
