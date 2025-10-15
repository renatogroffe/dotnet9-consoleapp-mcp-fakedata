using Bogus;
using FakeDataMcpServer.Models;
using FakeDataMcpServer.Validators;
using ModelContextProtocol.Server;
using System.ComponentModel;

namespace FakeDataMcpServer.Tools;

/// <summary>
/// MCP Tool para geracao de dados fake de contatos.
/// </summary>
internal class ContatosFakeDataTool
{
    [McpServerTool]
    [Description("Gera uma lista com dados fake de contatos.")]
    public async Task<Result<Contato>> GerarDadosContatosFake(
        [Description("Quantidade de registros")] int numberOfRecords)
    {
        try
        {
            var result = NumberOfRecordsValidator<Contato>.Validate(numberOfRecords)!;
            if (result.IsSuccess!.Value)
            {
                var random = new Random();
                var fakeContatos = new Faker<Contato>("pt_BR").StrictMode(false)
                            .RuleFor(c => c.Nome, f => f.Company.CompanyName())
                            .RuleFor(c => c.Telefone, f => f.Phone.PhoneNumber())
                            .Generate(numberOfRecords);
                result.Data = fakeContatos;
                result.Message = $"{numberOfRecords} contato(s) fake gerado(s) com sucesso!";
            }
            return await Task.FromResult(result);
        }
        catch (Exception ex)
        {
            return new Result<Contato>
            {
                IsSuccess = false,
                Message = $"Erro ao gerar dados fake de contatos: {ex.Message}"
            };
        }
    }
}