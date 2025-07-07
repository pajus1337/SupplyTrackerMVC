using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SupplyTrackerMVC.Application.Interfaces;
using SupplyTrackerMVC.Application.Responses;
using SupplyTrackerMVC.Application.ViewModels.Common;
using SupplyTrackerMVC.Application.ViewModels.SenderVm;
using SupplyTrackerMVC.Domain.Interfaces;
using SupplyTrackerMVC.Domain.Model.Contacts;
using SupplyTrackerMVC.Domain.Model.Senders;

namespace SupplyTrackerMVC.Application.Services
{
    public class SenderService : ISenderService
    {
        private readonly ISenderRepository _senderRepository;
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;
        private readonly IFluentValidatorFactory _fluentValidatorFactory;

        public SenderService(ISenderRepository senderRepository, IContactRepository contactRepository, IMapper mapper, IFluentValidatorFactory fluentValidatorFactory)
        {
            _senderRepository = senderRepository;
            _contactRepository = contactRepository;
            _mapper = mapper;
            _fluentValidatorFactory = fluentValidatorFactory;
        }

        public async Task<ActionResponse<NewSenderVm>> AddNewSenderAsync(NewSenderVm model, CancellationToken cancellationToken)
        {
            var validator = _fluentValidatorFactory.GetValidator<NewSenderVm>();
            var validationResult = await validator.ValidateAsync(model, cancellationToken);
            if (!validationResult.IsValid)
            {
                return ActionResponse<NewSenderVm>.Failed(validationResult.Errors.Select(e => e.ErrorMessage), true);
            }

            var sender = _mapper.Map<Sender>(model);
            var (senderId, success) = await _senderRepository.AddSenderAsync(sender, cancellationToken);
            if (!success)
            {
                // TODO: Consider a more robust solution
                return ActionResponse<NewSenderVm>.Failed(new string[] { "Failed to add new Sender ( More details ? )" });
            }

            return ActionResponse<NewSenderVm>.Success(model, senderId);
        }


        //TODO : Refactor to implement ReturnService, improve logic.
        public async Task<ActionResponse<VoidValue>> DeleteSenderByIdAsync(int senderId, CancellationToken cancellationToken)
        {
            var (success, error, additionalMessage) = await _senderRepository.DeleteSenderAsync(senderId, cancellationToken);
            if (!success)
            {
                var errorMessage = new List<string>();
                if (error != null)
                {
                    errorMessage.Add(error);
                }
                ActionResponse<VoidValue>.Failed(errorMessage);
            }

            return ActionResponse<VoidValue>.Success(new VoidValue(), null, additionalMessage);
        }

        public async Task<ActionResponse<SenderDetailsVm>> UpdateSenderAsync(UpdateSenderVm updateSenderVm, CancellationToken cancellationToken)
        {
            if (updateSenderVm == null)
            {
                return ActionResponse<SenderDetailsVm>.Failed(new string[] { "Error occurred while processing the HTTP POST form" });
            }

            try
            {
                var validator = _fluentValidatorFactory.GetValidator<UpdateSenderVm>();
                var validationResult = await validator.ValidateAsync(updateSenderVm, cancellationToken);
                if (!validationResult.IsValid)
                {
                    return ActionResponse<SenderDetailsVm>.Failed(validationResult.Errors.Select(e => e.ErrorMessage), true);
                }

                var sender = _mapper.Map<Sender>(updateSenderVm);
                var success = await _senderRepository.UpdateSenderAsync(sender, cancellationToken);
                if (!success)
                {
                    return ActionResponse<SenderDetailsVm>.Failed(new string[] { "Failed to update sender" });
                }

                var response = await GetSenderDetailsByIdAsync(sender.Id, cancellationToken);
                if (!response.IsSuccessful)
                {
                    return ActionResponse<SenderDetailsVm>.Failed(new string[] { "Sender not found in DB after update " });
                }

                return ActionResponse<SenderDetailsVm>.Success(response.Data);
            }
            catch (Exception ex)
            {
                return ActionResponse<SenderDetailsVm>.Failed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }

        public async Task<ActionResponse<ListSenderForListVm>> GetSendersForListAsync(int pageSize, int pageNo, string searchString, CancellationToken cancellationToken)
        {
            try
            {
                var sendersQuery = _senderRepository.GetAllSenders().Where(p => p.Name.StartsWith(searchString)).OrderBy(p => p.Id);
                var sendersToShow = sendersQuery.Skip(pageSize * (pageNo - 1)).Take(pageSize);
                var senders = await sendersToShow.ToListAsync(cancellationToken);

                ListSenderForListVm result = new ListSenderForListVm();
                result.Senders = new List<SenderForListVm>();
                result.CurrentPage = pageNo;
                result.PageSize = pageSize;
                result.SearchString = searchString;
                result.Count = sendersQuery.Count();
                
                foreach (var sender in senders)
                {
                    var sendersForListVm = new SenderForListVm()
                    {
                        Id = sender.Id,
                        Name = sender.Name,
                    };

                    result.Senders.Add(sendersForListVm);
                }

                return ActionResponse<ListSenderForListVm>.Success(result);
            }
            catch (Exception ex)
            {
                return ActionResponse<ListSenderForListVm>.Failed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }

        public async Task<ActionResponse<SenderSelectListVm>> GetAllSendersForSelectListAsync(CancellationToken cancellationToken)
        {
            var sendersQuery = _senderRepository.GetAllSenders().ProjectTo<SenderForSelectListVm>(_mapper.ConfigurationProvider);
            try
            {
                var senders = await sendersQuery.ToListAsync(cancellationToken);
                if (senders == null)
                {
                    return ActionResponse<SenderSelectListVm>.Failed(new string[] { "Sender is null" });
                }

                return ActionResponse<SenderSelectListVm>.Success(new SenderSelectListVm { Senders = senders });
            }

            catch (Exception ex)
            {
                return ActionResponse<SenderSelectListVm>.Failed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }

        public async Task<ActionResponse<SenderDetailsVm>> GetSenderDetailsByIdAsync(int senderId, CancellationToken cancellationToken)
            => await GetSenderViewModelAsync<SenderDetailsVm>(senderId, cancellationToken);

        public async Task<ActionResponse<UpdateSenderVm>> GetSenderForEditAsync(int senderId, CancellationToken cancellationToken)
            => await GetSenderViewModelAsync<UpdateSenderVm>(senderId, cancellationToken);

        public async Task<ActionResponse<SenderForDeleteVm>> GetSenderForDeleteAsync(int senderId, CancellationToken cancellationToken)
            => await GetSenderViewModelAsync<SenderForDeleteVm>(senderId, cancellationToken);

        private async Task<ActionResponse<TViewModel>> GetSenderViewModelAsync<TViewModel>(int senderId, CancellationToken cancellationToken)
        {
            if (senderId <= 0)
            {
                return ActionResponse<TViewModel>.Failed(new string[] { "Invalid sender ID" });
            }

            var senderQuery = _senderRepository.GetSenderById(senderId).ProjectTo<TViewModel>(_mapper.ConfigurationProvider);
            try
            {
                var senderVm = await senderQuery.SingleOrDefaultAsync(cancellationToken);
                if (senderVm == null)
                {
                    return ActionResponse<TViewModel>.Failed(new string[] { "Sender not found in Db" });
                }

                return ActionResponse<TViewModel>.Success(senderVm);
            }
            catch (Exception ex)
            {
                return ActionResponse<TViewModel>.Failed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }

        // TODO: Refine AddSenderContactAsync Method
        public async Task<ActionResponse<NewContactVm>> AddSenderContactAsync(NewContactVm newContactVm, CancellationToken cancellationToken)
        {
            try
            {
                var validator = _fluentValidatorFactory.GetValidator<NewContactVm>();
                var result = await validator.ValidateAsync(newContactVm, cancellationToken);
                if (!result.IsValid)
                {
                    return ActionResponse<NewContactVm>.Failed(result.Errors.Select(e => e.ErrorMessage), true);
                }

                var contact = _mapper.Map<NewContactVm, Contact>(newContactVm);
                contact.SenderId = newContactVm.ContactOwnerId;
                // TODO: Testing part
              //  contact.ContactDetails =  new List<ContactDetail> { _mapper.Map<ContactDetail>(newContactVm.ContactDetailVm) };
                // End Of testing
                var contactId = await _contactRepository.AddContactAsync(contact, cancellationToken);

                return ActionResponse<NewContactVm>.Success(null, contactId);
            }
            catch (Exception ex)
            {
                return ActionResponse<NewContactVm>.Failed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }

        public Task<ActionResponse<ContactVm>> UpdateSenderContactAsync(UpdateContactVm updateContactVm, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResponse<VoidValue>> DeleteSenderContactAsync(int senderId, int senderContactId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


        // TODO: ASync ?! - Change prototype
        // TODO: Move to contactService? Generic the logic ?
        public async Task<ActionResponse<NewContactVm>> PrepareAddContactVm(int senderId)
        {
            var model = new NewContactVm
            {
                ContactOwnerId = senderId,
                ContactDetailVm = new NewContactDetailVm
                {
                    ContactDetailTypeSelectList = GetContactTypes()
                },
            };

            return ActionResponse<NewContactVm>.Success(model);
        }

        private ContactDetailTypeSelectListVm GetContactTypes() => new ContactDetailTypeSelectListVm()
        {
            ContactDetailTypes = _contactRepository.GetContactDetailTypes().ProjectTo<ContactDetailTypeForSelectListVm>(_mapper.ConfigurationProvider)
        };
    }
}
