namespace Kalamari.Engine.DataModelV1;

public class DataModel : Instance
{
    public string PlaceName = "Game";
    public int PlaceId = 0;
    public DataModel(string placeName, int placeId) : base ("Game")
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