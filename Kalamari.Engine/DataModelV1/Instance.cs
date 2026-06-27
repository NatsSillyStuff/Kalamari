namespace Kalamari.Engine.DataModelV1;

public abstract class Instance
{
    //todo: idiotproof this
    //hi its idiotproofed now
    public Instance[] Children = []; 
    public Instance Parent = null!;
    public string Name;

    public Instance(string name)
    {
        this.Name = name;
        Children = Array.Empty<Instance>();
    }
    /// <summary>
    /// FindFirstChild will grab the first child of an instance with a matching <code>name</code>.
    /// If it cannot find one, it will return <code>null.</code>
    /// </summary>
    /// <param name="name">Name of the instance you'd like to get.</param>
    /// <returns>Instance</returns>
    /// <exception cref="ArgumentNullException">Putting a null name will throw an exception.</exception>
    public Instance FindFirstChild(string name)
    {
        if (name == null)
        {
            throw new ArgumentNullException(nameof(name), "Expected Instance as parameter #1 when calling FindFirstChild, got null");
        }
        foreach (Instance child in Children)
        {
            if (child.Name == name)
            {
                return child;
            }
        }
        
        return null!; //Hi .NET compiler! As it turns out, when I type return null, I *know* it'll return null!
    }

    public virtual void Render()
    { 
        return;
    } 
    public void SetParent(Instance newParent)
    {
        if (newParent == null)
        {
            throw new ArgumentNullException(nameof(newParent), "Expected Instance as parameter #1 when calling SetParent, got null");
        }

        if (newParent.Children.Contains(this) || newParent == this.Parent)
        {
            throw new Exception("Attempted to add an instance to it's own parent. It's already there.");
        }
        
        Parent = newParent;
        // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
        newParent.Children = newParent.Children.Append(this).ToArray();
    }
}