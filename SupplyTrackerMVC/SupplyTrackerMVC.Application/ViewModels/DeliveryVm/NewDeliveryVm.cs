using FluentValidation;
using SupplyTrackerMVC.Application.ViewModels.ReceiverVm;
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
        public DateTime DeliveryDataTime { get; set; }
        public int SelectedReceiverId { get; set; }
        public IEnumerable<ReceiverForSelectListVm> Receivers { get; set; }
        public int SelectedReceiverBranchId { get; set; }
        public IEnumerable<ReceiverBranch> ReceiverBranches { get; set; }
        public int SelectedPProductId { get; set; }
        public IEnumerable<Product> Products { get; set; }

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
