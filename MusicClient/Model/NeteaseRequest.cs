using System.Text.Json;
using System.Text.Json.Serialization;

namespace MusicClient.Model;

public class NeteaseSearchRequest
{
    public NeteaseSearchRequest()
    {
    }

    public NeteaseSearchRequest(Action<NeteaseSearchRequest> nsq)
    {
        nsq(this);
    }

    public string hlpretag => String.Empty;

    public string hlposttag => String.Empty;

    /// <summary>
    /// 欲搜索的关键词
    /// </summary>
    public string s { get; set; } = String.Empty;

    /// <summary>
    /// 搜索类型
    /// 1: 单曲, 10: 专辑, 100: 歌手, 1000: 歌单, 1002: 用户, 1004: MV, 1006: 歌词, 1009: 电台, 1014: 视频
    /// </summary>
    public int type { get; set; } = 1;

    /// <summary>
    /// 搜索数量
    /// </summary>
    public int limit { get; set; } = 30;


    public int offset { get; set; } = 0;


    public bool total { get; set; } = true;

    public virtual string BuildJsonString()
    {
        return JsonSerializer.Serialize(this, new JsonSerializerOptions()
        {
            WriteIndented = false,
            DefaultIgnoreCondition = JsonIgnoreCondition.Never,
        });
    }
}

public class NeteaseSongDetailRequest
{
    private static string _sid;

    public NeteaseSongDetailRequest(string songId)
    {
        _sid = songId;
    }

    public virtual string BuildJsonString()
    {
        return "{\"id\":\"{$SongId}\",\"c\":\"[{\\\"id\\\":\\\"{$SongId}\\\"}]\",\"csrf_token\":\"\"}".Replace(
            "{$SongId}", _sid);
    }
}

public class NeteaseLyricRequest
{
    private static string _sid;

    public NeteaseLyricRequest(string songId)
    {
        _sid = songId;
    }

    public string id { get; set; }
    public int lv = -1;
    public int tv = -1;
    public string csrf_token { get; }

    public virtual string BuildJsonString()
    {
        return JsonSerializer.Serialize(this, new JsonSerializerOptions()
        {
            WriteIndented = false,
            DefaultIgnoreCondition = JsonIgnoreCondition.Never,
        });
    }
}

public class NeteaseMVideoRequest
{
    public string id { get; init; }

    public NeteaseMVideoRequest(string id)
    {
        this.id = id;
    }

    public virtual string BuildJsonString()
    {
        return JsonSerializer.Serialize(this, new JsonSerializerOptions()
        {
            WriteIndented = false,
            DefaultIgnoreCondition = JsonIgnoreCondition.Never,
        });
    }
}

public class NeteaseMVideoUrlRequest
{
    public string id { get; init; }
    public string r { get; init; }

    public virtual string BuildJsonString()
    {
        return JsonSerializer.Serialize(this, new JsonSerializerOptions()
        {
            WriteIndented = false,
            DefaultIgnoreCondition = JsonIgnoreCondition.Never,
        });
    }
}