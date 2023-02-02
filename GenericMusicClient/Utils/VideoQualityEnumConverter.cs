using GenericMusicClient.Model;

namespace GenericMusicClient.Utils;

public static class VideoQualityEnumConverter
{
    public static VideoType? FromNetease(string q)
    {
        return q switch
        {
            "240" => VideoType.X240P,
            "360" => VideoType.X360P,
            "480" => VideoType.X480P,
            "720" => VideoType.X720P,
            "1080" => VideoType.X1080P,
            _ => null
        };
    }

    public static string? ToNetease(VideoType v)
    {
        return v switch
        {
            VideoType.X240P => "240",
            VideoType.X360P => "360",
            VideoType.X480P => "480",
            VideoType.X720P => "720",
            VideoType.X1080P => "1080",
            _ => null
        };
    }
}