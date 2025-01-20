using SupplyTrackerMVC.Application.Responses;
using SupplyTrackerMVC.Application.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.Interfaces
{
    public interface IContactService
    {
        Task<ServiceResponse<AddContactDetailTypeVm>> AddContactDetailTypeAsync(AddContactDetailTypeVm model, CancellationToken cancellationToken);
        Task<ServiceResponse<ContactDetailTypeVm>> UpdateContactDetailTypeAsync(UpdateContactDetailTypeVm model, CancellationToken cancellationToken);
        Task<ServiceResponse<VoidValue>> DeleteContactDetailTypeAsync(int contactTypeId, CancellationToken cancellationToken);
        Task<ServiceResponse<ListContactDetailTypesForListVm>> GetContactDetailTypesForListAsync(CancellationToken cancellationToken);
        Task<ServiceResponse<ContactDetailTypeVm>> GetContactDetailTypeAsync(int contactDetailTypeId, CancellationToken cancellationToken);
        Task<ServiceResponse<UpdateContactDetailTypeVm>> GetContactDetailTypeForEditAsync(int contactDetailTypeId, CancellationToken cancellationToken);
        Task<ServiceResponse<ContactDetailTypeForDeleteVm>> GetContactDetailTypeForDeleteAsync(int contactDetailTypeId, CancellationToken cancellationToken);
        Task<ServiceResponse<ContactVm>> GetContactAsync(int contactId, CancellationToken cancellationToken);
        Task<ServiceResponse<UpdateContactVm>> GetContactForUpdateAsync(int contactId, CancellationToken cancellationToken);
        Task<ServiceResponse<ContactVm>> UpdateContactAsync(UpdateContactVm model, CancellationToken cancellationToken);
        Task<ServiceResponse<VoidValue>> GetContactDetailAsync(int contactDetailId, CancellationToken cancellationToken);
        Task<ServiceResponse<VoidValue>> UpdateContactDetailAsync(UpdateContactDetailVm model, CancellationToken cancellationToken);
        Task<ServiceResponse<VoidValue>> DeleteContactDetailAsync(int contactDetailId, CancellationToken cancellationToken);
    }
}
