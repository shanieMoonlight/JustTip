using JustTip.Application.Domain.Entities.Tips;
using JustTip.Infrastructure.Persistence.Repos.Abstractions;

namespace JustTip.Infrastructure.Persistence.Repos;
internal class TipRepo(JtDbContext db)
    : AGenCrudRepo<Tip>(db), ITipRepo
{ }
