using System.Threading.Tasks;
using System.Threading;
using MediatR;
using Pricat.Domain.Entities;
using Pricat.Domain.Interfaces;

namespace Pricat.Application.UseCases.CreateCategory;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Category>
{
    private readonly ICategoryRepository _categoryRepository;

    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Category> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        return await _categoryRepository.AddAsync(request.Category);
    }
}