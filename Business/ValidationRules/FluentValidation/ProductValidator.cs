using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.ProductName).NotEmpty().WithMessage("Ürün adı boş bırakılamaz!");
            RuleFor(p => p.ProductName).Length(2, 30).WithMessage("Ürün adı 2 ile 30 karakter arasında olamlı.");
            RuleFor(p => p.UnitPrice).NotNull().WithMessage("Ürün fiyatı boş bırakılamaz");
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(1).WithMessage("Ürün fiyetı 1 değerinden büyük yada eşit olmalı.");
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(10).When(p => p.CategoryId == 1);
            RuleFor(p => p.ProductName).Must(StartWithA).WithMessage("Ürün adı A harfi ile başlamalı.");
        }

        /// <summary>
        /// Tanımlanan parametrenin "A" ile başlaması istenebilir.
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private bool StartWithA(string arg)
        {
            return arg.StartsWith("A");
        }
    }
}
