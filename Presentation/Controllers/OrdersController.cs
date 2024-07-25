using Application.Commands.Orders.Create;
using Application.Commands.Orders.Delete;
using Application.Queries.Orders.GetAll;
using Application.Queries.Orders.Statistics;
using Contracts.Orders.Create;
using Domain.OrderAggregate.ValueObjects;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/orders")]
public class OrdersController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public OrdersController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<GetAllOrdersResponseOrder>> GetAll(CancellationToken token)
    {
        return await _sender.Send(new GetAllOrdersQuery(), token);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrderRequest request, CancellationToken token)
    {
        var command = _mapper.Map<CreateOrderCommand>(request);

        var result = await _sender.Send(command, token);

        return new CreatedResult("orders", _mapper.Map<CreateOrderResponse>(result));
    }

    [HttpGet("statistic")]
    public async Task<GetOrderStatisticQueryResponse> GetStatistic(DateTime? dateFrom, DateTime? dateTo, CancellationToken token)
    {
        return await _sender.Send(new GetOrderStatisticQuery(dateFrom, dateTo), token);
    }

    [HttpDelete("{orderId:guid}")]
    public async Task Delete([FromRoute] Guid orderId, CancellationToken token)
    {
        var command = new DeleteOrderCommand(new OrderId(orderId));

        await _sender.Send(command, token);
    }
}
