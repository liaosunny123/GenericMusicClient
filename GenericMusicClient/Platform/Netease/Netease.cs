
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using GenericMusicClient.Interface;
using GenericMusicClient.Model;
using GenericMusicClient.Utils;
using RestSharp;


namespace GenericMusicClient.Platform.Netease;

public class Netease : GenericClient
{
    private readonly HttpBuilder _httpBuilder = new("https://music.163.com");

    private Netease()
    {
    }

    public static Netease Instance { get; } = new Netease();

    public override async Task<SongInfo?> GetById(string id)
    {
        return GetMusicInfo(id).Result;
    }


    public override async Task<List<SongInfo>> GetByName(string name)
    {
        return Search(name).Result;
    }

    public override bool GetCursor(out IMusicListCursor musicListCursor, string name)
    {
        throw new NotImplementedException();
    }

    public async Task<List<SongInfo>> Search(string s)
    {
        if (string.IsNullOrWhiteSpace(s))
        {
            return new List<SongInfo>();
        }

        var e = Crypto.NeteaseEncrypt(
            JsonSerializer.Serialize(new NeteaseSearchRequest(nsr => { nsr.s = s; }),
                new JsonSerializerOptions()
                {
                    WriteIndented = false,
                    DefaultIgnoreCondition = JsonIgnoreCondition.Never
                }
            )
        );
        var r = await _httpBuilder.DefPath("/weapi/cloudsearch/get/web", Method.Post)
            .AddQueryParameter("params", e["params"])
            .AddQueryParameter("encSecKey", e["encSecKey"])
            .ExecuteAsync();
        if (String.IsNullOrWhiteSpace(r.Content)) return new List<SongInfo>();
        var json = JsonNode.Parse(r.Content);
        if (int.TryParse(json["result"]["songCount"].ToString(), out var count)) return new List<SongInfo>();
        var result = new List<SongInfo>();
        for (var i = 0; i < count; i++)
        {
            var cur = json["result"]?["songs"][i];
            result.Add(new NeteaseSongInfo()
            {
                Id = cur["id"].ToString(),
                Name = cur["name"].ToString(),
                Album = cur["al"]["name"].ToString(),
                AlbumId = cur["al"]["id"].ToString(),
                CoverUrl = cur["al"]["picUrl"].ToString(),
                Platform = PlatformType.Netease,
                Author = cur["ar"].AsArray().Select(i => i["name"].ToString()).ToArray(),
                DirectUrl = $"https://music.163.com/song/media/outer/url?id={cur["id"].ToString()}.mp3"
            });
        }

        return result;
    }


    public async Task<SongInfo> GetMusicInfo(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            return new NeteaseSongInfo();
        }

        var e = Crypto.NeteaseEncrypt(new NeteaseSongDetailRequest(id).BuildJsonString());

        var r = await _httpBuilder.DefPath("/weapi/v3/song/detail", Method.Post)
            .AddQueryParameter("params", e["params"])
            .AddQueryParameter("encSecKey", e["encSecKey"])
            .ExecuteAsync();
        if (String.IsNullOrWhiteSpace(r.Content)) return null;
        JsonNode json = JsonNode.Parse(r.Content);

        return new NeteaseSongInfo
        {
            Id = json["songs"][0]["id"].ToString(),
            Name = json["songs"][0]["name"].ToString(),
            Album = json["songs"][0]["al"]["name"].ToString(),
            AlbumId = json["songs"][0]["al"]["id"].ToString(),
            CoverUrl = json["songs"][0]["al"]["picUrl"].ToString(),
            Platform = PlatformType.Netease,
            Author = (from i in json["songs"][0]["ar"].AsArray()
                select i["name"].ToString()).ToArray(),
            DirectUrl = $"https://music.163.com/song/media/outer/url?id={json["songs"][0]["id"]}.mp3",
            MVId = ((json["songs"][0]["mv"].ToString() == "0") ? "-1" : json["songs"][0]["mv"].ToString())
        };
    }
}