using Domain.MerchItemAggregate;
using MediatR;

namespace Application.Queries.MerchItems.GetAllMerchItems;
public sealed record GetAllMerchItemsQuery : IRequest<List<MerchItem>>;
