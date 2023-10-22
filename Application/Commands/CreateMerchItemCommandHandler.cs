using Domain.MerchItemAggregate;
using Domain.MerchItemAggregate.Repositories;
using MapsterMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands;
internal class CreateMerchItemCommandHandler : IRequestHandler<CreateMerchItemCommand, MerchItem>
{
    private readonly IMapper _mapper;
    private readonly IMerchItemRepository _repository;

    public CreateMerchItemCommandHandler(IMerchItemRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<MerchItem> Handle(CreateMerchItemCommand request, CancellationToken cancellationToken)
    {
        var item = _mapper.Map<MerchItem>(request);

        await _repository.AddAsync(item,cancellationToken);

        return item;
    }
}
