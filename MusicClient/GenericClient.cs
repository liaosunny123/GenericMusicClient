using MusicClient.Model;

namespace MusicClient;

/// <summary>
/// Client 接口
/// </summary>
public abstract class GenericClient
{
    /// <summary>
    /// 根据 Id 获取音乐的信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public abstract SongInfo GetById(string id);

    /// <summary>
    /// 根据 Name 获取音乐的信息
    /// 返回歌曲信息的列表，且若有分页那么只会获取第一页
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public abstract List<SongInfo> GetByName(string name);

    /// <summary>
    /// 获取游标，若支持游标返回True并传出游标
    /// </summary>
    /// <param name="musicListCursor"></param>
    /// <returns></returns>
    public abstract bool GetCursor(out IMusicListCursor musicListCursor);
}