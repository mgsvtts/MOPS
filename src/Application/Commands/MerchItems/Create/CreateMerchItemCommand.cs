using Domain.Common.ValueObjects;
using Domain.MerchItemAggregate;
using Domain.MerchItemAggregate.Entities;
using Domain.MerchItemAggregate.ValueObjects;
using Domain.TypeAggregate.ValueObjects;
using Mediator;

namespace Application.Commands.MerchItems.Create;
public sealed record CreateMerchItemCommand(TypeId TypeId,
                                            Name Name,
                                            Description? Description,
                                            MerchItemPrice Price,
                                            MerchItemPrice SelfPrice,
                                            MerchItemAmount AmountLeft,
                                            List<CreateMerchItemCommandImage> Images) : ICommand<MerchItem>;

public readonly record struct CreateMerchItemCommandImage(Image Value, Stream ImageStream);
