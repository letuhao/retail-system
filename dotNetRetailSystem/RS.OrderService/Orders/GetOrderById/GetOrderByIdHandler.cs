using Marten;
using RS.CommonLibrary.CQRS;
using RS.OrderService.Exceptions;
using RS.OrderService.Models;

namespace RS.OrderService.Orders.GetOrderById
{
    public record GetOrderByIdQuery(Guid Id) : IQuery<GetOrderByIdResult>;

    public record GetOrderByIdResult(Order Order);

    internal class GetOrderByIdHandler(IDocumentSession session) : IQueryHandler<GetOrderByIdQuery, GetOrderByIdResult>
    {
        public async Task<GetOrderByIdResult> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var Order = await session.LoadAsync<Order>(request.Id, cancellationToken);

            if (Order is null)
            {
                throw new OrderNotFoundException(request.Id);
            }

            return new GetOrderByIdResult(Order);
        }
    }
}
