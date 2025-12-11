using Jt.Application.Utility.Results;
using MediatR;

namespace JustTip.Application.Mediatr.CQRS;

/// <summary>
/// Basically a tag interface to identify all types of Command requests.
/// </summary>
public interface IBaseJtCommand;


public interface IJtCommand : IRequest<BasicResult>, IBaseJtCommand;
public interface IJtCommand<TResponse> : IRequest<GenResult<TResponse>>, IBaseJtCommand;


public interface IJtCommand_NonUow : IRequest<BasicResult>;
public interface IJtCommand_NonUow<TResponse> : IRequest<GenResult<TResponse>>;


