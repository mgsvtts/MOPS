using Domain.Common.ValueObjects;
using Domain.MerchItemAggregate;
using Domain.MerchItemAggregate.ValueObjects;
using Domain.TypeAggregate.ValueObjects;
using MediatR;

namespace Application.Commands.MerchItems.CreateMerchItem;
public sealed record CreateMerchItemCommand(TypeId TypeId,
                                            Name Name,
                                            Description? Description,
                                            MerchItemPrice Price,
                                            MerchItemPrice SelfPrice,
                                            MerchItemAmount AmountLeft) : IRequest<MerchItem>;
