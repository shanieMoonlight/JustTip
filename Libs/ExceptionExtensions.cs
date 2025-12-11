using System.Text;

namespace LogToFile;

public static class ExceptionExtensions
{
    /// <summary>
    /// Get all details from exception in String form.
    /// Formatted for a log report.
    /// </summary>
    /// <returns></returns>
    public static string ToLogString(this Exception ex)
    {

        if (ex == null)
            return "";

        StringBuilder messageBuilder = new();


        var innerException = ex;
        do
        {
            messageBuilder
                .AppendException(innerException)
                .AppendLine("- - - - - - - - - - - - - - - - - - - - - -");

            innerException = innerException.InnerException;
        }
        while (innerException != null);


        return messageBuilder.ToString();

    }

    //---------------------------------------------//

    private static StringBuilder AppendException(this StringBuilder sb, Exception ex)
    {
        sb ??= new StringBuilder();
        if (ex == null) 
            return sb;

        sb
          .AppendLine()
          .AppendLine($"TimeStamp: {DateTime.UtcNow:dddd, dd MMMM yyyy HH:mm:ss.fff}")
          .AppendLine()
          .AppendLine($"Type:  {ex?.GetType()?.ToString() ?? "No type"}")
          .AppendLine()
          .AppendLine($"Message:  {ex?.Message ?? "No message"}")
          .AppendLine()
          .AppendLine($"Source:  {ex?.Source ?? "No Source"}")
          .AppendLine()
          .AppendLine($"HResult:  {ex?.HResult}")
          .AppendLine()
          .AppendLine($"Help Link:  {ex?.HelpLink ?? "No HelpLink"}")
          .AppendLine()
          .AppendLine($"Stack Trace:  {ex?.StackTrace ?? "No StackTrace"}")
          .AppendLine();


        if (ex?.Data.Count > 0)
        {
            sb
               .AppendLine($"Data:")
               .AppendLine();

            foreach (var key in ex.Data.Keys)
                sb.AppendLine($"{key}: {ex.Data[key]}");
        }//If


        return sb;

    }

}//Cls