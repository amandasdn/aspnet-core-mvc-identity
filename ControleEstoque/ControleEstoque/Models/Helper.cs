using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ControleEstoque.Models
{
    public static class Helper
    {
        /// <summary>
        /// Exemplo: nome-imagem.jpeg | Retorno: nome-imagem
        /// </summary>
        /// <param name="texto"></param>
        public static string NomeSemExtensao(this string texto)
        {
            int posicao1 = 0;
            int posicao2 = texto.LastIndexOf('.');

            texto = texto.Substring(posicao1, posicao2);

            texto = texto.ToLower();
            texto = texto.Trim();

            return texto;
        }

        /// <summary>
        /// Exemplo: nome-imagem.jpeg | Retorno: .jpeg
        /// </summary>
        /// <param name="texto"></param>
        public static string SomenteExtensao(this string texto)
        {
            int posicao1 = texto.LastIndexOf('.');
            int posicao2 = texto.Length - texto.LastIndexOf('.');

            texto = texto.Substring(posicao1, posicao2);

            texto = texto.ToLower();
            texto = texto.Trim();

            return texto;
        }

        /// <summary>
        /// Remove todos os caracteres especiais, substituindo-os por caracteres simples.
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public static string SimplificarTexto(this string texto)
        {
            StringBuilder sbReturn = new StringBuilder();
            var arrayText = texto.Normalize(NormalizationForm.FormD).ToCharArray();
            foreach (char letter in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    sbReturn.Append(letter);
            }

            texto = sbReturn.ToString();

            texto = System.Text.RegularExpressions.Regex.Replace(texto, @"[^0-9a-zA-ZéúíóáÉÚÍÓÁèùìòàÈÙÌÒÀõãñÕÃÑêûîôâÊÛÎÔÂëÿüïöäËYÜÏÖÄçÇ]+?", string.Empty);

            texto = texto.ToLower().Replace(" ", "");

            texto = texto.Trim();

            return texto;
        }
    }
}
