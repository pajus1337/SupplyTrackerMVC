using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SupplyTrackerMVC.Application.Interfaces;
using SupplyTrackerMVC.Application.Responses;
using SupplyTrackerMVC.Application.ViewModels.Common;
using SupplyTrackerMVC.Application.ViewModels.ReceiverVm;
using SupplyTrackerMVC.Application.ViewModels.SenderVm;
using SupplyTrackerMVC.Domain.Interfaces;
using SupplyTrackerMVC.Domain.Model.Contacts;
using SupplyTrackerMVC.Domain.Model.Receivers;

namespace SupplyTrackerMVC.Application.Services
{
    public class ReceiverService : IReceiverService
    {
        private readonly IFluentValidatorFactory _fluentValidatorFactory;
        private readonly IReceiverRepository _receiverRepository;
        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;

        public ReceiverService(IReceiverRepository receiverRepository, IFluentValidatorFactory fluentValidatorFactory, IContactRepository contactRepository, IMapper mapper, IServiceProvider serviceProvider)
        {
            _receiverRepository = receiverRepository;
            _fluentValidatorFactory = fluentValidatorFactory;
            _contactRepository = contactRepository;
            _mapper = mapper;
        }

        public async Task<ActionResponse<VoidValue>> AddReceiverAsync(NewReceiverVm model, CancellationToken cancellationToken)
        {
            var validator = _fluentValidatorFactory.GetValidator<NewReceiverVm>();
            if (validator == null)
            {
                throw new InvalidOperationException("Validator not found");
            }

            var validationResult = await validator.ValidateAsync(model, cancellationToken);
            if (!validationResult.IsValid)
            {
                return ActionResponse<VoidValue>.Failed(validationResult.Errors.Select(e => e.ErrorMessage), true);
            }

            var receiver = _mapper.Map<Receiver>(model);
            var (isSuccess, receiverId) = await _receiverRepository.AddReceiverAsync(receiver, cancellationToken);

            return isSuccess ? ActionResponse<VoidValue>.Success(null, receiverId) : ActionResponse<VoidValue>.Failed(new string[] { "Failed to add receiver" });
        }

        public async Task<ActionResponse<ListReceiverForListVm>> GetReceiversForListAsync(int pageSize, int pageNo, string searchString, CancellationToken cancellationToken)
        {
            try
            {
                var receiversQuery = _receiverRepository.GetAllReceivers().Where(p => p.Name.StartsWith(searchString)).OrderBy(p => p.Id);
                var receiversToShow = receiversQuery.Skip(pageSize * (pageNo - 1)).Take(pageSize);
                var receivers = await receiversToShow.ToListAsync(cancellationToken);

                ListReceiverForListVm result = new ListReceiverForListVm();
                result.Receivers = new List<ReceiverForListVm>();
                result.CurrentPage = pageNo;
                result.PageSize = pageSize;
                result.SearchString = searchString;
                result.Count = receiversQuery.Count();

                foreach (var receiver in receivers)
                {
                    var receiverForListVm = new ReceiverForListVm()
                    {
                        Id = receiver.Id,
                        Name = receiver.Name,
                    };

                    result.Receivers.Add(receiverForListVm);
                }

                return ActionResponse<ListReceiverForListVm>.Success(result);
            }

            catch (Exception ex)
            {
                return ActionResponse<ListReceiverForListVm>.Failed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }


        // TODO: Include contacts and maybe more ? In Service or repo ? Check out´the best solution for such opertation.
        public async Task<ActionResponse<ReceiverDetailsVm>> GetReceiverDetailsByIdAsync(int receiverId, CancellationToken cancellationToken)
        {
            if (receiverId < 1)
            {
                return ActionResponse<ReceiverDetailsVm>.Failed(new string[] { "Invalid receiver ID" });
            }

            try
            {
                var receiverQuery = _receiverRepository.GetReceiverById(receiverId);
                var receiver = await receiverQuery.SingleOrDefaultAsync(cancellationToken);

                if (receiver == null)
                {
                    ActionResponse<ReceiverDetailsVm>.Failed(new string[] { "receiver not found" });
                }

                var receiverVm = _mapper.Map<ReceiverDetailsVm>(receiver);

                return ActionResponse<ReceiverDetailsVm>.Success(receiverVm);
            }
            catch (Exception ex)
            {
                return ActionResponse<ReceiverDetailsVm>.Failed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }

        public async Task<ActionResponse<ReceiverSelectListVm>> GetReceiversForSelectListAsync(CancellationToken cancellationToken)
        {
            try
            {
                var receivers = await _receiverRepository.GetAllReceivers().ProjectTo<ReceiverForSelectListVm>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
                if (receivers == null)
                {
                    return ActionResponse<ReceiverSelectListVm>.Failed(new string[] { "receiver not found" });
                }

                var receiversSelectListVm = new ReceiverSelectListVm()
                {
                    Receivers = receivers,
                };

                return ActionResponse<ReceiverSelectListVm>.Success(receiversSelectListVm);
            }
            catch (Exception ex)
            {
                return ActionResponse<ReceiverSelectListVm>.Failed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }

        public async Task<ActionResponse<ReceiverBranchSelectListVm>> GetReceiverBranchesForSelectListAsync(CancellationToken cancellationToken)
        {
            var receiverBranchesQuery = _receiverRepository.GetAllReceiverBranches().ProjectTo<ReceiverBranchForSelectListVm>(_mapper.ConfigurationProvider);

            try
            {
                var receiverBranches = await receiverBranchesQuery.ToListAsync(cancellationToken);

                // TODO: Remove null checks for collections in services. Lists and IQueryable results are never null, but may be empty
                if (receiverBranches == null)
                {
                    return ActionResponse<ReceiverBranchSelectListVm>.Failed(new string[] { "receiver is null" });
                }
                var receiverBranchesVm = new ReceiverBranchSelectListVm()
                {
                    ReceiverBranches = receiverBranchesQuery,
                };

                return ActionResponse<ReceiverBranchSelectListVm>.Success(receiverBranchesVm);
            }
            catch (Exception ex)
            {
                return ActionResponse<ReceiverBranchSelectListVm>.Failed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }

        public async Task<ActionResponse<VoidValue>> AddReceiverBranchAsync(NewReceiverBranchVm model, CancellationToken cancellationToken)
        {
            if (model == null)
            {
                return ActionResponse<VoidValue>.Failed(new string[] { "Error occurred while processing the HTTP POST form" });
            }

            var receiverBranch = _mapper.Map<ReceiverBranch>(model);

            try
            {
                var (isSuccess, receiverBranchId) = await _receiverRepository.AddReceiverBranchAsync(receiverBranch, cancellationToken);

                return isSuccess ? ActionResponse<VoidValue>.Success(null, receiverBranch.Id) : ActionResponse<VoidValue>.Failed(new string[] { "Failed to add new Receiver Branch" });
            }
            catch (Exception ex)
            {
                return ActionResponse<VoidValue>.Failed(new string[] { "An error occurred while creating new receiver branch", ex.Message });
            }
        }

        // TODO: Create better implementation of this prototype metod
        public async Task<NewReceiverBranchVm> PrepareNewReceiverBranchVm(CancellationToken cancellationToken, int receiverId = 0)
        {
            var receivers = await GetReceiversForSelectListAsync(cancellationToken);
            var contactTypes = await GetContactTypesForSelectListAsync(cancellationToken);

            var model = new NewReceiverBranchVm();

            model.ReceiverSelectList = receivers.Data;
            model.NewContactForReceiverBranch = new NewContactVm()
            {
                ContactDetailVm = new NewContactDetailVm()
                {
                    ContactDetailTypeSelectList = contactTypes.Data
                }
            };

            if (receiverId > 0)
            {
                model.ReceiverSelectedId = receiverId;
                model.ReceiverSelectList.Receivers = receivers.Data.Receivers.Where(p => p.Id == receiverId);
            }

            return model;
        }

        public async Task<ActionResponse<ReceiverBranchDetailsVm>> GetReceiverBranchDetailsAsync(int receiverBranchId, CancellationToken cancellationToken)
        {
            if (receiverBranchId <= 0)
            {
                return ActionResponse<ReceiverBranchDetailsVm>.Failed(new string[] { "Invalid receiver branch ID" });
            }

            try
            {
                var receiverBranchQuery = _receiverRepository.GetAllReceiverBranches().Where(rb => rb.Id == receiverBranchId).ProjectTo<ReceiverBranchDetailsVm>(_mapper.ConfigurationProvider);
                var receiverBranch = await receiverBranchQuery.SingleOrDefaultAsync(cancellationToken);

                if (receiverBranch == null)
                {
                    return ActionResponse<ReceiverBranchDetailsVm>.Failed(new string[] { "Receiver branch not found" });
                }

                return ActionResponse<ReceiverBranchDetailsVm>.Success(receiverBranch);

            }
            catch (Exception ex)
            {
                return ActionResponse<ReceiverBranchDetailsVm>.Failed(new string[] { "An error occurred while retrieving receiver branch details", ex.Message });
            }
        }

        private async Task<ActionResponse<ContactDetailTypeSelectListVm>> GetContactTypesForSelectListAsync(CancellationToken cancellationToken)
        {
            var contactTypesQuery = _contactRepository.GetContactDetailTypes().ProjectTo<ContactDetailTypeForSelectListVm>(_mapper.ConfigurationProvider);

            try
            {
                var contactTypes = await contactTypesQuery.ToListAsync(cancellationToken);
                if (contactTypes.Count == 0)
                {
                    return ActionResponse<ContactDetailTypeSelectListVm>.Failed(new string[] { "Collection is empty, Add new elements before use" });
                }

                var contactTypesVm = new ContactDetailTypeSelectListVm()
                {
                    ContactDetailTypes = contactTypes
                };

                return ActionResponse<ContactDetailTypeSelectListVm>.Success(contactTypesVm);
            }
            catch (Exception ex)
            {
                return ActionResponse<ContactDetailTypeSelectListVm>.Failed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }

        public async Task<ActionResponse<ReceiverDetailsVm>> UpdateReceiverAsync(UpdateReceiverVm updateReceiverVm, CancellationToken cancellationToken)
        {
            if (updateReceiverVm == null)
            {
                return ActionResponse<ReceiverDetailsVm>.Failed(new[] { "Error occurred while processing the HTTP POST form" });
            }

            try
            {
                var validator = _fluentValidatorFactory.GetValidator<UpdateReceiverVm>();
                var validationResult = await validator.ValidateAsync(updateReceiverVm, cancellationToken);
                if (!validationResult.IsValid)
                {
                    return ActionResponse<ReceiverDetailsVm>.Failed(validationResult.Errors.Select(e => e.ErrorMessage), true);
                }

                var receiver = _mapper.Map<Receiver>(updateReceiverVm);
                var success = await _receiverRepository.UpdateReceiverAsync(receiver, cancellationToken);
                if (!success)
                {
                    return ActionResponse<ReceiverDetailsVm>.Failed(new[] { "Failed to update receiver" });
                }

                var response = await GetReceiverDetailsByIdAsync(receiver.Id, cancellationToken);
                if (!response.IsSuccessful)
                {
                    return ActionResponse<ReceiverDetailsVm>.Failed(new[] { "Receiver not found in DB after update" });
                }

                return ActionResponse<ReceiverDetailsVm>.Success(response.Data);
            }
            catch (Exception ex)
            {
                return ActionResponse<ReceiverDetailsVm>.Failed(new[] { $"Error occurred -> {ex.Message}" });
            }
        }

        public async Task<ActionResponse<VoidValue>> DeleteReceiverByIdAsync(int receiverId, CancellationToken cancellationToken)
        {
            if (receiverId < 1)
            {
                return ActionResponse<VoidValue>.Failed(new string[] { "Invalid receiver ID" });
            }

            try
            {
                await _receiverRepository.DeleteReceiverAsync(receiverId, cancellationToken);
            }
            catch (Exception ex)
            {
                return ActionResponse<VoidValue>.Failed(new[] { $"An error occurred: {ex.Message}" });
            }

            return ActionResponse<VoidValue>.Success(null);
        }


        // TODO: Should we get ReceiverBranch seperet or get all with one db query call? Think - And implement best way for the project.
        public async Task<ActionResponse<UpdateReceiverVm>> GetReceiverForEditAsync(int receiverId, CancellationToken cancellationToken)
        {
            if (receiverId < 1)
            {
                return ActionResponse<UpdateReceiverVm>.Failed(new string[] { "Invalid receiver ID" });
            }

            try
            {
                var receiverVm = await _receiverRepository.GetReceiverById(receiverId).ProjectTo<UpdateReceiverVm>(_mapper.ConfigurationProvider).SingleOrDefaultAsync(cancellationToken);
                if (receiverVm == null)
                {
                    return ActionResponse<UpdateReceiverVm>.Failed(new string[] { "Receiver not found in Db" });
                }

                // var receiverBranches = await _receiverRepository.GetAllActiveReceiverBranches(receiverId).ProjectTo<ReceiverForListVm>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);

                return ActionResponse<UpdateReceiverVm>.Success(receiverVm);
            }
            catch (Exception ex)
            {
                return ActionResponse<UpdateReceiverVm>.Failed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }

        public async Task<ActionResponse<NewContactVm>> AddReceiverContactAsync(NewContactVm newContactVm, CancellationToken cancellationToken)
        {
            try
            {
                var validator = _fluentValidatorFactory.GetValidator<NewContactVm>();
                var validResult = await validator.ValidateAsync(newContactVm, cancellationToken);
                if (!validResult.IsValid)
                {
                    return ActionResponse<NewContactVm>.Failed(validResult.Errors.Select(e => e.ErrorMessage), true);
                }

                var contact = _mapper.Map<NewContactVm, Contact>(newContactVm);
                contact.ReceiverId = newContactVm.ContactOwnerId;
                var contactId = await _contactRepository.AddContactAsync(contact, cancellationToken);

                return ActionResponse<NewContactVm>.Success(null, contactId);
            }
            catch (Exception ex)
            {
                return ActionResponse<NewContactVm>.Failed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }

        public async Task<ActionResponse<ReceiverForDeleteVm>> GetReceiverForDeleteAsync(int receiverId, CancellationToken cancellationToken)
        {
            if (receiverId <= 0)
            {
                return ActionResponse<ReceiverForDeleteVm>.Failed(new string[] { "Invalid receiver ID" });
            }

            try
            {
                var receiverQuery = _receiverRepository.GetReceiverById(receiverId).ProjectTo<ReceiverForDeleteVm>(_mapper.ConfigurationProvider);
                var receiverVm = await receiverQuery.SingleOrDefaultAsync(cancellationToken);
                if (receiverVm == null)
                {
                    return ActionResponse<ReceiverForDeleteVm>.Failed(new string[] { "Receiver not found in Db" });
                }

                return ActionResponse<ReceiverForDeleteVm>.Success(receiverVm);
            }
            catch (Exception ex)
            {
                return ActionResponse<ReceiverForDeleteVm>.Failed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }
    }
}
