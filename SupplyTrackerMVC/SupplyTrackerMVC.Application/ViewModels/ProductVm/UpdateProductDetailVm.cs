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
    public class UpdateProductDetailVm : IMapFrom<ProductDetail>
    {
        public int Id { get; set; }
        public string ChemicalSymbol { get; set; }
        public string ChemicalName { get; set; }
        public string ProductDescription { get; set; }
        public double MassFraction { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ProductDetail, UpdateProductDetailVm>().ReverseMap();
        }

        public class UpdateProductDetailValidator : AbstractValidator<ProductDetail>
        {
            public UpdateProductDetailValidator()
            {
                RuleFor(x => x.Id).GreaterThan(0);
                RuleFor(x => x.ChemicalSymbol).NotEmpty().MaximumLength(15);
                RuleFor(x => x.ChemicalName).NotEmpty().MaximumLength(50);
                RuleFor(x => x.ProductDescription).NotEmpty().MaximumLength(2500);
                RuleFor(x => x.MassFraction).GreaterThan(0);
            }
        }
    }
}
