using AutoMapper;
using SupplyTrackerMVC.Application.Mapping;
using SupplyTrackerMVC.Domain.Model.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.ViewModels.ProductVm
{
    public class ProductDetailVm : IMapFrom<Product>
    {
        public int Id { get; set; }

        [Display(Name = "Common Name")]
        public string Name { get; set; }

        [Display(Name = "Chemical Symbol")]
        public string ChemicalSymbol { get; set; }

        [Display(Name = "Chemical Name")]
        public string ChemicalName { get; set; }

        [Display(Name = "Mass Fraction")]
        public double MassFraction { get; set; }

        [Display(Name = "Product Description")]
        public string ProductDescription { get; set; }

        [Display(Name = "Product Type")]
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
