using MediatR;
using RO.DevTest.Domain.Abstract;

namespace RO.DevTest.Application.Features.Products.Commands.DeleteProductsCommand;

public record class DeleteProductCommandHandler(IProductRepository ProductRepository) : IRequestHandler<DeleteProductCommand, Unit>
{
    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        await ProductRepository.DeleteAsync(request.Id);
        return Unit.Value;
    }
}