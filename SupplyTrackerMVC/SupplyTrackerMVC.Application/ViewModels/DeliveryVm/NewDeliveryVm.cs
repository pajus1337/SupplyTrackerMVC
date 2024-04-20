using FluentValidation;
using SupplyTrackerMVC.Application.ViewModels.ProductVm;
using SupplyTrackerMVC.Application.ViewModels.ReceiverVm;
using SupplyTrackerMVC.Application.ViewModels.SenderVm;
using SupplyTrackerMVC.Domain.Model.Products;
using SupplyTrackerMVC.Domain.Model.Receivers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.ViewModels.DeliveryVm
{
    public class NewDeliveryVm
    {
        public int Id { get; set; }
        public DateTime DeliveryDataTime { get; set; } = DateTime.Now;
        public int SelectedSenderId { get; set; }
        public SenderSelectListVm Senders { get; set; }
        public int SelectedReceiverId { get; set; }
        public ReceiverSelectListVm Receivers { get; set; }
        public int SelectedReceiverBranchId { get; set; }
        public ReceiverBranchSelectListVm ReceiverBranches { get; set; }
        public int SelectedProductId { get; set; }
        public ProductSelectListVm Products { get; set; }

        public int ProductDeliveryWeight { get; set; }

        public class NewDeliveryValidator : AbstractValidator<NewDeliveryVm>
        {
            public NewDeliveryValidator()
            {
                RuleFor(x => x.Id).NotNull();
                RuleFor(x => x.DeliveryDataTime).GreaterThanOrEqualTo(DateTime.Today);
            }
        }
    }
}
