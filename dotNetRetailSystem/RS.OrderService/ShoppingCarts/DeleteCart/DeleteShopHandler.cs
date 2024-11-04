using FluentValidation;
using Marten;
using RS.CommonLibrary.CQRS;
using RS.OrderService.Models;

namespace RS.OrderService.ShoppingCarts.DeleteCart
{
    public record DeleteCartCommand(Guid Id) : ICommand<DeleteCartResult>;

    public record DeleteCartResult(bool IsSuccess);

    public class DeleteCartCommandValidator : AbstractValidator<DeleteCartCommand>
    {
        public DeleteCartCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Cart ID is required");
        }
    }

    internal class DeleteCartHandler(IDocumentSession session) : ICommandHandler<DeleteCartCommand, DeleteCartResult>
    {
        public async Task<DeleteCartResult> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
        {
            session.Delete<ShoppingCart>(request.Id);
            await session.SaveChangesAsync(cancellationToken);

            return new DeleteCartResult(true);
        }
    }
}
