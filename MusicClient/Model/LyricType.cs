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
    SingleTranslation,
    /// <summary>
    /// 音译翻译
    /// </summary>
    MusicTranslation,
}