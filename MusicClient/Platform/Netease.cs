using MusicClient.Model;

namespace MusicClient.Platform;

public class Netease : GenericClient
{
    private Netease()
    {
    }

    public static Netease Instance { get; } = new Netease();

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