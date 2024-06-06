using FluentValidation;

namespace Zajecia10;

public class PostProductModelValidator : AbstractValidator<PostProductRequestModel>
{
    public PostProductModelValidator()
    {
        RuleFor(e => e.ProductName).MaximumLength(100).NotEmpty();
        RuleFor(e => e.ProductCategories).NotEmpty();
        RuleFor(e => e.ProductDepth).NotNull();
        RuleFor(e => e.ProductHeight).NotNull();
        RuleFor(e => e.ProductWeight).NotNull();
        RuleFor(e => e.ProductWidth).NotNull();
    }
}