namespace GenericMusicClient.Model;

public enum VideoType
{
    /// <summary>
    /// 4K清晰度
    /// </summary>
    X4K,
    /// <summary>
    /// 2K清晰度
    /// </summary>
    X2K,
    /// <summary>
    /// 1080P清晰度
    /// </summary>
    X1080P,
    /// <summary>
    /// 720P清晰度
    /// </summary>
    X720P,
    /// <summary>
    /// 480P清晰度
    /// </summary>
    X480P,
    /// <summary>
    /// 360P清晰度
    /// </summary>
    X360P,
    /// <summary>
    /// 默认清晰度
    /// </summary>
    XAuto,
    /// <summary>
    /// 非直链，返回MV所在的网页地址
    /// </summary>
    WebUrl
}