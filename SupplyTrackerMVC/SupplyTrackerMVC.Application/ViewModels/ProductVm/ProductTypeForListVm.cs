using AutoMapper;
using SupplyTrackerMVC.Application.Mapping;
using SupplyTrackerMVC.Domain.Model.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.ViewModels.ProductVm
{
    public class ProductTypeForListVm : IMapFrom<ProductType>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ProductType, ProductTypeForListVm>();
        }
    }
}
