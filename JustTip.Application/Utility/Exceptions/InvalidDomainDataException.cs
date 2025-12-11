using JustTip.Application.Domain.Entities.Common;

namespace JustTip.Application.Utility.Exceptions;
public class InvalidDomainDataException(string entity, string message) 
    : Exception(message)
{
    public string Entity { get; set; } = entity;
}
