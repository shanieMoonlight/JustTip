using JustTip.Application.Features.Tips;

namespace JustTip.Dtos.Tests.Data.Initializers;

public static class TipDtoDataFactory
{

    public static List<TipDto> CreateMany(int count = 20) => 
        [.. IdGenerator.GetGuidIdsList(count).Select(id => Create(id))];

    //- - - - - - - - - - - - - //

    public static TipDto Create(
          Guid? id = null,
		DateTime? dateTime = null,
		decimal? amount = null
        ) 
    {

       amount ??= 0;

        var paramaters = new[]
            {
                         new PropertyAssignment(nameof(Tip.DateTime),  () => dateTime ),
		          new PropertyAssignment(nameof(Tip.AmountEuros),  () => amount ),
		          new PropertyAssignment(nameof(Tip.Id),  () => id )
            };

       return ConstructorInvoker.CreateNoParamsInstance<TipDto>(paramaters);
    }

    //--------------------------// 

   public static TipDto Update(
          TipDto tipDto,
          Guid? id = null,
		DateTime? dateTime = null,
		decimal? amount = null,
		DateTime? createdDate = null,
		DateTime? lastModifiedDate = null
        ) 
    {

        List<PropertyAssignment> propertAssignments = [];

 
        
            if (dateTime is not null)
                    propertAssignments.Add(new PropertyAssignment(nameof(Tip.DateTime), () => dateTime));
		
            if (amount is not null)
                    propertAssignments.Add(new PropertyAssignment(nameof(Tip.AmountEuros), () => amount));
		
            if (id is not null)
                    propertAssignments.Add(new PropertyAssignment(nameof(Tip.Id), () => id));
		
            if (createdDate is not null)
                    propertAssignments.Add(new PropertyAssignment(nameof(Tip.CreatedDate), () => createdDate));
		
            if (lastModifiedDate is not null)
                    propertAssignments.Add(new PropertyAssignment(nameof(Tip.LastModifiedDate), () => lastModifiedDate));
 

       return PrivatePropertyUpdater.UpdateProperties(tipDto, [.. propertAssignments]);
    }



}//Cls

