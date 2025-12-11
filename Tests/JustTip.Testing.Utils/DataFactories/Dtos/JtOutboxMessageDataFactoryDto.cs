using JustTip.Application.Domain.Entities.OutboxMessages;
using JustTip.Application.Features.JtOutboxMessages;

namespace JustTip.Testing.Utils.DataFactories.Dtos;

public static class JtOutboxMessageDtoDataFactory
{

    public static List<JtOutboxMessageDto> CreateMany(int count = 20) => 
        [.. IdGenerator.GetGuidIdsList(count).Select(id => Create(id))];

    //- - - - - - - - - - - - - //

    public static JtOutboxMessageDto Create(
          Guid? id = null,
		string? type = null,
		string? contentJson = null,
		string? error = null
        ) 
    {

       type ??= $"{RandomStringGenerator.Generate(20)}";
		contentJson ??= $"{RandomStringGenerator.Generate(20)}";
		error ??= $"{RandomStringGenerator.Generate(20)}";

        var paramaters = new[]
            {
                         new PropertyAssignment(nameof(JtOutboxMessage.Type),  () => type ),
		          new PropertyAssignment(nameof(JtOutboxMessage.ContentJson),  () => contentJson ),
		          new PropertyAssignment(nameof(JtOutboxMessage.Error),  () => error ),
		          new PropertyAssignment(nameof(JtOutboxMessage.Id),  () => id )
            };

       return ConstructorInvoker.CreateNoParamsInstance<JtOutboxMessageDto>(paramaters);
    }

    //--------------------------// 

   public static JtOutboxMessageDto Update(
        JtOutboxMessageDto jtOutboxMessageDto,
        Guid? id = null,
        DateTime? createdOnUtc = null,
        DateTime? processedOnUtc = null,
        string? type = null,
		string? contentJson = null,
		string? error = null
        ) 
    {

        List<PropertyAssignment> propertAssignments = [];

 
        
            if (type is not null)
                    propertAssignments.Add(new PropertyAssignment(nameof(JtOutboxMessage.Type), () => type));
		
            if (contentJson is not null)
                    propertAssignments.Add(new PropertyAssignment(nameof(JtOutboxMessage.ContentJson), () => contentJson));
		
            if (createdOnUtc is not null)
                    propertAssignments.Add(new PropertyAssignment(nameof(JtOutboxMessage.CreatedOnUtc), () => createdOnUtc));
		
            if (processedOnUtc is not null)
                    propertAssignments.Add(new PropertyAssignment(nameof(JtOutboxMessage.ProcessedOnUtc), () => processedOnUtc));
		
            if (error is not null)
                    propertAssignments.Add(new PropertyAssignment(nameof(JtOutboxMessage.Error), () => error));
		
            if (id is not null)
                    propertAssignments.Add(new PropertyAssignment(nameof(JtOutboxMessage.Id), () => id));
		 

       return PrivatePropertyUpdater.UpdateProperties(jtOutboxMessageDto, [.. propertAssignments]);
    }



}//Cls

