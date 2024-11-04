using Marten;
using RS.CommonLibrary.CQRS;
using RS.OrderService.Exceptions;
using RS.OrderService.Models;

namespace RS.OrderService.Inventorys.GetInventoryById
{
    public record GetInventoryByIdQuery(Guid Id) : IQuery<GetInventoryByIdResult>;

    public record GetInventoryByIdResult(Inventory Inventory);

    internal class GetInventoryByIdHandler(IDocumentSession session) : IQueryHandler<GetInventoryByIdQuery, GetInventoryByIdResult>
    {
        public async Task<GetInventoryByIdResult> Handle(GetInventoryByIdQuery request, CancellationToken cancellationToken)
        {
            var Inventory = await session.LoadAsync<Inventory>(request.Id, cancellationToken);

            if (Inventory is null)
            {
                throw new InventoryNotFoundException(request.Id);
            }

            return new GetInventoryByIdResult(Inventory);
        }
    }
}
