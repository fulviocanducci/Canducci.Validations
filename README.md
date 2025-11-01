# Canducci.Validations

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
Install-Package Canducci.Validations -Version 1.0.0
```

### Via .NET CLI
```bash
dotnet add package Canducci.Validations --version 1.0.0
```

### Via PackageReference (csproj)
```xml
<PackageReference Include="Canducci.Validations" Version="1.0.0" />
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

### Helper para Scripts de Validação

Para facilitar a inclusão dos scripts client-side, utilize o helper `ValidationScriptsHelper`:

**No seu Layout ou View:**
```html
@using Canducci.Validations.Helpers

@* Injeção automática de todos os scripts necessários *@
@Html.AddValidationScripts()
```

**Scripts incluídos automaticamente:**
- `dayjs.min.js` - Biblioteca Day.js para manipulação de datas
- `jquery.validate.config.js` - Configuração do jQuery Validation
- `jquery.validate.dateoroptional.js` - Validação client-side para DateOrOptional
- `jquery.validate.datetimeoroptional.js` - Validação client-side para DateTimeOrOptional
- `jquery.validate.timeoroptional.js` - Validação client-side para TimeOrOptional

**Uso em View específica:**
```html
@page
@model AppointmentModel
@{
    ViewData["Title"] = "Criar Agendamento";
}

<h2>Criar Agendamento</h2>

<form asp-action="CreateAppointment">
    <!-- Campos do formulário -->
</form>

@section Scripts {
    @Html.AddValidationScripts()
}
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
<input type="text" 
       name="AppointmentDate" 
       data-val="true" 
       data-val-date-or-optional="Date inválid" 
       data-val-date-or-optional-formats="DD/MM/YYYY,YYYY-MM-DD" />
```

### 3. Formatos Personalizados

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
using Canducci.Validations.Attributes;

public class Program
{
    static void Main()
    {
        var model = new SampleModel 
        { 
            EventDate = DateTime.Now 
        };
        
        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();
        
        bool isValid = Validator.TryValidateObject(model, context, results, true);
        
        if (!isValid)
        {
            foreach (var result in results)
            {
                Console.WriteLine($"Erro: {result.ErrorMessage}");
            }
        }
    }
}

public class SampleModel
{
    [DateOrOptional]
    public DateTime? EventDate { get; set; }
}
```

## 🧪 Testes

O projeto inclui **26 testes unitários completos** que cobrem:

- ✅ Validação com valores nulos
- ✅ Validação com tipos válidos (DateTime, DateOnly, TimeSpan, TimeOnly)
- ✅ Validação com valores inválidos
- ✅ Configuração de formatos personalizados
- ✅ Mensagens de erro personalizadas
- ✅ Integração com validação de modelo
- ✅ Compatibilidade multi-framework

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

## 🔗 Dependências

- **.NET 6.0+** ou **.NET Standard 2.1+**
- **jQuery** (para validação client-side)
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