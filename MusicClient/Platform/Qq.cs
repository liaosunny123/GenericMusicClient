﻿using MusicClient.Model;
using MusicClient.Model.Instance;

namespace MusicClient.Platform;

public class Qq : GenericClient
{
    private static readonly Qq instance = new Qq();
    private Qq(){}
    
    public static Qq Instance => instance;
    
    public override SongInfo GetById(string id)
    {
        throw new NotImplementedException();
    }

    public override List<SongInfo> GetByName(string name)
    {
        throw new NotImplementedException();
    }

    public override bool GetCursor(out IMusicListCursor musicListCursor)
    {
        throw new NotImplementedException();
    }
}