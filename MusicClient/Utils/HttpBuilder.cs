using System.Text;
using RestSharp;

namespace MusicClient.Utils;

public class HttpBuilder
{
    private readonly RestClient _client;
    private RestRequest _request;

    /// <summary>
    /// base url
    /// </summary>
    /// <param name="url"></param>
    public HttpBuilder(string url)
    {
        _client = new RestClient(url);
    }

    public HttpBuilder DefPath(string path, Method method)
    {
        _request = new RestRequest(path, method);
        return this;
    }

    public HttpBuilder DefUa(string ua)
    {
        this._request.AddHeader("User-Agent", ua);
        return this;
    }

    public HttpBuilder DefReferer(string @ref)
    {
        this._request.AddHeader("referer", @ref);
        return this;
    }

    public HttpBuilder DefDefaultEdgeUa()
    {
        this._request.AddHeader("User-Agent",
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36" +
            " (KHTML, like Gecko) Chrome/109.0.0.0 Safari/537.36 Edg/109.0.1518.70");
        return this;
    }

    public HttpBuilder DefUrlCookies(string cookies)
    {
        this._request.AddHeader("cookie", cookies);
        return this;
    }

    public HttpBuilder DefAuthority(string authority)
    {
        this._request.AddHeader("authority", authority);
        return this;
    }

    public HttpBuilder AddQueryParameter(string key, string value)
    {
        this._request.AddQueryParameter(key, value);
        return this;
    }

    public HttpBuilder AddJsonBody(Object o)
    {
        this._request.AddBody(o);
        return this;
    }

    public RestResponse Execute()
    {
        return this._client.Execute(_request);
    }

    public async Task<RestResponse> ExecuteAsync()
    {
        return await this._client.ExecuteAsync(_request);
    }
}