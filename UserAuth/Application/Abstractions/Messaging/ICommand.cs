using MediatR;
using UserAuth.Domain.Abstractions;

namespace UserAuth.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result>, IBaseCommand { }
public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand { }
public interface IBaseCommand { }
