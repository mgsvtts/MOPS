using Application.Commands.Orders.Create;
using Application.Queries.Orders.GetAllOrders;
using Application.Queries.Orders.Statistics;
using Contracts.Orders.Create;
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

    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrderRequest request, CancellationToken token)
    {
        var command = _mapper.Map<CreateOrderCommand>(request);

        var result = await _sender.Send(command, token);

        return new CreatedResult("orders", _mapper.Map<CreateOrderResponse>(result));
    }

    [HttpGet("statistic")]
    public async Task<GetOrderStatisticQueryResponse> GetStatistic(CancellationToken token)
    {
        return await _sender.Send(new GetOrderStatisticQuery(), token);
    }

    [HttpGet]
    public async Task<object> GetAll(CancellationToken token)
    {
        return await _sender.Send(new GetAllOrdersQuery(), token);
    }
}
