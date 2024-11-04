using FluentValidation;
using Marten;
using RS.CommonLibrary.CQRS;
using RS.OrderService.Models;

namespace RS.OrderService.OrderItems.DeleteOrderItem
{
    public record DeleteOrderItemCommand(Guid Id) : ICommand<DeleteOrderItemResult>;

    public record DeleteOrderItemResult(bool IsSuccess);

    public class DeleteOrderItemCommandValidator : AbstractValidator<DeleteOrderItemCommand>
    {
        public DeleteOrderItemCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("OrderItem ID is required");
        }
    }

    internal class DeleteOrderItemHandler(IDocumentSession session) : ICommandHandler<DeleteOrderItemCommand, DeleteOrderItemResult>
    {
        public async Task<DeleteOrderItemResult> Handle(DeleteOrderItemCommand request, CancellationToken cancellationToken)
        {
            session.Delete<OrderItem>(request.Id);
            await session.SaveChangesAsync(cancellationToken);

            return new DeleteOrderItemResult(true);
        }
    }
}
