using System.Text;
using System.Text.Json.Nodes;
using GenericMusicClient.Model;
using GenericMusicClient.Utils;
using RestSharp;

namespace GenericMusicClient.Platform.QQ;

public class QQSongInfo : SongInfo
{
    
    public override async Task<string?> GetMVUrl(VideoType videoType = VideoType.WebUrl)
    {
        if (videoType == VideoType.WebUrl) return "https://y.qq.com/n/ryqq/mv/" + Id;
        var response = (await new HttpBuilder("https://u.y.qq.com")
            .DefPath("/cgi-bin/musicu.fcg", Method.Post)
            .DefReferer("https://y.qq.com")
            .DefDefaultEdgeUa()
            .DefJsonParameter()
            .AddJsonParameters("mvUrl")
                .AddSubParameter("module","music.stream.MvUrlProxy")
                .AddSubParameter("method","GetMvUrls")
                .AddJsonParameters("param")
                    .AddSubParameter("vids",new List<string>(){Id})
                    .AddSubParameter("request_type",10003)
                    .AddSubParameter("addrtype",3)
                    .AddSubParameter("format",264)
                    .AddSubParameter("maxFiletype",60)
                    .EndAddJsonSubParameter()
                .EndAddJsonSubParameter()
            .EndAddJsonParameter()
            .ExecuteAsync()).Content!;
        if (String.IsNullOrWhiteSpace(response)) return null;
        var node = JsonNode.Parse(response);
        if (node["code"].ToString() == "500001") return null;
        var nodes = node["mvUrl"]["data"][Id]["mp4"].AsArray();
        if (nodes[1]["freeflow_url"].ToString() == "[]") return null;
        switch (videoType)
        {
            case VideoType.X4K:
                return nodes[6] == null ? null : nodes[6]["freeflow_url"].AsArray()[0].ToString();
            case VideoType.X2K:
                return nodes[5] == null ? null : nodes[5]["freeflow_url"].AsArray()[0].ToString();
            case VideoType.X1080P:
                return nodes[4] == null ? null : nodes[4]["freeflow_url"].AsArray()[0].ToString();
            case VideoType.X720P:
                return nodes[3] == null ? null : nodes[3]["freeflow_url"].AsArray()[0].ToString();
            case VideoType.X480P:
                return nodes[2] == null ? null : nodes[2]["freeflow_url"].AsArray()[0].ToString();
            case VideoType.X360P:
            case VideoType.XAuto:
                return nodes[1] == null ? null : nodes[1]["freeflow_url"].AsArray()[0].ToString();
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