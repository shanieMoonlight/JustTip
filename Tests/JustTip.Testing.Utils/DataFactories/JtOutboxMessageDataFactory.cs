using JustTip.Application.Domain.Entities.OutboxMessages;
using TestingHelpers;
using TestingHelpers.Reflection;

namespace JustTip.Testing.Utils.DataFactories;

public static class JtOutboxMessageDataFactory
{
    
    static readonly string _sampleJson = """{"$type":"Jt.Application.Domain.Entities.Regions.Events.GridRegionCreatedDomainEvent, Jt.Application","GridId":"08de1bec-d1b7-fbe1-18c0-4d950a2b0000"}""";
    
    
    
    public static List<JtOutboxMessage> CreateMany(int count = 20) => 
        [.. IdGenerator.GetGuidIdsList(count).Select(id => Create(id))];

    //- - - - - - - - - - - - - //

    public static JtOutboxMessage Create(
          Guid? id = null,
		string? type = null,
		string? contentJson = null,
		DateTime? createdOnUtc = null,
		DateTime? processedOnUtc = null,
		string? error = null,
		DateTime? createdDate = null,
		DateTime? lastModifiedDate = null
        ) 
    {

       type ??= $"{RandomStringGenerator.Generate(20)}";
		contentJson ??= _sampleJson;
		error ??= $"{RandomStringGenerator.Generate(20)}";
		id ??= Guid.NewGuid();

        var paramaters = new[]
            {
                  new PropertyAssignment(nameof(JtOutboxMessage.Type),  () => type ),
		          new PropertyAssignment(nameof(JtOutboxMessage.ContentJson),  () => contentJson ),
		          new PropertyAssignment(nameof(JtOutboxMessage.CreatedOnUtc),  () => createdOnUtc ),
		          new PropertyAssignment(nameof(JtOutboxMessage.ProcessedOnUtc),  () => processedOnUtc ),
		          new PropertyAssignment(nameof(JtOutboxMessage.Error),  () => error ),
		          new PropertyAssignment(nameof(JtOutboxMessage.Id),  () => id ),
		          new PropertyAssignment(nameof(JtOutboxMessage.CreatedDate),  () => createdDate ),
		          new PropertyAssignment(nameof(JtOutboxMessage.LastModifiedDate),  () => lastModifiedDate )
            };

       return ConstructorInvoker.CreateNoParamsInstance<JtOutboxMessage>(paramaters);
    }

    //--------------------------// 

   public static JtOutboxMessage Update(
          JtOutboxMessage gbOutboxMessage,
          Guid? id = null,
		string? type = null,
		string? contentJson = null,
		DateTime? createdOnUtc = null,
		DateTime? processedOnUtc = null,
		string? error = null,
		DateTime? createdDate = null,
		DateTime? lastModifiedDate = null
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
		
            if (createdDate is not null)
                    propertAssignments.Add(new PropertyAssignment(nameof(JtOutboxMessage.CreatedDate), () => createdDate));
		
            if (lastModifiedDate is not null)
                    propertAssignments.Add(new PropertyAssignment(nameof(JtOutboxMessage.LastModifiedDate), () => lastModifiedDate));
 

       return PrivatePropertyUpdater.UpdateProperties(gbOutboxMessage, [.. propertAssignments]);
    }



}//Cls

