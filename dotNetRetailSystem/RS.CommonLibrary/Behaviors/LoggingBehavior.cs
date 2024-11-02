using MediatR;
using Microsoft.Extensions.Logging;
using RS.CommonLibrary.Constants;
using System.Diagnostics;

namespace RS.CommonLibrary.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull, IRequest<TResponse>
        where TResponse : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            logger.LogInformation(
                "[START] Handle request={Request} - Response={Response} - RequestData={RequestData}", 
                typeof(TRequest).Name, typeof(TResponse).Name, request);

            var timer = new Stopwatch();
            timer.Start();

            var response = await next();

            timer.Stop();
            var timeTaken = timer.Elapsed;
            // if the request is greater than PERFORMANCE_LIMIT_TIME seconds, then log the warnings
            if (timeTaken.Seconds > CommonConstants.PERFORMANCE_LIMIT_TIME) 
                logger.LogWarning(
                    "[PERFORMANCE] The request {Request} took {TimeTaken} seconds.",
                    typeof(TRequest).Name, timeTaken.Seconds);

            logger.LogInformation(
                "[END] Handled {Request} with {Response}", 
                typeof(TRequest).Name, typeof(TResponse).Name);

            return response;
        }
    }
}
