using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SupplyTrackerMVC.Application.Interfaces;
using SupplyTrackerMVC.Application.Responses;
using SupplyTrackerMVC.Application.ViewModels.ReceiverVm;
using SupplyTrackerMVC.Application.ViewModels.SenderVm;
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

        public async Task<(bool Success, IEnumerable<string>? Errors, int? ReceiverId)> AddReceiverAsync(NewReceiverVm model, CancellationToken cancellationToken)
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
            var (isSuccess, receiverId) = await _receiverRepository.AddReceiverAsync(receiver, cancellationToken);

            return isSuccess ? (true, null, receiverId) : (false, new string[] { "Failed to add receiver" }, null);
        }

        public ListReceiverForListVm GetAllActiveReceiversForList()
        {
            var receivers = _receiverRepository.GetAllReceivers();
            ListReceiverForListVm result = new ListReceiverForListVm();
            result.Receivers = new List<ReceiverForListVm>();

            foreach (var receiver in receivers)
            {
                var receiverForListVm = new ReceiverForListVm()
                {
                    Id = receiver.Id,
                    Name = receiver.Name,
                };

                result.Receivers.Add(receiverForListVm);
            }
            return result;
        }

        public async Task<ServiceResponse<ReceiverDetailsVm>> GetReceiverDetailsByIdAsync(int receiverId, CancellationToken cancellationToken)
        {
            if (receiverId == 0)
            {
                return ServiceResponse<ReceiverDetailsVm>.CreateFailed(new string[] { "Wrong receiver Id" });
            }

            var receiverQuery = _receiverRepository.GetReceiverById(receiverId);
            try
            {
                var receiver = await receiverQuery.SingleOrDefaultAsync(cancellationToken);
                if (receiver == null)
                {
                    ServiceResponse<ReceiverDetailsVm>.CreateFailed(new string[] { "receiver is null" });
                }

                var receiverVm = _mapper.Map<ReceiverDetailsVm>(receiver);

                return ServiceResponse<ReceiverDetailsVm>.CreateSuccess(receiverVm);
            }
            catch (Exception ex)
            {
                return ServiceResponse<ReceiverDetailsVm>.CreateFailed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }


        public ReceiverSelectListVm GetAllActiveReceiversForSelectList()
        {
            var receivers = _receiverRepository.GetAllReceivers().ProjectTo<ReceiverForSelectListVm>(_mapper.ConfigurationProvider);
            var receiversVm = new ReceiverSelectListVm()
            {
                Receivers = receivers,
            };

            return receiversVm;
        }

        public ReceiverBranchSelectListVm GetAllActiveReceiverBranchesForSelectList()
        {
            var receiverBranches = _receiverRepository.GetAllReceiverBranches().ProjectTo<ReceiverBranchForSelectListVm>(_mapper.ConfigurationProvider);
            var receiverBranchesVm = new ReceiverBranchSelectListVm()
            {
                ReceiverBranches = receiverBranches,
            };

            return receiverBranchesVm;
        }
    }
}
