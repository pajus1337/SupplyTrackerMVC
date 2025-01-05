﻿using SupplyTrackerMVC.Domain.Model.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Domain.Interfaces
{
    public interface IContactRepository
    {
        Task<(int ContactId, bool Success)> AddContactAsync(Contact contact, CancellationToken cancellationToken);
        Task<(int ContactTypeId, bool Success)> AddContactDetailTypeAsync(ContactDetailType contactDetailType, CancellationToken cancellationToken);
        Task<bool> DeleteAddressAsync(int contactId, CancellationToken cancellationToken);
        Task<bool> UpdateAddressAsync(Contact contact, CancellationToken cancellationToken);
        IQueryable<Contact> GetContactById(int contactId);
        IQueryable<ContactDetailType> GetContactDetailTypes();
        IQueryable<ContactDetailType> GetContactDetailTypeById(int contactDetailTypeId);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<bool> UpdateContactDetailTypeAsync(ContactDetailType contactTypeDetail, CancellationToken cancellationToken);
    }
}
