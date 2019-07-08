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
        public int Quantidade { get; set; }

        [Required]
        [Display(Name = "Preço Total")]
        public decimal PrecoTotal { get; set; }

        [Required]
        [Display(Name = "Data de Cadastro")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime DataCadastro { get; set; }

        [Required]
        public int IDProduto { get; set; }

        [Required]
        public int IDClienteFornecedor { get; set; }
    }
}
