using System.Threading.Tasks;
using System.Threading;
using MediatR;
using Pricat.Domain.Interfaces;

namespace Pricat.Application.UseCases.UpdateCategory;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
{
    private readonly ICategoryRepository _categoryRepository;

    public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        await _categoryRepository.UpdateAsync(request.Id, request.Category);
    }
}