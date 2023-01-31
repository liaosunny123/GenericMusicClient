using MusicClient.Platform;

namespace MusicClientTest;

public class Tests
{

    [Test]
    public void JsonParameterTest()
    {
        QQ.Instance.GetByName("寂寞烟火");
        Assert.Pass();
    }
}