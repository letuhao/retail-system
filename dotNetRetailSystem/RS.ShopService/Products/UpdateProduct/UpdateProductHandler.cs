using FluentValidation;
using Marten;
using RS.CommonLibrary.CQRS;
using RS.ShopService.Exceptions;
using RS.ShopService.Models;

namespace RS.ShopService.Products.UpdateProduct
{
    public record UpdateProductCommand(UpdateProductCommandArgs Args) : ICommand<UpdateProductResult>;

    public record UpdateProductResult(bool IsSuccess);

    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(command => command.Args.Id)
                .NotEmpty().WithMessage("Product ID is required");

            RuleFor(command => command.Args.Name)
                .NotEmpty().WithMessage("Name is required")
                .Length(2, 150).WithMessage("Name must be between 2 and 150 characters");

            RuleFor(command => command.Args.Category)
                .NotEmpty().WithMessage("Category is required");

            RuleFor(command => command.Args.Description)
                .NotEmpty().WithMessage("Description is required");

            RuleFor(command => command.Args.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }

    internal class UpdateProductHandler(IDocumentSession session) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(request.Args.Id, cancellationToken);

            if (product is null)
            {
                throw new ProductNotFoundException(request.Args.Id);
            }

            product.Name = request.Args.Name;
            product.Category = request.Args.Category;
            product.Description = request.Args.Description;
            product.ImageFile = request.Args.ImageFile;
            product.Price = request.Args.Price;

            session.Update(product);
            await session.SaveChangesAsync(cancellationToken);

            return new UpdateProductResult(true);
        }
    }
}
