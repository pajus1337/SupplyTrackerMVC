using Microsoft.EntityFrameworkCore;
using SupplyTrackerMVC.Application.Interfaces;
using SupplyTrackerMVC.Application.Responses;
using SupplyTrackerMVC.Application.ViewModels.SenderVm;
using SupplyTrackerMVC.Application.ViewModels.StatisticsVm;
using SupplyTrackerMVC.Domain.Interfaces;
using SupplyTrackerMVC.Domain.Model.Receivers;
using SupplyTrackerMVC.Domain.Model.Senders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IDeliveryRepository _deliveryRepository;
        private readonly IReceiverRepository _receiverRepository;
        private readonly ISenderRepository _senderRepository;

        public StatisticsService(IDeliveryRepository deliveryRepository, IReceiverRepository receiverRepository, ISenderRepository senderRepository)
        {
            _deliveryRepository = deliveryRepository;
            _receiverRepository = receiverRepository;
            _senderRepository = senderRepository;
        }

        public async Task<ActionResponse<StatisticsDataVm>> GetStatisticsAsync(CancellationToken cancellationToken)
        {
            try
            {
                var deliveriesQuery = _deliveryRepository.GetAllDeliveries();
                var sendersQuery = _senderRepository.GetAllSenders();
                var receiversQuery = _receiverRepository.GetAllReceivers();

                var now = DateTime.Now;
                var startOfWeek = now.Date.AddDays(-(int)now.DayOfWeek + (int)DayOfWeek.Monday);

                var totalDeliveries = await deliveriesQuery.CountAsync(cancellationToken);
                var deliveriesThisWeek = await deliveriesQuery.Where(delivery => delivery.DeliveryDateTime >= startOfWeek).CountAsync();
                var mostFrequentProduct = await deliveriesQuery
                    .GroupBy(delivery => delivery.Product.Name)
                    .OrderByDescending(g => g.Count())
                    .Select(g => g.Key)
                    .FirstOrDefaultAsync(cancellationToken)
                    ?? "Most Frequent Products data error";

                var totalProductsUsed = await deliveriesQuery
                    .Select(delivery => delivery.Product.Name)
                    .Distinct()
                    .CountAsync(cancellationToken);

                var totalSenders = await sendersQuery.CountAsync();
                var totalReceivers = await receiversQuery.CountAsync();
                var totalReceiverBranches = await receiversQuery
                    .SelectMany(receiver => receiver.ReceiverBranches)
                    .CountAsync(cancellationToken);


                var statisticsDataVm = new StatisticsDataVm
                {
                    TotalDeliveries = totalDeliveries,
                    DeliveriesThisWeek = deliveriesThisWeek,
                    MostFrequentProduct = mostFrequentProduct,
                    TotalProducts = totalProductsUsed,
                    TotalSenders = totalSenders,
                    TotalReceivers = totalReceivers,
                    TotalReceiverBranches = totalReceiverBranches,
                };

                var serviceResponse = ActionResponse<StatisticsDataVm>.Success(statisticsDataVm);

                return serviceResponse;
            }
            catch (Exception ex)
            {
                return ActionResponse<StatisticsDataVm>.Failed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }
    }
}
