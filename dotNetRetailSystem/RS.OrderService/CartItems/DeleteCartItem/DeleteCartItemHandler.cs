using FluentValidation;
using Marten;
using RS.CommonLibrary.CQRS;
using RS.OrderService.Models;

namespace RS.OrderService.CartItems.DeleteCartItem
{
    public record DeleteCartItemCommand(Guid Id) : ICommand<DeleteCartItemResult>;

    public record DeleteCartItemResult(bool IsSuccess);

    public class DeleteCartItemCommandValidator : AbstractValidator<DeleteCartItemCommand>
    {
        public DeleteCartItemCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("CartItem ID is required");
        }
    }

    internal class DeleteCartItemHandler(IDocumentSession session) : ICommandHandler<DeleteCartItemCommand, DeleteCartItemResult>
    {
        public async Task<DeleteCartItemResult> Handle(DeleteCartItemCommand request, CancellationToken cancellationToken)
        {
            session.Delete<CartItem>(request.Id);
            await session.SaveChangesAsync(cancellationToken);

            return new DeleteCartItemResult(true);
        }
    }
}
