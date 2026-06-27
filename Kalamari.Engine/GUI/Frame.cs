using Raylib_cs;

namespace Kalamari.Engine.GUI;

public class Frame(string Name) : GUIObject(Name)
{
    public Rect guiRect;
    public Color BackgroundColor;
    public float BackgroundOpacity = 1f;
    private Texture2D guiTexture;
    
    
    public override void GuiRender()
    {
        BackgroundColor.A = (byte) (BackgroundOpacity * 255);
        guiTexture = Raylib.LoadTextureFromImage(Raylib.GenImageColor((int)guiRect.Width, (int)guiRect.Height, BackgroundColor));
        Raylib.DrawTexture(guiTexture, (int)guiRect.X, (int)guiRect.Y, Color.White);
        foreach (GUIObject obj in Children)
        {
            obj.GuiRender();
        }
    }
}