using System;

namespace Sat.Recruitment.Api.Utils
{
    public static class EmailUtils
    {
        public static string NormalizeEmail(string email)
        {
            if (String.IsNullOrWhiteSpace(email))
            {
                return string.Empty;
            }

            var aux = email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);
            aux[0] = atIndex < 0 ? aux[0].Replace(".", string.Empty) : aux[0].Replace(".", string.Empty).Remove(atIndex);
            return string.Join("@", new string[] { aux[0], aux[1] });
        }
    }
}