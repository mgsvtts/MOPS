using Domain.MerchItemAggregate;
using Domain.MerchItemAggregate.Repositories;
using MediatR;

namespace Application.Queries.MerchItems.GetAll;

internal sealed class GetAllMerchItemsQueryHandler : IRequestHandler<GetAllMerchItemsQuery, List<MerchItem>>
{
    private readonly IMerchItemRepository _repository;

    public GetAllMerchItemsQueryHandler(IMerchItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<MerchItem>> Handle(GetAllMerchItemsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync(request.ShowNotAvailable);
    }
}
