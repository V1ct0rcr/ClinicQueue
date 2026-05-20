namespace eUseControl.Domain.Models.Responses;

public class ActionResponse
{
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
}

public class ActionResponse<T> : ActionResponse
{
    public T? Data { get; set; }
}
