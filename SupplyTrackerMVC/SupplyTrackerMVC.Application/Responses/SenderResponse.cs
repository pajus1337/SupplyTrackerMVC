using SupplyTrackerMVC.Application.ViewModels.SenderVm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.Responses
{
    public class SenderResponse<T>
    {
        public bool Success { get; private set; }
        public IEnumerable<string> ErrorMessage { get; private set; }
        public T ViewModel { get; private set; }

        private SenderResponse()
        {
        }

        public static SenderResponse<T> CreateSuccess(T viewModel) => new SenderResponse<T> 
        { 
            Success = true, 
            ViewModel = viewModel 
        };

        public static SenderResponse<T> CreateFail(IEnumerable<string> errorMessage) => new SenderResponse<T> 
        { 
            Success = false, 
            ErrorMessage = errorMessage ?? Enumerable.Empty<string>() 
        };
    }
}
