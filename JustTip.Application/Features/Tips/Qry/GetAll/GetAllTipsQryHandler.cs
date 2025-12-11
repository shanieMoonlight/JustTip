using JustTip.Application.Features.Tips;

namespace JustTip.Application.Features.Tips.Qry.GetAll;
internal class GetAllTipsQryHandler(ITipRepo repo) : IJtQueryHandler<GetAllTipsQry, IEnumerable<TipDto>>
{

    public async Task<GenResult<IEnumerable<TipDto>>> Handle(GetAllTipsQry request, CancellationToken cancellationToken)
    {
        var mdls = await repo.ListAllAsync();
        var dtos = mdls.Select(mdl => mdl.ToDto());
        return GenResult<IEnumerable<TipDto>>.Success(dtos);

    }

}//Cls
