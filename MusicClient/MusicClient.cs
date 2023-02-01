using MusicClient.Model;
using MusicClient.Enums;
using MusicClient.Interface;
using MusicClient.Platform;

namespace MusicClient;

public class MusicClient
{
    private GenericClient _genericClient;

    public MusicClient(PlatformType platformType)
    {
        this._genericClient = PrepareGenericClient(platformType);
    }
    
    private GenericClient PrepareGenericClient(PlatformType platform)
        => platform switch
        {
            PlatformType.Netease => Netease.Instance,
            PlatformType.KuGou => KuGou.Instance,
            PlatformType.QQ => QQ.Instance,
            PlatformType.XiaMi => XiaMi.Instance,
            _ => throw new NotSupportedException(
                "Do not support this platform type.\n" +
                "Please submit a issue at https://github.com/liaosunny123/GenericMusicClient if needed.")
        };

    public SongInfo? GetById(string id)
    {
        return this._genericClient.GetById(id);
    }

    public List<SongInfo> GetByName(string name)
    {
        return this._genericClient.GetByName(name);
    }

    public bool GetCursor(out IMusicListCursor musicListCursor,string name)
    {
        return this._genericClient.GetCursor(out musicListCursor,name);
    }

    public void SetPlatform(PlatformType platformType)
    {
        this._genericClient = PrepareGenericClient(platformType);
    }
}