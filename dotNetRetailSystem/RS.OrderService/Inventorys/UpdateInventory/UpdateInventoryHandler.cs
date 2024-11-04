using FluentValidation;
using Marten;
using RS.CommonLibrary.CQRS;
using RS.OrderService.Exceptions;
using RS.OrderService.Models;

namespace RS.OrderService.Inventorys.UpdateInventory
{
    public record UpdateInventoryCommand(UpdateInventoryCommandArgs Args) : ICommand<UpdateInventoryResult>;

    public record UpdateInventoryResult(bool IsSuccess);

    public class UpdateInventoryCommandValidator : AbstractValidator<UpdateInventoryCommand>
    {
        public UpdateInventoryCommandValidator()
        {
            RuleFor(command => command.Args.Quantity)
                .NotEmpty().WithMessage("Quantity is required");

            RuleFor(command => command.Args.UnitPrice)
                .NotEmpty().WithMessage("UnitPrice is required");

            RuleFor(command => command.Args.ProductId)
                .NotEmpty().WithMessage("ProductId is required");

            RuleFor(command => command.Args.ShopId)
                .NotEmpty().WithMessage("ShopId is required");
        }
    }

    internal class UpdateInventoryHandler(IDocumentSession session) : ICommandHandler<UpdateInventoryCommand, UpdateInventoryResult>
    {
        public async Task<UpdateInventoryResult> Handle(UpdateInventoryCommand request, CancellationToken cancellationToken)
        {
            var Inventory = await session.LoadAsync<Inventory>(request.Args.Id, cancellationToken);

            if (Inventory is null)
            {
                throw new InventoryNotFoundException(request.Args.Id);
            }

            Inventory.Quantity = request.Args.Quantity;
            Inventory.UnitPrice = request.Args.UnitPrice;

            session.Update(Inventory);
            await session.SaveChangesAsync(cancellationToken);

            return new UpdateInventoryResult(true);
        }
    }
}
