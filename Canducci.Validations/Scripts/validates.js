function validateCPF(value) {
   if (!value) return false;
   const cpf = value.replace(/\D/g, '');
   if (cpf.length !== 11) return false;
   if (/^(\d)\1{10}$/.test(cpf)) return false;
   let sum = 0;
   for (let i = 0; i < 9; i++) sum += parseInt(cpf.charAt(i), 10) * (10 - i);
   let r = (sum * 10) % 11;
   if (r === 10) r = 0;
   if (r !== parseInt(cpf.charAt(9), 10)) return false;
   sum = 0;
   for (let i = 0; i < 10; i++) sum += parseInt(cpf.charAt(i), 10) * (11 - i);
   r = (sum * 10) % 11;
   if (r === 10) r = 0;
   if (r !== parseInt(cpf.charAt(10), 10)) return false;
   return true;
}

function validateCNPJ(value) {
   if (!value) return false;
   const cnpj = value.replace(/\D/g, '');
   if (cnpj.length !== 14) return false;
   if (/^(\d)\1{13}$/.test(cnpj)) return false;

   const calcVerifier = (cnpjSlice, weights) => {
      let sum = 0;
      for (let i = 0; i < weights.length; i++) {
         sum += parseInt(cnpjSlice.charAt(i), 10) * weights[i];
      }
      let r = sum % 11;
      return r < 2 ? 0 : 11 - r;
   };

   const firstWeights = [5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
   const secondWeights = [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];

   const firstDigit = calcVerifier(cnpj.slice(0, 12), firstWeights);
   const secondDigit = calcVerifier(cnpj.slice(0, 12) + firstDigit, secondWeights);

   return firstDigit === parseInt(cnpj.charAt(12), 10) &&
      secondDigit === parseInt(cnpj.charAt(13), 10);
}