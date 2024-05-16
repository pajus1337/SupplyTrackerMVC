using SupplyTrackerMVC.Application.ViewModels.SenderVm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.Responses
{
    public class ServiceResponse<T>
    {
        public bool Success { get; private set; }
        public IEnumerable<string>? ErrorMessage { get; private set; }
        public T? Data { get; private set; }
        public string? AdditionalMessage { get; set; }
        public int? ObjectId { get; private set; }

        private ServiceResponse()
        {
        }

        public static ServiceResponse<T> CreateSuccess(T? data, int? senderId = null, string? additionalMessage = null ) => new ServiceResponse<T> 
        { 
            Success = true, 
            Data = data,
            ObjectId = senderId,
            AdditionalMessage = additionalMessage ?? string.Empty,
        };

        public static ServiceResponse<T> CreateFailed(IEnumerable<string>? errorMessage) => new ServiceResponse<T> 
        { 
            Success = false, 
            ErrorMessage = errorMessage ?? Enumerable.Empty<string>() 
        };
    }
}
