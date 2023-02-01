using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Nodes;
using MusicClient.Utils;
using RestSharp;

namespace MusicClient.Model;

public class NeteaseSongInfo : SongInfo
{
    public string AlbumId { get; init; }
    public string MVId { get; init; }

    private readonly HttpBuilder _httpBuilder = new("https://music.163.com");

    public override async Task<string?> GetMVUrl(VideoType videoType = VideoType.WebUrl)
    {
        return videoType switch
        {
            VideoType.WebUrl => $"https://music.163.com/#/mv?id={MVId}",
            _ => GetMVDirectUrl(videoType).Result
        };
    }

    public override async Task<string?> GetRawLyrics(LyricType lyricType = LyricType.Origin)
    {
        var l = GetLyric(Id).Result;

        return lyricType switch
        {
            LyricType.Origin => l[LyricType.Origin],
            LyricType.Translation => l[LyricType.Translation],
            LyricType.Transliteration => l[LyricType.Transliteration],
            _ => null
        };
    }

    public override async Task<Comment?> GetComment()
    {
        throw new NotImplementedException();
    }

    public override async Task<string?> GetDirectUrl(MusicType musicType = MusicType.Auto)
    {
        return DirectUrl;
    }

    public async Task<Dictionary<LyricType, string?>?> GetLyric(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            return null;
        }

        var e = Crypto.NeteaseEncrypt(new NeteaseLyricRequest(id).BuildJsonString());

        var r = await _httpBuilder.DefPath("/weapi/song/lyric", Method.Post)
            .AddQueryParameter("params", e["params"])
            .AddQueryParameter("encSecKey", e["encSecKey"])
            .ExecuteAsync();

        var json = JsonNode.Parse(r.Content);

        var ret = new Dictionary<LyricType, string>();
        ret.Add(LyricType.Origin, json["lrc"]["lyric"].ToString());
        ret.Add(LyricType.Translation, json["tlyric"]["lyric"].ToString());
        ret.Add(LyricType.Transliteration, null);

        return ret;
    }

    public async Task<List<VideoType?>?> GetMVQuality(string mvid)
    {
        if (string.IsNullOrWhiteSpace(mvid))
        {
            return null;
        }

        var e = Crypto.NeteaseEncrypt(new NeteaseMVideoRequest(mvid).BuildJsonString());

        var r = await _httpBuilder.DefPath("/weapi/v1/mv/detail", Method.Post)
            .AddQueryParameter("params", e["params"])
            .AddQueryParameter("encSecKey", e["encSecKey"])
            .ExecuteAsync();

        var json = JsonNode.Parse(r.Content);

        var ret = new List<string>();
        foreach (var item in json["data"]["brs"].AsArray())
        {
            ret.Add(item["br"].ToString());
        }

        return ret.Select(VideoQualityEnumConverter.FromNetease).ToList();
    }

    public async Task<string?> GetMVDirectUrl(VideoType vtype)
    {
        var qlist = await GetMVQuality(MVId);
        if (qlist == null)
        {
            return null;
        }

        if (!qlist.Contains(vtype))
        {
            return null;
        }

        var e = Crypto.NeteaseEncrypt(
            new NeteaseMVideoUrlRequest()
            {
                id = MVId,
                r = int.Parse(VideoQualityEnumConverter.ToNetease(vtype))
            }.BuildJsonString());

        var r = await _httpBuilder.DefPath("/weapi/song/enhance/play/mv/url", Method.Post)
            .AddQueryParameter("params", e["params"])
            .AddQueryParameter("encSecKey", e["encSecKey"])
            .ExecuteAsync();

        var json = JsonNode.Parse(r.Content);

        return json["data"]["url"].ToString();
    }
}