using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SupplyTrackerMVC.Application.Interfaces;
using SupplyTrackerMVC.Application.Responses;
using SupplyTrackerMVC.Application.ViewModels.DeliveryVm;
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
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;

        public ReceiverService(IReceiverRepository receiverRepository, IContactRepository contactRepository, IMapper mapper, IServiceProvider serviceProvider)
        {
            _receiverRepository = receiverRepository;
            _contactRepository = contactRepository;
            _mapper = mapper;
            _serviceProvider = serviceProvider;
        }

        public async Task<ServiceResponse<VoidValue>> AddReceiverAsync(NewReceiverVm model, CancellationToken cancellationToken)
        {
            var validator = _serviceProvider.GetService<IValidator<NewReceiverVm>>();
            if (validator == null)
            {
                throw new InvalidOperationException("Validator not found");
            }

            var result = await validator.ValidateAsync(model);
            if (!result.IsValid)
            {
                return ServiceResponse<VoidValue>.CreateFailed(result.Errors.Select(e => e.ErrorMessage));
            }

            var receiver = _mapper.Map<Receiver>(model);
            var (isSuccess, receiverId) = await _receiverRepository.AddReceiverAsync(receiver, cancellationToken);

            return isSuccess ? ServiceResponse<VoidValue>.CreateSuccess(null, receiverId) : ServiceResponse<VoidValue>.CreateFailed(new string[] { "Failed to add receiver" });
        }

        public async Task<ServiceResponse<ListReceiverForListVm>> GetReceiversForListAsysnc(CancellationToken cancellationToken)
        {
            var receiversQuery = _receiverRepository.GetAllReceivers();

            try
            {
                var receivers = await receiversQuery.ToListAsync(cancellationToken);

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

                return ServiceResponse<ListReceiverForListVm>.CreateSuccess(result);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ServiceResponse<ReceiverDetailsAfterCreateVm>> GetReceiverDetailsByIdAsync(int receiverId, CancellationToken cancellationToken)
        {
            if (receiverId == 0)
            {
                return ServiceResponse<ReceiverDetailsAfterCreateVm>.CreateFailed(new string[] { "Wrong receiver Id" });
            }

            var receiverQuery = _receiverRepository.GetReceiverById(receiverId);
            try
            {
                var receiver = await receiverQuery.SingleOrDefaultAsync(cancellationToken);
                if (receiver == null)
                {
                    ServiceResponse<ReceiverDetailsAfterCreateVm>.CreateFailed(new string[] { "receiver is null" });
                }

                var receiverVm = _mapper.Map<ReceiverDetailsAfterCreateVm>(receiver);

                return ServiceResponse<ReceiverDetailsAfterCreateVm>.CreateSuccess(receiverVm);
            }
            catch (Exception ex)
            {
                return ServiceResponse<ReceiverDetailsAfterCreateVm>.CreateFailed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }


        public async Task<ServiceResponse<ReceiverSelectListVm>> GetReceiversForSelectListAsync(CancellationToken cancellationToken)
        {
            var receiversQuery = _receiverRepository.GetAllReceivers().ProjectTo<ReceiverForSelectListVm>(_mapper.ConfigurationProvider);

            try
            {
                var receivers = await receiversQuery.ToListAsync(cancellationToken);
                if (receivers == null)
                {
                    return ServiceResponse<ReceiverSelectListVm>.CreateFailed(new string[] { "receiver is null" });
                }
                var receiversVm = new ReceiverSelectListVm()
                {
                    Receivers = receiversQuery,
                };

                return ServiceResponse<ReceiverSelectListVm>.CreateSuccess(receiversVm);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ServiceResponse<ReceiverBranchSelectListVm>> GetReceiverBranchesForSelectListAsync(CancellationToken cancellationToken)
        {
            var receiverBranchesQuery = _receiverRepository.GetAllReceiverBranches().ProjectTo<ReceiverBranchForSelectListVm>(_mapper.ConfigurationProvider);

            try
            {
                var receiverBranches = await receiverBranchesQuery.ToListAsync(cancellationToken);
                if (receiverBranches == null)
                {
                    return ServiceResponse<ReceiverBranchSelectListVm>.CreateFailed(new string[] { "receiver is null" });
                }
                var receiverBranchesVm = new ReceiverBranchSelectListVm()
                {
                    ReceiverBranches = receiverBranchesQuery,
                };

                return ServiceResponse<ReceiverBranchSelectListVm>.CreateSuccess(receiverBranchesVm);
            }
            catch (Exception ex)
            {
                return ServiceResponse<ReceiverBranchSelectListVm>.CreateFailed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }

        public async Task<ServiceResponse<VoidValue>> AddReceiverBranchAsync(NewReceiverBranchVm model, CancellationToken cancellationToken)
        {
            if (model == null)
            {
                return ServiceResponse<VoidValue>.CreateFailed(new string[] { "Error occurred while processing the HTTP POST form" });
            }

            var receiverBranch = _mapper.Map<ReceiverBranch>(model);

            try
            {
                var isSuccess = await _receiverRepository.AddReceiverBranchAsync(receiverBranch, cancellationToken);

                return isSuccess ? ServiceResponse<VoidValue>.CreateSuccess(null, receiverBranch.Id) : ServiceResponse<VoidValue>.CreateFailed(new string[] {"Failed to add new Receiver Branch"});
            }
            catch (Exception)
            {

                throw;
            }
        }


        // TODO: Create better implementation of this prototype metod
        public async Task<NewReceiverBranchVm> PrepareNewReceiverBranchVm(CancellationToken cancellationToken)
        {
            var receivers = await GetReceiversForSelectListAsync(cancellationToken);
            var contactTypes = 
        
            var model = new NewReceiverBranchVm();
            model.ReceiverSelectList = receivers.Data;

            return model;
        }
    }
}
