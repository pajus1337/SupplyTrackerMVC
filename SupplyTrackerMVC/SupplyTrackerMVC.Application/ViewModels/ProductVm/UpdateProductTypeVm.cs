using AutoMapper;
using FluentValidation;
using SupplyTrackerMVC.Application.Mapping;
using SupplyTrackerMVC.Domain.Model.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyTrackerMVC.Application.ViewModels.ProductVm
{
    public class UpdateProductTypeVm : IMapFrom<ProductType>
    {
        public int Id { get; set; }
        public string PhysicalState { get; set; }
        public bool IsADRProduct { get; set; }
        public bool IsPackaged { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateProductTypeVm, ProductType>().ReverseMap();
        }

        public class UpdateProductTypeValidator : AbstractValidator<UpdateProductTypeVm>
        {
            public UpdateProductTypeValidator()
            {
                RuleFor(p => p.Id).GreaterThan(0);
                RuleFor(p => p.PhysicalState).NotEmpty().Length(3, 10);
            }
        }
    }
}
