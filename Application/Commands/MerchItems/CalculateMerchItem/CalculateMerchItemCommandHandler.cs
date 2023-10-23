using Domain.MerchItemAggregate;
using Domain.MerchItemAggregate.Repositories;
using MapsterMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        var (Sum, Benefit) = CalculateSumAndBenefit(itemsWithAmount);

        return new CalculateMerchItemResponse(Sum, Benefit);
    }

    private static (decimal Sum, float Benefit) CalculateSumAndBenefit(Dictionary<MerchItem, int> itemsWithAmount)
    {
        decimal sum = 0;
        float benefit = 0;
        foreach (var item in itemsWithAmount)
        {
            sum += item.Key.Price.Value * item.Value;
            benefit += item.Key.GetBenefitPercent();
        }

        return (sum, benefit);
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
