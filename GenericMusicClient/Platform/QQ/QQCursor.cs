using System.Text.Json.Nodes;
using GenericMusicClient.Interface;
using GenericMusicClient.Model;

namespace GenericMusicClient.Platform.QQ;

public class QQCursor : IMusicListCursor
{
    private string keyword;
    private int index;
    private int page;
    private List<SongInfo>? _songInfos = new List<SongInfo>();
    
    public QQCursor(string keyword)
    {
        this.keyword = keyword;
        this.index = 0;
        this.page = 1;
    }
    
    public List<SongInfo> GetByPage(int id)
    {
        JsonNode jsonNode = JsonObject.Parse(QQ.GetResponse(keyword,id).Result);
        JsonArray jsonArray = jsonNode["music.search.SearchCgiService"]["data"]["body"]["song"]["list"].AsArray();
        List<SongInfo> songInfos = new List<SongInfo>();
        foreach (var node in jsonArray)
        {
            if (QQ.GetDirectUrlByMid(node["mid"].ToString()) == null) continue;
            SongInfo songInfo = new QQSongInfo()
            {
                Id = node["mid"].ToString(),
                DirectUrl = QQ.GetDirectUrlByMid(node["mid"].ToString()).Result,
                CoverUrl = String.IsNullOrWhiteSpace(node["album"]["mid"].ToString()) ? null : "http://y.gtimg.cn/music/photo_new/T002R300x300M000" + node["album"]["mid"] + ".jpg",
                Platform = PlatformType.QQ,
                Name = node["name"].ToString(),
                Author = node["singer"].AsArray().ToList().Select(sp => sp["name"].ToString()).ToArray(),
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