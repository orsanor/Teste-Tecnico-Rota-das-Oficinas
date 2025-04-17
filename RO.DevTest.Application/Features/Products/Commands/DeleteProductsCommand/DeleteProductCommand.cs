using MediatR;

namespace RO.DevTest.Application.Features.Products.Commands.DeleteProductsCommand
{
    public class DeleteProductCommand(Guid id) : IRequest<Unit>
    {
        public Guid Id { get; set; } = id;
    }
}