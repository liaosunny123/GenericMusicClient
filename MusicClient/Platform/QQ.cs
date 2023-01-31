using MusicClient.Model;

namespace MusicClient.Platform;

public class QQ : GenericClient
{
    private QQ()
    {
    }

    public static QQ Instance { get; } = new QQ();

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