using FluentValidation;
using Marten;
using RS.CommonLibrary.CQRS;
using RS.OrderService.Models;

namespace RS.OrderService.OrderItems.CreateOrderItem
{
    public record CreateOrderItemCommand(CreateOrderItemCommandArgs Args) : ICommand<CreateOrderItemResult>;

    public record CreateOrderItemResult(Guid Id);

    public class CreateOrderItemCommandValidator : AbstractValidator<CreateOrderItemCommand>
    {
        public CreateOrderItemCommandValidator()
        {
            RuleFor(command => command.Args.Quantity)
                .NotEmpty().WithMessage("Quantity is required");

            RuleFor(command => command.Args.UnitPrice)
                .NotEmpty().WithMessage("UnitPrice is required");

            RuleFor(command => command.Args.TotalPrice)
                .NotEmpty().WithMessage("TotalPrice is required");

            RuleFor(command => command.Args.ProductName)
                .NotEmpty().WithMessage("ProductName is required");

            RuleFor(command => command.Args.ProductId)
                .NotEmpty().WithMessage("ProductId is required");

            RuleFor(command => command.Args.OrderId)
                .NotEmpty().WithMessage("OrderId is required");
        }
    }

    internal class CreateOrderItemCommandHandler(IDocumentSession session) : ICommandHandler<CreateOrderItemCommand, CreateOrderItemResult>
    {
        public async Task<CreateOrderItemResult> Handle(CreateOrderItemCommand request, CancellationToken cancellationToken)
        {
            //create OrderItem entity from command object
            //save to database
            //return CreateOrderItemResult result               

            var OrderItem = new OrderItem
            {
                Quantity = request.Args.Quantity,
                UnitPrice = request.Args.UnitPrice,
                TotalPrice = request.Args.TotalPrice,
                ProductName = request.Args.ProductName,
                ProductId = request.Args.ProductId,
                OrderId = request.Args.OrderId
            };

            //save to database
            session.Store(OrderItem);
            await session.SaveChangesAsync(cancellationToken);

            //return result
            return new CreateOrderItemResult(OrderItem.Id);
        }
    }
}
