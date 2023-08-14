using System.Net;

namespace Medical.System.Core.Messages.Responses;
public class ErrorResponse
{
    public string Message { get; set; }
    public HttpStatusCode HttpStatusCode { get; set; }
    public IEnumerable<ErrorDetail> Errors { get; set; }
}