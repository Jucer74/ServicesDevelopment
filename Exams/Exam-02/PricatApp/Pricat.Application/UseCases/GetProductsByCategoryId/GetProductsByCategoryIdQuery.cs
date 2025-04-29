using MediatR;
using Pricat.Domain.Entities;
using System.Collections.Generic;

namespace Pricat.Application.UseCases.GetProductsByCategoryId;

public class GetProductsByCategoryIdQuery : IRequest<List<Product>>
{
    public int CategoryId { get; set; }
}