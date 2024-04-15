using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SupplyTrackerMVC.Application.Interfaces;
using SupplyTrackerMVC.Application.ViewModels.ReceiverVm;
using SupplyTrackerMVC.Domain.Interfaces;
using SupplyTrackerMVC.Domain.Model.Receivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.Services
{
    public class ReceiverService : IReceiverService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IReceiverRepository _receiverRepository;
        private readonly IMapper _mapper;

        public ReceiverService(IReceiverRepository receiverRepository, IMapper mapper, IServiceProvider serviceProvider)
        {
            _receiverRepository = receiverRepository;
            _mapper = mapper;
            _serviceProvider = serviceProvider;
        }

        public int AddNewReceiver(NewReceiverVm receiver)
        {
            throw new NotImplementedException();
        }

        public async Task<(bool Success, IEnumerable<string>? Errors, int? ReceiverId)> AddNewReceiverAsync(NewReceiverVm model)
        {
            var validator = _serviceProvider.GetService<IValidator<NewReceiverVm>>();
            if (validator == null)
            {
                throw new InvalidOperationException("Validator not found");
            }

            var result = await validator.ValidateAsync(model);
            if (!result.IsValid)
            {
                return (false, result.Errors.Select(e => e.ErrorMessage), null);
            }

            var receiver = _mapper.Map<Receiver>(model);
            var receiverId = _receiverRepository.AddReceiver(receiver);
            await _receiverRepository.SaveChangesAsync();

            return (true, null, receiverId);
        }

        public ListReceiverForListVm GetAllActiveReceiversForList()
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

        public ReceiverDetailsVm GetReceiverDetailsById(int receiverId)
        {
            var receiver = _receiverRepository.GetReceiverById(receiverId);
            var receiverVm = new ReceiverDetailsVm();
            receiverVm.Id = receiver.Id;
            receiverVm.Name = receiver.Name;
            receiverVm.Street = receiver.Address.Street;
            receiverVm.City = receiver.Address.City;
            receiverVm.ZIP = receiver.Address.ZIP;

            return receiverVm;
        }
    }
}
