using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using MediatR;
using Pricat.Domain.Entities;
using Pricat.Domain.Interfaces;

namespace Pricat.Application.UseCases.GetCategories
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, List<Category>>
    {
        private readonly ICategoryRepository _repository;

        public GetCategoriesQueryHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Category>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync();
        }
    }
}
