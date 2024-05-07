using AutoMapper;
using AutoMapper.QueryableExtensions;
using SupplyTrackerMVC.Application.Interfaces;
using SupplyTrackerMVC.Application.ViewModels.SenderVm;
using SupplyTrackerMVC.Domain.Interfaces;
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
        public SenderService(ISenderRepository senderRepository, IMapper mapper)
        {
            _senderRepository = senderRepository;
            _mapper = mapper;
        }
        public async Task<(bool Success, IEnumerable<string>? Errors, int? SenderId)> AddNewSenderAsync(NewSenderVm model, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteSenderAsync(int senderId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public ListSenderForListVm GetAllActiveSendersForList()
        {
            throw new NotImplementedException();
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

        public SenderDetailsVm GetSenderDetailsById(int senderId)
        {
            throw new NotImplementedException();
        }

        public Task<(bool Success, IEnumerable<string>? Errors)> UpdateSenderByIdAsync(int senderId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
