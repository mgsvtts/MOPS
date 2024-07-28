using Infrastructure.Models;
using Mediator;

namespace Application.Queries.MerchItems.GetAll;
public sealed record GetAllMerchItemsQuery(bool ShowNotAvailable,
                                           MerchItemSort Sort) : IQuery<List<MerchItem>>;
