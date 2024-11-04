using FluentValidation;
using Marten;
using RS.CommonLibrary.CQRS;
using RS.OrderService.Models;

namespace RS.OrderService.Orders.DeleteOrder
{
    public record DeleteOrderCommand(Guid Id) : ICommand<DeleteOrderResult>;

    public record DeleteOrderResult(bool IsSuccess);

    public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
    {
        public DeleteOrderCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Order ID is required");
        }
    }

    internal class DeleteOrderHandler(IDocumentSession session) : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
    {
        public async Task<DeleteOrderResult> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            session.Delete<Order>(request.Id);
            await session.SaveChangesAsync(cancellationToken);

            return new DeleteOrderResult(true);
        }
    }
}
