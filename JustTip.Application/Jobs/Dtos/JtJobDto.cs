namespace JustTip.Application.Jobs.Dtos;
public class JtJobDto(string type, string method, IEnumerable<object> args)
{
    public string Type { get; set; } = type;
    public string Method { get; set; } = method;
    public IEnumerable<object> Args { get; set; } = args;

}
