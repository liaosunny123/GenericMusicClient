namespace MusicClient.Model;

public enum LyricType
{
    /// <summary>
    /// 原版歌词，不做干预
    /// </summary>
    Origin,

    /// <summary>
    /// 单层翻译，如英语+中文
    /// </summary>
    Translation,

    /// <summary>
    /// 音译翻译
    /// </summary>
    Transliteration,
}