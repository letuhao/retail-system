using Marten;
using RS.CommonLibrary.CQRS;
using RS.OrderService.Exceptions;
using RS.OrderService.Models;

namespace RS.OrderService.Shippings.GetShippingById
{
    public record GetShippingByIdQuery(Guid Id) : IQuery<GetShippingByIdResult>;

    public record GetShippingByIdResult(Shipping Shipping);

    internal class GetShippingByIdHandler(IDocumentSession session) : IQueryHandler<GetShippingByIdQuery, GetShippingByIdResult>
    {
        public async Task<GetShippingByIdResult> Handle(GetShippingByIdQuery request, CancellationToken cancellationToken)
        {
            var Shipping = await session.LoadAsync<Shipping>(request.Id, cancellationToken);

            if (Shipping is null)
            {
                throw new ShippingNotFoundException(request.Id);
            }

            return new GetShippingByIdResult(Shipping);
        }
    }
}
