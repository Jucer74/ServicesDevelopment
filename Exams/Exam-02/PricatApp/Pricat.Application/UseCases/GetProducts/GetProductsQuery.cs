using MediatR;
using Pricat.Domain.Entities;
using System.Collections.Generic;

namespace Pricat.Application.UseCases.GetProducts;

public class GetProductsQuery : IRequest<IEnumerable<Product>>
{
}