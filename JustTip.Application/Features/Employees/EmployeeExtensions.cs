namespace JustTip.Application.Features.Employees;
internal static class EmployeeExtensions
{
    public static Employee Update(this Employee model, EmployeeDto dto) =>
        model.Update(
            name: dto.Name,
            description: dto.Description
        );

    //--------------------------// 

    public static Employee ToModel(this EmployeeDto dto) => 
        Employee.Create(
            name: dto.Name,
            description: dto.Description
        );

    //--------------------------// 

    public static EmployeeDto ToDto(this Employee model) =>
        new(model);


}//Cls
