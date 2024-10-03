using Application.Queries.Common;
using Domain.TypeAggregate.Repositories;
using Infrastructure.Common;
using Infrastructure.Models;
using Mediator;
using Newtonsoft.Json.Linq;

namespace Application.Queries.Types.GetAll;

public sealed class GetAllTypesQueryHandler : IQueryHandler<GetAllTypesQuery, Pagination<Infrastructure.Models.Type>>
{
    public async ValueTask<Pagination<Infrastructure.Models.Type>> Handle(GetAllTypesQuery request, CancellationToken cancellationToken)
    {
        using var db = new DbConnection();

        return await db.Types.PaginateAsync<Infrastructure.Models.Type, Guid>(request.Meta, cancellationToken);
    }
}