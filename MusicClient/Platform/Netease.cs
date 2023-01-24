using MusicClient.Model;
using MusicClient.Model.Instance;

namespace MusicClient.Platform;

public class Netease : GenericClient
{
    private static readonly Netease instance = new Netease();
    private Netease(){}
    
    public static Netease Instance => instance;
    
    public override SongInfo GetById(string id)
    {
        throw new NotImplementedException();
    }

    public override List<SongInfo> GetByName(string name)
    {
        throw new NotImplementedException();
    }

    public override bool GetCursor(out IMusicListCursor musicListCursor)
    {
        throw new NotImplementedException();
    }
}