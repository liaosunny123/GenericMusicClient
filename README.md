# GenericMusicClient
[![NuGet](https://img.shields.io/nuget/vpre/GenericMusicClient?style=flat&label=NuGet&color=9866ca)](https://www.nuget.org/packages/GenericMusicClient/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/GenericMusicClient?style=flat&label=Downloads&color=42a5f5)](https://www.nuget.org/packages/GenericMusicClient/)  
GenericMusicClient covering QQMusic, Netease, KuGou and etc.  
C#网易云音乐,QQ音乐,酷狗等等多平台API。  
一个泛型的，集成了qq音乐，网易云，酷狗等在内的音乐NuGet库。  
# 项目构成  
- MusicClient：通用的音乐客户端  
- MusicListCursor：游标  
# 安装
方式一：NuGet搜索`GenericMusicClient`下载安装即可
方式二：使用Github Release的NuGet包并导入
# 使用  
以QQ音乐平台为例子：  
```csharp  
MusicClient musicClient = new (PlatformType.QQ);
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
```
输出为：
```
寂寞烟火   苏星婕
寂寞烟火   宣统
```
以网易云为例子：
```csharp
var s = "Hope";
        MusicClient mc = new(PlatformType.Netease);
        var r = mc.GetByName(s);
        foreach (var item in r)
        {
            Console.WriteLine(item.Name + ":" + item.DirectUrl);
        }
```
输出:
```
Hope:Sick Boy
Hope:?
安室奈美惠 - Hope（海贼王One Piece OP20）:2017十月动漫新番歌曲合集Vol.2
......
```
