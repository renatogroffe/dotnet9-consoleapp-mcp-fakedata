using Bogus;
using FakeDataMcpServer.Models;
using FakeDataMcpServer.Validators;
using ModelContextProtocol.Server;
using System.ComponentModel;

namespace FakeDataMcpServer.Tools;

/// <summary>
/// MCP Tool para geracao de dados fake de Empresas
/// </summary>
internal class EmpresasFakeDataTool
{
    [McpServerTool]
    [Description("Gera uma lista com dados fake de empresas.")]
    public async Task<Result<Empresa>> GerarDadosEmpresasFake(
        [Description("Quantidade de registros")] int numberOfRecords)
    {
        try
        {
            var result = NumberOfRecordsValidator<Empresa>.Validate(numberOfRecords)!;
            if (result.IsSuccess!.Value)
            {
                var random = new Random();
                var fakeEmpresas = new Faker<Empresa>("pt_BR").StrictMode(false)
                    .RuleFor(e => e.Nome, f => f.Company.CompanyName())
                    .RuleFor(e => e.Cidade, f => f.Address.City())
                    .RuleFor(e => e.Pais, f => "Brasil")
                    .Generate(numberOfRecords);
                result.Data = fakeEmpresas;
                result.Message = $"{numberOfRecords} empresa(s) fake gerada(s) com sucesso!";
            }
            return await Task.FromResult(result);
        }
        catch (Exception ex)
        {
            return new Result<Empresa>
            {
                IsSuccess = false,
                Message = $"Erro ao gerar dados fake de empresas: {ex.Message}"
            };
        }
    }
}