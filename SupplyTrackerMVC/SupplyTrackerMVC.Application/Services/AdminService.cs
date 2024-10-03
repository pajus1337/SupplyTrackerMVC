using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SupplyTrackerMVC.Application.Interfaces;
using SupplyTrackerMVC.Application.Responses;
using SupplyTrackerMVC.Application.ViewModels.Common;
using SupplyTrackerMVC.Application.ViewModels.SenderVm;
using SupplyTrackerMVC.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.Services
{
    public class AdminService(IContactRepository contactRepository) : IAdminService
    {
        private readonly IContactRepository _contactRepository = contactRepository;

        public Task<ServiceResponse<AddContactDetailTypeVm>> AddContactDetailTypeAsync(AddContactDetailTypeVm model, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
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
