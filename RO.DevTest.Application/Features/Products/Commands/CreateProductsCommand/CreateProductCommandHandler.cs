using FluentValidation;
using MediatR;
using RO.DevTest.Domain.Abstract;
using RO.DevTest.Domain.Entities;

namespace RO.DevTest.Application.Features.Products.Commands.CreateProductsCommand;

public class CreateProductCommandHandler(IProductRepository productRepository, IValidator<CreateProductCommand> validator)
    : IRequestHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
        {
            var errorMessages = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException($"Erro de validação: {errorMessages}");
        }
        
        var product = new Product(
            command.Name!,
            command.Description!,
            command.Price,
            command.Quantity
        );
        
        await productRepository.AddAsync(product);
        
        return new CreateProductResult(product);
    }
}
