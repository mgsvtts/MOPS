using Domain.MerchItemAggregate;
using Domain.MerchItemAggregate.Repositories;
using Mediator;

namespace Application.Commands.MerchItems.Update;

public sealed class UpdateMerchItemCommandHandler(IMerchItemRepository _repository) : ICommandHandler<UpdateMerchItemCommand, MerchItem>
{
    public async ValueTask<MerchItem> Handle(UpdateMerchItemCommand request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(request.Id, cancellationToken)
                   ?? throw new InvalidOperationException($"Merch item with id: {request.Id.Identity} not found");

        item.WithTypeId(request.TypeId ?? item.TypeId)
            .WithName(request.Name ?? item.Name)
            .WithDescription(request.Description ?? item.Description)
            .WithPrice(request.Price ?? item.Price)
            .WithSelfPrice(request.SelfPrice ?? item.SelfPrice)
            .WithAmount(request.AmountLeft ?? item.AmountLeft);

        await _repository.UpdateAsync(item, cancellationToken);

        return item;
    }
}