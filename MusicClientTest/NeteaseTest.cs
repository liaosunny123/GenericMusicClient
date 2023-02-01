using MusicClient.Enums;

namespace MusicClientTest;

using MusicClient;

[TestFixture]
public class NeteaseTest
{
    [SetUp]
    public void SetUp()
    {
    }

    [Test]
    public void NeteaseSearchTest()
    {
        var s = "Hope";
        MusicClient mc = new(PlatformType.Netease);
        var r = mc.GetByName(s);
        TestContext.Out.WriteLine(r.Count);
        foreach (var item in r)
        {
            TestContext.Out.WriteLine(item.Name + ":" + item.DirectUrl);
        }

        Assert.Pass();
    }
}