using Domain.MerchItemAggregate;
using MediatR;

namespace Application.Queries.MerchItems.GetAll;
public record struct GetAllMerchItemsQuery(bool ShowNotAvailable) : IRequest<List<MerchItem>>;
