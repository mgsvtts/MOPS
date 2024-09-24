using Domain.OrderAggregate.Repositories;
using Mediator;

namespace Application.Commands.Orders.Delete;

public sealed class DeleteOrderCommandHandler(IOrderRepository _orderRepository) : ICommandHandler<DeleteOrderCommand>
{
    public async ValueTask<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.Id, cancellationToken)
                   ?? throw new InvalidOperationException($"Cannot find order with id {request.Id.Identity}");

        await _orderRepository.DeleteAsync(order, cancellationToken);

        return Unit.Value;
    }
}