using SupplyTrackerMVC.Application.Responses;
using SupplyTrackerMVC.Application.ViewModels.Common;
using SupplyTrackerMVC.Domain.Model.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.Interfaces
{
    public interface IContactService
    {
        Task<ActionResponse<NewContactDetailTypeVm>> AddContactDetailTypeAsync(NewContactDetailTypeVm model, CancellationToken cancellationToken);
        Task<ActionResponse<NewContactDetailVm>> AddContactDetailAsync(NewContactDetailVm model, CancellationToken cancellationToken);
        Task<ActionResponse<ContactDetailTypeVm>> UpdateContactDetailTypeAsync(UpdateContactDetailTypeVm model, CancellationToken cancellationToken);
        Task<ActionResponse<VoidValue>> DeleteContactDetailTypeAsync(int contactTypeId, CancellationToken cancellationToken);
        Task<ActionResponse<ListContactDetailTypesForListVm>> GetContactDetailTypesForListAsync(CancellationToken cancellationToken);
        Task<ActionResponse<ContactDetailTypeVm>> GetContactDetailTypeAsync(int contactDetailTypeId, CancellationToken cancellationToken);
        Task<ActionResponse<UpdateContactDetailTypeVm>> GetContactDetailTypeForEditAsync(int contactDetailTypeId, CancellationToken cancellationToken);
        Task<ActionResponse<ContactDetailTypeForDeleteVm>> GetContactDetailTypeForDeleteAsync(int contactDetailTypeId, CancellationToken cancellationToken);
        Task<ActionResponse<ContactVm>> GetContactAsync(int contactId, CancellationToken cancellationToken);
        Task<ActionResponse<UpdateContactVm>> GetContactForUpdateAsync(int contactId, CancellationToken cancellationToken);
        Task<ActionResponse<ContactVm>> UpdateContactAsync(UpdateContactVm model, CancellationToken cancellationToken);
        Task<ActionResponse<ContactDetailVm>> GetContactDetailAsync(int contactDetailId, CancellationToken cancellationToken);
        Task<ActionResponse<UpdateContactDetailVm>> GetContactDetailForUpdateAsync(int contactDetailId, CancellationToken cancellationToken);
        Task<ActionResponse<VoidValue>> UpdateContactDetailAsync(UpdateContactDetailVm model, CancellationToken cancellationToken);
        Task<ActionResponse<VoidValue>> DeleteContactDetailAsync(int contactDetailId, CancellationToken cancellationToken);
        Task<ActionResponse<NewContactDetailVm>> PrepareAddContactDetailVmAsync(int contactId, CancellationToken cancellationToken);
        Task<ActionResponse<NewContactVm>> PrepareAddContactVm(int contactOwnerId, CancellationToken cancellationToken);
    }
}
