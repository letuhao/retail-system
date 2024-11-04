using Marten;
using RS.CommonLibrary.CQRS;
using RS.OrderService.Models;

namespace RS.OrderService.CartItems.GetCartItemByCartId
{
    public record GetCartItemByCartIdQuery(Guid CartId) : IQuery<GetCartItemByCartIdResult>;

    public record GetCartItemByCartIdResult(IEnumerable<CartItem> CartItems);

    internal class GetCartItemByCartIdQueryHandler(IDocumentSession session) : IQueryHandler<GetCartItemByCartIdQuery, GetCartItemByCartIdResult>
    {
        public async Task<GetCartItemByCartIdResult> Handle(GetCartItemByCartIdQuery request, CancellationToken cancellationToken)
        {
            var cart = await session
                .Query<CartItem>()
                .Where(p => p.CartId == request.CartId)
                .ToListAsync(cancellationToken);

            return new GetCartItemByCartIdResult(cart);
        }
    }
}
