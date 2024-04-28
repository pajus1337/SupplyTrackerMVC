﻿using SupplyTrackerMVC.Application.ViewModels.ProductVm;
using SupplyTrackerMVC.Application.ViewModels.ReceiverVm;
using SupplyTrackerMVC.Domain.Model.Receivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.Interfaces
{
    public interface IProductService
    {
        Task<(bool Success, IEnumerable<string>? Errors, int? ProductId)> AddNewProductAsync(NewProductVm model,CancellationToken cancellationToken);
        Task<(bool Success, string Error)> UpdateProductAsync(int productId, CancellationToken cancellationToken);
        Task<(bool Success, string Error)> DeleteProductASync(int productId, CancellationToken cancellationToken);
        Task<(bool Success, IEnumerable<string>? Errors, int? ProductTypeId)> AddNewProductTypeAsync(NewProductTypeVm model, CancellationToken cancellationToken);
        Task<(bool Success, ProductTypeVm)> GetProductTypeById(int productTypeId, CancellationToken cancellationToken);
        ListProductForList GetAllActiveProductsForList();
        ProductDetailVm GetProductDetailsById(int productId);
        ProductSelectListVm GetAllActiveProductsForSelectList();
        NewProductVm PrepareNewProductViewModel();


    }
}
