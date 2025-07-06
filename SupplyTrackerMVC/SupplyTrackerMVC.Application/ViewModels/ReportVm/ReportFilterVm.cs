using FluentValidation;
using SupplyTrackerMVC.Application.Enums;
using SupplyTrackerMVC.Application.ViewModels.ProductVm;
using SupplyTrackerMVC.Application.ViewModels.ReceiverVm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.ViewModels.ReportVm
{
    public class ReportFilterVm
    {
        public int SelectedReceiverId { get; set; }
        public int SelectedReceiverBranchId { get; set; }
        public int SelectedProductId { get; set; }
        public ReceiverSelectListVm Receivers { get; set; }
        public ReceiverBranchSelectListVm ReceiverBranches { get; set; }
        public ProductSelectListVm Products { get; set; }
        public ReportType ReportType { get; set; }
        public DateTime? SelectedDate { get; set; }
        public int SelectedMonth { get; set; }
        public int SelectedYear { get; set; }

        public class ReportFilterValidator : AbstractValidator<ReportFilterVm>
        {
            public ReportFilterValidator()
            {
                RuleFor(x => x.ReportType).IsInEnum();
                RuleFor(x => x.SelectedReceiverId).GreaterThan(0);
                RuleFor(x => x.SelectedReceiverBranchId).GreaterThan(0);
                RuleFor(x => x.SelectedProductId).GreaterThan(0);
            }

        }
    }
}
