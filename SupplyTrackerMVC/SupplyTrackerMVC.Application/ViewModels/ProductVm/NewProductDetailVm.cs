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
    public class NewProductDetailVm : IMapFrom<ProductDetail>
    {
        public int Id { get; set; }
        [Display(Name = "Chemical Symbol")]
        public string ChemicalSymbol { get; set; }
        [Display(Name = "Chemical Name")]
        public string ChemicalName { get; set; }
        [Display(Name = "Product Description")]
        public string ProductDescription { get; set; }
        [Display(Name = "Mass Fraction")]
        public double MassFraction { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<NewProductDetailVm, ProductDetail>();
        }

        public class NewProductDetailValidator : AbstractValidator<ProductDetail>
        {
            public NewProductDetailValidator()
            {
                RuleFor(x => x.ChemicalSymbol).NotEmpty().MaximumLength(15);
                RuleFor(x => x.ChemicalName).NotEmpty().MaximumLength(50);
                RuleFor(x => x.ProductDescription).NotEmpty().MaximumLength(2500);
                RuleFor(x => x.MassFraction).GreaterThan(0);
            }
        }
    }
}
