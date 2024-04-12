using SupplyTrackerMVC.Application.Interfaces;
using SupplyTrackerMVC.Application.ViewModels.Receiver;
using SupplyTrackerMVC.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.Services
{
    public class ReceiverService : IReceiverService
    {
        private readonly IReceiverRepository _receiverRepository;
        public int AddNewReceiver(NewReceiverVm receiver)
        {
            throw new NotImplementedException();
        }

        public ListReceiverForListVm GetAllReceiversForList()
        {
            var receivers = _receiverRepository.GetAllActiveReceivers();
            ListReceiverForListVm result = new ListReceiverForListVm();
            result.Receivers = new List<ReceiverForListVm>();

            foreach (var receiver in receivers)
            {
                var receiverForListVm = new ReceiverForListVm();
                {
                    receiverForListVm.Id = receiver.Id;
                    receiverForListVm.Name = receiver.Name;
                }
                result.Receivers.Add(receiverForListVm);
            }
            return result;
        }

        public ReceiverDetailsVm GetReceiiverDetailsById(int receiverId)
        {
            var receiver = _receiverRepository.GetReceiverById(receiverId);
            var receiverVm = new NewReceiverVm();
            receiverVm.Id = 
        }
    }
}
