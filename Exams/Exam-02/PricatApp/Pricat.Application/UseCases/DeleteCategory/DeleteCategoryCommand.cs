using MediatR;

namespace Pricat.Application.UseCases.DeleteCategory;

public class DeleteCategoryCommand : IRequest
{
    public int Id { get; set; }
}