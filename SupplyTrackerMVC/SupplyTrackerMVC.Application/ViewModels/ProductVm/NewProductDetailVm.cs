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
    public class NewProductDetailVm : IMapFrom<ProductDetail>
    {
        public int Id { get; set; }
        public string ChemicalSymbol { get; set; }
        public string ChemicalName { get; set; }
        public string ProductDescription { get; set; }
        public int MassFraction { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ProductDetail, NewProductDetailVm>();
            profile.CreateMap<NewProductDetailVm, ProductDetail>();
        }
    }
}
