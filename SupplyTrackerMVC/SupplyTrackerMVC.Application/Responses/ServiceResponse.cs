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
        public string? AdditionalMessage { get; private set; }
        public int? ObjectId { get; private set; }
        public bool IsValidationError { get; private set; }

        private ServiceResponse()
        {
        }

        public static ServiceResponse<T> CreateSuccess(T? data, int? objectId = null, string? additionalMessage = null ) => new ServiceResponse<T> 
        { 
            Success = true, 
            Data = data,
            ObjectId = objectId,
            AdditionalMessage = additionalMessage ?? string.Empty,
            IsValidationError = false,
        };

        public static ServiceResponse<T> CreateFailed(IEnumerable<string>? errorMessage, bool isValidationErrror = false) => new ServiceResponse<T> 
        { 
            Success = false, 
            ErrorMessage = errorMessage ?? Enumerable.Empty<string>(),
            IsValidationError = isValidationErrror,          
        };
    }
}
