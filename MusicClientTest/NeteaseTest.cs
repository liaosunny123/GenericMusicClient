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
        MusicClient mc = new MusicClient(PlatformType.Netease);
        SongInfo r = mc.GetById(id).Result;
        TestContext.Out.WriteLine(r.Name + ":" + r.CoverUrl);
        Assert.Pass();
    }

    [Test]
    public void NeteaseGetLyric()
    {
        string id = "1333199956";
        MusicClient mc = new MusicClient(PlatformType.Netease);
        TestContext.Out.WriteLine(mc.GetById(id).Result.GetRawLyrics(LyricType.Origin).Result);
        TestContext.Out.WriteLine(mc.GetById(id).Result.GetRawLyrics(LyricType.Translation).Result);
    }

    [Test]
    public void NeteaseGetMVideoTest()
    {
        string id = "1333199956";
        MusicClient mc = new MusicClient(PlatformType.Netease);
        TestContext.Out.WriteLine(mc.GetById(id).Result.GetMVUrl(VideoType.WebUrl).Result);
        TestContext.Out.WriteLine(mc.GetById(id).Result.GetMVUrl(VideoType.X1080P).Result);
    }
}