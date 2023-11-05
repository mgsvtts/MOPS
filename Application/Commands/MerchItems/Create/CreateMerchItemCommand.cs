using Domain.Common.ValueObjects;
using Domain.MerchItemAggregate;
using Domain.MerchItemAggregate.Entities;
using Domain.MerchItemAggregate.ValueObjects;
using Domain.TypeAggregate.ValueObjects;
using MediatR;

namespace Application.Commands.MerchItems.Create;
public record struct CreateMerchItemCommand(TypeId TypeId,
                                            Name Name,
                                            Description? Description,
                                            MerchItemPrice Price,
                                            MerchItemPrice SelfPrice,
                                            MerchItemAmount AmountLeft,
                                            List<CreateMerchItemCommandImage> Images) : IRequest<MerchItem>;
public record struct CreateMerchItemCommandImage(Image Value, Stream ImageStream);