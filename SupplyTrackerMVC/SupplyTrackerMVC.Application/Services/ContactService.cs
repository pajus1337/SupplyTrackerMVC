using AutoMapper;
using AutoMapper.QueryableExtensions;
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

        public async Task<ServiceResponse<AddContactDetailTypeVm>> AddContactDetailTypeAsync(AddContactDetailTypeVm model, CancellationToken cancellationToken)
        {
            if (model == null)
            {
                return ServiceResponse<AddContactDetailTypeVm>.CreateFailed(new string[] { "Error occurred while processing the HTTP POST form" });
            }

            var validator = _validatorFactory.GetValidator<AddContactDetailTypeVm>();
            var result = await validator.ValidateAsync(model, cancellationToken);
            if (!result.IsValid)
            {
                return ServiceResponse<AddContactDetailTypeVm>.CreateFailed(result.Errors.Select(e => e.ErrorMessage), true);
            }

            var NewContactDetailType = _mapper.Map<ContactDetailType>(model);
            try
            {
                var (contactDetailTypeId, isSuccess) = await _contactRepository.AddContactDetailTypeAsync(NewContactDetailType, cancellationToken);

                if (!isSuccess)
                {
                    return ServiceResponse<AddContactDetailTypeVm>.CreateFailed(new string[] { "Failed to add new delivery" });
                }

                return ServiceResponse<AddContactDetailTypeVm>.CreateSuccess(null, contactDetailTypeId);
            }
            catch (Exception ex)
            {
                return ServiceResponse<AddContactDetailTypeVm>.CreateFailed(new string[] { $"Error occurred -> {ex.Message}" }, false);
            }
        }

        public async Task<ServiceResponse<VoidValue>> DeleteContactDetailTypeAsync(int contactTypeId, CancellationToken cancellationToken)
        {
            var success = await _contactRepository.DeleteContactDetailTypeAsync(contactTypeId, cancellationToken);
            if (!success)
            {
                // TODO: Complete the implementation of whole function
            }

            return ServiceResponse<VoidValue>.CreateSuccess(new VoidValue(),null,null);
        }

        public async Task<ServiceResponse<ContactDetailTypeVm>> GetContactDetailTypeAsync(int contactDetailTypeId, CancellationToken cancellationToken)
        {
            if (contactDetailTypeId <= 0)
            {
                return ServiceResponse<ContactDetailTypeVm>.CreateFailed(new string[] { "Invalid contact detail type ID" });
            }

            var contactDetailTypeQuery = _contactRepository.GetContactDetailTypeById(contactDetailTypeId).ProjectTo<ContactDetailTypeVm>(_mapper.ConfigurationProvider);
            try
            {
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
            if (contactDetailTypeId <= 0)
            {
                return ServiceResponse<UpdateContactDetailTypeVm>.CreateFailed(new string[] { "Invalid contact detail type ID" });
            }

            var contactDetailTypeQuery = _contactRepository.GetContactDetailTypes().ProjectTo<UpdateContactDetailTypeVm>(_mapper.ConfigurationProvider);
            try
            {
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
            var contactTypesQuery = _contactRepository.GetContactDetailTypes().ProjectTo<ContactDetailTypeForListVm>(_mapper.ConfigurationProvider);

            try
            {
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


        // TODO: Create Implementation.
        public async Task<ServiceResponse<ContactDetailTypeForDeleteVm>> GetContactDetailTypeForDeleteAsync(int contactDetailTypeId, CancellationToken cancellationToken)
        {

            if (contactDetailTypeId <= 0)
            {
                return ServiceResponse<ContactDetailTypeForDeleteVm>.CreateFailed(new string[] { "Invalid contact detail type ID" });
            }

            var contactDetailTypeQuery = _contactRepository.GetContactDetailTypeById(contactDetailTypeId).ProjectTo<ContactDetailTypeForDeleteVm>(_mapper.ConfigurationProvider);
            try
            {
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

        // TODO: Make manual test - Open.
        public async Task<ServiceResponse<ContactDetailsVm>> GetContactDetailsAsync(int contactId, CancellationToken cancellationToken)
        {
            if (contactId <= 0)
            {
                return ServiceResponse<ContactDetailsVm>.CreateFailed(new string[] { "Invalid contact detail type ID" });
            }

            try
            {
                var contactDetailsQuery = _contactRepository.GetContactDetailsById(contactId);
                var contactDetails = await contactDetailsQuery.ProjectTo<ContactDetailsVm>(_mapper.ConfigurationProvider).SingleOrDefaultAsync(cancellationToken);
                
                if (contactDetails == null)
                {
                    return ServiceResponse<ContactDetailsVm>.CreateFailed(new[] { "Contact not found" });
                }

                return ServiceResponse<ContactDetailsVm>.CreateSuccess(contactDetails);

            }
            catch (Exception ex)
            {
                return ServiceResponse<ContactDetailsVm>.CreateFailed(new[] { $"An error occurred: {ex.Message}" });
            }
        }
    }
}
