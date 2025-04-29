using System.Threading.Tasks;
using System.Threading;
using MediatR;
using Pricat.Domain.Entities;
using Pricat.Domain.Interfaces;

public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Category?>
{
    private readonly ICategoryRepository _repository;

    public GetCategoryByIdQueryHandler(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<Category?> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.Id);
    }
}
