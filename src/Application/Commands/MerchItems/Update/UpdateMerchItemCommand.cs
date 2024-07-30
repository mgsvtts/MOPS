using Domain.Common.ValueObjects;
using Domain.MerchItemAggregate;
using Domain.MerchItemAggregate.ValueObjects;
using Domain.TypeAggregate.ValueObjects;
using Mediator;

namespace Application.Commands.MerchItems.Update;

public sealed record UpdateMerchItemCommand(MerchItemId Id,
                                            TypeId? TypeId = null,
                                            Name? Name = null,
                                            Description? Description = null,
                                            MerchItemPrice? Price = null,
                                            MerchItemPrice? SelfPrice = null,
                                            MerchItemAmount? AmountLeft = null) : ICommand<MerchItem>;