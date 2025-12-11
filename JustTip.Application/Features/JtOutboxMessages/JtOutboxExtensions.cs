namespace JustTip.Application.Features.JtOutboxMessages;
internal static class JtOutboxExtensions
{
    public static JtOutboxMessageDto ToDto(this JtOutboxMessage model) =>
        new(model);


}//Cls
