using JustTip.Application.Features.Tips;
using JustTip.Application.Features.Tips.Cmd.Create;

namespace JustTip.Testing.Utils.DataFactories.Dtos;

public static class CreateTipDtoDataFactory
{

    public static List<CreateTipDto> CreateMany(int count = 20) => 
        [.. IdGenerator.GetGuidIdsList(count).Select(id => Create(id))];

    //- - - - - - - - - - - - - //

    public static CreateTipDto Create(
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

       return ConstructorInvoker.CreateNoParamsInstance<CreateTipDto>(paramaters);
    }



}//Cls

