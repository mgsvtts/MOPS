using Domain.MerchItemAggregate;
using Domain.MerchItemAggregate.Repositories;
using Domain.OrderAggregate;
using Domain.OrderAggregate.Repositories;
using Mapster;
using Mediator;

namespace Application.Commands.Orders.Create;

public sealed class CreateOrderCommandHandler(IOrderRepository _orderRepository,
                                              IMerchItemRepository _merchItemRepository) : ICommandHandler<CreateOrderCommand, Order>
{
    public async ValueTask<Order> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = request.Adapt<Order>();

        var actualMerchItems = await _merchItemRepository.GetAllByIdsAsync(request.Items.Select(x => x.ItemId), cancellationToken);

        order.AddOrderItems(request.Items)
             .SetActualPriceToItems(actualMerchItems);

        SubtractAmountFromItems(order, actualMerchItems);

        var orderTask = _orderRepository.AddAsync(order, cancellationToken);
        var tasks = actualMerchItems.Select(x => _merchItemRepository.UpdateAsync(x, cancellationToken)).ToList();

        tasks.Add(orderTask);

        await Task.WhenAll(tasks);

        return order;
    }

    private static void SubtractAmountFromItems(Order order, List<MerchItem> actualMerchItems)
    {
        foreach (var item in actualMerchItems)
        {
            var minusAmount = order.Items.First(x => x.ItemId == item.Id).Amount;
            item.SubtractAmount(minusAmount);
        }
    }
}