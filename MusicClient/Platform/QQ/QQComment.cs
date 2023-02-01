namespace MusicClient.Model;

public class QQComment : Comment
{
    public override bool Next()
    {
        return false;
    }

    public override void NavigateTo(int pageIndex)
    {
        throw new NotImplementedException();
    }
}