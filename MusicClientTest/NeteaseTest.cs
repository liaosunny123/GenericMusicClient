using MusicClient.Enums;
using MusicClient.Model;

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
        var r = mc.GetByName(s).Result;
        TestContext.Out.WriteLine(r.Count);
        foreach (var item in r)
        {
            TestContext.Out.WriteLine(item.Name + ":" + item.DirectUrl);
        }

        Assert.Pass();
    }

    [Test]
    public void NeteaseGetByIDTest()
    {
        string id = "1333199956";
        global::MusicClient.MusicClient mc = new global::MusicClient.MusicClient(PlatformType.Netease);
        SongInfo r = mc.GetById(id).Result;
        TestContext.Out.WriteLine(r.Name + ":" + r.CoverUrl);
        Assert.Pass();
    }
}