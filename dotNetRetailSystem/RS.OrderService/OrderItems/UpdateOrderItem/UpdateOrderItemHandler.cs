using FluentValidation;
using Marten;
using RS.CommonLibrary.CQRS;
using RS.OrderService.Exceptions;
using RS.OrderService.Models;

namespace RS.OrderService.OrderItems.UpdateOrderItem
{
    public record UpdateOrderItemCommand(UpdateOrderItemCommandArgs Args) : ICommand<UpdateOrderItemResult>;

    public record UpdateOrderItemResult(bool IsSuccess);

    public class UpdateOrderItemCommandValidator : AbstractValidator<UpdateOrderItemCommand>
    {
        public UpdateOrderItemCommandValidator()
        {
            RuleFor(command => command.Args.Quantity)
                .NotEmpty().WithMessage("Quantity is required");

            RuleFor(command => command.Args.UnitPrice)
                .NotEmpty().WithMessage("UnitPrice is required");

            RuleFor(command => command.Args.TotalPrice)
                .NotEmpty().WithMessage("TotalPrice is required");

            RuleFor(command => command.Args.ProductName)
                .NotEmpty().WithMessage("ProductName is required");
        }
    }

    internal class UpdateOrderItemHandler(IDocumentSession session) : ICommandHandler<UpdateOrderItemCommand, UpdateOrderItemResult>
    {
        public async Task<UpdateOrderItemResult> Handle(UpdateOrderItemCommand request, CancellationToken cancellationToken)
        {
            var OrderItem = await session.LoadAsync<OrderItem>(request.Args.Id, cancellationToken);

            if (OrderItem is null)
            {
                throw new OrderItemNotFoundException(request.Args.Id);
            }

            OrderItem.Quantity = request.Args.Quantity;
            OrderItem.UnitPrice = request.Args.UnitPrice;
            OrderItem.TotalPrice = request.Args.TotalPrice;
            OrderItem.ProductName = request.Args.ProductName;

            session.Update(OrderItem);
            await session.SaveChangesAsync(cancellationToken);

            return new UpdateOrderItemResult(true);
        }
    }
}
