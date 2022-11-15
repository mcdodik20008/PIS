using FluentValidation;
using PISWF.domain.registermc.model.entity;

namespace PISWF.domain.model.validator;

public class RegistermcValidator : AbstractValidator<RegisterMC>
{
    public RegistermcValidator()
    {
        var msg = "Ошибка в поле {PropertyName}: значение {PropertyValue}";
 
        RuleFor(c => c.Id)
            .Must(c => c > 0).WithMessage(msg + " должно быть больше 0");
        
        RuleFor(c => c.Number)
            .Must(c => c.Length > 0).WithMessage(msg + " должно быть больше 0");

        RuleFor(c => c)
            .Must(c => c.Municipality is not null || c.Municipality is not null)
            .WithMessage("МО или организация должна быть не null");
        
        RuleFor(c => c.Year)
            .Must(c => c > 0).WithMessage(msg + " должно быть больше 0");

        RuleFor(c => c.Price)
            .Must(c => c > 0).WithMessage(msg + " должно быть больше 0");

        RuleFor(c => c.SubventionShare)
            .Must(c => c > 0).WithMessage(msg + " должно быть больше 0");
        
        RuleFor(c => c.AmountMoney)
            .Must(c => c > 0).WithMessage(msg + " должно быть больше 0");
        
        RuleFor(c => c.ShareFundsSubvention)
            .Must(c => c > 0).WithMessage(msg + " должно быть больше 0");
    }
}