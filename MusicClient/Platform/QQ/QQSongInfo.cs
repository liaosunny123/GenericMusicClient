using System.Text;
using System.Text.Json.Nodes;
using MusicClient.Model;
using MusicClient.Utils;
using RestSharp;

namespace MusicClient.Platform;

public class QQSongInfo : SongInfo
{
    
    public override string? GetMVUrl(VideoType videoType = VideoType.WebUrl)
    {
        return null;
    }

    public override string? GetRawLyrics(LyricType lyricType = LyricType.Origin)
    {
        var response
            = new HttpBuilder("https://c.y.qq.com")
                .DefPath("/lyric/fcgi-bin/fcg_query_lyric_new.fcg?format=json&inCharset=utf-8&outCharset=utf-8&notice=0&platform=yqq.json&songmid=" + this.Id, Method.Get)
                .DefHost("c.y.qq.com")
                .DefReferer("https://c.y.qq.com")
                .Execute().Content!;
        var node = JsonObject.Parse(response);
        return Encoding.UTF8.GetString(Convert.FromBase64String(node["lyric"].ToString()));
    }

    public override Comment? GetComment()
    {
        throw new NotImplementedException();
    }

    public override string? GetDirectUrl(MusicType musicType = MusicType.Auto)
    {
        return DirectUrl;
    }
}