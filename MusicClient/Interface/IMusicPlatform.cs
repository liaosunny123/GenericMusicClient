using MusicClient.Model;

namespace MusicClient.Interface;

public interface IMusicPlatform
{
    /// <summary>
    /// 搜索
    /// </summary>
    /// <param name="content">欲搜索内容</param>
    /// <returns></returns>
    public IEnumerator<SongInfo> Search(string content);

    /// <summary>
    /// 获取歌曲详细信息
    /// </summary>
    /// <param name="uri">歌曲地址</param>
    /// <returns></returns>
    public SongInfo GetSongDetail(Uri uri);

    /// <summary>
    /// 获取歌曲详细信息
    /// </summary>
    /// <param name="song">不完整的SongInfo</param>
    /// <returns></returns>
    public SongInfo GetSongDetail(SongInfo song);

    /// <summary>
    /// 获取歌曲评论
    /// </summary>
    /// <param name="uri">歌曲地址</param>
    /// <returns></returns>
    public Comment GetSongComment(Uri uri);

    /// <summary>
    /// 获取歌曲评论
    /// </summary>
    /// <param name="song">不完整的SongInfo</param>
    /// <returns></returns>
    public Comment GetSongComment(SongInfo song);
}