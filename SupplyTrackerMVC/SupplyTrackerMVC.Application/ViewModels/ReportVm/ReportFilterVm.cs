using FluentValidation;
using SupplyTrackerMVC.Application.Enums;
using SupplyTrackerMVC.Application.ViewModels.ProductVm;
using SupplyTrackerMVC.Application.ViewModels.ReceiverVm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.ViewModels.ReportVm
{
    public class ReportFilterVm
    {
        [Display(Name = "Receiver")]
        public int SelectedReceiverId { get; set; }

        [Display(Name = "Receiver Branch")]
        public int SelectedReceiverBranchId { get; set; }

        [Display(Name = "Product")]
        public int SelectedProductId { get; set; }

        public ReceiverSelectListVm Receivers { get; set; }
        public ReceiverBranchSelectListVm ReceiverBranches { get; set; }
        public ProductSelectListVm Products { get; set; }

        [Display(Name = "Report Type")]
        public ReportType ReportType { get; set; }

        [Display(Name = "Date")]
        public DateTime? SelectedDate { get; set; }

        [Display(Name = "Month")]
        public int SelectedMonth { get; set; }

        [Display(Name = "Year")]
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
