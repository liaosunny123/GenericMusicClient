namespace MusicClient.Model;

public class NeteaseSongInfo : SongInfo
{
    public string AlbumId { get; init; }

    public override string? GetMVUrl()
    {
        throw new NotImplementedException();
    }

    public override string? GetRawLyrics()
    {
        throw new NotImplementedException();
    }

    public override TimeSpan? GetSongLength()
    {
        throw new NotImplementedException();
    }

    public override Comment? GetComment()
    {
        throw new NotImplementedException();
    }
}