using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleEstoque.Models
{
    public static class Helper
    {
        public static string NomeSemExtensao(this string texto)
        {
            int posicao1 = 0;
            int posicao2 = texto.LastIndexOf('.');

            texto = texto.Substring(posicao1, posicao2);

            return texto;
        }

        public static string SomenteExtensao(this string texto)
        {
            int posicao1 = texto.LastIndexOf('.');
            int posicao2 = texto.Length - texto.LastIndexOf('.');

            texto = texto.Substring(posicao1, posicao2);

            return texto;
        }

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

            return texto;
        }
    }
}
