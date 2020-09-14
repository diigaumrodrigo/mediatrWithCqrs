using App.Application.Domain.Commands;
using App.Application.Domain.Repositories;
using FluentValidation;

namespace App.Application.Domain.Validations
{
    public class CadastraProdutoValidator : AbstractValidator<CadastraProdutoCommand>
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ICategoriaProdutoRepository _categoriaProdutoRepository;

        public CadastraProdutoValidator(IProdutoRepository produtoRepository, ICategoriaProdutoRepository categoriaProdutoRepository)
        {
            _produtoRepository = produtoRepository;
            _categoriaProdutoRepository = categoriaProdutoRepository;

            RuleFor(x => x.Categoria)
                .GreaterThan(0)
                .WithMessage("Categoria é obrigatória");

            RuleFor(x => x.Descricao)
                .NotNull()
                .WithMessage("Descrição do produto é obrigatória");

            RuleFor(x => x.Valor)
                .NotEmpty()
                .NotNull()
                .GreaterThan(0)
                .WithMessage("Valor do produto deve ser maior que zero");

            RuleFor(x => x.Categoria)
                .Custom((categoria, context) =>
                {
                    var categoriaExiste = _categoriaProdutoRepository.CategoriaExiste(categoria).Result;

                    if (!categoriaExiste)
                        context.AddFailure("Categoria inválida");
                });

            RuleFor(x => x.Descricao)
                .Custom((descricao, context) =>
                {
                    var produtoJaExiste = _produtoRepository.ProdutoExiste(descricao).Result;

                    if (produtoJaExiste)
                        context.AddFailure("Produto já cadastrado");
                });
        }
    }
}