using System.Text;
using System.Text.Json.Nodes;
using MusicClient.Model;
using MusicClient.Utils;
using RestSharp;

namespace MusicClient.Platform;

public class QQSongInfo : SongInfo
{
    
    public override async Task<string?> GetMVUrl(VideoType videoType = VideoType.WebUrl)
    {
        var id = "s0034zewczm";
        if (videoType == VideoType.WebUrl) return "https://y.qq.com/n/ryqq/mv/" + id;
        var response = (await new HttpBuilder("https://u.y.qq.com")
            .DefPath("/cgi-bin/musicu.fcg", Method.Post)
            .DefReferer("https://y.qq.com")
            .DefDefaultEdgeUa()
            .AddJsonBody(
                "{\"mvUrl\":{\"module\":\"music.stream.MvUrlProxy\",\"method\":\"GetMvUrls\",\"param\":{\"vids\":[\""+id+"\"],\"request_type\":10003,\"addrtype\":3,\"format\":264,\"maxFiletype\":60}}}")
            .ExecuteAsync()).Content!;
        if (String.IsNullOrWhiteSpace(response)) return null;
        var node = JsonNode.Parse(response);
        if (node["code"].ToString() == "500001") return null;
        var nodes = node["mvUrl"]["data"][id]["mp4"].AsArray();
        switch (videoType)
        {
            case VideoType.X4K:
                return node[6] == null ? null : nodes[6]["freeflow_url"].AsArray()[0].ToString();
            case VideoType.X2K:
                return node[5] == null ? null : nodes[5]["freeflow_url"].AsArray()[0].ToString();
            case VideoType.X1080P:
                return node[4] == null ? null : nodes[4]["freeflow_url"].AsArray()[0].ToString();
            case VideoType.X720P:
                return node[3] == null ? null : nodes[3]["freeflow_url"].AsArray()[0].ToString();
            case VideoType.X480P:
                return node[2] == null ? null : nodes[2]["freeflow_url"].AsArray()[0].ToString();
            case VideoType.X360P:
            case VideoType.XAuto:
                return node[1] == null ? null : nodes[1]["freeflow_url"].AsArray()[0].ToString();
            default:
                throw new ArgumentOutOfRangeException(nameof(videoType), videoType, "Not support VideoType");
        }
    }

    public override async Task<string?> GetRawLyrics(LyricType lyricType = LyricType.Origin)
    {
        var response
            = (await new HttpBuilder("https://c.y.qq.com")
                .DefPath("/lyric/fcgi-bin/fcg_query_lyric_new.fcg?format=json&inCharset=utf-8&outCharset=utf-8&notice=0&platform=yqq.json&songmid=" + this.Id, Method.Get)
                .DefHost("c.y.qq.com")
                .DefReferer("https://c.y.qq.com")
                .ExecuteAsync()).Content!;
        var node = JsonNode.Parse(response);
        return Encoding.UTF8.GetString(Convert.FromBase64String(node["lyric"].ToString()));
    }

    public override async Task<Comment?> GetComment()
    {
        return null;
    }

    public override async Task<string?> GetDirectUrl(MusicType musicType = MusicType.Auto)
    {
        return DirectUrl;
    }
}