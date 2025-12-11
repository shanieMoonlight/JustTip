namespace JustTip.Testing.Utils.DataFactories;

public static class TipDataFactory
{

    public static List<Tip> CreateMany(int count = 20) => 
        [.. IdGenerator.GetGuidIdsList(count).Select(id => Create(id))];

    //- - - - - - - - - - - - - //

    public static Tip Create(
          Guid? id = null,
		DateTime? dateTime = null,
		decimal? amount = null,
		DateTime? createdDate = null,
		DateTime? lastModifiedDate = null
        ) 
    {

       amount ??= 0;
		id ??= Guid.NewGuid();

        var paramaters = new[]
            {
                  new PropertyAssignment(nameof(Tip.DateTime),  () => dateTime ),
		          new PropertyAssignment(nameof(Tip.AmountEuros),  () => amount ),
		          new PropertyAssignment(nameof(Tip.Id),  () => id ),
		          new PropertyAssignment(nameof(Tip.CreatedDate),  () => createdDate ),
		          new PropertyAssignment(nameof(Tip.LastModifiedDate),  () => lastModifiedDate )
            };

       return ConstructorInvoker.CreateNoParamsInstance<Tip>(paramaters);
    }

    //--------------------------// 

   public static Tip Update(
          Tip tip,
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
 

       return PrivatePropertyUpdater.UpdateProperties(tip, [.. propertAssignments]);
    }



}//Cls

