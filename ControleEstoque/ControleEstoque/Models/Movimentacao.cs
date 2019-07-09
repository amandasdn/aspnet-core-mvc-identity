using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControleEstoque.Models
{
    public class Movimentacao
    {
        [Key]
        [Display(Name = "Código")]
        public int IDMovimentacao { get; set; }

        /// <summary>
        /// 0 = entrada
        /// 1 = saida
        /// </summary>
        [Required(ErrorMessage = "Selecione o tipo da movimentação.")]
        public int Tipo { get; set; }

        [Required(ErrorMessage = "Digite a quantidade de produtos movimentados.")]
        [Range(1, 1000, ErrorMessage = "Digite um valor entre 1 e 1000.")]
        public int Quantidade { get; set; }

        [Required]
        [Display(Name = "Preço Total")]
        public decimal PrecoTotal { get; set; }

        [Required]
        [Display(Name = "Data")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime DataCadastro { get; set; }

        [Required(ErrorMessage = "Selecione o produto")]
        [Display(Name = "Produto")]
        public int IDProduto { get; set; }

        [Required(ErrorMessage = "Selecione o cliente.")]
        [Display(Name = "Cliente")]
        public int IDCliente { get; set; }

        [Required(ErrorMessage = "Selecione o fornecedor.")]
        [Display(Name = "Fornecedor")]
        public int IDFornecedor { get; set; }
    }
}
