using FluentValidation;

namespace NasaBrowser.Application.Queries.GroupAsteroids;

public class GroupAsteroidsQueryValidator : AbstractValidator<GroupAsteroidsQuery>
{
    public GroupAsteroidsQueryValidator()
    {
        RuleFor(v => v.GroupRequest)
            .Must(s =>
            {
                int currentYear = DateTime.UtcNow.Year;

                return (s.StartYear is null || s.StartYear < currentYear)
                       && (s.EndYear is null || s.EndYear < currentYear);
            })
            .WithMessage("The start or end year must be greater than the current year.")
            .WithErrorCode("InvalidYear")
            .Must(s =>
            {
                return (s.StartYear is null || s.EndYear is null) || (s.StartYear <= s.EndYear);
            })
            .WithMessage("The start year must be less than or equal to the end year.")
            .WithErrorCode("InvalidYear");
    }
}