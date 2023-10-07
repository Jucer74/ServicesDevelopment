using FluentValidation;
using MoneyBankService.Api.Dto;
using Org.BouncyCastle.Security;
namespace MoneyBankService.Api.Validators;

public class AccountValidator: AbstractValidator<AccountDto>
{
    public AccountValidator()
    {
        // TODO: Implement the Validation Rules
        RuleFor(m => m.AccountType)
            .NotEmpty()
            .WithMessage("The AccountType is required.");

        RuleFor(m => m.CreationDate)
            .Must(isDate)
            .WithMessage("The CreationDate must be a valid date.");

        RuleFor(m => m.AccountNumber)
            .NotEmpty()
            .WithMessage("The AccountNumber is required.")
            .MaximumLength(10)
            .WithMessage("The AccountNumber has a maximum length of 10 characters.")
            .Matches(@"\d{10}")
            .WithMessage("The AccountNumber only accepts numbers.");

        RuleFor(m => m.OwnerName)
            .NotEmpty()
            .WithMessage("The OwnerName is required.")
            .MaximumLength(100)
            .WithMessage("The OwnerName has a maximum length of 100 characters.");

        RuleFor(m => m.BalanceAmount)
            .NotEmpty()
            .WithMessage("The BalanceAmount is required.");

        RuleFor(m => m.OverdraftAmount)
            .NotEmpty()
            .WithMessage("The OverdraftAmount is required.");

        //RuleFor(m => m.FIELD)
        //    .NotEmpty()
        //    .WithMessage("El campo FIELD es Requerido.")
        //    .MaximumLength(50)
        //    .WithMessage("El campo FIELD tiene una longitud maxima de 10 caracteres")
        //    .Matches(@"\d{10}")
        //    .WithMessage("El campo FIELD Solo Acepta Numeros")
        //    .GreaterThan(0)
        //    .WithMessage("El campo FIELD debe ser mayor a cero");


    }
    private static bool isDate(DateTime date)
    {
        return isYear(date.Year) && isMonth(date.Month) && isDay(date.Day);
    }
    private static bool isDay(int day)
    {
        return day >= 1 && day <= 31;
    }
    private static bool isMonth(int month)
    {
        return month >= 1 && month <= 12;
    }
    private static bool isYear(int year)
    {
        return year >= DateTime.Now.Year;
    }

    

    
}
