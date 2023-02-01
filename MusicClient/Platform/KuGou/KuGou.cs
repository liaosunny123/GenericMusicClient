using MusicClient.Interface;
using MusicClient.Model;

namespace MusicClient.Platform;

public class KuGou : GenericClient
{
    private KuGou()
    {
    }

    public static KuGou Instance { get; } = new KuGou();

    public override SongInfo? GetById(string id)
    {
        throw new NotImplementedException();
    }

    public override List<SongInfo> GetByName(string name)
    {
        throw new NotImplementedException();
    }

    public override bool GetCursor(out IMusicListCursor musicListCursor,string name)
    {
        throw new NotImplementedException();
    }
}