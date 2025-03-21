using AutoMapper;
using FluentValidation;
using SupplyTrackerMVC.Application.Mapping;
using SupplyTrackerMVC.Application.ViewModels.SenderVm;
using SupplyTrackerMVC.Domain.Model.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SupplyTrackerMVC.Application.ViewModels.SenderVm.NewContactForSenderVm;

namespace SupplyTrackerMVC.Application.ViewModels.ProductVm
{
    public class NewProductVm : IMapFrom<Product>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Display(Name = "Active Product For Selection ?")]
        public bool isActive { get; set; }

        public NewProductDetailVm ProductDetail { get; set; }
        public int ProductTypeId { get; set; }
        public ProductTypeSelectListVm ProductType { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, NewProductVm>();
            profile.CreateMap<NewProductDetailVm, ProductDetail>();
            profile.CreateMap<NewProductVm, Product>();
        }

        public class NewProductValidator : AbstractValidator<NewProductVm>
        {
            public NewProductValidator()
            {
                RuleFor(x => x.ProductTypeId).GreaterThan(0);
                RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
                RuleFor(x => x.ProductDetail).NotNull();
            }
        }
    }
}