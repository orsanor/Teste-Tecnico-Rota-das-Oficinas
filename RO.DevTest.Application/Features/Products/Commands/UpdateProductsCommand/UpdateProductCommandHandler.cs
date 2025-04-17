using MediatR;
using FluentValidation;
using RO.DevTest.Domain.Abstract;

namespace RO.DevTest.Application.Features.Products.Commands.UpdateProductsCommand;

public class UpdateProductCommandHandler(
    IProductRepository productRepository,
    IValidator<UpdateProductCommand> validator)
    : IRequestHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            var errorMessages = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException($"Erro de validação: {errorMessages}");
        }

        var product = await productRepository.GetByIdAsync(request.Id);

        if (product == null)
            throw new KeyNotFoundException($"Produto com ID '{request.Id}' não encontrado.");

        product.Name = request.Name;
        product.Price = (float)request.Price;
        product.Description = request.Description;
        product.Quantity = request.Quantity;

        await productRepository.UpdateAsync(product);
        return new UpdateProductResult(product);
    }
}
