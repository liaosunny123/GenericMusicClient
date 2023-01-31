namespace MusicClient.Model;

public class Comment
{
    /// <summary>
    /// 评论者 Id
    /// </summary>
    public string Id { get; init; }

    /// <summary>
    /// 平台头像
    /// </summary>
    public string ProfileImg { get; init; }

    /// <summary>
    /// 平台名称
    /// </summary>
    public string NickName { get; init; }

    /// <summary>
    /// 评论
    /// </summary>
    public string CommentContent { get; init; }
}