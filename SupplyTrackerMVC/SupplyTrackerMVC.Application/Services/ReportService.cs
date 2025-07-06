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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.Services
{
    public class ReportService : IReportService
    {
        private readonly IReceiverRepository _receiverRepository;
        private readonly IProductRepository _productRepository;
        private readonly IFluentValidatorFactory _validatorFactory;
        private readonly IReportGenerator _reportGenerator;
        private readonly IMapper _mapper;

        public ReportService(IFluentValidatorFactory validatorFactory, IReceiverRepository receiverRepository, IProductRepository productRepository, IReportGenerator reportGenerator, IMapper mapper)
        {
            _validatorFactory = validatorFactory;
            _receiverRepository = receiverRepository;
            _productRepository = productRepository;
            _reportGenerator = reportGenerator;
            _mapper = mapper;
        }


        // TODO: Refine prototype 
        public async Task<ServiceResponse<ReportFilterVm>> PrepareReportFilterVm(CancellationToken cancellationToken)
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
                    Receivers = new ReceiverSelectListVm { Receivers = receivers },
                    Products = new ProductSelectListVm { Products = products },
                    ReceiverBranches = new ReceiverBranchSelectListVm { ReceiverBranches = receiverBranches }
                };

                return ServiceResponse<ReportFilterVm>.CreateSuccess(filterModel);
            }
            catch (Exception ex)
            {
                return ServiceResponse<ReportFilterVm>.CreateFailed(new string[] { $"Error occurred -> {ex.Message}" });
            }
        }

        public void GenerateReportPdf(ReportFilterVm filterModel)
        {
            var reportData = filterModel.ReportType switch
            {
                ReportType.Daily => _reportGenerator.GetDailyReportData(filterModel),

                ReportType.Monthly => _reportGenerator.GetMonthlyReportData(filterModel),

                _ => throw new InvalidOperationException("Unsupported report type")
            };
        }
    }
}
