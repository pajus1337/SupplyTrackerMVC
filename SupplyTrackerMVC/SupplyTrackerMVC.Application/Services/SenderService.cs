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

        public async Task<ServiceResponse<NewSenderVm>> AddNewSenderAsync(NewSenderVm model, CancellationToken cancellationToken)
        {
            var validator = _fluentValidatorFactory.GetValidator<NewSenderVm>();
            var validationResult = await validator.ValidateAsync(model, cancellationToken);
            if (!validationResult.IsValid)
            {
                return ServiceResponse<NewSenderVm>.CreateFailed(validationResult.Errors.Select(e => e.ErrorMessage), true);
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

            try
            {
                var validator = _fluentValidatorFactory.GetValidator<UpdateSenderVm>();
                var validationResult = await validator.ValidateAsync(updateSenderVm, cancellationToken);
                if (!validationResult.IsValid)
                {
                    return ServiceResponse<SenderDetailsVm>.CreateFailed(validationResult.Errors.Select(e => e.ErrorMessage), true);
                }

                var sender = _mapper.Map<Sender>(updateSenderVm);
                var success = await _senderRepository.UpdateSenderAsync(sender, cancellationToken);
                if (!success)
                {
                    return ServiceResponse<SenderDetailsVm>.CreateFailed(new string[] { "Failed to update sender" });
                }

                var response = await GetSenderDetailsByIdAsync(sender.Id, cancellationToken);
                if (!response.Success)
                {
                    return ServiceResponse<SenderDetailsVm>.CreateFailed(new string[] { "Sender not found in DB after update " });
                }

                return ServiceResponse<SenderDetailsVm>.CreateSuccess(response.Data);
            }
            catch (Exception ex)
            {
                return ServiceResponse<SenderDetailsVm>.CreateFailed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }

        public async Task<ServiceResponse<ListSenderForListVm>> GetSendersForListAsync(CancellationToken cancellationToken)
        {
            var sendersQuery = _senderRepository.GetAllSenders();

            try
            {
                var senders = await sendersQuery.ToListAsync(cancellationToken);

                ListSenderForListVm result = new ListSenderForListVm();
                result.Senders = new List<SenderForListVm>();

                foreach (var sender in senders)
                {
                    var sendersForListVm = new SenderForListVm()
                    {
                        Id = sender.Id,
                        Name = sender.Name,
                    };

                    result.Senders.Add(sendersForListVm);
                }

                return ServiceResponse<ListSenderForListVm>.CreateSuccess(result);
            }
            catch (Exception ex)
            {
                return ServiceResponse<ListSenderForListVm>.CreateFailed(new string[] { $"Error occurred -> {ex.Message}" });
            }
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
            => await GetSenderViewModelAsync<SenderDetailsVm>(senderId, cancellationToken);

        public async Task<ServiceResponse<UpdateSenderVm>> GetSenderForEditAsync(int senderId, CancellationToken cancellationToken)
            => await GetSenderViewModelAsync<UpdateSenderVm>(senderId, cancellationToken);

        public async Task<ServiceResponse<SenderForDeleteVm>> GetSenderForDeleteAsync(int senderId, CancellationToken cancellationToken)
            => await GetSenderViewModelAsync<SenderForDeleteVm>(senderId, cancellationToken);

        private async Task<ServiceResponse<TViewModel>> GetSenderViewModelAsync<TViewModel>(int senderId, CancellationToken cancellationToken)
        {
            if (senderId <= 0)
            {
                return ServiceResponse<TViewModel>.CreateFailed(new string[] { "Invalid sender ID" });
            }

            var senderQuery = _senderRepository.GetSenderById(senderId).ProjectTo<TViewModel>(_mapper.ConfigurationProvider);
            try
            {
                var senderVm = await senderQuery.SingleOrDefaultAsync(cancellationToken);
                if (senderVm == null)
                {
                    return ServiceResponse<TViewModel>.CreateFailed(new string[] { "Sender not found in Db" });
                }

                return ServiceResponse<TViewModel>.CreateSuccess(senderVm);
            }
            catch (Exception ex)
            {
                return ServiceResponse<TViewModel>.CreateFailed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }

        // TODO: Refine AddSenderContactAsync Method
        public async Task<ServiceResponse<AddContactVm>> AddSenderContactAsync(AddContactVm newContactVm, CancellationToken cancellationToken)
        {
            var validator = _fluentValidatorFactory.GetValidator<AddContactVm>();

            try
            {
                var result = await validator.ValidateAsync(newContactVm, cancellationToken);
                if (!result.IsValid)
                {
                    return ServiceResponse<AddContactVm>.CreateFailed(result.Errors.Select(e => e.ErrorMessage), true);
                }

                var contact = _mapper.Map<AddContactVm, Contact>(newContactVm);
                contact.SenderId = newContactVm.ContactOwnerId;
                // TODO: Testing part
              //  contact.ContactDetails =  new List<ContactDetail> { _mapper.Map<ContactDetail>(newContactVm.ContactDetailVm) };
                // End Of testing
                var (contactId, success) = await _contactRepository.AddContactAsync(contact, cancellationToken);

                if (!success)
                {
                    return ServiceResponse<AddContactVm>.CreateFailed(new string[] { "Failed to save new contact in Db" });
                }
                return ServiceResponse<AddContactVm>.CreateSuccess(null, contactId);
            }
            catch (Exception ex)
            {
                return ServiceResponse<AddContactVm>.CreateFailed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }

        public Task<ServiceResponse<ContactDetailsVm>> UpdateSenderContactAsync(UpdateContactVm updateContactVm, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<VoidValue>> DeleteSenderContactAsync(int senderId, int senderContactId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


        // TODO: ASync ?! - Change prototype
        public async Task<ServiceResponse<AddContactVm>> PrepareAddContactVm(int senderId)
        {
            var model = new AddContactVm
            {
                ContactOwnerId = senderId,
                ContactDetailVm = new AddContactDetailVm
                {
                    ContactDetailTypeSelectList = GetContactTypes()
                },
            };

            return ServiceResponse<AddContactVm>.CreateSuccess(model);
        }

        private ContactDetailTypeSelectListVm GetContactTypes() => new ContactDetailTypeSelectListVm()
        {
            ContactDetailTypes = _contactRepository.GetContactDetailTypes().ProjectTo<ContactDetailTypeForSelectListVm>(_mapper.ConfigurationProvider)
        };

        // TODO: Add, Add , Edit & Delete metod's for Sender Contact's 



    }
}
