using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.ViewModels.StatisticsVm
{
    public class StatisticsDataVm
    {
        public int TotalDeliveries { get; set; }
        public int DeliveriesThisWeek { get; set; }
        public string MostFrequentProduct { get; set; }
        public int TotalProducts { get; set; }
        public int TotalSenders { get; set; }
        public int TotalReceivers { get; set; }
        public int TotalReceiverBranches { get; set; }
    }
}
