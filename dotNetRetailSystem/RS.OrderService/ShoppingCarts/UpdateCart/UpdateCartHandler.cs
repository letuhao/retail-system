using FluentValidation;
using Marten;
using RS.CommonLibrary.CQRS;
using RS.OrderService.Exceptions;
using RS.OrderService.Models;

namespace RS.OrderService.ShoppingCarts.UpdateCart
{
    public record UpdateCartCommand(UpdateCartCommandArgs Args) : ICommand<UpdateCartResult>;

    public record UpdateCartResult(bool IsSuccess);

    public class UpdateCartCommandValidator : AbstractValidator<UpdateCartCommand>
    {
        public UpdateCartCommandValidator()
        {
            RuleFor(command => command.Args.Id)
                .NotEmpty().WithMessage("Cart ID is required");

            RuleFor(command => command.Args.ExpiresDate)
                .NotEmpty().WithMessage("ExpiresDate is required");

            RuleFor(command => command.Args.Total)
                .NotEmpty().WithMessage("Description is required");

            RuleFor(command => command.Args.TotalItem)
                .NotEmpty().WithMessage("TotalItem is required");
        }
    }

    internal class UpdateCartHandler(IDocumentSession session) : ICommandHandler<UpdateCartCommand, UpdateCartResult>
    {
        public async Task<UpdateCartResult> Handle(UpdateCartCommand request, CancellationToken cancellationToken)
        {
            var cart = await session.LoadAsync<ShoppingCart>(request.Args.Id, cancellationToken);

            if (cart is null)
            {
                throw new ShoppingCartNotFoundException(request.Args.Id);
            }

            cart.ExpiresDate = request.Args.ExpiresDate;
            cart.Total = request.Args.Total;
            cart.TotalItem = request.Args.TotalItem;

            session.Update(cart);
            await session.SaveChangesAsync(cancellationToken);

            return new UpdateCartResult(true);
        }
    }
}
