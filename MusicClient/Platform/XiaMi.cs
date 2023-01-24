using MusicClient.Model;
using MusicClient.Model.Instance;

namespace MusicClient.Platform;

public class XiaMi : GenericClient
{
    private static readonly XiaMi instance = new XiaMi();
    private XiaMi(){}
    
    public static XiaMi Instance => instance;
    
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