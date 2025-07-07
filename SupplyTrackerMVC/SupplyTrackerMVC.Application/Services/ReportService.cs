using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SupplyTrackerMVC.Application.Enums;
using SupplyTrackerMVC.Application.Interfaces;
using SupplyTrackerMVC.Application.Responses;
using SupplyTrackerMVC.Application.ViewModels.ProductVm;
using SupplyTrackerMVC.Application.ViewModels.ReceiverVm;
using SupplyTrackerMVC.Application.ViewModels.ReportVm;
using SupplyTrackerMVC.Domain.Interfaces;

namespace SupplyTrackerMVC.Application.Services
{
    public class ReportService : IReportService
    {
        private readonly IFluentValidatorFactory _fluentValidatorFactory;
        private readonly IReceiverRepository _receiverRepository;
        private readonly IProductRepository _productRepository;
        private readonly IDeliveryRepository _deliveryRepository;
        private readonly IReportGenerator _reportGenerator;
        private readonly IMapper _mapper;

        public ReportService(IFluentValidatorFactory fluentValidatorFactory, IReceiverRepository receiverRepository, IProductRepository productRepository, IDeliveryRepository deliveryRepository, IReportGenerator reportGenerator, IMapper mapper)
        {
            _fluentValidatorFactory = fluentValidatorFactory;
            _receiverRepository = receiverRepository;
            _productRepository = productRepository;
            _deliveryRepository = deliveryRepository;
            _reportGenerator = reportGenerator;
            _mapper = mapper;
        }

        public async Task<ActionResponse<ReportFilterVm>> PrepareReportFilterVm(ReportType reportType, CancellationToken cancellationToken)
        {
            try
            {
                var receiversQuery = _receiverRepository.GetAllReceivers().ProjectTo<ReceiverForSelectListVm>(_mapper.ConfigurationProvider);
                var receiverBranchesQuery = _receiverRepository.GetAllReceiverBranches().ProjectTo<ReceiverBranchForSelectListVm>(_mapper.ConfigurationProvider);
                var productsQuery = _productRepository.GetAllProducts().ProjectTo<ProductForSelectListVm>(_mapper.ConfigurationProvider);

                var receivers = await receiversQuery.ToListAsync(cancellationToken);
                var receiverBranches = await receiverBranchesQuery.ToListAsync(cancellationToken);
                var products = await productsQuery.ToListAsync(cancellationToken);

                var filterModel = new ReportFilterVm
                {
                    ReportType = reportType,
                    Receivers = new ReceiverSelectListVm { Receivers = receivers },
                    Products = new ProductSelectListVm { Products = products },
                    ReceiverBranches = new ReceiverBranchSelectListVm { ReceiverBranches = receiverBranches }
                };

                return ActionResponse<ReportFilterVm>.Success(filterModel);
            }
            catch (Exception ex)
            {
                return ActionResponse<ReportFilterVm>.Failed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }

        public async Task<ActionResponse<ReportGenerationResult>> GenerateReportAsync(ReportFilterVm filterModel, CancellationToken cancellationToken)
        {
            if (filterModel == null)
            {
                return ActionResponse<ReportGenerationResult>.Failed(new string[] { "Error occurred while processing the HTTP POST form" });
            }

            try
            {
                var validationResult = await ValidateReportFilterVm(filterModel, cancellationToken);

                if (!validationResult.IsSuccessful)
                {
                    return validationResult;
                }

                var actionResponse = await (filterModel.ReportType switch
                {
                    ReportType.Daily => GetDailyReportDataAsync(filterModel, cancellationToken),
                    ReportType.Monthly => GetMonthlyReportDataAsync(filterModel, cancellationToken),
                    _ => Task.FromResult(ActionResponse<ReportGenerationResult>.Failed(new[] { $"Unsupported report type: {filterModel.ReportType}" }))
                });
                 
                if (actionResponse.Data?.ReportData is null)
                {
                    return ActionResponse<ReportGenerationResult>.Failed(new[] { "Report data could not be generated." });
                }

                var generatedPdf = _reportGenerator.GeneratePdf(actionResponse.Data.ReportData);

                return ActionResponse<ReportGenerationResult>.Success(new ReportGenerationResult { GeneratedPdf = generatedPdf});
            }
            catch (Exception ex)
            {
                return ActionResponse<ReportGenerationResult>.Failed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }

        private async Task<ActionResponse<ReportGenerationResult>> ValidateReportFilterVm(ReportFilterVm filterModel, CancellationToken cancellationToken)
        {
            var validator = _fluentValidatorFactory.GetValidator<ReportFilterVm>();
            var validationResult = await validator.ValidateAsync(filterModel, cancellationToken);
            if (!validationResult.IsValid)
            {
                return ActionResponse<ReportGenerationResult>.Failed(validationResult.Errors.Select(e => e.ErrorMessage), true);
            }

            var validFilterModel = new ReportGenerationResult
            {
                InputModel = filterModel
            };

            return ActionResponse<ReportGenerationResult>.Success(validFilterModel);
        }

        // TODO: Split it some day ?:> -> SRP
        private async Task<ActionResponse<ReportGenerationResult>> GetDailyReportDataAsync(ReportFilterVm filter, CancellationToken cancellationToken)
        {
            if (filter.SelectedDate is null)
            {
                return ActionResponse<ReportGenerationResult>.Failed(new string[] { "Date must be selected." }, true);
            }

            try
            {
                var start = filter.SelectedDate.Value.Date;
                var end = start.AddDays(1);

                var deliveriesReport = await _deliveryRepository.GetAllDeliveries()
                    .Where(d =>
                        d.DeliveryDataTime >= start &&
                        d.DeliveryDataTime < end &&
                        d.ReceiverId == filter.SelectedReceiverId &&
                        d.ReceiverBranchId == filter.SelectedReceiverBranchId &&
                        d.ProductId == filter.SelectedProductId)
                    .ProjectTo<ReportDeliveryVm>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                var reportData = new ListReportDeliveryVm
                {
                    ReportDeliveries = deliveriesReport
                };

                var result = new ReportGenerationResult
                {
                    ReportData = reportData,
                };

                return ActionResponse<ReportGenerationResult>.Success(result);
            }
            catch (Exception ex)
            {
                return ActionResponse<ReportGenerationResult>.Failed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }

        private async Task<ActionResponse<ReportGenerationResult>> GetMonthlyReportDataAsync(ReportFilterVm filter, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
