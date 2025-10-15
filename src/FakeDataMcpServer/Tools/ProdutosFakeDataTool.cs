using Bogus;
using FakeDataMcpServer.Models;
using FakeDataMcpServer.Validators;
using ModelContextProtocol.Server;
using System.ComponentModel;

namespace FakeDataMcpServer.Tools;

/// <summary>
/// MCP Tool para geracao de dados fake de Produtos
/// </summary>
internal class ProdutosFakeDataTool
{
    [McpServerTool]
    [Description("Gera uma lista com dados fake de produtos.")]
    public async Task<Result<Produto>> GerarDadosProdutosFake(
        [Description("Quantidade de registros")] int numberOfRecords)
    {
        try
        {
            var result = NumberOfRecordsValidator<Produto>.Validate(numberOfRecords)!;
            if (result.IsSuccess!.Value)
            {
                var random = new Random();
                var fakeProdutos = new Faker<Produto>("pt_BR").StrictMode(false)
                    .RuleFor(p => p.Nome, f => f.Commerce.Product())
                    .RuleFor(p => p.CodigoBarras, f => f.Commerce.Ean13())
                    .RuleFor(p => p.Preco, f => random.Next(10, 30))
                    .Generate(numberOfRecords);
                result.Data = fakeProdutos;
                result.Message = $"{numberOfRecords} produto(s) fake gerado(s) com sucesso!";
            }
            return await Task.FromResult(result);
        }
        catch (Exception ex)
        {
            return new Result<Produto>
            {
                IsSuccess = false,
                Message = $"Erro ao gerar dados fake de produtos: {ex.Message}"
            };
        }
    }
}