using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SupplyTrackerMVC.Application.Interfaces;
using SupplyTrackerMVC.Application.Responses;
using SupplyTrackerMVC.Application.ViewModels.SenderVm;
using SupplyTrackerMVC.Domain.Interfaces;
using SupplyTrackerMVC.Domain.Model.Senders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.Services
{
    public class SenderService : ISenderService
    {
        private readonly ISenderRepository _senderRepository;
        private readonly IMapper _mapper;
        private readonly IFluentValidatorFactory _fluentValidatorFactory;

        public SenderService(ISenderRepository senderRepository, IMapper mapper, IFluentValidatorFactory fluentValidatorFactory)
        {
            _senderRepository = senderRepository;
            _mapper = mapper;
            _fluentValidatorFactory = fluentValidatorFactory;
        }

        public async Task<ServiceResponse<NewSenderVm>> AddNewSenderAsync(NewSenderVm model, CancellationToken cancellationToken)
        {
            var validator = _fluentValidatorFactory.GetValidator<NewSenderVm>();
            var validationResult = await validator.ValidateAsync(model, cancellationToken);
            if (!validationResult.IsValid)
            {
                return ServiceResponse<NewSenderVm>.CreateFailed(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var sender = _mapper.Map<Sender>(model);
            var (senderId, success) = await _senderRepository.AddSenderAsync(sender, cancellationToken);
            if (!success)
            {
                // TODO: Consider a more robust solution
                return ServiceResponse<NewSenderVm>.CreateFailed(new string[] { "Failed to add new Sender ( More details ? )" });
            }

            return ServiceResponse<NewSenderVm>.CreateSuccess(model, senderId);
        }

        public async Task<ServiceResponse<VoidValue>> DeleteSenderAsync(int senderId, CancellationToken cancellationToken)
        {
            var (success, error, additionalMessage) = await _senderRepository.DeleteSenderAsync(senderId, cancellationToken);
            if (!success)
            {
                var errorMessage = new List<string>();
                if (error != null)
                {
                    errorMessage.Add(error);
                }
                ServiceResponse<VoidValue>.CreateFailed(errorMessage);
            }

            return ServiceResponse<VoidValue>.CreateSuccess(new VoidValue(), null, additionalMessage);
        }

        public async Task<ServiceResponse<SenderDetailsVm>> UpdateSenderByIdAsync(UpdateSenderVm updateSenderVm, CancellationToken cancellationToken)
        {
            if (updateSenderVm == null)
            {
                return ServiceResponse<SenderDetailsVm>.CreateFailed(new string[] { "Error occurred while processing the HTTP POST form" });
            }

            var sender = _mapper.Map<Sender>(updateSenderVm);
            var success = await _senderRepository.UpdateSenderAsync(sender, cancellationToken);
            if (!success)
            {
                return ServiceResponse<SenderDetailsVm>.CreateFailed(new string[] { "Failed to update sender" });
            }

            var response = await GetSenderDetailsByIdAsync(sender.Id, cancellationToken);
            if (response.Data == null)
            {
                return ServiceResponse<SenderDetailsVm>.CreateFailed(new string[] { "Sender not found after update " });
            }

            return ServiceResponse<SenderDetailsVm>.CreateSuccess(response.Data);
        }

        public Task<ServiceResponse<ListSenderForListVm>> GetAllSendersForListAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<SenderSelectListVm>> GetAllSendersForSelectListAsync(CancellationToken cancellationToken)
        {
            var sendersQuery = _senderRepository.GetAllSenders().ProjectTo<SenderForSelectListVm>(_mapper.ConfigurationProvider);
            try
            {
                var senders = await sendersQuery.ToListAsync(cancellationToken);
                if (senders == null)
                {
                    return ServiceResponse<SenderSelectListVm>.CreateFailed(new string[] { "Sender is null" });
                }

                return ServiceResponse<SenderSelectListVm>.CreateSuccess(new SenderSelectListVm { Senders = senders });
            }

            catch (Exception ex)
            {
                return ServiceResponse<SenderSelectListVm>.CreateFailed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }

        public async Task<ServiceResponse<SenderDetailsVm>> GetSenderDetailsByIdAsync(int senderId, CancellationToken cancellationToken)
        {
            var senderQuery = _senderRepository.GetSenderById(senderId).ProjectTo<SenderDetailsVm>(_mapper.ConfigurationProvider);
            try
            {
                var senderVm = await senderQuery.SingleOrDefaultAsync(cancellationToken);
                if (senderVm == null)
                {
                    return ServiceResponse<SenderDetailsVm>.CreateFailed(new string[] { "Sender not found in Db" });
                }

                return ServiceResponse<SenderDetailsVm>.CreateSuccess(senderVm);
            }
            catch (Exception ex)
            {
                return ServiceResponse<SenderDetailsVm>.CreateFailed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }
    }
}
