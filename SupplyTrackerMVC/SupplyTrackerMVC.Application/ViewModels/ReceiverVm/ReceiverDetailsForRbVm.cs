using SupplyTrackerMVC.Application.Mapping;
using SupplyTrackerMVC.Domain.Model.Receivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.ViewModels.ReceiverVm
{
    public class ReceiverDetailsForRbVm : IMapFrom<Receiver>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
