using SupplyTrackerMVC.Application.ViewModels.ReportVm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.Interfaces
{
    public interface IReportGenerator
    {
        ListReportDeliveryVm GetDailyReportData(ReportFilterVm filter);
        ListReportDeliveryVm GetMonthlyReportData(ReportFilterVm filter);
    }
}
