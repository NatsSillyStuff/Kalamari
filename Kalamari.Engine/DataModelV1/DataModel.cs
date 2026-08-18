namespace Kalamari.Engine.DataModelV1;

public class DataModel : Instance
{
    public string PlaceName = "Root";
    public int PlaceId = 0;
    public DataModel(string placeName, int placeId) : base ("Root")
    {
        PlaceName = placeName;
        PlaceId = placeId;
    }

    public override void Render()
    {
        foreach (Instance inst in Children)
        {
            inst.Render();
        }
    }
}