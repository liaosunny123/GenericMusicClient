using GenericMusicClient.Enums;
using GenericMusicClient.Interface;
using GenericMusicClient.Model;
using GenericMusicClient.Platform.KuGou;
using GenericMusicClient.Platform.Netease;
using GenericMusicClient.Platform.QQ;
using GenericMusicClient.Platform.XiaMi;

namespace GenericMusicClient;

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

    public async Task<SongInfo?> GetById(string id)
    {
        return await this._genericClient.GetById(id);
    }

    public async Task<List<SongInfo>> GetByName(string name)
    {
        return await this._genericClient.GetByName(name);
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