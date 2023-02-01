using System.Text;
using System.Text.Json.Nodes;
using MusicClient.Enums;
using MusicClient.Interface;
using MusicClient.Model;
using MusicClient.Utils;
using RestSharp;

namespace MusicClient.Platform;

public class QQ : GenericClient
{
    private QQ()
    {
    }

    public static QQ Instance { get; } = new QQ();

    public override async Task<SongInfo?> GetById(string id)
    {
        var res
            = await new HttpBuilder("https://u.y.qq.com")
                .DefPath("/cgi-bin/musicu.fcg?format=json&data=%7B%22req_0%22%3A" +
                         "%7B%22module%22%3A%22music.pf_song_detail_svr%22%2C%22method%22%3" +
                         "A%22get_song_detail_yqq%22%2C%22param%22%3A%7B%22song_mid%22%3A%22" + id + "%22" +
                         "%7D%7D%2C%22comm%22%3A%7B%22uin%22%3A%221905222%22%2C%22format%22%3A%22json%22%2C" +
                         "%22ct%22%3A24%2C%22cv%22%3A0%7D%7D", Method.Get)
                .DefDefaultEdgeUa()
                .ExecuteAsync();
        var response = res.Content!;
        if (response == null) return null;
        JsonNode jsonNode = JsonObject.Parse(response);
        if (jsonNode["code"].ToString() != "0") return null;
        if (jsonNode["req_0"]["code"].ToString() != "0") return null;
        JsonNode node = jsonNode["req_0"]["data"]["track_info"];
        SongInfo songInfo = new QQSongInfo()
        {
            Id = id,
            DirectUrl = await GetDirectUrlByMid(id),
            CoverUrl = String.IsNullOrWhiteSpace(node["album"]["mid"].ToString()) ? null : "http://y.gtimg.cn/music/photo_new/T002R300x300M000" + node["album"]["mid"] + ".jpg",
            Platform = PlatformType.QQ,
            Name =   node["name"].ToString(),
            Author =  node["name"].AsArray().ToList().Select(sp => sp["name"].ToString()).ToArray(),
            Album = String.IsNullOrWhiteSpace(node["album"]["name"].ToString()) ? null : node["album"]["name"].ToString()
        };
        return songInfo;
    }

    public static async Task<string?> GetDirectUrlByMid(string mid)
    {
        var response
            = (await new HttpBuilder("https://u.y.qq.com")
                .DefPath("/cgi-bin/musicu.fcg?format=json&data=%7B%22req_0%22%3A%7B%22module%22%3A%22" +
                         "vkey.GetVkeyServer%22%2C%22method%22%3A%22CgiGetVkey%22%2C%22param%22%3A%7B%22" +
                         "guid%22%3A%22358840384%22%2C%22songmid%22%3A%5B%22" + mid + "%22%5D%2C%22songtype%22%3A%5B" +
                         "0%5D%2C%22uin%22%3A%22114514%22%2C%22loginflag%22%3A1%2C%22platform%22%3A%2" +
                         "220%22%7D%7D%2C%22comm%22%3A%7B%22uin%22%3A%221919810%22%2C%22format%22%3A%" +
                         "22json%22%2C%22ct%22%3A24%2C%22cv%22%3A0%7D%7D", Method.Get)
                .DefHost("u.y.qq.com")
                .DefDefaultEdgeUa()
                .ExecuteAsync()).Content;
        if (response == null) return null;
        JsonNode jsonNode = JsonObject.Parse(response);
        if (jsonNode["code"].ToString() != "0") return null;
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(jsonNode["req_0"]["data"]["sip"].AsArray()[0]);
        string purl = jsonNode["req_0"]["data"]["midurlinfo"].AsArray()[0]["purl"].ToString();
        if (String.IsNullOrWhiteSpace(purl)) return null;
        return stringBuilder.Append(purl).ToString();
    }
    
    public override async Task<List<SongInfo>> GetByName(string name)
    {
        JsonNode jsonNode = JsonObject.Parse(await GetResponse(name,1));
        JsonArray jsonArray = jsonNode["music.search.SearchCgiService"]["data"]["body"]["song"]["list"].AsArray();
        List<SongInfo> songInfos = new List<SongInfo>();
        foreach (var node in jsonArray)
        {
            if (GetDirectUrlByMid(node["mid"].ToString()) == null) continue;
            SongInfo songInfo = new QQSongInfo()
            {
                Id = node["mid"].ToString(),
                DirectUrl = await GetDirectUrlByMid(node["mid"].ToString()),
                CoverUrl = String.IsNullOrWhiteSpace(node["album"]["mid"].ToString()) ? null : "http://y.gtimg.cn/music/photo_new/T002R300x300M000" + node["album"]["mid"] + ".jpg",
                Platform = PlatformType.QQ,
                Name = node["name"].ToString(),
                Author = node["singer"].AsArray().ToList().Select(sp => sp["name"].ToString()).ToArray(),
                Album = String.IsNullOrWhiteSpace(node["album"]["name"].ToString()) ? null :  node["album"]["name"].ToString()
            };
            songInfos.Add(songInfo);
        }
        return songInfos;
    }

    public override bool GetCursor(out IMusicListCursor musicListCursor,string name)
    {
        musicListCursor = new QQCursor(name);
        return true;
    }

    public static async Task<string> GetResponse(string name,int page)
        =>  (await new HttpBuilder("https://u.y.qq.com/cgi-bin/musicu.fcg")
                  .DefPath("", Method.Post)
                  .DefHost("u.y.qq.com")
                  .DefReferer("https://y.qq.com")
                  .DefDefaultEdgeUa()
                  .DefJsonParameter()
                  .AddJsonParameters("music.search.SearchCgiService")
                    .AddSubParameter("module","music.search.SearchCgiService")
                        .AddSubParameter("method","DoSearchForQQMusicDesktop")
                            .AddJsonParameters("param")
                                .AddSubParameter("query",name)
                                .AddSubParameter("num_per_page",5)
                                .AddSubParameter("search_type",0)
                                .AddSubParameter("page_num",page)
                                .EndAddJsonSubParameter()
                        .EndAddJsonSubParameter()
                    .EndAddJsonParameter()
                  .ExecuteAsync()).Content!;
}

