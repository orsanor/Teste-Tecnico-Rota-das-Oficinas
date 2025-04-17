using MediatR;
using FluentValidation;
using RO.DevTest.Domain.Abstract;

namespace RO.DevTest.Application.Features.Products.Commands.UpdateProductsCommand;

public class UpdateProductCommandHandler(
    IProductRepository productRepository,
    IValidator<UpdateProductCommand> validator)
    : IRequestHandler<UpdateProductCommand, bool>
{
    public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
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
        product.Price = request.Price;
        product.Description = request.Description;
        product.Quantity = request.Quantity;

        await productRepository.UpdateAsync(product);
        return true;
    }
}