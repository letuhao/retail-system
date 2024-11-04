using FluentValidation;
using Marten;
using RS.CommonLibrary.CQRS;
using RS.OrderService.Exceptions;
using RS.OrderService.Models;

namespace RS.OrderService.Orders.UpdateOrder
{
    public record UpdateOrderCommand(UpdateOrderCommandArgs Args) : ICommand<UpdateOrderResult>;

    public record UpdateOrderResult(bool IsSuccess);

    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(command => command.Args.Id)
                .NotEmpty().WithMessage("Order ID is required");

            RuleFor(command => command.Args.Status)
                .NotEmpty().WithMessage("Status is required");

            RuleFor(command => command.Args.TotalAmount)
                .NotEmpty().WithMessage("TotalAmount is required");

            RuleFor(command => command.Args.SubTotal)
                .NotEmpty().WithMessage("SubTotal is required");

            RuleFor(command => command.Args.Tax)
                .NotEmpty().WithMessage("Tax is required");

            RuleFor(command => command.Args.ShippingCost)
                .NotEmpty().WithMessage("ShippingCost is required");

            RuleFor(command => command.Args.Discount)
                .NotEmpty().WithMessage("Discount is required");

            RuleFor(command => command.Args.ShippingAddress)
                .NotEmpty().WithMessage("ShippingAddress is required");

            RuleFor(command => command.Args.BillingAddress)
                .NotEmpty().WithMessage("BillingAddress is required");

            RuleFor(command => command.Args.PaymentMethodId)
                .NotEmpty().WithMessage("PaymentMethodId is required");

            RuleFor(command => command.Args.PaymentStatus)
                .NotEmpty().WithMessage("PaymentStatus is required");

            RuleFor(command => command.Args.Note)
                .NotEmpty().WithMessage("Note is required");
        }
    }

    internal class UpdateOrderHandler(IDocumentSession session) : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
    {
        public async Task<UpdateOrderResult> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var Order = await session.LoadAsync<Order>(request.Args.Id, cancellationToken);

            if (Order is null)
            {
                throw new OrderNotFoundException(request.Args.Id);
            }

            Order.Status = request.Args.Status;
            Order.TotalAmount = request.Args.TotalAmount;
            Order.SubTotal = request.Args.SubTotal;
            Order.Tax = request.Args.Tax;
            Order.ShippingCost = request.Args.ShippingCost;
            Order.Discount = request.Args.Discount;
            Order.ShippingAddress = request.Args.ShippingAddress;
            Order.BillingAddress = request.Args.BillingAddress;
            Order.PaymentMethodId = request.Args.PaymentMethodId;
            Order.PaymentStatus = request.Args.PaymentStatus;
            Order.Note = request.Args.Note;

            session.Update(Order);
            await session.SaveChangesAsync(cancellationToken);

            return new UpdateOrderResult(true);
        }
    }
}
