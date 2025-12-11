namespace JustTip.Application.Abstractions;

public interface IDbInitializer
{
    Task MigrateAsync();
    Task InitializeAsync(List<Employee> employees);

}