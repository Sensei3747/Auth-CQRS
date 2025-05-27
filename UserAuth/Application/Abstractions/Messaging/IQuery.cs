using UserAuth.Domain.Abstractions;
using MediatR;

namespace UserAuth.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>> {}
