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
    public class ProductDetailVm : IMapFrom<Product>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string ChemicalSymbol { get; set; }
        public string ChemicalName { get; set; }
        public double MassFraction { get; set; }
        public string ProductDescription { get; set; }
        public string ProductType { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, ProductDetailVm>()
                .ForMember(p => p.ChemicalName, opt => opt.MapFrom(src => src.ProductDetail.ChemicalName))
                .ForMember(p => p.ChemicalSymbol, opt => opt.MapFrom(src => src.ProductDetail.ChemicalName))
                .ForMember(p => p.MassFraction, opt => opt.MapFrom(src => src.ProductDetail.MassFraction))
                .ForMember(p => p.ProductType, opt => opt.MapFrom(src => src.ProductType.PhysicalState))
                .ForMember(p => p.ProductDescription, opt => opt.MapFrom(src => src.ProductDetail.ProductDescription));
        }
    }
}
