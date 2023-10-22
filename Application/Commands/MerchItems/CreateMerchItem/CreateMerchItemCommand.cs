﻿using Domain.Common.ValueObjects;
using Domain.MerchItemAggregate;
using Domain.MerchItemAggregate.ValueObjects;
using Domain.TypeAggregate.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.MerchItems.CreateMerchItem;
public sealed record CreateMerchItemCommand(TypeId TypeId,
                                            Name Name,
                                            Description? Description,
                                            MerchItemPrice Price,
                                            MerchItemPrice SelfPrice,
                                            MerchItemAmount AmountLeft) : IRequest<MerchItem>;