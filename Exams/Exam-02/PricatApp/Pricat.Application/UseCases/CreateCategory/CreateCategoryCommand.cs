using MediatR;
using Pricat.Domain.Entities;

namespace Pricat.Application.UseCases.CreateCategory;

public class CreateCategoryCommand : IRequest<Category>
{
    public Category Category { get; set; } = null!;
}