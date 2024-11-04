using FluentValidation;
using Marten;
using RS.CommonLibrary.CQRS;
using RS.OrderService.Exceptions;
using RS.OrderService.Models;

namespace RS.OrderService.Shippings.UpdateShipping
{
    public record UpdateShippingCommand(UpdateShippingCommandArgs Args) : ICommand<UpdateShippingResult>;

    public record UpdateShippingResult(bool IsSuccess);

    public class UpdateShippingCommandValidator : AbstractValidator<UpdateShippingCommand>
    {
        public UpdateShippingCommandValidator()
        {
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
        }
    }

    internal class UpdateShippingHandler(IDocumentSession session) : ICommandHandler<UpdateShippingCommand, UpdateShippingResult>
    {
        public async Task<UpdateShippingResult> Handle(UpdateShippingCommand request, CancellationToken cancellationToken)
        {
            var Shipping = await session.LoadAsync<Shipping>(request.Args.Id, cancellationToken);

            if (Shipping is null)
            {
                throw new ShippingNotFoundException(request.Args.Id);
            }

            Shipping.StartDate = request.Args.StartDate;
            Shipping.EstimatedDate = request.Args.EstimatedDate;
            Shipping.DeliveredDate = request.Args.DeliveredDate;
            Shipping.ShippingMethod = request.Args.ShippingMethod;
            Shipping.ShippingAddress = request.Args.ShippingAddress;
            Shipping.RecipentName = request.Args.RecipentName;
            Shipping.RecipentPhone = request.Args.RecipentPhone;
            Shipping.CourierName = request.Args.CourierName;
            Shipping.CourierId = request.Args.CourierId;

            session.Update(Shipping);
            await session.SaveChangesAsync(cancellationToken);

            return new UpdateShippingResult(true);
        }
    }
}
