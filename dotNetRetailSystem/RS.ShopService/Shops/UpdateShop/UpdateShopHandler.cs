using FluentValidation;
using Marten;
using RS.CommonLibrary.CQRS;
using RS.ShopService.Exceptions;
using RS.ShopService.Models;

namespace RS.ShopService.Shops.UpdateShop
{
    public record UpdateShopCommand(UpdateShopCommandArgs Args) : ICommand<UpdateShopResult>;

    public record UpdateShopResult(bool IsSuccess);

    public class UpdateShopCommandValidator : AbstractValidator<UpdateShopCommand>
    {
        public UpdateShopCommandValidator()
        {
            RuleFor(command => command.Args.Id)
                .NotEmpty().WithMessage("Shop ID is required");

            RuleFor(command => command.Args.Name)
                .NotEmpty().WithMessage("Name is required")
                .Length(2, 150).WithMessage("Name must be between 2 and 150 characters");

            RuleFor(command => command.Args.Description)
                .NotEmpty().WithMessage("Description is required");
        }
    }

    internal class UpdateShopHandler(IDocumentSession session) : ICommandHandler<UpdateShopCommand, UpdateShopResult>
    {
        public async Task<UpdateShopResult> Handle(UpdateShopCommand request, CancellationToken cancellationToken)
        {
            var shop = await session.LoadAsync<Shop>(request.Args.Id, cancellationToken);

            if (shop is null)
            {
                throw new ShopNotFoundException(request.Args.Id);
            }

            shop.Name = request.Args.Name;
            shop.Description = request.Args.Description;
            shop.PhoneNumber = request.Args.PhoneNumber;
            shop.Address = request.Args.Address;
            shop.ImageFile = request.Args.ImageFile;

            session.Update(shop);
            await session.SaveChangesAsync(cancellationToken);

            return new UpdateShopResult(true);
        }
    }
}
