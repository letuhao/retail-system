using FluentValidation;
using MongoDB;
using MongoDB.Driver;
using RS.CommonLibrary.CQRS;
using RS.ProductService.Models;

namespace RS.ProductService.Products.CreateProduct
{
    public record CreateProductCommand(CreateProductCommandArgs Args) : ICommand<CreateProductResult>;

    public record CreateProductResult(Guid Id);

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(command => command.Args.Name)
                .NotEmpty().WithMessage("Name is required")
                .Length(2, 150).WithMessage("Name must be between 2 and 150 characters");

            RuleFor(command => command.Args.Category)
                .NotEmpty().WithMessage("Category is required");

            RuleFor(command => command.Args.Description)
                .NotEmpty().WithMessage("Description is required");

            RuleFor(command => command.Args.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0");

            RuleFor(command => command.Args.ShopId)
                .NotEmpty().WithMessage("ShopId is required");
        }
    }

    internal class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        private readonly IMongoCollection<Product> _products;

        public CreateProductCommandHandler(IMongoCollection<Product> products)
        {
            _products = products;
        }

        public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            //create Product entity from command object
            //save to database
            //return CreateProductResult result               

            var product = new Product
            {
                Name = request.Args.Name,
                Category = request.Args.Category,
                Description = request.Args.Description,
                ImageFile = request.Args.ImageFile,
                Price = request.Args.Price,
                ShopId = request.Args.ShopId,
            };

            //save to database
            var insertOptions = new InsertOneOptions
            {
                BypassDocumentValidation = false
            };

            await _products.InsertOneAsync(
                product,
                insertOptions,
                cancellationToken);

            //return result
            return new CreateProductResult(product.Id);
        }
    }
}
