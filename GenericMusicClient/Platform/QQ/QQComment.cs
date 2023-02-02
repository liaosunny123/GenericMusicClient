using GenericMusicClient.Model;

namespace GenericMusicClient.Platform.QQ;

public class QQComment : Comment
{
    public override bool Next()
    {
        return false;
    }

    public override void NavigateTo(int pageIndex)
    {
        
    }
}