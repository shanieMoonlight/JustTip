using JustTip.Application.Utility.ErrorHandling;
using JustTip.Application.Utility.Exceptions;

namespace JustTip.API.Middleware.Exceptions;

//########################################//


public record ExceptionDetails(int Status, object Details);
public record MessageResponse(string Message);


//########################################//


public class JtExceptionConverter : IExceptionConverter
{

    public ExceptionDetails GetExceptionDetails(Exception exception)
    {

        return exception switch
        {
            InvalidDomainDataException idde => new ExceptionDetails(
                StatusCodes.Status400BadRequest,
                BadRequestResponse.Create(idde.Entity, idde.Message)),

            _ => new ExceptionDetails(
                StatusCodes.Status500InternalServerError,
                new MessageResponse(exception.Message)
            )
        };

    }


}//Cls