using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using MusicClient.Enums;
using MusicClient.Model;
using MusicClient.Utils;
using RestSharp;

namespace MusicClient.Platform;

public class Netease : GenericClient
{
    private readonly HttpBuilder _httpBuilder = new("https://music.163.com");

    private Netease()
    {
    }

    public static Netease Instance { get; } = new Netease();

    public override SongInfo GetById(string id)
    {
        throw new NotImplementedException();
    }

    public override List<SongInfo> GetByName(string name)
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

        var json = JsonNode.Parse(r.Content);

        int.TryParse(json["result"]["songCount"].ToString(), out var count);
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
}