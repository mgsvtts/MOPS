using Dapper;
using Infrastructure;
using Infrastructure.Models;
using MapsterMapper;
using MediatR;

namespace Application.Queries.Orders.GetAll;

internal class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, IEnumerable<GetAllOrdersResponseOrder>>
{
    private readonly IMapper _mapper;
    private readonly DbContext _db;

    public GetAllOrdersQueryHandler(IMapper mapper, DbContext db)
    {
        _mapper = mapper;
        _db = db;
    }

    public async Task<IEnumerable<GetAllOrdersResponseOrder>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        var query = Infrastructure.Queries.Order.GetAll();

        using var connection = _db.CreateConnection();

        var dbOrders = (await connection.QueryAsync<orders, order_items, merch_items, types, orders>(query, (order, order_item, merch_item, type) =>
        {
            merch_item.type_id = type.id;
            merch_item.type = type;
            order_item.merch_item_id = merch_item.id;
            order_item.merch_item = merch_item;
            order.order_items.Add(order_item);
            return order;
        }, splitOn: "id"))
        .GroupBy(p => p.id).Select(g =>
        {
            var groupedOrder = g.First();
            groupedOrder.order_items = g.Select(p => p.order_items.Single()).ToList();
            return groupedOrder;
        });

        return _mapper.Map<IEnumerable<GetAllOrdersResponseOrder>>(dbOrders);
    }
}
