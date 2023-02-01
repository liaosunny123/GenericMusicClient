namespace MusicClient.Model;

public abstract class Comment
{
    private string id;
    private string profileImg;
    private string nickName;
    private string commentContent;

    /// <summary>
    /// 评论者 Id
    /// </summary>
    public string Id => id;

    /// <summary>
    /// 平台头像
    /// </summary>
    public string ProfileImg => profileImg;

    /// <summary>
    /// 平台名称
    /// </summary>
    public string NickName => nickName;

    /// <summary>
    /// 评论
    /// </summary>
    public string CommentContent => commentContent;

    public abstract bool Next();

    public abstract void NavigateTo(int pageIndex);
}