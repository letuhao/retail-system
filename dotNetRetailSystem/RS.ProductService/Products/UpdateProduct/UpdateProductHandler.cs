using FluentValidation;
using MongoDB.Driver;
using RS.CommonLibrary.CQRS;
using RS.ProductService.Exceptions;
using RS.ProductService.Models;

namespace RS.ProductService.Products.UpdateProduct
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

    internal class UpdateProductHandler : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        private readonly IMongoCollection<Product> _products;

        public UpdateProductHandler(IMongoCollection<Product> products)
        {
            _products = products;
        }

        public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, request.Args.Id);

            var product = await _products.Find(filter).FirstOrDefaultAsync();

            if (product == null)
            {
                throw new ProductNotFoundException(request.Args.Id);
            }

            var update = Builders<Product>.Update.Set(p => p.Name, request.Args.Name)
                    .Set(p => p.Category, request.Args.Category)
                    .Set(p => p.Description, request.Args.Description)
                    .Set(p => p.ImageFile, request.Args.ImageFile)
                    .Set(p => p.Price, request.Args.Price);

            var updateOptions = new UpdateOptions
            {
                IsUpsert = false
            };

            await _products.UpdateOneAsync(filter, update, updateOptions, cancellationToken);

            return new UpdateProductResult(true);
        }
    }
}
