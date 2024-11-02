using MediatR;

namespace RS.CommonLibrary.CQRS
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }

    public interface ICommand : ICommand<Unit>
    {
    }
}
