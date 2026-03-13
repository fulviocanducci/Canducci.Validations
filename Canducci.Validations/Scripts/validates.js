function validateCPF(value) {
  if (!value) return false;
  const cpf = value.replace(/\D/g, "");
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
  const cnpj = value.toUpperCase().replace(/[^A-Z0-9]/g, "");
  if (cnpj.length !== 14) return false;
  const charToNumber = (c) => {
    return c.charCodeAt(0) - 48;
  };
  const calc = (base, weights) => {
    let sum = 0;
    for (let i = 0; i < weights.length; i++) {
      sum += charToNumber(base[i]) * weights[i];
    }
    const r = sum % 11;
    return r < 2 ? 0 : 11 - r;
  };

  const w1 = [5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
  const w2 = [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
  const dv1 = calc(cnpj.slice(0, 12), w1);
  const dv2 = calc(cnpj.slice(0, 12) + dv1, w2);
  return dv1 === Number(cnpj[12]) && dv2 === Number(cnpj[13]);
}
