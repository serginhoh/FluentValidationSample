using FluentValidation;
using FluentValidationSample.Models;

namespace FluentValidationSample.Validators;

public class PersonValidator : AbstractValidator<Person>
{
    public PersonValidator()
    {
        //Name não pode ser Null ou Vazio
        RuleFor(r => r.Name).NotNull().NotEmpty();

        //Deve ser maior de 18 anos
        RuleFor(r => r.BirthDate).LessThan(DateTime.Now.AddYears(-18))
            .WithMessage("Menor de 18 anos.");
    }
}
