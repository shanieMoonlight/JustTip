using FluentValidation;
using FluentValidation.Results;
using Jt.Application.Utility.Results;
using JustTip.Application.Utility.ErrorHandling;
using MediatR;

namespace JustTip.Application.Mediatr.Behaviours;
public sealed class JtValidationPipelineBehaviour<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : BasicResult
{

    private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

    //--------------------------// 

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var ctx = new ValidationContext<TRequest>(request);

        var failures = _validators
            .Select(v => v.Validate(ctx))
            .SelectMany(v => v.Errors)
            .Where(e => e is not null)
            .Distinct()
            .ToArray();

        if (failures.Length == 0)
            return await next(cancellationToken);

        return CreateValidationResult<TResponse>(failures);

    }

    //--------------------------// 

    private TResult CreateValidationResult<TResult>(ValidationFailure[] errors) where TResult : BasicResult
    {
        //var errorMsg = errors.First().ErrorMessage;
        var errorResponse = BadRequestResponse.Create();
        foreach (var error in errors)
            errorResponse.AddError(GetErrorKey(error), error.ErrorMessage);

        if (typeof(TResult) == typeof(BasicResult))
            return (BasicResult.BadRequestResult(errorResponse) as TResult)!;

        //Must be GenResult
        return (TResult)typeof(GenResult<>)
              .GetGenericTypeDefinition()
              .MakeGenericType(typeof(TResult).GenericTypeArguments[0])
              .GetMethod(nameof(GenResult<object>.BadRequestResult), [typeof(object)])!
              .Invoke(null, [errorResponse])!
              ;

    }


    //--------------------------// 

    private static string GetErrorKey(ValidationFailure error) =>
        error.PropertyName.Contains('.')
            ? Path.GetExtension(error.PropertyName).Replace(".", "")
            : error.PropertyName;

}//Cls

