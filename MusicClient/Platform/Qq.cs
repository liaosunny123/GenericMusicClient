using System.Text.Json.Nodes;
using MusicClient.Model;
using MusicClient.Model.Instance;
using MusicClient.Utils;
using RestSharp;

namespace MusicClient.Platform;

public class Qq : GenericClient
{
    private static readonly Qq instance = new Qq();
    private Qq(){}
    
    public static Qq Instance => instance;
    
    public override SongInfo GetById(string id)
    {
        throw new NotImplementedException();
    }

    private string GetDirectUrlByMid(string mid)
    {
        
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
                .Excute().Content!;
        JsonNode jsonNode = JsonObject.Parse(response);
        JsonArray jsonArray = jsonNode["music.search.SearchCgiService"]["data"]["body"]["song"]["list"].AsArray();
        List<SongInfo> songInfos = new List<SongInfo>();
        foreach (var node in jsonArray)
        {
            SongInfo songInfo = new SongInfo()
            {
                Platform = Model.Platform.QQ,
                Author = "12",
                Cd = String.IsNullOrWhiteSpace(node["album"]["name"].ToString()) ? null :  node["album"]["name"].ToString(),
                Id = 
            };
        }
        return null;
    }

    public override bool GetCursor(out IMusicListCursor musicListCursor)
    {
        throw new NotImplementedException();
    }
}