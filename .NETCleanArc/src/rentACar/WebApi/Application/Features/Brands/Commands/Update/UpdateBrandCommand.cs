using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Commands.Update;

public class UpdateBrandCommand:IRequest<updateBrandResponse>
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, updateBrandResponse>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;
        public UpdateBrandCommandHandler(IMapper mapper, IBrandRepository brandRepository)
        {
            _mapper = mapper;
            _brandRepository = brandRepository;
        }
        public async Task<updateBrandResponse> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            Brand? brand = await _brandRepository.GetAsync(predicate: b=> b.Id == request.Id, cancellationToken:cancellationToken );

            brand = _mapper.Map(request, brand);

            await _brandRepository.UpdateAsync(brand);

            updateBrandResponse response = _mapper.Map<updateBrandResponse>(brand);

            return response;
        }
    }
   
}
