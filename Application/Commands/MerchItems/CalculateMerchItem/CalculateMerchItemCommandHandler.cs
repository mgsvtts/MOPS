using Domain.MerchItemAggregate;
using Domain.MerchItemAggregate.Repositories;
using MediatR;

namespace Application.Commands.MerchItems.CalculateMerchItem;

public class CalculateMerchItemCommandHandler : IRequestHandler<CalculateMerchItemCommand, CalculateMerchItemResponse>
{
    private readonly IMerchItemRepository _repository;

    public CalculateMerchItemCommandHandler(IMerchItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<CalculateMerchItemResponse> Handle(CalculateMerchItemCommand request, CancellationToken cancellationToken)
    {
        var items = await _repository.GetAllByIdsAsync(request.Items.Select(x => x.ItemId));

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
