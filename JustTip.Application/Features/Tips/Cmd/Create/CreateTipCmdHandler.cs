namespace JustTip.Application.Features.Tips.Cmd.Create;
public class CreateTipCmdHandler(ITipRepo _repo ) : IJtCommandHandler<CreateTipCmd, TipDto>
{
    public async Task<GenResult<TipDto>> Handle(CreateTipCmd request, CancellationToken cancellationToken)
    {
     
            var mdl = request.Dto.ToModel();

            var dbMdl = await _repo.AddAsync(mdl, cancellationToken);

            return GenResult<TipDto>.Success(dbMdl.ToDto());
            
    }


}//Cls
