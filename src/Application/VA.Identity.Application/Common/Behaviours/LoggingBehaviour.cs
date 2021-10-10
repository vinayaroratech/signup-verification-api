using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using VA.Identity.Application.Common.Interfaces;
using VA.Identity.Application.Jwt;

namespace VA.Identity.Application.Common.Behaviours
{
    public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;
        private readonly ICurrentUserContext _currentUserService;
        private readonly IIdentityService _identityService;

        public LoggingBehaviour(ILogger<TRequest> logger,
            ICurrentUserContext currentUserService
            //IIdentityService identityService
            )
        {
            _logger = logger;
            _currentUserService = currentUserService;
            //_identityService = identityService;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            string requestName = typeof(TRequest).Name;
            System.Guid userId = _currentUserService.GetUserId();
            string userName = string.Empty;

            if (userId != System.Guid.Empty)
            {
                await Task.CompletedTask;
                //userName = await _identityService.GetUserNameAsync(userId);
            }

            _logger.LogInformation("Land Request: {Name} {@UserId} {@UserName} {@Request}",
                requestName, userId, userName, request);
        }
    }
}
