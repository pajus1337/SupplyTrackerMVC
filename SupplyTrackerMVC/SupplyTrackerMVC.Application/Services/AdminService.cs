using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SupplyTrackerMVC.Application.Interfaces;
using SupplyTrackerMVC.Application.Responses;
using SupplyTrackerMVC.Application.ViewModels.Common;
using SupplyTrackerMVC.Domain.Interfaces;
using SupplyTrackerMVC.Domain.Model.Contacts;
using SupplyTrackerMVC.Domain.Model.Deliveries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.Services
{
    public class AdminService(IContactRepository contactRepository, IFluentValidatorFactory fluentValidatorFactory, IMapper mapper) : IAdminService
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

                return ServiceResponse<AddContactDetailTypeVm>.CreateSuccess(null,contactDetailTypeId);
            }
            catch (Exception ex)
            {
                return ServiceResponse<AddContactDetailTypeVm>.CreateFailed(new string[] { $"Error occurred -> {ex.Message}"}, false);
            }
        }

        public Task<ServiceResponse<VoidValue>> DeleteContactDetailTypeAsync(int contactTypeId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<ListContactDetailTypesForListVm>> GetContactDetailTypeForListAsync(CancellationToken cancellationToken)
        {
            var contactTypesQuery = _contactRepository.GetContactDetailTypes();

            try
            {
                var contactTypes = await contactTypesQuery.ToListAsync(cancellationToken);

                ListContactDetailTypesForListVm result = new ListContactDetailTypesForListVm();
                result.ContactDetailTypes = new List<ContactDetailTypeForListVm>();

                return ServiceResponse<ListContactDetailTypesForListVm>.CreateSuccess(result);
            }
            catch (Exception ex)
            {
                return ServiceResponse<ListContactDetailTypesForListVm>.CreateFailed(new string[] { $"Error occurred -> {ex.Message}" });
            }

        }

        public Task<ServiceResponse<VoidValue>> UpdateContactDetailTypeAsync(UpdateContactDetailTypeVm model, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
