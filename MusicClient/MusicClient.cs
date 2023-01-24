using MusicClient.Model;
using MusicClient.Model.Instance;
using MusicClient.Platform;

namespace MusicClient;

public class MusicClient
{
    private GenericClient PrepareGenericClient(Model.Platform platform)
        => platform switch
        {
            Model.Platform.Netease => Netease.Instance,
            Model.Platform.KuGou => KuGou.Instance,
            Model.Platform.QQ => Qq.Instance,
            Model.Platform.XiaMi => XiaMi.Instance
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

    public SongInfo GetById(Model.Platform platform, string id)
    {
        throw new NotImplementedException();
    }

    public List<SongInfo> GetByName(Model.Platform platform, string name)
    {
        throw new NotImplementedException();
    }

    public bool GetCursor(Model.Platform platform, out IMusicListCursor musicListCursor)
    {
        throw new NotImplementedException();
    }
}