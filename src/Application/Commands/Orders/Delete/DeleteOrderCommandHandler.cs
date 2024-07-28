using Domain.OrderAggregate.Repositories;
using Mediator;

namespace Application.Commands.Orders.Delete;

public sealed class DeleteOrderCommandHandler : ICommandHandler<DeleteOrderCommand>
{
    private readonly IOrderRepository _orderRepository;

    public DeleteOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async ValueTask<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.Id)
                   ?? throw new InvalidOperationException($"Cannot find order with id {request.Id.Identity}");

        await _orderRepository.DeleteAsync(order);
        
        return Unit.Value;
    }
}
