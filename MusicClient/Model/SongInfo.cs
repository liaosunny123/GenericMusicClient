﻿using MusicClient.Enums;

namespace MusicClient.Model;

/// <summary>
/// 歌曲实例
/// </summary>
public abstract class SongInfo
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
    /// 歌曲附属的 MV 地址
    /// </summary>
    public abstract string? GetMVUrl();

    /// <summary>
    /// 纯文本歌词
    /// </summary>
    public abstract string? GetRawLyrics();

    /// <summary>
    /// 歌曲的名字
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// 歌曲的作者
    /// </summary>
    public string[] Author { get; init; }

    /// <summary>
    /// 歌曲的专辑
    /// </summary>
    public string? Album { get; init; }

    /// <summary>
    /// 歌曲的所属平台
    /// </summary>
    public PlatformType Platform { get; init; }

    /// <summary>
    /// 歌曲时长
    /// </summary>
    public abstract TimeSpan? GetSongLength();

    /// <summary>
    /// 歌曲的评论，尽量提供
    /// </summary>
    public abstract Comment? GetComment();

    /// <summary>
    /// 歌曲封面的 Url 直链地址
    /// </summary>
    public string? CoverUrl { get; init; }
}