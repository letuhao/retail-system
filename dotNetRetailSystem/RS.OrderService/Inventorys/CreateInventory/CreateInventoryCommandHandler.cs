using FluentValidation;
using Marten;
using RS.CommonLibrary.CQRS;
using RS.OrderService.Models;

namespace RS.OrderService.Inventorys.CreateInventory
{
    public record CreateInventoryCommand(CreateInventoryCommandArgs Args) : ICommand<CreateInventoryResult>;

    public record CreateInventoryResult(Guid Id);

    public class CreateInventoryCommandValidator : AbstractValidator<CreateInventoryCommand>
    {
        public CreateInventoryCommandValidator()
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

    internal class CreateInventoryCommandHandler(IDocumentSession session) : ICommandHandler<CreateInventoryCommand, CreateInventoryResult>
    {
        public async Task<CreateInventoryResult> Handle(CreateInventoryCommand request, CancellationToken cancellationToken)
        {
            //create Inventory entity from command object
            //save to database
            //return CreateInventoryResult result               

            var Inventory = new Inventory
            {
                Quantity = request.Args.Quantity,
                UnitPrice = request.Args.UnitPrice,
                ProductId = request.Args.ProductId,
                ShopId = request.Args.ShopId
            };

            //save to database
            session.Store(Inventory);
            await session.SaveChangesAsync(cancellationToken);

            //return result
            return new CreateInventoryResult(Inventory.Id);
        }
    }
}
