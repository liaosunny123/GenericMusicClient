using System.Text.Json.Nodes;

namespace MusicClient.Model.Instance;

/// <summary>
/// 歌曲实例
/// </summary>
public class SongInfo
{
    /// <summary>
    /// 歌曲 Id，具有平台区分性
    /// </summary>
    public string Id { get; init; }

    /// <summary>
    /// 歌曲直链地址
    /// </summary>
    public string DirectUrl { get; init; }

    /// <summary>
    /// 歌曲封面的 Url 直链地址
    /// </summary>
    public string CoverUrl { get; init; }

    /// <summary>
    /// 歌曲附属的 MV 地址
    /// </summary>
    public string? TiedVideoUrl { get; init; }

    /// <summary>
    /// 纯文本歌词
    /// </summary>
    public string RawLyrics { get; init; }

    /// <summary>
    /// 歌曲的名字
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// 歌曲的作者
    /// </summary>
    public string Author { get; init; }

    /// <summary>
    /// 歌曲的专辑
    /// </summary>
    public string? Cd { get; init; }

    /// <summary>
    /// 歌曲的所属平台
    /// </summary>
    public Platform Platform { get; init; }

    /// <summary>
    /// 歌曲时长
    /// </summary>
    public TimeSpan SongeLength { get; init; }

    /// <summary>
    /// 歌曲的评论，尽量提供
    /// </summary>
    public Comment? Comment { get; init; }
}