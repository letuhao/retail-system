using FluentValidation;
using Marten;
using RS.CommonLibrary.CQRS;
using RS.ShopService.Models;

namespace RS.ShopService.Shops.DeleteShop
{
    public record DeleteShopCommand(Guid Id) : ICommand<DeleteShopResult>;

    public record DeleteShopResult(bool IsSuccess);

    public class DeleteShopCommandValidator : AbstractValidator<DeleteShopCommand>
    {
        public DeleteShopCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Shop ID is required");
        }
    }

    internal class DeleteShopHandler(IDocumentSession session) : ICommandHandler<DeleteShopCommand, DeleteShopResult>
    {
        public async Task<DeleteShopResult> Handle(DeleteShopCommand request, CancellationToken cancellationToken)
        {
            session.Delete<Shop>(request.Id);
            await session.SaveChangesAsync(cancellationToken);

            return new DeleteShopResult(true);
        }
    }
}
