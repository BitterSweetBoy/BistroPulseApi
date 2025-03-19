namespace Module.Shared.Response
{
    public record ApiResponse<T>(
        bool Success,
        string Message,
        T Data,
        Dictionary<string, string[]> Errors = null
    );
}
