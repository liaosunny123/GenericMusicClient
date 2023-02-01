using MusicClient.Enums;
using MusicClient.Platform;

namespace MusicClientTest;

public class Tests
{

    [Test]
    public void JsonParameterTest()
    {
        MusicClient.MusicClient musicClient = new (PlatformType.QQ);
        var list = musicClient.GetByName("寂寞烟火");
        list.ForEach(sp =>
        {
            Console.Write(sp.Name+"   ");
            sp.Author.ToList().ForEach(sp =>
            {
                Console.Write(sp);
            });
            Console.WriteLine();
        });
        Assert.Pass();
    }
}