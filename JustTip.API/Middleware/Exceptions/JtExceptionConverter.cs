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
            //SomeGbException gbEx => new ExceptionDetails(
            //    StatusCodes.Status400BadRequest,
            //    BadRequestResponse.Create(ipe.Property, ipe.Message)),

            _ => new ExceptionDetails(
                StatusCodes.Status500InternalServerError,
                new MessageResponse(exception.Message)
            )
        };

    }


}//Cls