using SupplyTrackerMVC.Domain.Interfaces;
using SupplyTrackerMVC.Domain.Model.Addresses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Infrastructure.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        public Task<bool> AddAddressAsync(Address address, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAddressAsync(int addressId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAddressAsync(int addressId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Address> GetAddressById(int addressId)
        {
            throw new NotImplementedException();
        }
    }
}
