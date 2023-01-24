using MusicClient.Model;
using MusicClient.Model.Instance;

namespace MusicClient.Platform;

public class KuGou : GenericClient
{
    private static readonly KuGou instance = new KuGou();

    private KuGou()
    {
    }

    public static KuGou Instance => instance;

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