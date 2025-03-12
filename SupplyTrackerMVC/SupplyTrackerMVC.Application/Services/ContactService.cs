﻿using AutoMapper;
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

        public async Task<ServiceResponse<NewContactDetailTypeVm>> AddContactDetailTypeAsync(NewContactDetailTypeVm model, CancellationToken cancellationToken)
        {
            if (model == null)
            {
                return ServiceResponse<NewContactDetailTypeVm>.CreateFailed(new string[] { "Error occurred while processing the HTTP POST form" });
            }

            try
            {
                var validator = _validatorFactory.GetValidator<NewContactDetailTypeVm>();
                var validationResult = await validator.ValidateAsync(model, cancellationToken);
                if (!validationResult.IsValid)
                {
                    return ServiceResponse<NewContactDetailTypeVm>.CreateFailed(validationResult.Errors.Select(e => e.ErrorMessage), true);
                }

                var contactDetailType = _mapper.Map<ContactDetailType>(model);
                var contactDetailTypeId = await _contactRepository.AddContactDetailTypeAsync(contactDetailType, cancellationToken);

                return ServiceResponse<NewContactDetailTypeVm>.CreateSuccess(null, contactDetailTypeId);
            }
            catch (Exception ex)
            {
                return ServiceResponse<NewContactDetailTypeVm>.CreateFailed(new string[] { $"Error occurred -> {ex.Message}" }, false);
            }
        }

        public async Task<ServiceResponse<VoidValue>> DeleteContactDetailTypeAsync(int contactTypeId, CancellationToken cancellationToken)
        {
            var success = await _contactRepository.DeleteContactDetailTypeAsync(contactTypeId, cancellationToken);

            if (!success)
            {
                // TODO: Complete the implementation of whole function
            }

            return ServiceResponse<VoidValue>.CreateSuccess(new VoidValue(), null, null);
        }

        public async Task<ServiceResponse<ContactDetailTypeVm>> GetContactDetailTypeAsync(int contactDetailTypeId, CancellationToken cancellationToken)
        {
            if (contactDetailTypeId < 1)
            {
                return ServiceResponse<ContactDetailTypeVm>.CreateFailed(new string[] { "Invalid contact detail type ID" });
            }

            try
            {
                var contactDetailTypeQuery = _contactRepository.GetContactDetailTypeById(contactDetailTypeId).ProjectTo<ContactDetailTypeVm>(_mapper.ConfigurationProvider);
                var contactDetailTypeVm = await contactDetailTypeQuery.SingleOrDefaultAsync(p => p.Id == contactDetailTypeId, cancellationToken);
                if (contactDetailTypeVm == null)
                {
                    return ServiceResponse<ContactDetailTypeVm>.CreateFailed(new string[] { "Contact detail type not found in Db" });
                }

                return ServiceResponse<ContactDetailTypeVm>.CreateSuccess(contactDetailTypeVm);
            }
            catch (Exception ex)
            {
                return ServiceResponse<ContactDetailTypeVm>.CreateFailed(new string[] { $"Error occurred -> {ex.Message}" });
            }

        }

        public async Task<ServiceResponse<UpdateContactDetailTypeVm>> GetContactDetailTypeForEditAsync(int contactDetailTypeId, CancellationToken cancellationToken)
        {
            if (contactDetailTypeId < 1)
            {
                return ServiceResponse<UpdateContactDetailTypeVm>.CreateFailed(new string[] { "Invalid contact detail type ID" });
            }

            try
            {
                var contactDetailTypeQuery = _contactRepository.GetContactDetailTypes().ProjectTo<UpdateContactDetailTypeVm>(_mapper.ConfigurationProvider);
                var contactDetailTypeVm = await contactDetailTypeQuery.SingleOrDefaultAsync(p => p.Id == contactDetailTypeId, cancellationToken);
                if (contactDetailTypeVm == null)
                {
                    return ServiceResponse<UpdateContactDetailTypeVm>.CreateFailed(new string[] { "Contact detail type not found in Db" });
                }

                return ServiceResponse<UpdateContactDetailTypeVm>.CreateSuccess(contactDetailTypeVm);
            }
            catch (Exception ex)
            {
                return ServiceResponse<UpdateContactDetailTypeVm>.CreateFailed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }

        public async Task<ServiceResponse<ListContactDetailTypesForListVm>> GetContactDetailTypesForListAsync(CancellationToken cancellationToken)
        {
            try
            {
                var contactTypesQuery = _contactRepository.GetContactDetailTypes().ProjectTo<ContactDetailTypeForListVm>(_mapper.ConfigurationProvider);
                var contactTypes = await contactTypesQuery.ToListAsync(cancellationToken);

                ListContactDetailTypesForListVm result = new ListContactDetailTypesForListVm();
                result.ContactDetailTypes = contactTypes;
                result.Count = result.ContactDetailTypes.Count;

                return ServiceResponse<ListContactDetailTypesForListVm>.CreateSuccess(result);
            }
            catch (Exception ex)
            {
                return ServiceResponse<ListContactDetailTypesForListVm>.CreateFailed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }

        public async Task<ServiceResponse<ContactDetailTypeForDeleteVm>> GetContactDetailTypeForDeleteAsync(int contactDetailTypeId, CancellationToken cancellationToken)
        {
            if (contactDetailTypeId < 1)
            {
                return ServiceResponse<ContactDetailTypeForDeleteVm>.CreateFailed(new string[] { "Invalid contact detail type ID" });
            }

            try
            {
                var contactDetailTypeQuery = _contactRepository.GetContactDetailTypeById(contactDetailTypeId).ProjectTo<ContactDetailTypeForDeleteVm>(_mapper.ConfigurationProvider);
                var contactDetailTypeVm = await contactDetailTypeQuery.SingleOrDefaultAsync(cancellationToken);
                if (contactDetailTypeVm == null)
                {
                    return ServiceResponse<ContactDetailTypeForDeleteVm>.CreateFailed(new string[] { "Contact detail type not found in Db" });
                }

                return ServiceResponse<ContactDetailTypeForDeleteVm>.CreateSuccess(contactDetailTypeVm);
            }
            catch (Exception ex)
            {
                return ServiceResponse<ContactDetailTypeForDeleteVm>.CreateFailed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }

        public async Task<ServiceResponse<ContactDetailTypeVm>> UpdateContactDetailTypeAsync(UpdateContactDetailTypeVm updateContactDetailTypeVm, CancellationToken cancellationToken)
        {
            if (updateContactDetailTypeVm == null)
            {
                return ServiceResponse<ContactDetailTypeVm>.CreateFailed(new string[] { "Error occurred while processing the HTTP POST form" });
            }

            try
            {
                var validator = _validatorFactory.GetValidator<UpdateContactDetailTypeVm>();
                var validationResult = await validator.ValidateAsync(updateContactDetailTypeVm, cancellationToken);
                if (!validationResult.IsValid)
                {
                    return ServiceResponse<ContactDetailTypeVm>.CreateFailed(validationResult.Errors.Select(e => e.ErrorMessage), true);
                }

                var contactTypeDetail = _mapper.Map<ContactDetailType>(updateContactDetailTypeVm);
                var success = await _contactRepository.UpdateContactDetailTypeAsync(contactTypeDetail, cancellationToken);
                if (!success)
                {
                    return ServiceResponse<ContactDetailTypeVm>.CreateFailed(new string[] { "Failed to update contact detail type" });
                }

                var response = await GetContactDetailTypeAsync(contactTypeDetail.Id, cancellationToken);
                if (!response.Success)
                {
                    return ServiceResponse<ContactDetailTypeVm>.CreateFailed(new string[] { "Contact detail type not found in DB after update " });
                }

                return ServiceResponse<ContactDetailTypeVm>.CreateSuccess(response.Data);
            }
            catch (Exception ex)
            {
                return ServiceResponse<ContactDetailTypeVm>.CreateFailed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }

        // TODO: Make manual test - Open. - Rename ? and call Contact not contact  but mapp obj into ContactDetails with includ ? ? 
        public async Task<ServiceResponse<ContactVm>> GetContactAsync(int contactId, CancellationToken cancellationToken)
        {
            if (contactId < 1)
            {
                return ServiceResponse<ContactVm>.CreateFailed(new string[] { "Invalid contact detail type ID" });
            }

            try
            {
                var contactDetailsQuery = _contactRepository.GetContactById(contactId);
                var contactDetails = await contactDetailsQuery.ProjectTo<ContactVm>(_mapper.ConfigurationProvider).SingleOrDefaultAsync(cancellationToken);
                if (contactDetails == null)
                {
                    return ServiceResponse<ContactVm>.CreateFailed(new[] { "Contact not found" });
                }

                return ServiceResponse<ContactVm>.CreateSuccess(contactDetails);
            }
            catch (Exception ex)
            {
                return ServiceResponse<ContactVm>.CreateFailed(new[] { $"An error occurred: {ex.Message}" });
            }
        }

        public async Task<ServiceResponse<UpdateContactVm>> GetContactForUpdateAsync(int contactId, CancellationToken cancellationToken)
        {
            if (contactId < 1)
            {
                return ServiceResponse<UpdateContactVm>.CreateFailed(new string[] { "Invalid contact ID" });
            }

            try
            {
                var contactQuery = _contactRepository.GetContactById(contactId);
                var contact = await contactQuery.ProjectTo<UpdateContactVm>(_mapper.ConfigurationProvider).SingleOrDefaultAsync(cancellationToken);
                if (contact == null)
                {
                    return ServiceResponse<UpdateContactVm>.CreateFailed(new[] { "Contact not found" });
                }

                return ServiceResponse<UpdateContactVm>.CreateSuccess(contact);

            }
            catch (Exception ex)
            {
                return ServiceResponse<UpdateContactVm>.CreateFailed(new[] { $"An error occurred: {ex.Message}" });
            }
        }

        public async Task<ServiceResponse<ContactVm>> UpdateContactAsync(UpdateContactVm model, CancellationToken cancellationToken)
        {
            if (model == null)
            {
                return ServiceResponse<ContactVm>.CreateFailed(new string[] { "Error occurred while processing the HTTP POST form" });
            }

            try
            {
                var validator = _validatorFactory.GetValidator<UpdateContactVm>();
                var validationResult = await validator.ValidateAsync(model, cancellationToken);
                if (!validationResult.IsValid)
                {
                    return ServiceResponse<ContactVm>.CreateFailed(validationResult.Errors.Select(e => e.ErrorMessage), true);
                }

                var contact = _mapper.Map<Contact>(model);
                var isSuccess = await _contactRepository.UpdateContactAsync(contact, cancellationToken);
                if (!isSuccess)
                {
                    return ServiceResponse<ContactVm>.CreateFailed(new string[] { "Failed to update contact" });
                }

                var response = await GetContactAsync(contact.Id, cancellationToken);
                if (!response.Success)
                {
                    return ServiceResponse<ContactVm>.CreateFailed(new string[] { "Contact found in DB after update" });
                }

                return ServiceResponse<ContactVm>.CreateSuccess(response.Data);
            }

            catch (Exception ex)
            {
                return ServiceResponse<ContactVm>.CreateFailed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }

        public async Task<ServiceResponse<ContactDetailVm>> GetContactDetailAsync(int contactDetailId, CancellationToken cancellationToken)
        {
            if (contactDetailId < 1)
            {
                return ServiceResponse<ContactDetailVm>.CreateFailed(new string[] { "Invalid contact detail ID" });
            }

            try
            {
                var query = _contactRepository.GetContactDetailById(contactDetailId);
                var contactDetail = await query.ProjectTo<ContactDetailVm>(_mapper.ConfigurationProvider).SingleOrDefaultAsync(cancellationToken);
                if (contactDetail == null)
                {
                    return ServiceResponse<ContactDetailVm>.CreateFailed(new[] { "Contact not found" });
                }

                return ServiceResponse<ContactDetailVm>.CreateSuccess(contactDetail);

            }
            catch (Exception ex)
            {
                return ServiceResponse<ContactDetailVm>.CreateFailed(new[] { $"An error occurred: {ex.Message}" });
            }
        }

        public async Task<ServiceResponse<VoidValue>> UpdateContactDetailAsync(UpdateContactDetailVm model, CancellationToken cancellationToken)
        {
            if (model == null)
            {
                return ServiceResponse<VoidValue>.CreateFailed(new string[] { "Error occurred while processing the HTTP POST form" });
            }

            try
            {
                var validator = _validatorFactory.GetValidator<UpdateContactDetailVm>();
                var validationResult = await validator.ValidateAsync(model, cancellationToken);
                if (!validationResult.IsValid)
                {
                    return ServiceResponse<VoidValue>.CreateFailed(validationResult.Errors.Select(e => e.ErrorMessage), true);
                }

                var contactDetail = _mapper.Map<ContactDetail>(model);
                await _contactRepository.UpdateContactDetailAsync(contactDetail, cancellationToken);
            }
            catch (Exception ex)
            {
                return ServiceResponse<VoidValue>.CreateFailed(new string[] { $"Error occurred -> {ex.Message}" }, false);
            }

            return ServiceResponse<VoidValue>.CreateSuccess(null);
        }

        public async Task<ServiceResponse<VoidValue>> DeleteContactDetailAsync(int contactDetailId, CancellationToken cancellationToken)
        {
            if (contactDetailId < 1)
            {
                return ServiceResponse<VoidValue>.CreateFailed(new string[] { "Invalid contact detail ID" });
            }

            try
            {
                await _contactRepository.DeleteContactDetailAsync(contactDetailId, cancellationToken);
            }
            catch (Exception ex)
            {
                return ServiceResponse<VoidValue>.CreateFailed(new[] { $"An error occurred: {ex.Message}" });
            }

            return ServiceResponse<VoidValue>.CreateSuccess(null);
        }

        public async Task<ServiceResponse<UpdateContactDetailVm>> GetContactDetailForUpdateAsync(int contactDetailId, CancellationToken cancellationToken)
        {
            // TODO: Check and if needed include ContactDetail Types for selectList

            // TODO: Refine ?
            if (contactDetailId < 1)
            {
                return ServiceResponse<UpdateContactDetailVm>.CreateFailed(new string[] { "Invalid contact detail ID" });
            }

            try
            {
                var query = _contactRepository.GetContactDetailById(contactDetailId);
                var contactDetail = await query.ProjectTo<UpdateContactDetailVm>(_mapper.ConfigurationProvider).SingleOrDefaultAsync(cancellationToken);
                var serviceResponse = await GetContactDetailTypesForListAsync(cancellationToken);
                contactDetail.ContactDetailTypes = serviceResponse.Data.ContactDetailTypes;

                return ServiceResponse<UpdateContactDetailVm>.CreateSuccess(contactDetail);
            }
            catch (Exception ex)
            {
                return ServiceResponse<UpdateContactDetailVm>.CreateFailed(new[] { $"An error occurred: {ex.Message}" });
            }
        }

        public async Task<ServiceResponse<NewContactDetailVm>> AddContactDetailAsync(NewContactDetailVm model, CancellationToken cancellationToken)
        {
            if (model == null)
            {
                return ServiceResponse<NewContactDetailVm>.CreateFailed(new string[] { "Error occurred while processing the HTTP POST form" });
            }

            try
            {
                var validator = _validatorFactory.GetValidator<NewContactDetailVm>();
                var validationResult = await validator.ValidateAsync(model, cancellationToken);
                if (!validationResult.IsValid)
                {
                    return ServiceResponse<NewContactDetailVm>.CreateFailed(validationResult.Errors.Select(e => e.ErrorMessage), true);
                }
                // TODO: Get Answer => Add over root object, or create just new ContactDetail with FK to contact
                var contactDetail = _mapper.Map<NewContactDetailVm, ContactDetail>(model);
                var contactDetailId = await _contactRepository.AddContactDetailAsync(contactDetail, cancellationToken);
                return ServiceResponse<NewContactDetailVm>.CreateSuccess(null, contactDetailId);
            }
            catch (Exception ex)
            {
                return ServiceResponse<NewContactDetailVm>.CreateFailed(new[] { $"An error occurred: {ex.Message}" });
            }
        }

        private ContactDetailTypeSelectListVm GetContactTypesSelectList() => new ContactDetailTypeSelectListVm()
        {
            ContactDetailTypes = _contactRepository.GetContactDetailTypes().ProjectTo<ContactDetailTypeForSelectListVm>(_mapper.ConfigurationProvider)
        };


        public async Task<ServiceResponse<NewContactDetailVm>> PrepareAddContactDetailVmAsync(int contactId, CancellationToken cancellationToken)
        {
            if (contactId < 1)
            {
                return ServiceResponse<NewContactDetailVm>.CreateFailed(new string[] { "Invalid contact ID" });
            }

            try
            {
                var model = new NewContactDetailVm
                {
                    ContactId = contactId,
                    ContactDetailTypeSelectList = GetContactTypesSelectList()
                };

                return ServiceResponse<NewContactDetailVm>.CreateSuccess(model);
            }
            catch (Exception ex)
            {
                return ServiceResponse<NewContactDetailVm>.CreateFailed(new[] { $"An error occurred: {ex.Message}" });
            }
        }


        public async Task<ServiceResponse<NewContactVm>> PrepareAddContactVm(int contactOwnerId, CancellationToken cancellationToken)
        {
            var model = new NewContactVm
            {
                ContactOwnerId = contactOwnerId,
                ContactDetailVm = new NewContactDetailVm
                {
                    ContactDetailTypeSelectList = GetContactTypesForSelectList()
                },
            };

            return ServiceResponse<NewContactVm>.CreateSuccess(model);
        }

        
        private ContactDetailTypeSelectListVm GetContactTypesForSelectList() => new ContactDetailTypeSelectListVm()
        {
            ContactDetailTypes = _contactRepository.GetContactDetailTypes().ProjectTo<ContactDetailTypeForSelectListVm>(_mapper.ConfigurationProvider)
        };
    }
}
