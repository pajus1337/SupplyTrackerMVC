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
    public class ProductTypeForSelectListVm : IMapFrom<ProductType>
    {
        public int Id { get; set; }
        public string PhysicalState { get; set; }
        public bool IsADRProduct { get; set; }
        public bool IsPackaged { get; set; }
        public string Description { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ProductType, ProductTypeForSelectListVm>()
                .ForMember(d => d.Description, opt => opt.MapFrom(s => $"{s.PhysicalState} | {(s.IsADRProduct ? "ADR" : "Non-ADR")} | {(s.IsPackaged ? "Packaged" : "Loose")}"));
        }
    }
}
