using UnityEngine;

[ExecuteAlways]
public class TestManualTileColorProvider : Poller<Color>
    , HierarchyMsg<TileColor>.IProvider
{
    public Color color = Color.magenta;

    protected override Color CurrentValue
    {
        get => color;
    }
    protected override Color LastValue
    {
        set => HierarchyMsg<TileColor>.Publish(this);
    }

    TileColor HierarchyMsg<TileColor>.IProvider.Provide()
    {
        return new TileColor { color = LastValue };
    }
}
