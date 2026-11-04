using KhatamSchool_Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhatamSchool_Application.Services
{
    public class CustomizedMediator : ICustomizedMediator
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CustomizedMediator(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _mediator = mediator;
        }

        public async Task<object> Send(object request, CancellationToken cancellationToken = default)
        {
            var cancellationTokenToUse = GetRequestAbortedCancellationTokenOrUseTokenFromMethodArguments(cancellationToken);
            return await _mediator.Send(request, cancellationTokenToUse);
        }

        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = new())
        {
            var cancellationTokenToUse = GetRequestAbortedCancellationTokenOrUseTokenFromMethodArguments(cancellationToken);
            return await _mediator.Send(request, cancellationTokenToUse);
        }

        public async Task Publish(object notification, CancellationToken cancellationToken = new())
        {
            var cancellationTokenToUse = GetRequestAbortedCancellationTokenOrUseTokenFromMethodArguments(cancellationToken);
            await _mediator.Publish(notification, cancellationTokenToUse);
        }

        public async Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = new()) where TNotification : INotification
        {
            var cancellationTokenToUse = GetRequestAbortedCancellationTokenOrUseTokenFromMethodArguments(cancellationToken);
            await _mediator.Publish(notification, cancellationTokenToUse);
        }

        private CancellationToken GetRequestAbortedCancellationTokenOrUseTokenFromMethodArguments(CancellationToken cancellationToken)
        {
            var cancellationTokenAsResult = _httpContextAccessor.HttpContext?.RequestAborted ?? cancellationToken;
            return cancellationTokenAsResult;
        }

        public async Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IRequest
        {
            var cancellationTokenToUse = GetRequestAbortedCancellationTokenOrUseTokenFromMethodArguments(cancellationToken);
            await _mediator.Send(request, cancellationTokenToUse);
        }

        public IAsyncEnumerable<TResponse> CreateStream<TResponse>(IStreamRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<object?> CreateStream(object request, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
