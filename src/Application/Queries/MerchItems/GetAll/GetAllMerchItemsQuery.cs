using Application.Queries.Common;
using Domain.MerchItemAggregate;
using Mediator;

namespace Application.Queries.MerchItems.GetAll;
public sealed record GetAllMerchItemsQuery(MerchItemSort Sort,
                                           PaginationMeta Meta) : IQuery<Pagination<MerchItem>>;