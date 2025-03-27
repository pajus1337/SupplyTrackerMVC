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
    public class ProductTypeVm : IMapFrom<ProductType>
    {
        public int Id { get; set; }
        [Display(Name = "Physical State")]
        public string PhysicalState { get; set; }
        [Display(Name = "ADR Product")]
        public bool IsADRProduct { get; set; }
        [Display(Name = "Packaged")]
        public bool IsPackaged { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ProductType, ProductTypeVm>();
        }
    }
}
