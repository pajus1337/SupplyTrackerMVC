using SupplyTrackerMVC.Application.ViewModels.SenderVm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.Responses
{
    public class ActionResponse<T>
    {
        public T? Data { get; private set; }
        public bool IsSuccessful { get; private set; }
        public IEnumerable<string>? ErrorMessage { get; private set; }
        public string? AdditionalMessage { get; private set; }
        public int? ObjectId { get; private set; }
        public bool IsValidationError { get; private set; }

        private ActionResponse()
        {
        }

        public static ActionResponse<T> Success(T? data, int? objectId = null, string? additionalMessage = null ) => new ActionResponse<T> 
        { 
            IsSuccessful = true, 
            Data = data,
            ObjectId = objectId,
            AdditionalMessage = additionalMessage ?? string.Empty,
            IsValidationError = false,
        };

        public static ActionResponse<T> Failed(IEnumerable<string>? errorMessage, bool isValidationError = false) => new ActionResponse<T> 
        { 
            IsSuccessful = false, 
            ErrorMessage = errorMessage ?? Enumerable.Empty<string>(),
            IsValidationError = isValidationError,          
        };
    }
}
