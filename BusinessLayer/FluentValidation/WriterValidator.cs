using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessLayer.FluentValidation
{
    public class WriterValidator : AbstractValidator<Writer>
    {
        public WriterValidator()
        {
            RuleFor(x => x.WriterName).NotEmpty().WithMessage("Yazar adını boş geçemezsiniz");
            RuleFor(x => x.WriterName).MinimumLength(2).WithMessage("Lütfen en az 2 karakter giriniz");
            RuleFor(x => x.WriterSurName).NotEmpty().WithMessage("Yazar soy adını boş geçemezsiniz");
            RuleFor(x => x.WriterAbout).NotEmpty().WithMessage("Yazar hakkında kısmını boş geçemezsiniz");
            RuleFor(x => x.WriterAbout).MaximumLength(50).WithMessage("Lütfen en fazla 50 karakter degeri giriniz");
            RuleFor(x => x.WriterAbout).Must(IsAboutValid).WithMessage("En az bir tane a harfi içeren bir değer giriniz");
        }
        private bool IsAboutValid(string arg)
        {
            Regex regex = new Regex(@"^(?=.*[a,A])");
            return regex.IsMatch(arg);
        }
    }
}