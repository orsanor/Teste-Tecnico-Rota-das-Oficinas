using MediatR;
using RO.DevTest.Domain.Abstract;

namespace RO.DevTest.Application.Features.Products.Commands.DeleteProductsCommand
{
    public record class DeleteProductCommandHandler(IProductRepository ProductRepository) : IRequestHandler<DeleteProductCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await ProductRepository.GetByIdAsync(request.Id);
            
            if (product == null)
            {
                throw new KeyNotFoundException($"Produto com ID '{request.Id}' não encontrado.");
            }

            await ProductRepository.DeleteAsync(product.Id);
            
            
            return Unit.Value;
        }
    }
}