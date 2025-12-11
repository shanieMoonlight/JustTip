namespace JustTip.API.Middleware.Exceptions;

public interface IExceptionConverter
{
    ExceptionDetails GetExceptionDetails(Exception exception);
}