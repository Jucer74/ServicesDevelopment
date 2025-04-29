using System.Collections.Generic;
using MediatR;
using Pricat.Domain.Entities;

namespace Pricat.Application.UseCases.GetCategories
{
    public class GetCategoriesQuery : IRequest<List<Category>> { }
}
