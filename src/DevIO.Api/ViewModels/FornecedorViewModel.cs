﻿using System.ComponentModel.DataAnnotations;

namespace DevIO.Api.ViewModels
{
    public class FornecedorViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        [DeniedValues("none", "lol")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(14, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 11)]
        [DeniedValues("none", "lol")]
        public string? Documento { get; set; }

        [AllowedValues("1", "2")]
        public int TipoFornecedor { get; set; }

        public bool Ativo { get; set; }

        public EnderecoViewModel? Endereco { get; set; }

        public IEnumerable<ProdutoViewModel>? Produtos { get; set; }
    }
}
