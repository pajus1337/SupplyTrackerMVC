using AutoMapper;
using FluentValidation;
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
    public class UpdateProductVm : IMapFrom<Product>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Display(Name = "Active Product For Selection ?")]
        public bool isActive { get; set; }
        public int ProductTypeId { get; set; }
        public ProductTypeSelectListVm ProductType { get; set; }

        public UpdateProductDetailVm ProductDetail { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, UpdateProductVm>()
                .ForMember(x => x.ProductType, opt => opt.Ignore()); 
        }

        public class UpdateProductValidator : AbstractValidator<UpdateProductVm>
        {
            public UpdateProductValidator()
            {
                RuleFor(x => x.Name).NotEmpty().MaximumLength(30).MinimumLength(3);
                RuleFor(x => x.ProductTypeId).GreaterThan(0);
            }
        }
    }
}
