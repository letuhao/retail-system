using FluentValidation;
using Marten;
using RS.CommonLibrary.CQRS;
using RS.OrderService.Models;

namespace RS.OrderService.ShoppingCarts.CreateCart
{
    public record CreateCartCommand(CreateCartCommandArgs Args) : ICommand<CreateCartResult>;

    public record CreateCartResult(Guid Id);

    public class CreateCartCommandValidator : AbstractValidator<CreateCartCommand>
    {
        public CreateCartCommandValidator()
        {
            RuleFor(command => command.Args.ExpiresDate)
                .NotEmpty().WithMessage("ExpiresDate is required");

            RuleFor(command => command.Args.Total)
                .NotEmpty().WithMessage("Total is required");

            RuleFor(command => command.Args.TotalItem)
                .NotEmpty().WithMessage("TotalItem is required");
        }
    }

    internal class CreateCartCommandHandler(IDocumentSession session) : ICommandHandler<CreateCartCommand, CreateCartResult>
    {
        public async Task<CreateCartResult> Handle(CreateCartCommand request, CancellationToken cancellationToken)
        {
            //create Cart entity from command object
            //save to database
            //return CreateCartResult result               

            var cart = new ShoppingCart
            {
                ExpiresDate = request.Args.ExpiresDate,
                Total = request.Args.Total,
                TotalItem = request.Args.TotalItem
            };

            //save to database
            session.Store(cart);
            await session.SaveChangesAsync(cancellationToken);

            //return result
            return new CreateCartResult(cart.Id);
        }
    }
}
