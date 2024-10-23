using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Infrastructure.Operations
{
    public static class NameOperation
    {
        public static string CharacterRegulatory(string name)
            => name
            .Replace("\"", "")
            .Replace("!", "")
            .Replace("'", "")
            .Replace("^", "")
            .Replace("+", "")
            .Replace("%", "")
            .Replace("&", "")
            .Replace("/", "")
            .Replace("(", "")
            .Replace(")", "")
            .Replace("=", "")
            .Replace("?", "")
            .Replace("_", "")
            .Replace("@", "")
            .Replace("€", "")
            .Replace("``", "")
            .Replace("~", "")
            .Replace(",", "")
            .Replace(";", "")
            .Replace(":", "")
            .Replace(".", "")
            .Replace("Ö", "O")
            .Replace("ö", "o")
            .Replace("Ü", "u")
            .Replace("ü", "u")
            .Replace("ı", "i")
            .Replace("İ", "I")
            .Replace("Ğ", "G")
            .Replace("ğ", "g")
            .Replace("æ", "")
            .Replace("â", "a")
            .Replace("Â", "a")
            .Replace("î", "i")
            .Replace("Î", "i")
            .Replace("Ş", "s")
            .Replace("ş", "s")
            .Replace("Ç", "c")
            .Replace("ç", "c")
            .Replace("<", "")
            .Replace(">", "")
            .Replace("|", "")
            .Replace(" ", "")
            ;                        
    }
}
