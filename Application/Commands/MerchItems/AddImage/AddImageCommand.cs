using Domain.MerchItemAggregate.Entities;
using Domain.MerchItemAggregate.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.MerchItems.AddImage;
public record struct AddImageCommand(IDictionary<Image, Stream> Images) : IRequest;