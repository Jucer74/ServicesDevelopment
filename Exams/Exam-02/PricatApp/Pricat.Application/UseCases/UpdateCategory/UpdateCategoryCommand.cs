using MediatR;
using Pricat.Domain.Entities;

namespace Pricat.Application.UseCases.UpdateCategory;

public class UpdateCategoryCommand : IRequest
{
    public int Id { get; set; }
    public Category Category { get; set; } = null!;
}