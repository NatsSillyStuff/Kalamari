using System.Diagnostics;
using Raylib_cs;

namespace Kalamari.Engine.Utils;

public enum AssetType
{
    Decal,
    Font,
    Audio
}

public enum FontTypes
{
    // ReSharper disable once InconsistentNaming
    BMFont,
    Truetype
}
public class Asset
{
    public AssetType Type { get; set; }
    public string Name { get; set; }
    public string Path { get; set; }

    public dynamic GetContents()
    {
        //Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory + "Content/" + Path + "/" + Name + ".fnt");    
        if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "Content/" + Path))
        {
            throw new FileNotFoundException(Path);
        }

        if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Content/" + Path + "/AssetManifest.cfg"))
        {
            throw new Exception("AssetManifest.cfg not found! Are you sure this is a Kalamari asset?");
        }

        Name = Path.Split("/")[Path.Split("/").Length - 1]; //TODO: This wastes time by executing the same code twice, can we find a better way?
        string Config = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "Content/" + Path + "/AssetManifest.cfg");
        string[] Cfg = Config.Split("\n");
        string Assettype = Cfg[0].Split("=")[1];
        
        switch (Assettype)
        {
            case "Font":
                string fontType = Cfg[1].Split("=")[1];
                switch (fontType)
                {
                    case "BMFont":
                        return Raylib.LoadFont(AppDomain.CurrentDomain.BaseDirectory + "Content/" + Path + "/" + Name + ".fnt");
                }
                break;
            case "Decal":
                throw new NotImplementedException("Decal not implemented");
                break;
            case "Audio":
                throw new NotImplementedException("Audio not implemented");
                break;
            default:
                throw new Exception($"Unknown asset type {Assettype}");
        }
        return null!;
    }
}