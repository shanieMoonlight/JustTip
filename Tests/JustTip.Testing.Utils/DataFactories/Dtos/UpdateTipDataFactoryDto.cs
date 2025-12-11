using JustTip.Application.Features.Tips;

namespace JustTip.Dtos.Tests.Data.Initializers;

public static class UpdateTipDtoDataFactory
{

    public static List<UpdateTipDto> CreateMany(int count = 20) => 
        [.. IdGenerator.GetGuidIdsList(count).Select(id => Create(id))];

    //- - - - - - - - - - - - - //

    public static UpdateTipDto Create(
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

       return ConstructorInvoker.CreateNoParamsInstance<UpdateTipDto>(paramaters);
    }


}//Cls

