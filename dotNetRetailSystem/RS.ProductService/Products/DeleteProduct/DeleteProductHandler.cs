using FluentValidation;
using MongoDB;
using MongoDB.Driver;
using RS.CommonLibrary.CQRS;
using RS.ProductService.Models;

namespace RS.ProductService.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;

    public record DeleteProductResult(bool IsSuccess);

    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Product ID is required");
        }
    }

    internal class DeleteProductHandler : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        private readonly IMongoCollection<Product> _products;

        public DeleteProductHandler(IMongoCollection<Product> products)
        {
            _products = products;
        }

        public async Task<DeleteProductResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, request.Id);
            var deleteOptions = new DeleteOptions
            {
                Hint = null // You can specify an index hint if needed
            };

            var result = await _products.DeleteOneAsync(
                filter,
                deleteOptions,
                cancellationToken);

            return new DeleteProductResult(true);
        }
    }
}
