using Jt.Application.Utility.Results;
using MediatR;

namespace JustTip.Application.Mediatr.CQRS;
public interface IJtCommandHandler<TCommand> : IRequestHandler<TCommand, BasicResult>
    where TCommand : IJtCommand;


public interface IJtCommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, GenResult<TResponse>>
    where TCommand : IJtCommand<TResponse>;





public interface IJtCommandHandler_NonUow<TCommand> : IRequestHandler<TCommand, BasicResult>
    where TCommand : IJtCommand_NonUow;

public interface IJtCommandHandler_NonUow<TCommand, TResponse> : IRequestHandler<TCommand, GenResult<TResponse>>
    where TCommand : IJtCommand_NonUow<TResponse>;