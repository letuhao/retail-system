using FluentValidation;
using Marten;
using RS.CommonLibrary.CQRS;
using RS.OrderService.Models;

namespace RS.OrderService.Orders.CreateOrder
{
    public record CreateOrderCommand(CreateOrderCommandArgs Args) : ICommand<CreateOrderResult>;

    public record CreateOrderResult(Guid Id);

    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(command => command.Args.OrderNumber)
                .NotEmpty().WithMessage("OrderNumber is required");

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

            RuleFor(command => command.Args.UserId)
                .NotEmpty().WithMessage("UserId is required");

            RuleFor(command => command.Args.ShopId)
                .NotEmpty().WithMessage("ShopId is required");
        }
    }

    internal class CreateOrderCommandHandler(IDocumentSession session) : ICommandHandler<CreateOrderCommand, CreateOrderResult>
    {
        public async Task<CreateOrderResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            //create Order entity from command object
            //save to database
            //return CreateOrderResult result               

            var Order = new Order
            {
                OrderNumber = request.Args.OrderNumber,
                Status = request.Args.Status,
                TotalAmount = request.Args.TotalAmount,
                SubTotal = request.Args.SubTotal,
                Tax = request.Args.Tax,
                ShippingCost = request.Args.ShippingCost,
                Discount = request.Args.Discount,
                ShippingAddress = request.Args.ShippingAddress,
                BillingAddress = request.Args.BillingAddress,
                PaymentMethodId = request.Args.PaymentMethodId,
                PaymentStatus = request.Args.PaymentStatus,
                Note = request.Args.Note,
                UserId = request.Args.UserId,
                ShopId = request.Args.ShopId
            };

            //save to database
            session.Store(Order);
            await session.SaveChangesAsync(cancellationToken);

            //return result
            return new CreateOrderResult(Order.Id);
        }
    }
}
