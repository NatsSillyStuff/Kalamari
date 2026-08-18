using Raylib_cs;

namespace Kalamari.Engine.GUI;

public class Button(string Name) : GUIObject(Name)
{
    public Rect guiRect
    {
        get;
        set
        {
            field = value;
            guiTexture = Raylib.LoadTextureFromImage(Raylib.GenImageColor((int)value.Width, (int)value.Height, BackgroundColor));
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
    private int testInt = 0;
    public event EventHandler Click;
    private bool CanClick = false;
    protected virtual void OnClick()
    {
        Click?.Invoke(this, EventArgs.Empty);
    }
    
    public override void GuiRender()
    {
        //BackgroundColor.A = (byte) (BackgroundOpacity * 255);
        if (Raylib.GetMouseX() < guiRect.X + guiRect.Width && Raylib.GetMouseY() < guiRect.Y + guiRect.Height)
        {
            BackgroundOpacity = (BackgroundOpacity / 2);
            if (Raylib.IsMouseButtonDown(MouseButton.Left) && CanClick)
            {
                BackgroundOpacity = (BackgroundOpacity / 5);
                OnClick();
                CanClick = false;
            }
        }

        if (!Raylib.IsMouseButtonDown(MouseButton.Left))
        {
            CanClick = true;
        }
       // guiTexture = Raylib.LoadTextureFromImage(Raylib.GenImageColor((int)guiRect.Width, (int)guiRect.Height, BackgroundColor));
        Raylib.DrawTexture(guiTexture, (int)guiRect.X, (int)guiRect.Y, Color.White);
        
    }
}