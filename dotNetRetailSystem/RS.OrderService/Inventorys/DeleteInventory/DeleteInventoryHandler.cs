using FluentValidation;
using Marten;
using RS.CommonLibrary.CQRS;
using RS.OrderService.Models;

namespace RS.OrderService.Inventorys.DeleteInventory
{
    public record DeleteInventoryCommand(Guid Id) : ICommand<DeleteInventoryResult>;

    public record DeleteInventoryResult(bool IsSuccess);

    public class DeleteInventoryCommandValidator : AbstractValidator<DeleteInventoryCommand>
    {
        public DeleteInventoryCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Inventory ID is required");
        }
    }

    internal class DeleteInventoryHandler(IDocumentSession session) : ICommandHandler<DeleteInventoryCommand, DeleteInventoryResult>
    {
        public async Task<DeleteInventoryResult> Handle(DeleteInventoryCommand request, CancellationToken cancellationToken)
        {
            session.Delete<Inventory>(request.Id);
            await session.SaveChangesAsync(cancellationToken);

            return new DeleteInventoryResult(true);
        }
    }
}
