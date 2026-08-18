using Raylib_cs;

namespace Kalamari.Engine.GUI;

public class Frame(string Name) : GUIObject(Name)
{
    public Rect guiRect
    {
        get;
        set
        {
            field = value;
            Raylib.UnloadTexture(guiTexture);
            guiTexture = Raylib.LoadTextureFromImage(Raylib.GenImageColor((int)value.Width, (int)value.Height, BackgroundColor));
            //guiTexture.Height = (int)value.Height;
            //guiTexture.Width = (int)value.Width;
        }
    }

    private Color _bgColor;
    public Color BackgroundColor
    {
        get => _bgColor;
        set
        {
            guiTexture = Raylib.LoadTextureFromImage(Raylib.GenImageColor((int)guiRect.X, (int)guiRect.Y, _bgColor));
            
            _bgColor = value;
        }
    }

    public float BackgroundOpacity
    {
        get;
        set
        {
            field = value;
            _bgColor.A = (byte) (value * 255);
        }
    }
    private Texture2D guiTexture;
    
    
    public override void GuiRender()
    {
        //_bgColor.A = (byte) (BackgroundOpacity * 255);
        Raylib.DrawTexture(guiTexture, (int)guiRect.X, (int)guiRect.Y, Color.White);
        foreach (GUIObject obj in Children)
        {
            obj.GuiRender();
        }
    }
}