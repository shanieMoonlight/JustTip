namespace JustTip.Application.Features.JtOutboxMessages;
public class JtOutboxMessageDto
{
    public Guid Id { get; set; }

    public string Type { get; set; }
    public string ContentJson { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ProcessedOnUtc { get; private set; }
    public string? Error { get; set; }

    //--------------------------// 

    #region ModelBindingCtor
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public JtOutboxMessageDto() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    #endregion

    public JtOutboxMessageDto(JtOutboxMessage mdl)
    {
        Type = mdl.Type;
        ContentJson = mdl.ContentJson;
        ProcessedOnUtc = mdl.ProcessedOnUtc;
        CreatedOnUtc = mdl.CreatedOnUtc;
        Error = mdl.Error;
        Id = mdl.Id;

    }


}//Cls

