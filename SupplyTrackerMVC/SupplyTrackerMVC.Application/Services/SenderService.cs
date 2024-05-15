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
        public async Task<(bool Success, IEnumerable<string>? Errors, int? SenderId)> AddNewSenderAsync(NewSenderVm model, CancellationToken cancellationToken)
        {
            var validator = _fluentValidatorFactory.GetValidator<NewSenderVm>();
            var validationResult = await validator.ValidateAsync(model, cancellationToken);
            if (!validationResult.IsValid)
            {
                return (false, validationResult.Errors.Select(e => e.ErrorMessage) , null);
            }

            var sender = _mapper.Map<Sender>(model);
            var (senderId, success) = await _senderRepository.AddSenderAsync(sender,cancellationToken);
            if (!success)
            {
                // TODO: Think about fail case scenario :>
            }
            return (true, null, senderId);
        }

        public async Task<bool> DeleteSenderAsync(int senderId, CancellationToken cancellationToken) => await _senderRepository.DeleteSenderAsync(senderId, cancellationToken);

        public Task<(bool Success, ListSenderForListVm Model)> GetAllActiveSendersForListAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public SenderSelectListVm GetAllActiveSendersForSelectList()
        {
            var senders = _senderRepository.GetAllSenders().ProjectTo<SenderForSelectListVm>(_mapper.ConfigurationProvider);
            var senderSelectListVm = new SenderSelectListVm()
            {
                Senders = senders
            };

            return senderSelectListVm;
        }

        public async Task<SenderResponse<SenderDetailsVm>> GetSenderDetailsByIdAsync(int senderId, CancellationToken cancellationToken)
        {
            var senderQuery = _senderRepository.GetSenderById(senderId).ProjectTo<SenderDetailsVm>(_mapper.ConfigurationProvider);
            try
            {
                var senderVm = await senderQuery.SingleOrDefaultAsync(cancellationToken);

                if (senderVm == null)
                {
                   return SenderResponse<SenderDetailsVm>.CreateFail("Sender not found in Db");
                }

                return SenderResponse<SenderDetailsVm>.CreateSuccess(senderVm);
            }
            catch (Exception ex)
            {
                return SenderResponse<SenderDetailsVm>.CreateFail($"Error occured -> {ex}");
            }
        }

        public Task<(bool Success, IEnumerable<string>? Errors)> UpdateSenderByIdAsync(int senderId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<(bool Success, SenderSelectListVm Model)> ISenderService.GetAllActiveSendersForSelectList()
        {
            throw new NotImplementedException();
        }
    }
}
