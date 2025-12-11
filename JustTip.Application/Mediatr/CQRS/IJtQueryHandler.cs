using Jt.Application.Utility.Results;
using MediatR;

namespace JustTip.Application.Mediatr.CQRS;
public interface IJtQueryHandler<TResponse> : IRequestHandler<TResponse, BasicResult>
    where TResponse : IJtQuery;


public interface IJtQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, GenResult<TResponse>>
    where TQuery : IJtQuery<TResponse>;