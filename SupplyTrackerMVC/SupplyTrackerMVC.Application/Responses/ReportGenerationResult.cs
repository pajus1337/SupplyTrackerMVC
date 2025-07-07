using SupplyTrackerMVC.Application.ViewModels.ReportVm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.Responses
{
    public class ReportGenerationResult
    {
        public ReportFilterVm? InputModel { get; set; }
        public ListReportDeliveryVm? ReportData { get; set; }
        public byte[]? GeneratedPdf { get; set; }
    }
}
