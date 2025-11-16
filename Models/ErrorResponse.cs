namespace GoVisit.Models
{
    public class ErrorResponse
    {
        public string ErrorCode { get; set; } = default!;
        public string Message { get; set; } = default!;
        public string? Details { get; set; }
    }
}
