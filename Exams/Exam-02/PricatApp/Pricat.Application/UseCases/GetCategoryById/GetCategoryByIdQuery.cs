using MediatR;
using Pricat.Domain.Entities;

public record GetCategoryByIdQuery(int Id) : IRequest<Category?>;
