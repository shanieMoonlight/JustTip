using Jt.Application.Utility.Results;
using MediatR;

namespace JustTip.Application.Mediatr.CQRS;
public interface IJtQuery : IRequest<BasicResult>;


public interface IJtQuery<TResponse> : IRequest<GenResult<TResponse>>;

