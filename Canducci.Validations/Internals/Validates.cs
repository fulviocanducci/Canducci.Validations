using System;
using System.Text.RegularExpressions;
namespace Canducci.Validations.Internals
{
    public static class Validates
    {
        public static bool ValidateCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf)) return false;

            cpf = Regex.Replace(cpf, @"\D", "");
            if (cpf.Length != 11) return false;
            if (Regex.IsMatch(cpf, @"^(\d)\1{10}$"))
            {
                return false;
            }

            int sum = 0;
            for (int i = 0; i < 9; i++)
                sum += (cpf[i] - '0') * (10 - i);

            int r = (sum * 10) % 11;
            if (r == 10) r = 0;
            if (r != (cpf[9] - '0')) return false;

            sum = 0;
            for (int i = 0; i < 10; i++)
                sum += (cpf[i] - '0') * (11 - i);

            r = (sum * 10) % 11;
            if (r == 10) r = 0;

            return r == (cpf[10] - '0');
        }

        [Obsolete("CNPJ old validation.")]
        public static bool ValidateCnpjOld(string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj)) return false;

            cnpj = Regex.Replace(cnpj, @"\D", "");
            if (cnpj.Length != 14) return false;
            if (Regex.IsMatch(cnpj, @"^(\d)\1{13}$"))
            {
                return false;
            }

            int[] firstWeights = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] secondWeights = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            int CalcDigit(string slice, int[] weights)
            {
                int sum = 0;
                for (int i = 0; i < weights.Length; i++)
                    sum += (slice[i] - '0') * weights[i];
                int r = sum % 11;
                return r < 2 ? 0 : 11 - r;
            }

            int firstDigit = CalcDigit(cnpj.Substring(0, 12), firstWeights);
            int secondDigit = CalcDigit(cnpj.Substring(0, 12) + firstDigit, secondWeights);

            return firstDigit == (cnpj[12] - '0') && secondDigit == (cnpj[13] - '0');
        }

        public static bool ValidateCnpj(ReadOnlySpan<char> input)
        {
            Span<char> buffer = stackalloc char[14];
            int length = 0;
            foreach (char c in input)
            {
                if (char.IsLetterOrDigit(c))
                {
                    if (length == 14)
                        return false;
                    buffer[length++] = char.ToUpperInvariant(c);
                }
            }
            if (length != 14) return false;
            int dv1 = CalculateDigit(buffer.Slice(0, 12), Weights1);
            int dv2 = CalculateDigit(buffer.Slice(0, 13), Weights2);
            return buffer[12] - '0' == dv1 && buffer[13] - '0' == dv2;
        }

        private static int CalculateDigit(ReadOnlySpan<char> span, ReadOnlySpan<int> weights)
        {
            int sum = 0;
            for (int i = 0; i < weights.Length; i++)
            {
                int value = CharToNumber(span[i]);
                sum += value * weights[i];
            }
            int remainder = sum % 11;
            return remainder < 2 ? 0 : 11 - remainder;
        }

        private static int CharToNumber(char c) => (c >= '0' && c <= '9') ? c - '0' : c;
        private static ReadOnlySpan<int> Weights1 => new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        private static ReadOnlySpan<int> Weights2 => new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
    }
}
