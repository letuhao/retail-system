using FluentValidation;
using Marten;
using RS.CommonLibrary.CQRS;
using RS.OrderService.Exceptions;
using RS.OrderService.Models;

namespace RS.OrderService.CartItems.UpdateCartItem
{
    public record UpdateCartItemCommand(UpdateCartItemCommandArgs Args) : ICommand<UpdateCartItemResult>;

    public record UpdateCartItemResult(bool IsSuccess);

    public class UpdateCartItemCommandValidator : AbstractValidator<UpdateCartItemCommand>
    {
        public UpdateCartItemCommandValidator()
        {
            RuleFor(command => command.Args.Id)
                .NotEmpty().WithMessage("CartItem ID is required");

            RuleFor(command => command.Args.ProductId)
                .NotEmpty().WithMessage("ProductId is required");
        }
    }

    internal class UpdateCartItemHandler(IDocumentSession session) : ICommandHandler<UpdateCartItemCommand, UpdateCartItemResult>
    {
        public async Task<UpdateCartItemResult> Handle(UpdateCartItemCommand request, CancellationToken cancellationToken)
        {
            var CartItem = await session.LoadAsync<CartItem>(request.Args.Id, cancellationToken);

            if (CartItem is null)
            {
                throw new CartItemNotFoundException(request.Args.Id);
            }

            CartItem.Quantity = request.Args.Quantity;
            CartItem.Total = request.Args.Total;
            CartItem.SubTotal = request.Args.SubTotal;
            CartItem.UnitPrice = request.Args.UnitPrice;
            CartItem.ProductId = request.Args.ProductId;

            session.Update(CartItem);
            await session.SaveChangesAsync(cancellationToken);

            return new UpdateCartItemResult(true);
        }
    }
}
