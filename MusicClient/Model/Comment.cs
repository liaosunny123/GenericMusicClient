namespace MusicClient.Model;

public abstract class Comment
{
    /// <summary>
    /// 评论者 Id
    /// </summary>
    public string Id { get; private set; }

    /// <summary>
    /// 平台头像
    /// </summary>
    public string ProfileImg { get; private set; }

    /// <summary>
    /// 平台名称
    /// </summary>
    public string NickName { get; private set; }

    /// <summary>
    /// 评论
    /// </summary>
    public string CommentContent { get; private set; }

    /// <summary>
    /// 子评论
    /// </summary>
    public Comment? SubComment { get; private set; }

    public abstract bool Next();

    public abstract void NavigateTo(int pageIndex);
}