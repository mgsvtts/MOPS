using Domain.MerchItemAggregate;
using Domain.MerchItemAggregate.Repositories;
using Mediator;

namespace Application.Commands.MerchItems.Calculate;

public sealed class CalculateMerchItemCommandHandler(IMerchItemRepository _repository) : ICommandHandler<CalculateMerchItemCommand, CalculateMerchItemResponse>
{
    public async ValueTask<CalculateMerchItemResponse> Handle(CalculateMerchItemCommand request, CancellationToken cancellationToken)
    {
        var items = await _repository.GetAllByIdsAsync(request.Items.Select(x => x.ItemId), cancellationToken);

        var itemsWithAmount = BindItemsWithAmount(request, items);

        var sum = CalculateSum(itemsWithAmount);

        return new CalculateMerchItemResponse(sum);
    }

    private static decimal CalculateSum(Dictionary<MerchItem, int> itemsWithAmount)
    {
        decimal sum = 0;
        foreach (var item in itemsWithAmount)
        {
            sum += item.Key.Price.Value * item.Value;
        }

        return sum;
    }

    private static Dictionary<MerchItem, int> BindItemsWithAmount(CalculateMerchItemCommand request, List<MerchItem> items)
    {
        var itemsWithAmount = new Dictionary<MerchItem, int>();
        foreach (var item in items)
        {
            itemsWithAmount.Add(item, request.Items.First(x => x.ItemId == item.Id).Amount);
        }

        return itemsWithAmount;
    }
}