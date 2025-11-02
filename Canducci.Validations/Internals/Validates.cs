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

        public static bool ValidateCnpj(string cnpj)
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
    }
}
