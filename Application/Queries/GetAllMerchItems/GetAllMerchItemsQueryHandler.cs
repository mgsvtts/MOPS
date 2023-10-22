using Contracts.GetAllMerchItems;
using Domain.MerchItemAggregate;
using Domain.MerchItemAggregate.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.GetAllMerchItems;
internal sealed class GetAllMerchItemsQueryHandler : IRequestHandler<GetAllMerchItemsQuery, List<MerchItem>>
{
    private readonly IMerchItemRepository _repository;

    public GetAllMerchItemsQueryHandler(IMerchItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<MerchItem>> Handle(GetAllMerchItemsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync(cancellationToken);
    }
}
