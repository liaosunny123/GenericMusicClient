using GenericMusicClient.Model;

namespace MusicClientTest;

public class QQClientTest
{
    [Test]
    public void MidTest()
    {
        GenericMusicClient.MusicClient musicClient = new (PlatformType.QQ);
        var list = musicClient.GetByName("寂寞烟火").Result;
        Console.WriteLine(list[0].Name);
        Console.WriteLine(list[0].GetRawLyrics().Result);
        Console.WriteLine(list[0].GetMVUrl(VideoType.X360P).Result);
    }
}