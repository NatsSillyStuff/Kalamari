using Kalamari.Engine.DataModelV1;
using Raylib_cs;

namespace Kalamari.Engine.GUI;

public abstract partial class GUIObject(string Name) : Instance(Name)
{
    public virtual void GuiRender()
    {
        return;
    }
}