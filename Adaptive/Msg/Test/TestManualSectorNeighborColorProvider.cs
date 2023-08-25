using UnityEngine;

[ExecuteAlways]
public class TestManualSectorNeighborColorProvider : Poller<Color>
    , HierarchyMsg<TileNeighborColor>.IProvider
{
    public Color color = Color.magenta;

    protected override Color CurrentValue
    {
        get => color;
    }
    protected override Color LastValue
    {
        set => HierarchyMsg<TileNeighborColor>.Publish(this);
    }

    TileNeighborColor HierarchyMsg<TileNeighborColor>.IProvider.Provide()
    {
        return new TileNeighborColor { color = LastValue };
    }
}
