using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Commands.Create;

public class CreateBrandCommand:IRequest<CreatedBrandResponse>
{
    public string Name { get; set; }

    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, CreatedBrandResponse>
    {
        private readonly IBrandRepository _brandsRepository;
        private readonly IMapper _mapper;

        public CreateBrandCommandHandler(IBrandRepository brandsRepository, IMapper mapper)
        {
            _brandsRepository = brandsRepository;
            _mapper = mapper;
        }

        public async Task<CreatedBrandResponse>? Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            Brand brand = _mapper.Map<Brand>(request);           
            brand.Id = Guid.NewGuid();

            var result = await _brandsRepository.AddAsync(brand);

            CreatedBrandResponse createdBrandResponse = _mapper.Map<CreatedBrandResponse>(result);
            return createdBrandResponse;
        }
    }
}
