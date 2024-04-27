using FluentValidation;

namespace Zajecia6;

public class CreateAnimalRequestValidator : AbstractValidator<CreateAnimalRequest>
{
	public CreateAnimalRequestValidator()
	{
		RuleFor(e => e.Name).MaximumLength(200).NotNull();
		RuleFor(e => e.Description).MaximumLength(200);
		RuleFor(e => e.Category).MaximumLength(200).NotNull();
		RuleFor(e => e.Area).MaximumLength(200).NotNull();
	}
}
public class EditAnimalRequestValidator : AbstractValidator<EditAnimalRequest>
{
	public EditAnimalRequestValidator()
	{
		RuleFor(e => e.Name).MaximumLength(200).NotNull();
		RuleFor(e => e.Description).MaximumLength(200);
		RuleFor(e => e.Category).MaximumLength(200).NotNull();
		RuleFor(e => e.Area).MaximumLength(200).NotNull();
	}
}