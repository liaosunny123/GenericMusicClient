using MusicClient.Model;
using MusicClient.Enums;
using MusicClient.Platform;

namespace MusicClient;

public class MusicClient
{
    private GenericClient PrepareGenericClient(PlatformType platform)
        => platform switch
        {
            PlatformType.Netease => Netease.Instance,
            PlatformType.KuGou => KuGou.Instance,
            PlatformType.QQ => QQ.Instance,
            PlatformType.XiaMi => XiaMi.Instance
        };

    public SongInfo GetById(string id)
    {
        throw new NotImplementedException();
    }

    public List<SongInfo> GetByName(string name)
    {
        throw new NotImplementedException();
    }

    public bool GetCursor(out IMusicListCursor musicListCursor)
    {
        throw new NotImplementedException();
    }

    public SongInfo GetById(PlatformType platform, string id)
    {
        throw new NotImplementedException();
    }

    public List<SongInfo> GetByName(PlatformType platform, string name)
    {
        throw new NotImplementedException();
    }

    public bool GetCursor(PlatformType platform, out IMusicListCursor musicListCursor)
    {
        throw new NotImplementedException();
    }
}