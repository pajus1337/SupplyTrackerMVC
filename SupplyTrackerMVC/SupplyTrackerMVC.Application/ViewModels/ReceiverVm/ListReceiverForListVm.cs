

namespace SupplyTrackerMVC.Application.ViewModels.ReceiverVm
{
    public class ListReceiverForListVm
    {
        public List<ReceiverForListVm> Receivers { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
        public int Count { get; set; }
    }
}
