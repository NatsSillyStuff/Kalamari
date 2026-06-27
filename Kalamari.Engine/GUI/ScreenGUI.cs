using Kalamari.Engine.DataModelV1;

namespace Kalamari.Engine.GUI;

public class ScreenGUI : GUIObject
{
    public ScreenGUI(string name) : base(name)
    {
    }

    public override void GuiRender()
    {
        foreach (GUIObject obj in Children)
        {
            obj.GuiRender();
        }
    }
}