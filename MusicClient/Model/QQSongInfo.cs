using MusicClient.Model;

namespace MusicClient.Platform;

public class QQSongInfo : SongInfo
{
    
    public override string? GetMVUrl()
    {
        throw new NotImplementedException();
    }

    public override string? GetRawLyrics()
    {
        throw new NotImplementedException();
    }

    public override Comment? GetComment()
    {
        throw new NotImplementedException();
    }
    
}