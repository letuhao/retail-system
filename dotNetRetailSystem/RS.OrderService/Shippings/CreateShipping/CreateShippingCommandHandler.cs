using FluentValidation;
using Marten;
using RS.CommonLibrary.CQRS;
using RS.OrderService.Models;

namespace RS.OrderService.Shippings.CreateShipping
{
    public record CreateShippingCommand(CreateShippingCommandArgs Args) : ICommand<CreateShippingResult>;

    public record CreateShippingResult(Guid Id);

    public class CreateShippingCommandValidator : AbstractValidator<CreateShippingCommand>
    {
        public CreateShippingCommandValidator()
        {
            RuleFor(command => command.Args.TrackingNumber)
                .NotEmpty().WithMessage("TrackingNumber is required");

            RuleFor(command => command.Args.StartDate)
                .NotEmpty().WithMessage("StartDate is required");

            RuleFor(command => command.Args.EstimatedDate)
                .NotEmpty().WithMessage("EstimatedDate is required");

            RuleFor(command => command.Args.DeliveredDate)
                .NotEmpty().WithMessage("DeliveredDate is required");

            RuleFor(command => command.Args.ShippingMethod)
                .NotEmpty().WithMessage("ShippingMethod is required");

            RuleFor(command => command.Args.ShippingAddress)
                .NotEmpty().WithMessage("ShippingAddress is required");

            RuleFor(command => command.Args.RecipentName)
                .NotEmpty().WithMessage("RecipentName is required");

            RuleFor(command => command.Args.RecipentPhone)
                .NotEmpty().WithMessage("RecipentPhone is required");

            RuleFor(command => command.Args.CourierId)
                .NotEmpty().WithMessage("CourierId is required");

            RuleFor(command => command.Args.CourierName)
                .NotEmpty().WithMessage("CourierName is required");

            RuleFor(command => command.Args.OrderId)
                .NotEmpty().WithMessage("OrderId is required");
        }
    }

    internal class CreateShippingCommandHandler(IDocumentSession session) : ICommandHandler<CreateShippingCommand, CreateShippingResult>
    {
        public async Task<CreateShippingResult> Handle(CreateShippingCommand request, CancellationToken cancellationToken)
        {
            //create Shipping entity from command object
            //save to database
            //return CreateShippingResult result               

            var Shipping = new Shipping
            {
                TrackingNumber = request.Args.TrackingNumber,
                StartDate = request.Args.StartDate,
                EstimatedDate = request.Args.EstimatedDate,
                DeliveredDate = request.Args.DeliveredDate,
                ShippingMethod = request.Args.ShippingMethod,
                ShippingAddress = request.Args.ShippingAddress,
                RecipentName = request.Args.RecipentName,
                RecipentPhone = request.Args.RecipentPhone,
                CourierId = request.Args.CourierId,
                CourierName = request.Args.CourierName,
                OrderId = request.Args.OrderId,
            };

            //save to database
            session.Store(Shipping);
            await session.SaveChangesAsync(cancellationToken);

            //return result
            return new CreateShippingResult(Shipping.Id);
        }
    }
}
