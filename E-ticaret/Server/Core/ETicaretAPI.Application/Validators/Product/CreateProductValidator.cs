using ETicaretAPI.Application.Dtos.Product;
using ETicaretAPI.Application.Repositories.ProductRepo;
using FluentValidation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Validators.Product
{
    public class CreateProductValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductValidator()
        {
            RuleFor(x=>x.Name)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Ürünün Adı Boş Bırakılamaz.!")
                .MinimumLength(2)
                .MaximumLength(250)
                    .WithMessage("Minimum 2 Maksimum 250 Karakter Girin.!")
                //.Matches(@"^[a-zA-Z0-9\s]+$")
                //    .WithMessage("Ürün adı sadece harfler, rakamlar ve boşluk içerebilir.!")
                .Must(name => name is not null && !name.Contains("Salak"))
                    .WithMessage("Ürünün Adı Uygunsuz Kelimeler İçermemelidir.!")
            ;


            RuleFor(x => x.Stock)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Stok Bilgisi Boş Bırakılamaz.!")
                .Must(stock => stock >= 0)
                    .WithMessage("0 Değerinin Altında Bir Değer Girilemez.!")
                .Must(stock => stock % 1 ==0)
                    .WithMessage("Stok Tam Sayı Olmalıdır.!")
                ;


            RuleFor(x => x.Price)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Fiyat Bilgisi Boş Bırakılamaz.!")
                .Must(s => s >= 0)
                    .WithMessage("0 Değerinin Altında Bir Değer Girilemez.!")
                .Must(stock => stock % 1 == 0)
                    .WithMessage("Fiyat Tam Sayı Olmalıdır")
                .InclusiveBetween(1,100000)
                    .WithMessage("Fiyat Aralığı 1 İla 100.000 Aralığında Olmalıdır.! ")
                ;

            RuleFor(x => x.CreatedDate)
                .LessThanOrEqualTo(DateTime.UtcNow)
                    .WithMessage("Oluşturulma Tarihi Gelecek veya Geçmiş Zaman Olamaz.!")
                ;
        }
    }
}
