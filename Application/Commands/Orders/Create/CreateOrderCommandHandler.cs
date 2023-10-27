using Domain.MerchItemAggregate.Repositories;
using Domain.OrderAggregate;
using Domain.OrderAggregate.Repositories;
using MapsterMapper;
using MediatR;

namespace Application.Commands.Orders.Create;

internal sealed class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Order>
{
    private readonly IMapper _mapper;
    private readonly IOrderRepository _orderRepository;
    private readonly IMerchItemRepository _merchItemRepository;

    public CreateOrderCommandHandler(IOrderRepository orderRepository,
                                     IMapper mapper,
                                     IMerchItemRepository merchItemRepository)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _merchItemRepository = merchItemRepository;
    }

    public async Task<Order> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = _mapper.Map<Order>(request);

        var actualMerchItems = await _merchItemRepository.GetAllByIdsAsync(request.Items.Select(x => x.ItemId));

        order.AddOrderItems(request.Items)
             .SetActualPriceToItems(actualMerchItems);

        foreach (var item in actualMerchItems)
        {
            var minusAmount = order.Items.First(x => x.ItemId == item.Id).Amount;
            item.SubtractAmount(minusAmount);
        }

        await _orderRepository.AddAsync(order);
        var merchTasks = actualMerchItems.Select(_merchItemRepository.UpdateAsync);

        await Task.WhenAll(merchTasks);

        return order;
    }
}
