using Marten;
using RS.CommonLibrary.CQRS;
using RS.OrderService.Exceptions;
using RS.OrderService.Models;

namespace RS.OrderService.ShoppingCarts.GetCartById
{
    public record GetCartByIdQuery(Guid Id) : IQuery<GetCartByIdResult>;

    public record GetCartByIdResult(ShoppingCart Cart);

    internal class GetCartByIdHandler(IDocumentSession session) : IQueryHandler<GetCartByIdQuery, GetCartByIdResult>
    {
        public async Task<GetCartByIdResult> Handle(GetCartByIdQuery request, CancellationToken cancellationToken)
        {
            var Cart = await session.LoadAsync<ShoppingCart>(request.Id, cancellationToken);

            if (Cart is null)
            {
                throw new ShoppingCartNotFoundException(request.Id);
            }

            return new GetCartByIdResult(Cart);
        }
    }
}
