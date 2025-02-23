using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SupplyTrackerMVC.Application.Interfaces;
using SupplyTrackerMVC.Application.Responses;
using SupplyTrackerMVC.Application.ViewModels.Common;
using SupplyTrackerMVC.Application.ViewModels.ReceiverVm;
using SupplyTrackerMVC.Domain.Interfaces;
using SupplyTrackerMVC.Domain.Model.Receivers;

namespace SupplyTrackerMVC.Application.Services
{
    public class ReceiverService : IReceiverService
    {
        private readonly IFluentValidatorFactory _fluentValidatorFactory;
        private readonly IReceiverRepository _receiverRepository;
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;

        public ReceiverService(IReceiverRepository receiverRepository,IFluentValidatorFactory fluentValidatorFactory, IContactRepository contactRepository, IMapper mapper, IServiceProvider serviceProvider)
        {
            _receiverRepository = receiverRepository;
            _fluentValidatorFactory = fluentValidatorFactory;
            _contactRepository = contactRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<VoidValue>> AddReceiverAsync(NewReceiverVm model, CancellationToken cancellationToken)
        {
            var validator = _fluentValidatorFactory.GetValidator<NewReceiverVm>();
            if (validator == null)
            {
                throw new InvalidOperationException("Validator not found");
            }

            var validationResult = await validator.ValidateAsync(model, cancellationToken);
            if (!validationResult.IsValid)
            {
                return ServiceResponse<VoidValue>.CreateFailed(validationResult.Errors.Select(e => e.ErrorMessage), true);
            }

            var receiver = _mapper.Map<Receiver>(model);
            var (isSuccess, receiverId) = await _receiverRepository.AddReceiverAsync(receiver, cancellationToken);

            return isSuccess ? ServiceResponse<VoidValue>.CreateSuccess(null, receiverId) : ServiceResponse<VoidValue>.CreateFailed(new string[] { "Failed to add receiver" });
        }

        public async Task<ServiceResponse<ListReceiverForListVm>> GetReceiversForListAsync(CancellationToken cancellationToken)
        {
            var receiversQuery = _receiverRepository.GetAllReceivers();

            try
            {
                var receivers = await receiversQuery.ToListAsync(cancellationToken);

                ListReceiverForListVm result = new ListReceiverForListVm();
                result.Receivers = new List<ReceiverForListVm>();

                foreach (var receiver in receivers)
                {
                    var receiverForListVm = new ReceiverForListVm()
                    {
                        Id = receiver.Id,
                        Name = receiver.Name,
                    };

                    result.Receivers.Add(receiverForListVm);
                }

                return ServiceResponse<ListReceiverForListVm>.CreateSuccess(result);
            }
            catch (Exception)
            {

                throw;
            }
        }


        // TODO: Include contacts and maybe more ? In Service or repo ? Check out´the best solution for such opertation.
        public async Task<ServiceResponse<ReceiverDetailsVm>> GetReceiverDetailsByIdAsync(int receiverId, CancellationToken cancellationToken)
        {
            if (receiverId < 1)
            {
                return ServiceResponse<ReceiverDetailsVm>.CreateFailed(new string[] { "Invalid receiver ID" });
            }

            try
            {
                var receiverQuery = _receiverRepository.GetReceiverById(receiverId);
                var receiver = await receiverQuery.SingleOrDefaultAsync(cancellationToken);

                if (receiver == null)
                {
                    ServiceResponse<ReceiverDetailsVm>.CreateFailed(new string[] { "receiver not found" });
                }

                var receiverVm = _mapper.Map<ReceiverDetailsVm>(receiver);

                return ServiceResponse<ReceiverDetailsVm>.CreateSuccess(receiverVm);
            }
            catch (Exception ex)
            {
                return ServiceResponse<ReceiverDetailsVm>.CreateFailed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }

        public async Task<ServiceResponse<ReceiverSelectListVm>> GetReceiversForSelectListAsync(CancellationToken cancellationToken)
        {
            try
            {
                var receivers = await _receiverRepository.GetAllReceivers().ProjectTo<ReceiverForSelectListVm>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
                if (receivers == null)
                {
                    return ServiceResponse<ReceiverSelectListVm>.CreateFailed(new string[] { "receiver not found" });
                }

                var receiversSelectListVm = new ReceiverSelectListVm()
                {
                    Receivers = receivers,
                };

                return ServiceResponse<ReceiverSelectListVm>.CreateSuccess(receiversSelectListVm);
            }
            catch (Exception ex)
            {
                return ServiceResponse<ReceiverSelectListVm>.CreateFailed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }

        public async Task<ServiceResponse<ReceiverBranchSelectListVm>> GetReceiverBranchesForSelectListAsync(CancellationToken cancellationToken)
        {
            var receiverBranchesQuery = _receiverRepository.GetAllReceiverBranches().ProjectTo<ReceiverBranchForSelectListVm>(_mapper.ConfigurationProvider);

            try
            {
                var receiverBranches = await receiverBranchesQuery.ToListAsync(cancellationToken);

                // TODO: Remove null checks for collections in services. Lists and IQueryable results are never null, but may be empty
                if (receiverBranches == null)
                {
                    return ServiceResponse<ReceiverBranchSelectListVm>.CreateFailed(new string[] { "receiver is null" });
                }
                var receiverBranchesVm = new ReceiverBranchSelectListVm()
                {
                    ReceiverBranches = receiverBranchesQuery,
                };

                return ServiceResponse<ReceiverBranchSelectListVm>.CreateSuccess(receiverBranchesVm);
            }
            catch (Exception ex)
            {
                return ServiceResponse<ReceiverBranchSelectListVm>.CreateFailed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }

        public async Task<ServiceResponse<VoidValue>> AddReceiverBranchAsync(NewReceiverBranchVm model, CancellationToken cancellationToken)
        {
            if (model == null)
            {
                return ServiceResponse<VoidValue>.CreateFailed(new string[] { "Error occurred while processing the HTTP POST form" });
            }

            var receiverBranch = _mapper.Map<ReceiverBranch>(model);

            try
            {
                var (isSuccess, receiverBranchId) = await _receiverRepository.AddReceiverBranchAsync(receiverBranch, cancellationToken);

                return isSuccess ? ServiceResponse<VoidValue>.CreateSuccess(null, receiverBranch.Id) : ServiceResponse<VoidValue>.CreateFailed(new string[] { "Failed to add new Receiver Branch" });
            }
            catch (Exception ex)
            {
                return ServiceResponse<VoidValue>.CreateFailed(new string[] { "An error occurred while creating new receiver branch", ex.Message });
            }
        }

        // TODO: Create better implementation of this prototype metod
        public async Task<NewReceiverBranchVm> PrepareNewReceiverBranchVm(CancellationToken cancellationToken)
        {
            var receivers = await GetReceiversForSelectListAsync(cancellationToken);
            var contactTypes = await GetContactTypesForSelectListAsync(cancellationToken);

            var model = new NewReceiverBranchVm();

            model.ReceiverSelectList = receivers.Data;
            model.NewContactForReceiverBranch = new AddContactVm()
            {
                ContactDetailVm = new AddContactDetailVm()
                {
                    ContactDetailTypeSelectList = contactTypes.Data
                }
            };

                return model;
        }

        public async Task<ServiceResponse<ReceiverBranchDetailsVm>> GetReceiverBranchDetailsAsync(int receiverBranchId, CancellationToken cancellationToken)
        {
            if (receiverBranchId <= 0)
            {
                return ServiceResponse<ReceiverBranchDetailsVm>.CreateFailed(new string[] { "Invalid receiver branch ID" });
            }

            try
            {
                var receiverBranchQuery = _receiverRepository.GetAllReceiverBranches().Where(rb => rb.Id == receiverBranchId).ProjectTo<ReceiverBranchDetailsVm>(_mapper.ConfigurationProvider);
                var receiverBranch = await receiverBranchQuery.SingleOrDefaultAsync(cancellationToken);

                if (receiverBranch == null)
                {
                    return ServiceResponse<ReceiverBranchDetailsVm>.CreateFailed(new string[] { "Receiver branch not found" });
                }

                return ServiceResponse<ReceiverBranchDetailsVm>.CreateSuccess(receiverBranch);

            }
            catch (Exception ex)
            {
                return ServiceResponse<ReceiverBranchDetailsVm>.CreateFailed(new string[] { "An error occurred while retrieving receiver branch details", ex.Message });
            }
        }

        private async Task<ServiceResponse<ContactDetailTypeSelectListVm>> GetContactTypesForSelectListAsync(CancellationToken cancellationToken)
        {
            var contactTypesQuery = _contactRepository.GetContactDetailTypes().ProjectTo<ContactDetailTypeForSelectListVm>(_mapper.ConfigurationProvider);

            try
            {
                var contactTypes = await contactTypesQuery.ToListAsync(cancellationToken);
                if (contactTypes.Count == 0)
                {
                    return ServiceResponse<ContactDetailTypeSelectListVm>.CreateFailed(new string[] { "Collection is empty, Add new elements before use" });
                }

                var contactTypesVm = new ContactDetailTypeSelectListVm()
                {
                    ContactDetailTypes = contactTypes
                };

                return ServiceResponse<ContactDetailTypeSelectListVm>.CreateSuccess(contactTypesVm);
            }
            catch (Exception ex)
            {
                return ServiceResponse<ContactDetailTypeSelectListVm>.CreateFailed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }

        public Task<ServiceResponse<UpdateReceiverVm>> UpdateReceiverAsync(UpdateReceiverVm model, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<VoidValue>> DeleteReceiverByIdAsync(int receiverId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<UpdateReceiverVm>> GetReceiverForEditAsync(int receiverId, CancellationToken cancellationToken)
        {
            if (receiverId < 1)
            {
                return ServiceResponse<UpdateReceiverVm>.CreateFailed(new string[] { "Invalid receiver ID" });
            }

            try
            {
                var receiverVm = await _receiverRepository.GetReceiverById(receiverId).ProjectTo<UpdateReceiverVm>(_mapper.ConfigurationProvider).SingleOrDefaultAsync(cancellationToken);
                if (receiverVm == null)
                {
                    return ServiceResponse<UpdateReceiverVm>.CreateFailed(new string[] { "Receiver not found in Db" });
                }

                return ServiceResponse<UpdateReceiverVm>.CreateSuccess(receiverVm);
            }
            catch (Exception ex)
            {
                return ServiceResponse<UpdateReceiverVm>.CreateFailed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }
    }
}
