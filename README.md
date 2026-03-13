# Canducci.Validations

[![Version](https://img.shields.io/nuget/v/Canducci.Validations.svg?style=plastic&label=version)](https://www.nuget.org/packages/Canducci.Validations/) [![NuGet](https://img.shields.io/nuget/dt/Canducci.Validations.svg)](https://www.nuget.org/packages/Canducci.Validations/)
[![Canducci.Validations](https://github.com/fulviocanducci/Canducci.Validations/actions/workflows/dotnet-desktop.yml/badge.svg)](https://github.com/fulviocanducci/Canducci.Validations/actions/workflows/dotnet-desktop.yml)

Pacote de validação customizadas para **ASP.NET Core MVC** e **.NET Standard** com suporte completo para validação client-side e server-side.

## 🚀 Características

- ✅ **Validação Server-side**: Implementa `DataAnnotations` para validação em Models
- ✅ **Validação Client-side**: Suporte completo para jQuery Unobtrusive + Day.js
- ✅ **Compatibilidade Multi-Framework**: Suporte para .NET 6.0+, .NET Standard 2.1+
- ✅ **Validações de Data/Hora**: Validações específicas para datas, DateTime e horários
- ✅ **Validações Opcionais**: Aceita valores opcionais (null) ou válidos
- ✅ **Formatos Personalizados**: Configuração flexível de formatos de data/hora
- ✅ **Helper para Scripts**: Extension method para injeção automática de scripts
- ✅ **Suporte Multilíngue**: Scripts de localização inclusos
- ✅ **Testes Completos**: 26 testes unitários com cobertura abrangente

## 📦 Instalação

### Via NuGet Package Manager

```bash
Install-Package Canducci.Validations
```

### Via .NET CLI

```bash
dotnet add package Canducci.Validations
```

### Via PackageReference (csproj)

```xml
<PackageReference Include="Canducci.Validations"/>
```

## 🎯 Validações Disponíveis

### DateOrOptionalAttribute

Valida se o valor é uma data válida (`DateTime` ou `DateOnly`) ou opcional (null).

```csharp
public class EventModel
{
    [DateOrOptional]
    public DateTime? EventDate { get; set; }

    [DateOrOptional("DD-MM-YYYY", "YYYY-MM-DD")]
    public DateOnly? AlternativeDate { get; set; }
}
```

### DateTimeOrOptionalAttribute

Valida se o valor é um `DateTime` válido ou opcional (null).

```csharp
public class MeetingModel
{
    [DateTimeOrOptional]
    public DateTime? MeetingDateTime { get; set; }

    [DateTimeOrOptional("DD-MM-YYYY HH:mm", "MM/DD/YYYY HH:mm:ss")]
    public DateTime? ScheduledTime { get; set; }
}
```

### TimeOrOptionalAttribute

Valida se o valor é um horário válido (`TimeSpan` ou `TimeOnly`) ou opcional (null).

```csharp
public class ScheduleModel
{
    [TimeOrOptional]
    public TimeSpan? WorkTime { get; set; }

    [TimeOrOptional("HH:mm:ss")]
    public TimeOnly? EventTime { get; set; }
}
```

### CpfOrOptionAttribute

Valida se o valor é um CPF válido ou opcional (null). Aceita CPF com ou sem formatação.

```csharp
public class PersonModel
{
    [CpfOrOption]
    public string? CPF { get; set; }

    [CpfOrOption(ErrorMessage = "CPF inválido")]
    public string? PersonalCPF { get; set; }
}
```

**CPF aceitos:**

- `12345678901` (apenas dígitos)
- `123.456.789-01` (com formatação)

### CnpjOrOptionalAttribute

Valida se o valor é um CNPJ válido ou opcional (null). Aceita tanto o modelo tradicional numérico quanto o novo modelo alfanumérico, com ou sem formatação.

```csharp
public class CompanyModel
{
    [CnpjOrOptional]
    public string? CNPJ { get; set; }

    [CnpjOrOptional(ErrorMessage = "CNPJ inválido")]
    public string? CompanyCNPJ { get; set; }
}
```

**CNPJ aceitos:**

- `12345678000195` (apenas dígitos)
- `12.345.678/0001-95` (com formatação)
- `12ABC34501DE45` (alfanumérico sem formatação)
- `12.ABC.345/01DE-45` (alfanumérico com formatação)

### CpfCnpjOrOptionalAttribute

Valida se o valor é um CPF OU CNPJ válido ou opcional (null). Para CNPJ, aceita tanto o modelo tradicional numérico quanto o novo modelo alfanumérico. Útil para campos que podem aceitar ambos os tipos de documento.

```csharp
public class DocumentModel
{
    [CpfCnpjOrOptional]
    public string? Document { get; set; }

    [CpfCnpjOrOptional(ErrorMessage = "CPF ou CNPJ inválido")]
    public string? PersonalOrCompanyDocument { get; set; }
}
```

**Documentos aceitos:**

- CPF: `12345678901` ou `123.456.789-01`
- CNPJ numérico: `12345678000195` ou `12.345.678/0001-95`
- CNPJ alfanumérico: `12ABC34501DE45` ou `12.ABC.345/01DE-45`

### Helper para Scripts de Validação

Para facilitar a inclusão dos scripts client-side, utilize o helper `ValidationScriptsHelper`:

**No seu Layout ou View:**

```html
@using Canducci.Validations.Helpers @* Injeção automática de todos os scripts necessários *@ @Html.AddValidationScripts()
```

**Scripts incluídos automaticamente:**

- `dayjs.min.js` - Biblioteca Day.js para manipulação de datas
- `jquery.validate.config.js` - Configuração do jQuery Validation
- `jquery.validate.dateoroptional.js` - Validação client-side para DateOrOptional
- `jquery.validate.datetimeoroptional.js` - Validação client-side para DateTimeOrOptional
- `jquery.validate.timeoroptional.js` - Validação client-side para TimeOrOptional
- `jquery.validate.cpforoptional.js` - Validação client-side para CpfOrOption
- `jquery.validate.cnpjoroptional.js` - Validação client-side para CnpjOrOptional
- `jquery.validate.cpfcnpjoroptional.js` - Validação client-side para CpfCnpjOrOptional
- `validates.js` - Funções de validação CPF/CNPJ

**Uso em View específica:**

```html
@page @model AppointmentModel @{ ViewData["Title"] = "Criar Agendamento"; }

<h2>Criar Agendamento</h2>

<form asp-action="CreateAppointment">
  <!-- Campos do formulário -->
</form>

@section Scripts { @Html.AddValidationScripts() }
```

## 💻 Exemplos de Uso

### 1. Validação Server-Side (MVC)

```csharp
public class AppointmentModel
{
    [Required]
    [DateOrOptional]
    public DateTime? AppointmentDate { get; set; }

    [Required]
    [TimeOrOptional]
    public TimeSpan? AppointmentTime { get; set; }

    [Required]
    [DateTimeOrOptional]
    public DateTime? CreatedAt { get; set; }

    [CpfOrOption]
    public string? ClientCPF { get; set; }

    [CnpjOrOptional]
    public string? CompanyCNPJ { get; set; }

    [CpfCnpjOrOptional]
    public string? PersonalOrCompanyDocument { get; set; }
}
```

**Controller:**

```csharp
[HttpPost]
public IActionResult CreateAppointment(AppointmentModel model)
{
    if (ModelState.IsValid)
    {
        // Processar agendamento
        return RedirectToAction("Success");
    }

    return View(model);
}
```

**View:**

```html
@model AppointmentModel

<form asp-action="CreateAppointment">
  <div>
    <label asp-for="AppointmentDate"></label>
    <input asp-for="AppointmentDate" />
    <span asp-validation-for="AppointmentDate"></span>
  </div>

  <div>
    <label asp-for="AppointmentTime"></label>
    <input asp-for="AppointmentTime" />
    <span asp-validation-for="AppointmentTime"></span>
  </div>

  <div>
    <label asp-for="CreatedAt"></label>
    <input asp-for="CreatedAt" />
    <span asp-validation-for="CreatedAt"></span>
  </div>

  <button type="submit">Criar Agendamento</button>
</form>

@section Scripts {
<partial name="_ValidationScriptsPartial" />
}
```

### 2. Validação Client-Side

O pacote gera automaticamente os atributos data-val para validação client-side:

```html
<!-- Date Validation -->
<input
  type="text"
  name="AppointmentDate"
  data-val="true"
  data-val-date-or-optional="Date inválid"
  data-val-date-or-optional-formats="DD/MM/YYYY,YYYY-MM-DD"
/>

<!-- CPF Validation -->
<input type="text" name="ClientCPF" data-val="true" data-val-cpf-or-optional="CPF inválid" />

<!-- CNPJ Validation -->
<input type="text" name="CompanyCNPJ" data-val="true" data-val-cnpj-or-optional="CNPJ inválid" />

<!-- CPF/CNPJ Validation -->
<input type="text" name="PersonalOrCompanyDocument" data-val="true" data-val-cpfcnpj-or-optional="CPF ou CNPJ inválid" />
```

### 3. Formulário Completo com Validação CPF/CNPJ

```csharp
public class CustomerModel
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [CpfOrOption]
    public string? CPF { get; set; }

    [CnpjOrOptional]
    public string? CNPJ { get; set; }

    [CpfCnpjOrOptional]
    public string? Document { get; set; }

    [DateOrOptional]
    public DateTime? BirthDate { get; set; }

    [TimeOrOptional]
    public TimeSpan? PreferredTime { get; set; }
}
```

### 4. Formatos Personalizados (Data)

```csharp
public class CustomDateModel
{
    // Formatos personalizados para validação client-side
    [DateOrOptional("DD-MM-YYYY", "MM/DD/YYYY", "DD.MM.YYYY")]
    public DateTime? CustomDate { get; set; }

    // Formatos padrão: "DD/MM/YYYY", "YYYY-MM-DD"
    [DateOrOptional]
    public DateOnly? DefaultDate { get; set; }
}
```

### 5. Exemplo de Formulário Web

```html
@model CustomerModel

<form asp-action="CreateCustomer">
  <div>
    <label asp-for="Name">Nome:</label>
    <input asp-for="Name" />
    <span asp-validation-for="Name"></span>
  </div>

  <div>
    <label asp-for="CPF">CPF (opcional):</label>
    <input asp-for="CPF" placeholder="000.000.000-00" />
    <span asp-validation-for="CPF"></span>
  </div>

  <div>
    <label asp-for="CNPJ">CNPJ (opcional):</label>
    <input asp-for="CNPJ" placeholder="00.000.000/0000-00" />
    <span asp-validation-for="CNPJ"></span>
  </div>

  <div>
    <label asp-for="Document">CPF ou CNPJ:</label>
    <input asp-for="Document" placeholder="000.000.000-00 ou 00.000.000/0000-00" />
    <span asp-validation-for="Document"></span>
  </div>

  <div>
    <label asp-for="BirthDate">Data de Nascimento (opcional):</label>
    <input asp-for="BirthDate" />
    <span asp-validation-for="BirthDate"></span>
  </div>

  <div>
    <label asp-for="PreferredTime">Horário Preferido (opcional):</label>
    <input asp-for="PreferredTime" />
    <span asp-validation-for="PreferredTime"></span>
  </div>

  <button type="submit">Cadastrar</button>
</form>

@section Scripts { @Html.AddValidationScripts() }
```

## 🔧 Configuração

### ASP.NET Core MVC

1. **Instalar o pacote:**

```bash
dotnet add package Canducci.Validations --version 1.0.0
```

2. **Os scripts são incluídos automaticamente** via arquivo `.targets`

### .NET Standard/Console Applications

```csharp
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Canducci.Validations.Attributes;

public class Program
{
    static void Main()
    {
        // Testando validação CPF
        var cpfModel = new PersonModel { CPF = "123.456.789-09" };
        var cpfContext = new ValidationContext(cpfModel);
        var cpfResults = new List<ValidationResult>();
        bool cpfValid = Validator.TryValidateObject(cpfModel, cpfContext, cpfResults, true);

        Console.WriteLine($"CPF válido: {cpfValid}");
        if (!cpfValid)
        {
            foreach (var result in cpfResults)
            {
                Console.WriteLine($"Erro CPF: {result.ErrorMessage}");
            }
        }

        // Testando validação CNPJ
        var cnpjModel = new CompanyModel { CNPJ = "12.ABC.345/01DE-45" };
        var cnpjContext = new ValidationContext(cnpjModel);
        var cnpjResults = new List<ValidationResult>();
        bool cnpjValid = Validator.TryValidateObject(cnpjModel, cnpjContext, cnpjResults, true);

        Console.WriteLine($"CNPJ válido: {cnpjValid}");
        if (!cnpjValid)
        {
            foreach (var result in cnpjResults)
            {
                Console.WriteLine($"Erro CNPJ: {result.ErrorMessage}");
            }
        }

        // Testando validação CPF ou CNPJ
        var documentModel = new DocumentModel { Document = "12345678901" };
        var documentContext = new ValidationContext(documentModel);
        var documentResults = new List<ValidationResult>();
        bool documentValid = Validator.TryValidateObject(documentModel, documentContext, documentResults, true);

        Console.WriteLine($"Documento válido: {documentValid}");
        if (!documentValid)
        {
            foreach (var result in documentResults)
            {
                Console.WriteLine($"Erro Documento: {result.ErrorMessage}");
            }
        }
    }
}

public class PersonModel
{
    [CpfOrOption]
    public string? CPF { get; set; }
}

public class CompanyModel
{
    [CnpjOrOptional]
    public string? CNPJ { get; set; }
}

public class DocumentModel
{
    [CpfCnpjOrOptional]
    public string? Document { get; set; }
}
```

## 🧪 Testes

O projeto inclui **30+ testes unitários completos** que cobrem:

**Testes de Data/Hora:**

- ✅ Validação com valores nulos
- ✅ Validação com tipos válidos (DateTime, DateOnly, TimeSpan, TimeOnly)
- ✅ Validação com valores inválidos
- ✅ Configuração de formatos personalizados
- ✅ Mensagens de erro personalizadas

**Testes de CPF/CNPJ:**

- ✅ Validação CPF com formatação (123.456.789-09)
- ✅ Validação CPF sem formatação (12345678909)
- ✅ Validação CPF inválido
- ✅ Validação CNPJ com formatação (12.345.678/0001-95)
- ✅ Validação CNPJ sem formatação (12345678000195)
- ✅ Validação CNPJ inválido
- ✅ Validação CPF/CNPJ combinado
- ✅ Validação com valores nulos/ vazios

**Gerais:**

- ✅ Integração com validação de modelo
- ✅ Compatibilidade multi-framework
- ✅ Validação client-side e server-side

**Executar testes:**

```bash
cd Canducci.Validation.TestProject
dotnet test
```

## 🌐 Suporte a Localização

O pacote inclui suporte para **50+ idiomas** através dos scripts de localização inclusos:

- `pt-br.js` - Português (Brasil)
- `en.js` - Inglês
- `es.js` - Espanhol
- `fr.js` - Francês
- E muitos outros...

## 📋 Formatos Padrão

### DateOrOptionalAttribute

- `DD/MM/YYYY`
- `YYYY-MM-DD`

### DateTimeOrOptionalAttribute

- `DD/MM/YYYY`
- `DD/MM/YYYY HH:mm`
- `DD/MM/YYYY HH:mm:ss`
- `YYYY-MM-DD`
- `YYYY-MM-DD HH:mm`
- `YYYY-MM-DD HH:mm:ss`

### TimeOrOptionalAttribute

- `HH:mm`
- `HH:mm:ss`

### CPF/CNPJ Validation

**CPF (CpfOrOptionAttribute):**

- Aceita: `12345678901` (11 dígitos)
- Aceita: `123.456.789-09` (com formatação)
- Rejeita: Números inválidos ou com menos/mais de 11 dígitos

**CNPJ (CnpjOrOptionalAttribute):**

- Aceita: `12345678000195` (14 caracteres numéricos)
- Aceita: `12.345.678/0001-95` (numérico com formatação)
- Aceita: `12ABC34501DE45` (14 caracteres alfanuméricos)
- Aceita: `12.ABC.345/01DE-45` (alfanumérico com formatação)
- Rejeita: valores inválidos ou com menos/mais de 14 caracteres alfanuméricos

**CPF ou CNPJ (CpfCnpjOrOptionalAttribute):**

- Aceita: Qualquer CPF válido
- Aceita: Qualquer CNPJ válido, numérico ou alfanumérico
- Rejeita: Documentos inválidos em ambos os formatos

## 🔗 Dependências

- **.NET 6.0+** ou **.NET Standard 2.1+**
- **jQuery** (para validação client-side)
- **jQuery Validation** (plugin de validação)
- **jQuery Validation Unobtrusive** (integração com ASP.NET Core)
- **Day.js** (para validação de data client-side)
- **Microsoft.AspNetCore.Mvc.ModelBinding.Validation** (para .NET 6.0+)

## 📄 Licença

Este projeto está sob a licença MIT. Consulte o arquivo `LICENSE.txt` para mais detalhes.

## 🤝 Contribuição

Contribuições são bem-vindas! Por favor:

1. Faça um fork do projeto
2. Crie uma branch para sua feature (`git checkout -b feature/AmazingFeature`)
3. Commit suas mudanças (`git commit -m 'Add some AmazingFeature'`)
4. Push para a branch (`git push origin feature/AmazingFeature`)
5. Abra um Pull Request

## 📞 Suporte

Para suporte e questões:

- Abra uma **Issue** no GitHub
- Consulte a **documentação de testes** para exemplos práticos

---

**Desenvolvido com ❤️ pela comunidade .NET**

## 📝 Changelog

### Versão 0.0.8+

- ✅ Adicionadas validações de CPF (`CpfOrOptionAttribute`)
- ✅ Adicionadas validações de CNPJ (`CnpjOrOptionalAttribute`)
- ✅ Adicionadas validações de CPF/CNPJ combinado (`CpfCnpjOrOptionalAttribute`)
- ✅ Implementadas validações client-side para CPF/CNPJ
- ✅ Scripts JavaScript para validação CPF/CNPJ
- ✅ Suporte a formatação com e sem pontuação
- ✅ Suporte ao novo formato de CNPJ alfanumérico
- ✅ Integração completa com jQuery Validation
- ✅ Testes unitários para todas as validações CPF/CNPJ
