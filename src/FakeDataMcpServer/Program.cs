using FakeDataMcpServer.Tools;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = Host.CreateApplicationBuilder(args);

builder.Logging.AddConsole(o => o.LogToStandardErrorThreshold = LogLevel.Trace);

builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithTools<ContatosFakeDataTool>()
    .WithTools<EmpresasFakeDataTool>()
    .WithTools<ProdutosFakeDataTool>();

await builder.Build().RunAsync();