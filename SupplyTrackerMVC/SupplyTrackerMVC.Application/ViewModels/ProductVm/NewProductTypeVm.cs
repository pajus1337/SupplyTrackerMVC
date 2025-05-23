﻿using AutoMapper;
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
    public class NewProductTypeVm : IMapFrom<ProductType>
    {
        public int Id { get; set; }
        public string PhysicalState { get; set; }
        public bool IsADRProduct { get; set; }
        public bool IsPackaged { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<NewProductTypeVm, ProductType>();
        }

        public class NewProductTypeValidator : AbstractValidator<NewProductTypeVm>
        {
            public NewProductTypeValidator()
            {
                RuleFor(x => x.PhysicalState).NotEmpty().Length(3, 10);
            }
        }
    }
}
