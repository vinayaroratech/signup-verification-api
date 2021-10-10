using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using VA.Identity.Application.Common.Interfaces;
using VA.Identity.Application.Jwt;

namespace VA.Identity.Application.Common.Behaviours
{
    public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly Stopwatch _timer;
        private readonly ILogger<TRequest> _logger;
        private readonly ICurrentUserContext _currentUserService;
        private readonly IIdentityService _identityService;

        public PerformanceBehaviour(
            ILogger<TRequest> logger,
            ICurrentUserContext currentUserService
            //IIdentityService identityService
            )
        {
            _timer = new Stopwatch();

            _logger = logger;
            _currentUserService = currentUserService;
            //_identityService = identityService;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _timer.Start();

            TResponse response = await next();

            _timer.Stop();

            long elapsedMilliseconds = _timer.ElapsedMilliseconds;

            if (elapsedMilliseconds > 500)
            {
                string requestName = typeof(TRequest).Name;
                var userId = _currentUserService.GetUserId();
                string userName = string.Empty;

                if (userId != System.Guid.Empty)
                {
                    await Task.CompletedTask;
                    //userName = await _identityService.GetUserNameAsync(userId);
                }

                _logger.LogWarning("Land Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@UserName} {@Request}",
                    requestName, elapsedMilliseconds, userId, userName, request);
            }

            return response;
        }
    }
}
