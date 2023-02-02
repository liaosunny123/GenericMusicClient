﻿using GenericMusicClient.Interface;
using GenericMusicClient.Model;

namespace GenericMusicClient.Platform.XiaMi;

public class XiaMi : GenericClient
{
    private XiaMi()
    {
    }
    public static XiaMi Instance { get; } = new XiaMi();

    public override Task<SongInfo?> GetById(string id)
    {
        throw new NotImplementedException();
    }

    public override Task<List<SongInfo>> GetByName(string name)
    {
        throw new NotImplementedException();
    }

    public override bool GetCursor(out IMusicListCursor musicListCursor, string name)
    {
        throw new NotImplementedException();
    }
}