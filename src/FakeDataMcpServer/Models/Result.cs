namespace FakeDataMcpServer.Models;

public class Result<T> where T : class
{
    public bool? IsSuccess { get; set; }
    public string? Message { get; set; }
    public List<T>? Data { get; set; }
}