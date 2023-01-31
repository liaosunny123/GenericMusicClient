using System.Text.Json;
using RestSharp;

namespace MusicClient.Utils;

public class JsonParameter
{
    private RestRequest _restRequest;
    private HttpBuilder _httpBuilder;
    private Dictionary<string, object> _objects = new();
    private Stack<string> _stack = new();
    private Stack<Dictionary<string, object>> _subParameters = new();
    public JsonParameter(RestRequest restRequest, HttpBuilder httpBuilder)
    {
        this._restRequest = restRequest;
        this._httpBuilder = httpBuilder;
    }

    public JsonParameter AddJsonParameter(string key, object value)
    {
        _objects.Add(key, value);
        return this;
    }

    public JsonParameter AddJsonParameters(string key)
    {
        this._stack.Push(key);
        this._subParameters.Push(new Dictionary<string, object>());
        return this;
    }

    public JsonParameter AddSubParameter(string key, object value)
    {
        var temp = this._subParameters.Pop();
        temp.Add(key,value);
        this._subParameters.Push(temp);
        return this;
    }

    public JsonParameter EndAddJsonSubParameter()
    {
        var temp = this._subParameters.Pop();
        if (_subParameters.TryPop(out var res))
        {
            res.Add(_stack.Pop(),temp);
            _subParameters.Push(res);
        }
        else
        {
            this._objects.Add(_stack.Pop(),temp);
        }
        return this;
    }

    public HttpBuilder EndAddJsonParameter()
    {
        JsonSerializerOptions jso = new JsonSerializerOptions();
        jso.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
        _restRequest.AddJsonBody(JsonSerializer.Serialize(_objects, jso));
        return _httpBuilder;
    }
}