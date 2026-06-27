using Raylib_cs;

namespace Kalamari.Engine.Utils;

public static class AssetLoader
{
    public static Asset LoadAssetFromURI(string uri)
    {
        Asset idk = new()
        {
            Path = uri.Split("kla://")[1],
            Name = uri.Split("/").Last()
        };
        try
        {
            idk.GetContents();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null!;
        }
        return idk;
    }
}