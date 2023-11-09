using Dapper;
using Domain.MerchItemAggregate;
using Domain.MerchItemAggregate.Repositories;
using Infrastructure;
using Infrastructure.Models;
using MapsterMapper;
using MediatR;

namespace Application.Queries.MerchItems.GetAll;

internal sealed class GetAllMerchItemsQueryHandler : IRequestHandler<GetAllMerchItemsQuery, List<MerchItem>>
{
    private readonly IMapper _mapper;
    private readonly DbContext _db;

    public GetAllMerchItemsQueryHandler(DbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<List<MerchItem>> Handle(GetAllMerchItemsQuery request, CancellationToken cancellationToken)
    {
        var query = Infrastructure.Queries.MerchItem.GetAllMerchItems(request.ShowNotAvailable, request.Sort);

        using var connection = _db.CreateConnection();

        var items = (await connection.QueryAsync<merch_items, images, merch_items>(query, (item, image) =>
        {
            image.merch_item_id = item.id;
            item.images.Add(image);
            return item;
        }, splitOn: "id"))
        .GroupBy(p => p.id).Select(g =>
        {
            var groupedItem = g.First();
            groupedItem.images = g.Select(p => p.images.Single()).ToList();
            return groupedItem;
        });

        return _mapper.Map<List<MerchItem>>(items);
    }
}
