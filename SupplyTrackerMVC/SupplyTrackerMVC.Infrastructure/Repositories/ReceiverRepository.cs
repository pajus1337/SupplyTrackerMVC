﻿using SupplyTrackerMVC.Domain.Interfaces;
using SupplyTrackerMVC.Domain.Model.Receivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Infrastructure.Repositories
{
    public class ReceiverRepository : IReceiverRepository
    {
        private readonly Context _context;

        public ReceiverRepository(Context context)
        {
            _context = context;
        }

        public int AddReceiver(Receiver receiver)
        {
            _context.Add(receiver);
            return receiver.Id;
        }

        public void DeleteReceiver(int receiverId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ReceiverBranch> GetAllActiveReceiverBranches(int receiverId)
        {
            var activeBranches = _context.DeliveryBranches.Where(b => b.ReceiverId == receiverId && b.isActive);
            return activeBranches;
        }

        public IQueryable<ReceiverBranch> GetAllActiveReceiverBranches()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Receiver> GetAllActiveReceivers()
        {
            var allActiveReceiver = _context.Receivers.Where(p => p.isActive);
            return allActiveReceiver;
        }

        public Receiver GetReceiverById(int receiverId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void UpdateReceiver()
        {
            throw new NotImplementedException();
        }
    }
}
