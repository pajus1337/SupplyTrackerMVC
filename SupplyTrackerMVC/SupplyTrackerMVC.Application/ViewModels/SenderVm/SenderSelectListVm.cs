﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.ViewModels.SenderVm
{
    public class SenderSelectListVm
    {
        public IEnumerable<SenderForSelectListVm> Senders { get; set; }
    }
}
