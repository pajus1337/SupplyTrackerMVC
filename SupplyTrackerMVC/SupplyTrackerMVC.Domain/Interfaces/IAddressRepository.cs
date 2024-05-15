using SupplyTrackerMVC.Domain.Model.Addresses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Domain.Interfaces
{
    public interface IAddressRepository
    {
        Task<bool> AddAddressAsync(Address address, CancellationToken cancellationToken);
        Task<bool> UpdateAddressAsync(int addressId, CancellationToken cancellationToken);
        Task<bool> DeleteAddressAsync(int addressId, CancellationToken cancellationToken);
        IQueryable<Address> GetAddressById(int addressId);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
