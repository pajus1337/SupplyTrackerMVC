﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.ViewModels.SenderVm
{
    public class ListSenderForListVm
    {
        public List<SenderForListVm>? Senders { get; set; }
        public int Count { get; set; }
    }
}
