using FluentValidation;
using Marten;
using RS.CommonLibrary.CQRS;
using RS.OrderService.Models;

namespace RS.OrderService.CartItems.CreateCartItem
{
    public record CreateCartItemCommand(CreateCartItemCommandArgs Args) : ICommand<CreateCartItemResult>;

    public record CreateCartItemResult(Guid Id);

    public class CreateCartItemCommandValidator : AbstractValidator<CreateCartItemCommand>
    {
        public CreateCartItemCommandValidator()
        {
            RuleFor(command => command.Args.CartId)
                .NotEmpty().WithMessage("CartId is required");

            RuleFor(command => command.Args.ProductId)
                .NotEmpty().WithMessage("ProductId is required");

            RuleFor(command => command.Args.UserId)
                .NotEmpty().WithMessage("UserId is required");
        }
    }

    internal class CreateCartItemCommandHandler(IDocumentSession session) : ICommandHandler<CreateCartItemCommand, CreateCartItemResult>
    {
        public async Task<CreateCartItemResult> Handle(CreateCartItemCommand request, CancellationToken cancellationToken)
        {
            //create CartItem entity from command object
            //save to database
            //return CreateCartItemResult result               

            var CartItem = new CartItem
            {
                Quantity = request.Args.Quantity,
                UnitPrice = request.Args.UnitPrice,
                Total = request.Args.Total,
                SubTotal = request.Args.SubTotal,
                ProductId = request.Args.ProductId,
                CartId = request.Args.CartId,
                UserId = request.Args.UserId
            };

            //save to database
            session.Store(CartItem);
            await session.SaveChangesAsync(cancellationToken);

            //return result
            return new CreateCartItemResult(CartItem.Id);
        }
    }
}
