using FluentValidation;
using Marten;
using RS.CommonLibrary.CQRS;
using RS.OrderService.Models;

namespace RS.OrderService.Shippings.DeleteShipping
{
    public record DeleteShippingCommand(Guid Id) : ICommand<DeleteShippingResult>;

    public record DeleteShippingResult(bool IsSuccess);

    public class DeleteShippingCommandValidator : AbstractValidator<DeleteShippingCommand>
    {
        public DeleteShippingCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Shipping ID is required");
        }
    }

    internal class DeleteCartItemHandler(IDocumentSession session) : ICommandHandler<DeleteShippingCommand, DeleteShippingResult>
    {
        public async Task<DeleteShippingResult> Handle(DeleteShippingCommand request, CancellationToken cancellationToken)
        {
            session.Delete<Shipping>(request.Id);
            await session.SaveChangesAsync(cancellationToken);

            return new DeleteShippingResult(true);
        }
    }
}
