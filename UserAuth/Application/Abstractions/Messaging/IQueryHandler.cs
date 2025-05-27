using Microsoft.AspNetCore.Identity;
using MediatR;
using UserAuth.Domain.Abstractions;

namespace UserAuth.Application.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>> where TQuery : IQuery<TResponse> {}

