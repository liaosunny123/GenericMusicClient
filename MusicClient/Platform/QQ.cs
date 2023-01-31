using System.Text;
using System.Text.Json.Nodes;
using MusicClient.Enums;
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

    public override SongInfo? GetById(string id)
    {
        var response
            = new HttpBuilder("https://u.y.qq.com")
                .DefPath("/cgi-bin/musicu.fcg?format=json&data=%7B%22req_0%22%3A" +
                         "%7B%22module%22%3A%22music.pf_song_detail_svr%22%2C%22method%22%3" +
                         "A%22get_song_detail_yqq%22%2C%22param%22%3A%7B%22song_mid%22%3A%22" + id + "%22" +
                         "%7D%7D%2C%22comm%22%3A%7B%22uin%22%3A%221905222%22%2C%22format%22%3A%22json%22%2C" +
                         "%22ct%22%3A24%2C%22cv%22%3A0%7D%7D", Method.Get)
                .DefDefaultEdgeUa()
                .Execute().Content!;
        if (response == null) return null;
        JsonNode jsonNode = JsonObject.Parse(response);
        if (jsonNode["code"].ToString() != "0") return null;
        if (jsonNode["req_0"]["code"].ToString() != "0") return null;
        JsonNode node = jsonNode["req_0"]["data"]["track_info"];
        SongInfo songInfo = new SongInfo()
        {
            Id = id,
            DirectUrl = GetDirectUrlByMid(id),
            CoverUrl = String.IsNullOrWhiteSpace(node["album"]["mid"].ToString()) ? null : "http://y.gtimg.cn/music/photo_new/T002R300x300M000" + node["album"]["mid"] + ".jpg",
            Platform = PlatformType.QQ,
            MVUrl = null,
            RawLyrics = null,
            Name =   node["name"].ToString(),
            Author =  node["name"].AsArray().ToList().Select(sp => sp["name"].ToString()).ToArray(),
            SongLength = null,
            Comment = null,
            Album = String.IsNullOrWhiteSpace(node["album"]["name"].ToString()) ? null : node["album"]["name"].ToString()
        };
        return null;
    }

    public static string? GetDirectUrlByMid(string mid)
    {
        var response
            = new HttpBuilder("https://u.y.qq.com")
                .DefPath("/cgi-bin/musicu.fcg?format=json&data=%7B%22req_0%22%3A%7B%22module%22%3A%22" +
                         "vkey.GetVkeyServer%22%2C%22method%22%3A%22CgiGetVkey%22%2C%22param%22%3A%7B%22" +
                         "guid%22%3A%22358840384%22%2C%22songmid%22%3A%5B%22" + mid + "%22%5D%2C%22songtype%22%3A%5B" +
                         "0%5D%2C%22uin%22%3A%221443481947%22%2C%22loginflag%22%3A1%2C%22platform%22%3A%2" +
                         "220%22%7D%7D%2C%22comm%22%3A%7B%22uin%22%3A%2218585073516%22%2C%22format%22%3A%" +
                         "22json%22%2C%22ct%22%3A24%2C%22cv%22%3A0%7D%7D", Method.Get)
                .DefHost("u.y.qq.com")
                .DefDefaultEdgeUa()
                .Execute().Content;
        if (response == null) return null;
        JsonNode jsonNode = JsonObject.Parse(response);
        if (jsonNode["code"].ToString() != "0") return null;
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(jsonNode["req_0"]["data"]["sip"].AsArray()[0]);
        string purl = jsonNode["req_0"]["data"]["midurlinfo"].AsArray()[0]["purl"].ToString();
        if (String.IsNullOrWhiteSpace(purl)) return null;
        return stringBuilder.Append(purl).ToString();
    }
    
    public override List<SongInfo> GetByName(string name)
    {
        var response
            = new HttpBuilder("https://u.y.qq.com/cgi-bin/musicu.fcg")
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
                            .AddSubParameter("num_per_page",3)
                            .AddSubParameter("search_type",0)
                            .AddSubParameter("page_num",1)
                            .EndAddJsonSubParameter()
                        .EndAddJsonSubParameter()
                    .EndAddJsonParameter()
                .Execute().Content!;
        JsonNode jsonNode = JsonObject.Parse(response);
        JsonArray jsonArray = jsonNode["music.search.SearchCgiService"]["data"]["body"]["song"]["list"].AsArray();
        List<SongInfo> songInfos = new List<SongInfo>();
        foreach (var node in jsonArray)
        {
            if (GetDirectUrlByMid(node["mid"].ToString()) == null) continue;
            SongInfo songInfo = new SongInfo()
            {
                Id = node["mid"].ToString(),
                DirectUrl = GetDirectUrlByMid(node["mid"].ToString()),
                CoverUrl = String.IsNullOrWhiteSpace(node["album"]["mid"].ToString()) ? null : "http://y.gtimg.cn/music/photo_new/T002R300x300M000" + node["album"]["mid"] + ".jpg",
                Platform = PlatformType.QQ,
                MVUrl = null,
                RawLyrics = null,
                Name = node["name"].ToString(),
                Author = node["singer"].AsArray().ToList().Select(sp => sp["name"].ToString()).ToArray(),
                SongLength = null,
                Comment = null,
                Album = String.IsNullOrWhiteSpace(node["album"]["name"].ToString()) ? null :  node["album"]["name"].ToString()
            };
            songInfos.Add(songInfo);
        }
        return songInfos;
    }

    public override bool GetCursor(out IMusicListCursor musicListCursor,string name)
    {
        musicListCursor = new Cursor(name);
        return true;
    }
    
}

public class Cursor : IMusicListCursor
{
    private string keyword;
    private int index;
    private int page;
    private List<SongInfo>? _songInfos = new List<SongInfo>();
    public Cursor(string keyword)
    {
        this.keyword = keyword;
        this.index = 0;
        this.page = 1;
    }
    public List<SongInfo> GetByPage(int id)
    {
        var response
            = new HttpBuilder("https://u.y.qq.com/cgi-bin/musicu.fcg")
                .DefPath("", Method.Post)
                .DefHost("u.y.qq.com")
                .DefReferer("https://y.qq.com")
                .DefDefaultEdgeUa()
                    .DefJsonParameter()
                    .AddJsonParameters("music.search.SearchCgiService")
                        .AddSubParameter("module","music.search.SearchCgiService")
                        .AddSubParameter("method","DoSearchForQQMusicDesktop")
                            .AddJsonParameters("param")
                            .AddSubParameter("query",keyword)
                            .AddSubParameter("num_per_page",3)
                            .AddSubParameter("search_type",0)
                            .AddSubParameter("page_num",id)
                            .EndAddJsonSubParameter()
                        .EndAddJsonSubParameter()
                    .EndAddJsonParameter()
                .Execute().Content!;
        JsonNode jsonNode = JsonObject.Parse(response);
        JsonArray jsonArray = jsonNode["music.search.SearchCgiService"]["data"]["body"]["song"]["list"].AsArray();
        List<SongInfo> songInfos = new List<SongInfo>();
        foreach (var node in jsonArray)
        {
            if (QQ.GetDirectUrlByMid(node["mid"].ToString()) == null) continue;
            SongInfo songInfo = new SongInfo()
            {
                Id = node["mid"].ToString(),
                DirectUrl = QQ.GetDirectUrlByMid(node["mid"].ToString()),
                CoverUrl = String.IsNullOrWhiteSpace(node["album"]["mid"].ToString()) ? null : "http://y.gtimg.cn/music/photo_new/T002R300x300M000" + node["album"]["mid"] + ".jpg",
                Platform = PlatformType.QQ,
                MVUrl = null,
                RawLyrics = null,
                Name = node["name"].ToString(),
                Author = node["singer"].AsArray().ToList().Select(sp => sp["name"].ToString()).ToArray(),
                SongLength = null,
                Comment = null,
                Album = String.IsNullOrWhiteSpace(node["album"]["name"].ToString()) ? null :  node["album"]["name"].ToString()
            };
            songInfos.Add(songInfo);
        }
        this.index = 0;
        this.page = id;
        this._songInfos = songInfos;
        return songInfos;
    }

    public bool Next()
    {
        if (index < _songInfos.Count)
        {
            _current = _songInfos[index++];
            return true;
        }
        else
        {
            this.GetByPage(++page);
            if (_songInfos != null)
            {
                this.index = 0;
                _current = _songInfos[index];
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public SongInfo CurrentSong => _current;

    private SongInfo _current;
}
