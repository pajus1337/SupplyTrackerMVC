using AutoMapper;
using AutoMapper.QueryableExtensions;
using SupplyTrackerMVC.Application.Interfaces;
using SupplyTrackerMVC.Application.ViewModels.SenderVm;
using SupplyTrackerMVC.Domain.Interfaces;
using SupplyTrackerMVC.Domain.Model.Senders;
using System;
using System.Collections.Generic;
using System.Linq;
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


        public ListSenderForListVm GetAllActiveSendersForList()
        {
            var senders = _senderRepository.GetAllActiveSenders();

        }

        public SenderSelectListVm GetAllActiveSendersForSelectList()
        {
            var senders = _senderRepository.GetAllActiveSenders().ProjectTo<SenderForSelectListVm>(_mapper.ConfigurationProvider);
            var senderSelectListVm = new SenderSelectListVm()
            {
                Senders = senders
            };

            return senderSelectListVm;
        }

        public async Task<(bool Success, SenderDetailsVm)> GetSenderDetailsByIdAsync(int senderId, CancellationToken cancellationToken)
        {
            var (success, sender) = await _senderRepository.GetSenderByIdAsync(senderId, cancellationToken);
            if (!success)
            {
                return (false, null);
            }

            var senderVm = _mapper.Map<SenderDetailsVm>(sender);
            return (true, senderVm);
        }

        public Task<(bool Success, IEnumerable<string>? Errors)> UpdateSenderByIdAsync(int senderId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
