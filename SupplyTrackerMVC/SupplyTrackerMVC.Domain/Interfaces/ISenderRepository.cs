﻿using SupplyTrackerMVC.Domain.Model.Senders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Domain.Interfaces
{
    public interface ISenderRepository
    {
        Task<(int SenderId, bool Success)> AddSenderAsync(Sender sender, CancellationToken cancellationToken);
        Task<bool> UpdateSenderAsync(Sender sender, CancellationToken cancellationToken);
        Task<(bool Success, string? Error)> DeleteSenderAsync(int senderId, CancellationToken cancellationToken);
        IQueryable<Sender> GetSenderById(int senderId);
        IQueryable<Sender> GetAllSenders();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
