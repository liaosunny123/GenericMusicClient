using GenericMusicClient.Interface;
using GenericMusicClient.Model;

namespace GenericMusicClient.Platform.KuGou;

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