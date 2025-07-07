using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SupplyTrackerMVC.Application.Interfaces;
using SupplyTrackerMVC.Application.Responses;
using SupplyTrackerMVC.Application.ViewModels.Common;
using SupplyTrackerMVC.Domain.Interfaces;
using SupplyTrackerMVC.Domain.Model.Contacts;
using SupplyTrackerMVC.Domain.Model.Senders;

namespace SupplyTrackerMVC.Application.Services
{
    public class ContactService(IContactRepository contactRepository, IFluentValidatorFactory fluentValidatorFactory, IMapper mapper) : IContactService
    {
        private readonly IContactRepository _contactRepository = contactRepository;
        private readonly IFluentValidatorFactory _validatorFactory = fluentValidatorFactory;
        private readonly IMapper _mapper = mapper;

        public async Task<ActionResponse<NewContactDetailTypeVm>> AddContactDetailTypeAsync(NewContactDetailTypeVm model, CancellationToken cancellationToken)
        {
            if (model == null)
            {
                return ActionResponse<NewContactDetailTypeVm>.Failed(new string[] { "Error occurred while processing the HTTP POST form" });
            }

            try
            {
                var validator = _validatorFactory.GetValidator<NewContactDetailTypeVm>();
                var validationResult = await validator.ValidateAsync(model, cancellationToken);
                if (!validationResult.IsValid)
                {
                    return ActionResponse<NewContactDetailTypeVm>.Failed(validationResult.Errors.Select(e => e.ErrorMessage), true);
                }

                var contactDetailType = _mapper.Map<ContactDetailType>(model);
                var contactDetailTypeId = await _contactRepository.AddContactDetailTypeAsync(contactDetailType, cancellationToken);

                return ActionResponse<NewContactDetailTypeVm>.Success(null, contactDetailTypeId);
            }
            catch (Exception ex)
            {
                return ActionResponse<NewContactDetailTypeVm>.Failed(new string[] { $"Error occurred -> {ex.Message}" }, false);
            }
        }

        public async Task<ActionResponse<VoidValue>> DeleteContactDetailTypeAsync(int contactTypeId, CancellationToken cancellationToken)
        {
            var success = await _contactRepository.DeleteContactDetailTypeAsync(contactTypeId, cancellationToken);

            if (!success)
            {
                // TODO: Complete the implementation of whole function
            }

            return ActionResponse<VoidValue>.Success(new VoidValue(), null, null);
        }

        public async Task<ActionResponse<ContactDetailTypeVm>> GetContactDetailTypeAsync(int contactDetailTypeId, CancellationToken cancellationToken)
        {
            if (contactDetailTypeId < 1)
            {
                return ActionResponse<ContactDetailTypeVm>.Failed(new string[] { "Invalid contact detail type ID" });
            }

            try
            {
                var contactDetailTypeQuery = _contactRepository.GetContactDetailTypeById(contactDetailTypeId).ProjectTo<ContactDetailTypeVm>(_mapper.ConfigurationProvider);
                var contactDetailTypeVm = await contactDetailTypeQuery.SingleOrDefaultAsync(p => p.Id == contactDetailTypeId, cancellationToken);
                if (contactDetailTypeVm == null)
                {
                    return ActionResponse<ContactDetailTypeVm>.Failed(new string[] { "Contact detail type not found in Db" });
                }

                return ActionResponse<ContactDetailTypeVm>.Success(contactDetailTypeVm);
            }
            catch (Exception ex)
            {
                return ActionResponse<ContactDetailTypeVm>.Failed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }

        public async Task<ActionResponse<UpdateContactDetailTypeVm>> GetContactDetailTypeForEditAsync(int contactDetailTypeId, CancellationToken cancellationToken)
        {
            if (contactDetailTypeId < 1)
            {
                return ActionResponse<UpdateContactDetailTypeVm>.Failed(new string[] { "Invalid contact detail type ID" });
            }

            try
            {
                var contactDetailTypeQuery = _contactRepository.GetContactDetailTypes().ProjectTo<UpdateContactDetailTypeVm>(_mapper.ConfigurationProvider);
                var contactDetailTypeVm = await contactDetailTypeQuery.SingleOrDefaultAsync(p => p.Id == contactDetailTypeId, cancellationToken);
                if (contactDetailTypeVm == null)
                {
                    return ActionResponse<UpdateContactDetailTypeVm>.Failed(new string[] { "Contact detail type not found in Db" });
                }

                return ActionResponse<UpdateContactDetailTypeVm>.Success(contactDetailTypeVm);
            }
            catch (Exception ex)
            {
                return ActionResponse<UpdateContactDetailTypeVm>.Failed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }

        public async Task<ActionResponse<ListContactDetailTypesForListVm>> GetContactDetailTypesForListAsync(CancellationToken cancellationToken)
        {
            try
            {
                var contactTypesQuery = _contactRepository.GetContactDetailTypes().ProjectTo<ContactDetailTypeForListVm>(_mapper.ConfigurationProvider);
                var contactTypes = await contactTypesQuery.ToListAsync(cancellationToken);

                ListContactDetailTypesForListVm result = new ListContactDetailTypesForListVm();
                result.ContactDetailTypes = contactTypes;
                result.Count = result.ContactDetailTypes.Count;

                return ActionResponse<ListContactDetailTypesForListVm>.Success(result);
            }
            catch (Exception ex)
            {
                return ActionResponse<ListContactDetailTypesForListVm>.Failed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }

        public async Task<ActionResponse<ContactDetailTypeForDeleteVm>> GetContactDetailTypeForDeleteAsync(int contactDetailTypeId, CancellationToken cancellationToken)
        {
            if (contactDetailTypeId < 1)
            {
                return ActionResponse<ContactDetailTypeForDeleteVm>.Failed(new string[] { "Invalid contact detail type ID" });
            }

            try
            {
                var contactDetailTypeQuery = _contactRepository.GetContactDetailTypeById(contactDetailTypeId).ProjectTo<ContactDetailTypeForDeleteVm>(_mapper.ConfigurationProvider);
                var contactDetailTypeVm = await contactDetailTypeQuery.SingleOrDefaultAsync(cancellationToken);
                if (contactDetailTypeVm == null)
                {
                    return ActionResponse<ContactDetailTypeForDeleteVm>.Failed(new string[] { "Contact detail type not found in Db" });
                }

                return ActionResponse<ContactDetailTypeForDeleteVm>.Success(contactDetailTypeVm);
            }
            catch (Exception ex)
            {
                return ActionResponse<ContactDetailTypeForDeleteVm>.Failed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }

        public async Task<ActionResponse<ContactDetailTypeVm>> UpdateContactDetailTypeAsync(UpdateContactDetailTypeVm updateContactDetailTypeVm, CancellationToken cancellationToken)
        {
            if (updateContactDetailTypeVm == null)
            {
                return ActionResponse<ContactDetailTypeVm>.Failed(new string[] { "Error occurred while processing the HTTP POST form" });
            }

            try
            {
                var validator = _validatorFactory.GetValidator<UpdateContactDetailTypeVm>();
                var validationResult = await validator.ValidateAsync(updateContactDetailTypeVm, cancellationToken);
                if (!validationResult.IsValid)
                {
                    return ActionResponse<ContactDetailTypeVm>.Failed(validationResult.Errors.Select(e => e.ErrorMessage), true);
                }

                var contactTypeDetail = _mapper.Map<ContactDetailType>(updateContactDetailTypeVm);
                var success = await _contactRepository.UpdateContactDetailTypeAsync(contactTypeDetail, cancellationToken);
                if (!success)
                {
                    return ActionResponse<ContactDetailTypeVm>.Failed(new string[] { "Failed to update contact detail type" });
                }

                var response = await GetContactDetailTypeAsync(contactTypeDetail.Id, cancellationToken);
                if (!response.IsSuccessful)
                {
                    return ActionResponse<ContactDetailTypeVm>.Failed(new string[] { "Contact detail type not found in DB after update " });
                }

                return ActionResponse<ContactDetailTypeVm>.Success(response.Data);
            }
            catch (Exception ex)
            {
                return ActionResponse<ContactDetailTypeVm>.Failed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }

        // TODO: Make manual test - Open. - Rename ? and call Contact not contact  but mapp obj into ContactDetails with includ ? ? 
        public async Task<ActionResponse<ContactVm>> GetContactAsync(int contactId, CancellationToken cancellationToken)
        {
            if (contactId < 1)
            {
                return ActionResponse<ContactVm>.Failed(new string[] { "Invalid contact detail type ID" });
            }

            try
            {
                var contactDetailsQuery = _contactRepository.GetContactById(contactId);
                var contactDetails = await contactDetailsQuery.ProjectTo<ContactVm>(_mapper.ConfigurationProvider).SingleOrDefaultAsync(cancellationToken);
                if (contactDetails == null)
                {
                    return ActionResponse<ContactVm>.Failed(new[] { "Contact not found" });
                }

                return ActionResponse<ContactVm>.Success(contactDetails);
            }
            catch (Exception ex)
            {
                return ActionResponse<ContactVm>.Failed(new[] { $"An error occurred: {ex.Message}" });
            }
        }

        public async Task<ActionResponse<UpdateContactVm>> GetContactForUpdateAsync(int contactId, CancellationToken cancellationToken)
        {
            if (contactId < 1)
            {
                return ActionResponse<UpdateContactVm>.Failed(new string[] { "Invalid contact ID" });
            }

            try
            {
                var contactQuery = _contactRepository.GetContactById(contactId);
                var contact = await contactQuery.ProjectTo<UpdateContactVm>(_mapper.ConfigurationProvider).SingleOrDefaultAsync(cancellationToken);
                if (contact == null)
                {
                    return ActionResponse<UpdateContactVm>.Failed(new[] { "Contact not found" });
                }

                return ActionResponse<UpdateContactVm>.Success(contact);

            }
            catch (Exception ex)
            {
                return ActionResponse<UpdateContactVm>.Failed(new[] { $"An error occurred: {ex.Message}" });
            }
        }

        public async Task<ActionResponse<ContactVm>> UpdateContactAsync(UpdateContactVm model, CancellationToken cancellationToken)
        {
            if (model == null)
            {
                return ActionResponse<ContactVm>.Failed(new string[] { "Error occurred while processing the HTTP POST form" });
            }

            try
            {
                var validator = _validatorFactory.GetValidator<UpdateContactVm>();
                var validationResult = await validator.ValidateAsync(model, cancellationToken);
                if (!validationResult.IsValid)
                {
                    return ActionResponse<ContactVm>.Failed(validationResult.Errors.Select(e => e.ErrorMessage), true);
                }

                var contact = _mapper.Map<Contact>(model);
                var isSuccess = await _contactRepository.UpdateContactAsync(contact, cancellationToken);
                if (!isSuccess)
                {
                    return ActionResponse<ContactVm>.Failed(new string[] { "Failed to update contact" });
                }

                var response = await GetContactAsync(contact.Id, cancellationToken);
                if (!response.IsSuccessful)
                {
                    return ActionResponse<ContactVm>.Failed(new string[] { "Contact found in DB after update" });
                }

                return ActionResponse<ContactVm>.Success(response.Data);
            }

            catch (Exception ex)
            {
                return ActionResponse<ContactVm>.Failed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }

        public async Task<ActionResponse<ContactDetailVm>> GetContactDetailAsync(int contactDetailId, CancellationToken cancellationToken)
        {
            if (contactDetailId < 1)
            {
                return ActionResponse<ContactDetailVm>.Failed(new string[] { "Invalid contact detail ID" });
            }

            try
            {
                var query = _contactRepository.GetContactDetailById(contactDetailId);
                var contactDetail = await query.ProjectTo<ContactDetailVm>(_mapper.ConfigurationProvider).SingleOrDefaultAsync(cancellationToken);
                if (contactDetail == null)
                {
                    return ActionResponse<ContactDetailVm>.Failed(new[] { "Contact not found" });
                }

                return ActionResponse<ContactDetailVm>.Success(contactDetail);

            }
            catch (Exception ex)
            {
                return ActionResponse<ContactDetailVm>.Failed(new[] { $"An error occurred: {ex.Message}" });
            }
        }

        public async Task<ActionResponse<VoidValue>> UpdateContactDetailAsync(UpdateContactDetailVm model, CancellationToken cancellationToken)
        {
            if (model == null)
            {
                return ActionResponse<VoidValue>.Failed(new string[] { "Error occurred while processing the HTTP POST form" });
            }

            try
            {
                var validator = _validatorFactory.GetValidator<UpdateContactDetailVm>();
                var validationResult = await validator.ValidateAsync(model, cancellationToken);
                if (!validationResult.IsValid)
                {
                    return ActionResponse<VoidValue>.Failed(validationResult.Errors.Select(e => e.ErrorMessage), true);
                }

                var contactDetail = _mapper.Map<ContactDetail>(model);
                await _contactRepository.UpdateContactDetailAsync(contactDetail, cancellationToken);
            }
            catch (Exception ex)
            {
                return ActionResponse<VoidValue>.Failed(new string[] { $"Error occurred -> {ex.Message}" }, false);
            }

            return ActionResponse<VoidValue>.Success(null);
        }

        public async Task<ActionResponse<VoidValue>> DeleteContactDetailAsync(int contactDetailId, CancellationToken cancellationToken)
        {
            if (contactDetailId < 1)
            {
                return ActionResponse<VoidValue>.Failed(new string[] { "Invalid contact detail ID" });
            }

            try
            {
                await _contactRepository.DeleteContactDetailAsync(contactDetailId, cancellationToken);
            }
            catch (Exception ex)
            {
                return ActionResponse<VoidValue>.Failed(new[] { $"An error occurred: {ex.Message}" });
            }

            return ActionResponse<VoidValue>.Success(null);
        }

        public async Task<ActionResponse<UpdateContactDetailVm>> GetContactDetailForUpdateAsync(int contactDetailId, CancellationToken cancellationToken)
        {
            // TODO: Check and if needed include ContactDetail Types for selectList

            // TODO: Refine ?
            if (contactDetailId < 1)
            {
                return ActionResponse<UpdateContactDetailVm>.Failed(new string[] { "Invalid contact detail ID" });
            }

            try
            {
                var query = _contactRepository.GetContactDetailById(contactDetailId);
                var contactDetail = await query.ProjectTo<UpdateContactDetailVm>(_mapper.ConfigurationProvider).SingleOrDefaultAsync(cancellationToken);
                var serviceResponse = await GetContactDetailTypesForListAsync(cancellationToken);
                contactDetail.ContactDetailTypes = serviceResponse.Data.ContactDetailTypes;

                return ActionResponse<UpdateContactDetailVm>.Success(contactDetail);
            }
            catch (Exception ex)
            {
                return ActionResponse<UpdateContactDetailVm>.Failed(new[] { $"An error occurred: {ex.Message}" });
            }
        }
        public async Task<ActionResponse<NewContactDetailVm>> AddContactDetailAsync(NewContactDetailVm model, CancellationToken cancellationToken)
        {
            if (model == null)
            {
                return ActionResponse<NewContactDetailVm>.Failed(new string[] { "Error occurred while processing the HTTP POST form" });
            }

            try
            {
                var validator = _validatorFactory.GetValidator<NewContactDetailVm>();
                var validationResult = await validator.ValidateAsync(model, cancellationToken);
                if (!validationResult.IsValid)
                {
                    return ActionResponse<NewContactDetailVm>.Failed(validationResult.Errors.Select(e => e.ErrorMessage), true);
                }
                // TODO: Get Answer => Add over root object, or create just new ContactDetail with FK to contact
                var contactDetail = _mapper.Map<NewContactDetailVm, ContactDetail>(model);
                var contactDetailId = await _contactRepository.AddContactDetailAsync(contactDetail, cancellationToken);
                return ActionResponse<NewContactDetailVm>.Success(null, contactDetailId);
            }
            catch (Exception ex)
            {
                return ActionResponse<NewContactDetailVm>.Failed(new[] { $"An error occurred: {ex.Message}" });
            }
        }

        private ContactDetailTypeSelectListVm GetContactTypesSelectList() => new ContactDetailTypeSelectListVm()
        {
            ContactDetailTypes = _contactRepository.GetContactDetailTypes().ProjectTo<ContactDetailTypeForSelectListVm>(_mapper.ConfigurationProvider)
        };

        public async Task<ActionResponse<NewContactDetailVm>> PrepareAddContactDetailVmAsync(int contactId, CancellationToken cancellationToken)
        {
            if (contactId < 1)
            {
                return ActionResponse<NewContactDetailVm>.Failed(new string[] { "Invalid contact ID" });
            }

            try
            {
                var model = new NewContactDetailVm
                {
                    ContactId = contactId,
                    ContactDetailTypeSelectList = GetContactTypesSelectList()
                };

                return ActionResponse<NewContactDetailVm>.Success(model);
            }
            catch (Exception ex)
            {
                return ActionResponse<NewContactDetailVm>.Failed(new[] { $"An error occurred: {ex.Message}" });
            }
        }

        public async Task<ActionResponse<NewContactVm>> PrepareAddContactVm(int contactOwnerId, CancellationToken cancellationToken)
        {
            var model = new NewContactVm
            {
                ContactOwnerId = contactOwnerId,
                ContactDetailVm = new NewContactDetailVm
                {
                    ContactDetailTypeSelectList = GetContactTypesForSelectList()
                },
            };

            return ActionResponse<NewContactVm>.Success(model);
        }

        private ContactDetailTypeSelectListVm GetContactTypesForSelectList() => new ContactDetailTypeSelectListVm()
        {
            ContactDetailTypes = _contactRepository.GetContactDetailTypes().ProjectTo<ContactDetailTypeForSelectListVm>(_mapper.ConfigurationProvider)
        };
    }
}
