using SupplyTrackerMVC.Application.Interfaces;
using SupplyTrackerMVC.Application.ViewModels.ReportVm;
using SupplyTrackerMVC.Domain.Interfaces;
using SupplyTrackerMVC.Domain.Model.Deliveries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.Generators
{
    public class ReportGenerator : IReportGenerator
    {
        private readonly IDeliveryRepository _deliveryRepository;

        public ReportGenerator(IDeliveryRepository deliveryRepository)
        {
            _deliveryRepository = deliveryRepository;
        }


        // Create Async after prototype is finish
        public ListReportDeliveryVm GetDailyReportData(ReportFilterVm filter)
        {
            var deliveries = _deliveryRepository.GetAllDeliveries()
                .Where(delivery =>
                delivery.DeliveryDataTime == filter.SelectedDate.Value.Date &&
                delivery.ReceiverId == filter.SelectedReceiverId &&
                delivery.ReceiverBranchId == filter.SelectedReceiverBranchId &&
                delivery.ProductId == filter.SelectedProductId)
                .ToList();
        }

        public ListReportDeliveryVm GetMonthlyReportData(ReportFilterVm filter)
        {
            throw new NotImplementedException();
        }
    }
}

