using MusicClient.Enums;
using MusicClient.Platform;

namespace MusicClientTest;

public class QQClientTest
{
    [Test]
    public void MidTest()
    {
        MusicClient.MusicClient musicClient = new (PlatformType.QQ);
        var list = musicClient.GetByName("寂寞烟火");
        Console.WriteLine(list[0].Name);
        Console.WriteLine(list[0].GetRawLyrics());
    }
}