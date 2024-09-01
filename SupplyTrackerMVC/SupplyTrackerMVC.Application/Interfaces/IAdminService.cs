using SupplyTrackerMVC.Application.Responses;
using SupplyTrackerMVC.Application.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.Interfaces
{
    public interface IAdminService
    {
        Task<ServiceResponse<VoidValue>> AddNewContactDetailTypeAsync(AddContactDetailTypeVm model, CancellationToken cancellationToken);
    }

}