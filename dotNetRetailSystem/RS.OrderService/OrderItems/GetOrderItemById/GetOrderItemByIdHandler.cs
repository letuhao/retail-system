using Marten;
using RS.CommonLibrary.CQRS;
using RS.OrderService.Exceptions;
using RS.OrderService.Models;

namespace RS.OrderService.OrderItems.GetOrderItemById
{
    public record GetOrderItemByIdQuery(Guid Id) : IQuery<GetOrderItemByIdResult>;

    public record GetOrderItemByIdResult(OrderItem OrderItem);

    internal class GetOrderItemByIdHandler(IDocumentSession session) : IQueryHandler<GetOrderItemByIdQuery, GetOrderItemByIdResult>
    {
        public async Task<GetOrderItemByIdResult> Handle(GetOrderItemByIdQuery request, CancellationToken cancellationToken)
        {
            var OrderItem = await session.LoadAsync<OrderItem>(request.Id, cancellationToken);

            if (OrderItem is null)
            {
                throw new OrderItemNotFoundException(request.Id);
            }

            return new GetOrderItemByIdResult(OrderItem);
        }
    }
}
