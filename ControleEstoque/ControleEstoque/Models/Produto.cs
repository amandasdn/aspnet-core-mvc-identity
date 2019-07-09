using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControleEstoque.Models
{
    public class Produto
    {
        [Key]
        [Display(Name = "Código")]
        public int IDProduto { get; set; }

        [Required(ErrorMessage = "Digite o nome do produto.", AllowEmptyStrings = false)]
        [Display(Name = "Nome do Produto")]
        public string Nome { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        public int Quantidade { get; set; }

        [Required(ErrorMessage = "Digite o preço unitário.")]
        [Display(Name = "Preço Unitário")]
        //[DisplayFormat(DataFormatString = "{0,c}")]
        public decimal PrecoUnitario { get; set; }

        public string Imagem { get; set; }

        [Required(ErrorMessage = "Selecione a categoria do produto.")]
        public string Categoria { get; set; }

        [Required]
        [Display(Name = "Data de Cadastro")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime DataCadastro { get; set; }
    }
}
