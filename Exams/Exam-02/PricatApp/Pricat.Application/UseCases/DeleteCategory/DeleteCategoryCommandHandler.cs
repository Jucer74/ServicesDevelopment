using System.Threading.Tasks;
using System.Threading;
using MediatR;
using Pricat.Domain.Interfaces;

namespace Pricat.Application.UseCases.DeleteCategory;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
{
    private readonly ICategoryRepository _categoryRepository;

    public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        await _categoryRepository.RemoveAsync(request.Id);
    }
}