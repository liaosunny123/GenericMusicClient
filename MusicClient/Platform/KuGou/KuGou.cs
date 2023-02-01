using MusicClient.Interface;
using MusicClient.Model;

namespace MusicClient.Platform;

public class KuGou : GenericClient
{
    private KuGou()
    {
    }

    public static KuGou Instance { get; } = new KuGou();

    public override Task<SongInfo?> GetById(string id)
    {
        throw new NotImplementedException();
    }

    public override Task<List<SongInfo>> GetByName(string name)
    {
        throw new NotImplementedException();
    }

    public override bool GetCursor(out IMusicListCursor musicListCursor, string name)
    {
        throw new NotImplementedException();
    }
}