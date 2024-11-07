using Marten;
using RS.CommonLibrary.CQRS;
using RS.OrderService.Exceptions;
using RS.OrderService.Models;

namespace RS.OrderService.CartItems.GetCartItemById
{
    public record GetCartItemByIdQuery(Guid Id) : IQuery<GetCartItemByIdResult>;

    public record GetCartItemByIdResult(CartItem cartItem);

    internal class GetCartItemByIdHandler(IDocumentSession session) : IQueryHandler<GetCartItemByIdQuery, GetCartItemByIdResult>
    {
        public async Task<GetCartItemByIdResult> Handle(GetCartItemByIdQuery request, CancellationToken cancellationToken)
        {
            var cartItem = await session.LoadAsync<CartItem>(request.Id, cancellationToken);

            if (cartItem is null)
            {
                throw new CartItemNotFoundException(request.Id);
            }

            return new GetCartItemByIdResult(cartItem);
        }
    }
}
