namespace VertexERP.Shared.Error;

public class ErrorDetails
{
    public int Status { get; set; }
    public string Title { get; set; } = default!;
    public string Detail { get; set; } = default!;
    public string Instance { get; set; } = default!;
    public string CorrelationId { get; set; } = default!;
    public string? ExceptionMessage { get; set; }
}