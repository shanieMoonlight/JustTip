//using Jt.Application.Utility.Results;
//using MediatR.Pipeline;

//namespace Jt.Application.Mediatr.Behaviours;

//public class JtRequestExceptionHandler<TRequest, TResponse, TException>()
//  : IRequestExceptionHandler<TRequest, TResponse, TException>
//      where TRequest : notnull
//      where TResponse : BasicResult, new()
//      where TException : Exception
//{

//    public Task Handle(TRequest request, TException exception, RequestExceptionHandlerState<TResponse> state,
//        CancellationToken cancellationToken)
//    {
//        var response = exception switch
//        {
//            UniqueConstraintException e => GenerateBadRequestResponse(e.Message),
//            CantDeleteException e => GenerateBadRequestResponse(e.Message),
//            _ => null
//        };

//        if (response is not null)
//            state.SetHandled(response);

//        return Task.CompletedTask;
//    }

//    //--------------------------// 

//    private TResponse GenerateBadRequestResponse(string msg)
//    {
//        if (typeof(TResponse) == typeof(BasicResult))
//            return (BasicResult.BadRequestResult(msg) as TResponse)!;

//        //Must be GenResult
//        return (TResponse)typeof(GenResult<>)
//              .GetGenericTypeDefinition()
//              .MakeGenericType(typeof(TResponse).GenericTypeArguments[0])
//              .GetMethod(nameof(GenResult<object>.BadRequestResult), [typeof(string)])!
//              .Invoke(null, [msg])!
//              ;


//    }

//}//Cls