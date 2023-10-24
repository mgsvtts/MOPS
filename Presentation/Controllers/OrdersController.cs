﻿using Application.Commands.Orders;
using Contracts.Orders.Create;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    public async Task CreateOrder(CreateOrderRequest request, CancellationToken token = default)
    {
        var command = _mapper.Map<CreateOrderCommand>(request);

        var result = await _sender.Send(command, token);


    }
}