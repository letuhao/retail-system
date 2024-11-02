using FluentValidation;
using Marten;
using RS.CommonLibrary.CQRS;
using RS.ShopService.Models;

namespace RS.ShopService.Shops.CreateShop
{
    public record CreateShopCommand(CreateShopCommandArgs Args) : ICommand<CreateShopResult>;

    public record CreateShopResult(Guid Id);

    public class CreateShopCommandValidator : AbstractValidator<CreateShopCommand>
    {
        public CreateShopCommandValidator()
        {
            RuleFor(x => x.Args.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Args.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(x => x.Args.Owner).NotEmpty().WithMessage("Owner is required");
        }
    }

    internal class CreateShopCommandHandler(IDocumentSession session) : ICommandHandler<CreateShopCommand, CreateShopResult>
    {
        public async Task<CreateShopResult> Handle(CreateShopCommand request, CancellationToken cancellationToken)
        {
            //create shop entity from command object
            //save to database
            //return CreateShopResult result               

            var shop = new Shop
            {
                Name = request.Args.Name,
                Description = request.Args.Description,
                ImageFile = request.Args.ImageFile,
                Owner = request.Args.Owner
            };

            //save to database
            session.Store(shop);
            await session.SaveChangesAsync(cancellationToken);

            //return result
            return new CreateShopResult(shop.Id);
        }
    }
}
