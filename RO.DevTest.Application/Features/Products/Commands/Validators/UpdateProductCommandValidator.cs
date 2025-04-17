﻿using FluentValidation;

namespace RO.DevTest.Application.Features.Products.Commands.Validators;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductsCommand.UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty()
            .WithMessage("O ID do produto é obrigatório.");

        RuleFor(p => p.Name)
            .NotEmpty()
            .WithMessage("O campo nome é obrigatório.")
            .MaximumLength(100)
            .WithMessage("O campo nome deve ter no máximo 100 caracteres.");

        RuleFor(p => p.Price)
            .GreaterThan(0)
            .WithMessage("O campo preço deve ser maior que zero.");

        RuleFor(p => p.Quantity)
            .GreaterThanOrEqualTo(0)
            .WithMessage("O campo quantidade não pode ser negativo.");

        RuleFor(p => p.Description)
            .MaximumLength(500)
            .WithMessage("A descrição deve ter no máximo 500 caracteres.")
            .When(p => !string.IsNullOrWhiteSpace(p.Description));
    }
}