﻿using SupplyTrackerMVC.Application.ViewModels.ReceiverVm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.ViewModels.ProductVm
{
    public class ListProductForList
    {
        public List<ReceiverForListVm> Receivers { get; set; }
        public int Count { get; set; }
    }
}
