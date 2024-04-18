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
    public class ProductForSelectListVm : IMapFrom<Product>
    {
        public int Id { get; set; }
        public string ChemicalSymbol { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ProductForSelectListVm, Product>()
                .ForMember(x => x.ProductDetail.ChemicalSymbol, opt => opt.MapFrom(s => s.ChemicalSymbol));
        }
    }
}
