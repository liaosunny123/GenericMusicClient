using MusicClient.Model;

namespace MusicClient.Interface;

/// <summary>
/// 分页游标
/// </summary>
public interface IMusicListCursor
{
    /// <summary>
    /// 根据页数获取某个特定分页的歌曲信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public List<SongInfo> GetByPage(int id);

    /// <summary>
    /// 下一首歌曲
    /// </summary>
    /// <returns>是否存在下一首歌曲</returns>
    public bool Next();

    /// <summary>
    /// 当前所在的歌曲，使用 Next 使属性被赋值为下一首歌曲
    /// </summary>
    public SongInfo CurrentSong { get; }
}