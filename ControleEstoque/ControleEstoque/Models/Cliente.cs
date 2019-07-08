using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControleEstoque.Models
{
    public class Cliente
    {
        [Key]
        [Display(Name = "Código")]
        public int IDCliente { get; set; }

        [Required(ErrorMessage = "Digite o nome fantasia do cliente.", AllowEmptyStrings = false)]
        [Display(Name = "Nome Fantasia")]
        public string NomeFantasia { get; set; }

        [Required(ErrorMessage = "Digite o nome da razão social do cliente.", AllowEmptyStrings = false)]
        [Display(Name = "Razão Social")]
        public string RazaoSocial { get; set; }

        [Required(ErrorMessage = "Digite o CNPJ do cliente.")]
        [Display(Name = "CNPJ")]
        public string CNPJ { get; set; }

        //[Required(ErrorMessage = "Digite o telefone do fornecedor.")]
        public string Telefone { get; set; }

        //[Required(ErrorMessage = "Digite e-mail do fornecedor.")]
        [EmailAddress(ErrorMessage = "Formato de e-mail inválido.")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Data de Cadastro")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime DataCadastro { get; set; }

        // ENDEREÇO

        [Required(ErrorMessage = "Digite o nome do endereço.")]
        public string Endereco { get; set; }

        [Display(Name = "Número")]
        [Required(ErrorMessage = "Digite o número do endereço.")]
        public string Numero { get; set; }

        public string Complemento { get; set; }

        [Required(ErrorMessage = "Digite o nome do bairro.")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "Digite o nome da cidade.")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "Selecione a sigla do estado.")]
        public string UF { get; set; }

        [Required(ErrorMessage = "Digite o CEP.")]
        public string CEP { get; set; }
    }
}
