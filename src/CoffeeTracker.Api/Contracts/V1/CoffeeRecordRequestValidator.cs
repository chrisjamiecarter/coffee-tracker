using FluentValidation;

namespace CoffeeTracker.Api.Contracts.V1;

public class CoffeeRecordRequestValidator : AbstractValidator<CoffeeRecordRequest>
{
    public CoffeeRecordRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Date).NotEmpty();
    }
}
