using System.Net.Http.Headers;

public struct Response
{
    public HttpResponseHeaders headers;
    public string response;

    public Response(HttpResponseHeaders headers, string response)
    {
        this.headers = headers;
        this.response = response;
    }
}