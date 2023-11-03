using Domain.MerchItemAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.MerchItems.AddImage.Exceptions;
internal class OnlyOneMainImageAllowedException : Exception
{
    public OnlyOneMainImageAllowedException(MerchItemId ItemId)
        : base($"Merch item: {ItemId.Identity} can have only one main image")
    { }
}