using System.Text;
using System.Text.Json;
using RestSharp;

namespace MusicClient.Utils;

public class HttpBuilder
{
    private RestClient _restClient;
    private RestRequest _restRequest;

    /// <summary>
    /// 靶 Url
    /// </summary>
    /// <param name="url"></param>
    public HttpBuilder(string url)
    {
        _restClient = new RestClient(url);
    }

    public HttpBuilder DefPath(string path, Method method)
    {
        _restRequest = new RestRequest(path, method);
        return this;
    }

    public HttpBuilder DefUa(string ua)
    {
        this._restRequest.AddHeader("User-Agent", ua);
        return this;
    }

    public HttpBuilder DefReferer(string @ref)
    {
        this._restRequest.AddHeader("Referer", @ref);
        return this;
    }

    public HttpBuilder DefDefaultEdgeUa()
    {
        this._restRequest.AddHeader("User-Agent",
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36" +
            " (KHTML, like Gecko) Chrome/109.0.0.0 Safari/537.36 Edg/109.0.1518.70");
        return this;
    }

    public HttpBuilder DefUrlCookies(string cookies)
    {
        this._restRequest.AddHeader("cookie", cookies);
        return this;
    }

    public HttpBuilder DefAuthority(string authority)
    {
        this._restRequest.AddHeader("authority", authority);
        return this;
    }

    public HttpBuilder AddQueryParameter(string key, string value)
    {
        this._restRequest.AddQueryParameter(key, value);
        return this;
    }

    public HttpBuilder AddJsonBody(Object o)
    {
        JsonSerializerOptions jso = new JsonSerializerOptions();
        jso.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
        this._restRequest.AddJsonBody(JsonSerializer.Serialize(o, jso));
        return this;
    }

    public JsonParameter DefJsonParameter()
    {
        return new JsonParameter(_restRequest, this);
    }

    public HttpBuilder DefHost(string host)
    {
        this._restRequest.AddHeader("Host", host);
        return this;
    }

    public RestResponse Excute()
    {
        return this._restClient.Execute(_restRequest);
    }

    public async Task<RestResponse> ExcuteAsync()
    {
        return await this._restClient.ExecuteAsync(_restRequest);
    }
}