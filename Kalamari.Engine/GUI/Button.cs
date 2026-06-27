using Raylib_cs;

namespace Kalamari.Engine.GUI;

public class Button(string Name) : GUIObject(Name)
{
    public Rectangle guiRect;
    public Color BackgroundColor;
    public float BackgroundOpacity = 1f;
    private Texture2D guiTexture;
    private int testInt = 0;
    public event EventHandler Click;
    private bool CanClick = false;
    protected virtual void OnClick()
    {
        Click?.Invoke(this, EventArgs.Empty);
    }
    
    public override void GuiRender()
    {
        BackgroundColor.A = (byte) (BackgroundOpacity * 255);
        if (Raylib.GetMouseX() < guiRect.X + guiRect.Width && Raylib.GetMouseY() < guiRect.Y + guiRect.Height)
        {
            BackgroundColor.A = (byte) (BackgroundOpacity * 255/2);
            if (Raylib.IsMouseButtonDown(MouseButton.Left) && CanClick)
            {
                BackgroundColor.A = (byte) (BackgroundOpacity * 255/5);
                OnClick();
                CanClick = false;
            }
        }

        if (!Raylib.IsMouseButtonDown(MouseButton.Left))
        {
            CanClick = true;
        }
        guiTexture = Raylib.LoadTextureFromImage(Raylib.GenImageColor((int)guiRect.Width, (int)guiRect.Height, BackgroundColor));
        Raylib.DrawTexture(guiTexture, (int)guiRect.X, (int)guiRect.Y, Color.White);
        
    }
}